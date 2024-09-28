// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.Logging;
using Ocean.Core.BrowserDetective.Console.Models;

using ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole());
ILogger logger = factory.CreateLogger(typeof(Ocean.Core.BrowserDetective.Process));

Ocean.Core.BrowserDetective.Console.DataContext.HeaderContext context = new Ocean.Core.BrowserDetective.Console.DataContext.HeaderContext();
Ocean.Core.BrowserDetective.Console.DataContext.ResultContext resultContext = new Ocean.Core.BrowserDetective.Console.DataContext.ResultContext();
var detective = new Ocean.Core.BrowserDetective.Process(logger);

Console.WriteLine(detective.DefaultBrowser.Debug(0));

foreach (var item in context.Raw)
{
    var headers = context.Headers.Where(X => X.Raw_ID == item.ID).ToList();
    IDictionary<string, string> dic = new Dictionary<string, string>();
    Console.WriteLine("---------------------------Start Headers-------------------------------");
    foreach (var key in headers)
    {
        Console.WriteLine($"{key.Name}\t{key.Value}");
        dic.Add(key.Name, key.Value);
    }
    Console.WriteLine("----------------------------End Headers--------------------------------");
    Console.WriteLine("---------------------------Start Results-------------------------------");
    if (dic.Count > 0)
    {
        var h = detective.DefaultBrowser.Process(dic);

        var nodes = h.Debug(0);
        foreach (var n in nodes)
        {
            var d = new BrowserNode() { Raw_ID = item.ID, Node_ID = n.Key.Id, Index= n.Value };
            resultContext.Nodes.Add(d);
        }
        foreach (var key in h.results.Keys)
        {
            var r = new Ocean.Core.BrowserDetective.Console.Models.ResultItem() { Raw_ID= item.ID, Name = key, Value = h.results[key] };
            resultContext.Results.Add(r);
            Console.WriteLine($"{key}\t{h.results[key]}");
        }
    }
    resultContext.SaveChanges();
    Console.WriteLine("----------------------------End Results--------------------------------");

}