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
    public class ConsolesTests
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
        public void Bunjalloo_Nintendo_1()
        {
            //Web browser for the Nintendo DS
            System.Collections.Generic.Dictionary<string, string> Header;
            Header = new Dictionary<string, string>();
            Header.Add(@"User-Agent", @"Bunjalloo/0.7.6(Nintendo DS;U;en)");
            var RS2 = Detective.ProcessData(Header);

            ClassicAssert.AreEqual("Bunjalloo", RS2.BrowserName, "browser");
            ClassicAssert.AreEqual(new Version("0.7.6"), RS2.version, "version");
            ClassicAssert.AreEqual("Nintendo DS", RS2.OS, "os");
        }

        [Test]
        public void wii_1()
        {
            System.Collections.Generic.Dictionary<string, string> Header;
            Header = new Dictionary<string, string>();
            Header.Add(@"User-Agent", @"Opera/9.30 (Nintendo Wii; U; ; 2071; Wii Shop Channel/1.0; en)");
            var RS2 = Detective.ProcessData(Header);

            ClassicAssert.AreEqual("Opera", RS2.BrowserName, "browser");
            ClassicAssert.AreEqual(new Version("9.30"), RS2.version, "version");
            ClassicAssert.AreEqual("Nintendo Wii", RS2.OS, "os");
        }
        [Test]
        public void PLAYSTATION3_1()
        {
            System.Collections.Generic.Dictionary<string, string> Header;
            Header = new Dictionary<string, string>();
            Header.Add(@"User-Agent", @"Mozilla/5.0 (PLAYSTATION 3; 3.55)");
            var RS2 = Detective.ProcessData(Header);

            ClassicAssert.AreEqual("PLAYSTATION 3", RS2.BrowserName, "browser");
            ClassicAssert.AreEqual(new Version("3.55"), RS2.version, "version");
            ClassicAssert.AreEqual("Unknown", RS2.OS, "os");
        }

        [Test]
        public void PlayStationPortable_1()
        {
            System.Collections.Generic.Dictionary<string, string> Header;
            Header = new Dictionary<string, string>();
            Header.Add(@"User-Agent", @"PSP (PlayStation Portable); 2.00");
            var RS2 = Detective.ProcessData(Header);

            ClassicAssert.AreEqual("PlayStation Portable", RS2.BrowserName, "browser");
            ClassicAssert.AreEqual(new Version(" 2.00"), RS2.version, "version");
            ClassicAssert.AreEqual("Unknown", RS2.OS, "os");
        }
    }
}