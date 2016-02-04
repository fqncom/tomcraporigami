using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyBookShop.Model.MyEnum;

namespace MyBookShop
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string transCode = Request["transCode"] ?? "";
            if (transCode == "")
            {
                return;
            }
            string msg = string.Empty;
            Model.Users user = new Model.Users();
            user.Address = Request["Address"] ?? "";
            //user.Id = Request[""]
            user.LoginId = Request["LoginId"] ?? "";
            user.LoginPwd = Request["LoginPwd"] ?? "";
            user.Mail = Request["Mail"] ?? "";
            user.Name = Request["UserName"] ?? "";
            user.Phone = Request["Phone"] ?? "";
            user.UserState.Id = Convert.ToInt32(UsersEnum.NormalState);

            switch (transCode)
            {
                case "CheckVCode"://检测验证码是否正确
                    string vCode = Request["VCode"] ?? "";
                    msg = CheckVCode(vCode) ? "success" : "faild";
                    Session["vCode"] = null;//验证完成后清空session中的验证码，防止暴力破解
                    break;

                case "CheckIsExistName"://检测用户名是否存在 ==success表示存在，否则不存在
                    msg = CheckIsExistName(user.LoginId) ? "success" : "failed";
                    break;

                case "AddNewUserInfo":
                    if (!CheckEmpty(user) || CheckIsExistName(user.LoginId))
                    {
                        return;
                    }
                    int userId = -1;
                    user.LoginPwd = Common.CommonTools.GetMd5String(Common.CommonTools.GetMd5String(user.LoginPwd));//两次md5加密

                    //msg = AddNewUserInfo(user, out userId) ? "success" : "failed";
                    if (AddNewUserInfo(user, out userId))
                    {
                        //user.Id = userId;
                        //发送邮件
                        //SendEmailToUser(user);==坑爹的邮箱
                        msg = "success";
                        //跳转页面
                        //Response.Redirect("ShowMsg.aspx?TransCode=registerSuccess");//==========出现问题。无法跳转，临时使用前台跳转====如果前台使用了异步的数据处理方式，则在后台的时候不能使用response.redirect进行跳转
                    }
                    else
                    {
                        msg = "failed";
                        //Response.Redirect("ShowMsg.aspx?TransCode=registerFailed");
                    }
                    break;
            }
            Response.Write(msg);
            Response.End();
        }

        //发送邮件
        private void SendEmailToUser(Model.Users user)
        {
            Model.CheckEmail ce = new Model.CheckEmail();
            ce.ActiveCode = Guid.NewGuid().ToString();
            ce.Actived = false;
            ce.UserId = user.Id;
            BLL.CheckEmailBll bll = new BLL.CheckEmailBll();
            bll.Add(ce);
            bll.SendMailToUser(user, ce.ActiveCode);
        }

        //注册成功，增加数据
        private bool AddNewUserInfo(Model.Users user, out int userId)
        {
            BLL.UsersBll bll = new BLL.UsersBll();
            return bll.Add(user, out userId) > 0;
        }

        //检测用户名是否存在
        private bool CheckIsExistName(string loginId)
        {
            BLL.UsersBll bll = new BLL.UsersBll();
            return bll.CheckIsExistNameByName(loginId);
        }

        //验证码检测
        private bool CheckVCode(string vCode)
        {
            if (string.IsNullOrEmpty(vCode))
            {
                return false;
            }
            if (Session["vCode"] == null)
            {
                return false;
            }
            return Session["vCode"].ToString().Equals(vCode);
        }

        //空值检测
        private bool CheckEmpty(Model.Users user)
        {
            if (string.IsNullOrEmpty(user.LoginId))
            {
                return false;
            }
            if (string.IsNullOrEmpty(user.LoginPwd))
            {
                return false;
            }
            if (string.IsNullOrEmpty(user.Mail))
            {
                return false;
            }
            if (string.IsNullOrEmpty(user.Name))
            {
                return false;
            }
            if (string.IsNullOrEmpty(user.Phone))
            {
                return false;
            }
            return true;
        }
    }
}