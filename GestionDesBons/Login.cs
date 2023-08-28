using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace GestionDesBons
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnConnexion_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = "server=localhost;database=bonsoin;uid=root;pwd=";
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.Text;
           
                cmd.CommandText = "select * from Admin where username=@username and passWord=@pwd";
                cmd.Parameters.Add("username", MySqlDbType.VarChar).Value = txtUserName.Text;
                cmd.Parameters.Add("pwd", MySqlDbType.VarChar).Value = txtPwd.Text;
                cmd.Connection = conn;
                DataTable dt = new DataTable();
                MySqlDataAdapter tuyau = new MySqlDataAdapter();
                tuyau.SelectCommand = cmd;
                tuyau.Fill(dt);
                if (dt.Rows.Count == 1)
                {
                    Form1 frm = new Form1();
                    //this.Hide();
                    frm.ShowDialog();
                }
                else
                {
                    //MessageBox.Show("Nom d'utilisateur ou Mot de passe incorrect!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    lblErreur.Text = "Nom d'utilisateur ou Mot de passe incorrect!";
                }
            
        }

        private void Login_Load(object sender, EventArgs e)
        {
            bunifuCheckbox1.Checked = false;
            if (bunifuCheckbox1.Checked)
            {
                txtPwd.isPassword = false;
            }
            else
            {
                txtPwd.isPassword = true;
            }
        }

        private void bunifuCheckbox1_OnChange(object sender, EventArgs e)
        {
            txtPwd.isPassword = false;
            //bunifuCheckbox1.Checked = true;
        }

        private void btnHide_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
