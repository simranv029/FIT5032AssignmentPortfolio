namespace SimranAssignmentFolio.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Consultant")]
    public partial class Consultant
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int consultantId { get; set; }

        [Required]
        public string Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public int? SuburbId { get; set; }

        public string Emailld { get; set; }
    }
}
