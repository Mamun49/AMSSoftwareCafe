using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace AMS.Models
{
    [Table("Category")]
    public partial class Category
    {
        [Key]
        public int CID { get; set; }
        [DisplayName("Category Name")]
        public string CatName { get; set; }
    }
}