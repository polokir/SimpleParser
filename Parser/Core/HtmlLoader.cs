using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace Parser.Core
{
    class HtmlLoader
    {
        readonly HttpClient client;
        readonly string url;

        public HtmlLoader(IParserSettings settings)
        {
            url = $"{settings.BaseUrl}/{settings.Prefix}/";
            client = new HttpClient();
        }

        public async Task<string> GetSourseByPageId(int id)
        {
            var current = url.Replace("{CurrentId}", id.ToString());
            var response = await client.GetAsync(current);
            string source = null;

            if(response!=null && response.StatusCode == HttpStatusCode.OK)
            {
                source = await response.Content.ReadAsStringAsync();
            }
            return source;

        }
    }
}
