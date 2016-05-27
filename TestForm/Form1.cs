using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GetHTML;
using HTMLParser;
using GetHTML;

namespace TestForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private String HTML;
        private void button1_Click(object sender, EventArgs e)
        {
            HTML = GetHTML.GetHTML.URLtoHTMLFoody(txtAddr.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            rtbValue.AppendText(txtXPath.Text + "\r\n");
            rtbValue.AppendText(HTMLParse.GetXPathValue(HTML, @txtXPath.Text) + "\r\n");
            rtbValue.AppendText("--------" + "\r\n");
            rtbValue.SelectionStart = rtbValue.Text.Length;
            rtbValue.ScrollToCaret();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            rtbValue.AppendText(txtXPath.Text + " Get " + txtAddr.Text + "\r\n");
            rtbValue.AppendText(HTMLParse.GetAttr(HTML, @txtXPath.Text, @txtAttr.Text) + "\r\n");
            rtbValue.AppendText("--------" + "\r\n");
            rtbValue.SelectionStart = rtbValue.Text.Length;
            rtbValue.ScrollToCaret();
        }
    }
}
