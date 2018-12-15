using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace ZfbQrcode
{
    public partial class Form1 : Form
    {
        Method method = new Method();
        string let;//存储备注字符
        DateTime dt = DateTime.Parse("00:01:00");
        public Form1()
        {
            InitializeComponent();
        }

        private void btqr_Click(object sender, EventArgs e)
        {
            let = method.LetterNumber(10);//生成备注字符
            timer1.Stop();//停止计时容器
            dt = DateTime.Parse("00:01:00");//重新倒计时
            string url = string.Format($"http://paysdog.net/json/show_qr.html?i=alipays%3A%2F%2Fplatformapi%2Fstartapp%3FappId%3D09999988%26actionType%3DtoAccount%26goBack%3DYES%26amount%3D{tboxmoney.Text.Trim()}%26userId%3D{tboxacc.Text.Trim()}%26memo%3D{let}", 5, 123456);
            System.Net.WebRequest webreq = System.Net.WebRequest.Create(url);
            System.Net.WebResponse webres = webreq.GetResponse();
            using (System.IO.Stream stream = webres.GetResponseStream())
            {
                pbox.Image = Image.FromStream(stream);
            }
            //timer1.Start();
            timer1_Tick(sender, e);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = true;//开启计时事件
            string time = dt.ToLongTimeString().ToString();//记录时间
            lbtime.Text = "付款码过期后请勿支付" + "\n" + let + "\n" + time;//在控件显示时间
            if (dt == DateTime.Parse("00:00:00"))
            {
                pbox.Image = Properties.Resources.过期图;
                timer1.Stop();
            }
            dt = dt.AddSeconds(-1);//每次减一秒

        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

            this.webBrowser1.Document.GetElementById("J-input-user").SetAttribute("placeholder", "xiaohe238@qq.com");
            this.webBrowser1.Document.GetElementById("password_rsainput").SetAttribute("value", "he362017");
            //this.webBrowser1.Document.GetElementById("J-login-btn").InvokeMember("click");
            this.webBrowser1.DocumentCompleted -= new WebBrowserDocumentCompletedEventHandler(webBrowser1_DocumentCompleted);//以上代码只执行一次           

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.webBrowser1.Navigate("https://consumeprod.alipay.com/record/standard.htm");




        }


        private void button2_Click(object sender, EventArgs e)
        {
            string content;

            //for (int i = 0; i < 1000; i++)
            //{

                content = webBrowser1.Document.Body.InnerHtml;//获取源码

                if (content.Contains("切换到高级版"))
                {
                //this.webBrowser1.Document.GetElementById("J-today").InvokeMember("click");
                this.webBrowser1.Navigate("https://consumeprod.alipay.com/record/standard.htm");
                content = webBrowser1.Document.Body.InnerHtml;//获取当前网页源码
                    if (content.Contains(let))
                    {
                        pbox.Image = Properties.Resources.pay_ok;//换为支付成功图片
                        lbtime.Text = "支付成功！";
                        timer1.Stop();
                        //i = 1000;
                    }
                    else Thread.Sleep(1500);
                }

            //}




        }
    }
}
