using Egn.Validator;

namespace Egn.Validator.Tests
{
    public class EgnTest
    {
        [Theory]
        [InlineData("", false)] // empty string
        [InlineData("801015391", false)] // 9 digits
        [InlineData("80101539111", false)] // 11 digits
        [InlineData("9948094663", false)] // invalid date
        [InlineData("8010153912", false)] // invalid checksum
        [InlineData("0301115350", true)]
        [InlineData("2311051940", true)]
        [InlineData("2810243280", true)]
        [InlineData("3701179099", true)]
        [InlineData("8010153911", true)]
        public void IsEgnValidCorrect(string input, bool expected)
        {
            var result = Egn.isValid(input);

            Assert.Equal(expected, result);
        }
    }
}