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
        public void IsGettingEgnInfoTry(string input, bool expected)
        {
            var result = Egn.TryGetInfo(input, out var info);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("0301115350", "EGN_VAL.REGIONS.RUSE")]
        [InlineData("2311051940", "EGN_VAL.REGIONS.VRATSA")]
        [InlineData("2810243280", "EGN_VAL.REGIONS.MONTANA")]
        [InlineData("3701179099", "EGN_VAL.REGIONS.YAMBOL")]
        [InlineData("8010153911", "EGN_VAL.REGIONS.PERNIK")]
        public void IsGettingEgnInfoRegionKey(string input, string expected)
        {
            var result = Egn.TryGetInfo(input, out var info);
            Assert.Equal(expected, info.Region.Key);
        }

        [Theory]
        [InlineData("0301115350", 1903)]
        [InlineData("2311051940", 1923)]
        [InlineData("2810243280", 1928)]
        [InlineData("3701179099", 1937)]
        [InlineData("8010153911", 1980)]
        [InlineData("0650071580", 2006)]
        public void IsGettingEgnInfoBirthdayYear(string input, int expected)
        {
            var result = Egn.TryGetInfo(input, out var info);
            Assert.Equal(expected, info.Birthday.Year);
        }

        [Theory]
        [InlineData("0301115350", 1)]
        [InlineData("2311051940", 11)]
        [InlineData("2810243280", 10)]
        [InlineData("3701179099", 1)]
        [InlineData("8010153911", 10)]
        [InlineData("0650071580", 10)]
        public void IsGettingEgnInfoBirthdayMonth(string input, int expected)
        {
            var result = Egn.TryGetInfo(input, out var info);
            Assert.Equal(expected, info.Birthday.Month);
        }

        [Theory]
        [InlineData("0301115350", 11)]
        [InlineData("2311051940", 5)]
        [InlineData("2810243280", 24)]
        [InlineData("3701179099", 17)]
        [InlineData("8010153911", 15)]
        [InlineData("0650071580", 7)]
        public void IsGettingEgnInfoBirthdayDay(string input, int expected)
        {
            var result = Egn.TryGetInfo(input, out var info);
            Assert.Equal(expected, info.Birthday.Day);
        }

        [Theory]
        [InlineData("0301115350", 3)]
        [InlineData("2311051940", 5)]
        [InlineData("2810243280", 4)]
        [InlineData("3701179099", 2)]
        [InlineData("8010153911", 6)]
        [InlineData("0650071580", 9)]
        public void IsGettingEgnInfoBornBefore(string input, int expected)
        {
            var result = Egn.TryGetInfo(input, out var info);
            Assert.Equal(expected, info.BornBefor);
        }

        [Theory]
        [InlineData("0301115350", 44763)]
        [InlineData("2311051940", 37160)]
        [InlineData("2810243280", 35345)]
        [InlineData("3701179099", 32338)]
        [InlineData("8010153911", 16361)]
        [InlineData("0650071580", 6873)]
        public void IsGettingEgnInfoAge(string input, int expected)
        {
            var result = Egn.TryGetInfo(input, out var info);
            Assert.Equal(expected, info.Age.Days);
        }
    }
}