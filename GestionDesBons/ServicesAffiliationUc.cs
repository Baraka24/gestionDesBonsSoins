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
    public partial class ServicesAffiliationUc : UserControl
    {
        public ServicesAffiliationUc()
        {
            InitializeComponent();
        }
        MySqlConnection conn = new MySqlConnection(@"server=localhost;database=bonsoin;uid=root;pwd=;Convert Zero DateTime=true");
        MySqlCommand cmd = new MySqlCommand();
        private void ServicesAffiliationUc_Load(object sender, EventArgs e)
        {
            AfficherAffiliations();
            AfficherCategories();
            RemplirIdAgent();
        }
        private void AfficherCategories()
        {
             string requete = "select * from categorie";
             cmd = new MySqlCommand(requete, conn);
            DataTable td = new DataTable();
            MySqlDataAdapter adaptateur = new MySqlDataAdapter();
            adaptateur.SelectCommand = cmd;
            adaptateur.Fill(td);
            dataGridView1.DataSource = td;
        }
        private void AfficherAffiliations()
        {
            string requete = "SELECT * FROM `affiliation`";
            //conn.Open();
            cmd = new MySqlCommand(requete, conn);
            DataTable td = new DataTable();
            MySqlDataAdapter adaptateur = new MySqlDataAdapter();
            adaptateur.SelectCommand = cmd;
            adaptateur.Fill(td);
            dataGridView2.DataSource = td;
        }
        private bool champsOk()
        {
            if (txtCode.Text != "" && txtNom.Text != "")
            {
                return true;
            }
            return false;
        }
        private void NotifierErreur()
        {
            if (txtNom.Text == "")
            {
                errorProvider1.SetError(txtNom, "Saisissez le nom svp!");
            }
            else 
            {
                errorProvider1.SetError(txtNom, "");
            }

            if (txtCode.Text == "")
            {
                errorProvider1.SetError(txtCode, "Saisissez le code svp!");
            }
            else
            {
                errorProvider1.SetError(txtCode, "");
            }
        }
        private void btnAjouterCat_Click(object sender, EventArgs e)
        {
            if (!champsOk())
            {
                NotifierErreur();
                return;
            }
            if (modifierCat == false)
            {
                string requete = "INSERT INTO `categorie`(`codecat`, `nom`) VALUES (@codecat,@nom)";
                
                cmd = new MySqlCommand(requete, conn);
            }
            else
            {
                string requete = "Update categorie set nom=@nom where codecat=@codecat";
                //conn.Open();
                cmd = new MySqlCommand(requete, conn);
            }
            conn.Open();
            //string requete = "INSERT INTO `categorie`(`codecat`, `nom`) VALUES (@codecat,@nom)";
            //conn.Open();
            //cmd = new MySqlCommand(requete, conn);
            cmd.Parameters.Add("@nom", MySqlDbType.VarChar).Value = txtNom.Text;
            cmd.Parameters.Add("@codecat", MySqlDbType.VarChar).Value = txtCode.Text;
            cmd.ExecuteNonQuery();
            MessageBox.Show("Bien ajouté", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ViderCategoriesChamps();
            AfficherCategories();
            conn.Close();
        }
        double montant;
        private bool champsCorrects()
        {
            if (double.TryParse(txtMontant.Text, out montant) && cbxIdAgent.SelectedIndex != -1)
            {
                return true;
            }
            return false;
        }
        private void Erreur()
        {
            if (txtMontant.Text != double.TryParse(txtMontant.Text, out montant).ToString())
            {
                errorProvider2.SetError(txtMontant, "Montant invalide");
            }
            else
            {
                errorProvider2.SetError(txtMontant, "");
            }
            if (cbxIdAgent.SelectedIndex != -1)
            {
                errorProvider2.SetError(cbxIdAgent, "");
            }
            else
            {
                errorProvider2.SetError(cbxIdAgent, "Selectionner un id!");
            }
        }

        private void btnAjouterAff_Click(object sender, EventArgs e)
        {
            if (!champsCorrects())
            {
                Erreur();
                return;
            }
            if (modifierAff == true)
            {
                string requete = "UPDATE `affiliation` SET `idagent`=@idagent,`dateaffiliation`=@dateaffiliation,`montant`=@montant WHERE `idaffiliation`=@idaffiliation";
                cmd = new MySqlCommand(requete, conn);
            }
            else 
            {
                string requete = "INSERT INTO `affiliation`(`idaffiliation`, `idagent`, `dateaffiliation`, `montant`) VALUES ('',@idagent,@dateaffiliation,@montant)";
                cmd = new MySqlCommand(requete, conn);
            }  
            conn.Open();
            cmd.Parameters.Add("idaffiliation", MySqlDbType.VarChar).Value = lblIdAff.Text;
            cmd.Parameters.Add("idagent", MySqlDbType.VarChar).Value = cbxIdAgent.Text;
            cmd.Parameters.Add("@montant", MySqlDbType.VarChar).Value = txtMontant.Text;
            cmd.Parameters.Add("dateaffiliation", MySqlDbType.Date).Value = dateTimePicker1.Value;
            cmd.ExecuteNonQuery();
            MessageBox.Show("Bien ajouté", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ViderAffiliationsChamps();
            AfficherAffiliations();
            conn.Close();
        }
        List<string> nom= new List<string>();
        List<string> postnom = new List<string>();
        private void RemplirIdAgent()
        {
            string requete = "select * from agent";
            //conn.Open();
            cmd = new MySqlCommand(requete, conn);
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

        private void txtSearchCat_TextChanged(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = "server=localhost;database=bonsoin;uid=root;pwd=";
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM categorie WHERE nom LIKE @nom";
            cmd.Parameters.Add("@nom", MySqlDbType.VarChar).Value = txtSearchCat.Text + "%";
            cmd.Connection = conn;
            conn.Open();
            MySqlDataAdapter tuyau = new MySqlDataAdapter();
            DataTable table = new DataTable();
            tuyau.SelectCommand = cmd;
            tuyau.Fill(table);
            dataGridView1.DataSource = table;
        }

        private void supprimerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult reponse = MessageBox.Show("Voulez-vous vraiment supprimer?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (reponse == DialogResult.No)
            {
                return;
            }
            string requete = "delete from categorie where codecat=@codecat";
            cmd = new MySqlCommand(requete, conn);
            conn.Open();
            cmd.Parameters.Add("@codecat", MySqlDbType.VarChar).Value = dataGridView1.CurrentRow.Cells["codecat"].Value.ToString();
            cmd.ExecuteNonQuery();
            MessageBox.Show("Bien supprimé", "Info", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            AfficherCategories();
            conn.Close();
        }
        private void ViderCategoriesChamps()
        {
            txtCode.Clear();
            txtNom.Clear();
        }
        private void ViderAffiliationsChamps()
        {
            //cbxIdAgent.SelectedIndex = -1;
            txtMontant.Text = "";
            lbNom.Text = "";
            lblPostnom.Text = "";
            
        }

        private void supprimerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DialogResult reponse = MessageBox.Show("Voulez-vous vraiment supprimer?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (reponse == DialogResult.No)
            {
                return;
            }
            string requete = "delete from affiliation where idaffiliation=@id";
            cmd = new MySqlCommand(requete, conn);
            conn.Open();
            cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = dataGridView2.CurrentRow.Cells["idaffiliation"].Value.ToString();
            cmd.ExecuteNonQuery();
            MessageBox.Show("Bien supprimé", "Info", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            AfficherAffiliations();
            conn.Close();
        }
        bool modifierAff;
        private void contextMenuStrip2_Opening(object sender, CancelEventArgs e)
        {
            
        }
        bool modifierCat;
        private void modifierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            modifierCat = true;
            txtNom.Text = dataGridView1.CurrentRow.Cells["nom"].Value.ToString();
            txtCode.Text = dataGridView1.CurrentRow.Cells["codecat"].Value.ToString();
            txtCode.ReadOnly = true;
        }

        private void modifierToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            modifierAff = true;
            cbxIdAgent.Text = dataGridView2.CurrentRow.Cells["idagent"].Value.ToString();
            dateTimePicker1.Text = dataGridView2.CurrentRow.Cells["dateaffiliation"].Value.ToString();
            txtMontant.Text = dataGridView2.CurrentRow.Cells["montant"].Value.ToString();
            lblIdAff.Text = dataGridView2.CurrentRow.Cells["idaffiliation"].Value.ToString();
            lblIdAff.Visible = false;
        }

        private void txtSearchAff_TextChanged(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = "server=localhost;database=bonsoin;uid=root;pwd=";
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM affiliation WHERE idagent LIKE @id";
            cmd.Parameters.Add("@id", MySqlDbType.VarChar).Value = txtSearchAff.Text + "%";
            cmd.Connection = conn;
            conn.Open();
            MySqlDataAdapter tuyau = new MySqlDataAdapter();
            DataTable table = new DataTable();
            tuyau.SelectCommand = cmd;
            tuyau.Fill(table);
            dataGridView2.DataSource = table;
        }

        private void cbxIdAgent_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorProvider2.SetError(cbxIdAgent, "");
            lbNom.Text = nom[cbxIdAgent.SelectedIndex];
            lblPostnom.Text = postnom[cbxIdAgent.SelectedIndex];
        }

        private void txtCode_TextChanged(object sender, EventArgs e)
        {
             errorProvider1.SetError(txtCode, "");
        }

        private void txtNom_TextChanged(object sender, EventArgs e)
        {
            errorProvider1.SetError(txtNom, "");
        }

        private void txtMontant_TextChanged(object sender, EventArgs e)
        {
            errorProvider2.SetError(txtMontant, "");
        }
    }
}
