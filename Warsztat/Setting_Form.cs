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

        private void button1_Click(object sender, EventArgs e)
        {
            if (Ukranian.Checked == true)
            {
                Settings.SaveXml(this, "Ukranian");
            }
            else if (Polish.Checked == true)
            {
                Settings.SaveXml(this, "Polish");
            }
        }

        private void Setting_Form_Load_1(object sender, EventArgs e)
        {
            Settings.ReadXML(this);
        }
    }
}