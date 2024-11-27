using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace YourNamespace
{
    public partial class addemp : Form
    {
        private string connectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=ввв;Integrated Security=True";

        public addemp()
        {
            InitializeComponent();
            LoadRoles();
            LoadEmployees(); // Load employees when the form is initialized
        }

        private void LoadRoles()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT RoleID, RoleName FROM Roles", connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    comboBox1.Items.Add(new { RoleID = reader.GetInt32(0), RoleName = reader.GetString(1) });
                }
                comboBox1.DisplayMember = "RoleName";
                comboBox1.ValueMember = "RoleID";
            }
        }

        private void LoadEmployees()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Employees", connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable; // Bind the DataTable to the DataGridView
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            string firstName = textBox1.Text.Trim();
            string lastName = textBox2.Text.Trim();
            int roleID = (comboBox1.SelectedItem != null) ? ((dynamic)comboBox1.SelectedItem).RoleID : 0;
            string phone = textBox3.Text.Trim();
            string email = textBox4.Text.Trim();
            DateTime hireDate = DateTime.Now;

            // Check if required fields are filled
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || roleID == 0)
            {
                MessageBox.Show("Please fill in all required fields.");
                return;
            }

            // Check if role is selected
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Please select a role.");
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("INSERT INTO Employees (FirstName, LastName, RoleID, Phone, Email, HireDate) VALUES (@FirstName, @LastName, @RoleID, @Phone, @Email, @HireDate)", connection);

                    command.Parameters.AddWithValue("@FirstName", firstName);
                    command.Parameters.AddWithValue("@LastName", lastName);
                    command.Parameters.AddWithValue("@RoleID", roleID);
                    command.Parameters.AddWithValue("@Phone", phone);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@HireDate", hireDate);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Employee added successfully!");
                        ClearFields();
                        LoadEmployees(); // Refresh the DataGridView after adding a new employee
                    }
                    else
                    {
                        MessageBox.Show("Failed to add employee.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void ClearFields()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            comboBox1.SelectedIndex = -1;
        }

        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void textBox2_TextChanged(object sender, EventArgs e) { }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) { }
        private void textBox3_TextChanged(object sender, EventArgs e) { }
        private void textBox4_TextChanged(object sender, EventArgs e) { }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
    }
}
