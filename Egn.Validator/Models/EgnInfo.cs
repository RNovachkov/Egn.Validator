namespace Egn.Validator.Models
{
    public class EgnInfo
    {
        /// <summary>
        /// 0 for Male, 1 for Female
        /// </summary>
        public byte Gender { get; set; }
        public Region Region { get; set; }
        public int BornBefore { get; set; }
        public TimeSpan Age { get; set; }
        public DateOnly Birthday { get; set; }
    }
}
