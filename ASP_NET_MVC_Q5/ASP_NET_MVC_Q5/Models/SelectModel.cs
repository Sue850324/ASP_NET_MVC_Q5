using System;
using System.Collections.Generic;
using System.Web;
namespace ASP_NET_MVC_Q5.Models
{
    public class Select
    {
        public string Locale { get; set; }
        public string Product_Name { get; set; }
        public decimal MaxPrice { get; set; }
        public decimal MiniPrice { get; set; }
        public int Id { set; get; }
        public DateTime Create_Date { set; get; }
        public decimal Price { set; get; }
    }

}


 