using System.Linq;

namespace JavaProgrammingContest.DataAccess{
    public interface IRepository<T>{
        IQueryable<T> GetAll();
        T GetById(int id);
        void Add(T entity);
        void Remove(T entity);
        void SaveChanges();
    }
}