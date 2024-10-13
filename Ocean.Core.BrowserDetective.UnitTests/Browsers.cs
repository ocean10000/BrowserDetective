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
    public class BrowsersTests
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
        #region Opera
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
        #endregion
        #region Text Browsers
        [Test]
        public void Lynx_1()
        {
            //https://lynx.invisible-island.net/current/ Lynx is a text browser for the World Wide Web. 
            System.Collections.Generic.Dictionary<string, string> Header;
            Header = new Dictionary<string, string>();
            Header.Add(@"User-Agent", @"Lynx/2.8.5dev.16 libwww-FM/2.14 SSL-MM/1.4.1 OpenSSL/0.9.7a");
            var RS2 = Detective.ProcessData(Header);

            foreach (var item in RS2.Trace)
            {
                Console.WriteLine(item);
            }

            ClassicAssert.AreEqual("Lynx", RS2.BrowserName, "browser");
            ClassicAssert.AreEqual(new Version("2.8.5"), RS2.version, "version");
            ClassicAssert.AreEqual(false, RS2.isMobileDevice, "isMobileDevice");
            ClassicAssert.AreEqual(false, RS2.Crawler, "Crawler");
            ClassicAssert.AreEqual(string.Empty, RS2.OS, "os"); //https://en.wikipedia.org/wiki/Symbian
            ClassicAssert.AreEqual(false, RS2.IsRandomRobobotUserAgent, "IsRandomRobobotUserAgent");

        }
        #endregion
        [Test]
        public void UCBrowser_Android_1()
        {
            //https://play.google.com/store/apps/details?id=com.UCMobile.intl&hl=en_US&pli=1
            System.Collections.Generic.Dictionary<string, string> Header;
            Header = new Dictionary<string, string>();
            Header.Add(@"User-Agent", @"Mozilla/5.0 (Linux; U; Android 7.0; zh-CN; KNT-AL20 Build/HUAWEIKNT-AL20) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/57.0.2987.108 UCBrowser/11.9.4.974 UWS/2.14.0.2 Mobile Safari/537.36 AliApp(TB/7.7.5) UCBS/2.11.1.1 TTID/227200@taobao_android_7.7.5 WindVane/8.3.0 1440X2427");
            var RS2 = Detective.ProcessData(Header);

            foreach (var item in RS2.Trace)
            {
                Console.WriteLine(item);
            }
            ClassicAssert.AreEqual("Android 7.0", RS2.OS, "os");
            ClassicAssert.AreEqual("UCBrowser", RS2.BrowserName, "browser");
            ClassicAssert.AreEqual(new Version("11.9.4"), RS2.version, "version");
            ClassicAssert.AreEqual(false, RS2.IsRandomRobobotUserAgent, "IsRandomRobobotUserAgent");
        }
        [Test]
        public void QQBrowser_Windows_1()
        {
            System.Collections.Generic.Dictionary<string, string> Header;
            Header = new Dictionary<string, string>();
            Header.Add(@"User-Agent", @"Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2776.145 Safari/537.36 Core/1.70.3722.400 QQBrowser/10.5.3739.400");
            var RS2 = Detective.ProcessData(Header);

            ClassicAssert.AreEqual("QQBrowser", RS2.BrowserName, "browser");
            ClassicAssert.AreEqual(new Version("10.5.3739"), RS2.version, "version");
            ClassicAssert.AreEqual("Microsoft Windows 10", RS2.OS, "os");
            ClassicAssert.AreEqual(false, RS2.IsRandomRobobotUserAgent, "IsRandomRobobotUserAgent");
        }

        [Test]
        public void MQQBrowser_Android_1()
        {
            System.Collections.Generic.Dictionary<string, string> Header;
            Header = new Dictionary<string, string>();
            Header.Add(@"User-Agent", @"Mozilla/5.0 (Linux; U; Android 7.1.1; zh-cn; Mi Note 3 Build/NMF26X) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/57.0.2987.132 MQQBrowser/8.5 Mobile Safari/537.36");
            var RS2 = Detective.ProcessData(Header);

            ClassicAssert.AreEqual("QQBrowser", RS2.BrowserName, "browser");
            ClassicAssert.AreEqual(new Version("8.5"), RS2.version, "version");
            ClassicAssert.AreEqual("Android 7.1.1", RS2.OS, "os");
            ClassicAssert.AreEqual(false, RS2.IsRandomRobobotUserAgent, "IsRandomRobobotUserAgent");
        }

        [Test]
        public void YaBrowser_macOS_1()
        {
            System.Collections.Generic.Dictionary<string, string> Header;
            Header = new Dictionary<string, string>();
            Header.Add(@"User-Agent", @"Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/106.0.0.0 YaBrowser/22.11.3.824 Yowser/2.5 Safari/537.36");
            var RS2 = Detective.ProcessData(Header);

            ClassicAssert.AreEqual("YaBrowser", RS2.BrowserName, "browser");
            ClassicAssert.AreEqual(new Version("22.11.3"), RS2.version, "version");
            ClassicAssert.AreEqual("Macintosh OS X", RS2.OS, "os");
            ClassicAssert.AreEqual(false, RS2.IsRandomRobobotUserAgent, "IsRandomRobobotUserAgent");
        }

        [Test]
        public void es50_SymbianOS_1()
        {
            System.Collections.Generic.Dictionary<string, string> Header;
            Header = new Dictionary<string, string>();
            Header.Add(@"User-Agent", @"Mozilla/5.0 (SymbianOS/9.1; U; en-us) AppleWebKit/413 (KHTML, like Gecko) Safari/413 es50");
            var RS2 = Detective.ProcessData(Header);

            foreach (var item in RS2.Trace)
            {
                Console.WriteLine(item);
            }

            ClassicAssert.AreEqual("Nokia", RS2.BrowserName, "browser");
            ClassicAssert.AreEqual(false, RS2.Crawler, "Crawler");
            ClassicAssert.AreEqual(true, RS2.isMobileDevice, "isMobileDevice");
            ClassicAssert.AreEqual("Nokia es50", RS2.mobileDeviceModel, "mobileDeviceModel");
            ClassicAssert.AreEqual("Nokia", RS2.mobileDeviceManufacturer, "mobileDeviceManufacturer");
            ClassicAssert.AreEqual(new Version("0.0"), RS2.version, "version");
            ClassicAssert.AreEqual("SymbianOS 9.1", RS2.OS, "os"); //https://en.wikipedia.org/wiki/Symbian
            ClassicAssert.AreEqual(false, RS2.IsRandomRobobotUserAgent, "IsRandomRobobotUserAgent");
        }
        [Test]
        public void es70_SymbianOS_1()
        {
            System.Collections.Generic.Dictionary<string, string> Header;
            Header = new Dictionary<string, string>();
            Header.Add(@"User-Agent", @"Mozilla/5.0 (SymbianOS/9.1; U; en-us) AppleWebKit/413 (KHTML, like Gecko) Safari/413 es70");
            var RS2 = Detective.ProcessData(Header);

            foreach (var item in RS2.Trace)
            {
                Console.WriteLine(item);
            }

            ClassicAssert.AreEqual("Nokia", RS2.BrowserName, "browser");
            ClassicAssert.AreEqual(false, RS2.Crawler, "Crawler");
            ClassicAssert.AreEqual(true, RS2.isMobileDevice, "isMobileDevice");
            ClassicAssert.AreEqual("Nokia es70", RS2.mobileDeviceModel, "mobileDeviceModel");
            ClassicAssert.AreEqual("Nokia", RS2.mobileDeviceManufacturer, "mobileDeviceManufacturer");
            ClassicAssert.AreEqual(new Version("0.0"), RS2.version, "version");
            ClassicAssert.AreEqual("SymbianOS 9.1", RS2.OS, "os"); //https://en.wikipedia.org/wiki/Symbian
            ClassicAssert.AreEqual(false, RS2.IsRandomRobobotUserAgent, "IsRandomRobobotUserAgent");

        }
        [Test]
        public void es90_SymbianOS_1()
        {
            System.Collections.Generic.Dictionary<string, string> Header;
            Header = new Dictionary<string, string>();
            Header.Add(@"User-Agent", @"Mozilla/5.0 (SymbianOS/9.2; U; Series60/3.1 NokiaE90-1/07.24.0.3; Profile/MIDP-2.0 Configuration/CLDC-1.1 ) AppleWebKit/413 (KHTML, like Gecko) Safari/413 UP.Link/6.2.3.18.0");
            var RS2 = Detective.ProcessData(Header);

            foreach (var item in RS2.Trace)
            {
                Console.WriteLine(item);
            }

            ClassicAssert.AreEqual("Nokia", RS2.BrowserName, "browser");
            ClassicAssert.AreEqual(false, RS2.Crawler, "Crawler");
            ClassicAssert.AreEqual(true, RS2.isMobileDevice, "isMobileDevice");
            ClassicAssert.AreEqual("Nokia E90-1", RS2.mobileDeviceModel, "mobileDeviceModel");
            ClassicAssert.AreEqual("Nokia", RS2.mobileDeviceManufacturer, "mobileDeviceManufacturer");
            ClassicAssert.AreEqual(new Version("7.24.0"), RS2.version, "version");
            ClassicAssert.AreEqual("SymbianOS 9.2", RS2.OS, "os"); //https://en.wikipedia.org/wiki/Symbian
            ClassicAssert.AreEqual(false, RS2.IsRandomRobobotUserAgent, "IsRandomRobobotUserAgent");
        }

        [Test]
        public void Unknown_1()
        {
            System.Collections.Generic.Dictionary<string, string> Header;
            Header = new Dictionary<string, string>();
            Header.Add(@"User-Agent", @"Uzbl (Webkit 1.3) (Linux i686 [i686])");
            var RS2 = Detective.ProcessData(Header);

            foreach (var item in RS2.Trace)
            {
                Console.WriteLine(item);
            }

            ClassicAssert.AreEqual("Unknown", RS2.BrowserName, "browser");
            ClassicAssert.AreEqual(new Version("0.0"), RS2.version, "version");
            ClassicAssert.AreEqual("Linux", RS2.OS, "os");
            ClassicAssert.AreEqual(false, RS2.IsRandomRobobotUserAgent, "IsRandomRobobotUserAgent");
        }
        [Test]
        public void Unknown_2()
        {
            System.Collections.Generic.Dictionary<string, string> Header;
            Header = new Dictionary<string, string>();
            Header.Add(@"User-Agent", @"Mozilla/5.0 (SymbianOS/9.1; U; en-us) AppleWebKit/413 (KHTML, like Gecko) Safari/413");
            var RS2 = Detective.ProcessData(Header);

            foreach (var item in RS2.Trace)
            {
                Console.WriteLine(item);
            }

            ClassicAssert.AreEqual("Unknown", RS2.BrowserName, "browser");
            ClassicAssert.AreEqual(new Version("0.0"), RS2.version, "version");
            ClassicAssert.AreEqual(true, RS2.isMobileDevice, "isMobileDevice");
            ClassicAssert.AreEqual("SymbianOS 9.1", RS2.OS, "os"); //https://en.wikipedia.org/wiki/Symbian
            ClassicAssert.AreEqual(false, RS2.IsRandomRobobotUserAgent, "IsRandomRobobotUserAgent"); 
        }

        //https://github.com/codewatchorg/Burp-UserAgent/blob/master/useragents.xml
    }
}