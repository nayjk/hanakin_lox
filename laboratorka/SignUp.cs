using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace laboratorka
{
    public partial class SignUp : Form
    {

        DataBase dataBase = new DataBase();

        public SignUp()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void SignUp_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (checkUser())
            {
                return;
            }

            var login = textBox1.Text;
            var password = md5.hashPassword(textBox2.Text);

            string querystring = $"insert into accountDetails(login_user, password_user) values('{login}', '{password}')";

            SqlCommand command = new SqlCommand(querystring, dataBase.GetConnection());

            dataBase.openConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Аккаунт успешно создан", "Успешно");
                Form1 form1 = new Form1();
                this.Hide();
                form1.ShowDialog();
            }
            else
            {
                MessageBox.Show("Аккаунт не создан!");
            }
            dataBase.closeConnection();
        }

        private Boolean checkUser()
        {
            var loginUser = textBox1.Text;
            var passwordUser = textBox2.Text;
            var passwordUserConfirm = textBox3.Text;

            if(passwordUser != passwordUserConfirm)
            {
                MessageBox.Show("Пароли не совпадают");
                return true;

            }
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dataTable = new DataTable();
            string querystring = $"select id, login_user, password_user from accountDetails where login_user = '{loginUser}' and password_user = '{passwordUser}'";

            SqlCommand command = new SqlCommand(querystring, dataBase.GetConnection());

            adapter.SelectCommand = command;
            adapter.Fill(dataTable);


            if(dataTable.Rows.Count > 0)
            {
                MessageBox.Show("Пользователь уже существует!");
                return true;
            }
            else
            {
                return false; 
            }

        }

    }
}
