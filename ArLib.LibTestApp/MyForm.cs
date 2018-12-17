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
        private LogHelper logger;
        public MyForm()
        {
            InitializeComponent();
            logger = new LogHelper(true, "MyForm", true);
        }

        private void btCreateLog_Click(object sender, EventArgs e)
        {
            if (logger.bReleased) MessageBox.Show("Logger is released!", "SAD!");
            //logger.ExecuteCMD("NSLOOKUP -QT=A JP1.AR-DISTRIBUTED.COM 8.8.8.8");

            //logger.AsyncExecuteCMD("DIR");
            //logger.AsyncExecuteCMD("TASKLIST | FINSTR AWCC");
            logger.AsyncExecuteCMD("NSLOOKUP -QT=A JP1.AR-DISTRIBUTED.COM 8.8.8.8");
            logger.AsyncExecuteCMD("NSLOOKUP -QT=A HK1.AR-DISTRIBUTED.COM 8.8.8.8");
            logger.AsyncExecuteCMD("NSLOOKUP -QT=A RU1.AR-DISTRIBUTED.COM 8.8.8.8");
            logger.AsyncExecuteCMD("NSLOOKUP -QT=A CN1.AR-DISTRIBUTED.COM 8.8.8.8");
            logger.AsyncExecuteCMD("NSLOOKUP -QT=A CN2.AR-DISTRIBUTED.COM 8.8.8.8");
            logger.AsyncExecuteCMD("NSLOOKUP -QT=A CN3.AR-DISTRIBUTED.COM 8.8.8.8");
            logger.AsyncExecuteCMD("NSLOOKUP -QT=A CN3KF1.AR-DISTRIBUTED.COM 8.8.8.8");
            //logger.AsyncExecuteCMD("TIMEOUT -T 3");

            //logger.Log("Current thread sleeps for 1 sec...", MsgLevel.Further);

            logger.ReleaseConsole();
            //logger.Pause();
        }
    }
}
