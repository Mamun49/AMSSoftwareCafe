using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace AMS.Models
{
    public class PS_tbl
    {
        [Key]
        public int ID { get; set; }
        public string PSName { get; set; }
        public int Mobile { get; set; }
        public string Address { get; set; }
        public string Remarks { get; set; }
        public string Type { get; set; }
        
    }
}