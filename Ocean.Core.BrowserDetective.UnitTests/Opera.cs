using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using System.Configuration;
using Ocean.Core.BrowserDetective.Extentions;
using NUnit.Framework.Legacy;

namespace Ocean.Core.BrowserDetective.UnitTests
{
    //This is removing a anoying warnings.
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Assertion",
    "NUnit2005:Consider using ClassicAssert.That(actual, Is.EqualTo(expected)) instead of ClassicClassicAssert.AreEqual(expected, actual)",
    Justification = "Reason...")]
    public class OperaTests
    {
        private Ocean.Core.BrowserDetective.Process Detective;

        [SetUp]
        public void Setup()
        {
            ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole());
            ILogger logger = factory.CreateLogger(typeof(Ocean.Core.BrowserDetective.Process));
            Detective = new Ocean.Core.BrowserDetective.Process(logger);
        }

        [OneTimeTearDown]
        public void Dispose()
        {
        }
        [Test]
        public void Opera_Windows_1()
        {
            System.Collections.Generic.Dictionary<string, string> Header;
            Header = new Dictionary<string, string>();
            Header.Add(@"User-Agent", @"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/94.0.4606.61 Safari/537.36 OPR/80.0.4170.63");
            var RS2 = Detective.ProcessData(Header);

            ClassicAssert.AreEqual("Opera", RS2.BrowserName, "browser");
            ClassicAssert.AreEqual(new Version("80.0.4170"), RS2.version, "version");
            ClassicAssert.AreEqual("Microsoft Windows 10", RS2.OS, "os");
            ClassicAssert.AreEqual(false, RS2.IsRandomRobobotUserAgent, "IsRandomRobobotUserAgent");
        }
        [Test]
        public void Opera_Windows_2()
        {
            //Header.ID = 99075
            Dictionary<string, string> Header = new Dictionary<string, string>();
            Header.Add("Connection", "keep-alive");
            Header.Add("Accept", "*/*");
            Header.Add("Accept-Encoding", "gzip, deflate");
            Header.Add("Host", "owenbrady.net");
            Header.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:47.0) Trident/5.0 Chrome/51.0.2704.106 Safari/537.36 OPR/38.0.2220.41");

            var RS2 = Detective.ProcessData(Header);
            ClassicAssert.AreEqual("Opera", RS2.BrowserName, "browser");
            ClassicAssert.AreEqual(new Version("38.0.2220"), RS2.version, "browser");
            ClassicAssert.AreEqual("Microsoft Windows 7", RS2.OS, "OS");
            ClassicAssert.AreEqual(false, RS2.Crawler, "Crawler");
            ClassicAssert.AreEqual(false, RS2.isMobileDevice, "isMobileDevice");
            ClassicAssert.AreEqual("", RS2.mobileDeviceModel, "isMobileDevice");
            ClassicAssert.AreEqual("", RS2.mobileDeviceManufacturer, "mobileDeviceManufacturer");
            ClassicAssert.AreEqual("Unknown", RS2.Platform, "Platform");
            ClassicAssert.AreEqual("", RS2.layoutEngineVersion, "layoutEngineVersion");
            ClassicAssert.AreEqual("", RS2.layoutEngine, "layoutEngine");
            ClassicAssert.AreEqual("51.0.2704", RS2.Chromeversion, "Chromeversion");
            ClassicAssert.AreEqual("537.36", RS2.AppleWebTechnologyVersion, "AppleWebTechnologyVersion");
        }
        [Test]
        public void Opera_Windows_3()
        {
            //Header.ID = 128976
            Dictionary<string, string> Header = new Dictionary<string, string>();
            Header.Add("Connection", "keep-alive");
            Header.Add("Accept", "*/*");
            Header.Add("Accept-Encoding", "gzip, deflate");
            Header.Add("Host", "owenbrady.net");
            Header.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:47.0) Gecko/20100101 Chrome/51.0.2704.106 Safari/537.36 OPR/38.0.2220.41");

            var RS2 = Detective.ProcessData(Header);
            ClassicAssert.AreEqual("Opera", RS2.BrowserName, "browser");
            ClassicAssert.AreEqual(new Version("38.0.2220"), RS2.version, "browser");
            ClassicAssert.AreEqual("Microsoft Windows 7", RS2.OS, "OS");
            ClassicAssert.AreEqual(false, RS2.Crawler, "Crawler");
            ClassicAssert.AreEqual(false, RS2.isMobileDevice, "isMobileDevice");
            ClassicAssert.AreEqual("", RS2.mobileDeviceModel, "isMobileDevice");
            ClassicAssert.AreEqual("", RS2.mobileDeviceManufacturer, "mobileDeviceManufacturer");
            ClassicAssert.AreEqual("Unknown", RS2.Platform, "Platform");
            ClassicAssert.AreEqual("20100101", RS2.layoutEngineVersion, "layoutEngineVersion");
            ClassicAssert.AreEqual("Gecko", RS2.layoutEngine, "layoutEngine");
            ClassicAssert.AreEqual("51.0.2704", RS2.Chromeversion, "Chromeversion");
            ClassicAssert.AreEqual("", RS2.AppleWebTechnologyVersion, "AppleWebTechnologyVersion");


        }
        [Test]
        public void Opera_Linux_1()
        {
            System.Collections.Generic.Dictionary<string, string> Header;
            Header = new Dictionary<string, string>();
            Header.Add(@"User-Agent", @"Mozilla/4.0 (compatible; MSIE 6.0; X11; Linux i686; de) Opera 10.10");
            var RS2 = Detective.ProcessData(Header);

            foreach (var item in RS2.Trace)
            {
                Console.WriteLine(item);
            }

            ClassicAssert.AreEqual("Opera", RS2.BrowserName, "browser");
            ClassicAssert.AreEqual(new Version("10.10"), RS2.version, "version");
            ClassicAssert.AreEqual("Linux", RS2.OS, "os");
            ClassicAssert.AreEqual(false, RS2.IsRandomRobobotUserAgent, "IsRandomRobobotUserAgent");
        }
        [Test]
        public void Opera_Linux_2()
        {
            //Header.ID = 16890
            Dictionary<string, string> Header = new Dictionary<string, string>();
            Header.Add("Connection", "keep-alive");
            Header.Add("Accept", "*/*");
            Header.Add("Accept-Encoding", "gzip, deflate");
            Header.Add("Host", "owenbrady.net");
            Header.Add("User-Agent", "Mozilla/5.0 (X11; Linux x86_64) Trident/5.0 Chrome/51.0.2704.106 Safari/537.36 OPR/38.0.2220.41");

            var RS2 = Detective.ProcessData(Header);
            ClassicAssert.AreEqual("Opera", RS2.BrowserName, "browser");
            ClassicAssert.AreEqual(new Version("38.0.2220"), RS2.version, "browser");
            ClassicAssert.AreEqual("Linux", RS2.OS, "OS");
            ClassicAssert.AreEqual(false, RS2.Crawler, "Crawler");
            ClassicAssert.AreEqual(false, RS2.isMobileDevice, "isMobileDevice");
            ClassicAssert.AreEqual("", RS2.mobileDeviceModel, "isMobileDevice");
            ClassicAssert.AreEqual("", RS2.mobileDeviceManufacturer, "mobileDeviceManufacturer");
            ClassicAssert.AreEqual("Unknown", RS2.Platform, "Platform");
            ClassicAssert.AreEqual("", RS2.layoutEngineVersion, "layoutEngineVersion");
            ClassicAssert.AreEqual("", RS2.layoutEngine, "layoutEngine");
            ClassicAssert.AreEqual("51.0.2704", RS2.Chromeversion, "Chromeversion");
            ClassicAssert.AreEqual("", RS2.AppleWebTechnologyVersion, "AppleWebTechnologyVersion");

        }
        [Test]
        public void Opera_Android_1()
        {
            //Header.ID = 87565
            Dictionary<string, string> Header = new Dictionary<string, string>();
            Header.Add("Accept", "image/webp,*/*;q=0.8");
            Header.Add("Accept-Encoding", "gzip, deflate");
            Header.Add("Accept-Language", "es-ES,es;q=0.8,en-us;q=0.6,en;q=0.4");
            Header.Add("Host", "owenbrady.net");
            Header.Add("Referer", "https://www.google.com/");
            Header.Add("User-Agent", "Mozilla/5.0 (Linux; Android 4.0.4; MW0711 Build/IMM76D) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/35.0.1916.138 Safari/537.36 OPR/22.0.1485.79355");
            Header.Add("X-Forwarded-For", "189.167.8.46");

            var RS2 = Detective.ProcessData(Header);
            ClassicAssert.AreEqual("Opera Mobile", RS2.BrowserName, "browser");
            ClassicAssert.AreEqual(new Version("22.0.1485"), RS2.version, "browser");
            ClassicAssert.AreEqual("Android 4.0.4", RS2.OS, "OS");
            ClassicAssert.AreEqual(false, RS2.Crawler, "Crawler");
            ClassicAssert.AreEqual(true, RS2.isMobileDevice, "isMobileDevice");
            ClassicAssert.AreEqual("MW0711 Build/IMM76D", RS2.mobileDeviceModel, "isMobileDevice");
            ClassicAssert.AreEqual("Unknown", RS2.mobileDeviceManufacturer, "mobileDeviceManufacturer");
            ClassicAssert.AreEqual("Unknown", RS2.Platform, "Platform");
            ClassicAssert.AreEqual("537.36", RS2.layoutEngineVersion, "layoutEngineVersion");
            ClassicAssert.AreEqual("WebKit", RS2.layoutEngine, "layoutEngine");

        }
        [Test]
        public void Opera_Mac_1()
        {
            //Header.ID = 119890
            Dictionary<string, string> Header = new Dictionary<string, string>();
            Header.Add("Connection", "keep-alive");
            Header.Add("Accept", "*/*");
            Header.Add("Accept-Encoding", "gzip, deflate");
            Header.Add("Host", "owenbrady.net");
            Header.Add("User-Agent", "Mozilla/5.0 (Macintosh; Intel Mac OS X x.y; rv:42.0) Trident/5.0 Chrome/51.0.2704.106 Safari/537.36 OPR/38.0.2220.41");

            var RS2 = Detective.ProcessData(Header);
            ClassicAssert.AreEqual("Opera", RS2.BrowserName, "browser");
            ClassicAssert.AreEqual(new Version("38.0.2220"), RS2.version, "browser");
            ClassicAssert.AreEqual("Macintosh OS X", RS2.OS, "OS");
            ClassicAssert.AreEqual(false, RS2.Crawler, "Crawler");
            ClassicAssert.AreEqual(false, RS2.isMobileDevice, "isMobileDevice");
            ClassicAssert.AreEqual("", RS2.mobileDeviceModel, "isMobileDevice");
            ClassicAssert.AreEqual("", RS2.mobileDeviceManufacturer, "mobileDeviceManufacturer");
            ClassicAssert.AreEqual("Unknown", RS2.Platform, "Platform");
            ClassicAssert.AreEqual(string.Empty, RS2.layoutEngineVersion, "layoutEngineVersion");
            ClassicAssert.AreEqual("", RS2.layoutEngine, "layoutEngine");
            ClassicAssert.AreEqual("51.0.2704", RS2.Chromeversion, "Chromeversion");
            ClassicAssert.AreEqual("537.36", RS2.AppleWebTechnologyVersion, "AppleWebTechnologyVersion");

        }
    }
}