using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ocean.Core.BrowserDetective.Models;

public partial class Browser
{
    public long Id { get; set; }

    public long? ParentId { get; set; }

    [NotMapped]
    public string parentID { get; set; } = string.Empty;

    public BrowserType Type { get; set; }

    public long? Prority { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Capability> Capabilities { get; set; } = new List<Capability>();

    public virtual ICollection<Capture> Captures { get; set; } = new List<Capture>();

    public virtual ICollection<Identification> Identifications { get; set; } = new List<Identification>();

    public virtual ICollection<SampleHeader> Samples { get; set; } = new List<SampleHeader>();

    public virtual ICollection<Browser> InverseParent { get; set; } = new List<Browser>();

    public virtual Browser? Parent { get; set; }

    public virtual ResultBrowserItem Process(IDictionary<string, string> header)
    {
        return Process(header, new Result());
    }
    [NotMapped]
    public ILogger? _logger { get; set; } = null;
    public virtual ResultBrowserItem Process(IDictionary<string, string> header, Result result, int level = 0)
    {
        Console.WriteLine($"{level}");
        for (int i = 1; i <= level; ++i)
        {
            Console.WriteLine("\t");
        }
        Console.WriteLine($"{Name}");
        if (!result.ContainsKey(string.Empty))
        {
            result.Add(string.Empty, header["User-Agent"]);
        }
        ResultBrowserItem Historyitem = new ResultBrowserItem(this);
        Historyitem.results = result;

        foreach (var item in Identifications)
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
            else if (this.Type == BrowserType.Browser)
            {
                //Marks the Item as failed match.
                Historyitem.NotMatchedList.AddLast(id);
                Historyitem.Success = false;
                //Console.WriteLine("Fail");
                return Historyitem;
            }
        }
        //These are optional, additional RegEx to help refine the lookups.
        foreach (var item in Captures)
        {
            var id = new BrowserDetective.Identification(item);
            id.Match(header, ref result);
            //Only care about the sucessfull ones, ignore the other ones.
            if (id.Success)
            {
                Historyitem.MatchList.AddLast(id);
            }
        }
        foreach (var item in Capabilities)
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
        foreach (var item in InverseParent.Where(X => X.Type == BrowserType.GateWay))
        {
            ResultBrowserItem ChildHistoryitem = item.Process(header, Historyitem.results, level+1);

            //First sucessful, ignore the rest. (basicly treate all items at the same level as a simple if structure.)
            if (ChildHistoryitem.Success == true)
            {
                Historyitem.Childern.Add(ChildHistoryitem);
                Historyitem.results = ChildHistoryitem.results;
            }
        }

        foreach (var item in InverseParent.Where(X => X.Type == BrowserType.Browser))
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

    public virtual String Debug(int level)
    {
        System.Text.StringBuilder writer = new System.Text.StringBuilder();
        writer.Append($"{level}");
        for (int i = 1; i <= level; ++i)
        {
            writer.Append("\t");
        }
        writer.Append($"{Type}:{this.Name}\t{this.parentID}" + System.Environment.NewLine);
        foreach (var item in InverseParent.Where(X => X.Type == BrowserType.GateWay))
        {
            writer.Append(item.Debug(level + 1));
        }
        foreach (var item in InverseParent.Where(X => X.Type == BrowserType.Browser))
        {
            writer.Append(item.Debug(level + 1));
        }
        return writer.ToString();
    }
}
