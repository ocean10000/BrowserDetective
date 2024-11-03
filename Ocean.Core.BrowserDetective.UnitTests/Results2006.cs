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
    public class Results2006 : ResultsYear
    {
      
        [SetUp]
        public void Setup()
        {
            if (System.IO.File.Exists("Data/Core.Results.2006.db"))
            {
                //PreGenerated Results for 2024, so we can compare against
                resultContext = new Ocean.Core.BrowserDetective.Data.Context.ResultContext($"Data Source=Data/Core.Results.2006.db");
            }

            //Headers captured in 2024
            Headercontext = new Ocean.Core.BrowserDetective.Data.Context.HeaderContext($"Data Source=Data/Headers2006.DB");

            ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddFilter("Ocean.Core.BrowserDetective", LogLevel.Error).AddConsole());
            ILogger logger = factory.CreateLogger(typeof(Ocean.Core.BrowserDetective.Process));
            Detective = new Ocean.Core.BrowserDetective.Process(logger);
        }


        [Test]
        public void MozillaCamino()
        {
            //Only really care about the headers actually used in the Results table.
            var HeaderIDs = resultContext.Result.Where(X => X.BrowserName == "Mozilla Camino").Select(X => X.Raw_ID).Distinct().ToList();
            Test(HeaderIDs);
        }

        [Test]
        public void MozillaEpiphany()
        {
            //Only really care about the headers actually used in the Results table.
            var HeaderIDs = resultContext.Result.Where(X => X.BrowserName == "Mozilla Epiphany").Select(X => X.Raw_ID).Distinct().ToList();
            Test(HeaderIDs);
        }
    }
}