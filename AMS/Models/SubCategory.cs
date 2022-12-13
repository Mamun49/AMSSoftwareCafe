using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace AMS.Models
{
    [Table("SubCategory")]
    public partial class SubCategory
    {
        [Key]
        public int SCID { get; set; }
       [DisplayName("Sub Category Name")]
        public string SubCatName { get; set; }
        [DisplayName("Category Name")]
        public int CID { get; set; }
    }
}