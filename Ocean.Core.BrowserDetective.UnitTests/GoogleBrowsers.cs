using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using System.Configuration;
using Ocean.Core.BrowserDetective.Extentions;
using NUnit.Framework.Legacy;
using Ocean.Core.BrowserDetective.Data.Models;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Collections;

namespace Ocean.Core.BrowserDetective.UnitTests
{
    //This is removing a anoying warnings.
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Assertion",
    "NUnit2005:Consider using ClassicAssert.That(actual, Is.EqualTo(expected)) instead of ClassicClassicAssert.AreEqual(expected, actual)",
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

            ClassicAssert.AreEqual("Chrome", RS2.BrowserName, "browser");
            ClassicAssert.AreEqual(new Version("94.0.4606"), RS2.version, "version");
            ClassicAssert.AreEqual("Microsoft Windows 10", RS2.OS, "os");
        }
        [Test]
        public void GoogleChrome_macOS_1()
        {
            System.Collections.Generic.Dictionary<string, string> Header;
            Header = new Dictionary<string, string>();
            Header.Add(@"User-Agent", @"Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/94.0.4606.61 Safari/537.36");
            var RS2 = Detective.ProcessData(Header);

            ClassicAssert.AreEqual("Chrome", RS2.BrowserName, "browser");
            ClassicAssert.AreEqual(new Version("94.0.4606"), RS2.version, "version");
            ClassicAssert.AreEqual("Macintosh OS X", RS2.OS, "os");
        }

        [Test]
        public void GoogleChrome_Android_1()
        {
            System.Collections.Generic.Dictionary<string, string> Header;
            Header = new Dictionary<string, string>();
            Header.Add(@"User-Agent", @"Mozilla/5.0 (Linux; Android 11; Pixel 5 Build/RQ3A.210805.001) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/94.0.4606.61 Mobile Safari/537.36");
            var RS2 = Detective.ProcessData(Header);

            ClassicAssert.AreEqual("Chrome Mobile", RS2.BrowserName, "browser");
            ClassicAssert.AreEqual(new Version("94.0.4606"), RS2.version, "version");
            ClassicAssert.AreEqual("Android 11", RS2.OS, "os");
        }

        [Test]
        public void GoogleChrome_Android_2()
        {
            System.Collections.Generic.Dictionary<string, string> Header;
            Header = new Dictionary<string, string>();
            Header.Add(@"User-Agent", @"Mozilla/5.0 (Linux; Android 7.0; Redmi Note 4 Build/NRD90M) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/67.0.3396.87 Mobile Safari/537.36");
            var RS2 = Detective.ProcessData(Header);

            ClassicAssert.AreEqual("Chrome Mobile", RS2.BrowserName, "browser");
            ClassicAssert.AreEqual(new Version("67.0.3396"), RS2.version, "version");
            ClassicAssert.AreEqual("Android 7.0", RS2.OS, "os");
        }

        [Test]
        public void GoogleChrome_Android_3()
        {
            System.Collections.Generic.Dictionary<string, string> Header;
            Header = new Dictionary<string, string>();
            Header.Add(@"User-Agent", @"Mozilla/5.0 (Linux; Android 7.0; SM-G892A Build/NRD90M; wv) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/60.0.3112.107 Moblie Safari/537.36");
            //Samsung Galaxy S8 Active 64GB SM-G892A at&T - Meteor Gray
            //https://www.samsung.com/us/business/support/owners/product/galaxy-s8-active-at-t/
            var RS2 = Detective.ProcessData(Header);

            foreach (var item in RS2.Trace)
            {
                Console.WriteLine(item);
            }

            ClassicAssert.AreEqual("Chrome Mobile", RS2.BrowserName, "browser");
            ClassicAssert.AreEqual(new Version("60.0.3112"), RS2.version, "version");
            ClassicAssert.AreEqual("Android 7.0", RS2.OS, "os");

            ClassicAssert.AreEqual(false, RS2.Crawler, "Crawler");
            ClassicAssert.AreEqual(true, RS2.isMobileDevice, "isMobileDevice");
            ClassicAssert.AreEqual("G892A", RS2.mobileDeviceModel, "mobileDeviceModel");
            ClassicAssert.AreEqual("SAMSUNG", RS2.mobileDeviceManufacturer, "mobileDeviceManufacturer");
        }


        [Test]
        public void GoogleChrome_Android_4()
        {
            //RawID=181277

            Dictionary<string, string> Header = new Dictionary<string, string>();
            Header.Add("Connection", "keep-alive");
            Header.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7");
            Header.Add("Accept-Encoding", "gzip, deflate, br, zstd");
            Header.Add("Accept-Language", "en-IN,en-US;q=0.9,en;q=0.8");
            Header.Add("Host", "owenbrady.net");
            Header.Add("Referer", "https://lm.facebook.com/");
            Header.Add("User-Agent", "Mozilla/5.0 (Linux; Android 13; LAVA LXX504 Build/TP1A.220624.014; wv) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/117.0.0.0 Mobile Safari/537.36 [FB_IAB/FB4A;FBAV/436.0.0.35.101;]");
            Header.Add("sec-ch-ua", "\"Android WebView\";v=\"117\", \"Not; A = Brand\";v=\"8\", \"Chromium\";v=\"117\"");
            Header.Add("sec-ch-ua-mobile", "?1");
            Header.Add("sec-ch-ua-platform", "\"Android\"");
            Header.Add("Upgrade-Insecure-Requests", "1");
            Header.Add("Sec-Fetch-Site", "cross-site");
            Header.Add("Sec-Fetch-Mode", "navigate");
            Header.Add("Sec-Fetch-Dest", "document");

            var RS2 = Detective.ProcessData(Header);

            foreach (var item in RS2.Trace)
            {
                Console.WriteLine(item);
            }

            ClassicAssert.AreEqual("Facebook App", RS2.BrowserName, "browser");
            ClassicAssert.AreEqual(new Version("0.0"), RS2.version, "browser");
            ClassicAssert.AreEqual("Android 13", RS2.OS, "OS");
            ClassicAssert.AreEqual("\"Android\"", RS2.Platform, "Platform");
            ClassicAssert.AreEqual(false, RS2.Crawler, "Crawler");
            ClassicAssert.AreEqual(true, RS2.isMobileDevice, "isMobileDevice");
            ClassicAssert.AreEqual("LXX504", RS2.mobileDeviceModel, "mobileDeviceModel");
            ClassicAssert.AreEqual("LAVA", RS2.mobileDeviceManufacturer, "mobileDeviceManufacturer");
        }


        [Test]
        public void GoogleChrome_Android_5()
        {
            //RawID=150888

            Dictionary<string, string> Header = new Dictionary<string, string>();
            Header.Add("Connection", "keep-alive");
            Header.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7");
            Header.Add("Accept-Encoding", "gzip, deflate, br");
            Header.Add("Accept-Language", "en-US,en-IN;q=0.9,en;q=0.8");
            Header.Add("Host", "owenbrady.net");
            Header.Add("Referer", "https://lm.facebook.com/");
            Header.Add("User-Agent", "Mozilla/5.0 (Linux; Android 13; EB2101 Build/TP1A.220905.001; wv) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/117.0.0.0 Mobile Safari/537.36 [FB_IAB/FB4A;FBAV/437.0.0.23.116;]");
            Header.Add("sec-ch-ua", "\"Android WebView\";v=\"117\", \"Not; A = Brand\";v=\"8\", \"Chromium\";v=\"117\"");
            Header.Add("sec-ch-ua-mobile", "?1");
            Header.Add("sec-ch-ua-platform", "\"Android\"");
            Header.Add("Upgrade-Insecure-Requests", "1");
            Header.Add("X-Requested-With", "com.facebook.katana");
            Header.Add("Sec-Fetch-Site", "cross-site");
            Header.Add("Sec-Fetch-Mode", "navigate");
            Header.Add("Sec-Fetch-Dest", "document");

            var RS2 = Detective.ProcessData(Header);
            ClassicAssert.AreEqual("Facebook App", RS2.BrowserName, "browser");
            ClassicAssert.AreEqual(new Version("0.0"), RS2.version, "browser");
            ClassicAssert.AreEqual("Android 13", RS2.OS, "OS");
            ClassicAssert.AreEqual("\"Android\"", RS2.Platform, "Platform");
            ClassicAssert.AreEqual(false, RS2.Crawler, "Crawler");
            ClassicAssert.AreEqual(true, RS2.isMobileDevice, "isMobileDevice");
            ClassicAssert.AreEqual("EB2101 Build/TP1A.220905.001; wv", RS2.mobileDeviceModel, "mobileDeviceModel");
            ClassicAssert.AreEqual("Unknown", RS2.mobileDeviceManufacturer, "mobileDeviceManufacturer");

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

            ClassicAssert.AreEqual("Chrome Mobile", RS2.BrowserName, "browser");
            ClassicAssert.AreEqual(new Version("74.0.3729.0"), RS2.version, "version");
            ClassicAssert.AreEqual("iOS", RS2.OS, "os");
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

            ClassicAssert.AreEqual("Dalvik", RS2.BrowserName, "browser");
            ClassicAssert.AreEqual(new Version("2.1.0"), RS2.version, "version");
            ClassicAssert.AreEqual("Android 6.0.1", RS2.OS, "os");

            ClassicAssert.AreEqual(false, RS2.Crawler, "Crawler");
            ClassicAssert.AreEqual(true, RS2.isMobileDevice, "isMobileDevice");
            ClassicAssert.AreEqual("SM-J700F Build/MMB29K", RS2.mobileDeviceModel, "mobileDeviceModel");
            ClassicAssert.AreEqual("Unknown", RS2.mobileDeviceManufacturer, "mobileDeviceManufacturer");
        }
    }
}