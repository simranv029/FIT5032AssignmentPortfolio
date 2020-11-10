using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimranAssignmentFolio.Models
{
    public class Helper
    {
        private PatientModel db = new PatientModel();
        private MindSpot mp = new MindSpot();

        private ASPRole role = new ASPRole();
        public Consultant getDoctorFromId(string id)
        {
            var doctor = mp.Consultants.FirstOrDefault(m => m.Id == id);
            return doctor;
        }
        public Patient getPatientFromId(string id)
        {
            var patient = db.Patients.FirstOrDefault(m => m.Id == id);
            return patient;
        }
    }
}