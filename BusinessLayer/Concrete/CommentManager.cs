using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class CommentManager : ICommentService
    {
        ICommentDal _commentdal;
        public CommentManager (ICommentDal commentdal)
        {
            _commentdal = commentdal;
        }

        public List<Comment> GetListCommentWithDestinationAndUser(int id)
        {
           return _commentdal.GetListCommentWithDestinationAndUser(id);
        }

        public void TAdd(Comment t)
        {
            _commentdal.Insert(t);
        }

        public void TDelete(Comment t)
        {
            _commentdal.Delete(t);
        }

        public Comment TGetByID(int id)
        {
           return _commentdal.GetByID(id);  
        }

        public List<Comment> TGetDestinationByID(int id)
        {
            return _commentdal.GetListByFilter(x=>x.DestinationID==id);
        }

        public List<Comment> TGetList()
        {
            throw new NotImplementedException();
        }

        public List<Comment> TGetListByStatus(bool filter)
        {
            throw new NotImplementedException();
        }

        public List<Comment> TGetListCommentWithDestination()
        {
            return _commentdal.GetListCommentWithDestination();
        }

        public List<Comment> TGetUserCommentsList(int id)
        {
            return _commentdal.GetUserCommentsList(id);
        }

        public void TUpdate(Comment t)
        {
            _commentdal.Update(t);
        }
    }
}
