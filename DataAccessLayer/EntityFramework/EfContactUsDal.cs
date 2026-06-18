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
    public class EfContactUsDal : GenericRepository<ContactUs>, IContactUsDal
    {
        Context context = new Context();

        public void ChangeStatus(int id, bool status)
        {
            var values = context.ContactUs.Find(id);
            values.MessageStatus = status;
            context.Update(values);
            context.SaveChanges();
        }

        public List<ContactUs> GetListContactUsByStatus(bool status)
        {
            var values = context.ContactUs.Where(x => x.MessageStatus == status).ToList();
            return values;
        }


    }
}
