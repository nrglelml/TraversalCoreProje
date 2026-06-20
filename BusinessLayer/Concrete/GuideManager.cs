using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class GuideManager : IGuideService
    {
        IGuideDal _guidedal;
        public GuideManager(IGuideDal guidedal)
        {
            _guidedal = guidedal;
        }

        public void ChangeStatus(int id, bool status)
        {
            _guidedal.ChangeStatus(id, status);
        }

        public void TAdd(Guide t)
        {
            _guidedal.Insert(t);
        }

        public void TDelete(Guide t)
        {
            _guidedal.Delete(t);
        }

        public Guide TGetByID(int id)
        {
            return _guidedal.GetByID(id);
        }

        public List<Guide> TGetList()
        {
            return _guidedal.GetList();
        }

        public List<Guide> TGetListByStatus(bool filter)
        {
            return _guidedal.GetListByFilter(x => x.GuideStatus == filter);
        }

        public void TUpdate(Guide t)
        {
            _guidedal.Update(t);
        }
    }
}
