using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Ocean.Core.BrowserDetective.Data.Context;
using Ocean.Core.BrowserDetective.Data.Models;
using Ocean.Core.BrowserDetective.Extentions;
using System.Collections.Generic;


namespace Ocean.Core.BrowserDetective
{
    public class Process
    {
        ILogger _logger;
        /// <summary>
        /// This is the top most Default Browser Definition file.
        /// </summary>
        public Browser DefaultBrowser = null;

        /// <summary>
        /// List of Headers used by this BrowseCap files.
        /// </summary>
        public List<String> Headers = new List<String>();

        public Process(ILogger logger)
        {
            //-----------------------------------------------------------------------------
            //I am putting some logging details in this. But will probably end up 
            //redoing this whole project to make it more modern.
            //-----------------------------------------------------------------------------
            _logger = logger;

            //-----------------------------------------------------------------------------
            //SQL Lite DB Access
            //-----------------------------------------------------------------------------
            BrowserCapsContext Context = new BrowserCapsContext();

            //-----------------------------------------------------------------------------
            //The Origianl Browser definitions were stored in Xml files. But I am a DB Guy
            //and I prefer a database interface to access data, so I dumped the Definition
            //files which I hand coded into a SQL Lite Database format, to make it easier 
            //for me to work with the data. It also pushes this away from the Microsoft
            //controled format of the past.
            //-----------------------------------------------------------------------------
            //Ok We are going to download all the browser Objects stored in the Database.
            //no changes are going to be made this is just to dump to useable ojects.
            //-----------------------------------------------------------------------------
            //Sorting by Parent ID, by Name, then By Type. to make searching a bit simplier.
            //-----------------------------------------------------------------------------
            //AsNoTracking - Is to stop Entity Framework from trying to track these objects
            //for changes. We are not going to be making any changes from this readonly code
            //-----------------------------------------------------------------------------
            var browsers = Context.Browsers.AsNoTracking().OrderBy(X => X.ParentId).ThenBy(X => X.Name).ThenBy(X => X.Type).ToList();

            //-----------------------------------------------------------------------------
            //Setting the Logger for each browser detection.
            //-----------------------------------------------------------------------------
            browsers.ForEach(X => X._logger = _logger);

            logger.Log(LogLevel.Trace, $"Browser Count {browsers.Count()}");

            var identifications = Context.Identifications.AsNoTracking().ToList();
            logger.Log(LogLevel.Trace, $"identifications Count {identifications.Count()}");

            var captures = Context.Captures.AsNoTracking().ToList();
            logger.Log(LogLevel.Trace, $"captures Count {captures.Count()}");

            var capabilities = Context.Capabilities.AsNoTracking().ToList();
            logger.Log(LogLevel.Trace, $"capabilities Count {capabilities.Count()}");

            var sampleHeaders = Context.SampleHeaders.AsNoTracking().ToList();
            logger.Log(LogLevel.Trace, $"SampleHeaders Count {sampleHeaders.Count()}");

            //-----------------------------------------------------------------------------
            //Gets a List of Headers Used by the BrowseCap files.
            //-----------------------------------------------------------------------------
            var list = identifications.Where(X => X.Type == CaptureType.Header).Select(X=>X.Name).Distinct().ToList();
            list.AddRange(captures.Where(X => X.Type == CaptureType.Header).Select(X => X.Name).Distinct().ToList());
            Headers = list.Distinct().ToList();

            //-----------------------------------------------------------------------------
            //Loops though all the Browsers and builds the Browser Tree.
            //-----------------------------------------------------------------------------
            foreach (var b in browsers)
            {
                b.Captures = captures.Where(c => c.BrowserId == b.Id).ToList();
                b.Identifications = identifications.Where(c => c.BrowserId == b.Id).ToList();
                b.Capabilities = capabilities.Where(c => c.BrowserId == b.Id).ToList();
                b.Samples = sampleHeaders.Where(c => c.BrowserId == b.Id).ToList();
                b.InverseParent = browsers.Where(c => c.ParentId == b.Id).ToList();
                if (b.ParentId != null && b.ParentId > 0)
                {
                    b.Parent = browsers.FirstOrDefault(x => x.Id == b.ParentId);
                    b.parentID = b.Parent.Name;
                }
            }

            //-----------------------------------------------------------------------------'
            //Not sure what function this is doiner right now. since it is a local variable only.
            //-----------------------------------------------------------------------------
            //var test = identifications.Where(x => x.Type == CaptureType.Header).Select(x => x.Name).Distinct().ToList();
            //test.AddRange(captures.Where(x => x.Type == CaptureType.Header).Select(x => x.Name).Distinct().ToList());

            //-----------------------------------------------------------------------------
            //Sets the Top Most Browser Object. (This is based off the original formate of the
            //Microsoft Browser Files from Dot.net 1.X to 4.7X.)
            //-----------------------------------------------------------------------------
            DefaultBrowser = browsers.Where(x => x.Name == "Default").FirstOrDefault();
        }
        public Result ProcessData(string userAgent)
        {
            if (DefaultBrowser == null)
                return new Result();

            Microsoft.AspNetCore.Http.IHeaderDictionary header = new Microsoft.AspNetCore.Http.HeaderDictionary();
            header.Add("User-Agent", userAgent);

            return ProcessData(header);
        }
        public Result ProcessData(IDictionary<string, string> header)
        {
            if (DefaultBrowser == null)
                return new Result();

            Microsoft.AspNetCore.Http.IHeaderDictionary header2 = new Microsoft.AspNetCore.Http.HeaderDictionary();
            foreach (var k in header.Keys)
            {
                header2.Add(k, header[k]);
            }

            return DefaultBrowser.Process(header2);
        }
        //This is more of a way to allow conversion from older version of unit test and code pre
        //dot.net core.
        public Result ProcessData(System.Collections.Specialized.NameValueCollection header)
        {
            if (DefaultBrowser == null)
                return new Result();

            Microsoft.AspNetCore.Http.IHeaderDictionary header2 = new Microsoft.AspNetCore.Http.HeaderDictionary();
            foreach (var k in header.AllKeys)
            {
                header2.Add(k, header[k]);
            }

            return DefaultBrowser.Process(header2);
        }

        public Result ProcessData(Microsoft.AspNetCore.Http.IHeaderDictionary header)
        {
            if (DefaultBrowser == null)
                return new Result();

            return DefaultBrowser.Process(header);
        }
    }
}
