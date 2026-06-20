using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class Feature2Manager : IFeature2Service
    {
        public void TAdd(Feature2 t)
        {
            throw new NotImplementedException();
        }

        public void TDelete(Feature2 t)
        {
            throw new NotImplementedException();
        }

        public Feature2 TGetByID(int id)
        {
            throw new NotImplementedException();
        }

        public List<Feature2> TGetList()
        {
            throw new NotImplementedException();
        }


        public List<Feature2> TGetListByStatus(bool filter)
        {
            throw new NotImplementedException();
        }

        public void TUpdate(Feature2 t)
        {
            throw new NotImplementedException();
        }
    }
}
