using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GetHTML
{
    public class GetHTML
    {
        public static String URLtoHTML(string URL)
        {
            String Data = "";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            request.KeepAlive = false;
            request.UnsafeAuthenticatedConnectionSharing = true;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = null;

                if (response.CharacterSet == null)
                {
                    readStream = new StreamReader(receiveStream);
                }
                else
                {
                    readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                }

                Data = readStream.ReadToEnd();

                response.Close();
                readStream.Close();
            }
            return Data;
        }

        public static String URLtoHTMLFoody(string URL)
        {
            //URL += "&page=2";
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(URL);
            myRequest.Method = "GET";
            myRequest.UserAgent = "FireFox";
            WebResponse myResponse = myRequest.GetResponse();

            StreamReader sr = new StreamReader(myResponse.GetResponseStream(), System.Text.Encoding.UTF8);
            string result = sr.ReadToEnd();
            sr.Close();
            myResponse.Close();
            return result;
        }

        private static String HTMLData1;
        private static String HTMLData2;
        private static bool IsDone = false;
        public static String FoodyCrawer(String URL, int Count)
        {
            WebBrowser Foody = new WebBrowser();
            //Foody.ScriptErrorsSuppressed = true;
            Foody.Navigate(URL);
            IsDone = false;

            while (Foody.ReadyState != WebBrowserReadyState.Complete)
            {
                Thread.Sleep(100);
            }

            return Foody.DocumentText;
        }

        private static void Foody_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            HTMLData1 = ((WebBrowser) sender).DocumentText;
            HtmlDocument htmlDocument = ((WebBrowser) sender).Document;
            if (htmlDocument != null)
            {
                object ClickDone = htmlDocument.GetElementById("scrollLoadingPage").InvokeMember("Click");
            }
            IsDone = true;
        }
    }
}
