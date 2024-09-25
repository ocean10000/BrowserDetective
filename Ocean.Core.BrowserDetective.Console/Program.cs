// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.Logging;

using ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole());
ILogger logger = factory.CreateLogger(typeof(Ocean.Core.BrowserDetective.Process));

Ocean.Core.BrowserDetective.DataContext.HeaderContext context = new Ocean.Core.BrowserDetective.DataContext.HeaderContext();
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
        //var result = detective.ProcessData(dic);
        var h = detective.DefaultBrowser.Process(dic);

        Console.WriteLine(h.Debug(0));
        foreach (var key in h.results.Keys)
        {
            Console.WriteLine($"{key}\t{h.results[key]}");
        }
    }
    Console.WriteLine("----------------------------End Results--------------------------------");

}