using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http;
using Models;
using WhatToEatTodayApi.Models;
using HTMLParser;
using GetHTML;

namespace WhatToEatTodayApi.Controllers
{
    public class SearchController: ApiController
    {
        public List<FoodyItemInfo> getSearch(String q, int count)
        {
            String URL = Global.BaseURL + q + "&page=5";

            return HTMLParse.ParseFromFoody(GetHTML.GetHTML.URLtoHTMLFoody(URL), count);
        }

       

    }

    public class FullSizeAlbumController : ApiController
    {
        [HttpPost]
        public FoodyItemInfo getFullSizeAlbum([FromBody] FoodyItemInfo Item)
        {
            return HTMLParse.LoadFullSizePic(Item);
        }
    }

    public class MenuItemController : ApiController
    {
        [HttpPost]
        public FoodyItemInfo getMenuItem([FromBody] FoodyItemInfo Item)
        {
            return HTMLParse.LoadMenuItem(Item);
        }
    }
    
}