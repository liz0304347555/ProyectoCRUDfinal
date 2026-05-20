using System.Collections.Generic;
using ProyectoCRUD.Interfaces;
using ProyectoCRUD.Entidades;

namespace ProyectoCRUD.Repositories
{
    public class ClienteRepository : IRepository<Cliente>
    {
        List<Cliente> clientes = new List<Cliente>();

        public void Save(Cliente entity)
        {
            clientes.Add(entity);
        }

        public void Update(Cliente entity)
        {
            Cliente c = Search(entity.Id);

            if (c != null)
            {
                c.Nombre = entity.Nombre;
                c.Correo = entity.Correo;
            }
        }

        public void Delete(int id)
        {
            Cliente c = Search(id);

            if (c != null)
            {
                clientes.Remove(c);
            }
        }

        public Cliente Search(int id)
        {
            return clientes.Find(x => x.Id == id);
        }

        public List<Cliente> GetAll()
        {
            return clientes;
        }
    }
}