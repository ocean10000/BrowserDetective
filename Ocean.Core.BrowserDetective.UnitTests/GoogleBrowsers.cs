using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using System.Configuration;
using Ocean.Core.BrowserDetective.Extentions;

namespace Ocean.Core.BrowserDetective.UnitTests
{
    //This is removing a anoying warnings.
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Assertion",
    "NUnit2005:Consider using Assert.That(actual, Is.EqualTo(expected)) instead of ClassicAssert.AreEqual(expected, actual)",
    Justification = "Reason...")]
    public class GoogleBrowsersTests
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
        #region Chrome
        [Test]
        public void GoogleChrome_Windows_1()
        {
            System.Collections.Generic.Dictionary<string, string> Header;
            Header = new Dictionary<string, string>();
            Header.Add(@"User-Agent", @"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/94.0.4606.61 Safari/537.36");
            var RS2 = Detective.ProcessData(Header);

            Assert.AreEqual("Chrome", RS2.BrowserName, "browser");
            Assert.AreEqual(new Version("94.0.4606"), RS2.version, "version");
            Assert.AreEqual("Microsoft Windows 10", RS2.OS, "os");
        }
        [Test]
        public void GoogleChrome_macOS_1()
        {
            System.Collections.Generic.Dictionary<string, string> Header;
            Header = new Dictionary<string, string>();
            Header.Add(@"User-Agent", @"Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/94.0.4606.61 Safari/537.36");
            var RS2 = Detective.ProcessData(Header);

            Assert.AreEqual("Chrome", RS2.BrowserName, "browser");
            Assert.AreEqual(new Version("94.0.4606"), RS2.version, "version");
            Assert.AreEqual("Macintosh OS X", RS2.OS, "os");
        }

        [Test]
        public void GoogleChrome_Android_1()
        {
            System.Collections.Generic.Dictionary<string, string> Header;
            Header = new Dictionary<string, string>();
            Header.Add(@"User-Agent", @"Mozilla/5.0 (Linux; Android 11; Pixel 5 Build/RQ3A.210805.001) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/94.0.4606.61 Mobile Safari/537.36");
            var RS2 = Detective.ProcessData(Header);

            Assert.AreEqual("Chrome", RS2.BrowserName, "browser");
            Assert.AreEqual(new Version("94.0.4606"), RS2.version, "version");
            Assert.AreEqual("Android 11", RS2.OS, "os");
        }

        [Test]
        public void GoogleChrome_Android_2()
        {
            System.Collections.Generic.Dictionary<string, string> Header;
            Header = new Dictionary<string, string>();
            Header.Add(@"User-Agent", @"Mozilla/5.0 (Linux; Android 7.0; Redmi Note 4 Build/NRD90M) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/67.0.3396.87 Mobile Safari/537.36");
            var RS2 = Detective.ProcessData(Header);

            Assert.AreEqual("Chrome", RS2.BrowserName, "browser");
            Assert.AreEqual(new Version("67.0.3396"), RS2.version, "version");
            Assert.AreEqual("Android 7.0", RS2.OS, "os");
        }

        [Test]
        public void GoogleChrome_Android_3()
        {
            System.Collections.Generic.Dictionary<string, string> Header;
            Header = new Dictionary<string, string>();
            Header.Add(@"User-Agent", @"Mozilla/5.0 (Linux; Android 7.0; SM-G892A Bulid/NRD90M; wv) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/60.0.3112.107 Moblie Safari/537.36");
            //Samsung Galaxy S8 Active 64GB SM-G892A at&T - Meteor Gray
            //https://www.samsung.com/us/business/support/owners/product/galaxy-s8-active-at-t/
            var RS2 = Detective.ProcessData(Header);

            foreach (var item in RS2.Trace)
            {
                Console.WriteLine(item);
            }

            Assert.AreEqual("Chrome", RS2.BrowserName, "browser");
            Assert.AreEqual(new Version("60.0.3112"), RS2.version, "version");
            Assert.AreEqual("Android 7.0", RS2.OS, "os");

            Assert.AreEqual(false, RS2.Crawler, "Crawler");
            Assert.AreEqual(true, RS2.isMobileDevice, "isMobileDevice");
            Assert.AreEqual("SM-G892A Bulid/NRD90M; wv", RS2.mobileDeviceModel, "mobileDeviceModel");
            Assert.AreEqual("Unknown", RS2.mobileDeviceManufacturer, "mobileDeviceManufacturer");
        }

        //https://community.cloudflare.com/t/why-crios-instead-of-chrome/375159/3
        //https://chromium.googlesource.com/chromium/src.git/+/HEAD/docs/ios/user_agent.md
        [Test]
        public void CriOS_iOS_1()
        {
            System.Collections.Generic.Dictionary<string, string> Header;
            Header = new Dictionary<string, string>();
            Header.Add(@"User-Agent", @"Mozilla/5.0 (iPhone; CPU iPhone OS 12_3_1 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) CriOS/74.0.3729.155 Mobile/15E148 Safari/604.1");
            var RS2 = Detective.ProcessData(Header);

            Assert.AreEqual("Chrome Mobile", RS2.BrowserName, "browser");
            Assert.AreEqual(new Version("74.0.3729"), RS2.version, "version");
            Assert.AreEqual("iOS", RS2.OS, "os");
        }
        //this is Chrome Model trying to act as the Desktop Model.
        [Test]
        public void CriOS_iOS_2()
        {
            System.Collections.Generic.Dictionary<string, string> Header;
            Header = new Dictionary<string, string>();
            Header.Add(@"User-Agent", @"Mozilla/5.0 (Macintosh; Intel Mac OS X 10_13_5) AppleWebKit/605.1.15 (KHTML, like Gecko) CriOS/85 Version/11.1.1 Safari/605.1.15");
            var RS2 = Detective.DefaultBrowser.Process(Header);


            foreach (var item in RS2.Trace)
            {
                Console.WriteLine(item);
            }

            Assert.AreEqual("Chrome Mobile", RS2.BrowserName, "browser");
            Assert.AreEqual(new Version("85.0"), RS2.version, "version");
            Assert.AreEqual("iOS", RS2.OS, "os");
        }
        #endregion

        [Test]
        public void Dalvik_Android_1()
        {
            //https://stackoverflow.com/questions/23804278/browser-sending-dalvik-as-user-agent
            Dictionary<string, string> Header;
            Header = new Dictionary<string, string>();
            Header.Add(@"User-Agent", @"Dalvik/2.1.0 (Linux; U; Android 6.0.1; SM-J700F Build/MMB29K)");
            var RS2 = Detective.ProcessData(Header);

            foreach (var item in RS2.Trace)
            {
                Console.WriteLine(item);
            }

            Assert.AreEqual("Dalvik", RS2.BrowserName, "browser");
            Assert.AreEqual(new Version("2.1.0"), RS2.version, "version");
            Assert.AreEqual("Android 6.0.1", RS2.OS, "os");

            Assert.AreEqual(false, RS2.Crawler, "Crawler");
            Assert.AreEqual(true, RS2.isMobileDevice, "isMobileDevice");
            Assert.AreEqual("SM-J700F Build/MMB29K", RS2.mobileDeviceModel, "mobileDeviceModel");
            Assert.AreEqual("Unknown", RS2.mobileDeviceManufacturer, "mobileDeviceManufacturer");
        }
    }
}