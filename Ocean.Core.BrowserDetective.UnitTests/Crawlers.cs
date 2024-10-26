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
    public class CrawlersTests
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
        public void Mozlila_Bot_1()
        {
            //Mozilla is misspelled as "Mozlila". So this is not Chrome and should be treated as a crawler/bot.
            System.Collections.Generic.Dictionary<string, string> Header;
            Header = new Dictionary<string, string>();
            Header.Add(@"User-Agent", @"Mozlila/5.0 (Linux; Android 7.0; SM-G892A Bulid/NRD90M; wv) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/60.0.3112.107 Moblie Safari/537.36");
            var RS2 = Detective.ProcessData(Header);

            foreach (var item in RS2.Trace)
            {
                Console.WriteLine(item);
            }

            ClassicAssert.AreEqual("Mozlila", RS2.BrowserName, "browser");
            ClassicAssert.AreEqual(new Version("5.0"), RS2.version, "version");
            ClassicAssert.AreEqual(string.Empty, RS2.OS, "os");

            ClassicAssert.AreEqual(true, RS2.Crawler, "Crawler");
            ClassicAssert.AreEqual(false, RS2.isMobileDevice, "isMobileDevice");
            ClassicAssert.AreEqual(string.Empty, RS2.mobileDeviceModel, "mobileDeviceModel");
            ClassicAssert.AreEqual(string.Empty, RS2.mobileDeviceManufacturer, "mobileDeviceManufacturer");
            ClassicAssert.AreEqual(false, RS2.IsRandomRobobotUserAgent, "IsRandomRobobotUserAgent");
        }

        [Test]
        public void PetalBot_1()
        {
            //https://stackoverflow.com/questions/23804278/browser-sending-dalvik-as-user-agent
            Dictionary<string, string> Header;
            Header = new Dictionary<string, string>();
            Header.Add(@"User-Agent", @"Mozilla/5.0 (Linux; Android 7.0;) AppleWebKit/537.36 (KHTML, like Gecko) Mobile Safari/537.36 (compatible; PetalBot;+https://webmaster.petalsearch.com/site/petalbot)");
            var RS2 = Detective.ProcessData(Header);

            foreach (var item in RS2.Trace)
            {
                Console.WriteLine(item);
            }

            ClassicAssert.AreEqual("PetalBot", RS2.BrowserName, "browser");
            ClassicAssert.AreEqual(new Version("0.0"), RS2.version, "version");
            ClassicAssert.AreEqual(string.Empty, RS2.OS, "os");

            ClassicAssert.AreEqual(true, RS2.Crawler, "Crawler");
            ClassicAssert.AreEqual(false, RS2.isMobileDevice, "isMobileDevice");
            ClassicAssert.AreEqual(string.Empty, RS2.mobileDeviceModel, "mobileDeviceModel");
            ClassicAssert.AreEqual(string.Empty, RS2.mobileDeviceManufacturer, "mobileDeviceManufacturer");
            ClassicAssert.AreEqual(false, RS2.IsRandomRobobotUserAgent, "IsRandomRobobotUserAgent");
        }


        [Test]
        public void PetalBot_2()
        {
            //https://stackoverflow.com/questions/23804278/browser-sending-dalvik-as-user-agent
            Dictionary<string, string> Header;
            Header = new Dictionary<string, string>();
            Header.Add(@"User-Agent", @"Mozilla/5.0 (compatible;PetalBot;+https://webmaster.petalsearch.com/site/petalbot)");
            var RS2 = Detective.ProcessData(Header);

            foreach (var item in RS2.Trace)
            {
                Console.WriteLine(item);
            }

            ClassicAssert.AreEqual("PetalBot", RS2.BrowserName, "browser");
            ClassicAssert.AreEqual(new Version("0.0"), RS2.version, "version");
            ClassicAssert.AreEqual(string.Empty, RS2.OS, "os");

            ClassicAssert.AreEqual(true, RS2.Crawler, "Crawler");
            ClassicAssert.AreEqual(false, RS2.isMobileDevice, "isMobileDevice");
            ClassicAssert.AreEqual(string.Empty, RS2.mobileDeviceModel, "mobileDeviceModel");
            ClassicAssert.AreEqual(string.Empty, RS2.mobileDeviceManufacturer, "mobileDeviceManufacturer");
            ClassicAssert.AreEqual(false, RS2.IsRandomRobobotUserAgent, "IsRandomRobobotUserAgent");
        }

        [Test]
        public void SemanticScholarBot_1()
        {
            //https://stackoverflow.com/questions/23804278/browser-sending-dalvik-as-user-agent
            Dictionary<string, string> Header;
            Header = new Dictionary<string, string>();
            Header.Add(@"User-Agent", @"Mozilla/5.0 (compatible) SemanticScholarBot (+https://www.semanticscholar.org/crawler)");
            var RS2 = Detective.ProcessData(Header);

            foreach (var item in RS2.Trace)
            {
                Console.WriteLine(item);
            }

            ClassicAssert.AreEqual("SemanticScholarBot", RS2.BrowserName, "browser");
            ClassicAssert.AreEqual(new Version("0.0"), RS2.version, "version");
            ClassicAssert.AreEqual(string.Empty, RS2.OS, "os");

            ClassicAssert.AreEqual(true, RS2.Crawler, "Crawler");
            ClassicAssert.AreEqual(false, RS2.isMobileDevice, "isMobileDevice");
            ClassicAssert.AreEqual(string.Empty, RS2.mobileDeviceModel, "mobileDeviceModel");
            ClassicAssert.AreEqual(string.Empty, RS2.mobileDeviceManufacturer, "mobileDeviceManufacturer");
            ClassicAssert.AreEqual(false, RS2.IsRandomRobobotUserAgent, "IsRandomRobobotUserAgent");
        }

        [Test]
        public void ScreamingFrogSEOSpider_1()
        {
            //https://stackoverflow.com/questions/23804278/browser-sending-dalvik-as-user-agent
            Dictionary<string, string> Header;
            Header = new Dictionary<string, string>();
            Header.Add(@"User-Agent", @"Screaming Frog SEO Spider/14.3");
            var RS2 = Detective.ProcessData(Header);

            foreach (var item in RS2.Trace)
            {
                Console.WriteLine(item);
            }

            ClassicAssert.AreEqual("Screaming Frog SEO Spider", RS2.BrowserName, "browser");
            ClassicAssert.AreEqual(new Version("14.3"), RS2.version, "version");
            ClassicAssert.AreEqual(string.Empty, RS2.OS, "os");

            ClassicAssert.AreEqual(true, RS2.Crawler, "Crawler");
            ClassicAssert.AreEqual(false, RS2.isMobileDevice, "isMobileDevice");
            ClassicAssert.AreEqual(string.Empty, RS2.mobileDeviceModel, "mobileDeviceModel");
            ClassicAssert.AreEqual(string.Empty, RS2.mobileDeviceManufacturer, "mobileDeviceManufacturer");
            ClassicAssert.AreEqual(false, RS2.IsRandomRobobotUserAgent, "IsRandomRobobotUserAgent");
        }


        [Test]
        public void Mail_RU_Bot_1()
        {
            //https://stackoverflow.com/questions/23804278/browser-sending-dalvik-as-user-agent
            Dictionary<string, string> Header;
            Header = new Dictionary<string, string>();
            Header.Add(@"User-Agent", @"Mozilla/5.0 (compatible; Linux x86_64; Mail.RU_Bot/Robots/2.0; +https://help.mail.ru/webmaster/indexing/robots)");
            var RS2 = Detective.ProcessData(Header);

            foreach (var item in RS2.Trace)
            {
                Console.WriteLine(item);
            }

            ClassicAssert.AreEqual("Mail.RU_Bot", RS2.BrowserName, "browser");
            ClassicAssert.AreEqual(new Version("2.0"), RS2.version, "version");
            ClassicAssert.AreEqual(string.Empty, RS2.OS, "os");

            ClassicAssert.AreEqual(true, RS2.Crawler, "Crawler");
            ClassicAssert.AreEqual(false, RS2.isMobileDevice, "isMobileDevice");
            ClassicAssert.AreEqual(string.Empty, RS2.mobileDeviceModel, "mobileDeviceModel");
            ClassicAssert.AreEqual(string.Empty, RS2.mobileDeviceManufacturer, "mobileDeviceManufacturer");
            ClassicAssert.AreEqual(false, RS2.IsRandomRobobotUserAgent, "IsRandomRobobotUserAgent");
        }


        [Test]
        public void Amazonbot_1()
        {
            //https://stackoverflow.com/questions/23804278/browser-sending-dalvik-as-user-agent
            Dictionary<string, string> Header;
            Header = new Dictionary<string, string>();
            Header.Add(@"User-Agent", @"Mozilla/5.0 (Macintosh; Intel Mac OS X 10_10_1) AppleWebKit/600.2.5 (KHTML, like Gecko) Version/8.0.2 Safari/600.2.5 (Amazonbot/0.1; +https://developer.amazon.com/support/amazonbot)");
            var RS2 = Detective.ProcessData(Header);

            foreach (var item in RS2.Trace)
            {
                Console.WriteLine(item);
            }

            ClassicAssert.AreEqual("Amazonbot", RS2.BrowserName, "browser");
            ClassicAssert.AreEqual(new Version("0.1"), RS2.version, "version");
            ClassicAssert.AreEqual(string.Empty, RS2.OS, "os");

            ClassicAssert.AreEqual(true, RS2.Crawler, "Crawler");
            ClassicAssert.AreEqual(false, RS2.isMobileDevice, "isMobileDevice");
            ClassicAssert.AreEqual(string.Empty, RS2.mobileDeviceModel, "mobileDeviceModel");
            ClassicAssert.AreEqual(string.Empty, RS2.mobileDeviceManufacturer, "mobileDeviceManufacturer");
            ClassicAssert.AreEqual(false, RS2.IsRandomRobobotUserAgent, "IsRandomRobobotUserAgent");
        }


        [Test]
        public void Nicecrawler_1()
        {
            //https://stackoverflow.com/questions/23804278/browser-sending-dalvik-as-user-agent
            Dictionary<string, string> Header;
            Header = new Dictionary<string, string>();
            Header.Add(@"User-Agent", @"Mozilla/5.0 AppleWebKit/537.36 (KHTML, like Gecko; compatible; Nicecrawler/1.1; +http://www.nicecrawler.com/) Chrome/90.0.4430.97 Safari/537.36");
            var RS2 = Detective.ProcessData(Header);

            foreach (var item in RS2.Trace)
            {
                Console.WriteLine(item);
            }

            ClassicAssert.AreEqual("Nicecrawler", RS2.BrowserName, "browser");
            ClassicAssert.AreEqual(new Version("1.1"), RS2.version, "version");
            ClassicAssert.AreEqual(string.Empty, RS2.OS, "os");

            ClassicAssert.AreEqual(true, RS2.Crawler, "Crawler");
            ClassicAssert.AreEqual(false, RS2.isMobileDevice, "isMobileDevice");
            ClassicAssert.AreEqual(string.Empty, RS2.mobileDeviceModel, "mobileDeviceModel");
            ClassicAssert.AreEqual(string.Empty, RS2.mobileDeviceManufacturer, "mobileDeviceManufacturer");
            ClassicAssert.AreEqual(false, RS2.IsRandomRobobotUserAgent, "IsRandomRobobotUserAgent");
        }

        public void Bytespider_1()
        {
            System.Collections.Generic.Dictionary<string, string> Header;
            Header = new Dictionary<string, string>();
            Header.Add(@"User-Agent", @"Mozilla/5.0 (Linux; Android 5.0) AppleWebKit/537.36 (KHTML, like Gecko) Mobile Safari/537.36 (compatible; Bytespider; spider-feedback@bytedance.com)");
            var RS2 = Detective.ProcessData(Header);

            ClassicAssert.AreEqual("Bytespider", RS2.BrowserName, "browser");
            ClassicAssert.AreEqual(true, RS2.Crawler, "Crawler");
            ClassicAssert.AreEqual(false, RS2.IsRandomRobobotUserAgent, "IsRandomRobobotUserAgent");
        }

        //this is Chrome Model trying to act as the Desktop Model.
        [Test]
        public void Bytespider_2()
        {
            System.Collections.Generic.Dictionary<string, string> Header;
            Header = new Dictionary<string, string>();
            Header.Add(@"User-Agent", @"Mozilla/5.0 (Linux; Android 5.0) AppleWebKit/537.36 (KHTML, like Gecko) Mobile Safari/537.36 (compatible; Bytespider; https://zhanzhang.toutiao.com/)");
            var RS2 = Detective.ProcessData(Header);

            ClassicAssert.AreEqual("Bytespider", RS2.BrowserName, "browser");
            ClassicAssert.AreEqual(true, RS2.Crawler, "Crawler");
            ClassicAssert.AreEqual(false, RS2.IsRandomRobobotUserAgent, "IsRandomRobobotUserAgent");
        }

        [Test]
        public void certifytheweb_1()
        {
            System.Collections.Generic.Dictionary<string, string> Header;
            Header = new Dictionary<string, string>();
            Header.Add(@"cf-worker", @"certifytheweb.com");
            var RS2 = Detective.ProcessData(Header);

            ClassicAssert.AreEqual("certifytheweb.com", RS2.BrowserName, "browser");
            ClassicAssert.AreEqual(new Version("0.0"), RS2.version, "version");
            ClassicAssert.AreEqual(string.Empty, RS2.OS, "os");
            ClassicAssert.AreEqual(true, RS2.Crawler, "Crawler");
            ClassicAssert.AreEqual(false, RS2.isMobileDevice, "isMobileDevice");
            ClassicAssert.AreEqual(false, RS2.IsRandomRobobotUserAgent, "IsRandomRobobotUserAgent");
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

            ClassicAssert.AreEqual("GPTBot", RS2.BrowserName, "browser");
            ClassicAssert.AreEqual(new Version("1.0"), RS2.version, "version");
            ClassicAssert.AreEqual(true, RS2.Crawler, "Crawler");
            ClassicAssert.AreEqual(false, RS2.isMobileDevice, "isMobileDevice");
            ClassicAssert.AreEqual(string.Empty, RS2.OS, "os"); //https://en.wikipedia.org/wiki/Symbian
            ClassicAssert.AreEqual(false, RS2.IsRandomRobobotUserAgent, "IsRandomRobobotUserAgent");
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

            ClassicAssert.AreEqual("bingbot", RS2.BrowserName, "browser");
            ClassicAssert.AreEqual(new Version("2.0"), RS2.version, "version");
            ClassicAssert.AreEqual(true, RS2.Crawler, "Crawler");
            ClassicAssert.AreEqual(false, RS2.isMobileDevice, "isMobileDevice");
            ClassicAssert.AreEqual(string.Empty, RS2.OS, "os"); //https://en.wikipedia.org/wiki/Symbian
            ClassicAssert.AreEqual(false, RS2.IsRandomRobobotUserAgent, "IsRandomRobobotUserAgent");
        }


        [Test]
        public void IonCrawl_1()
        {
            System.Collections.Generic.Dictionary<string, string> Header;
            Header = new Dictionary<string, string>();
            Header.Add(@"User-Agent", @"IonCrawl (https://www.ionos.de/terms-gtc/faq-crawler-en/)");
            var RS2 = Detective.ProcessData(Header);

            foreach (var item in RS2.Trace)
            {
                Console.WriteLine(item);
            }

            ClassicAssert.AreEqual("IonCrawl", RS2.BrowserName, "browser");
            ClassicAssert.AreEqual(new Version("0.0"), RS2.version, "version");
            ClassicAssert.AreEqual(true, RS2.Crawler, "Crawler");
            ClassicAssert.AreEqual(false, RS2.isMobileDevice, "isMobileDevice");
            ClassicAssert.AreEqual(string.Empty, RS2.OS, "os"); //https://en.wikipedia.org/wiki/Symbian
            ClassicAssert.AreEqual(false, RS2.IsRandomRobobotUserAgent, "IsRandomRobobotUserAgent");
        }

        [Test]
        public void DomCopBot_1()
        {
            System.Collections.Generic.Dictionary<string, string> Header;
            Header = new Dictionary<string, string>();
            Header.Add(@"User-Agent", @"DomCopBot (https://www.domcop.com/bot)");
            var RS2 = Detective.ProcessData(Header);

            foreach (var item in RS2.Trace)
            {
                Console.WriteLine(item);
            }

            ClassicAssert.AreEqual("DomCopBot", RS2.BrowserName, "browser");
            ClassicAssert.AreEqual(new Version("0.0"), RS2.version, "version");
            ClassicAssert.AreEqual(true, RS2.Crawler, "Crawler");
            ClassicAssert.AreEqual(false, RS2.isMobileDevice, "isMobileDevice");
            ClassicAssert.AreEqual(string.Empty, RS2.OS, "os"); //https://en.wikipedia.org/wiki/Symbian
            ClassicAssert.AreEqual(false, RS2.IsRandomRobobotUserAgent, "IsRandomRobobotUserAgent");
        }

        [Test]
        public void TweetmemeBot_1()
        {
            System.Collections.Generic.Dictionary<string, string> Header;
            Header = new Dictionary<string, string>();
            Header.Add(@"User-Agent", @"Mozilla/5.0 (TweetmemeBot/4.0; +http://datasift.com/bot.html) Gecko/20100101 Firefox/31.0");
            var RS2 = Detective.ProcessData(Header);

            foreach (var item in RS2.Trace)
            {
                Console.WriteLine(item);
            }

            ClassicAssert.AreEqual("TweetmemeBot", RS2.BrowserName, "browser");
            ClassicAssert.AreEqual(new Version("4.0"), RS2.version, "version");
            ClassicAssert.AreEqual(true, RS2.Crawler, "Crawler");
            ClassicAssert.AreEqual(false, RS2.isMobileDevice, "isMobileDevice");
            ClassicAssert.AreEqual(string.Empty, RS2.OS, "os"); //https://en.wikipedia.org/wiki/Symbian
            ClassicAssert.AreEqual(false, RS2.IsRandomRobobotUserAgent, "IsRandomRobobotUserAgent");
        }

        [Test]
        public void Tailrank_1()
        {
            System.Collections.Generic.Dictionary<string, string> Header;
            Header = new Dictionary<string, string>();
            Header.Add(@"User-Agent", @"Mozilla/5.0 (X11; U; Linux i686; en-US; rv:1.2.1; aggregator:Tailrank; http://tailrank.com/robot) Gecko/20021130");
            var RS2 = Detective.ProcessData(Header);

            foreach (var item in RS2.Trace)
            {
                Console.WriteLine(item);
            }

            ClassicAssert.AreEqual("Tailrank", RS2.BrowserName, "browser");
            ClassicAssert.AreEqual(new Version("1.2.1"), RS2.version, "version");
            ClassicAssert.AreEqual(true, RS2.Crawler, "Crawler");
            ClassicAssert.AreEqual(false, RS2.isMobileDevice, "isMobileDevice");
            ClassicAssert.AreEqual(string.Empty, RS2.OS, "os"); //https://en.wikipedia.org/wiki/Symbian
            ClassicAssert.AreEqual(false, RS2.IsRandomRobobotUserAgent, "IsRandomRobobotUserAgent");
        }

        [Test]
        public void LightspeedSystemsCrawler_1()
        {
            System.Collections.Generic.Dictionary<string, string> Header;
            Header = new Dictionary<string, string>();
            Header.Add(@"User-Agent", @"LightspeedSystemsCrawler Mozilla/5.0 (Windows; U; MSIE 9.0; Windows NT 9.0; en-US)");
            var RS2 = Detective.ProcessData(Header);

            foreach (var item in RS2.Trace)
            {
                Console.WriteLine(item);
            }

            ClassicAssert.AreEqual("LightspeedSystemsCrawler", RS2.BrowserName, "browser");
            ClassicAssert.AreEqual(new Version("0.0"), RS2.version, "version");
            ClassicAssert.AreEqual(true, RS2.Crawler, "Crawler");
            ClassicAssert.AreEqual(false, RS2.isMobileDevice, "isMobileDevice");
            ClassicAssert.AreEqual(string.Empty, RS2.OS, "os"); //https://en.wikipedia.org/wiki/Symbian
            ClassicAssert.AreEqual(false, RS2.IsRandomRobobotUserAgent, "IsRandomRobobotUserAgent");
        }

        [Test]
        public void lkxscan_1()
        {
            System.Collections.Generic.Dictionary<string, string> Header;
            Header = new Dictionary<string, string>();
            Header.Add(@"User-Agent", @"lkxscan/v0.1.0 (+https://leakix.net) l9explore/v1.0.0 (+https://github.com/LeakIX/l9explore)");
            var RS2 = Detective.ProcessData(Header);

            foreach (var item in RS2.Trace)
            {
                Console.WriteLine(item);
            }

            ClassicAssert.AreEqual("lkxscan", RS2.BrowserName, "browser");
            ClassicAssert.AreEqual(new Version("0.1.0"), RS2.version, "version");
            ClassicAssert.AreEqual(true, RS2.Crawler, "Crawler");
            ClassicAssert.AreEqual(false, RS2.isMobileDevice, "isMobileDevice");
            ClassicAssert.AreEqual("Unknown", RS2.OS, "os"); //https://en.wikipedia.org/wiki/Symbian
            ClassicAssert.AreEqual(false, RS2.IsRandomRobobotUserAgent, "IsRandomRobobotUserAgent");
        }

        [Test]
        public void RandomRobobotUserAgent_1()
        {
            System.Collections.Generic.Dictionary<string, string> Header;
            Header = new Dictionary<string, string>();
            Header.Add(@"User-Agent", @"gt gfnqw xjlNwibbgvkuusdrean");
            var RS2 = Detective.ProcessData(Header);

            foreach (var item in RS2.Trace)
            {
                Console.WriteLine(item);
            }

            ClassicAssert.AreEqual("Generic Browser", RS2.BrowserName, "browser");
            ClassicAssert.AreEqual(new Version("0.0"), RS2.version, "version");
            ClassicAssert.AreEqual(false, RS2.Crawler, "Crawler");
            ClassicAssert.AreEqual(false, RS2.isMobileDevice, "isMobileDevice");
            ClassicAssert.AreEqual("Unknown", RS2.OS, "os"); //https://en.wikipedia.org/wiki/Symbian
            ClassicAssert.AreEqual(true, RS2.IsRandomRobobotUserAgent, "IsRandomRobobotUserAgent");
        }
        [Test]
        public void RandomRobobotUserAgent_2()
        {
            System.Collections.Generic.Dictionary<string, string> Header;
            Header = new Dictionary<string, string>();
            Header.Add(@"User-Agent", @"jkxkakfFojxetFdxtvkjubietguc");
            var RS2 = Detective.ProcessData(Header);

            foreach (var item in RS2.Trace)
            {
                Console.WriteLine(item);
            }

            ClassicAssert.AreEqual("Generic Browser", RS2.BrowserName, "browser");
            ClassicAssert.AreEqual(new Version("0.0"), RS2.version, "version");
            ClassicAssert.AreEqual(false, RS2.Crawler, "Crawler");
            ClassicAssert.AreEqual(false, RS2.isMobileDevice, "isMobileDevice");
            ClassicAssert.AreEqual("Unknown", RS2.OS, "os"); //https://en.wikipedia.org/wiki/Symbian
            ClassicAssert.AreEqual(true, RS2.IsRandomRobobotUserAgent, "IsRandomRobobotUserAgent");
        }

        //https://github.com/codewatchorg/Burp-UserAgent/blob/master/useragents.xml


        [Test]
        public void ia_archiver_1()
        {
            Dictionary<string, string> Header = new Dictionary<string, string>();
            Header.Add("Connection", "close");
            Header.Add("From", "crawler@alexa.com");
            Header.Add("Host", "owenbrady.net");
            Header.Add("User-Agent", "ia_archiver (+http://www.alexa.com/site/help/webmasters; crawler@alexa.com)");

            var RS2 = Detective.ProcessData(Header);
            ClassicAssert.AreEqual("ia_archiver", RS2.BrowserName, "browser");
            ClassicAssert.AreEqual(new Version("0.0"), RS2.version, "browser");
            ClassicAssert.AreEqual("", RS2.OS, "OS");
            ClassicAssert.AreEqual(true, RS2.Crawler, "Crawler");
            ClassicAssert.AreEqual(false, RS2.isMobileDevice, "isMobileDevice");
            ClassicAssert.AreEqual("", RS2.mobileDeviceModel, "isMobileDevice");
            ClassicAssert.AreEqual("", RS2.mobileDeviceManufacturer, "mobileDeviceManufacturer");
            ClassicAssert.AreEqual("Unknown", RS2.Platform, "Platform");
            ClassicAssert.AreEqual("", RS2.layoutEngineVersion, "layoutEngineVersion");
            ClassicAssert.AreEqual("", RS2.layoutEngine, "layoutEngine");
        }
        [Test]
        public void ia_archiver_2()
        {
            Dictionary<string, string> Header = new Dictionary<string, string>();
            Header.Add("Connection", "close");
            Header.Add("From", "crawler@alexa.com");
            Header.Add("Host", "ocean.accesswa.net");
            Header.Add("User-Agent", "ia_archiver");

            var RS2 = Detective.ProcessData(Header);
            ClassicAssert.AreEqual("ia_archiver", RS2.BrowserName, "browser");
            ClassicAssert.AreEqual(new Version("0.0"), RS2.version, "browser");
            ClassicAssert.AreEqual("", RS2.OS, "OS");
            ClassicAssert.AreEqual(true, RS2.Crawler, "Crawler");
            ClassicAssert.AreEqual(false, RS2.isMobileDevice, "isMobileDevice");
            ClassicAssert.AreEqual("", RS2.mobileDeviceModel, "isMobileDevice");
            ClassicAssert.AreEqual("", RS2.mobileDeviceManufacturer, "mobileDeviceManufacturer");
            ClassicAssert.AreEqual("Unknown", RS2.Platform, "Platform");
            ClassicAssert.AreEqual("", RS2.layoutEngineVersion, "layoutEngineVersion");
            ClassicAssert.AreEqual("", RS2.layoutEngine, "layoutEngine");

        }
        [Test]
        public void ia_archiver_3()
        {
            Dictionary<string, string> Header = new Dictionary<string, string>();
            Header.Add("Connection", "TE, close");
            Header.Add("Host", "ocean.accesswa.net");
            Header.Add("TE", "deflate,gzip;q=0.3");
            Header.Add("User-Agent", "ia_archiver-web.archive.org");

            var RS2 = Detective.ProcessData(Header);
            ClassicAssert.AreEqual("ia_archiver", RS2.BrowserName, "browser");
            ClassicAssert.AreEqual(new Version("0.0"), RS2.version, "browser");
            ClassicAssert.AreEqual("", RS2.OS, "OS");
            ClassicAssert.AreEqual(true, RS2.Crawler, "Crawler");
            ClassicAssert.AreEqual(false, RS2.isMobileDevice, "isMobileDevice");
            ClassicAssert.AreEqual("", RS2.mobileDeviceModel, "isMobileDevice");
            ClassicAssert.AreEqual("", RS2.mobileDeviceManufacturer, "mobileDeviceManufacturer");
            ClassicAssert.AreEqual("Unknown", RS2.Platform, "Platform");
            ClassicAssert.AreEqual("", RS2.layoutEngineVersion, "layoutEngineVersion");
            ClassicAssert.AreEqual("", RS2.layoutEngine, "layoutEngine");
        }
        [Test]
        public void ia_archiver_4()
        {
            Dictionary<string, string> Header = new Dictionary<string, string>();
            Header.Add("Connection", "close");
            Header.Add("From", "crawler@alexa.com");
            Header.Add("Host", "ocean.accesswa.net");
            Header.Add("User-Agent", "ia_archiver");

            var RS2 = Detective.ProcessData(Header);
            ClassicAssert.AreEqual("ia_archiver", RS2.BrowserName, "browser");
            ClassicAssert.AreEqual(new Version("0.0"), RS2.version, "browser");
            ClassicAssert.AreEqual("", RS2.OS, "OS");
            ClassicAssert.AreEqual(true, RS2.Crawler, "Crawler");
            ClassicAssert.AreEqual(false, RS2.isMobileDevice, "isMobileDevice");
            ClassicAssert.AreEqual("", RS2.mobileDeviceModel, "isMobileDevice");
            ClassicAssert.AreEqual("", RS2.mobileDeviceManufacturer, "mobileDeviceManufacturer");
            ClassicAssert.AreEqual("Unknown", RS2.Platform, "Platform");
            ClassicAssert.AreEqual("", RS2.layoutEngineVersion, "layoutEngineVersion");
            ClassicAssert.AreEqual("", RS2.layoutEngine, "layoutEngine");
        }

        [Test]
        public void Apache_HttpClient_1()
        {
            Dictionary<string, string> Header = new Dictionary<string, string>();
            Header.Add("Connection", "Keep-Alive");
            Header.Add("Accept-Encoding", "gzip,deflate");
            Header.Add("Host", "owenbrady.net");
            Header.Add("User-Agent", "Apache-HttpClient/4.3 (java 1.5)");

            var RS2 = Detective.ProcessData(Header);
            ClassicAssert.AreEqual("Apache-HttpClient", RS2.BrowserName, "browser");
            ClassicAssert.AreEqual(new Version("4.3"), RS2.version, "browser");
            ClassicAssert.AreEqual("", RS2.OS, "OS");
            ClassicAssert.AreEqual(true, RS2.Crawler, "Crawler");
            ClassicAssert.AreEqual(false, RS2.isMobileDevice, "isMobileDevice");
            ClassicAssert.AreEqual("", RS2.mobileDeviceModel, "isMobileDevice");
            ClassicAssert.AreEqual("", RS2.mobileDeviceManufacturer, "mobileDeviceManufacturer");
            ClassicAssert.AreEqual("Unknown", RS2.Platform, "Platform");
            ClassicAssert.AreEqual("", RS2.layoutEngineVersion, "layoutEngineVersion");
            ClassicAssert.AreEqual("", RS2.layoutEngine, "layoutEngine");

        }

        [Test]
        public void Baiduspider_1()
        {
            Dictionary<string, string> Header = new Dictionary<string, string>();
            Header.Add("Content-Type", "text/html");
            Header.Add("Accept", "text/html, application/xhtml+xml, */*");
            Header.Add("Host", "owenbrady.net");
            Header.Add("User-Agent", "Mozilla/5.0 (compatible; Baiduspider/2.0; +http://www.baidu.com/search/spider.html");
            Header.Add("X-Forwarded-For", "123.88.167.215");
            Header.Add("Client-IP", "123.88.167.215");
            Header.Add("REMOTE_ADDR", "123.88.167.215");

            var RS2 = Detective.ProcessData(Header);
            ClassicAssert.AreEqual("Baiduspider", RS2.BrowserName, "browser");
            ClassicAssert.AreEqual(new Version("2.0"), RS2.version, "browser");
            ClassicAssert.AreEqual("", RS2.OS, "OS");
            ClassicAssert.AreEqual(true, RS2.Crawler, "Crawler");
            ClassicAssert.AreEqual(false, RS2.isMobileDevice, "isMobileDevice");
            ClassicAssert.AreEqual("", RS2.mobileDeviceModel, "isMobileDevice");
            ClassicAssert.AreEqual("", RS2.mobileDeviceManufacturer, "mobileDeviceManufacturer");
            ClassicAssert.AreEqual("Unknown", RS2.Platform, "Platform");
            ClassicAssert.AreEqual("", RS2.layoutEngineVersion, "layoutEngineVersion");
            ClassicAssert.AreEqual("", RS2.layoutEngine, "layoutEngine");

        }

        [Test]
        public void Baiduspider_2()
        {
            Dictionary<string, string> Header = new Dictionary<string, string>();
            Header.Add("Host", "owenbrady.net");
            Header.Add("User-Agent", "compatible;Baiduspider/2.0; +http://www.baidu.com/search/spider.html");
            Header.Add("X-Fowarded-For", "221.144.183.200");

            var RS2 = Detective.ProcessData(Header);
            ClassicAssert.AreEqual("Baiduspider", RS2.BrowserName, "browser");
            ClassicAssert.AreEqual(new Version("2.0"), RS2.version, "browser");
            ClassicAssert.AreEqual("", RS2.OS, "OS");
            ClassicAssert.AreEqual(true, RS2.Crawler, "Crawler");
            ClassicAssert.AreEqual(false, RS2.isMobileDevice, "isMobileDevice");
            ClassicAssert.AreEqual("", RS2.mobileDeviceModel, "isMobileDevice");
            ClassicAssert.AreEqual("", RS2.mobileDeviceManufacturer, "mobileDeviceManufacturer");
            ClassicAssert.AreEqual("Unknown", RS2.Platform, "Platform");
            ClassicAssert.AreEqual("", RS2.layoutEngineVersion, "layoutEngineVersion");
            ClassicAssert.AreEqual("", RS2.layoutEngine, "layoutEngine");

        }
        [Test]
        public void Baiduspider_3()
        {
            Dictionary<string, string> Header = new Dictionary<string, string>();
            Header.Add("Connection", "Close");
            Header.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
            Header.Add("Accept-Charset", "GB2312,utf-8;q=0.7,*;q=0.7");
            Header.Add("Accept-Encoding", "gzip, deflate");
            Header.Add("Accept-Language", "zh-cn,zh;q=0.5");
            Header.Add("Host", "owenbrady.net");
            Header.Add("User-Agent", "Mozilla/5.0 (compatible; Baiduspider/2.0; +http://www.baidu.com/search/spider.html)");

            var RS2 = Detective.ProcessData(Header);
            ClassicAssert.AreEqual("Baiduspider", RS2.BrowserName, "browser");
            ClassicAssert.AreEqual(new Version("2.0"), RS2.version, "browser");
            ClassicAssert.AreEqual("", RS2.OS, "OS");
            ClassicAssert.AreEqual(true, RS2.Crawler, "Crawler");
            ClassicAssert.AreEqual(false, RS2.isMobileDevice, "isMobileDevice");
            ClassicAssert.AreEqual("", RS2.mobileDeviceModel, "isMobileDevice");
            ClassicAssert.AreEqual("", RS2.mobileDeviceManufacturer, "mobileDeviceManufacturer");
            ClassicAssert.AreEqual("Unknown", RS2.Platform, "Platform");
            ClassicAssert.AreEqual("", RS2.layoutEngineVersion, "layoutEngineVersion");
            ClassicAssert.AreEqual("", RS2.layoutEngine, "layoutEngine");
        }
        [Test]
        public void Baiduspider_4()
        {
            Dictionary<string, string> Header = new Dictionary<string, string>();
            Header.Add("Connection", "Close");
            Header.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
            Header.Add("Accept-Charset", "GB2312,utf-8;q=0.7,*;q=0.7");
            Header.Add("Accept-Encoding", "gzip, deflate");
            Header.Add("Accept-Language", "zh-cn,zh;q=0.5");
            Header.Add("Host", "owenbrady.net");
            Header.Add("User-Agent", "Mozilla/5.0 (compatible; Baiduspider/2.0; +http://www.baidu.com/search/spider.html)");

            var RS2 = Detective.ProcessData(Header);
            ClassicAssert.AreEqual("Baiduspider", RS2.BrowserName, "browser");
            ClassicAssert.AreEqual(new Version("2.0"), RS2.version, "browser");
            ClassicAssert.AreEqual("", RS2.OS, "OS");
            ClassicAssert.AreEqual(true, RS2.Crawler, "Crawler");
            ClassicAssert.AreEqual(false, RS2.isMobileDevice, "isMobileDevice");
            ClassicAssert.AreEqual("", RS2.mobileDeviceModel, "isMobileDevice");
            ClassicAssert.AreEqual("", RS2.mobileDeviceManufacturer, "mobileDeviceManufacturer");
            ClassicAssert.AreEqual("Unknown", RS2.Platform, "Platform");
            ClassicAssert.AreEqual("", RS2.layoutEngineVersion, "layoutEngineVersion");
            ClassicAssert.AreEqual("", RS2.layoutEngine, "layoutEngine");

        }

        [Test]
        public void Baiduspider_5()
        {
            Dictionary<string, string> Header = new Dictionary<string, string>();
            Header.Add("Content-Length", "0");
            Header.Add("Accept", "*/*");
            Header.Add("Accept-Encoding", "gzip");
            Header.Add("Accept-Language", "zh-cn,zh-tw");
            Header.Add("Host", "owenbrady.net");
            Header.Add("User-Agent", "Mozilla/5.0 (Linux;u;Android 2.3.7;zh-cn;) AppleWebKit/533.1 (KHTML,like Gecko) Version/4.0 Mobile Safari/533.1 (compatible; +http://www.baidu.com/search/spider.html)");
            Header.Add("x-up-bear-type", "other");

            var RS2 = Detective.ProcessData(Header);
            ClassicAssert.AreEqual("Baiduspider", RS2.BrowserName, "browser");
            ClassicAssert.AreEqual(new Version("0.0"), RS2.version, "browser");
            ClassicAssert.AreEqual("", RS2.OS, "OS");
            ClassicAssert.AreEqual(true, RS2.Crawler, "Crawler");
            ClassicAssert.AreEqual(false, RS2.isMobileDevice, "isMobileDevice");
            ClassicAssert.AreEqual("", RS2.mobileDeviceModel, "isMobileDevice");
            ClassicAssert.AreEqual("", RS2.mobileDeviceManufacturer, "mobileDeviceManufacturer");
            ClassicAssert.AreEqual("Unknown", RS2.Platform, "Platform");
            ClassicAssert.AreEqual("", RS2.layoutEngineVersion, "layoutEngineVersion");
            ClassicAssert.AreEqual("", RS2.layoutEngine, "layoutEngine");


        }

        [Test]
        public void Baiduspider_6()
        {
            Dictionary<string, string> Header = new Dictionary<string, string>();
            Header.Add("Content-Length", "0");
            Header.Add("Accept", "*/*");
            Header.Add("Accept-Encoding", "gzip");
            Header.Add("Accept-Language", "zh-cn,zh-tw");
            Header.Add("Host", "owenbrady.net");
            Header.Add("User-Agent", "Mozilla/5.0 (Linux;u;Android 2.3.7;zh-cn;) AppleWebKit/533.1 (KHTML,like Gecko) Version/4.0 Mobile Safari/533.1 (compatible; +http://www.baidu.com/search/spider.html)");
            Header.Add("x-up-bear-type", "other");

            var RS2 = Detective.ProcessData(Header);
            ClassicAssert.AreEqual("Baiduspider", RS2.BrowserName, "browser");
            ClassicAssert.AreEqual(new Version("0.0"), RS2.version, "browser");
            ClassicAssert.AreEqual("", RS2.OS, "OS");
            ClassicAssert.AreEqual(true, RS2.Crawler, "Crawler");
            ClassicAssert.AreEqual(false, RS2.isMobileDevice, "isMobileDevice");
            ClassicAssert.AreEqual("", RS2.mobileDeviceModel, "isMobileDevice");
            ClassicAssert.AreEqual("", RS2.mobileDeviceManufacturer, "mobileDeviceManufacturer");
            ClassicAssert.AreEqual("Unknown", RS2.Platform, "Platform");
            ClassicAssert.AreEqual("", RS2.layoutEngineVersion, "layoutEngineVersion");
            ClassicAssert.AreEqual("", RS2.layoutEngine, "layoutEngine");
        }

    }
}