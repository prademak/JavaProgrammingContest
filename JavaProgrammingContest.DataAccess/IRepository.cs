using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JavaProgrammingContest.DataAccess
{
    public interface IRepository<T>
    {
        IQueryable<T> GetAll();
        T GetById(int id);
    }

    public class GenericRepository<T> : IRepository<T> {
        public IQueryable<T> GetAll() {
            return null;
        }

        public T GetById(int id) {
            return null;
        }
    }
}
