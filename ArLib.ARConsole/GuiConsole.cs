using System;
using System.Windows.Forms;

namespace ArLib.ARConsole
{
    /// <summary>
    /// A GUI style logger.
    /// </summary>
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
        private string Prefix
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

        /// <summary>
        /// Construct a GUI logger with a customized title.
        /// </summary>
        /// <param name="title">The form title you want.</param>
        public GuiConsole(string title = "ArHShRn GUI Logger")
        {
            InitializeComponent();

            this.Text = title;
            //tbLogs.WordWrap = false;
            //btWordWarp.Checked = false;

            ////Font
            //tbLogs.Font = new System.Drawing.Font("Arial", 9.25f);

            //AddLog("GUI Logger Initialized.");
        }

        /// <summary>
        /// Add a log into textbox
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="bAllowTracing"></param>
        public void AddLog(string msg, bool bAllowTracing = true)
        {
            tbLogs.Text = tbLogs.Text +
                Prefix.Trim() +
                "[" + DateTime.Now.ToString("HH:mm:ss") + "] " +
                msg +
                Environment.NewLine;

            if (bAllowTracing)
            {
                tbLogs.SelectionStart = tbLogs.TextLength;
                tbLogs.ScrollToCaret();
            }
        }

        private void btDispose_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void btToTop_Click(object sender, EventArgs e)
        {
            tbLogs.SelectionStart = 0;
            tbLogs.ScrollToCaret();
        }

        private void btToButtom_Click(object sender, EventArgs e)
        {
            tbLogs.SelectionStart = tbLogs.TextLength;
            tbLogs.ScrollToCaret();
        }

        private void btNewLog_Click(object sender, EventArgs e)
        {
            tbLogs.Clear();
        }

        private void btWordWarp_Click(object sender, EventArgs e)
        {
            btWordWarp.Checked = !btWordWarp.Checked;
            tbLogs.WordWrap = !tbLogs.WordWrap;

            tbLogs.SelectionStart = tbLogs.TextLength;
            tbLogs.ScrollToCaret();
        }
    }
}
