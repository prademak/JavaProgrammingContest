using System.Collections.Generic;
using System.Linq;

namespace JavaProgrammingContest.DataAccess.InMemory{
    public class GenericRepository<T> : IRepository<T> where T : class{
        private readonly List<T> _db;

        public GenericRepository(){
            _db = new List<T>();
        }

        public IQueryable<T> GetAll(){
            return _db.AsQueryable();
        }

        public T GetById(int id){
            return _db.ElementAt(id);
        }

        public void Add(T entity){
            _db.Add(entity);
        }

        public void Remove(T entity){
            _db.Remove(entity);
        }

        public void SaveChanges() {}
    }
}