using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ocean.Core.BrowserDetective.Console.Models
{
    public class BrowserNode
    {
        public int ID { get; set; }
        public long Raw_ID { get; set; }
        public string Node { get; set; } = string.Empty;
        public int Index { get; set; }
    }
}
