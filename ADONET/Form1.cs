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

namespace ADONET
{
    public partial class Form1 : Form
    {
        private SqlConnection connection;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnConectar_Click(object sender, EventArgs e)
        {
            try
            {
                connection = new SqlConnection("Server=DESKTOP-F970KVM; Database=School; Integrated Security=True");
                connection.Open();
                MessageBox.Show("Se abrió una conexión");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
          

        }

        private void btnDesconectar_Click(object sender, EventArgs e)
        {
            connection.Close();
            MessageBox.Show("Se cerró una conexión");

        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            try
            {
                Listar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void Listar()
        {
            SqlCommand command = new SqlCommand("select * from People", connection);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            dgvPeople.DataSource = dataTable;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand command = new SqlCommand("select count(1) from People", connection);
                int result = (int) command.ExecuteScalar();
                lblCantidad.Text =Convert.ToString( result);                                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand command = new SqlCommand(
                    "insert into People values ('"+ txtNombre.Text +"','"+ txtApellido.Text +"')", connection);
                command.ExecuteNonQuery();
                Listar();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAgregrProcedimiento_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand command = new SqlCommand("usp_InsPeople", connection);
                command.CommandType = CommandType.StoredProcedure;

                SqlParameter parameter = new SqlParameter();
                parameter.SqlDbType = SqlDbType.VarChar;
                parameter.ParameterName = "@FirstName";
                parameter.Value = txtNombre.Text;

                SqlParameter parameter2 = new SqlParameter();
                parameter2.SqlDbType = SqlDbType.VarChar;
                parameter2.ParameterName = "@LastName";
                parameter2.Value = txtApellido.Text;

                command.Parameters.Add(parameter);
                command.Parameters.Add(parameter2);

                command.ExecuteNonQuery();
                Listar();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
