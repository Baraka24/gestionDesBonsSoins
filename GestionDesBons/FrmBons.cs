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
    public partial class FrmBons : Form
    {
        public FrmBons()
        {
            InitializeComponent();
        }
        private void ChargerNumBon()
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
            foreach (DataRow item in td.Rows)
            {
                cbxNumBon.Items.Add(item[0]);

            }
        }
        private void FrmBons_Load(object sender, EventArgs e)
        {
            ChargerNumBon();
            // TODO: cette ligne de code charge les données dans la table 'DataSet1.Bons'. Vous pouvez la déplacer ou la supprimer selon vos besoins.
            //this.BonsTableAdapter.Fill(this.DataSet1.Bons);

            //this.reportViewer1.RefreshReport();
        }

        private void cbxNumBon_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.BonsTableAdapter.FillBy(this.DataSet1.Bons,int.Parse(cbxNumBon.Text.ToString()));

            this.reportViewer1.RefreshReport();
        }
    }
}
