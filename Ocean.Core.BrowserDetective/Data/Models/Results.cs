using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Ocean.Core.BrowserDetective.Data.Models
{
    public class ResultItem
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
        private string _Version { get; set; } = string.Empty;

        public string version
        {
            get
            {
                return _Version;
            }
            set
            {
                _Version = value;
                if (Version.TryParse(value, out var versionInfo))
                {
                    //have to deal with the crap entity framework, not wanting to save fields, without setters. even though I want them FUCKING readonly.
                    //but setable  by the parent property.
                    MajorVersion = versionInfo.Major;
                    MinorVersion = versionInfo.Minor;
                    MinorRevision = versionInfo.MinorRevision;
                    if (MinorRevision == -1)
                        MinorRevision = 0;
                }
            }
        }
        public int MajorVersion { get; private set; }
        public int MinorVersion { get; private set; }
        public int MinorRevision { get; private set; }

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
        public string browser_type { get; set; } = string.Empty;
        public string Application { get; set; } = string.Empty;
        public string Application_Version { get; set; } = string.Empty;
        public string Application_maker { get; set; } = string.Empty;
        public string Architecture { get; set; } = string.Empty;
    }
}
