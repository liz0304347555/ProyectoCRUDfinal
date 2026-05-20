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