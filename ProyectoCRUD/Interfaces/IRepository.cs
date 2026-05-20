using System.Collections.Generic;

namespace ProyectoCRUD.Interfaces
{
    public interface IRepository<T>
    {
        void Save(T entity);

        void Update(T entity);

        void Delete(int id);

        T Search(int id);

        List<T> GetAll();
    }
}