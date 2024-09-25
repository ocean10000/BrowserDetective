using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ocean.Core.BrowserDetective.Models;
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
        public LinkedList<Ocean.Core.BrowserDetective.Identification> MatchList { get; set; }
        public LinkedList<Ocean.Core.BrowserDetective.Identification> NotMatchedList { get; set; }
        public Result results { get; set; }  
        public bool Success { get; set; } = false;

        public virtual String Debug(int level)
        {
            System.Text.StringBuilder writer = new System.Text.StringBuilder();
            writer.Append($"{level}");
            for (int i = 1; i <= level; ++i)
            {
                writer.Append("\t");
            }
            writer.Append($"{browser.Type}:{browser.Name}\t{browser.parentID}" + System.Environment.NewLine);
            foreach (var item in Childern)
            {
                writer.Append(item.Debug(level + 1));
            }
            return writer.ToString();
        }
    }
}
