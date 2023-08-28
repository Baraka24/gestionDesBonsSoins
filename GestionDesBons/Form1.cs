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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            AccueilUc uc = new AccueilUc();
            //pnlAfficher.Controls.Clear();
            pnlAfficher.Controls.Add(uc);
            btnAccueil.IsTab = true;
            //btnAccueil.Activecolor = Color(0,0, 102, 204);
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            StructuresEtFacturesUc uc = new StructuresEtFacturesUc();
            pnlAfficher.Controls.Clear();
            pnlAfficher.Controls.Add(uc);
        }

        private void btnAgents_Click(object sender, EventArgs e)
        {
            AgentsUc uc = new AgentsUc();
            pnlAfficher.Controls.Clear();
            pnlAfficher.Controls.Add(uc);
        }

        private void btnAccueil_Click(object sender, EventArgs e)
        {
            AccueilUc uc = new AccueilUc();
            pnlAfficher.Controls.Clear();
            pnlAfficher.Controls.Add(uc);
        }

        private void btnServicesAffiliation_Click(object sender, EventArgs e)
        {
            ServicesAffiliationUc uc = new ServicesAffiliationUc();
            pnlAfficher.Controls.Clear();
            pnlAfficher.Controls.Add(uc);
        }

        private void btnSoins_Click(object sender, EventArgs e)
        {
            BonUc uc = new BonUc();
            pnlAfficher.Controls.Clear();
            pnlAfficher.Controls.Add(uc);
        }

        private void btnRapports_Click(object sender, EventArgs e)
        {
            RapportsUc uc = new RapportsUc();
            pnlAfficher.Controls.Clear();
            pnlAfficher.Controls.Add(uc);
        }
    }
}
