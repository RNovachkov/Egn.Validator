using Egn.Validator;

namespace Egn.Validator.Tests
{
    public class EgnTest
    {
        [Theory]
        [InlineData("", false)] // empty string
        [InlineData("801015391", false)] // 9 digits
        [InlineData("80101539111", false)] // 11 digits
        [InlineData("9942304663", false)] // invalid date
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
        [InlineData("9942304663", false)] // invalid date
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
            Assert.Equal(expected, info.BornBefore);
        }

        //[Theory]
        //[InlineData("0301115350", 44763)]
        //[InlineData("2311051940", 37160)]
        //[InlineData("2810243280", 35345)]
        //[InlineData("3701179099", 32338)]
        //[InlineData("8010153911", 16361)]
        //[InlineData("0650071580", 6873)]
        //public void IsGettingEgnInfoAge(string input, int expected)
        //{
        //    var result = Egn.TryGetInfo(input, out var info);
        //    Assert.Equal(expected, info.Age.Days);
        //}

        [Fact]
        public void GeneratesValidRandomEgn()
        {
            for (int i = 0; i < 10; i++)
            {
                var egn = Egn.Generate();
                Assert.True(egn.Length == 10);
                var monthFirstDigit = int.Parse(egn.Substring(2, 1));
                Assert.True(0 <= monthFirstDigit && monthFirstDigit <= 5);
                //Assert.True(Egn.isValid(egn));
            }
        }

        [Theory]
        [InlineData(15, 4, 2015, 1, "EGN_VAL.REGIONS.RAZGRAD", "1544155020,1544155041,1544155062,1544155083,1544155102,1544155123,1544155144,1544155165,1544155186,1544155205,1544155226,1544155247,1544155268")]
        [InlineData(11,8,2023,2, "EGN_VAL.REGIONS.BURGAS", "2348110456,2348110477,2348110498,2348110517,2348110538,2348110559,2348110570,2348110590,2348110610,2348110630,2348110651,2348110672,2348110693,2348110712,2348110733,2348110754,2348110775,2348110796,2348110815,2348110836,2348110857,2348110878,2348110899,2348110918,2348110939")]
        public void GeneratesSpecificEgn(uint day, uint mon, uint year, byte sex, string region, string possibleResults)
        {
            var egn = Egn.Generate(day, mon, year, sex, region);
            Assert.Contains(egn, possibleResults);
            Assert.Equal(1, possibleResults.Split(egn).Length - 1);
        }

        [Theory]
        [InlineData(0,0,1)]
        [InlineData(0,0,1799)]
        [InlineData(0,0,2100)]
        [InlineData(0,13,0)]
        [InlineData(32,0,0)]
        [InlineData(32,1,0)]
        [InlineData(30,2,0)]
        [InlineData(31,4,0)]
        [InlineData(30,2,2016)]
        [InlineData(29,2,2015)]
        public void GeneratorThrowsException(uint day, uint mon, uint year)
        {
            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() =>
            {
                // Code that should throw
                Egn.Generate(day,mon,year);
            });
            Assert.Equal("One or more invalid date parameters.", ex.Message);
        }
    }
}