Interfaces

Form
using System;
using System.Windows.Forms;
using ProyectoCRUD.Entidades;
using ProyectoCRUD.Repositories;

namespace ProyectoCRUD
{
    public partial class Form1 : Form
    {
        ProductoRepository repo = new ProductoRepository();

        public Form1()
        {
            InitializeComponent();

            MostrarProductos();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Producto p = new Producto();

            p.Id = int.Parse(txtId.Text);
            p.Nombre = txtNombre.Text;
            p.Precio = double.Parse(txtPrecio.Text);

            repo.Save(p);

            MessageBox.Show("Producto Guardado");

            MostrarProductos();
        }

        private void MostrarProductos()
        {
            dgvProductos.DataSource = null;
            dgvProductos.DataSource = repo.GetAll();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtId.Text);

            repo.Delete(id);

            MessageBox.Show("Producto Eliminado");

            MostrarProductos();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
} 
Entidades/Clientes

namespace ProyectoCRUD.Entidades
{
    public class Cliente
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Correo { get; set; }
    }
}

Entidades/Producto

namespace ProyectoCRUD.Entidades
{
    public class Producto
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public double Precio { get; set; }
    }
}

Interfaces/IRepository

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

repositories/ClienteRepository

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

repositories/ProductoRepository

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

<img width="469" height="453" alt="image" src="https://github.com/user-attachments/assets/f48531e4-5b7b-4507-b9b6-71c7fefd8136" />

<img width="461" height="438" alt="image" src="https://github.com/user-attachments/assets/25e4ff95-d759-4db2-ba9d-66e216c03ad7" />

Reflexión

¿Qué ventajas ofrecen las interfaces?
Permiten organizar mejor el código, reutilizar métodos y facilitar cambios en el sistema.

¿Por qué las interfaces ayudan a desacoplar el código?
Porque las clases trabajan usando contratos y no dependen directamente de una implementación específica.

¿Qué tan fácil fue cambiar entre implementaciones?
Fue fácil porque todas las implementaciones usan la misma interfaz y los mismos métodos.


¿Qué problemas se evitarían en proyectos grandes usando interfaces?
Se evita código repetido, dependencias fuertes y dificultad para modificar partes del sistema.

¿Por qué las interfaces son importantes en arquitecturas modernas?
Porque ayudan a crear sistemas más flexibles, modulares y fáciles de mantener.

¿Qué relación existe entre interfaces y mantenibilidad?
Las interfaces hacen que el código sea más fácil de actualizar y modificar sin afectar todo el proyecto.


¿Qué ocurriría si no existieran interfaces en este proyecto?
El código estaría más desorganizado y cambiar la lógica o la base de datos sería más difícil.

¿Qué diferencia existe entre programar contra una clase y programar contra un contrato?
Programar contra una clase significa depender directamente de una implementación específica.
Programar contra un contrato significa trabajar usando una interfaz, permitiendo cambiar implementaciones fácilmente.


