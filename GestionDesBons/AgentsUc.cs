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
using System.IO;

namespace GestionDesBons
{
    public partial class AgentsUc : UserControl
    {
        public AgentsUc()
        {
            InitializeComponent();
        }

        private void AgentsUc_Load(object sender, EventArgs e)
        {
            AfficherAgents();
            RemplirCategorie();
            lblIdAgent.Visible = false;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            errorProvider1.SetError(txtPostnom, "");
            errorProvider1.SetError(txtPrenom, "");
            errorProvider1.SetError(txtNumTel, "");
            errorProvider1.SetError(txtAdresse, "");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            errorProvider1.SetError(txtNom, "");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        MySqlConnection conn = new MySqlConnection(@"server=localhost;database=bonsoin;uid=root;pwd=");
        MySqlCommand cmd = new MySqlCommand();
        
        private void AfficherAgents()
        {
            string requete = "select * from agent";
            //conn.Open();
            cmd = new MySqlCommand(requete, conn);
            DataTable td = new DataTable();
            MySqlDataAdapter adaptateur = new MySqlDataAdapter();
            dataGridView1.RowTemplate.Height = 200;
            adaptateur.SelectCommand = cmd;
            adaptateur.Fill(td);
            dataGridView1.DataSource = td;

        }
        List<string> categories = new List<string>();
        private void RemplirCategorie()
        {
            string requete = "select * from categorie";
            //conn.Open();
            cmd = new MySqlCommand(requete, conn);
            DataTable td = new DataTable();
            MySqlDataAdapter adaptateur = new MySqlDataAdapter();
            adaptateur.SelectCommand = cmd;
            adaptateur.Fill(td);
            foreach (DataRow item in td.Rows)
            {
                cbxCategorie.Items.Add(item[0]);
                categories.Add(item[1].ToString());
            }
        }

        private void btnChoisirPhoto_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog fichier = new OpenFileDialog();
                fichier.Filter = "Choisir seulement |*.jpg;*jpeg;*png;*gif";
                if (fichier.ShowDialog() == DialogResult.OK)
                {
                    photo.Image = Image.FromFile(fichier.FileName);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           // DialogResult resultat = fichier.ShowDialog();//boite de dialogue
        }
        private void NotifierErreur()
        {
            if (txtAdresse.Text == "")
            {
                errorProvider1.SetError(txtAdresse, "Entrer l'adresse svp!");
            }
            else
            {
                errorProvider1.SetError(txtAdresse, "");
            }
            if (txtNumTel.Text== "")
            {
                errorProvider1.SetError(txtNumTel, "Entrer le numero svp!");
            }
            else
            {
                errorProvider1.SetError(txtNumTel, "");
            }
            if (txtPostnom.Text == "")
            {
                errorProvider1.SetError(txtPostnom, "Entrer le postnom svp!");
            }
            else
            {
                errorProvider1.SetError(txtPostnom, "");
            }
            if (txtPrenom.Text == "")
            {
                errorProvider1.SetError(txtPrenom, "Entrer le prenom svp!");
            }
            else
            {
                errorProvider1.SetError(txtPrenom, "");
            }
            if (txtNom.Text == "")
            {
                errorProvider1.SetError(txtNom, "Entrer le nom svp!");
            }
            else
            {
                errorProvider1.SetError(txtNom, "");
            }
            if (cbxCategorie.SelectedIndex == -1)
            {
                errorProvider1.SetError(cbxCategorie ,"Sélectionner une catégorie svp!");
            }
            else
            {
                errorProvider1.SetError(cbxCategorie, "");
            }
            if (cbxGenre.SelectedIndex == -1)
            {
                errorProvider1.SetError(cbxGenre, "Sélectionner le genre svp!");
            }
            else
            {
                errorProvider1.SetError(cbxGenre, "");
            }
            if (photo.Image == null)
            {
                errorProvider1.SetError(photo, "Choisir une image svp!");
            }
            else
            {
                errorProvider1.SetError(photo, "");
            }
        }
        private bool champOK()
        {
            if (cbxGenre.SelectedIndex != -1 && cbxCategorie.SelectedIndex != -1 && txtPostnom.Text != "" && txtNumTel.Text != "" && txtPrenom.Text != "" && txtNom.Text != "" && txtAdresse.Text != ""&&photo.Image!=null)
            {
                return true;
            }
            return false;
        }

        private void btnAjouter_Click(object sender, EventArgs e)
        {
            if (!champOK())
            {
                NotifierErreur();
                return;
            }
            MemoryStream ms = new MemoryStream();
            photo.Image.Save(ms, photo.Image.RawFormat);
            byte[] img = ms.ToArray();
            if (update == true)
            {
                string requete = "UPDATE `agent` SET `nom`=@nom,`postnom`=@postnom,`prenom`=@prenom,`numtel`=@numTel,`genre`=@genre,`adresse`=@adresse,`categorie`=@categorie,`photo`=@image WHERE `id`=@id";
                cmd = new MySqlCommand(requete, conn);
            }
            else
            {
                string requete = "INSERT INTO `agent`(`id`, `nom`, `postnom`, `prenom`, `genre`, `numtel`, `adresse`, `categorie`, `photo`) VALUES ('',@nom,@postnom,@prenom,@genre,@numTel,@adresse,@categorie,@image)";
                cmd = new MySqlCommand(requete, conn);
            }
            conn.Open();
            cmd.Parameters.Add("@id", MySqlDbType.VarChar).Value = lblIdAgent.Text;
            cmd.Parameters.Add("@postnom", MySqlDbType.VarChar).Value = txtPostnom.Text;
            cmd.Parameters.Add("@nom", MySqlDbType.VarChar).Value = txtNom.Text;
            cmd.Parameters.Add("@prenom", MySqlDbType.VarChar).Value = txtPrenom.Text;
            cmd.Parameters.Add("genre", MySqlDbType.VarChar).Value = cbxGenre.Text;
            cmd.Parameters.Add("@numTel", MySqlDbType.VarChar).Value = txtNumTel.Text;
            cmd.Parameters.Add("@categorie", MySqlDbType.VarChar).Value = cbxCategorie.Text;
            cmd.Parameters.Add("@adresse", MySqlDbType.VarChar).Value = txtAdresse.Text;
            cmd.Parameters.Add("@image", MySqlDbType.LongBlob).Value = img;
            cmd.ExecuteNonQuery();
            MessageBox.Show("Bien ajouté", "Info", MessageBoxButtons.OK,MessageBoxIcon.Information);
            ViderChamps();
            AfficherAgents();
            conn.Close();
        }
        private void ViderChamps()
        {
            txtAdresse.Text = "";
            txtNom.Text = "";
            txtPostnom.Text = "";
            txtPrenom.Text = "";
            cbxGenre.SelectedIndex = -1;
            //cbxCategorie.SelectedIndex = -1;
            txtNumTel.Text = "";
            photo.Image = null;
            lblCategorie.Text = "";
        }
        bool update = false;
        private void btnModifier_Click(object sender, EventArgs e)
        {
            update = true;
            lblIdAgent.Text = dataGridView1.CurrentRow.Cells["id"].Value.ToString();
            txtAdresse.Text=dataGridView1.CurrentRow.Cells["adresse"].Value.ToString();
            txtNom.Text = dataGridView1.CurrentRow.Cells["nom"].Value.ToString();
            txtNumTel.Text = dataGridView1.CurrentRow.Cells["numtel"].Value.ToString();
            txtPostnom.Text = dataGridView1.CurrentRow.Cells["postnom"].Value.ToString();
            txtPrenom.Text = dataGridView1.CurrentRow.Cells["prenom"].Value.ToString();
            cbxCategorie.Text = dataGridView1.CurrentRow.Cells["categorie"].Value.ToString();
            cbxGenre.Text = dataGridView1.CurrentRow.Cells["genre"].Value.ToString();
            //photo.Image=Image.FromFile(dataGridView1.CurrentRow.Cells["photo"].Value.ToString());
            var data =(Byte[])(dataGridView1.CurrentRow.Cells["photo"].Value);
            var stream = new MemoryStream(data);
            photo.Image = Image.FromStream(stream);
        }

        private void btnSupprimer_Click(object sender, EventArgs e)
        {
            DialogResult reponse = MessageBox.Show("Voulez-vous vraiment supprimer?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (reponse == DialogResult.No)
            {
                return;
            }
            string requete = "delete from agent where id=@id";
            cmd = new MySqlCommand(requete, conn);
            conn.Open();
            cmd.Parameters.Add("@id", MySqlDbType.VarChar).Value = dataGridView1.CurrentRow.Cells["id"].Value.ToString();
            cmd.ExecuteNonQuery();
            MessageBox.Show("Bien supprimé", "Info", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            AfficherAgents();
            conn.Close();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = "server=localhost;database=bonsoin;uid=root;pwd=";
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM agent WHERE nom LIKE @nom ";
            cmd.Parameters.Add("@nom", MySqlDbType.VarChar).Value = txtSearch.Text + "%";
            
            cmd.Connection = conn;
            conn.Open();
            MySqlDataAdapter tuyau = new MySqlDataAdapter();
            DataTable table = new DataTable();
            tuyau.SelectCommand = cmd;
            tuyau.Fill(table);
            dataGridView1.DataSource = table;
        }

        private void cbxCategorie_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblCategorie.Text = categories[cbxCategorie.SelectedIndex];
            errorProvider1.SetError(cbxCategorie, "");
        }

        private void photo_Click(object sender, EventArgs e)
        {
            errorProvider1.SetError(photo, "");
        }

        private void cbxGenre_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorProvider1.SetError(cbxGenre, "");
        }
    }
}
