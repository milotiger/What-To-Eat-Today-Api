//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Net;
//using System.Security.Policy;
//using System.Text;
//using System.Threading.Tasks;

//namespace Controller
//{
   
//    class GetHTML
//    {
//        public static String URLtoHTML(string URL)
//        {
//            String Data = "";
//            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
//            request.KeepAlive = false;
//            request.UnsafeAuthenticatedConnectionSharing = true;
//            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

//            if (response.StatusCode == HttpStatusCode.OK)
//            {
//                Stream receiveStream = response.GetResponseStream();
//                StreamReader readStream = null;

//                if (response.CharacterSet == null)
//                {
//                    readStream = new StreamReader(receiveStream);
//                }
//                else
//                {
//                    readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
//                }

//                Data = readStream.ReadToEnd();

//                response.Close();
//                readStream.Close();
//            }
//            return Data;
//        }

//        public static String URLtoHTML2(string URL)
//        {
//            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(URL);
//            myRequest.Method = "GET";
//            myRequest.UserAgent = "Fiddler";
//            WebResponse myResponse = myRequest.GetResponse();

//            StreamReader sr = new StreamReader(myResponse.GetResponseStream(), System.Text.Encoding.UTF8);
//            string result = sr.ReadToEnd();
//            sr.Close();
//            myResponse.Close();
//            return result;
//        }
//    }
//}
