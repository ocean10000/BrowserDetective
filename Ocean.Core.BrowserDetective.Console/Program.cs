// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.Logging;
using Ocean.Core.BrowserDetective.Extentions;
using Ocean.Core.BrowserDetective.Data.Extentions;
using Ocean.Core.BrowserDetective.Data.Models;
using System.Configuration;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

using ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddFilter("Ocean.Core.BrowserDetective", LogLevel.Error).AddConsole());
ILogger logger = factory.CreateLogger(typeof(Ocean.Core.BrowserDetective.Process));


Ocean.Core.BrowserDetective.Data.Context.HeaderContext context = new Ocean.Core.BrowserDetective.Data.Context.HeaderContext();
Ocean.Core.BrowserDetective.Data.Context.ResultContext resultContext = new Ocean.Core.BrowserDetective.Data.Context.ResultContext();

var detective = new Ocean.Core.BrowserDetective.Process(logger);

System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();


if (detective.DefaultBrowser != null)
{
    System.Collections.Generic.List<string> AlreadyProcessedHeaders = new List<string>();
    var HeaderKeys = detective.Headers;

    foreach (var item in context.Raw.OrderByDescending(X => X.ID))
    {
        System.DateTime start = DateTime.Now;

        //------------------------------------------------------
        //Only allow headers which are in the Headers we check.
        //This is to reduce the amount of checks we have to do.
        //------------------------------------------------------
        var headers = context.Headers.Where(X => X.Raw_ID == item.ID && HeaderKeys.Contains(X.Name)).ToList();
        IDictionary<string, string> dic = new Dictionary<string, string>();
        string HeaderKey = string.Empty;
        foreach (var key in headers)
        {
            dic.Add(key.Name, key.Value);
            HeaderKey += $"{key.Name}:{key.Value}\n";
        }

        //------------------------------------------------------
        //Get a MD5 has of the HeaderKey Data, I don't care that 
        //the Hash is considered Weak. ITS FAST and Takes little 
        //Ram and CPU to do, and little space.
        //------------------------------------------------------
        HeaderKey = Convert.ToHexString(md5.ComputeHash(System.Text.Encoding.ASCII.GetBytes(HeaderKey))); // .NET 5 +

        //Only process the data if it doesn't already exist.
        if (AlreadyProcessedHeaders.Contains(HeaderKey) == false)
        {
            //Add it to the existing list
            AlreadyProcessedHeaders.Add(HeaderKey);
            if (dic.Count > 0)
            {
                var h = detective.DefaultBrowser.Process(dic);

                foreach (var n in h.Trace)
                {
                    var d = new BrowserNode() { Raw_ID = item.ID, Node_ID = n.BrowserID, Name = n.Name, Value = n.Value };
                    resultContext.Nodes.Add(d);
                }
                foreach (var key in h.Keys)
                {
                    var r = new ResultItem() { Raw_ID = item.ID, Name = key, Value = h[key] };
                    resultContext.Results.Add(r);
                }
            }
            try
            {
                resultContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            Console.WriteLine($"{item.ID}-Seconds {DateTime.Now.Subtract(start).TotalSeconds}");
        }
        else
        {
            Console.WriteLine($"{item.ID}-Skipped-Seconds {DateTime.Now.Subtract(start).TotalSeconds}");
        }
    }
}