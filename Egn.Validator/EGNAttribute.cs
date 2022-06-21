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
                    if (currentFieldValue.Length != 10 || !Regex.IsMatch(currentFieldValue, @"\d{10}"))
                    {
                        return ReturnError(validationContext);
                    }

                    var year = int.Parse(currentFieldValue.Substring(0, 2));
                    var month = int.Parse(currentFieldValue.Substring(2, 2));
                    var day = int.Parse(currentFieldValue.Substring(4, 2));
                    if (month > 40)
                    {
                        if (!CheckDate(day, month - 40, year))
                        {
                            return ReturnError(validationContext);
                        }
                    }
                    else if (month > 20)
                    {
                        if (!CheckDate(day, month - 20, year))
                        {
                            return ReturnError(validationContext);
                        }
                    }
                    else if (!CheckDate(day, month, year))
                    {
                        return ReturnError(validationContext);
                    }

                    var checksum = int.Parse(currentFieldValue.Substring(9, 1));
                    var egnSum = 0;
                    for (int i = 0; i < 9; i++)
                    {
                        egnSum += int.Parse(currentFieldValue.Substring(i, 1)) * _weights[i];
                    }
                    var validChecksum = egnSum % 11;
                    validChecksum = validChecksum == 10 ? 0 : validChecksum;
                    if (checksum != validChecksum)
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

        private bool CheckDate(int day, int month, int year)
        {
            DateTime _date;
            return DateTime.TryParseExact($"{day}.{month}.{year}", "dd.M.yy", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out _date) && _date < DateTime.Now;
        }
    }
}