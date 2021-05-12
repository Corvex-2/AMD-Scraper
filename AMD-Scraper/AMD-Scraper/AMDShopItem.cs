using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMD_Scraper
{
    public class AMDShopItem
    {
        public string Name { get; private set; }
        public string Price { get; private set; }
        public bool Available { get; private set; }
        
        public AMDShopItem(string Name, string Price, bool Available)
        {
            this.Name = Name;
            this.Price = Price;
            this.Available = Available;
        }

        public override string ToString()
        {
            return $"Article: {Name}\r\nPrice: {Price}\r\nAvailable: {(Available ? "Yes" : "No")}";
        }
    }
}
