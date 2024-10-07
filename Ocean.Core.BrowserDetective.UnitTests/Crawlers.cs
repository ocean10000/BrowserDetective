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
            var RS2 = Detective.DefaultBrowser.Process(Header);

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
            var RS2 = Detective.DefaultBrowser.Process(Header);

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

            ClassicAssert.AreEqual(string.Empty, RS2.BrowserName, "browser");
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

            ClassicAssert.AreEqual(string.Empty, RS2.BrowserName, "browser");
            ClassicAssert.AreEqual(new Version("0.0"), RS2.version, "version");
            ClassicAssert.AreEqual(false, RS2.Crawler, "Crawler");
            ClassicAssert.AreEqual(false, RS2.isMobileDevice, "isMobileDevice");
            ClassicAssert.AreEqual("Unknown", RS2.OS, "os"); //https://en.wikipedia.org/wiki/Symbian
            ClassicAssert.AreEqual(true, RS2.IsRandomRobobotUserAgent, "IsRandomRobobotUserAgent");
        }

        //https://github.com/codewatchorg/Burp-UserAgent/blob/master/useragents.xml
    }
}