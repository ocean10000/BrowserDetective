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
    public class FireFoxBrowsersTests
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
        public void Netscape_Windows_1()
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
            ClassicAssert.AreEqual("Netscape", RS2["browser"], "browser");
            ClassicAssert.AreEqual(new Version("8.1"), RS2.version, "version");
            ClassicAssert.AreEqual("Microsoft Windows XP", RS2.OS, "os");
        }
        #region Firefox
        [Test]
        public void MozillaFirefox_Windows_1()
        {
            System.Collections.Generic.Dictionary<string, string> Header;
            Header = new Dictionary<string, string>();
            Header.Add(@"User-Agent", @"Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:93.0) Gecko/20100101 Firefox/93.0");
            var RS2 = Detective.ProcessData(Header);

            ClassicAssert.AreEqual("Firefox", RS2.BrowserName, "browser");
            ClassicAssert.AreEqual("Microsoft Windows 10", RS2.OS, "os");
        }
        [Test]
        public void MozillaFirefox_Windows_2()
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
            ClassicAssert.AreEqual("Firefox", RS2.BrowserName, "browser");
            ClassicAssert.AreEqual(new Version(1, 5, 0), RS2.version, "version");
            ClassicAssert.AreEqual("Microsoft Windows XP", RS2.OS, "os");
        }
        [Test]
        public void MozillaFirefox_macOS_1()
        {
            System.Collections.Generic.Dictionary<string, string> Header;
            Header = new Dictionary<string, string>();
            Header.Add(@"User-Agent", @"Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7; rv:93.0) Gecko/20100101 Firefox/93.0");
            var RS2 = Detective.ProcessData(Header);

            ClassicAssert.AreEqual("Firefox", RS2.BrowserName, "browser");
            ClassicAssert.AreEqual(new Version("93.0"), RS2.version, "version");
            ClassicAssert.AreEqual("Macintosh OS X", RS2.OS, "os");
        }
        #endregion
    }
}