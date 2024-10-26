namespace Ocean.Core.BrowserDetective
{
    public class Result : Dictionary<string, string>
    {
        private string[] RandomRoboBotKeywords;
        /// <summary>
        /// This is a MD5 checksum based on the actual headers used, during the determination process.
        /// </summary>
        public string HeaderChecksum
        {
            get; set;
        }
        public Result() : base()
        {
            this["browser"] = string.Empty;
            this["OS"] = string.Empty;

            System.Reflection.Assembly asm = typeof(Data.Models.Browser).Assembly;
            using (System.IO.Stream CP = asm.GetManifestResourceStream("Ocean.Core.BrowserDetective.Data.RandomRobotKeywords.txt"))
            {
                System.IO.StreamReader Read = new System.IO.StreamReader(CP, System.Text.Encoding.Default);
                RandomRoboBotKeywords = System.Text.RegularExpressions.Regex.Split(Read.ReadToEnd(), System.Environment.NewLine);
                Read.Close();
            }
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
                if (this.ContainsKey("crawler") && string.IsNullOrWhiteSpace(this["crawler"]) == false && bool.TryParse(this["crawler"], out bool crawler) == true)
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
                if (this.ContainsKey("version") && string.IsNullOrWhiteSpace(this["version"]) == false)
                    return new Version(this["version"]);
                else
                    return new Version(0, 0);
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
                if (this.ContainsKey("isMobileDevice") && string.IsNullOrWhiteSpace(this["isMobileDevice"]) == false && bool.TryParse(this["isMobileDevice"], out bool MobileDevice) == true)
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
        /// <summary>
        /// Used to Identify Robobots that are using randomly generated Useragents
        /// that are nonsensical in nature/gibberish.
        /// </summary>
        /// <remarks>
        /// Current implementation is more of an elimination of common traits, which
        /// most Useragent/browser have. Which leave us with what can be assumed as
        /// randomized useragent names, which serve no purpose cept to drive stats
        /// programs nuts.
        /// </remarks>
        public bool IsRandomRobobotUserAgent
        {
            get
            {
                #region  Check for Common Words in UserAgents
                if (
                    string.IsNullOrWhiteSpace(this.BrowserName) == false && 
                    (string.Compare(this.BrowserName, "Unknown", true, System.Globalization.CultureInfo.CurrentCulture) != 0 && 
                    string.Compare(this.BrowserName, "Generic Browser", true, System.Globalization.CultureInfo.CurrentCulture) != 0)
                    )
                {
                    //---------------------------------------------------------------
                    //Browser name was able to be determined then the Useragent had
                    //enough details, thus not a random Useragent.
                    //---------------------------------------------------------------
                    return false;
                }
                else if (string.IsNullOrEmpty(this.UserAgent) == true)
                {
                    //---------------------------------------------------------------
                    //Null or empty. ^he Programer was just to lazy which to give it a
                    //name, which is fine with me but doesn't not count as a Randomized
                    //Browser Agent, since it doesn't have a Useragent at all to begin
                    //with.
                    //---------------------------------------------------------------
                    return false;
                }

                //---------------------------------------------------------------
                //I assume ones under 8 charactors are not really randomly named
                //but the coder was just lazy or picked a short name.
                //---------------------------------------------------------------
                if (this.UserAgent.Length < 8)
                {
                    return false;
                }

                //---------------------------------------------------------------
                //Up to this point I have not seen a randomly generated Agent string
                //with a period in it.
                //---------------------------------------------------------------
                if (this.UserAgent.IndexOf('.') > -1)
                {
                    return false;
                }
                //---------------------------------------------------------------
                //Compare keywords often found in useragents to the current useragent
                //and if we find one we assume its not a randomized useragent.
                //---------------------------------------------------------------
                foreach (string keyword in RandomRoboBotKeywords)
                {
                    if (keyword.Length <= this.UserAgent.Length)
                    {
                        if (this.UserAgent.IndexOf(keyword, StringComparison.CurrentCultureIgnoreCase) != -1)
                        {
                            return false;
                        }
                    }
                }
                #endregion
                //---------------------------------------------------------------
                //Since it made it though all the checks I assume that the useragent
                //doesn't match any known format that I can determine, and label it
                //a randomized Useragent/browser. AKA SPAM / Scraper / Pests Bots.
                //---------------------------------------------------------------
                return true;
            }
        }
        public System.Collections.Generic.List<Ocean.Core.BrowserDetective.Data.Models.Trackitem> Trace
        { get; set; } = new List<Data.Models.Trackitem>();

        /// <summary>
        ///  user agent client hint request header provides the platform or operating system on which the user agent is running.
        /// </summary>
        public string Platform
        {
            get
            {
                if (this.ContainsKey("platform"))
                    return this["platform"];
                else
                    return string.Empty;
            }
        }

        /// <summary>
        ///  
        /// </summary>
        public string layoutEngine
        {
            get
            {
                if (this.ContainsKey("layoutEngine"))
                    return this["layoutEngine"];
                else
                    return string.Empty;
            }
        }
        /// <summary>
        ///
        /// </summary>
        public string layoutEngineVersion
        {
            get
            {
                if (this.ContainsKey("layoutEngineVersion"))
                    return this["layoutEngineVersion"];
                else
                    return string.Empty;
            }
        }

        /// <summary>
        ///
        /// </summary>
        public string Chromeversion
        {
            get
            {
                if (this.ContainsKey("Chromeversion"))
                    return this["Chromeversion"];
                else
                    return string.Empty;
            }
        }
        /// <summary>
        ///
        /// </summary>
        public string AppleWebTechnologyVersion
        {
            get
            {
                if (this.ContainsKey("appleWebTechnologyVersion"))
                    return this["appleWebTechnologyVersion"];
                else
                    return string.Empty;
            }
        }
    }
}
