using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MineHackingTools
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new PasswordCrackingForm().ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new ExploitsAndBugsForm().ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new MalwareForm().ShowDialog();
        }
    }
}
