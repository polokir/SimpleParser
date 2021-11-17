using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Core.CurrencyParser
{
    class CurrencyParserSettings : IParserSettings
    {
        public CurrencyParserSettings(int s, int f)
        {
            StartPoint=s;
            EndPoint = f;
        }
        public string BaseUrl { get; set; } = "https://minfin.com.ua/";
        public string Prefix { get; set; } = "currency{CurrentId}";
        public int StartPoint { get ; set ; }
        public int EndPoint { get ; set ; }
    }
}
