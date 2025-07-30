using Egn.Validator;
using System.ComponentModel.DataAnnotations;

namespace Egn.Validator.Tests
{
    public class EGNAttributeTest
    {
        [Fact]
        public void IsValid_WhenValidEgnOrEmptyStringPassed_ReturnsSuccess()
        {
            // 0301115350
            var model = new TestModel { Egn = "0301115350" };

            var context = new ValidationContext(model)
            {
                MemberName = nameof(TestModel.Egn)
            };

            var property = typeof(TestModel).GetProperty(nameof(TestModel.Egn))!;
            var attribute = property.GetCustomAttributes(typeof(EGNAttribute), false)[0] as ValidationAttribute;

            var result = attribute!.GetValidationResult(model.Egn, context);

            Assert.Equal(ValidationResult.Success, result);

            // 2311051940
            model = new TestModel { Egn = "2311051940" };

            result = attribute!.GetValidationResult(model.Egn, context);

            Assert.Equal(ValidationResult.Success, result);

            // 2810243280
            model = new TestModel { Egn = "2810243280" };

            result = attribute!.GetValidationResult(model.Egn, context);

            Assert.Equal(ValidationResult.Success, result);

            // 3701179099
            model = new TestModel { Egn = "3701179099" };

            result = attribute!.GetValidationResult(model.Egn, context);

            Assert.Equal(ValidationResult.Success, result);

            // 8010153911
            model = new TestModel { Egn = "8010153911" };

            result = attribute!.GetValidationResult(model.Egn, context);

            Assert.Equal(ValidationResult.Success, result);

            // empty string
            model = new TestModel { Egn = "" };

            result = attribute!.GetValidationResult(model.Egn, context);

            Assert.Equal(ValidationResult.Success, result);
        }

        [Fact]
        public void IsInvalid_WhenInvalidEgnPassed_ReturnsError()
        {
            // 801015391 - 9 digits
            var model = new TestModel { Egn = "801015391" };

            var context = new ValidationContext(model)
            {
                MemberName = nameof(TestModel.Egn)
            };

            var property = typeof(TestModel).GetProperty(nameof(TestModel.Egn))!;
            var attribute = property.GetCustomAttributes(typeof(EGNAttribute), false)[0] as ValidationAttribute;

            var result = attribute!.GetValidationResult(model.Egn, context);

            Assert.NotNull(result);
            Assert.Contains(String.Format("{0} is invalid.", nameof(model.Egn)), result!.ErrorMessage);

            // 80101539111 - 11 digits
            model = new TestModel { Egn = "80101539111" };
            result = attribute!.GetValidationResult(model.Egn, context);
            Assert.Contains(String.Format("{0} is invalid.", nameof(model.Egn)), result!.ErrorMessage);

            // 9948094663 - invalid date
            model = new TestModel { Egn = "9948094663" };
            result = attribute!.GetValidationResult(model.Egn, context);
            Assert.Contains(String.Format("{0} is invalid.", nameof(model.Egn)), result!.ErrorMessage);

            // 8010153912 - invalid checksum
            model = new TestModel { Egn = "8010153912" };
            result = attribute!.GetValidationResult(model.Egn, context);
            Assert.Contains(String.Format("{0} is invalid.", nameof(model.Egn)), result!.ErrorMessage);
        }

        [Fact]
        public void IsInvalid_WhenInvalidEgnPassed_ReturnsCustomError()
        {
            // 801015391 - 9 digits
            var model = new TestModelWithMessage { Egn = "801015391" };

            var context = new ValidationContext(model)
            {
                MemberName = nameof(TestModel.Egn)
            };

            var property = typeof(TestModelWithMessage).GetProperty(nameof(TestModelWithMessage.Egn))!;
            var attribute = property.GetCustomAttributes(typeof(EGNAttribute), false)[0] as ValidationAttribute;

            var result = attribute!.GetValidationResult(model.Egn, context);

            Assert.NotNull(result);
            Assert.Contains(TestConstants.CustomErrorMessage, result!.ErrorMessage);

            // 80101539111 - 11 digits
            model = new TestModelWithMessage { Egn = "80101539111" };
            result = attribute!.GetValidationResult(model.Egn, context);
            Assert.Contains(TestConstants.CustomErrorMessage, result!.ErrorMessage);

            // 9948094663 - invalid date
            model = new TestModelWithMessage { Egn = "9948094663" };
            result = attribute!.GetValidationResult(model.Egn, context);
            Assert.Contains(TestConstants.CustomErrorMessage, result!.ErrorMessage);

            // 8010153912 - invalid checksum
            model = new TestModelWithMessage { Egn = "8010153912" };
            result = attribute!.GetValidationResult(model.Egn, context);
            Assert.Contains(TestConstants.CustomErrorMessage, result!.ErrorMessage);
        }

        [Fact]
        public void IsValid_WithNullEgnPassed_ReturnsSuccess()
        {
            // null
            var model = new TestModelNull { Egn = null };
            var context = new ValidationContext(model)
            {
                MemberName = nameof(TestModelNull.Egn)
            };

            var property = typeof(TestModelNull).GetProperty(nameof(TestModelNull.Egn))!;
            var attribute = property.GetCustomAttributes(typeof(EGNAttribute), false)[0] as ValidationAttribute;

            var result = attribute!.GetValidationResult(model.Egn, context);

            // Assert
            Assert.Equal(ValidationResult.Success, result);
        }
    }
}
