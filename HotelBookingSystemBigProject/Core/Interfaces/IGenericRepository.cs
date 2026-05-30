using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        public IEnumerable<T> GetAll();
        public T GetByID(int id);
        public void Add(T entity);
        public void Update(T entity);   
        public void Delete(T entity);
    }
}
