namespace Egn.Validator.Tests
{
    public static class TestConstants
    {
        public const string CustomErrorMessage = "INVALID_EGN";
    }

    public class TestModel
    {
        [EGN]
        public string Egn { get; set; }
    }

    public class TestModelNull
    {
        [EGN]
        public string? Egn { get; set; }
    }

    public class TestModelWithMessage
    {
        [EGN(ErrorMessage = TestConstants.CustomErrorMessage)]
        public string Egn { get; set; }
    }
}
