using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using System.Configuration;
using Ocean.Core.BrowserDetective.Extentions;
using NUnit.Framework.Legacy;
using Ocean.Core.BrowserDetective.Data.Context;
using System.Text;

namespace Ocean.Core.BrowserDetective.UnitTests
{
    [TestFixture, Explicit]
    //This is removing a anoying warnings.
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Assertion",
    "NUnit2005:Consider using ClassicAssert.That(actual, Is.EqualTo(expected)) instead of ClassicClassicAssert.AreEqual(expected, actual)",
    Justification = "Reason...")]
    public class ResultsYear
    {
        protected Ocean.Core.BrowserDetective.Process Detective;
        protected Ocean.Core.BrowserDetective.Data.Context.HeaderContext Headercontext;
        protected Ocean.Core.BrowserDetective.Data.Context.ResultContext resultContext;

        [TearDown]
        public void Dispose()
        {
            resultContext.Dispose();
            Headercontext.Dispose();
        }

        [Test]
        public void Chrome()
        {
            //Only really care about the headers actually used in the Results table.
            var HeaderIDs = resultContext.Result.Where(X => X.BrowserName == "Chrome").Select(X => X.Raw_ID).Distinct().ToList();
            Test(HeaderIDs);
        }
        [Test]
        public void Chromium()
        {
            System.Collections.Generic.Dictionary<long, string> FailedTest = new Dictionary<long, string>();
            int Failed = 0;
            //Only really care about the headers actually used in the Results table.
            var HeaderIDs = resultContext.Result.Where(X => X.BrowserName == "Chromium").Select(X => X.Raw_ID).Distinct().ToList();
            Test(HeaderIDs);
        }

        [Test]
        public void FireFox()
        {
            //Only really care about the headers actually used in the Results table.
            var HeaderIDs = resultContext.Result.Where(X => X.BrowserName == "Firefox").Select(X => X.Raw_ID).Distinct().ToList();
            Test(HeaderIDs);
        }

        [Test]
        public void MSIE()
        {
            //Only really care about the headers actually used in the Results table.
            var HeaderIDs = resultContext.Result.Where(X => X.BrowserName == "Microsoft Internet Explorer").Select(X => X.Raw_ID).Distinct().ToList();
            Test(HeaderIDs);
        }

        [Test]
        public void Edge()
        {
            //Only really care about the headers actually used in the Results table.
            var HeaderIDs = resultContext.Result.Where(X => X.BrowserName == "Microsoft Edge").Select(X => X.Raw_ID).Distinct().ToList();
            Test(HeaderIDs);
        }

        [Test]
        public void EdgeLegacy()
        {
            //Only really care about the headers actually used in the Results table.
            var HeaderIDs = resultContext.Result.Where(X => X.BrowserName == "Microsoft Edge Legacy").Select(X => X.Raw_ID).Distinct().ToList();
            Test(HeaderIDs);
        }

        [Test]
        public void Safari()
        {
            //Only really care about the headers actually used in the Results table.
            var HeaderIDs = resultContext.Result.Where(X => X.BrowserName == "Safari").Select(X => X.Raw_ID).Distinct().ToList();
            Test(HeaderIDs);
        }

        [Test]
        public void Opera()
        {
            //Only really care about the headers actually used in the Results table.
            var HeaderIDs = resultContext.Result.Where(X => X.BrowserName == "Opera").Select(X => X.Raw_ID).Distinct().ToList();
            Test(HeaderIDs);
        }

        [Test]
        public void Netscape()
        {
            System.Collections.Generic.Dictionary<long, string> FailedTest = new Dictionary<long, string>();
            int Failed = 0;
            //Only really care about the headers actually used in the Results table.
            var HeaderIDs = resultContext.Result.Where(X => X.BrowserName == "Netscape").Select(X => X.Raw_ID).Distinct().ToList();
            Test(HeaderIDs);
        }

        [Test]
        public void GenericBrowser()
        {
            System.Collections.Generic.Dictionary<long, string> FailedTest = new Dictionary<long, string>();
            int Failed = 0;
            //Only really care about the headers actually used in the Results table.
            var HeaderIDs = resultContext.Result.Where(X => X.BrowserName == "Generic Browser").Select(X => X.Raw_ID).Distinct().ToList();
            Test(HeaderIDs);
        }


        [Test]
        public void Mozilla2010()
        {
            //Only really care about the headers actually used in the Results table.
            var HeaderIDs = resultContext.Result.Where(X => X.BrowserName == "Mozilla").Select(X => X.Raw_ID).Distinct().ToList();
            Test(HeaderIDs);
        }
        [Test]
        public void YaBrowser()
        {
            //Only really care about the headers actually used in the Results table.
            var HeaderIDs = resultContext.Result.Where(X => X.BrowserName == "YaBrowser").Select(X => X.Raw_ID).Distinct().ToList();
            Test(HeaderIDs);
        }

        [Test]
        public void QQBrowser()
        {
            //Only really care about the headers actually used in the Results table.
            var HeaderIDs = resultContext.Result.Where(X => X.BrowserName == "QQBrowser").Select(X => X.Raw_ID).Distinct().ToList();
            Test(HeaderIDs);
        }
        [Test]
        public void Konqueror()
        {
            //Only really care about the headers actually used in the Results table.
            var HeaderIDs = resultContext.Result.Where(X => X.BrowserName == "Konqueror").Select(X => X.Raw_ID).Distinct().ToList();
            Test(HeaderIDs);
        }

        [Test]
        public void Misc()
        {
            //Removing the most common Browsers at this time. and grouping the rest (anything with less then 10 matches)
            var HeaderIDs = resultContext.Result.Where(X => X.Crawler == "False" && X.isMobileDevice == "False"
            && X.BrowserName != "Chrome"
            && X.BrowserName != "Chromium"
            && X.BrowserName != "Firefox"
            && X.BrowserName != "Microsoft Internet Explorer"
            && X.BrowserName != "Microsoft Edge Legacy"
            && X.BrowserName != "Safari"
            && X.BrowserName != "Opera"
            && X.BrowserName != "Microsoft Edge"
            && X.BrowserName != "YaBrowser"
            && X.BrowserName != "QQBrowser"
            && X.BrowserName != "Generic Browser"
            && X.BrowserName != "Netscape"
            && X.BrowserName != "Mozilla"
            && X.BrowserName != "Konqueror"
            ).Select(X => X.Raw_ID).Distinct().ToList();
            Test(HeaderIDs);
        }

        [Test]
        public void Mobile()
        {
            //Only really care about the headers actually used in the Results table.
            var HeaderIDs = resultContext.Result.Where(X => X.isMobileDevice == "True"
            && X.mobileDeviceManufacturer != "Apple"
            && X.mobileDeviceManufacturer != "Xiaomi"
            && X.mobileDeviceManufacturer != "SAMSUNG"
            && X.mobileDeviceManufacturer != "Nexus"
            && X.mobileDeviceManufacturer != "LG"
            && X.mobileDeviceManufacturer != "HTC"
            && X.mobileDeviceManufacturer != "MOTOROLA"
            && X.mobileDeviceManufacturer != "Pixel"
            && X.mobileDeviceManufacturer != "T-Mobile"
            ).Select(X => X.Raw_ID).Distinct().ToList();
            Test(HeaderIDs);
        }


        [Test]
        public void Mobile_Apple()
        {
            //Only really care about the headers actually used in the Results table.
            var HeaderIDs = resultContext.Result.Where(X => X.isMobileDevice == "True" && X.mobileDeviceManufacturer == "Apple").Select(X => X.Raw_ID).Distinct().ToList();
            Test(HeaderIDs);
        }
        [Test]
        public void Mobile_Xiaomi()
        {
            //Only really care about the headers actually used in the Results table.
            var HeaderIDs = resultContext.Result.Where(X => X.isMobileDevice == "True" && X.mobileDeviceManufacturer== "Xiaomi").Select(X => X.Raw_ID).Distinct().ToList();
            Test(HeaderIDs);
        }
        [Test]
        public void Mobile_SAMSUNG()
        {
            //Only really care about the headers actually used in the Results table.
            var HeaderIDs = resultContext.Result.Where(X => X.isMobileDevice == "True" && X.mobileDeviceManufacturer == "SAMSUNG").Select(X => X.Raw_ID).Distinct().ToList();
            Test(HeaderIDs);
        }
        [Test]
        public void Mobile_Nexus()
        {
            //Only really care about the headers actually used in the Results table.
            var HeaderIDs = resultContext.Result.Where(X => X.isMobileDevice == "True" && X.mobileDeviceManufacturer == "Nexus").Select(X => X.Raw_ID).Distinct().ToList();
            Test(HeaderIDs);
        }
        [Test]
        public void Mobile_LG()
        {
            //Only really care about the headers actually used in the Results table.
            var HeaderIDs = resultContext.Result.Where(X => X.isMobileDevice == "True" && X.mobileDeviceManufacturer == "LG").Select(X => X.Raw_ID).Distinct().ToList();
            Test(HeaderIDs);
        }
        [Test]
        public void Mobile_HTC()
        {
            //Only really care about the headers actually used in the Results table.
            var HeaderIDs = resultContext.Result.Where(X => X.isMobileDevice == "True" && X.mobileDeviceManufacturer == "HTC").Select(X => X.Raw_ID).Distinct().ToList();
            Test(HeaderIDs);
        }
        [Test]
        public void Mobile_MOTOROLA()
        {
            //Only really care about the headers actually used in the Results table.
            var HeaderIDs = resultContext.Result.Where(X => X.isMobileDevice == "True" && X.mobileDeviceManufacturer == "MOTOROLA").Select(X => X.Raw_ID).Distinct().ToList();
            Test(HeaderIDs);
        }
        [Test]
        public void Mobile_Pixel()
        {
            //Only really care about the headers actually used in the Results table.
            var HeaderIDs = resultContext.Result.Where(X => X.isMobileDevice == "True" && X.mobileDeviceManufacturer == "Pixel").Select(X => X.Raw_ID).Distinct().ToList();
            Test(HeaderIDs);
        }
        [Test]
        public void Mobile_TMobile()
        {
            //Only really care about the headers actually used in the Results table.
            var HeaderIDs = resultContext.Result.Where(X => X.isMobileDevice == "True" && X.mobileDeviceManufacturer == "T-Mobile").Select(X => X.Raw_ID).Distinct().ToList();
            Test(HeaderIDs);
        }
        [Test]
        public void Robots()
        {
            //Only really care about the headers actually used in the Results table.
            var HeaderIDs = resultContext.Result.Where(X => X.Crawler == "True").Select(X => X.Raw_ID).Distinct().ToList();
            Test(HeaderIDs);
        }

        public void Test(List<long> HeaderIDs)
        {
            System.Collections.Generic.Dictionary<long, string> FailedTest = new Dictionary<long, string>();
            int Failed = 0;
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