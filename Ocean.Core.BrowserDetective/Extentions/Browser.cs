using Microsoft.Extensions.Logging;

namespace Ocean.Core.BrowserDetective.Extentions;

public static class BrowserExtention
{
    public static ResultBrowserItem Process(this Data.Models.Browser browser, IDictionary<string, string> header)
    {
        return Process(browser, header, new Result());
    }
    public static ResultBrowserItem Process(this Data.Models.Browser browser, IDictionary<string, string> header, Result result, int level = 0)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        sb.AppendLine($"{level}:{browser.Type}:{browser.Name}");


        if (!result.ContainsKey(string.Empty) && header.ContainsKey("User-Agent"))
        {
            result.Add(string.Empty, header["User-Agent"]);
            sb.AppendLine($"User-Agent:\"{header["User-Agent"]}\"");
        }
        ResultBrowserItem Historyitem = new ResultBrowserItem(browser);

        Historyitem.Success = true; //Assume it will Pass (Default)
        Historyitem.results = result;

        //this is setup to Allow the group to fail. by one item.
        bool Success = true;
        foreach (var item in browser.Identifications)
        {
            var id = new BrowserDetective.Identification(item);
            id.Match(header, ref result);
            if (!String.IsNullOrWhiteSpace(item.Match))
            {
                sb.Append($"{browser.Name}:Match:{item.Match}:");
            }
            else
            {
                sb.Append($"{browser.Name}:NonMatch:{item.NonMatch}:");
            }
            // only continue precessing if they all pass.
            if (id.Success)
            {
                Historyitem.MatchList.AddLast(id);
                sb.AppendLine("Pass");
            }
            else if (browser.Type == BrowserType.Browser)
            {
                //Marks the Item as failed match.
                Historyitem.NotMatchedList.AddLast(id);
                Success = false;
                Historyitem.Success = false;
                sb.AppendLine("Fail");
                browser._logger.Log(LogLevel.Information, sb.ToString());
                return Historyitem;
            }
            else if (browser.Type == BrowserType.GateWay)
            {
                Success = false;
                sb.AppendLine("Fail");
            }
        }
        Historyitem.Success = Success;
        browser._logger.Log(LogLevel.Information, sb.ToString());
        sb = new System.Text.StringBuilder();
        sb.AppendLine($"{level}:{browser.Type}:{browser.Name}");
        //Must all pass so to apply tranformation methods.
        if (Success == true)
        {
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
                    //makes sure we have something to work with at least.
                    if (item.Value.Contains("${") && Historyitem.MatchList.Count > 0 && Historyitem.MatchList.First(x => x.Success == true) != null)
                    {
                        string v = Historyitem.MatchList.First(x => x.Success == true).Result(item.Value);
                        //empty or null means no valid convertion option available.
                        if (String.IsNullOrWhiteSpace(v) == false)
                            Historyitem.results[item.Name] = v;
                        else if (result.ContainsKey(item.Name))
                            Historyitem.results[item.Name] = result[item.Name];
                        else
                            Historyitem.results[item.Name] = string.Empty;
                    }
                    else
                    {
                        Historyitem.results[item.Name] = item.Value;
                    }
                    sb.AppendLine($"{browser.Name}:Result[{item.Name}]=\"{Historyitem.results[item.Name]}\"");
                }
            }
            //The wiping out of the Results has to happen after this point.
            //need to figure out why.
            foreach (var item in browser.InverseParent.Where(X => X.Type == BrowserType.GateWay && X.ParentId == browser.Id))
            {
                ResultBrowserItem ChildHistoryitem = item.Process(header, Historyitem.results, level + 1);

                if (ChildHistoryitem.Success == true)
                {
                    Historyitem.Childern.Add(ChildHistoryitem);
                    Historyitem.results = ChildHistoryitem.results;
                }
            }

            foreach (var item in browser.InverseParent.Where(X => X.Type == BrowserType.Browser && X.ParentId == browser.Id))
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
        }
        browser._logger.Log(LogLevel.Information, sb.ToString());
        return Historyitem;
    }
}
