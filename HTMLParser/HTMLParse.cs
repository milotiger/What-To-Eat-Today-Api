using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Models;
using GetHTML;

namespace HTMLParser
{
    public delegate void ParseDone(List<FoodyItemInfo> Items);
    public delegate void UpdateProgess(String Progress);
    public class HTMLParse
    {
        public static event ParseDone ParseDoneNotify;
        public static event UpdateProgess UpdateProgressNotify;

        public static int ListCount = 0;
        public static int Done = 0;
        public static List<FoodyItemInfo> Items;
        private static List<Task> SubThreads;

        private static ManualResetEvent mre = new ManualResetEvent(false);
        public static List<FoodyItemInfo> ParseFromFoody(String HTMLString, int RandomCount = 12)
        {
            Items = new List<FoodyItemInfo>();
            SubThreads = new List<Task>();

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(HTMLString);

            ListCount = doc.DocumentNode.SelectNodes("//div[@class='row-item filter-result-item']").Count;
            Done = 0;

            List<int> Index = new List<int>();

            for (int i = 0; i < ListCount; i++)
                Index.Add(i);

            for (int i = 0; i < Math.Min(RandomCount, ListCount); i++)
            {
                int Random = new Random(DateTime.Now.Millisecond).Next(0, Index.Count);
                Items.Add(ParseNode(doc.DocumentNode.SelectNodes("//div[@class='row-item filter-result-item']")[Index[Random]]));
                Index.RemoveAt(Random);
            }

            //int Random1 = (new Random()).Next(0, ListCount - 1);
            //int Random2 = (new Random(DateTime.Now.Millisecond)).Next(0, ListCount - 1);

            //Items.Add(ParseNode(doc.DocumentNode.SelectNodes("//div[@class='row-item filter-result-item']")[Random1]));
            //Items.Add(ParseNode(doc.DocumentNode.SelectNodes("//div[@class='row-item filter-result-item']")[Random2]));

            
            
            return Items;
        }

        public static void LoadMoreDetail(FoodyItemInfo Item)
        {
            String HTML = GetHTML.GetHTML.URLtoHTMLFoody(Item.DetailUrl);

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(HTML);

            HtmlNode MainNode = doc.DocumentNode;
            if (MainNode.SelectSingleNode(@"//div[@class='micro-timesopen']/span[3]/span/span[1]") != null)
            {
                Item.OpenTime.Add(
                    MainNode.SelectSingleNode(@"//div[@class='micro-timesopen']/span[3]/span/span[1]").InnerText);
                Item.OpenTime.Add(
                    MainNode.SelectSingleNode(@"//div[@class='micro-timesopen']/span[3]/span/span[2]").InnerText);
            }

            if (MainNode.SelectSingleNode(@"//div[@class='res-common-minmaxprice']/span[2]/span/text()[1]") != null)
            {
                Item.Price.Add(
                    MainNode.SelectSingleNode(@"//div[@class='res-common-minmaxprice']/span[2]/span/text()[1]")
                        .InnerText.Replace("đ - ", ""));
                Item.Price.Add(
                    MainNode.SelectSingleNode(@"//div[@class='res-common-minmaxprice']/span[2]/span/span").InnerText);
            }

            if (MainNode.SelectNodes(@"//*[@id='gallery_list']/ul/li[1]/a/img") != null)
                foreach (HtmlNode GalleryNode in MainNode.SelectNodes(@"//*[@id='gallery_list']/ul/li"))
                {
                    if (GalleryNode.SelectSingleNode(@"a/img") != null)
                        Item.MorePic.Add(GalleryNode.SelectSingleNode(@"a/img").GetAttributeValue("src", ""));
                }

            if (MainNode.SelectNodes(@"//div[@class='micro-home-recent-review review-item']") != null)
                foreach (HtmlNode CommentNode in MainNode.SelectNodes(@"//div[@class='micro-home-recent-review review-item']"))
                {
                    CommentDetail Cmt = new CommentDetail();
                    try
                    {
                        Cmt.UserAva = CommentNode.SelectSingleNode(@"article/div[1]/div/a/img")
                            .GetAttributeValue("src", "");
                        Cmt.UserName = System.Net.WebUtility.HtmlDecode(CommentNode.SelectSingleNode(@"article/div[2]/div[1]/a/span").InnerText);
                        Cmt.Content = CommentNode.SelectSingleNode(@"article/div[2]/div[3]").InnerText;
                        if (CommentNode.SelectSingleNode(@"article/div[2]/div[6]/div[1]/ul/li[1]/a/img") != null)
                            Cmt.ContentPic =
                                CommentNode.SelectSingleNode(@"article/div[2]/div[6]/div[1]/ul/li[1]/a/img")
                                    .GetAttributeValue("src", "");
                        Cmt.Time = CommentNode.SelectSingleNode(@"article//span[@class='address']").InnerText;
                        Cmt.Title =
                            CommentNode.SelectSingleNode(@"article//div[@class='title']")
                                .InnerText.Replace("  ", "")
                                .Replace("\r\n", "");
                        Cmt.Normalize();
                        Item.CommentDetails.Add(Cmt);
                    }
                    catch (Exception e)
                    {
                        //MessageBox.Show(e.Message);
                    }
                }
            if (Item.CommentDetails.Count > 0)
                Item.CommentDetails.RemoveAt(Item.CommentDetails.Count - 1);

            Done++;

            UpdateProgressNotify?.Invoke(Done + "/" + ListCount);

            if (Done == ListCount)
            {
                ParseDoneNotify?.Invoke(Items);
            }
        }

        private static FoodyItemInfo ParseNode(HtmlNode selectNode)
        {
            String Avatar = selectNode.SelectSingleNode(@"div[1]/a/img").GetAttributeValue("src", "");
            String Headline = selectNode.SelectSingleNode("div[2]//div[1]//h2//a").InnerText.Replace("  ", "").Replace("\r\n", "");
            String Addresslv1 = selectNode.SelectSingleNode("div[2]//div[2]//div[1]//span[1]").InnerText;
            String AddressLv2 = selectNode.SelectSingleNode("div[2]//div[2]//div[1]//a//span").InnerText;
            String AddressLv3 = selectNode.SelectSingleNode("div[2]//div[2]//div[1]//span[2]").InnerText;
            String Rating = selectNode.SelectSingleNode(@"div[2]/div[1]/div/div[1]").InnerText.Replace(" ", "").Replace("\r\n", "");
            String Comments = selectNode.SelectSingleNode(@"div[2]/div[4]/div[1]/div/div[2]/a/span[2]").InnerText;
            String Pic = selectNode.SelectSingleNode(@"div[2]/div[4]/div[1]/div/div[3]/a/span[2]").InnerText;
            String Url = selectNode.SelectSingleNode(@"div[1]/a").GetAttributeValue("href", "");

            List<String> Tags = new List<string>();
            if (selectNode.SelectNodes("div[2]//div[3]//a") != null)
                foreach (HtmlNode Tag in selectNode.SelectNodes("div[2]//div[3]//a"))
                {
                    Tags.Add(System.Net.WebUtility.HtmlDecode(Tag.InnerText.Replace("  ", "").Replace("\r\n", "")));
                }
            Tags.RemoveAt(Tags.Count - 1);

            FoodyItemInfo Item =  new FoodyItemInfo(Url, Headline, Avatar, Addresslv1, AddressLv2, AddressLv3, Rating, Comments, Pic, Tags);
            LoadMoreDetail(Item);

            return Item;
        }

        public static string GetXPathValue(String HTML, String XPath)
        {
            string Value = "null";

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(HTML);

            try
            {
                Value = doc.DocumentNode.SelectSingleNode(XPath).InnerHtml;
            }
            catch (Exception e)
            {
                Value = e.Message;
            }

            return Value;
        }

        public static string GetAttr(String HTML, String XPath, String Attr)
        {
            string Value = "null";

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(HTML);

            try
            {
                Value = doc.DocumentNode.SelectSingleNode(XPath).GetAttributeValue(Attr, Value);
            }
            catch (Exception e)
            {
                Value = e.Message;
            }

            return Value;
        }
    }

}
