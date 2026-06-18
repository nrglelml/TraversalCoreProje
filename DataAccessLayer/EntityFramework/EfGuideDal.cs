using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repository;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfGuideDal:GenericRepository<Guide>,IGuideDal
    {

        Context context = new Context();

        public void ChangeStatus(int id,bool status)
        {
            var values = context.Guides.Find(id);
            values.GuideStatus = status;
            context.Update(values);
            context.SaveChanges();
        }
    }
}
