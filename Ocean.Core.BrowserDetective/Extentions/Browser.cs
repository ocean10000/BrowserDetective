using Microsoft.Extensions.Logging;

namespace Ocean.Core.BrowserDetective.Extentions;

public static class BrowserExtention
{
    public static Result Process(this Data.Models.Browser browser, IDictionary<string, string> header)
    {
        return Process(browser, header, new Result());
    }
    public static Result Process(this Data.Models.Browser browser, IDictionary<string, string> header, Result result, int level = 0)
    {
        LinkedList<Identification> MatchList = new LinkedList<Identification>();
        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        sb.AppendLine($"{level}:{browser.Type}:{browser.Name}");

        if (!result.ContainsKey(string.Empty) && header.ContainsKey("User-Agent"))
        {
            result.Add(string.Empty, header["User-Agent"]);
            sb.AppendLine($"User-Agent:\"{header["User-Agent"]}\"");
        }

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
                MatchList.AddLast(id);
                sb.AppendLine("Pass");
            }
            else if (browser.Type == BrowserType.Browser)
            {
                //Marks the Item as failed match.
                Success = false;
                sb.AppendLine("Fail");
                browser._logger.Log(LogLevel.Information, sb.ToString());
                return result;
            }
            else if (browser.Type == BrowserType.GateWay)
            {
                Success = false;
                sb.AppendLine("Fail");
            }
        }
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
                    MatchList.AddLast(id);
                }
            }
            foreach (var item in browser.Capabilities)
            {
                //make sure we have a non-Null/non-empty value
                if (string.IsNullOrEmpty(item.Value) == false)
                {
                    result[item.Name] = item.Value;

                    //makes sure we have something to work with at least.
                    if (item.Value.Contains("${"))
                    {
                        result[item.Name] = string.Empty;
                        foreach (var m in MatchList)
                        {
                            string v = m.Result(item.Value);
                            if (String.IsNullOrWhiteSpace(v) == false && v.Contains("${") == false)
                            {
                                result[item.Name] = v;
                            }
                        }
                    }

                    result.Trace.Add(new Data.Models.Trackitem() { BrowserID = browser.Id, BrowserName = browser.Name, Name = item.Name, Value = result[item.Name] });
                    sb.AppendLine($"{browser.Name}:Result[{item.Name}]=\"{result[item.Name]}\"");
                }
            }
            //cheap way to sent it up the chain that this level was a sucess (even if there are no Capabilities at this level)
            result.Trace.Add(new Data.Models.Trackitem() { BrowserID = browser.Id, BrowserName = browser.Name, Name = "Success", Value = bool.TrueString });
            browser._logger.Log(LogLevel.Information, sb.ToString());
            sb = new System.Text.StringBuilder();

            foreach (var item in browser.InverseParent.Where(X => X.Type == BrowserType.GateWay && X.ParentId == browser.Id))
            {
                result = item.Process(header, result, level + 1);
            }

            foreach (var item in browser.InverseParent.Where(X => X.Type == BrowserType.Browser && X.ParentId == browser.Id))
            {
                result = item.Process(header, result, level + 1);

                //First sucessful, ignore the rest. (basicly treate all items at the same level as a massive if /else if.)
                if (result.Trace.Any(X => X.BrowserID == item.Id))
                {
                    break;
                }
            }
        }
        if (sb.Length > 0)
        {
            browser._logger.Log(LogLevel.Information, sb.ToString());
        }
        return result;
    }
}
