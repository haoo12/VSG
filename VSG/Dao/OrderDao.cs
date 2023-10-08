using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VSG.Models;

namespace VSG.Dao
{
    public class OrderDao
    {
        VSGModel db = null;
        public OrderDao()
        {
            db = new VSGModel();
        }
        public long Insert(Orderr orderr)
        {
            db.Orderrs.Add(orderr);
            db.SaveChanges();
            return orderr.ID;
        }
    }
}