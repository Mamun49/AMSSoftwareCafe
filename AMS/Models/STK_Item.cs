using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace AMS.Models
{
    [Table("STK_Item")]
    public partial class STK_Item
    {
        [Key]
        public int ID { get; set; }
        public int CID { get; set; }
        public int SCID { get; set; }
        public string ItemName { get; set; }
        public string ItemCode { get; set; }
        public string Brand { get; set; }
        public int Unit { get; set; }
        public int MinStock { get; set; }
        public int DiscountTp { get; set; }
        public int Discount { get; set; }
        public string Remarks { get; set; }
        public DateTime? InsertTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string InsertBy { get; set; }
        public string UpdateBy { get; set; }
    }
}