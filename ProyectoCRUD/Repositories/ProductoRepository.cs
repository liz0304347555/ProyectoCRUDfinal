using System.Collections.Generic;
using System.Data.SqlClient;
using ProyectoCRUD.Interfaces;
using ProyectoCRUD.Entidades;

namespace ProyectoCRUD.Repositories
{
    public class ProductoRepository : IRepository<Producto>
    {
        string connectionString =
            "Server=.;Database=TiendaDB;Trusted_Connection=True;";

        public void Save(Producto entity)
        {
            using (SqlConnection conn =
                new SqlConnection(connectionString))
            {
                string query =
                    "INSERT INTO Productos(Id,Nombre,Precio) VALUES(@Id,@Nombre,@Precio)";

                SqlCommand cmd =
                    new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@Id", entity.Id);
                cmd.Parameters.AddWithValue("@Nombre", entity.Nombre);
                cmd.Parameters.AddWithValue("@Precio", entity.Precio);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(Producto entity)
        {
        }

        public void Delete(int id)
        {
            using (SqlConnection conn =
                new SqlConnection(connectionString))
            {
                string query =
                    "DELETE FROM Productos WHERE Id=@Id";

                SqlCommand cmd =
                    new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@Id", id);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public Producto Search(int id)
        {
            return null;
        }

        public List<Producto> GetAll()
        {
            List<Producto> lista =
                new List<Producto>();

            using (SqlConnection conn =
                new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Productos";

                SqlCommand cmd =
                    new SqlCommand(query, conn);

                conn.Open();

                SqlDataReader reader =
                    cmd.ExecuteReader();

                while (reader.Read())
                {
                    Producto p = new Producto();

                    p.Id = (int)reader["Id"];
                    p.Nombre = reader["Nombre"].ToString();
                    p.Precio = (double)reader["Precio"];

                    lista.Add(p);
                }
            }

            return lista;
        }
    }
}