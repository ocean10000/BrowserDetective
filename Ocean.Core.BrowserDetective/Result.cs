namespace Ocean.Core.BrowserDetective
{
    public class Result : Dictionary<string, string>
    {
        public string OS
        {
            get
            {
                return this["OS"];
            }
        }

        public string UserAgent
        {
            get
            {
                return this[string.Empty];
            }
        }

        public string BrowserName
        {
            get
            {
                return this["browser"];
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
                    return this["mobileDeviceModel"];
                }
                return string.Empty;
            }
        }

        public string platform
        {
            get
            {
                return this["platform"];
            }
        }

    }
}
