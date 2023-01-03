using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace AMS.Models
{
    public class STK_TRANSMST
    {
        [Key]
        public int ID { get; set; }
        public string TransTP { get; set; }
        public DateTime? TransDate { get; set; }
        public string TransYear { get; set; }
        public string TransNo { get; set; }
        public string StoreFrom { get; set; }
        public string StoreTo { get; set; }
        public int PSID { get; set; }
        public int TotalAmount { get; set; }
        public string Remarks { get; set; }
        public DateTime? InsDate { get; set; }
        public string InsBy { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}