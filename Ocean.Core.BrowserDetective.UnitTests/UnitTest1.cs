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
    public class Tests
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
        public void MSIE_Windows_1()
        {
            System.Collections.Generic.Dictionary<string,string> Header;
            Header = new Dictionary<string, string>();
            Header.Add(@"Cache-Control", @"max-age=259200");
            Header.Add(@"Connection", @"keep-alive");
            Header.Add(@"Via", @"1.0 server:8080 (squid/2.5.STABLE12)");
            Header.Add(@"Accept", @"*/*");
            Header.Add(@"Accept-Language", @"ur");
            Header.Add(@"User-Agent", @"Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1)");
            Header.Add(@"X-Forwarded-For", @"192.168.1.63");
            var RS2 = Detective.ProcessData(Header);
           
            Assert.AreEqual("Microsoft Internet Explorer", RS2.BrowserName, "browser");
            Assert.AreEqual(new Version("6.0"), RS2.version, "version");
            Assert.AreEqual("Microsoft Windows XP", RS2.OS, "os");
        }

        [Test]
        public void TestId_10171_v4()
        {
            System.Collections.Generic.Dictionary<string, string> Header;
            Header = new Dictionary<string, string>();
            Header.Add(@"Connection", @"keep-alive");
            Header.Add(@"Keep-Alive", @"300");
            Header.Add(@"Accept", @"application/x-shockwave-flash,text/xml,application/xml,application/xhtml+xml,text/html;q=0.9,text/plain;q=0.8,image/png,*/*;q=0.5");
            Header.Add(@"Accept-Charset", @"ISO-8859-1,utf-8;q=0.7,*;q=0.7");
            Header.Add(@"Accept-Language", @"en-us,en;q=0.5");
            Header.Add(@"User-Agent", @"Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.7.5) Gecko/20060127 Netscape/8.1");
            Header.Add(@"---------------", @"------------");
            var RS2 = Detective.ProcessData(Header);
            Assert.AreEqual("Netscape", RS2["browser"], "browser");
            Assert.AreEqual(new Version("8.1"), RS2.version, "version");
            Assert.AreEqual("Microsoft Windows XP", RS2.OS, "os");
        }

        [Test]
        public void TestId_10275_V2()
        {
            System.Collections.Specialized.NameValueCollection Header;
            Header = new System.Collections.Specialized.NameValueCollection();
            Header.Add(@"Cache-Control", @"max-age=259200");
            Header.Add(@"Connection", @"keep-alive");
            Header.Add(@"Keep-Alive", @"300");
            Header.Add(@"Via", @"1.1 htc16.htc.nl.philips.com:8080 (squid/2.5.STABLE4)");
            Header.Add(@"Accept", @"text/xml,application/xml,application/xhtml+xml,text/html;q=0.9,text/plain;q=0.8,image/png,*/*;q=0.5");
            Header.Add(@"Accept-Charset", @"ISO-8859-1,utf-8;q=0.7,*;q=0.7");
            Header.Add(@"Accept-Encoding", @"gzip,deflate");
            Header.Add(@"Accept-Language", @"en-us,en;q=0.5");
            Header.Add(@"User-Agent", @"Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.8.0.1) Gecko/20060111 Firefox/1.5.0.1");
            Header.Add(@"X-Forwarded-For", @"unknown");
            var RS2 = Detective.ProcessData(Header);
            Assert.AreEqual("Firefox", RS2.BrowserName, "browser");
            Assert.AreEqual(new Version(1,5,0), RS2.version, "version");
            Assert.AreEqual("Microsoft Windows XP", RS2.OS, "os");
        }

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
        public void MozillaFirefox_Windows_1()
        {
            System.Collections.Generic.Dictionary<string, string> Header;
            Header = new Dictionary<string, string>();
            Header.Add(@"User-Agent", @"Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:93.0) Gecko/20100101 Firefox/93.0");
            var RS2 = Detective.ProcessData(Header);

            Assert.AreEqual("Firefox", RS2.BrowserName, "browser");
            Assert.AreEqual("Microsoft Windows 10", RS2.OS, "os");
        }
        [Test]
        public void MozillaFirefox_macOS_1()
        {
            System.Collections.Generic.Dictionary<string, string> Header;
            Header = new Dictionary<string, string>();
            Header.Add(@"User-Agent", @"Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7; rv:93.0) Gecko/20100101 Firefox/93.0");
            var RS2 = Detective.ProcessData(Header);

            Assert.AreEqual("Firefox", RS2.BrowserName, "browser");
            Assert.AreEqual(new Version("93.0"), RS2.version, "version");
            Assert.AreEqual("Macintosh OS X", RS2.OS, "os");
        }
        [Test]
        public void Safari_macOS_1()
        {
            System.Collections.Generic.Dictionary<string, string> Header;
            Header = new Dictionary<string, string>();
            Header.Add(@"User-Agent", @"Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/15.0 Safari/605.1.15");
            var RS2 = Detective.ProcessData(Header);

            Assert.AreEqual("Safari", RS2.BrowserName, "browser");
            Assert.AreEqual(new Version("15.0"), RS2.version, "version");
            Assert.AreEqual("Macintosh OS X", RS2.OS, "os");
        }

        [Test]
        public void Safari_iOS_1()
        {
            System.Collections.Generic.Dictionary<string, string> Header;
            Header = new Dictionary<string, string>();
            Header.Add(@"User-Agent", @"Mozilla/5.0 (iPhone; CPU iPhone OS 15_0 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/15.0 Mobile/15E148 Safari/604.1");
            var RS2 = Detective.ProcessData(Header);

            Assert.AreEqual("Safari", RS2.BrowserName, "browser");
            Assert.AreEqual(new Version("15.0"), RS2.version, "version");
            Assert.AreEqual("iOS", RS2.OS, "os");
        }
        [Test]
        public void MicrosoftEdge_Windows_1()
        {
            System.Collections.Generic.Dictionary<string, string> Header;
            Header = new Dictionary<string, string>();
            Header.Add(@"User-Agent", @"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/94.0.4606.61 Safari/537.36 Edg/94.0.992.47");
            var RS2 = Detective.ProcessData(Header);

            foreach (var item in RS2.Trace)
            {
                Console.WriteLine(item);
            }

            Assert.AreEqual("Microsoft Edge", RS2.BrowserName, "browser");
            Assert.AreEqual(new Version("94.0.992"), RS2.version, "version");
            Assert.AreEqual("Microsoft Windows 10", RS2.OS, "os");
        }
        [Test]
        public void Opera_Windows_1()
        {
            System.Collections.Generic.Dictionary<string, string> Header;
            Header = new Dictionary<string, string>();
            Header.Add(@"User-Agent", @"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/94.0.4606.61 Safari/537.36 OPR/80.0.4170.63");
            var RS2 = Detective.ProcessData(Header);

            Assert.AreEqual("Opera", RS2.BrowserName, "browser");
            Assert.AreEqual(new Version("80.0.4170"), RS2.version, "version");
            Assert.AreEqual("Microsoft Windows 10", RS2.OS, "os");
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

            Assert.AreEqual("Opera", RS2.BrowserName, "browser");
            Assert.AreEqual(new Version("10.10"), RS2.version, "version");
            Assert.AreEqual("Linux", RS2.OS, "os");
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

        //this is Chrome Model trying to act as the Desktop Model.
        [Test]
        public void Bytespider_1()
        {
            System.Collections.Generic.Dictionary<string, string> Header;
            Header = new Dictionary<string, string>();
            Header.Add(@"User-Agent", @"Mozilla/5.0 (Linux; Android 5.0) AppleWebKit/537.36 (KHTML, like Gecko) Mobile Safari/537.36 (compatible; Bytespider; spider-feedback@bytedance.com)");
            var RS2 = Detective.DefaultBrowser.Process(Header);

            Assert.AreEqual("Bytespider", RS2.BrowserName, "browser");
            Assert.AreEqual(true, RS2.Crawler, "Crawler");
        }

        //this is Chrome Model trying to act as the Desktop Model.
        [Test]
        public void Bytespider_2()
        {
            System.Collections.Generic.Dictionary<string, string> Header;
            Header = new Dictionary<string, string>();
            Header.Add(@"User-Agent", @"Mozilla/5.0 (Linux; Android 5.0) AppleWebKit/537.36 (KHTML, like Gecko) Mobile Safari/537.36 (compatible; Bytespider; https://zhanzhang.toutiao.com/)");
            var RS2 = Detective.DefaultBrowser.Process(Header);

            Assert.AreEqual("Bytespider", RS2.BrowserName, "browser");
            Assert.AreEqual(true, RS2.Crawler, "Crawler");
        }

        [Test]
        public void QQBrowser_Windows_1()
        {
            System.Collections.Generic.Dictionary<string, string> Header;
            Header = new Dictionary<string, string>();
            Header.Add(@"User-Agent", @"Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2776.145 Safari/537.36 Core/1.70.3722.400 QQBrowser/10.5.3739.400");
            var RS2 = Detective.DefaultBrowser.Process(Header);

            Assert.AreEqual("QQBrowser", RS2.BrowserName, "browser");
            Assert.AreEqual(new Version("10.5.3739"), RS2.version, "version");
            Assert.AreEqual("Microsoft Windows 10", RS2.OS, "os");
        }

        [Test]
        public void MQQBrowser_Android_1()
        {
            System.Collections.Generic.Dictionary<string, string> Header;
            Header = new Dictionary<string, string>();
            Header.Add(@"User-Agent", @"Mozilla/5.0 (Linux; U; Android 7.1.1; zh-cn; Mi Note 3 Build/NMF26X) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/57.0.2987.132 MQQBrowser/8.5 Mobile Safari/537.36");
            var RS2 = Detective.DefaultBrowser.Process(Header);

            Assert.AreEqual("QQBrowser", RS2.BrowserName, "browser");
            Assert.AreEqual(new Version("8.5"), RS2.version, "version");
            Assert.AreEqual("Android 7.1.1", RS2.OS, "os");
        }

        [Test]
        public void YaBrowser_macOS_1()
        {
            System.Collections.Generic.Dictionary<string, string> Header;
            Header = new Dictionary<string, string>();
            Header.Add(@"User-Agent", @"Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/106.0.0.0 YaBrowser/22.11.3.824 Yowser/2.5 Safari/537.36");
            var RS2 = Detective.ProcessData(Header);

            Assert.AreEqual("YaBrowser", RS2.BrowserName, "browser");
            Assert.AreEqual(new Version("22.11.3"), RS2.version, "version");
            Assert.AreEqual("Macintosh OS X", RS2.OS, "os");
        }

        [Test]
        public void certifytheweb_1()
        {
            System.Collections.Generic.Dictionary<string, string> Header;
            Header = new Dictionary<string, string>();
            Header.Add(@"cf-worker", @"certifytheweb.com");
            var RS2 = Detective.ProcessData(Header);

            Assert.AreEqual("certifytheweb.com", RS2.BrowserName, "browser");
            Assert.AreEqual(new Version("0.0"), RS2.version, "version");
            Assert.AreEqual(string.Empty, RS2.OS, "os");
            Assert.AreEqual(true, RS2.Crawler, "Crawler");
            Assert.AreEqual(false, RS2.isMobileDevice, "isMobileDevice");

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

            Assert.AreEqual("Nokia", RS2.BrowserName, "browser");
            Assert.AreEqual(false, RS2.Crawler, "Crawler");
            Assert.AreEqual(true, RS2.isMobileDevice, "isMobileDevice");
            Assert.AreEqual("Nokia es50", RS2.mobileDeviceModel, "mobileDeviceModel");
            Assert.AreEqual("Nokia", RS2.mobileDeviceManufacturer, "mobileDeviceManufacturer");
            Assert.AreEqual(new Version("0.0"), RS2.version, "version");
            Assert.AreEqual("SymbianOS 9.1", RS2.OS, "os"); //https://en.wikipedia.org/wiki/Symbian
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

            Assert.AreEqual("Nokia", RS2.BrowserName, "browser");
            Assert.AreEqual(false, RS2.Crawler, "Crawler");
            Assert.AreEqual(true, RS2.isMobileDevice, "isMobileDevice");
            Assert.AreEqual("Nokia es70", RS2.mobileDeviceModel, "mobileDeviceModel");
            Assert.AreEqual("Nokia", RS2.mobileDeviceManufacturer, "mobileDeviceManufacturer");
            Assert.AreEqual(new Version("0.0"), RS2.version, "version");
            Assert.AreEqual("SymbianOS 9.1", RS2.OS, "os"); //https://en.wikipedia.org/wiki/Symbian

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

            Assert.AreEqual("Nokia", RS2.BrowserName, "browser");
            Assert.AreEqual(false, RS2.Crawler, "Crawler");
            Assert.AreEqual(true, RS2.isMobileDevice, "isMobileDevice");
            Assert.AreEqual("Nokia E90-1", RS2.mobileDeviceModel, "mobileDeviceModel");
            Assert.AreEqual("Nokia", RS2.mobileDeviceManufacturer, "mobileDeviceManufacturer");
            Assert.AreEqual(new Version("7.24.0"), RS2.version, "version");
            Assert.AreEqual("SymbianOS 9.2", RS2.OS, "os"); //https://en.wikipedia.org/wiki/Symbian

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

            Assert.AreEqual("Unknown", RS2.BrowserName, "browser");
            Assert.AreEqual(new Version("0.0"), RS2.version, "version");
            Assert.AreEqual("Linux", RS2.OS, "os");

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

            Assert.AreEqual("Unknown", RS2.BrowserName, "browser");
            Assert.AreEqual(new Version("0.0"), RS2.version, "version");
            Assert.AreEqual(true, RS2.isMobileDevice, "isMobileDevice");
            Assert.AreEqual("SymbianOS 9.1", RS2.OS, "os"); //https://en.wikipedia.org/wiki/Symbian

        }

        [Test]
        public void GPTBot_1()
        {
            System.Collections.Generic.Dictionary<string, string> Header;
            Header = new Dictionary<string, string>();
            Header.Add(@"User-Agent", @"Mozilla/5.0 AppleWebKit/537.36 (KHTML, like Gecko; compatible; GPTBot/1.0; +https://openai.com/gptbot)");
            var RS2 = Detective.ProcessData(Header);

            foreach (var item in RS2.Trace)
            {
                Console.WriteLine(item);
            }

            Assert.AreEqual("GPTBot", RS2.BrowserName, "browser");
            Assert.AreEqual(new Version("1.0"), RS2.version, "version");
            Assert.AreEqual(true, RS2.Crawler, "Crawler");
            Assert.AreEqual(false, RS2.isMobileDevice, "isMobileDevice");
            Assert.AreEqual("Unknown", RS2.OS, "os"); //https://en.wikipedia.org/wiki/Symbian

        }

        [Test]
        public void bingbot_1()
        {
            System.Collections.Generic.Dictionary<string, string> Header;
            Header = new Dictionary<string, string>();
            Header.Add(@"User-Agent", @"Mozilla/5.0 AppleWebKit/537.36 (KHTML, like Gecko; compatible; bingbot/2.0; +http://www.bing.com/bingbot.htm) Chrome/116.0.1938.76 Safari/537.36");
            var RS2 = Detective.ProcessData(Header);

            foreach (var item in RS2.Trace)
            {
                Console.WriteLine(item);
            }

            Assert.AreEqual("bingbot", RS2.BrowserName, "browser");
            Assert.AreEqual(new Version("2.0"), RS2.version, "version");
            Assert.AreEqual(true, RS2.Crawler, "Crawler");
            Assert.AreEqual(false, RS2.isMobileDevice, "isMobileDevice");
            Assert.AreEqual("Unknown", RS2.OS, "os"); //https://en.wikipedia.org/wiki/Symbian

        }


        [Test]
        public void UCBrowser_Android_1()
        {
            System.Collections.Generic.Dictionary<string, string> Header;
            Header = new Dictionary<string, string>();
            Header.Add(@"User-Agent", @"Mozilla/5.0 (Linux; U; Android 7.0; zh-CN; KNT-AL20 Build/HUAWEIKNT-AL20) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/57.0.2987.108 UCBrowser/11.9.4.974 UWS/2.14.0.2 Mobile Safari/537.36 AliApp(TB/7.7.5) UCBS/2.11.1.1 TTID/227200@taobao_android_7.7.5 WindVane/8.3.0 1440X2427");
            var RS2 = Detective.ProcessData(Header);


            foreach (var item in RS2.Trace)
            {
                Console.WriteLine(item);
            }
            Assert.AreEqual("Android 7.0", RS2.OS, "os");
            Assert.AreEqual("UCBrowser", RS2.BrowserName, "browser");
            Assert.AreEqual(new Version("11.9.4"), RS2.version, "version");
        
        }

        //https://github.com/codewatchorg/Burp-UserAgent/blob/master/useragents.xml
    }
}