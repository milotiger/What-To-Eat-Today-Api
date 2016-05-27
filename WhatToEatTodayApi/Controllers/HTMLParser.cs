//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using HtmlAgilityPack;
//using Models;
//using HtmlDocument = HtmlAgilityPack.HtmlDocument;

//namespace Controller
//{
//    class HTMLParser
//    {
//        public static List<FoodyItemInfo> ParseFromFoody(String HTMLString)
//        {
//            List<FoodyItemInfo> Items = new List<FoodyItemInfo>();

//            HtmlDocument doc = new HtmlDocument();
//            doc.LoadHtml(HTMLString);

//            if (doc.DocumentNode.SelectNodes("//div[@class='row-item filter-result-item']") == null)
//            {
//                return null;
//            }

//            foreach (HtmlNode selectNode in doc.DocumentNode.SelectNodes("//div[@class='row-item filter-result-item']"))
//            {
//                String Avatar = selectNode.SelectSingleNode(@"div[1]/a/img").GetAttributeValue("src", "");
//                String Headline = selectNode.SelectSingleNode("div[2]//div[1]//h2//a").InnerText.Replace("  ", "").Replace("\r\n", "");
//                String Addresslv1 = selectNode.SelectSingleNode("div[2]//div[2]//div[1]//span[1]").InnerText;
//                String AddressLv2 = selectNode.SelectSingleNode("div[2]//div[2]//div[1]//a//span").InnerText;
//                String AddressLv3 = selectNode.SelectSingleNode("div[2]//div[2]//div[1]//span[2]").InnerText;
//                String Rating = selectNode.SelectSingleNode(@"div[2]/div[1]/div/div[1]").InnerText.Replace(" ", "").Replace("\r\n", "");
//                String Comments = selectNode.SelectSingleNode(@"div[2]/div[4]/div[1]/div/div[2]/a/span[2]").InnerText;
//                String Pic = selectNode.SelectSingleNode(@"div[2]/div[4]/div[1]/div/div[3]/a/span[2]").InnerText;
//                String Url = selectNode.SelectSingleNode(@"div[1]/a").GetAttributeValue("href", "");

//                List<String> Tags = new List<string>();
//                if (selectNode.SelectNodes("div[2]//div[3]//a") != null)
//                    foreach (HtmlNode Tag in selectNode.SelectNodes("div[2]//div[3]//a"))
//                    {
//                        Tags.Add(System.Net.WebUtility.HtmlDecode(Tag.InnerText.Replace("  ", "").Replace("\r\n", "")));
//                    }
//                Tags.RemoveAt(Tags.Count-1);

//                FoodyItemInfo Item = new FoodyItemInfo(Url, Headline, Avatar, Addresslv1, AddressLv2, AddressLv3, Rating, Comments, Pic, Tags);
//                LoadMoreDetail(Item);

//                Items.Add(Item);
//            }

//            return Items;
//        }

//        public static void LoadMoreDetail(FoodyItemInfo Item)
//        {
//            String HTML = GetHTML.URLtoHTML2(Item.DetailUrl);

//            HtmlDocument doc = new HtmlDocument();
//            doc.LoadHtml(HTML);

//            HtmlNode MainNode = doc.DocumentNode;
//            if (MainNode.SelectSingleNode(@"//div[@class='micro-timesopen']/span[3]/span/span[1]") != null)
//            {
//                Item.OpenTime.Add(
//                    MainNode.SelectSingleNode(@"//div[@class='micro-timesopen']/span[3]/span/span[1]").InnerText);
//                Item.OpenTime.Add(
//                    MainNode.SelectSingleNode(@"//div[@class='micro-timesopen']/span[3]/span/span[2]").InnerText);
//            }

//            if (MainNode.SelectSingleNode(@"//div[@class='res-common-minmaxprice']/span[2]/span/text()[1]") != null)
//            {
//                Item.Price.Add(
//                    MainNode.SelectSingleNode(@"//div[@class='res-common-minmaxprice']/span[2]/span/text()[1]")
//                        .InnerText.Replace("đ - ", ""));
//                Item.Price.Add(
//                    MainNode.SelectSingleNode(@"//div[@class='res-common-minmaxprice']/span[2]/span/span").InnerText);
//            }

//            if (MainNode.SelectNodes(@"//*[@id='gallery_list']/ul/li[1]/a/img") != null)
//                foreach (HtmlNode GalleryNode in MainNode.SelectNodes(@"//*[@id='gallery_list']/ul/li"))
//                {
//                    if (GalleryNode.SelectSingleNode(@"a/img") != null)
//                        Item.MorePic.Add(GalleryNode.SelectSingleNode(@"a/img").GetAttributeValue("src", ""));
//                }

//            if (MainNode.SelectNodes(@"//div[@class='micro-home-recent-review review-item']") != null)
//                foreach (HtmlNode CommentNode in MainNode.SelectNodes(@"//div[@class='micro-home-recent-review review-item']"))
//                {
//                    CommentDetail Cmt = new CommentDetail();
//                    try
//                    {
//                        Cmt.UserAva = CommentNode.SelectSingleNode(@"article/div[1]/div/a/img")
//                            .GetAttributeValue("src", "");
//                        Cmt.UserName = System.Net.WebUtility.HtmlDecode(CommentNode.SelectSingleNode(@"article/div[2]/div[1]/a/span").InnerText);
//                        Cmt.Content = CommentNode.SelectSingleNode(@"article/div[2]/div[3]").InnerText;
//                        if (CommentNode.SelectSingleNode(@"article/div[2]/div[6]/div[1]/ul/li[1]/a/img") != null)
//                            Cmt.ContentPic =
//                                CommentNode.SelectSingleNode(@"article/div[2]/div[6]/div[1]/ul/li[1]/a/img")
//                                    .GetAttributeValue("src", "");
//                        Cmt.Time = CommentNode.SelectSingleNode(@"article//span[@class='address']").InnerText;
//                        Cmt.Title =
//                            CommentNode.SelectSingleNode(@"article//div[@class='title']")
//                                .InnerText.Replace("  ", "")
//                                .Replace("\r\n", "");
//                        Cmt.Normalize();
//                        Item.CommentDetails.Add(Cmt);
//                    }
//                    catch (Exception e)
//                    {
//                        //MessageBox.Show(e.Message);
//                    }
//                }
//            if (Item.CommentDetails.Count > 0)
//                Item.CommentDetails.RemoveAt(Item.CommentDetails.Count - 1);
//        }

//    }
//}
