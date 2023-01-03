namespace AMS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("user")]
    public partial class user
    {
        public int ID { get; set; }

        [StringLength(50)]
        [Required]
        [DisplayName("Full Name")]
        public string Name { get; set; }
        [Required]
        [DisplayName("Phone Number")]
        public int? Phone { get; set; }

        public string Role { get; set; }
        [Required]
        [StringLength(50)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DisplayName("Password")]
        [DataType(DataType.Password)]
        public string Pass { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CretDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? UpdDate { get; set; }

        [StringLength(50)]
        public string CretBy { get; set; }

        [StringLength(10)]
        public string UpdBy { get; set; }
       
    }
}
