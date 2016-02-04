using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Maticsoft.DBUtility;

namespace MyBookShop.DAL
{
    public partial class CheckEmailDal
    {
        /// <summary>
        /// 根据用户和激活码，向用户邮箱发送激活码
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <param name="activeCode">激活码</param>
        public void SendMailToUser(Model.Users user, string activeCode)
        {
            MailMessage mailMsg = new MailMessage();//两个类，别混了，要引入System.Net这个Assembly
            mailMsg.From = new MailAddress(ConfigurationManager.AppSettings["MyEmailAddress"]);//源邮件地址 
            mailMsg.To.Add(new MailAddress(user.Mail));//目的邮件地址。可以有多个收件人
            mailMsg.Subject = ConfigurationManager.AppSettings["EmailTitle"];//发送邮件的标题 
            mailMsg.Body = string.Format("<a href = 'CheckActiveCodePage.ashx?id={0}&activeCode={1}'></a>", user.Id, activeCode);//发送邮件的内容 
            mailMsg.IsBodyHtml = true;
            SmtpClient client = new SmtpClient("smtp.qq.com");//smtp.163.com，smtp.qq.com
            client.Credentials = new NetworkCredential("wang_itcast", "wangchengwei");
            client.Send(mailMsg);
        }

        /// <summary>
        /// 根据用户Id查找激活码
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public object GetActiveCode(int userId)
        {
            string sql = "select ActiveCode from CheckEmail where UserId = @UserId";
            return DbHelperSQL.GetSingle(sql, new SqlParameter("@UserId", userId));
        }
    }
}
