using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace TempMail
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        protected void Timer1_Tick(object sender, EventArgs e)
        {
            if (Session["CountdownTimer"] != null)
                Timer.Text = "Expire email time: " + (Session["CountdownTimer"] as CountDownTimer).TimeLeft.ToString();
        }
        public class CountDownTimer
        {
            public TimeSpan TimeLeft;
            System.Threading.Thread thread;
            public CountDownTimer(TimeSpan original)
            {
                this.TimeLeft = original;
            }
            public void Start()
            {
                // Start a background thread to count down time
                thread = new System.Threading.Thread(() =>
                {
                    while (true)
                    {
                        System.Threading.Thread.Sleep(1000);
                        TimeLeft = TimeLeft.Subtract(TimeSpan.Parse("00:00:01"));
                        if (TimeLeft.ToString().Equals("00:00:00"))
                            break;
                    }
                });
                thread.Start();
            }
        }

        protected void Generate_Click(object sender, EventArgs e)
        {
            if (Session["CountdownTimer"] == null)
            {
                Session["CountdownTimer"] = new CountDownTimer(TimeSpan.Parse("00:10:00"));
                (Session["CountdownTimer"] as CountDownTimer).Start();
                Timer.Text = "Expire email time: 00:10:00";
            }
            
            updateMessages.Visible = true;
            updateMail.Visible = true;
            clearMessages.Visible = true;
            deleteMail.Visible = true;
            sendMessage.Visible = true;
        }

        protected void updateMail_Click(object sender, EventArgs e)
        {
            String id = "1000.WOL2S5T3JJLPDITV6PZSADKFBJHPBR";
            String secret = "be981947dd6d82e031110be2b5a4d2a283e2c6a7aa";
            String refreshToken = "1000.ec59a11a13a38153d373b0d29c6ec22c.35ae7e90dd6246676e6cb02b4ad39a95";

            Session["CountdownTimer"] = new CountDownTimer(TimeSpan.Parse("00:10:00"));
            (Session["CountdownTimer"] as CountDownTimer).Start();
            Timer.Text = "Expire email time: 00:10:00";

            var request = (HttpWebRequest)WebRequest.Create("https://accounts.zoho.eu/oauth/v2/token");

            var postData = "client_id=" + Uri.EscapeDataString(id);
            postData += "&grant_type=" + Uri.EscapeDataString("refresh_token");
            postData += "&client_secret=" + Uri.EscapeDataString(secret);
            postData += "&refresh_token=" + Uri.EscapeDataString(refreshToken);
            var data = Encoding.ASCII.GetBytes(postData);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = (HttpWebResponse)request.GetResponse();

            String responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            var details = new JavaScriptSerializer().Deserialize<Dictionary<string, string>>(responseString);

            Session["access_token"] = details["access_token"];
            Mail.Text = Session["access_token"].ToString();

            updateMessages.Visible = true;
            updateMail.Visible = true;
            clearMessages.Visible = true;
            deleteMail.Visible = true;
            sendMessage.Visible = true;
        }

        protected void updateMessages_Click(object sender, EventArgs e)
        {
            if (Session["access_token"] == null)
                return;

            if (Timer.Text.Equals("Expire email time: 00:00:00") || Timer.Text.Equals(""))
            {
                Session["access_token"] = null;
                return;
            }

            var client = new RestClient("https://mail.zoho.eu/api/accounts/1305411000000002002/messages/view");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            String header = "Zoho-oauthtoken " + Session["access_token"].ToString();
            request.AddHeader("Authorization", header);
            IRestResponse response = client.Execute(request);
            dynamic data = JObject.Parse(response.Content);
            
            //string from = data.data[0].sender;
            //string subject = data.data[0].subject;
            //string date = data.data[0].receivedTime;
            int numMsg = data.data.Count;

            Label[] lFrom = new Label[numMsg];
            Label[] lSubject = new Label[numMsg];
            Label[] lDate = new Label[numMsg];
            Label[] lContent = new Label[numMsg];

            for (int i = 0; i < numMsg; i++)
            {
                string idMsg = data.data[i].messageId;

                HtmlGenericControl div = new HtmlGenericControl("div");
                div.Attributes.Add("class", "msg");
                
                lFrom[i] = new Label();
                lSubject[i] = new Label();
                lDate[i] = new Label();
                lContent[i] = new Label();

                client = new RestClient("https://mail.zoho.eu/api/accounts/1305411000000002002/folders/1305411000000002014/messages/" + idMsg + "/content");
                client.Timeout = -1;
                request = new RestRequest(Method.GET);
                header = "Zoho-oauthtoken " + Session["access_token"].ToString();
                request.AddHeader("Authorization", header);
                response = client.Execute(request);

                dynamic dataMsg = JObject.Parse(response.Content);

                LiteralControl lineBreak1 = new LiteralControl("<br />");
                lFrom[i].Text = "From: " + data.data[i].sender;
                lFrom[i].Attributes.Add("style", "margin-left: 20px;");

                lSubject[i].Text = data.data[i].subject;
                lSubject[i].Font.Bold = true;
                lSubject[i].Attributes.Add("style", "margin-left: 50px;");

                System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                string time = data.data[i].receivedTime;
                dtDateTime = dtDateTime.AddMilliseconds(long.Parse(time)).ToLocalTime();
                lDate[i].Text = dtDateTime.ToString();
                lDate[i].Attributes.Add("style", "float: right; margin-right: 20px;");

                LiteralControl lineBreak2 = new LiteralControl("<br /><br />");
                lContent[i].Text = dataMsg["data"]["content"];
                LiteralControl lineBreak3 = new LiteralControl("<br />");

                div.Controls.Add(lineBreak1);
                div.Controls.Add(lFrom[i]);
                div.Controls.Add(lSubject[i]);
                div.Controls.Add(lDate[i]);
                div.Controls.Add(lineBreak2);
                div.Controls.Add(lContent[i]);
                div.Controls.Add(lineBreak3);

                Messages.Controls.Add(div);
                Messages.Controls.Add(lineBreak1);
            }

            if(numMsg == 0)
            {
                HtmlGenericControl h2 = new HtmlGenericControl("h2");
                Label noMsg = new Label();
                noMsg.Font.Bold = true;
                noMsg.Font.Italic = true;
                noMsg.Text = "You have no messages.";

                h2.Controls.Add(noMsg);

                Messages.Controls.Add(h2);
            }
        }

        protected void clearMessages_Click(object sender, EventArgs e)
        {

        }

        protected void deleteMail_Click(object sender, EventArgs e)
        {

        }
    }
}