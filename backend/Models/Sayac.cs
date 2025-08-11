namespace SayacApi.Models
{
    public class Sayac
    {
        public int Id { get; set; }
        public string MeterNumber { get; set; }
        public decimal ReadingValue { get; set; }
        public DateTime ReadingDate { get; set; }
    }
}
