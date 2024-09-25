namespace Ocean.Core.BrowserDetective.Models
{
    public class Header
    {
        public long ID { get; set; }
        public long Raw_ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public HeaderRaw Raw { get; set; } = new HeaderRaw();
    }
    public class HeaderRaw
    {
        public long ID { get; set; }
        public string CheckSum { get; set; } = string.Empty;
        public string CheckSumValues { get; set; } = string.Empty;
        public string Raw { get; set; } = string.Empty;
        public DateTime Stamp { get; set; }
        public string FileName { get; set; } = string.Empty;
        public virtual ICollection<Header> Headers { get; set; } = new List<Header>();

        public override string ToString()
        {
            return FileName + " - " + CheckSum;
        }
    }
}
