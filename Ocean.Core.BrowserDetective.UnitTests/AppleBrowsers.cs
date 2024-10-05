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
        #endregion
    }
}