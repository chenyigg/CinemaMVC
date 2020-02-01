using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Model;
using System.Web.Mvc;
using BLL;
using System.Net.Mail;
using System.Net;
using System.Runtime.Remoting.Contexts;

namespace WebUI.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        //登录界面
        public ActionResult Check()
        {
            //初始化时先判断是否有记住密码
            if (Request.Cookies["UserAccount"] != null && Request.Cookies["UserPwd"] != null)
            {
                ViewBag.Account = Request.Cookies["UserAccount"].Value;
                ViewBag.Pwd = Request.Cookies["UserPwd"].Value;
            }
            else
            {
                ViewBag.Account = "";
                ViewBag.Pwd = "";
            }

            return View();
        }

        #region 判断是否已经登录

        public ActionResult LoginName()
        {
            if (Session["UserAccount"] == null || Session["UserAccount"].ToString() == "" || Session["UserPwd"] == null || Session["UserPwd"].ToString() == "")
            {
                return Json(new { State = "no" });
            }
            else
            {
                return Json(new { State = "ok" });
            }
        }

        #endregion 判断是否已经登录

        #region 校验是否登录成功

        public ActionResult Verify(UsersInfo user)
        {
            UsersInfo us = new UsersInfoBLL().Select(use => use.UserAccount == user.UserAccount && use.UsersPwd == user.UsersPwd)
                           .Cast<UsersInfo>().FirstOrDefault();
            if (us != null)
            {
                //如果点击记住密码则生成缓存
                if (Request["Remember"] != null && Request["Remember"] == "true")
                {
                    HttpCookie account = new HttpCookie("UserAccount", us.UserAccount);
                    account.Expires = DateTime.Now.AddDays(1);
                    HttpCookie pwd = new HttpCookie("UserPwd", us.UsersPwd);
                    pwd.Expires = DateTime.Now.AddDays(1);

                    Response.Cookies.Add(account);
                    Response.Cookies.Add(pwd);
                }

                //记住登录状态
                Session["UserAccount"] = us.UserAccount;
                Session["UserPwd"] = us.UsersPwd;

                return Json(new { state = "ok" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { state = "no" }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion 校验是否登录成功

        #region 校验该账户是否被抢注

        public ActionResult HasAccount(UsersInfo user)
        {
            UsersInfo us = new UsersInfoBLL().Select(use => use.UserAccount == user.UserAccount)
                           .Cast<UsersInfo>().FirstOrDefault();

            //如果没找到，则证明没被抢注
            if (us == null)
            {
                return Json(new { state = "ok" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { state = "no" }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion 校验该账户是否被抢注

        #region 发送验证码

        public ActionResult GetEmail(string Email)
        { //发送邮件
            //实例化一个发送邮件类。
            MailMessage mailMessage = new MailMessage();
            //发件人邮箱地址，方法重载不同，可以根据需求自行选择。
            mailMessage.From = new MailAddress("1812613402@qq.com");
            //收件人邮箱地址。

            string num = Email.ToString();
            mailMessage.To.Add(new MailAddress("1812613402@qq.com"));
            //邮件标题。
            mailMessage.Subject = "发送邮件测试";
            //邮件内容。
            Random random = new Random();
            int x = random.Next(1000, 9999);
            mailMessage.Body = "您的验证码为" + x.ToString();

            //实例化一个SmtpClient类。
            SmtpClient client = new SmtpClient();

            //在这里我使用的是qq邮箱，所以是smtp.qq.com，如果你使用的是126邮箱，那么就是smtp.126.com。
            client.Host = "smtp.qq.com";

            //使用安全加密连接。
            client.EnableSsl = true;

            //不和请求一块发送。
            client.UseDefaultCredentials = false;

            //验证发件人身份(发件人的邮箱，邮箱里的生成授权码);
            client.Credentials = new NetworkCredential("1812613402@qq.com", "dduudtjypszgbceh");

            //发送
            client.Send(mailMessage);

            return Json(new { Email = x }, JsonRequestBehavior.AllowGet);
        }

        #endregion 发送验证码

        #region 进行注册

        public ActionResult Register(UsersInfo use)
        {
            try
            {
                bool b = new UsersInfoBLL().Add(use);
                if (b)
                {
                    return Json(new { state = "ok" });
                }
                else
                {
                    Response.Write("<script>alert('Error！');</script>");
                    return Json(new { state = "no" });
                }
            }
            catch
            {
                Response.Write("<script>alert('请勿重复注册！');</script>");
                return Json(new { state = "no" });
            }
        }

        #endregion 进行注册
    }
}