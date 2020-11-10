namespace SimranAssignmentFolio.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("location")]
    public partial class location
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int? postcode { get; set; }

        [Required]
        public string suburb { get; set; }

        [Required]
        public string state { get; set; }

        public decimal latitude { get; set; }

        public decimal longitude { get; set; }
    }
}
