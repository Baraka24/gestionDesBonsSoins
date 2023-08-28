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
    public partial class StructuresEtFacturesUc : UserControl
    {
        public StructuresEtFacturesUc()
        {
            InitializeComponent();
        }
        List<string> structures = new List<string>();
        List<string> nom = new List<string>();
        List<string> postnom = new List<string>();
        private void chargercombobox()
        {
            MySqlConnection con = new MySqlConnection();
            con.ConnectionString = "server=localhost;database=bonsoin;uid=root;pwd=''";
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select bon.numerobon,agent.nom,agent.postnom,structure.description,bon.datebon from agent,structure,bon where agent.id=bon.idagent and bon.structure=structure.codestructure";
            con.Open();
            MySqlDataAdapter adap = new MySqlDataAdapter();
            DataTable dt = new DataTable();
            adap.SelectCommand = cmd;
            adap.Fill(dt);
            foreach (DataRow item in dt.Rows)
            {
                cbxNumBon.Items.Add(item[0]);
                nom.Add(item[1].ToString());
                postnom.Add(item[2].ToString());
                structures.Add(item[3].ToString());
            }
        }

        private void StructuresEtFacturesUc_Load(object sender, EventArgs e)
        {
            chargercombobox();
            AfficherStructures();
            AfficherFactures();
        }

        private bool ChampsOk()
        {
            if (txtCode.Text != "" && txtdesc.Text != "")
            {
                return true;
            }
            return false;
        }
        private void NotifierErreur()
        {
            if (txtdesc.Text == "")
            {
                errorProvider1.SetError(txtdesc, "Entrer la description svp!");
            }
            else
            {
                errorProvider1.SetError(txtdesc, "");
            }
            if (txtCode.Text == "")
            {
                errorProvider1.SetError(txtCode, "Entrer le code svp!");
            }
            else
            {
                errorProvider1.SetError(txtCode, "");
            }
        }
        private void btnAjouterStruc_Click(object sender, EventArgs e)
        {
            if (!ChampsOk())
            {
                NotifierErreur();
                return;
            }
            MySqlConnection con = new MySqlConnection();
            con.ConnectionString = "server=localhost;database=bonsoin;uid=root;pwd=''";
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            if (modifierS == false)
            {
                cmd.CommandText = "INSERT INTO structure(codestructure, description) VALUES (@1, @2)";
            }
            else
            {
                cmd.CommandText = "UPDATE `structure` SET `description`=@2 WHERE codestructure=@1";
            }
            cmd.Parameters.Add("1", MySqlDbType.VarChar).Value = txtCode.Text;
            cmd.Parameters.Add("2", MySqlDbType.VarChar).Value = txtdesc.Text;
            con.Open();
            if (cmd.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Enrgistrement effectué avec succès");
                AfficherStructures();
                ViderStructures();
            }
            else
            {
                MessageBox.Show("Enrgistrement echoué");
            }
        }
        private void ViderStructures()
        {
            txtdesc.Text = "";
            txtCode.Clear();
        }
        private void AfficherStructures()
        {
            MySqlConnection con = new MySqlConnection();
            con.ConnectionString = "server=localhost;database=bonsoin;uid=root;pwd=''";
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from structure";
            con.Open();
            MySqlDataAdapter adap = new MySqlDataAdapter();
            DataTable dt = new DataTable();
            adap.SelectCommand = cmd;
            adap.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void btnAjouterCat_Click(object sender, EventArgs e)
        {

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = "server=localhost;database=bonsoin;uid=root;pwd=";
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM structure WHERE description LIKE @desc";
            cmd.Parameters.Add("@desc", MySqlDbType.VarChar).Value = txtSearch.Text + "%";
            cmd.Connection = conn;
            conn.Open();
            MySqlDataAdapter tuyau = new MySqlDataAdapter();
            DataTable table = new DataTable();
            tuyau.SelectCommand = cmd;
            tuyau.Fill(table);
            dataGridView1.DataSource = table;
        }

        private void txtdesc_TextChanged(object sender, EventArgs e)
        {
            
            errorProvider1.SetError(txtdesc, "");
            //try
            //{
            //    float mtt = float.Parse(txtTotalPlavuma.Text.ToString());
            //    if (txtTotalPlavuma.Text == "")
            //    {
            //        txtMm.Text = "0";
            //        txtMp.Text = "0";
            //    }
            //    float mMusosa = (mtt * 80) / 100;
            //    txtMm.Text = mMusosa.ToString();
            //    float mPvm = (mtt * 20) / 100;
            //    txtMp.Text = mPvm.ToString();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            //finally
            //{
                
            //}   
           
        }

        private void txtCode_TextChanged(object sender, EventArgs e)
        {
            errorProvider2.SetError(txtNumeroFac, "");
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = "server=localhost;database=bonsoin;uid=root;pwd=";
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM facture WHERE numero LIKE @num";
            cmd.Parameters.Add("num", MySqlDbType.VarChar).Value = txtSearchFac.Text + "%";
            cmd.Connection = conn;
            conn.Open();
            MySqlDataAdapter tuyau = new MySqlDataAdapter();
            DataTable table = new DataTable();
            tuyau.SelectCommand = cmd;
            tuyau.Fill(table);
            dataGridView2.DataSource = table;
            errorProvider1.SetError(txtCode, "");
        }
        private void AfficherFactures()
        {
            MySqlConnection con = new MySqlConnection();
            con.ConnectionString = "server=localhost;database=bonsoin;uid=root;pwd=''";
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from facture";
            con.Open();
            MySqlDataAdapter adap = new MySqlDataAdapter();
            DataTable dt = new DataTable();
            adap.SelectCommand = cmd;
            adap.Fill(dt);
            dataGridView2.DataSource = dt;
        }
        bool modifierS = false;
        private void modifierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            modifierS = true;
            txtCode.Text = dataGridView1.CurrentRow.Cells["codestructure"].Value.ToString();
            txtdesc.Text = dataGridView1.CurrentRow.Cells["description"].Value.ToString();
            txtCode.ReadOnly = true;

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
            cmd.Connection = con;
            cmd.CommandText = "DELETE FROM `structure` WHERE codestructure=@1";
            cmd.Parameters.Add("1", MySqlDbType.VarChar).Value = dataGridView1.CurrentRow.Cells["codestructure"].Value.ToString();
            con.Open();
            cmd.ExecuteNonQuery();
            MessageBox.Show("Bien supprimé", "Info", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            AfficherStructures();
            con.Close();
        }
        bool modifierF=false;
        private void modifierToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            modifierF = true;
            cbxNumBon.Text = dataGridView2.CurrentRow.Cells["numerobon"].Value.ToString();
            txtMusosa.Text = dataGridView2.CurrentRow.Cells["montantmusosa"].Value.ToString();
            txtPlavuma.Text = dataGridView2.CurrentRow.Cells["montantpvm"].Value.ToString();
            txtNumeroFac.Text = dataGridView2.CurrentRow.Cells["numero"].Value.ToString();
            txtNumeroFac.ReadOnly = true;
            txtTotal.Text = dataGridView2.CurrentRow.Cells["montantTotal"].Value.ToString();
            txtTPlavuma.Text = dataGridView2.CurrentRow.Cells["montantTotal(Plavuma)"].Value.ToString();
            txtNonMusosa.Text = dataGridView2.CurrentRow.Cells["montant(nonparmusosa)"].Value.ToString();
        }
        private void ViderFactures()
        {
            //cbxNumBon.SelectedIndex = -1;
            txtNumeroFac.Clear();
            txtNonMusosa.Clear();
            txtMusosa.Clear();
            txtTPlavuma.Clear();
            txtTotal.Clear();
            txtNonMusosa.Text = "";
            txtPlavuma.Clear();
            lbNom.Text = "";
            lblPostnom.Text = "";
            lblStructure.Text = "";
        }
        double montant;
        private bool ChampsOkS()
        {
            if (double.TryParse(txtMusosa.Text, out montant) && double.TryParse(txtNonMusosa.Text, out montant) && double.TryParse(txtNonMusosa.Text, out montant) && double.TryParse(txtTPlavuma.Text, out montant) && double.TryParse(txtTotal.Text, out montant) && cbxNumBon.SelectedIndex != -1 && txtNumeroFac.Text != "" && double.TryParse(txtPlavuma.Text, out montant))
            {
                return true;
            }
            return false;
        }
        private void NotifierErreurS()
        {
            if (txtMusosa.Text != double.TryParse(txtMusosa.Text, out montant).ToString())
            {
                errorProvider2.SetError(txtMusosa, "Entrer un montant correct!");
            }
            else
            {
                errorProvider2.SetError(txtMusosa, "");
            }
            if (txtNonMusosa.Text != double.TryParse(txtNonMusosa.Text, out montant).ToString())
            {
                errorProvider2.SetError(txtNonMusosa, "Entrer un montant correct!");
            }
            else
            {
                errorProvider2.SetError(txtNonMusosa, "");
            }
            if (txtNonMusosa.Text != double.TryParse(txtNonMusosa.Text, out montant).ToString())
            {
                errorProvider2.SetError(txtNonMusosa, "Entrer un montant correct!");
            }
            else
            {
                errorProvider2.SetError(txtNonMusosa, "");
                //float tP, nM, pvm;
                
                //pvm = float.Parse(txtPlavuma.Text.ToString());
                //nM = float.Parse(txtNonMusosa.Text.ToString());
                //tP = pvm + nM;
                //txtTPlavuma.Text = tP.ToString();
                //txtTPlavuma.Text = tP.ToString();
            }
            if (txtTPlavuma.Text != double.TryParse(txtTPlavuma.Text, out montant).ToString())
            {
                errorProvider2.SetError(txtTPlavuma, "Montant invalide!");
            }
            else
            {
                errorProvider2.SetError(txtTPlavuma, "");
            }
            if (txtTotal.Text != double.TryParse(txtTotal.Text, out montant).ToString())
            {
                errorProvider2.SetError(txtTotal, "Montant invalide!");
            }
            else
            {
                errorProvider2.SetError(txtTotal, "");
            }
            if (txtNumeroFac.Text=="")
            {
                errorProvider2.SetError(txtNumeroFac, "Entrer le numero de la facture svp!");
            }
            else
            {
                errorProvider2.SetError(txtNumeroFac, "");
            }
            if (cbxNumBon.SelectedIndex==-1)
            {
                errorProvider2.SetError(cbxNumBon, "Selectionner un numero de bon svp!");
            }
            else
            {
                errorProvider2.SetError(cbxNumBon, "");
            }
            if (txtPlavuma.Text != double.TryParse(txtPlavuma.Text, out montant).ToString())
            {
                errorProvider2.SetError(txtPlavuma, "Entrer un montant svp!");
            }
            else
            {
                errorProvider2.SetError(txtPlavuma, "");
            }
        }
        private void btnAjouterFac_Click(object sender, EventArgs e)
        {
            if (!ChampsOkS())
            {
                NotifierErreurS();
                return;
            }
            MySqlConnection con = new MySqlConnection();
            con.ConnectionString = "server=localhost;database=bonsoin;uid=root;pwd=''";
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            if (modifierF == false)
            {
                cmd.CommandText = "INSERT INTO `facture`(`numero`, `numerobon`, `montantpvm`, `montantnonparmusosa`, `montantmusosa`, `montantTotalPlavuma`, `montantTotal`) VALUES (@1,@2,@3,@4,@5,@6,@7)";
            }
            else
            {
                cmd.CommandText = "UPDATE `facture` SET `numerobon`=@2,`montantpvm`=@3,`montant(nonparmusosa)`=@4,`montantmusosa`=@5,`montantTotal(Plavuma)`=@6,`montantTotal`=@7 WHERE `numero`=@1";
            }
            cmd.Parameters.Add("1", MySqlDbType.VarChar).Value = txtNumeroFac.Text;
            cmd.Parameters.Add("2", MySqlDbType.Int32).Value = cbxNumBon.Text;
            cmd.Parameters.Add("3", MySqlDbType.Float).Value = txtPlavuma.Text;
            cmd.Parameters.Add("4", MySqlDbType.Float).Value = txtNonMusosa.Text;
            cmd.Parameters.Add("5", MySqlDbType.Float).Value = txtMusosa.Text;
            cmd.Parameters.Add("6", MySqlDbType.Float).Value = txtTPlavuma.Text;
            cmd.Parameters.Add("7", MySqlDbType.Float).Value = txtTotal.Text;
            con.Open();
            if (cmd.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Enrgistrement effectué avec succès");
                AfficherFactures();
                ViderFactures();
            }
            else
            {
                MessageBox.Show("Enrgistrement echoué");
            }
        }

        private void supprimerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DialogResult reponse = MessageBox.Show("Voulez-vous vraiment supprimer?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (reponse == DialogResult.No)
            {
                return;
            }
            MySqlConnection con = new MySqlConnection();
            con.ConnectionString = "server=localhost;database=bonsoin;uid=root;pwd=''";
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "DELETE FROM `facture` WHERE `numero`=@1";
            cmd.Parameters.Add("1", MySqlDbType.VarChar).Value = dataGridView2.CurrentRow.Cells["numero"].Value.ToString();
            con.Open();
            cmd.ExecuteNonQuery();
            MessageBox.Show("Bien supprimé", "Info", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            AfficherFactures();
            con.Close();
        }

        private void txtMTotal_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void cbxNumBon_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            lbNom.Text = nom[cbxNumBon.SelectedIndex];
            lblPostnom.Text = postnom[cbxNumBon.SelectedIndex];
            lblStructure.Text = structures[cbxNumBon.SelectedIndex];
            errorProvider2.SetError(cbxNumBon, "");
        }
        
        private void txtNonMusosa_TextChanged(object sender, EventArgs e)
        {
            errorProvider2.SetError(txtNonMusosa, "");
            try
            {
                float tP, nM, pvm;
                pvm = float.Parse(txtPlavuma.Text.ToString());
                nM = float.Parse(txtNonMusosa.Text.ToString());
                tP = pvm + nM;
                txtTPlavuma.Text = tP.ToString();
                txtTPlavuma.Text = tP.ToString();
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void txtPlavuma_TextChanged(object sender, EventArgs e)
        {
            errorProvider2.SetError(txtPlavuma, "");
            //try
            //{
            //    float tP, nM, pvm;
            //    pvm = float.Parse(txtPlavuma.Text.ToString());
            //    nM = float.Parse(txtNonMusosa.Text.ToString());
            //    tP = pvm + nM;
            //    txtTPlavuma.Text = tP.ToString();
            //    txtTPlavuma.Text = tP.ToString();
            //}
            //catch (FormatException ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

        private void txtMusosa_TextChanged(object sender, EventArgs e)
        {
            errorProvider2.SetError(txtMusosa, "");
            try
            {
                float mT, mM, Tpvm;
                Tpvm = float.Parse(txtTPlavuma.Text.ToString());
                mM = float.Parse(txtMusosa.Text.ToString());
                mT = Tpvm + mM;
                txtTotal.Text = mT.ToString();
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtTPlavuma_TextChanged(object sender, EventArgs e)
        {
            errorProvider2.SetError(txtTPlavuma, "");
        }

        private void txtTotal_TextChanged(object sender, EventArgs e)
        {
            errorProvider2.SetError(txtTotal, "");
        }
    }
}
