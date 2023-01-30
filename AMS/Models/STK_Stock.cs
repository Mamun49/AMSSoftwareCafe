using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace AMS.Models
{
    [Table("STK_Stock")]
    public partial class STK_Stock
    {
        [Key]
        public int StID { get; set; }
        public int ItemID { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public int StockQty { get; set; }
        public int LastPrice { get; set; }
        
        public string Remarks { get; set; }
        public DateTime? InsertTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string InsertBy { get; set; }
        public string UpdateBy { get; set; }
    }
}