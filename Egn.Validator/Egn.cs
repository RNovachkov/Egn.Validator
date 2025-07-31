using Egn.Validator.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Egn.Validator
{
    public static class Egn
    {
        private static readonly int[] weights = { 2, 4, 8, 5, 10, 9, 7, 3, 6 };

        /// <summary>
        /// Validates Bulgarian personal Identification number (ЕГН).
        /// </summary>
        /// <param name="egn">The Identification number you are trying to validate.</param>
        /// <returns>
        /// A boolean, with true for valid and false for invalid Identification number.
        /// </returns>
        public static bool isValid(string egn)
        {
            if (string.IsNullOrEmpty(egn))
            {
                return false;
            }

            if (egn.Length != 10 || !Regex.IsMatch(egn, @"\d{10}"))
            {
                return false;
            }

            var (year, month, day) = getDate(egn);

            if (!CheckDate(day, month, year))
            {
                return false;
            }

            if (!IsValidChecksum(egn))
            {
                return false;
            }

            return true;
        }

        public static bool TryGetInfo(string egn, out EgnInfo? value)
        {
            if (!isValid(egn))
            {
                value = null;
                return false;
            }

            value = new EgnInfo();

            var (year, month, day) = getDate(egn);
            value.Birthday = new DateOnly(year, month, day);
            value.Age = DateTime.Now - value.Birthday.ToDateTime(TimeOnly.MinValue);

            var gender = int.Parse(egn.Substring(8, 1)) % 2;
            value.Gender = gender == 0 ? (byte)0 : (byte)1;

            var regionCode = int.Parse(egn.Substring(6, 3));
            List<Region> regions = Regions.List.ToList() ?? new List<Region>();
            regions.Sort((p1, p2) => p1.Code.CompareTo(p2.Code));
            value.Region = regions.FirstOrDefault(r => r.Code >= regionCode) ?? Regions.List.Last();
            regions.Sort((p1, p2) => p2.Code.CompareTo(p1.Code));
            var minRegionCode = regions.FirstOrDefault(r => r.Code <= regionCode)?.Code;
            if (minRegionCode is not null)
            {
                value.BornBefor = (regionCode - (int)minRegionCode - 1) / 2;
            }

            return true;
        }

        private static (int year, int month, int day) getDate(string egn)
        {
            var year = int.Parse(egn.Substring(0, 2));
            var month = int.Parse(egn.Substring(2, 2));
            var day = int.Parse(egn.Substring(4, 2));
            if (month > 40)
            {
                month -= 40;
                year += 2000;
            }
            else if (month > 20)
            {
                month -= 20;
                year += 1800;
            }
            else
            {
                year += 1900;
            }
            return (year, month, day);
        }

        private static bool IsValidChecksum(string ssn)
        {
            var checksum = int.Parse(ssn.Substring(9, 1));
            var ssnSum = 0;
            for (int i = 0; i < 9; i++)
            {
                ssnSum += int.Parse(ssn.Substring(i, 1)) * weights[i];
            }
            var validChecksum = ssnSum % 11;
            validChecksum = validChecksum == 10 ? 0 : validChecksum;
            return checksum == validChecksum;
        }

        private static bool CheckDate(int day, int month, int year)
        {
            DateTime _date;
            return DateTime.TryParseExact($"{day}.{month}.{year}", "d.M.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out _date) && _date < DateTime.Now;
        }
    }
}
