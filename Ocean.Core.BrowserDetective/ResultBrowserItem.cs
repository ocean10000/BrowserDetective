using Ocean.Core.BrowserDetective.Data.Models;
namespace Ocean.Core.BrowserDetective
{
    //Track the Results of this Browser object and Processing of the Header / Exisxting Result Data.
    public class ResultBrowserItem
    {
        public ResultBrowserItem(Browser b)
        {
            this.browser = b;
            MatchList = new LinkedList<Identification>();
            results = new Result();
            NotMatchedList = new LinkedList<Identification>();
            Childern = new List<ResultBrowserItem>();
        }
        public Browser browser { get; set; }
        public List<ResultBrowserItem> Childern { get; set; }
        public LinkedList<Identification> MatchList { get; set; }
        public LinkedList<Identification> NotMatchedList { get; set; }
        public Result results { get; set; }
        public bool Success { get; set; } = false;

        public virtual List<KeyValuePair<Browser, int>> Debug(int level)
        {
            List<KeyValuePair<Browser, int>> NodesUsed = new List<KeyValuePair<Browser, int>>();
            NodesUsed.Add(new KeyValuePair<Browser, int>(browser, level));
            foreach (var item in Childern)
            {
                NodesUsed.AddRange(item.Debug(level + 1));
            }
            return NodesUsed;
        }
    }
}
