﻿//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Security.Policy;
//using System.Text;
//using System.Threading.Tasks;

//namespace PullFromFoody
//{
//    class DDAUItemInfo
//    {
//        public String Headline;
//        public String Address;
//        public String Rating;
//        public String Avatar;
//        public String StartPrice;
//        public String EndPrice;
//        public String Viewers;
//        public String Comments;
//        public String DetailUrl;

//        public DDAUItemInfo(string headline, string address, string rating, string avatar, string startPrice, string endPrice, string viewers, string comments, string url)
//        {
//            Headline = System.Net.WebUtility.HtmlDecode(headline);
//            Address = System.Net.WebUtility.HtmlDecode(address);
//            Rating = rating;
//            Avatar = avatar;
//            StartPrice = startPrice;
//            EndPrice = endPrice;
//            Viewers = viewers;
//            Comments = comments;
//            DetailUrl = "http://diadiemanuong.com" + url;
//        }
//    }

//    class FoodyItemInfo
//    {
//        public String DetailUrl;
//        public String Headline;
//        public String Thumbnail;
//        public String AddressLv1;
//        public String AddressLv2;
//        public String AddressLv3;
//        public String Rating;
//        public String Comments;
//        public String Pictures;
//        public List<String> Tag;
//        public List<String> OpenTime;
//        public List<String> Price;
//        public List<String> MorePic;
//        public List<FoodyMenuItem> MenuItems;
//        public List<CommentDetail> CommentDetails;
//        public String Phone;


//        public FoodyItemInfo(string detailUrl, string headline, string thumbnail, string addressLv1, string addressLv2, string addressLv3, string rating, string comments, string pictures, List<string> tag)
//        {
//            DetailUrl = "http://www.foody.vn" + detailUrl;
//            Headline = System.Net.WebUtility.HtmlDecode(headline);
//            Thumbnail = thumbnail;
//            AddressLv1 = System.Net.WebUtility.HtmlDecode(addressLv1);
//            AddressLv2 = System.Net.WebUtility.HtmlDecode(addressLv2);
//            AddressLv3 = System.Net.WebUtility.HtmlDecode(addressLv3);
//            Rating = rating;
//            Comments = comments;
//            Pictures = pictures;
//            Tag = tag;

//            OpenTime = new List<string>();
//            Price = new List<string>();
//            MorePic = new List<string>();
//            MenuItems = new List<FoodyMenuItem>();
//            CommentDetails = new List<CommentDetail>();
//        }
//    }

//     class FoodyMenuItem
//     {
//         public String Avatar;
//         public String Price;
//         public String DetailLink;
//     }

//    class CommentDetail
//    {
//        public String UserAva;
//        public String Title;
//        public String Content;
//        public String ContentPic;
//        public String UserName;
//        public String Time;

//        public CommentDetail(string userAva, string title, string content, string contentPic, string userName, string time)
//        {
//            UserAva = userAva;
//            Title = title;
//            Content = content;
//            ContentPic = contentPic.Replace("//", "http://www.foody.vn/");
//            UserName = userName;
//            Time = time;
//        }

//        public CommentDetail()
//        {
//            UserAva = "";
//            Title = "";
//            Content = "";
//            ContentPic = "";
//            UserName = "";
//            Time = "";
//        }

//        public void Normalize()
//        {
//            ContentPic = "http:" + ContentPic;
//            UserAva = "http:" + UserAva;
//            UserName = System.Net.WebUtility.HtmlDecode(UserName);
//        }
//    }
//}
