using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Warsztat
{
    public partial class Setting_Form : Form
    {
        Settings Settings = new Settings();

        public Setting_Form()
        {
            InitializeComponent();
        }

        private void PL_Click(object sender, EventArgs e)
        {
            Settings.SaveXml(this, "PL");
        }

        private void UA_Click(object sender, EventArgs e)
        {
            Settings.SaveXml(this, "UA");
        }
    }
}
