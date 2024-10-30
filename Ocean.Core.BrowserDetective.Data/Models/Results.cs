using System.ComponentModel.DataAnnotations;

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
        public string Name { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
    }
	public class BrowserResult
	{
        [Key]
        public long Raw_ID { get; set; }
        public string UserAgent { get; set; } = string.Empty;
        public string BrowserName { get; set; } = string.Empty;
        public string Crawler { get; set; } = string.Empty;
        public string version { get; set; } = string.Empty;
        public string isMobileDevice { get; set; } = string.Empty;
        public string mobileDeviceModel { get; set; } = string.Empty;
        public string mobileDeviceManufacturer { get; set; } = string.Empty;
        public string OS { get; set; } = string.Empty;
        public string platform { get; set; } = string.Empty;
        public string layoutEngineVersion { get; set; } = string.Empty;
        public string layoutEngine { get; set; } = string.Empty;
        public string appleWebTechnologyVersion { get; set; } = string.Empty;
        public string Chromeversion { get; set; } = string.Empty;
        public DateTime Stamp { get; set; }
        public string TimeSpent { get; set; } = string.Empty;
        public string MD5 { get; set; } = string.Empty;
    }
}
