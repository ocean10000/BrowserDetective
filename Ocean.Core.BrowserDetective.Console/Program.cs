// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.Logging;
using Ocean.Core.BrowserDetective.Data.Models;
using Ocean.Core.BrowserDetective.Extentions;
using System.Configuration;

using ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddFilter("Ocean.Core.BrowserDetective", LogLevel.Error).AddConsole());
ILogger logger = factory.CreateLogger(typeof(Ocean.Core.BrowserDetective.Process));

Ocean.Core.BrowserDetective.Data.Context.HeaderContext context;
Ocean.Core.BrowserDetective.Data.Context.ResultContext resultContext;

context = new Ocean.Core.BrowserDetective.Data.Context.HeaderContext();
resultContext = new Ocean.Core.BrowserDetective.Data.Context.ResultContext();
var CoreResultFile = ConfigurationManager.ConnectionStrings["Results"].ConnectionString;
CoreResultFile = CoreResultFile.Replace("Data Source=","");
if (System.IO.File.Exists(CoreResultFile))
{
    System.IO.File.Delete(CoreResultFile);
}
System.IO.File.Copy("Core.Results.db", CoreResultFile);

var detective = new Ocean.Core.BrowserDetective.Process(logger);

System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();


if (detective.DefaultBrowser != null)
{
    System.Collections.Generic.List<string> AlreadyProcessedHeaders = new List<string>();
    //This gets the actual Headers Checked by the Browser Detective.
    var HeaderKeys = detective.Headers;

    //------------------------------------------------------
    //Changed it to be Sorted from Smallest to Largest 
    //So that the Resulting output files weill be more 
    //Stable between releases, even after the header DB
    //is updated with more browser files. The Oldest will
    //not change, but new ones will be added at the end.
    //------------------------------------------------------
    //This was changed originally so I can see the newer changes
    //faster to help debug the code easier.  It also took a great
    //deal longer in the past, it doesn't require as long as a
    //waite now, that I no longer use Google Drive to read a SQLite
    //DB from. The speed delay was very very noticible for this case.
    //------------------------------------------------------
    foreach (var item in context.Raw.OrderBy(X => X.ID))
    {
        System.DateTime start = DateTime.Now;

        //------------------------------------------------------
        //Only allow headers which are in the Headers we check.
        //This is to reduce the amount of checks we have to do.
        //------------------------------------------------------
        var headers = context.Headers.Where(X => X.Raw_ID == item.ID).ToList();
        IDictionary<string, string> dic = headers.ToDictionary(X => X.Name, X => X.Value);
        string HeaderKey = string.Empty;
        foreach (var key in headers.Where(X => HeaderKeys.Contains(X.Name)))
        {
            HeaderKey += $"{key.Name}:{key.Value}\n";
        }

        //------------------------------------------------------
        //Making sure we have some headers to work with.
        //------------------------------------------------------
        if (dic.Count > 0)
        {
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
                    //------------------------------------------------------
                    //This is to save the actuall Processing time of the 
                    //BrowserCap, without taking into account the DB save 
                    //operations.
                    //------------------------------------------------------
                    System.DateTime Processstart = DateTime.Now;
                    var h = detective.ProcessData(dic);
                    System.DateTime ProcessEnd = DateTime.Now;

                    //------------------------------------------------------
                    //Want to Record the Header Checksum. Since I use it to 
                    //Reduce duplicate work.
                    //------------------------------------------------------
                    h["Header Checksum"] = HeaderKey;
                    h["Process Time"] = $"{ProcessEnd.Subtract(Processstart).TotalSeconds}";

                    //foreach (var n in h.Trace)
                    //{
                    //    var d = new BrowserNode() { Raw_ID = item.ID, Node_ID = n.BrowserID, Name = n.Name, Value = n.Value };
                    //    resultContext.Nodes.Add(d);
                    //}
                    BrowserResult flatResult = new BrowserResult() { Raw_ID = item.ID, Stamp = item.Stamp };
                    if (dic.ContainsKey("User-Agent"))
                    {
                        flatResult.UserAgent = dic["User-Agent"];
                    }
                    foreach (var key in h.Keys)
                    {
                        var r = new ResultItem() { Raw_ID = item.ID, Name = key, Value = h[key] };
                        resultContext.Results.Add(r);
                        switch (key)
                        {
                            case "browser":
                                flatResult.BrowserName = h[key];
                                break;
                            case "version":
                                flatResult.version = h[key];
                                break;
                            case "OS":
                                flatResult.OS = h[key];
                                break;
                            case "isMobileDevice":
                                flatResult.isMobileDevice = h[key];
                                break;
                            case "crawler":
                                flatResult.Crawler = h[key];
                                break;
                            case "mobileDeviceModel":
                                flatResult.mobileDeviceModel = h[key];
                                break;
                            case "mobileDeviceManufacturer":
                                flatResult.mobileDeviceManufacturer = h[key];
                                break;
                            case "appleWebTechnologyVersion":
                                flatResult.appleWebTechnologyVersion = h[key];
                                break;
                            case "platform":
                                flatResult.platform = h[key];
                                break;
                            case "Chromeversion":
                                flatResult.Chromeversion = h[key];
                                break;
                            case "layoutEngine":
                                flatResult.layoutEngine = h[key];
                                break;
                            case "layoutEngineVersion":
                                flatResult.layoutEngineVersion = h[key];
                                break;
                            case "Header Checksum":
                                flatResult.MD5 = h[key];
                                break;
                            case "Process Time":
                                flatResult.TimeSpent = h[key];
                                break;
                        }
                    }

                    resultContext.Result.Add(flatResult);

                }
                try
                {
                    resultContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    resultContext.SaveChanges();
                }
                //------------------------------------------------------
                //try to write the least amount of stuff to the Console.
                //So not to waste much time displaying extra stuff, that
                //will be mostly ignored. Its why we write as much as we
                //can to the db.
                //------------------------------------------------------
                Console.WriteLine($"{item.ID}-Seconds {DateTime.Now.Subtract(start).TotalSeconds}");
            }
            else
            {
                Console.WriteLine($"{item.ID}-Skipped-Seconds {DateTime.Now.Subtract(start).TotalSeconds}");
            }
        }
    }
}

