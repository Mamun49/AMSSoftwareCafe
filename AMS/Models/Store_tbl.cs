using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace AMS.Models
{
    [Table("Store_tbl")]
    public partial class Store_tbl
    {
        [Key]
        public int StID { get; set; }
        public string StoreName { get; set; }
        public int StoreID { get; set; }
        public int AgentID { get; set; }
        public string Remarks { get; set; }
        public string InsBy { get; set; }
        
        public string UpdateBy { get; set; }
        public DateTime? InsTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        
    }
}