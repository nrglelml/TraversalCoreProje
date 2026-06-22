using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class ReservationManager : IReservationService
    {

        IReservationDal _reservationdal;

        public ReservationManager(IReservationDal reservationdal)
        {
            _reservationdal = reservationdal;
        }

        public List<Reservation> GetListWithReservationByStatus(int id, string status)
        {
            return _reservationdal.GetListWithReservationByStatus(id, status);
        }

        public void TAdd(Reservation t)
        {
            _reservationdal.Insert(t);
        }

        public void TDelete(Reservation t)
        {
            _reservationdal.Delete(t);
        }

        public Reservation TGetByID(int id)
        {
            return _reservationdal.GetByID(id);
        }

        public List<Reservation> TGetList()
        {
            return _reservationdal.GetList();
        }

        public List<Reservation> TGetListByStatus(bool filter)
        {
            throw new NotImplementedException();
            // return _reservationdal.GetListByFilter(x => x.Status == filter);
        }

        public List<Reservation> TGetListWithDestination()
        {
            return _reservationdal.GetListWithDestination();
        }

        public void TUpdate(Reservation t)
        {
            _reservationdal.Update(t);
        }
    }
}
