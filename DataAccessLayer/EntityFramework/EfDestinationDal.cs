using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repository;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfDestinationDal : GenericRepository<Destination>, IDestinationDal
    {
        public List<Destination> GetListWithGuide()
        {
            using (var context = new Context())
            {
                return context.Destinations.Include(x => x.Guide).Where(x => x.DestinationStatus == true).ToList();
            }
        }
    }
}
