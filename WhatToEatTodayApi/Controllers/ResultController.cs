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
    public class ResultController: ApiController
    {
        public List<FoodyItemInfo> getResult(String q, int count)
        {
            String URL = Global.BaseURL + q;

            return HTMLParse.ParseFromFoody(GetHTML.GetHTML.URLtoHTMLFoody(URL), count);
        }

    }
    
}