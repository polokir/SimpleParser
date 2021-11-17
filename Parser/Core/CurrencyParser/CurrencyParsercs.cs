using AngleSharp.Html.Dom;
using System.Collections.Generic;
using System.Linq;


namespace Parser.Core.CurrencyParser
{
    class CurrencyParsercs : IParser<string[]>
    {
        public string[] Parse(IHtmlDocument document)
        {
            var list = new List<string>();
            var items = document.QuerySelectorAll("td").Where(item => item.ClassName != null && item.ClassName.Contains("mfcur-nbu-full-wrap"));

            foreach (var item in items)
            {
                list.Add(item.TextContent);
            }

            return list.ToArray();
        }
    }
}
