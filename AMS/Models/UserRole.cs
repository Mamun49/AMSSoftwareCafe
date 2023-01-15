namespace AMS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserRole")]
    public partial class UserRole
    {
        public int id { get; set; }
        [DisplayName("User List")]
        public int UserID { get; set; }
        public string Role { get; set; }

       

    }
}
