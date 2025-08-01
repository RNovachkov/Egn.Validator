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
                value.BornBefore = (regionCode - (int)minRegionCode - 1) / 2;
            }

            return true;
        }

        /// <summary>
        /// Generates a Bulgarian EGN (Единен граждански номер) number
        /// </summary>
        /// <param name="day">Day of birth (1-31, 0 for random)</param>
        /// <param name="mon">Month of birth (1-12, 0 for random)</param>
        /// <param name="year">Year of birth (1800-2099, 0 for random)</param>
        /// <param name="gender">Sex (1 = male, 2 = female, 0 = random)</param>
        /// <param name="region">Region key (null/empty for random)</param>
        /// <returns>Generated EGN string or throw if generation failed</returns>
        public static string Generate(uint day = 0, uint mon = 0, uint year = 0, byte gender = 0, string? region = null)
        {
            if (!hasValidDateParameters(day,mon,year))
            {
                throw new ArgumentException("One or more invalid date parameters.");
            }

            bool started = false;

            uint gDay = 0;
            uint gMon = 0;
            uint gYear = 0;

            do
            {
                started = true;

                gYear = year == 0 ? (uint)_random.Next(1800, 2100) : year;
                gMon = mon == 0 ? (uint)_random.Next(1, 13) : mon;
                gDay = day == 0 ? (uint)_random.Next(1, 32) : day;

            } while (!(hasValidDateParameters(gDay, gMon, gYear) && started));


            int cent = (int)gYear - ((int)gYear % 100);

            switch (cent)
            {
                case 1800:
                    gMon += 20;
                    break;
                case 2000:
                    gMon += 40;
                    break;
            }


            int? gRegion = region == null ? null : Regions.List.SingleOrDefault(r => r.Key == region).Code;
            if (gRegion == null)
            {
                var index = _random.Next(1, Regions.List.Length);
                gRegion = Regions.List[index].Code;
            }


            if (gender == 1 && (gRegion % 2 != 0))
                gRegion--;

            if (gender == 2 && (gRegion % 2 == 0))
                gRegion++;


            string egn = $"{(gYear - cent):D2}{gMon:D2}{gDay:D2}{gRegion:D3}";


            int egnsum = 0;
            for (int i = 0; i < 9; i++)
            {
                int digit = int.Parse(egn[i].ToString());
                egnsum += digit * weights[i];
            }

            int validChecksum = egnsum % 11;
            if (validChecksum == 10)
                validChecksum = 0;

            egn += validChecksum.ToString();

            return egn;
        }

        private static readonly Random _random = new();

        private static bool hasValidDateParameters(uint day = 0, uint mon = 0, uint year = 0)
        {
            if (mon > 12)
            {
                return false;
            }

            if (new uint[] { 2, 4, 6, 9, 11 }.Contains(mon) && day > 30)
            {
                return false;
            }

            if (day > 31)
            {
                return false;
            }

            if (mon == 2 && year % 4 != 0 && day > 28)
            {
                return false;
            }

            if (mon == 2 && day > 29)
            {
                return false;
            }

            if ((year < 1800 || year > 2099) && year != 0)
            {
                return false;
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
            return DateTime.TryParseExact($"{day}.{month}.{year}", "d.M.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out var _date);
        }
    }
}
