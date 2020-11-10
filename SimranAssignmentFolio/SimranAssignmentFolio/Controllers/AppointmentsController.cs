using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using SimranAssignmentFolio.Models;
using SimranAssignmentFolio.ViewModels;

namespace SimranAssignmentFolio.Controllers
{
   
    public class AppointmentsController : Controller
    {
        private MindSpot db = new MindSpot();
        private  ASPRole role = new ASPRole();
       

        public ActionResult Index()
        {
            var user = User.Identity.GetUserId();
            AppointmentViewModel appointmentViewModel= new AppointmentViewModel();
            appointmentViewModel.userRole = role.AspNetUserRoles.FirstOrDefault(m => m.UserId == user).RoleId;
            if (appointmentViewModel.userRole == "200")
            {
                appointmentViewModel.consultation = db.Appointments.Where(m => m.DoctorId == user).ToList();
            }
            else
            {
                appointmentViewModel.consultation = db.Appointments.Where(m => m.PatientId == user).ToList();
            }
            return View(appointmentViewModel);
        }

        public ActionResult RatingIndex()
        {
            var user = User.Identity.GetUserId();

            RatingViewModel rvm = new RatingViewModel();
            rvm.review = db.Ratings.Where(m => m.PatientId == user).ToList();
            return View(rvm);
        }
        [HttpGet]
        public ActionResult AddRating(int id)
        {
            RatingViewModel model = new RatingViewModel();
            model.ratereview = db.Ratings.Find(id);
            model.docName = db.Consultants.Find(model.ratereview.ConsultantId).FirstName;
            return View(model);
        }
        [HttpPost]
        public ActionResult AddRating(RatingViewModel model)
        {
            if (ModelState.IsValid)
            {
                db.Entry(model.ratereview).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Message"] = "Doctor Rated Successfully";
                return RedirectToAction("Index", "Patient");
            }
            return RedirectToAction("RatingIndex", "Appointments");
        }

        // GET: Consultations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppointmentViewModel cvm = new AppointmentViewModel();
            cvm.cons = db.Appointments.Find(id);
            if (cvm.cons == null)
            {
                return HttpNotFound();
            }
            return View(cvm);
        }

        // GET: Consultations/Create
        public ActionResult Create()
        {
            List<Consultant> doctors = db.Consultants.ToList();
            ViewBag.Doctors = (from item in db.Consultants.ToList()
                               select new SelectListItem
                               {
                                   Text = item.FirstName,
                                   Value = item.Id
                               });
            Appointment consultation = new Appointment();
            consultation.PatientId = User.Identity.GetUserId();
            return View(consultation);
        }

        // POST: Consultations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Appointment consultation)
        {
            if (ModelState.IsValid)
            {
                db.Appointments.Add(consultation);

                //Review r = db.Reviews.FirstOrDefault(m=>m.)
                Rating r = new Rating();
                r.ConsultantId = consultation.DoctorId;
                r.PatientId = consultation.PatientId;
                r.Rating1 = 0;
                Random rand = new Random();
                r.Id = rand.Next(1, 100000);
                db.Ratings.Add(r);
                db.SaveChanges();
                TempData["Message"] = "Consulation Booked Successfully";
                return RedirectToAction("Index");
            }

            return View(consultation);
        }

        // GET: Consultations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment consultation = db.Appointments.Find(id);
            if (consultation == null)
            {
                return HttpNotFound();
            }
            return View(consultation);
        }

        // POST: Consultations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Appointment consultation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(consultation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(consultation);
        }

        // GET: Consultations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppointmentViewModel cvm = new AppointmentViewModel();
            cvm.cons = db.Appointments.Find(id);
            if (cvm.cons == null)
            {
                return HttpNotFound();
            }
            return View(cvm);
        }

        // POST: Consultations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Appointment consultation = db.Appointments.Find(id);
            db.Appointments.Remove(consultation);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
