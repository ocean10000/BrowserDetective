using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using System.Configuration;
using Ocean.Core.BrowserDetective.Extentions;
using NUnit.Framework.Legacy;
using Ocean.Core.BrowserDetective.Data.Context;
using System.Text;

namespace Ocean.Core.BrowserDetective.UnitTests
{
    //This is removing a anoying warnings.
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Assertion",
    "NUnit2005:Consider using ClassicAssert.That(actual, Is.EqualTo(expected)) instead of ClassicClassicAssert.AreEqual(expected, actual)",
    Justification = "Reason...")]
    public class Results2026 : ResultsYear
    {
        [SetUp]
        public void Setup()
        {
            //PreGenerated Results for 2024, so we can compare against
            resultContext = new Ocean.Core.BrowserDetective.Data.Context.ResultContext($"Data Source=Data/Core.Results.2026.db");

            //Headers captured in 2024
            Headercontext = new Ocean.Core.BrowserDetective.Data.Context.HeaderContext($"Data Source=Data/Headers2026.DB");

            ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddFilter("Ocean.Core.BrowserDetective", LogLevel.Error).AddConsole());
            ILogger logger = factory.CreateLogger(typeof(Ocean.Core.BrowserDetective.Process));
            Detective = new Ocean.Core.BrowserDetective.Process(logger, null);
        }

    }
}