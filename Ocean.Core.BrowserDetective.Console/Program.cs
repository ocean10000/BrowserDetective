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

if (detective.DefaultBrowser != null)
{
    List<KeyValuePair<Browser, int>> nodes = detective.DefaultBrowser.Debug(0);
    foreach (var n in nodes)
    {
        Console.WriteLine($"{n.Key.Type}: {n.Key.Name}\t{n.Value}");
    }

    foreach (var item in context.Raw.OrderByDescending(X => X.ID))
    {
        System.DateTime start = DateTime.Now;

        var headers = context.Headers.Where(X => X.Raw_ID == item.ID).ToList();
        IDictionary<string, string> dic = new Dictionary<string, string>();
        foreach (var key in headers)
        {
            dic.Add(key.Name, key.Value);
        }
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
}