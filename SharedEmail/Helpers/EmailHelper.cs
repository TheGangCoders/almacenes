using SharedEmail.Email;
using SharedEmail.Entity;
using SharedEmail.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SharedEmail.Helpers
{
    public class EmailHelper
    {
        public static void SendTemplateNewPassword(string smtpServer, string[] to, string[] cc, TemplateNewPassword data)
        {
            string subject = "Su nueva contraseña ha sido generada";
            string content = getContentDataFromTemplate("template-new-password.html", typeof(TemplateNewPassword), data);
            Send(smtpServer, to, cc, subject, content, true);
        }

        private static string getContentDataFromTemplate(string template, Type type, Object data)
        {
            var pathTemplate = HttpContext.Current.Server.MapPath("~/" + Global.ROUTE_TEMPLATES + template);
            String content = File.ReadAllText(pathTemplate);

            PropertyInfo[] propertyInfo = type.GetProperties();

            foreach (PropertyInfo pInfo in propertyInfo)
            {
                string[] split = content.Split(new string[] { "${" + pInfo.Name + "}" }, StringSplitOptions.None);
                content = String.Join(pInfo.GetValue(data).ToString(), split);

            }

            return content;
        }

        private static void Send(string smtpServer, string[] to, string[] cc, string subject, string content, bool isHtml)
        {
            //Send teh High priority Email  
            EmailManager mailMan = new EmailManager(smtpServer);

            EmailSendConfigure myConfig = new EmailSendConfigure();
            // replace with your email userName  
            myConfig.ClientCredentialUserName = ConfigurationManager.AppSettings[Global.APP_SETTINGS_EMAIL_USERNAME];
            // replace with your email account password
            myConfig.ClientCredentialPassword = ConfigurationManager.AppSettings[Global.APP_SETTINGS_EMAIL_PASSWORD];
            myConfig.TOs = to;
            myConfig.CCs = cc;
            myConfig.From = ConfigurationManager.AppSettings[Global.APP_SETTINGS_EMAIL_USERNAME];
            myConfig.FromDisplayName = "";
            myConfig.Priority = System.Net.Mail.MailPriority.Normal;
            myConfig.Subject = subject;

            EmailContent myContent = new EmailContent();
            myContent.Content = content;
            myContent.IsHtml = isHtml;

            mailMan.SendMail(myConfig, myContent);
        }
    }
}
