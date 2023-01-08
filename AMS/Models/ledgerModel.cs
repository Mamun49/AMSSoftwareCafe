using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMS.Models
{
    public class ledgerModel
    {
        public int ItemID { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public DateTime datefrom { get; set; }
        public DateTime dateto { get; set; }
        public int storeid { get; set; }
    }
}