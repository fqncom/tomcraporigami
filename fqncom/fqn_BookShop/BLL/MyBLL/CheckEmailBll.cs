using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MyBookShop.BLL
{
    public partial class CheckEmailBll
    {
        #region 在业务层直接定义方法。而不是在数据层，因为此处的处理数据的方法都在其他的数据层实现了
        ///// <summary>
        ///// 根据用户和激活码，向用户邮箱发送激活码
        ///// </summary>
        ///// <param name="user">用户信息</param>
        ///// <param name="activeCode">激活码</param>
        //public void SendMailToUser(Model.Users user, string activeCode)
        //{
        //    dal.SendMailToUser(user, activeCode);
        //}
        #endregion

        /// <summary>
        /// 根据用户和激活码，向用户邮箱发送激活码
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <param name="activeCode">激活码</param>
        public void SendMailToUser(Model.Users user, string activeCode)
        {
            BLL.SettingsBll bll = new BLL.SettingsBll();
            MailMessage mailMsg = new MailMessage();//两个类，别混了，要引入System.Net这个Assembly
            mailMsg.From = new MailAddress(bll.GetModel("系统邮件地址").Value);//源邮件地址 
            mailMsg.To.Add(new MailAddress(user.Mail));//目的邮件地址。可以有多个收件人
            mailMsg.Subject = "感谢注册fqn_图书商城账号";//发送邮件的标题 
            mailMsg.Body = string.Format("<a href = 'CheckActiveCodePage.ashx?id={0}&activeCode={1}'></a>", user.Id, activeCode);//发送邮件的内容 
            mailMsg.IsBodyHtml = true;
            SmtpClient client = new SmtpClient(bll.GetModel("系统邮件SMTP").Value);//smtp.163.com，smtp.qq.com
            client.Credentials = new NetworkCredential(bll.GetModel("系统邮件用户名").Value, bll.GetModel("系统邮件密码").Value);
            client.Send(mailMsg);
        }

        public void SendEmailForGetPassword(string loginId, string loginMail)
        {
            BLL.SettingsBll bll = new BLL.SettingsBll();
            MailMessage mailMsg = new MailMessage();//两个类，别混了，要引入System.Net这个Assembly
            mailMsg.From = new MailAddress(bll.GetModel("系统邮件地址").Value);//源邮件地址 
            mailMsg.To.Add(new MailAddress(loginMail));//目的邮件地址。可以有多个收件人
            mailMsg.Subject = "感谢注册fqn_图书商城账号";//发送邮件的标题 
            mailMsg.Body = string.Format("<a href = 'ChangeMyPassword.aspx?loginId={0}&'></a>", loginId);//发送邮件的内容 
            mailMsg.IsBodyHtml = true;
            SmtpClient client = new SmtpClient(bll.GetModel("系统邮件SMTP").Value);//smtp.163.com，smtp.qq.com
            client.Credentials = new NetworkCredential(bll.GetModel("系统邮件用户名").Value, bll.GetModel("系统邮件密码").Value);
            client.Send(mailMsg);
        }

        /// <summary>
        /// 根据用户的激活码与数据库进行比对
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="activeCode"></param>
        /// <returns></returns>
        public bool CheckActiveCode(int userId, string activeCode)
        {
            string getActiveCode = dal.GetActiveCode(userId).ToString();
            if (string.IsNullOrEmpty(getActiveCode))
            {
                return false;
            }
            if (activeCode.Equals(getActiveCode))
            {
                return true;
            }
            return false;
        }
    }
}
