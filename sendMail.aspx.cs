using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TempMail
{
    public partial class sendMail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void SendMail_Click(object sender, EventArgs e)
        {
            if (Session["access_token"] == null)
                return;

            string emailTo = EmailTo.Text;
            string emailCc = EmailCc.Text;
            string emailBcc = EmailBcc.Text;
            string subject = Subject.Text;
            string content = Text.Text;

            string json = "{\"fromAddress\": \"" + Session["email"] + "\",";
            json += "\"toAddress\": \"" + emailTo + "\",";
            if(!emailCc.Equals(""))
                json += "\"ccAddress\": \"" + emailCc + "\",";
            if (!emailBcc.Equals(""))
                json += "\"bccAddress\": \"" + emailBcc + "\",";
            json += "\"subject\": \"" + subject+ "\",";
            json += "\"content\": \"" + content + "\"}";

            Console.WriteLine(json);

            var client = new RestClient("https://mail.zoho.eu/api/accounts/1305411000000002002/messages");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            string header = "Zoho-oauthtoken " + Session["access_token"].ToString();
            request.AddHeader("Authorization", header);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);

            DateTime dtDateTime = DateTime.Now;

            Database database = new Database(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
            database.addMessage(Session["email"].ToString(), emailTo, dtDateTime.Date.ToString("yyyy-MM-dd HH:mm:ss"), subject, content, false, "0");
        }
    }
}