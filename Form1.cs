using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlTypes;
using System.Collections;

namespace Contactos
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void contactosBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.contactosBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dBUNPHUDataSet);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //This line of code loads data into the 'dBUNPHUDataSet.Contactos' table. You can move, or remove it, as needed.
            // Crear una instancia de la conexión a la base de datos
            SqlConnection connection = new SqlConnection("Data Source=UNIVERSIDAD\\SQLEXPRESS;Initial Catalog=DBUNPHU;Integrated Security=True");
           
            this.contactosTableAdapter.Fill(this.dBUNPHUDataSet.Contactos);

            // Crear la consulta SQL para obtener los contactos
            string query = "SELECT id, Nombre, Apellidos, Empresa, Direccion, Relacion FROM Contactos";

            // Crear un objeto SqlDataAdapter para ejecutar la consulta y llenar un DataTable con los resultados
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            DataTable table = new DataTable();
            adapter.Fill(table);

            // Asignar el DataTable al DataGridView
            dataGridView1.DataSource = table;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Data Source=UNIVERSIDAD\\SQLEXPRESS;Initial Catalog=DBUNPHU;Integrated Security=True";

            conn.Open();

            string str = "INSERT INTO Contactos (Nombre, Apellidos, Empresa, Direccion, Relacion) VALUES (@Nombre, @Apellidos, @Empresa, @Direccion, @Relacion)";
            SqlCommand command = new SqlCommand(str);
            command.Parameters.AddWithValue("@Nombre", txtNombre.Text);
            command.Parameters.AddWithValue("@Apellidos", txtApellidos.Text);
            command.Parameters.AddWithValue("@Empresa", txtEmpresa.Text);
            command.Parameters.AddWithValue("@Direccion", txtDireccion.Text);
            command.Parameters.AddWithValue("@Relacion", txtRelacion.Text);
            command.Connection = conn;
            int numero = command.ExecuteNonQuery();
            if (numero > 0) //condicionnal
            {
                MessageBox.Show("correcto");//mensaje para confirmar que los datos fueron guardados si numero es mayor que 0.
            }
            conn.Close();//para cerrar la conexion en la base de datos
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection();//conexion a la base de datos ademas la cadena y nuestro datos de sesion.
            conn.ConnectionString = "Data Source=UNIVERSIDAD\\SQLEXPRESS;Initial Catalog=DBUNPHU;Integrated Security=True";

            conn.Open();//para abril la conexion en empezar a modificar la base de datos

            string str = "UPDATE Contactos SET  nombre=@Nombre, Apellidos=@Apellidos, Empresa=@Empresa, Direccion=@Direccion, Relacion=@Relacion where id=@id";
            SqlCommand command = new SqlCommand(str);
            command.Connection = conn;
            command.Parameters.AddWithValue("@Nombre", txtNombre.Text);
            command.Parameters.AddWithValue("@Apellidos", txtApellidos.Text);
            command.Parameters.AddWithValue("@Empresa", txtEmpresa.Text);
            command.Parameters.AddWithValue("@Direccion", txtDireccion.Text);
            command.Parameters.AddWithValue("@Relacion", txtRelacion.Text);
            command.Parameters.AddWithValue("@id",Convert.ToInt32( dataGridView1.CurrentRow.Cells[0].Value));
            command.ExecuteNonQuery();

            conn.Close();
        }
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(); //conexion a la base de datos ademas la cadena y nuestro datos de sesion.
            conn.ConnectionString = "Data Source=UNIVERSIDAD\\SQLEXPRESS;Initial Catalog=DBUNPHU;Integrated Security=True";

            conn.Open();

            string str = "DELETE FROM Contactos WHERE id = @id";
            SqlCommand command = new SqlCommand(str);
            command.Parameters.AddWithValue("@id", Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value));
            command.Connection = conn;
            command.ExecuteNonQuery();

            conn.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Data Source=UNIVERSIDAD\\SQLEXPRESS;Initial Catalog=DBUNPHU;Integrated Security=True";

            conn.Open();//para abril la conexion en empezar a modificar la base de datos

            txtNombre.Text = "";
            txtApellidos.Text = "";
            txtEmpresa.Text = "";
            txtDireccion.Text = "";
            txtRelacion.Text = "";
            string str = "SELECT * FROM Contactos";
            SqlCommand command = new SqlCommand(str);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);

            // Asignar el DataTable al DataGridView
            dataGridView1.DataSource = table;
            command.Connection = conn;
            command.ExecuteNonQuery();

            conn.Close(); //para cerrar la conexion en la base de datos
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            txtNombre.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
        }
    }
}
