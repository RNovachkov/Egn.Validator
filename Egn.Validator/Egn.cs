using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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

            var year = int.Parse(egn.Substring(0, 2));
            var month = int.Parse(egn.Substring(2, 2));
            var day = int.Parse(egn.Substring(4, 2));
            if (month > 40)
            {
                if (!Egn.CheckDate(day, month - 40, year))
                {
                    return false;
                }
            }
            else if (month > 20)
            {
                if (!Egn.CheckDate(day, month - 20, year))
                {
                    return false;
                }
            }
            else if (!Egn.CheckDate(day, month, year))
            {
                return false;
            }

            var checksum = int.Parse(egn.Substring(9, 1));
            var egnSum = 0;
            for (int i = 0; i < 9; i++)
            {
                egnSum += int.Parse(egn.Substring(i, 1)) * Egn.weights[i];
            }
            var validChecksum = egnSum % 11;
            validChecksum = validChecksum == 10 ? 0 : validChecksum;
            if (checksum != validChecksum)
            {
                return false;
            }
            return true;
        }

        private static bool CheckDate(int day, int month, int year)
        {
            DateTime _date;
            return DateTime.TryParseExact($"{day}.{month}.{year}", "dd.M.yy", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out _date) && _date < DateTime.Now;
        }
    }
}
