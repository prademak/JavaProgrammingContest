using System.Data.Entity;
using System.Linq;

namespace JavaProgrammingContest.DataAccess.SQLServer{
    public class GenericRepository<T> : IRepository<T> where T : class{
        private readonly DbContext _context;
        private readonly DbSet<T> _set;

        public GenericRepository(DbContext context){
            _context = context;
            _set = _context.Set<T>();
        }

        public IQueryable<T> GetAll(){
            return _set;
        }

        public T GetById(int id){
            return _set.Find(id);
        }

        public void Add(T entity){
            _set.Add(entity);
        }

        public void Remove(T entity){
            _set.Remove(entity);
        }

        public void SaveChanges(){
            _context.SaveChanges();
        }
    }
}