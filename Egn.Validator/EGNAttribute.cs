using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Egn.Validator
{
    public class EGNAttribute : ValidationAttribute
    {
        private string _errorMessage = "{0} is invalid.";
        private readonly int[] _weights = new int[] { 2, 4, 8, 5, 10, 9, 7, 3, 6 };

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (validationContext.ObjectInstance != null)
            {
                string currentFieldValue = value != null ? value.ToString() : string.Empty;

                if (!string.IsNullOrEmpty(currentFieldValue))
                {
                    if (currentFieldValue.Length != 10 || !Regex.IsMatch(currentFieldValue, @"^[0-9]{10}$"))
                    {
                        return ReturnError(validationContext);
                    }

                    var year = int.Parse(currentFieldValue.Substring(0, 2));
                    var month = int.Parse(currentFieldValue.Substring(2, 2));
                    var day = int.Parse(currentFieldValue.Substring(4, 2));
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

                    if (!CheckDate(day, month, year))
                    {
                        return ReturnError(validationContext);
                    }

                    if (!this.IsValidChecksum(currentFieldValue))
                    {
                        return ReturnError(validationContext);
                    }
                }
            }
            return ValidationResult.Success;
        }

        private ValidationResult ReturnError(ValidationContext validationContext)
        {
            var message = String.Format(_errorMessage, validationContext.DisplayName);
            if (!string.IsNullOrEmpty(this.ErrorMessage))
            {
                message = String.Format(this.ErrorMessage, validationContext.DisplayName);
            }
            return new ValidationResult(message, new[] { validationContext.MemberName });
        }

        private bool IsValidChecksum(string ssn)
        {
            var checksum = int.Parse(ssn.Substring(9, 1));
            var ssnSum = 0;
            for (int i = 0; i < 9; i++)
            {
                ssnSum += int.Parse(ssn.Substring(i, 1)) * _weights[i];
            }
            var validChecksum = ssnSum % 11;
            validChecksum = validChecksum == 10 ? 0 : validChecksum;
            return checksum == validChecksum;
        }

        private bool CheckDate(int day, int month, int year)
        {
            DateTime _date;
            return DateTime.TryParseExact($"{day}.{month}.{year}", "d.M.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out _date) && _date < DateTime.Now;
        }
    }
}