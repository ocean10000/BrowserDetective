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
    public class Results2008
    {
        private Ocean.Core.BrowserDetective.Process Detective;
        private Ocean.Core.BrowserDetective.Data.Context.HeaderContext Headercontext;
        private Ocean.Core.BrowserDetective.Data.Context.ResultContext resultContext;

        [SetUp]
        public void Setup()
        {
            if (System.IO.File.Exists("Data/Core.Results.2008.db"))
            {
                //PreGenerated Results for 2024, so we can compare against
                resultContext = new Ocean.Core.BrowserDetective.Data.Context.ResultContext($"Data Source=Data/Core.Results.2008.db");
            }

            //Headers captured in 2024
            Headercontext = new Ocean.Core.BrowserDetective.Data.Context.HeaderContext($"Data Source=Data/Headers2008.DB");

            ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddFilter("Ocean.Core.BrowserDetective", LogLevel.Error).AddConsole());
            ILogger logger = factory.CreateLogger(typeof(Ocean.Core.BrowserDetective.Process));
            Detective = new Ocean.Core.BrowserDetective.Process(logger);
        }

        [TearDown]
        public void Dispose()
        {
            resultContext.Dispose();
            Headercontext.Dispose();
        }
        [Test]
        public void Chrome2008()
        {
            System.Collections.Generic.Dictionary<long, string> FailedTest = new Dictionary<long, string>();
            int Failed = 0;
            //Only really care about the headers actually used in the Results table.
            var HeaderIDs = resultContext.Result.Where(X => X.BrowserName == "Chrome").Select(X => X.Raw_ID).Distinct().ToList();
            foreach (var HeaderID in HeaderIDs)
            {
                string R = UnitTests(HeaderID);
                if (string.IsNullOrEmpty(R) == false)
                {
                    Failed++;
                    FailedTest.Add(HeaderID, R);
                }
            }
            Console.WriteLine($"Failed:{Failed} out of {HeaderIDs.Count}");
            Console.WriteLine();
            foreach (var key in FailedTest.Keys)
            {
                Console.WriteLine(FailedTest[key]);
            }
            if (FailedTest.Count > 0)
            {
                Assert.Fail();
            }
        }
        [Test]
        public void Chromium2008()
        {
            System.Collections.Generic.Dictionary<long, string> FailedTest = new Dictionary<long, string>();
            int Failed = 0;
            //Only really care about the headers actually used in the Results table.
            var HeaderIDs = resultContext.Result.Where(X => X.BrowserName == "Chromium").Select(X => X.Raw_ID).Distinct().ToList();
            foreach (var HeaderID in HeaderIDs)
            {
                string R = UnitTests(HeaderID);
                if (string.IsNullOrEmpty(R) == false)
                {
                    Failed++;
                    FailedTest.Add(HeaderID, R);
                }
            }
            Console.WriteLine($"Failed:{Failed} out of {HeaderIDs.Count}");
            Console.WriteLine();
            foreach (var key in FailedTest.Keys)
            {
                Console.WriteLine(FailedTest[key]);
            }
            if (FailedTest.Count > 0)
            {
                Assert.Fail();
            }
        }
        [Test]
        public void FireFox2008()
        {
            System.Collections.Generic.Dictionary<long, string> FailedTest = new Dictionary<long, string>();
            int Failed = 0;
            //Only really care about the headers actually used in the Results table.
            var HeaderIDs = resultContext.Result.Where(X => X.BrowserName == "Firefox").Select(X => X.Raw_ID).Distinct().ToList();
            foreach (var HeaderID in HeaderIDs)
            {
                string R = UnitTests(HeaderID);
                if (string.IsNullOrEmpty(R) == false)
                {
                    Failed++;
                    FailedTest.Add(HeaderID, R);
                }
            }
            Console.WriteLine($"Failed:{Failed} out of {HeaderIDs.Count}");
            Console.WriteLine();
            foreach (var key in FailedTest.Keys)
            {
                Console.WriteLine(FailedTest[key]);
            }
            if (FailedTest.Count > 0)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void GranParadiso2008()
        {
            System.Collections.Generic.Dictionary<long, string> FailedTest = new Dictionary<long, string>();
            int Failed = 0;
            //Only really care about the headers actually used in the Results table.
            var HeaderIDs = resultContext.Result.Where(X => X.BrowserName == "GranParadiso").Select(X => X.Raw_ID).Distinct().ToList();
            foreach (var HeaderID in HeaderIDs)
            {
                string R = UnitTests(HeaderID);
                if (string.IsNullOrEmpty(R) == false)
                {
                    Failed++;
                    FailedTest.Add(HeaderID, R);
                }
            }
            Console.WriteLine($"Failed:{Failed} out of {HeaderIDs.Count}");
            Console.WriteLine();
            foreach (var key in FailedTest.Keys)
            {
                Console.WriteLine(FailedTest[key]);
            }
            if (FailedTest.Count > 0)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void GenericBrowser2008()
        {
            System.Collections.Generic.Dictionary<long, string> FailedTest = new Dictionary<long, string>();
            int Failed = 0;
            //Only really care about the headers actually used in the Results table.
            var HeaderIDs = resultContext.Result.Where(X => X.BrowserName == "Generic Browser").Select(X => X.Raw_ID).Distinct().ToList();
            foreach (var HeaderID in HeaderIDs)
            {
                string R = UnitTests(HeaderID);
                if (string.IsNullOrEmpty(R) == false)
                {
                    Failed++;
                    FailedTest.Add(HeaderID, R);
                }
            }
            Console.WriteLine($"Failed:{Failed} out of {HeaderIDs.Count}");
            Console.WriteLine();
            foreach (var key in FailedTest.Keys)
            {
                Console.WriteLine(FailedTest[key]);
            }
            if (FailedTest.Count > 0)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void Iron2008()
        {
            System.Collections.Generic.Dictionary<long, string> FailedTest = new Dictionary<long, string>();
            int Failed = 0;
            //Only really care about the headers actually used in the Results table.
            var HeaderIDs = resultContext.Result.Where(X => X.BrowserName == "Iron").Select(X => X.Raw_ID).Distinct().ToList();
            foreach (var HeaderID in HeaderIDs)
            {
                string R = UnitTests(HeaderID);
                if (string.IsNullOrEmpty(R) == false)
                {
                    Failed++;
                    FailedTest.Add(HeaderID, R);
                }
            }
            Console.WriteLine($"Failed:{Failed} out of {HeaderIDs.Count}");
            Console.WriteLine();
            foreach (var key in FailedTest.Keys)
            {
                Console.WriteLine(FailedTest[key]);
            }
            if (FailedTest.Count > 0)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void Konqueror2008()
        {
            System.Collections.Generic.Dictionary<long, string> FailedTest = new Dictionary<long, string>();
            int Failed = 0;
            //Only really care about the headers actually used in the Results table.
            var HeaderIDs = resultContext.Result.Where(X => X.BrowserName == "Konqueror").Select(X => X.Raw_ID).Distinct().ToList();
            foreach (var HeaderID in HeaderIDs)
            {
                string R = UnitTests(HeaderID);
                if (string.IsNullOrEmpty(R) == false)
                {
                    Failed++;
                    FailedTest.Add(HeaderID, R);
                }
            }
            Console.WriteLine($"Failed:{Failed} out of {HeaderIDs.Count}");
            Console.WriteLine();
            foreach (var key in FailedTest.Keys)
            {
                Console.WriteLine(FailedTest[key]);
            }
            if (FailedTest.Count > 0)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void Text2008()
        {
            System.Collections.Generic.Dictionary<long, string> FailedTest = new Dictionary<long, string>();
            int Failed = 0;
            //Only really care about the headers actually used in the Results table.
            var HeaderIDs = resultContext.Result.Where(X => X.BrowserName == "Links" || X.BrowserName == "Lynx").Select(X => X.Raw_ID).Distinct().ToList();
            foreach (var HeaderID in HeaderIDs)
            {
                string R = UnitTests(HeaderID);
                if (string.IsNullOrEmpty(R) == false)
                {
                    Failed++;
                    FailedTest.Add(HeaderID, R);
                }
            }
            Console.WriteLine($"Failed:{Failed} out of {HeaderIDs.Count}");
            Console.WriteLine();
            foreach (var key in FailedTest.Keys)
            {
                Console.WriteLine(FailedTest[key]);
            }
            if (FailedTest.Count > 0)
            {
                Assert.Fail();
            }
        }


        [Test]
        public void MalformedUA2008()
        {
            System.Collections.Generic.Dictionary<long, string> FailedTest = new Dictionary<long, string>();
            int Failed = 0;
            //Only really care about the headers actually used in the Results table.
            var HeaderIDs = resultContext.Result.Where(X => X.BrowserName == "Malformed User-Agent").Select(X => X.Raw_ID).Distinct().ToList();
            foreach (var HeaderID in HeaderIDs)
            {
                string R = UnitTests(HeaderID);
                if (string.IsNullOrEmpty(R) == false)
                {
                    Failed++;
                    FailedTest.Add(HeaderID, R);
                }
            }
            Console.WriteLine($"Failed:{Failed} out of {HeaderIDs.Count}");
            Console.WriteLine();
            foreach (var key in FailedTest.Keys)
            {
                Console.WriteLine(FailedTest[key]);
            }
            if (FailedTest.Count > 0)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void MSIE2008()
        {
            System.Collections.Generic.Dictionary<long, string> FailedTest = new Dictionary<long, string>();
            int Failed = 0;
            //Only really care about the headers actually used in the Results table.
            var HeaderIDs = resultContext.Result.Where(X => X.BrowserName == "Microsoft Internet Explorer").Select(X => X.Raw_ID).Distinct().ToList();
            foreach (var HeaderID in HeaderIDs)
            {
                string R = UnitTests(HeaderID);
                if (string.IsNullOrEmpty(R) == false)
                {
                    Failed++;
                    FailedTest.Add(HeaderID, R);
                }
            }
            Console.WriteLine($"Failed:{Failed} out of {HeaderIDs.Count}");
            Console.WriteLine();
            foreach (var key in FailedTest.Keys)
            {
                Console.WriteLine(FailedTest[key]);
            }
            if (FailedTest.Count > 0)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void MobileSafari2008()
        {
            System.Collections.Generic.Dictionary<long, string> FailedTest = new Dictionary<long, string>();
            int Failed = 0;
            //Only really care about the headers actually used in the Results table.
            var HeaderIDs = resultContext.Result.Where(X => X.BrowserName == "Mobile Safari").Select(X => X.Raw_ID).Distinct().ToList();
            foreach (var HeaderID in HeaderIDs)
            {
                string R = UnitTests(HeaderID);
                if (string.IsNullOrEmpty(R) == false)
                {
                    Failed++;
                    FailedTest.Add(HeaderID, R);
                }
            }
            Console.WriteLine($"Failed:{Failed} out of {HeaderIDs.Count}");
            Console.WriteLine();
            foreach (var key in FailedTest.Keys)
            {
                Console.WriteLine(FailedTest[key]);
            }
            if (FailedTest.Count > 0)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void Mozilla2008()
        {
            System.Collections.Generic.Dictionary<long, string> FailedTest = new Dictionary<long, string>();
            int Failed = 0;
            //Only really care about the headers actually used in the Results table.
            var HeaderIDs = resultContext.Result.Where(X => X.BrowserName == "Mozilla").Select(X => X.Raw_ID).Distinct().ToList();
            foreach (var HeaderID in HeaderIDs)
            {
                string R = UnitTests(HeaderID);
                if (string.IsNullOrEmpty(R) == false)
                {
                    Failed++;
                    FailedTest.Add(HeaderID, R);
                }
            }
            Console.WriteLine($"Failed:{Failed} out of {HeaderIDs.Count}");
            Console.WriteLine();
            foreach (var key in FailedTest.Keys)
            {
                Console.WriteLine(FailedTest[key]);
            }
            if (FailedTest.Count > 0)
            {
                Assert.Fail();
            }
        }


        [Test]
        public void Netscape2008()
        {
            System.Collections.Generic.Dictionary<long, string> FailedTest = new Dictionary<long, string>();
            int Failed = 0;
            //Only really care about the headers actually used in the Results table.
            var HeaderIDs = resultContext.Result.Where(X => X.BrowserName == "Netscape").Select(X => X.Raw_ID).Distinct().ToList();
            foreach (var HeaderID in HeaderIDs)
            {
                string R = UnitTests(HeaderID);
                if (string.IsNullOrEmpty(R) == false)
                {
                    Failed++;
                    FailedTest.Add(HeaderID, R);
                }
            }
            Console.WriteLine($"Failed:{Failed} out of {HeaderIDs.Count}");
            Console.WriteLine();
            foreach (var key in FailedTest.Keys)
            {
                Console.WriteLine(FailedTest[key]);
            }
            if (FailedTest.Count > 0)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void Opera2008()
        {
            System.Collections.Generic.Dictionary<long, string> FailedTest = new Dictionary<long, string>();
            int Failed = 0;
            //Only really care about the headers actually used in the Results table.
            var HeaderIDs = resultContext.Result.Where(X => X.BrowserName == "Opera").Select(X => X.Raw_ID).Distinct().ToList();
            foreach (var HeaderID in HeaderIDs)
            {
                string R = UnitTests(HeaderID);
                if (string.IsNullOrEmpty(R) == false)
                {
                    Failed++;
                    FailedTest.Add(HeaderID, R);
                }
            }
            Console.WriteLine($"Failed:{Failed} out of {HeaderIDs.Count}");
            Console.WriteLine();
            foreach (var key in FailedTest.Keys)
            {
                Console.WriteLine(FailedTest[key]);
            }
            if (FailedTest.Count > 0)
            {
                Assert.Fail();
            }
        }


        [Test]
        public void Mobile2008()
        {
            System.Collections.Generic.Dictionary<long, string> FailedTest = new Dictionary<long, string>();
            int Failed = 0;
            //Only really care about the headers actually used in the Results table.
            var HeaderIDs = resultContext.Result.Where(X => X.isMobileDevice == "True").Select(X => X.Raw_ID).Distinct().ToList();
            foreach (var HeaderID in HeaderIDs)
            {
                string R = UnitTests(HeaderID);
                if (string.IsNullOrEmpty(R) == false)
                {
                    Failed++;
                    FailedTest.Add(HeaderID, R);
                }
            }
            Console.WriteLine($"Failed:{Failed} out of {HeaderIDs.Count}");
            Console.WriteLine();
            foreach (var key in FailedTest.Keys)
            {
                Console.WriteLine(FailedTest[key]);
            }
            if (FailedTest.Count > 0)
            {
                Assert.Fail();
            }
        }


        [Test]
        public void PlayStation2008()
        {
            System.Collections.Generic.Dictionary<long, string> FailedTest = new Dictionary<long, string>();
            int Failed = 0;
            //Only really care about the headers actually used in the Results table.
            var HeaderIDs = resultContext.Result.Where(X => X.BrowserName.StartsWith("Playstation")).Select(X => X.Raw_ID).Distinct().ToList();
            foreach (var HeaderID in HeaderIDs)
            {
                string R = UnitTests(HeaderID);
                if (string.IsNullOrEmpty(R) == false)
                {
                    Failed++;
                    FailedTest.Add(HeaderID, R);
                }
            }
            Console.WriteLine($"Failed:{Failed} out of {HeaderIDs.Count}");
            Console.WriteLine();
            foreach (var key in FailedTest.Keys)
            {
                Console.WriteLine(FailedTest[key]);
            }
            if (FailedTest.Count > 0)
            {
                Assert.Fail();
            }
        }
        [Test]
        public void Safari2008()
        {
            System.Collections.Generic.Dictionary<long, string> FailedTest = new Dictionary<long, string>();
            int Failed = 0;
            //Only really care about the headers actually used in the Results table.
            var HeaderIDs = resultContext.Result.Where(X => X.BrowserName == "Safari").Select(X => X.Raw_ID).Distinct().ToList();
            foreach (var HeaderID in HeaderIDs)
            {
                string R = UnitTests(HeaderID);
                if (string.IsNullOrEmpty(R) == false)
                {
                    Failed++;
                    FailedTest.Add(HeaderID, R);
                }
            }
            Console.WriteLine($"Failed:{Failed} out of {HeaderIDs.Count}");
            Console.WriteLine();
            foreach (var key in FailedTest.Keys)
            {
                Console.WriteLine(FailedTest[key]);
            }
            if (FailedTest.Count > 0)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void Robots2008()
        {
            System.Collections.Generic.Dictionary<long, string> FailedTest = new Dictionary<long, string>();
            int Failed = 0;
            //Only really care about the headers actually used in the Results table.
            var HeaderIDs = resultContext.Result.Where(X => X.Crawler == "True").Select(X => X.Raw_ID).Distinct().ToList();
            foreach (var HeaderID in HeaderIDs)
            {
                string R = UnitTests(HeaderID);
                if (string.IsNullOrEmpty(R) == false)
                {
                    Failed++;
                    FailedTest.Add(HeaderID, R);
                }
            }
            Console.WriteLine($"Failed:{Failed} out of {HeaderIDs.Count}");
            Console.WriteLine();
            foreach (var key in FailedTest.Keys)
            {
                Console.WriteLine(FailedTest[key]);
            }
            if (FailedTest.Count > 0)
            {
                Assert.Fail();
            }
        }

        public string UnitTests(long HeaderID)
        {
            StringBuilder SB = new StringBuilder();
            bool DiditPass = true;
            //Get the Raw Headers used.
            var dic = Headercontext.Headers.Where(X => X.Raw_ID == HeaderID).ToDictionary(X => X.Name, X => X.Value);
            //Get a copy of the previous Result Dictionary
            var RS1 = resultContext.Results.Where(X => X.Raw_ID == HeaderID).ToDictionary(X => X.Name, X => X.Value);

            RS1.Remove("Header Checksum");
            RS1.Remove("Process Time");

            //Get a copy of the current Results based on the Previously used Headers.
            var RS2 = Detective.ProcessData(dic);

            foreach (var Key in RS2.Keys)
            {
                if (RS1.ContainsKey(Key))
                {
                    if (RS1[Key] != RS2[Key])
                    {
                        SB.AppendLine($"Failed: \"{Key}\"");
                        SB.AppendLine($"\"{RS1[Key]}\" != \"{RS2[Key]}\"");
                        DiditPass = false;
                    }
                }
                else
                {
                    SB.AppendLine($"Missing Key: \"{Key}\" in Previously Saved Results");
                    DiditPass = false;
                }
            }
            if (DiditPass == false)
            {
                SB.AppendLine();
                SB.AppendLine($"***Header ID: {HeaderID}");
                foreach (var Key in dic.Keys)
                {
                    SB.AppendLine($"{Key}: {dic[Key]}");
                }
                SB.AppendLine();
                SB.AppendLine($"**Expected");
                foreach (var Key in RS1.Keys)
                {
                    SB.AppendLine($"{Key}: {RS1[Key]}");
                }
                SB.AppendLine();
                SB.AppendLine($"**Actual");
                foreach (var Key in RS2.Keys)
                {
                    SB.AppendLine($"{Key}: {RS2[Key]}");
                }
                SB.AppendLine();
                return SB.ToString();
            }

            return string.Empty;
        }
    }
}