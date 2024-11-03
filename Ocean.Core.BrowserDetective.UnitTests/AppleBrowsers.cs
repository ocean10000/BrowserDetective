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
    public class AppleBrowsersTests
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
        #region Apple Safari
        [Test]
        public void Safari_macOS_1()
        {
            System.Collections.Generic.Dictionary<string, string> Header;
            Header = new Dictionary<string, string>();
            Header.Add(@"User-Agent", @"Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/15.0 Safari/605.1.15");
            var RS2 = Detective.ProcessData(Header);

            ClassicAssert.AreEqual("Safari", RS2.BrowserName, "browser");
            ClassicAssert.AreEqual(new Version("15.0"), RS2.version, "version");
            ClassicAssert.AreEqual("Macintosh OS X", RS2.OS, "os");
        }

        [Test]
        public void Safari_iOS_1()
        {
            System.Collections.Generic.Dictionary<string, string> Header;
            Header = new Dictionary<string, string>();
            Header.Add(@"User-Agent", @"Mozilla/5.0 (iPhone; CPU iPhone OS 15_0 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/15.0 Mobile/15E148 Safari/604.1");
            var RS2 = Detective.ProcessData(Header);

            ClassicAssert.AreEqual("Mobile Safari", RS2.BrowserName, "browser");
            ClassicAssert.AreEqual(new Version("15.0"), RS2.version, "version");
            ClassicAssert.AreEqual("iOS 15.0", RS2.OS, "os");
        }

        [Test]
        public void iCab_macOS_1()
        {
            System.Collections.Generic.Dictionary<string, string> Header;
            Header = new Dictionary<string, string>();
            Header.Add(@"User-Agent", @"Mozilla/5.0 (Macintosh; PPC Mac OS X 10_5_8) AppleWebKit/537.3+ (KHTML, like Gecko) iCab/5.0 Safari/533.16");
            var RS2 = Detective.ProcessData(Header);

            ClassicAssert.AreEqual("iCab", RS2.BrowserName, "browser");
            ClassicAssert.AreEqual(new Version("5.0"), RS2.version, "version");
            ClassicAssert.AreEqual("Macintosh OS X", RS2.OS, "os");
        }
        [Test]
        public void iCab_macOS_2()
        {
            System.Collections.Generic.Dictionary<string, string> Header;
            Header = new Dictionary<string, string>();
            Header.Add(@"User-Agent", @"iCab/5.0 (Macintosh; U; PPC Mac OS X)");
            var RS2 = Detective.ProcessData(Header);

            ClassicAssert.AreEqual("iCab", RS2.BrowserName, "browser");
            ClassicAssert.AreEqual(new Version("5.0"), RS2.version, "version");
            ClassicAssert.AreEqual("Macintosh OS X", RS2.OS, "os");
        }
        [Test]
        public void iCab_macOS_3()
        {
            System.Collections.Generic.Dictionary<string, string> Header;
            Header = new Dictionary<string, string>();
            Header.Add(@"User-Agent", @"Mozilla/4.5 (compatible; iCab 2.9.1; Macintosh; U; PPC)");
            var RS2 = Detective.ProcessData(Header);

            ClassicAssert.AreEqual("iCab", RS2.BrowserName, "browser");
            ClassicAssert.AreEqual(new Version("2.9.1"), RS2.version, "version");
            ClassicAssert.AreEqual("Macintosh PPC", RS2.OS, "os");
        }
        #endregion
        [Test]
        public void Facebook_iOS_1()
        {
            Dictionary<string, string> Header = new Dictionary<string, string>();
            Header.Add("User-Agent", "Mozilla/5.0 (iPhone; CPU iPhone OS 17_3_1 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Mobile/21D61 [FBAN/FBIOS;FBAV/463.0.4.49.101;FBBV/597123453;FBDV/iPhone12,8;FBMD/iPhone;FBSN/iOS;FBSV/17.3.1;FBSS/2;FBID/phone;FBLC/en_US;FBOP/5;FBRV/598951293");

            var RS2 = Detective.ProcessData(Header);
            ClassicAssert.AreEqual("Facebook App", RS2.BrowserName, "browser");
            ClassicAssert.AreEqual(new Version("0.0"), RS2.version, "browser");
            ClassicAssert.AreEqual("iOS 17.3", RS2.OS, "OS");
            ClassicAssert.AreEqual(false, RS2.Crawler, "Crawler");
            ClassicAssert.AreEqual(true, RS2.isMobileDevice, "isMobileDevice");
            ClassicAssert.AreEqual("iPhone", RS2.mobileDeviceModel, "isMobileDevice");
            ClassicAssert.AreEqual("Apple", RS2.mobileDeviceManufacturer, "mobileDeviceManufacturer");
            ClassicAssert.AreEqual("Unknown", RS2.Platform, "Platform");
            ClassicAssert.AreEqual("605.1.15", RS2.layoutEngineVersion, "layoutEngineVersion");
            ClassicAssert.AreEqual("WebKit", RS2.layoutEngine, "layoutEngine");
            ClassicAssert.AreEqual("", RS2.Chromeversion, "Chromeversion");
            ClassicAssert.AreEqual("605.1.15", RS2.AppleWebTechnologyVersion, "AppleWebTechnologyVersion");

        }
    }
}