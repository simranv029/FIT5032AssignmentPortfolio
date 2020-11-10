using Microsoft.AspNet.Identity;
using SimranAssignmentFolio.Models;
using SimranAssignmentFolio.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace SimranAssignmentFolio.Controllers
{
    public class CouncellorController : Controller
    {


        private MindSpot db = new MindSpot();
        private ASPRole role = new ASPRole();


        // GET: Doctor
        [Authorize(Roles = "Councellor")]
        public ActionResult Index()
        {
            var user = User.Identity.GetUserId();
            var userRole = role.AspNetUserRoles.FirstOrDefault(m => m.UserId == user).RoleId;
            if (userRole == "200")
            {
                ViewBag.UserName = db.Consultants.FirstOrDefault(m => m.Id == user).FirstName;
            }
            else
            {
                ViewBag.UserName = db.Patients.FirstOrDefault(m => m.Id == user).FirstName;
            }
            return View();
        }
        [Authorize(Roles = "Councellor")]
        public ActionResult SendEmail()
        {
            EmailViewModel model = new EmailViewModel();
            ViewBag.Patients = (from item in db.Patients.ToList()
                                select new SelectListItem
                                {
                                    Text = item.FirstName,
                                    Value = item.EmailId
                                });
            var user = User.Identity.GetUserId();
            model.emailFrom = "mindspot58@gmail.com";

            return View(model);
        }
        [HttpPost]
        [Authorize(Roles = "Councellor")]
        public ActionResult SendEmail(EmailViewModel model)
        {

            MailMessage mail = new MailMessage(model.emailFrom, model.emailTo);
            mail.Subject = model.Subject;
            mail.Body = model.Body;
            mail.IsBodyHtml = false;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            string password = "!QAZ2wsx00";
            NetworkCredential networkCredential = new NetworkCredential(model.emailFrom, password);
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = networkCredential;
            smtp.Port = 587;
            smtp.Send(mail);
            ViewBag.Message = "Sent";
            return RedirectToAction("Index", "Councellor");
        }


    }
}