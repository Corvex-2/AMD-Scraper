using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AMD_Scraper
{
    public class AMDShop
    {
        private const string AMD_SHOP_TITLE_EXPRESSION = "<div class=\"shop-title\">([^<]*)\\<";
        private const string AMD_SHOP_PRICE_EXPRESSION = "<div class=\"shop-price\">([^<]*)\\<";
        private const string AMD_SHOP_LINK_EXPRESSION = "<div class=\"shop-links\">([^<]*)\\<";
        private const string AMD_SHOP_URL = "https://www.amd.com/de/direct-buy/de";

        public static IEnumerable<AMDShopItem> GetItems()
        {
            HttpClient HTTP_CLIENT = new HttpClient()
            {
                Timeout = TimeSpan.FromSeconds(5),
            };
            HTTP_CLIENT.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate");
            HTTP_CLIENT.DefaultRequestHeaders.Add("Accept-Language", "en-US,en;q=0.5");
            HTTP_CLIENT.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");

            using (var message = new HttpRequestMessage(HttpMethod.Get, AMD_SHOP_URL))
            {
                using (var response = HTTP_CLIENT.SendAsync(message).Result)
                {
                    return getItemsInternal(GZipping.Decompress(response.Content.ReadAsByteArrayAsync().Result));
                }
            }
        }

        private static IEnumerable<AMDShopItem> getItemsInternal(string Html)
        {
            var str = Html.Replace(Environment.NewLine, "");

            List<AMDShopItem> found = new List<AMDShopItem>();

            MatchCollection TITLE_MATCHES = Regex.Matches(str, AMD_SHOP_TITLE_EXPRESSION);
            MatchCollection PRICE_MATCHES = Regex.Matches(str, AMD_SHOP_PRICE_EXPRESSION);
            MatchCollection LINK_MATCHES = Regex.Matches(str, AMD_SHOP_LINK_EXPRESSION);

            for (int i = 0; i < TITLE_MATCHES.Count; i++)
            {
                var name = TITLE_MATCHES[i].Groups[1].Value.Trim();
                var price = PRICE_MATCHES[i].Groups[1].Value.Trim();
                var available = LINK_MATCHES[i].Groups[1].Value.Trim() != "Out of Stock";
                var item = new AMDShopItem(name, price, available);
                found.Add(item);
            }
            return found;
        }
    }
}
