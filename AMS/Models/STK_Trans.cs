using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace AMS.Models
{
    public class STK_Trans
    {
        [Key]
        public int ID { get; set; }
        public string TRANSTP { get; set; }
        public DateTime? TRANSDT { get; set; }
        public string TRANSYY { get; set; }
        public string TRANSNO { get; set; }
        public string STOREFR { get; set; }
        public string STORETO { get; set; }
        public int PSID { get; set; }
        public int ITEMID { get; set; }
        public int QTY { get; set; }
        public string ITEMSL { get; set; }
        public string SIZE { get; set; }
        public string COLOR { get; set; }
        public int RATE { get; set; }
        public int AMOUNT { get; set; }
        public string REMARKS { get; set; }
        public string InsBy { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? InsDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}