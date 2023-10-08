using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VSG.Models;

namespace VSG.Model
{
    public class Shoe_Category
    {
        public List<Shoe> ListShoe { get; set; }
        public List<Category> ListCategory { get; set; }
        public List<Shoe_Service> ListShoeService { get; set; }
        public List<SanPham> ListSanPham { get; set; }
    }
}