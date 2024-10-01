namespace Ocean.Core.BrowserDetective
{
    public class Result : Dictionary<string, string>
    {
        public Result() : base() 
        { 
            this["browser"] = string.Empty;
            this["os"] = string.Empty;
        }
        public string OS
        {
            get
            {
                if (this.ContainsKey("OS"))
                    return this["OS"];
                else
                    return string.Empty;
            }
        }

        public string UserAgent
        {
            get
            {
                if (this.ContainsKey(string.Empty))
                    return this[string.Empty];
                else
                    return string.Empty;
            }
        }

        public string BrowserName
        {
            get
            {
                if (this.ContainsKey("browser"))
                    return this["browser"];
                else
                    return string.Empty;
            }
        }

        public bool Crawler
        {
            get
            {
                if (bool.TryParse(this["crawler"], out bool crawler) == true)
                {
                    return crawler;
                }
                return false;
            }
        }

        public Version version
        {
            get
            {
                return new Version(this["version"]);
            }
        }

        public int MajorVersion
        {
            get
            {
                return version.Major;
            }
        }

        public decimal MinorVersion
        {
            get
            {
                return version.Minor;
            }
        }

        public decimal MinorRevisionVersion
        {
            get
            {
                return version.MinorRevision;
            }
        }

        public bool isMobileDevice
        {
            get
            {
                if (bool.TryParse(this["isMobileDevice"], out bool MobileDevice) == true)
                {
                    return MobileDevice;
                }
                return false;
            }
        }

        public string mobileDeviceManufacturer
        {
            get
            {
                if (isMobileDevice == true)
                {
                    if (this.ContainsKey("mobileDeviceManufacturer"))
                        return this["mobileDeviceManufacturer"];
                }
                return string.Empty;
            }
        }

        public string mobileDeviceModel
        {
            get
            {
                if (isMobileDevice == true)
                {
                    if (this.ContainsKey("mobileDeviceModel"))
                        return this["mobileDeviceModel"];
                }
                return string.Empty;
            }
        }

        public System.Collections.Generic.List<Ocean.Core.BrowserDetective.Data.Models.Trackitem> Trace
        { get; set; } = new List<Data.Models.Trackitem>();
    }
}
