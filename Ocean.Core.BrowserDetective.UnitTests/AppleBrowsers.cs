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
            ClassicAssert.AreEqual("iOS", RS2.OS, "os");
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
    }
}