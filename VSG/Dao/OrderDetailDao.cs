using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VSG.Models;

namespace VSG.Dao
{
    public class OrderDetailDao
    {
        VSGModel db = null;
        public OrderDetailDao()
        {
            db = new VSGModel();
        }
        public bool Insert(OrderDetaill detaill)
        {
            try
            {
                db.OrderDetaills.Add(detaill);
                db.SaveChanges();
                return true;
            }
            
            catch
            {
                return false;
            }
        }
    }
}