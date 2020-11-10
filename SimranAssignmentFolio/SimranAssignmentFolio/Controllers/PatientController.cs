using SimranAssignmentFolio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimranAssignmentFolio.Controllers
{
    public class PatientController : Controller
    {

        private MindSpot db = new MindSpot();
        // GET: Patient
        [Authorize(Roles = "Patient")]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Patient")]
        public ActionResult showConsultantsNearMe()
        {
            //CompleteModel Cm = new CompleteModel();
            //List<Doctor>doctors=db.Doctors.ToList();
            List<Marker> markers = new List<Marker>();
            //ViewBag.Doctors
            markers = (from item in db.Consultants.ToList()
                       select new Marker
                       {
                           latitude = (float)db.locations.Find(item.SuburbId).latitude,
                           longitude = (float)db.locations.Find(item.SuburbId).longitude
                       }).ToList();
            return View(markers);
        }

    }
}