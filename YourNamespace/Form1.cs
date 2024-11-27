using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace YourNamespace
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            string connectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=ввв;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                  
                    SqlDataAdapter adapter = new SqlDataAdapter(@"
                SELECT 
                    e.EmployeeID, 
                    e.FirstName, 
                    e.LastName, 
                    e.Phone, 
                    e.Email, 
                    e.HireDate, 
                    r.RoleName
                FROM 
                    Employees e
                JOIN 
                    Roles r ON e.RoleID = r.RoleID", connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable; 

                    
                    dataGridView1.Columns[0].HeaderText = "ID Сотрудника"; 
                    dataGridView1.Columns[1].HeaderText = "Имя"; 
                    dataGridView1.Columns[2].HeaderText = "Фамилия"; 
                    dataGridView1.Columns[3].HeaderText = "Телефон"; 
                    dataGridView1.Columns[4].HeaderText = "Электронная почта"; 
                    dataGridView1.Columns[5].HeaderText = "Дата найма"; 
                    dataGridView1.Columns[6].HeaderText = "Роль"; 
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при загрузке данных: " + ex.Message);
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
             addemp jj = new addemp();
            jj.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            buses orderForm = new buses(); // Передаем userId в конструктор
            orderForm.Show();
        }
    }
}
