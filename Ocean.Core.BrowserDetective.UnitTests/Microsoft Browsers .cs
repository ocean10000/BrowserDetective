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
    public class MicrosoftBrowsersTests
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
        //https://github.com/MicrosoftDocs/edge-developer/blob/main/microsoft-edge/web-platform/user-agent-guidance.md
        //https://github.com/MicrosoftDocs/edge-developer/blob/main/microsoft-edge/web-platform/how-to-detect-win11.md
        //https://github.com/MicrosoftDocs/edge-developer/blob/main/microsoft-edge/web-platform/os-regional-settings.md
        //https://wicg.github.io/ua-client-hints/#sec-ch-ua-platform-version
        //https://learn.microsoft.com/en-us/previous-versions/windows/internet-explorer/ie-developer/compatibility/ms537503(v=vs.85)

        #region Internet Explorer
        [Test]
        public void InternetExplorer11_0_Windows_1()
        {
            System.Collections.Generic.Dictionary<string, string> Header;
            Header = new Dictionary<string, string>();
            Header.Add(@"User-Agent", @"Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; AS; rv:11.0) like Gecko");
            var RS2 = Detective.ProcessData(Header);

            ClassicAssert.AreEqual("Microsoft Internet Explorer", RS2.BrowserName, "browser");
            ClassicAssert.AreEqual(new Version("11.0"), RS2.version, "version");
            ClassicAssert.AreEqual("Microsoft Windows 7", RS2.OS, "os");
        }
        [Test]
        public void InternetExplorer11_0_Windows_2()
        {
            System.Collections.Generic.Dictionary<string, string> Header;
            Header = new Dictionary<string, string>();
            Header.Add(@"User-Agent", @"Mozilla/5.0 (compatible, MSIE 11, Windows NT 6.3; Trident/7.0; rv:11.0) like Gecko");
            var RS2 = Detective.ProcessData(Header);

            ClassicAssert.AreEqual("Microsoft Internet Explorer", RS2.BrowserName, "browser");
            ClassicAssert.AreEqual(new Version("11.0"), RS2.version, "version");
            ClassicAssert.AreEqual("Microsoft Windows 8.1", RS2.OS, "os");
        }

        [Test]
        public void InternetExplorer10_6_Windows_1()
        {
            System.Collections.Generic.Dictionary<string, string> Header;
            Header = new Dictionary<string, string>();
            Header.Add(@"User-Agent", @"Mozilla/5.0 (compatible; MSIE 10.6; Windows NT 6.1; Trident/5.0; InfoPath.2; SLCC1; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729; .NET CLR 2.0.50727) 3gpp-gba UNTRUSTED/1.0");
            var RS2 = Detective.ProcessData(Header);

            ClassicAssert.AreEqual("Microsoft Internet Explorer", RS2.BrowserName, "browser");
            ClassicAssert.AreEqual(new Version("9.0"), RS2.version, "version");
            ClassicAssert.AreEqual("Microsoft Windows 7", RS2.OS, "os");
        }

        [Test]
        public void InternetExplorer10_0_Windows_1()
        {
            System.Collections.Generic.Dictionary<string, string> Header;
            Header = new Dictionary<string, string>();
            Header.Add(@"User-Agent", @"Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.1; Trident/6.0)");
            var RS2 = Detective.ProcessData(Header);

            ClassicAssert.AreEqual("Microsoft Internet Explorer", RS2.BrowserName, "browser");
            ClassicAssert.AreEqual(new Version("10.0"), RS2.version, "version");
            ClassicAssert.AreEqual("Microsoft Windows 7", RS2.OS, "os");
        }
        [Test]
        public void InternetExplorer09_Windows_1()
        {
            System.Collections.Generic.Dictionary<string, string> Header;
            Header = new Dictionary<string, string>();
            Header.Add(@"User-Agent", @"Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0; SLCC2; Media Center PC 6.0; InfoPath.3; MS-RTC LM 8; Zune 4.7)");
            var RS2 = Detective.ProcessData(Header);

            ClassicAssert.AreEqual("Microsoft Internet Explorer", RS2.BrowserName, "browser");
            ClassicAssert.AreEqual(new Version("9.0"), RS2.version, "version");
            ClassicAssert.AreEqual("Microsoft Windows 7", RS2.OS, "os");
        }

        [Test]
        public void InternetExplorer08_0_Windows_1()
        {
            System.Collections.Generic.Dictionary<string, string> Header;
            Header = new Dictionary<string, string>();
            Header.Add(@"User-Agent", @"Mozilla/5.0 (compatible; MSIE 8.0; Windows NT 6.0; Trident/4.0; .NET CLR 2.7.58687; SLCC2; Media Center PC 5.0; Zune 3.4; Tablet PC 3.6; InfoPath.3)");
            var RS2 = Detective.ProcessData(Header);

            ClassicAssert.AreEqual("Microsoft Internet Explorer", RS2.BrowserName, "browser");
            ClassicAssert.AreEqual(new Version("8.0"), RS2.version, "version");
            ClassicAssert.AreEqual("Microsoft Windows Vista", RS2.OS, "os");
        }

        [Test]
        public void InternetExplorer08_0_Windows_2()
        {
            System.Collections.Generic.Dictionary<string, string> Header;
            Header = new Dictionary<string, string>();
            Header.Add(@"User-Agent", @"Mozilla/5.0 (compatible; MSIE 8.0; Windows NT 5.0; Trident/4.0; InfoPath.1; SV1; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729; .NET CLR 3.0.04506.30)");
            var RS2 = Detective.ProcessData(Header);

            ClassicAssert.AreEqual("Microsoft Internet Explorer", RS2.BrowserName, "browser");
            ClassicAssert.AreEqual(new Version("8.0"), RS2.version, "version");
            ClassicAssert.AreEqual("Microsoft Windows 2000", RS2.OS, "os");
        }
        [Test]
        public void InternetExplorer07_0_Windows_1()
        {
            System.Collections.Generic.Dictionary<string, string> Header;
            Header = new Dictionary<string, string>();
            Header.Add(@"User-Agent", @"Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; Win64; x64; Trident/4.0; .NET CLR 2.0.50727; SLCC2; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET4.0C; .NET4.0E; CMDTDFJS; f9J; InfoPath.3)");
            var RS2 = Detective.ProcessData(Header);

            ClassicAssert.AreEqual("Microsoft Internet Explorer", RS2.BrowserName, "browser");
            ClassicAssert.AreEqual(new Version("7.0"), RS2.version, "version");
            ClassicAssert.AreEqual("Microsoft Windows 7", RS2.OS, "os");
        }
        [Test]
        public void InternetExplorer6_0_Windows_1()
        {
            System.Collections.Generic.Dictionary<string, string> Header;
            Header = new Dictionary<string, string>();
            Header.Add(@"Cache-Control", @"max-age=259200");
            Header.Add(@"Connection", @"keep-alive");
            Header.Add(@"Via", @"1.0 server:8080 (squid/2.5.STABLE12)");
            Header.Add(@"Accept", @"*/*");
            Header.Add(@"Accept-Language", @"ur");
            Header.Add(@"User-Agent", @"Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1)");
            Header.Add(@"X-Forwarded-For", @"192.168.1.63");
            var RS2 = Detective.ProcessData(Header);

            ClassicAssert.AreEqual("Microsoft Internet Explorer", RS2.BrowserName, "browser");
            ClassicAssert.AreEqual(new Version("6.0"), RS2.version, "version");
            ClassicAssert.AreEqual("Microsoft Windows XP", RS2.OS, "os");
        }
        [Test]
        public void InternetExplorer05_0_Windows_1()
        {
            System.Collections.Generic.Dictionary<string, string> Header;
            Header = new Dictionary<string, string>();
            Header.Add(@"User-Agent", @"Mozilla/4.0 (compatible; MSIE 5.01; Windows NT 5.0)");
            var RS2 = Detective.ProcessData(Header);

            ClassicAssert.AreEqual("Microsoft Internet Explorer", RS2.BrowserName, "browser");
            ClassicAssert.AreEqual(new Version("5.01"), RS2.version, "version");
            ClassicAssert.AreEqual("Microsoft Windows 2000", RS2.OS, "os");
        }
        [Test]
        public void InternetExplorer05_15_MacintoshPPC_1()
        {
            System.Collections.Generic.Dictionary<string, string> Header;
            Header = new Dictionary<string, string>();
            Header.Add(@"User-Agent", @"Mozilla/4.0 (compatible; MSIE 5.15; Mac_PowerPC)");
            var RS2 = Detective.ProcessData(Header);

            ClassicAssert.AreEqual("Microsoft Internet Explorer", RS2.BrowserName, "browser");
            ClassicAssert.AreEqual(new Version("5.15"), RS2.version, "version");
            ClassicAssert.AreEqual("Macintosh PPC", RS2.OS, "os");
        }
        #endregion
        #region Edge
        [Test]
        public void MicrosoftEdge16_Windows_1()
        {
            System.Collections.Generic.Dictionary<string, string> Header;
            Header = new Dictionary<string, string>();
            Header.Add(@"User-Agent", @"Mozilla/5.0 (MSIE 9.0; Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36 Edge/16.16299");
            var RS2 = Detective.ProcessData(Header);

            foreach (var item in RS2.Trace)
            {
                Console.WriteLine(item);
            }

            ClassicAssert.AreEqual("Microsoft Edge Legacy", RS2.BrowserName, "browser");
            ClassicAssert.AreEqual(new Version("16.16299"), RS2.version, "version");
            ClassicAssert.AreEqual("Microsoft Windows 10", RS2.OS, "os");
        }
        [Test]
        public void MicrosoftEdge94_Windows_1()
        {
            System.Collections.Generic.Dictionary<string, string> Header;
            Header = new Dictionary<string, string>();
            Header.Add(@"User-Agent", @"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/94.0.4606.61 Safari/537.36 Edg/94.0.992.47");
            var RS2 = Detective.ProcessData(Header);

            foreach (var item in RS2.Trace)
            {
                Console.WriteLine(item);
            }

            ClassicAssert.AreEqual("Microsoft Edge", RS2.BrowserName, "browser");
            ClassicAssert.AreEqual(new Version("94.0.992"), RS2.version, "version");
            ClassicAssert.AreEqual("Microsoft Windows 10", RS2.OS, "os");
        }
        [Test]
        public void MicrosoftEdge107_MacintoshOSX_1()
        {
            System.Collections.Generic.Dictionary<string, string> Header;
            Header = new Dictionary<string, string>();
            Header.Add(@"User-Agent", @"Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/107.0.0.0 Safari/537.36 Edg/107.0.1418.35");
            var RS2 = Detective.ProcessData(Header);

            foreach (var item in RS2.Trace)
            {
                Console.WriteLine(item);
            }

            ClassicAssert.AreEqual("Microsoft Edge", RS2.BrowserName, "browser");
            ClassicAssert.AreEqual(new Version("107.0.1418"), RS2.version, "version");
            ClassicAssert.AreEqual("Macintosh OS X", RS2.OS, "os");
        }
        [Test]
        public void MicrosoftEdge_Android_1()
        {
            System.Collections.Generic.Dictionary<string, string> Header;
            Header = new Dictionary<string, string>();
            Header.Add(@"User-Agent", @"Mozilla/5.0 (Linux; Android 7.1.1; Moto E (4) Build/NDQS26.69-64-2) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/65.0.3325.109 Mobile Safari/537.36 EdgA/41.0.0.1921");
            var RS2 = Detective.ProcessData(Header);

            foreach (var item in RS2.Trace)
            {
                Console.WriteLine(item);
            }

            ClassicAssert.AreEqual("Microsoft Edge", RS2.BrowserName, "browser");
            ClassicAssert.AreEqual(new Version("41.0.0"), RS2.version, "version");
            ClassicAssert.AreEqual("Android 7.1.1", RS2.OS, "os");
        }
        [Test]
        public void MicrosoftEdge_Android_2()
        {
            System.Collections.Generic.Dictionary<string, string> Header;
            Header = new Dictionary<string, string>();
            Header.Add(@"User-Agent", @"Mozilla/5.0 (Linux; Android 12; SM-G780G) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/101.0.4951.54 Mobile Safari/537.36 EdgA/101.0.1210.39");
            var RS2 = Detective.ProcessData(Header);

            foreach (var item in RS2.Trace)
            {
                Console.WriteLine(item);
            }

            ClassicAssert.AreEqual("Microsoft Edge", RS2.BrowserName, "browser");
            ClassicAssert.AreEqual(new Version("101.0.1210"), RS2.version, "version");
            ClassicAssert.AreEqual("Android 12", RS2.OS, "os");
        }
        [Test]
        public void MicrosoftEdge_Android_3()
        {
            System.Collections.Generic.Dictionary<string, string> Header;
            Header = new Dictionary<string, string>();
            Header.Add(@"User-Agent", @"Mozilla/5.0 (Linux; Android 10; K) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/116.0.0.0 Mobile Safari/537.36 EdgA/116.0.1938.75");
            var RS2 = Detective.ProcessData(Header);

            foreach (var item in RS2.Trace)
            {
                Console.WriteLine(item);
            }

            ClassicAssert.AreEqual("Microsoft Edge", RS2.BrowserName, "browser");
            ClassicAssert.AreEqual(new Version("116.0.1938"), RS2.version, "version");
            ClassicAssert.AreEqual("Android 10", RS2.OS, "os");
        }
        #endregion

        //https://github.com/codewatchorg/Burp-UserAgent/blob/master/useragents.xml
    }
}