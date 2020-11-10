namespace SimranAssignmentFolio.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Rating")]
    public partial class Rating
    {
        public int Id { get; set; }

        [Column("Rating")]
        public int Rating1 { get; set; }

        [Required]
        public string PatientId { get; set; }

        [Required]
        public string ConsultantId { get; set; }
    }
}
