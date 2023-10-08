using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VSG.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public string tenSP { get; set; }
        public string AnhSP { get; set; }
        public int SoLuong { get; set; }
        public decimal Gia { get; set; }
        public decimal tongTien
        {
            get { return Gia * SoLuong; }
        }
    }
}