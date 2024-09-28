namespace Ocean.Core.BrowserDetective.Extentions;

public static class BrowserExtention
{
    public static ResultBrowserItem Process(this Data.Models.Browser browser, IDictionary<string, string> header)
    {
        return Process(browser, header, new Result());
    }
    public static ResultBrowserItem Process(this Data.Models.Browser browser, IDictionary<string, string> header, Result result, int level = 0)
    {
        Console.WriteLine($"{level}");
        for (int i = 1; i <= level; ++i)
        {
            Console.WriteLine("\t");
        }
        Console.WriteLine($"{browser.Name}");
        if (!result.ContainsKey(string.Empty))
        {
            result.Add(string.Empty, header["User-Agent"]);
        }
        ResultBrowserItem Historyitem = new ResultBrowserItem(browser);
        Historyitem.results = result;

        foreach (var item in browser.Identifications)
        {
            var id = new BrowserDetective.Identification(item);
            id.Match(header, ref result);
            for (int i = 1; i <= level; ++i)
            {
                Console.WriteLine("\t");
            }
            if (!String.IsNullOrWhiteSpace(item.Match))
            {
                Console.WriteLine($"Match:{item.Match}");
            }
            else
            {
                Console.WriteLine($"NonMatch:{item.NonMatch}");
            }
            // only continue precessing if they all pass.
            if (id.Success)
            {
                Historyitem.MatchList.AddLast(id);
                Historyitem.Success = true;
                Console.WriteLine("Pass");
            }
            else if (browser.Type == BrowserType.Browser)
            {
                //Marks the Item as failed match.
                Historyitem.NotMatchedList.AddLast(id);
                Historyitem.Success = false;
                //Console.WriteLine("Fail");
                return Historyitem;
            }
        }
        //These are optional, additional RegEx to help refine the lookups.
        foreach (var item in browser.Captures)
        {
            var id = new BrowserDetective.Identification(item);
            id.Match(header, ref result);
            //Only care about the sucessfull ones, ignore the other ones.
            if (id.Success)
            {
                Historyitem.MatchList.AddLast(id);
            }
        }
        foreach (var item in browser.Capabilities)
        {
            //make sure we have a non-Null/non-empty value
            if (string.IsNullOrEmpty(item.Value) == false)
            {
                if (item.Value.Contains("${"))
                {
                    string v = Historyitem.MatchList.First(x => x.Success == true).Result(item.Value);
                    //empty or null means no valid convertion option available.
                    if (String.IsNullOrWhiteSpace(v) == false)
                        Historyitem.results[item.Name] = v;
                    else if (result.ContainsKey(item.Name))
                        Historyitem.results[item.Name] = result[item.Name];

                    Console.WriteLine($"{item.Name}\t{v}");
                }
                else
                {
                    Historyitem.results[item.Name] = item.Value;
                }
            }
        }
        //The wiping out of the Results has to happen after this point.
        //need to figure out why.
        foreach (var item in browser.InverseParent.Where(X => X.Type == BrowserType.GateWay))
        {
            ResultBrowserItem ChildHistoryitem = item.Process(header, Historyitem.results, level+1);

            //First sucessful, ignore the rest. (basicly treate all items at the same level as a simple if structure.)
            if (ChildHistoryitem.Success == true)
            {
                Historyitem.Childern.Add(ChildHistoryitem);
                Historyitem.results = ChildHistoryitem.results;
            }
        }

        foreach (var item in browser.InverseParent.Where(X => X.Type == BrowserType.Browser))
        {
            ResultBrowserItem ChildHistoryitem = item.Process(header, Historyitem.results, level + 1);
            //First sucessful, ignore the rest. (basicly treate all items at the same level as a massive if /else if.)
            if (ChildHistoryitem.Success == true)
            {
                Historyitem.Childern.Add(ChildHistoryitem);
                Historyitem.results = ChildHistoryitem.results;
                break;
            }
        }
        return Historyitem;
    }
}
