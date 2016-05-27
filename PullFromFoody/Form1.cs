using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Models;
using HTMLParser;
using GetHTML;

namespace PullFromFoody
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            HTMLParse.ParseDoneNotify += HtmlParseOnParseDoneNotify;
            HTMLParse.UpdateProgressNotify += HtmlParseOnUpdateProgressNotify;
        }

        static EventWaitHandle waitHandle = new ManualResetEvent(false);
        private void HtmlParseOnUpdateProgressNotify(string progress)
        {
            this.Invoke((MethodInvoker) delegate
            {
                lbStatus.Text = progress;
            });
        }

        private void HtmlParseOnParseDoneNotify(List<FoodyItemInfo> items)
        {
            isDone = true;
            this.Invoke((MethodInvoker)delegate
            {
                //MessageBox.Show("Done!");
            });
        }

        String baseURL = "http://www.foody.vn/ho-chi-minh/dia-diem?q=";
        static bool isDone = false;
        private void btnGet_Click(object sender, EventArgs e)
        {
            String URL = baseURL + txtURL.Text.Replace(" ", "+");
            //Thread Parse = new Thread(() => HTMLParse.ParseFromFoody(GetHTML.GetHTML.URLtoHTML2(URL)));
            //Parse.Start();

            GetHTML.GetHTML.FoodyCrawer(URL, 1);
            //Writer.DatatoJson(HTMLParse.ParseFromFoody(GetHTML.GetHTML.FoodyCrawer(URL, 1)), "E:\\" + txtURL.Text + ".json");

            MessageBox.Show("Main Done");
        }

        private void txtURL_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
