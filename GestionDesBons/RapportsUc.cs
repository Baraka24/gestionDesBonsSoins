using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestionDesBons
{
    public partial class RapportsUc : UserControl
    {
        public RapportsUc()
        {
            InitializeComponent();
        }

        private void btnAccueil_Click(object sender, EventArgs e)
        {
            FrmBons frm = new FrmBons();
            frm.ShowDialog();
        }

        private void btnRapportDates_Click(object sender, EventArgs e)
        {
            FrmRapportsParDates frm = new FrmRapportsParDates();
            frm.ShowDialog();
        }

        private void btnRStructures_Click(object sender, EventArgs e)
        {
            FrmRapportsStructures frm = new FrmRapportsStructures();
            frm.ShowDialog();
        }
    }
}
