namespace Ocean.Core.BrowserDetective.Data.Models
{
    public  class ResultItem
    {
        public long ID { get; set; }
        public long Raw_ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
    }
    public class BrowserNode
    {
        public int ID { get; set; }
        public long Raw_ID { get; set; }
        public long Node_ID { get; set; }
        public int Index { get; set; }
    }
}
