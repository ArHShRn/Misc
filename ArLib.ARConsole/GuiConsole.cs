using System;
using System.Windows.Forms;

namespace ArLib.ARConsole
{
    public partial class GuiConsole : Form
    {
        /// <summary>
        /// For get/set usage.
        /// </summary>
        private string _Prefix = "";
        /// <summary>
        /// Default prefix of the log.
        /// </summary>
        private readonly string _Default_Prefix = "[GuiConsole] ";
        /// <summary>
        /// <para>Prefix of the log.</para>
        /// <para>Default prefix is "[Log] "</para>
        /// <para>The log message will like</para>
        /// <para>[Log] This is a sample log.</para>
        /// </summary>
        public string Prefix
        {
            get
            {
                if (_Prefix == "")
                    return _Default_Prefix;
                else
                    return _Prefix;
            }
            set
            {
                _Prefix = "[" + value + "] ";
            }
        }

        public GuiConsole(string title = "ArHShRn GUI Logger")
        {
            InitializeComponent();
            this.Text = title;
            AddLog("Initialized.");
        }

        public void AddLog(string msg, bool bAllowTracing = true)
        {
            tbLogs.Text = tbLogs.Text + 
                Prefix.Trim() + 
                "[" + DateTime.Now.ToString("HH:mm:ss") + "] " + 
                msg + 
                Environment.NewLine;
            Text = "Log: " + msg;

            if (bAllowTracing)
            {
                tbLogs.SelectionStart = tbLogs.TextLength;
                tbLogs.ScrollToCaret();
            }

        }

        private void ClsLogLb_Click(object sender, EventArgs e)
        {
            tbLogs.Clear();
        }

        private void Log_Load(object sender, EventArgs e)
        {
            //Implementation
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tbLogs.Text = "/////////" + "\n//TDICTIONAL TRIE LOGFRAME INSTANCE" + "\n//USAGE FOR LOGGING STATUS" + "\n////////\n";
        }

        private void topToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tbLogs.SelectionStart = 0;
            tbLogs.ScrollToCaret();
        }

        private void buttomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tbLogs.SelectionStart = tbLogs.TextLength;
            tbLogs.ScrollToCaret();
        }

        private void saveLogToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
