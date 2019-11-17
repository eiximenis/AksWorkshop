using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeerBook.Web
{
    public class Settings
    {
        public SettingUrls Urls { get; set; }
    }

    public class SettingUrls
    {
        public string Basket { get; set; }
        public string BasketGrpc { get; set; }
        public string Catalog { get; set; }
        public string Order { get; set; }
    }

}
