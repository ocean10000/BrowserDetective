using Ocean.Core.BrowserDetective.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ocean.Core.BrowserDetective.Console.Models
{
    public  class ResultItem
    {
        public long ID { get; set; }
        public long Raw_ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
    }
}
