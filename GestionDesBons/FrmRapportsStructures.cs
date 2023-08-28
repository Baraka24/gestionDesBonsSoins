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
    public partial class FrmRapportsStructures : Form
    {
        public FrmRapportsStructures()
        {
            InitializeComponent();
        }
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
                comboBox1.Items.Add(item[0]);
                
            }
        }
        private void FrmRapportsStructures_Load(object sender, EventArgs e)
        {
            // TODO: cette ligne de code charge les données dans la table 'DataSet1.ParStructures'. Vous pouvez la déplacer ou la supprimer selon vos besoins.
            ChargerStructures();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.ParStructuresTableAdapter.FillBy(this.DataSet1.ParStructures,dateTimePicker1.Value,dateTimePicker2.Value,comboBox1.Text);

            this.reportViewer1.RefreshReport();
        }
    }
}
