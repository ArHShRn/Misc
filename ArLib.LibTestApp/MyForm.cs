using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ArLib.ARConsole;

namespace ArLib.LibTestApp
{
    public partial class MyForm : Form
    {
        private ARConsole.ARConsole logger;
        public MyForm()
        {
            InitializeComponent();
            logger = new ARConsole.ARConsole(true, "MyForm", true);
        }

        private void btCreateLog_Click(object sender, EventArgs e)
        {
            logger.Log("test");
        }
    }
}
