using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace GestionDesBons
{
    public partial class BonUc : UserControl
    {
        public BonUc()
        {
            InitializeComponent();
        }


        bool modifier = false;

        private void viderchamps()
        {
            txtdescription.Clear();
            //cbxIdAgent.SelectedIndex = -1;
            //cbxStructures.SelectedIndex = -1;
        }
        private void afficherdatagridview()
        {
            MySqlConnection con = new MySqlConnection();
            con.ConnectionString = "server=localhost;database=bonsoin;uid=root;pwd=''";
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "select * from bon";
            DataTable td = new DataTable();
            MySqlDataAdapter adaptateur = new MySqlDataAdapter();
            adaptateur.SelectCommand = cmd;
            adaptateur.Fill(td);
            dataGridView1.DataSource = td;
        
        }

        private void BonUc_Load(object sender, EventArgs e)
        {
            ChargerIAgent();
            ChargerStructures();
            afficherdatagridview();
        }
        List<string> nom = new List<string>();
        List<string> postnom = new List<string>();
        private void ChargerIAgent()
        {
            MySqlConnection con = new MySqlConnection();
            con.ConnectionString = "server=localhost;database=bonsoin;uid=root;pwd=''";
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "select * from agent";
            DataTable td = new DataTable();
            MySqlDataAdapter adaptateur = new MySqlDataAdapter();
            adaptateur.SelectCommand = cmd;
            adaptateur.Fill(td);
            foreach (DataRow item in td.Rows)
            {
                cbxIdAgent.Items.Add(item[0]);
                nom.Add(item[1].ToString());
                postnom.Add(item[2].ToString());
            }
        }
        List<string> structures = new List<string>();
        private void ChargerStructures()
        {
            MySqlConnection con = new MySqlConnection();
            con.ConnectionString = "server=localhost;database=bonsoin;uid=root;pwd=''";
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "select * from structure";
            DataTable td = new DataTable();
            MySqlDataAdapter adaptateur = new MySqlDataAdapter();
            adaptateur.SelectCommand = cmd;
            adaptateur.Fill(td);
            foreach (DataRow item in td.Rows)
            {
                cbxStructures.Items.Add(item[0]);
                structures.Add(item[1].ToString());
            }
        }

        private void btnAjouterAff_Click(object sender, EventArgs e)
        {
            MySqlConnection con = new MySqlConnection();
            con.ConnectionString = "server=localhost;database=bonsoin;uid=root;pwd=''";
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            if (modifier==false)
            {
                cmd.CommandText = "INSERT INTO bon(datebon, structure, description, idagent) VALUES (@1, @2, @3, @4)";
                
            }
            else
            {
                cmd.CommandText = "UPDATE bon SET datebon=@1, structure=@2, description=@3, idagent=@4 WHERE numerobon=@0";
                cmd.Parameters.Add("0", MySqlDbType.VarChar).Value = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            }

            cmd.Parameters.Add("1", MySqlDbType.Date).Value = dateTimePicker1.Value;
            cmd.Parameters.Add("2", MySqlDbType.VarChar).Value = cbxStructures.Text;
            cmd.Parameters.Add("3", MySqlDbType.VarChar).Value = txtdescription.Text;
            cmd.Parameters.Add("4", MySqlDbType.VarChar).Value = cbxIdAgent.Text;
            con.Open();
            if (cmd.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Enrgistrement effectué avec succès");
                viderchamps();
                afficherdatagridview();
           
            }
            else
            {
                MessageBox.Show("Enregistrement echoué");
            }
        }

        private void txtMontant_TextChanged(object sender, EventArgs e)
        {

        }

        private void modifierToolStripMenuItem_Click(object sender, EventArgs e)
        {

            dateTimePicker1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            cbxIdAgent.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            cbxStructures.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
           txtdescription.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
           modifier = true;
        }

        private void supprimerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult reponse = MessageBox.Show("Voulez-vous vraiment supprimer?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (reponse == DialogResult.No)
            {
                return;
            }

            MySqlConnection con = new MySqlConnection();
            con.ConnectionString = "server=localhost;database=bonsoin;uid=root;pwd=''";
            MySqlCommand cmd = new MySqlCommand();
            //cmd.Connection = con;
            cmd.CommandText = "delete from bon where numerobon=@num";
            cmd.Connection = con;
            con.Open();
            cmd.Parameters.Add("num", MySqlDbType.VarChar).Value = dataGridView1.CurrentRow.Cells["numerobon"].Value.ToString();
            cmd.ExecuteNonQuery();
            MessageBox.Show("Bien supprimé", "Info", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            afficherdatagridview();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = "server=localhost;database=bonsoin;uid=root;pwd=";
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM bon WHERE idAgent LIKE @id";
            cmd.Parameters.Add("@id", MySqlDbType.VarChar).Value = txtSearch.Text + "%";
            cmd.Connection = conn;
            conn.Open();
            MySqlDataAdapter tuyau = new MySqlDataAdapter();
            DataTable table = new DataTable();
            tuyau.SelectCommand = cmd;
            tuyau.Fill(table);
            dataGridView1.DataSource = table;
        }

        private void cbxIdAgent_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbNom.Text = nom[cbxIdAgent.SelectedIndex];
            lblPostnom.Text = postnom[cbxIdAgent.SelectedIndex];
        }

        private void cbxStructures_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblStructure.Text = structures[cbxStructures.SelectedIndex];
        }
    }
}
