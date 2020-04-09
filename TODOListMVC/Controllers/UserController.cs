using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TODOListMVC.Models;
using TODOListMVC.Services;

namespace TODOListMVC.Controllers
{
    public class UserController : Controller
    {
        private readonly ITaskData db;

        public UserController(ITaskData db)
        {
            this.db = db;
        }

        //Register Action
        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }


        //Register POST action
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration([Bind(Exclude = "IsEmailVerified,ActivationCode")] UserModel user)
        {
            bool Status = false;
            string Message = "";
            //Model validation
            if (ModelState.IsValid)
            {
                #region//email is already exist
                var isExist = IsEmailExist(user.Email);
                if (isExist)
                {
                    ModelState.AddModelError("EmailExist", "Email already exist");
                    return View(user);
                }
                #endregion

                #region//Generate activation code
                user.ActivationCode = Guid.NewGuid();
                #endregion

                #region Password Hashing
                user.Password = Crypto.Hash(user.Password);
                user.ConfirmPassword = Crypto.Hash(user.ConfirmPassword);
                #endregion
                user.IsEmailVerified = false;

                #region Save to db

                    db.Add(user);
                    //send email to user
                    SendVerificationLinkEmail(user.Email, user.ActivationCode.ToString());
                    Message = "Registration successfully done. Account activation link " +
                        " has been sent to your email id: " + user.Email;
                    Status = true;
                
                #endregion
            }
            else
            {
                Message = "Invalid Request";
            }

            ViewBag.Message = Message;
            ViewBag.Status = Status;

            return View(user);
        }

        //Verify Email
        [HttpGet]
        public ActionResult VerifyAccount(string id)
        {
            bool Status = false;
 
                //db.Configuration.ValidateOnSaveEnabled = false;

                var account = db.GetVerifyCode(id);
                if (account != null)
                {
                    account.IsEmailVerified = true;
                    db.UpdateEmailVerifyStatus(account);
                    Status = true;
                }
                else
                {
                    ViewBag.Message = "Invalid Request";
                }

                ViewBag.Status = Status;
                return View();
            
        }



        //Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }


        //Login POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLogin login, string ReturnUrl)
        {
            string message = "";
                var user = db.GetUserByEmail(login.Email);
                if (user != null)
                {
                    if (string.Compare(Crypto.Hash(login.Password), user.Password) == 0)
                    {
                        int timeout = login.RememberMe ? 525600 : 20; // 52560 min = 1 year
                        var ticket = new FormsAuthenticationTicket(login.Email, login.RememberMe, timeout);
                        string encrypted = FormsAuthentication.Encrypt(ticket);
                        var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                        cookie.Expires = DateTime.Now.AddMinutes(timeout);
                        cookie.HttpOnly = true;
                        Response.Cookies.Add(cookie);

                        if (Url.IsLocalUrl(ReturnUrl))
                        {
                            return Redirect(ReturnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        message = "Invalid credential provided";
                    }
                }
                else
                {
                    message = "Invalid credential provided";
                }
            


            ViewBag.Message = message;
            return View();
        }


        //Logout
        [Authorize]
        [HttpPost]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "User");
        }

        [NonAction]
        public bool IsEmailExist(string email)
        {
                var check = db.GetUserByEmail(email);
                //var v = dc.Users.Where(u => u.Email == email).FirstOrDefault();
                return check != null;            
        }

        [NonAction]
        public void SendVerificationLinkEmail(string email, string activationCode)
        {
            var verifyUrl = "/User/VerifyAccount/" + activationCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

            var fromEmail = new MailAddress("mytodolistmvc@gmail.com", "TODO List activation");
            var toEmail = new MailAddress(email);
            var fromEmailPassword = "zaq1@WSX";
            string subject = "Your account is successfully created!";

            string body = "<br/><br/>We are excited to tell you that your account is " +
                " successfully create. Please click on the below link to verify your account" +
                "<br/><br/><a href = '" + link + "'>" + link + " </a>";
            var smtp = new SmtpClient()
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })

                smtp.Send(message);

        }
    }
}