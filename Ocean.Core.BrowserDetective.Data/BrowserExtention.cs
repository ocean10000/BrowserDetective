using Ocean.Core.BrowserDetective.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ocean.Core.BrowserDetective.Data.Extentions
{
    public static class BrowserExtention
    {
        public static List<KeyValuePair<Browser, int>> Debug(this Data.Models.Browser browser, int level)
        {
            List<KeyValuePair<Browser, int>> dic = new List<KeyValuePair<Browser, int>>();
            dic.Add(new KeyValuePair<Browser, int>(browser, level));
            foreach (var item in browser.InverseParent.Where(X => X.Type == BrowserType.GateWay))
            {
                dic.AddRange(item.Debug(level + 1));
            }
            foreach (var item in browser.InverseParent.Where(X => X.Type == BrowserType.Browser))
            {
                dic.AddRange(item.Debug(level + 1));
            }
            return dic;
        }
    }
}
