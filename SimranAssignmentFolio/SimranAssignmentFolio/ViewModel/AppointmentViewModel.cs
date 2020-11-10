using SimranAssignmentFolio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace SimranAssignmentFolio.ViewModels
{
    public class AppointmentViewModel
    {
        public Appointment cons { get; set; }
        public List<Appointment> consultation { get; set; }
    
        public string userRole { get; set; }

        public Helper helper = new Helper();
    }
    public class RatingViewModel
    {
        public string userRole { get; set; }
        public List<Rating> review { get; set; }

        public Helper help = new Helper();
        public Rating ratereview { get; set; }
        public string docName { get; set; }

    }
}