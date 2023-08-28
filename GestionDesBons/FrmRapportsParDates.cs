using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestionDesBons
{
    public partial class FrmRapportsParDates : Form
    {
        public FrmRapportsParDates()
        {
            InitializeComponent();
        }

        private void FrmRapportsParDates_Load(object sender, EventArgs e)
        {
            // TODO: cette ligne de code charge les données dans la table 'DataSet1.RapportsDates'. Vous pouvez la déplacer ou la supprimer selon vos besoins.
            //this.RapportsDatesTableAdapter.Fill(this.DataSet1.RapportsDates);

            //this.reportViewer1.RefreshReport();
            //this.reportViewer1.RefreshReport();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            // TODO: cette ligne de code charge les données dans la table 'DataSet1.RapportsDates'. Vous pouvez la déplacer ou la supprimer selon vos besoins.
            this.RapportsDatesTableAdapter.FillBy(this.DataSet1.RapportsDates,dateTimePicker1.Value,dateTimePicker2.Value);

            this.reportViewer1.RefreshReport();
            this.reportViewer1.RefreshReport();
        }
    }
}
