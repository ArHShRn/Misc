using System;
using System.Windows.Forms;
using System.Threading;

namespace ArLib.ARConsole
{
    /// <summary>
    /// A GUI style logger.
    /// </summary>
    partial class GuiConsole : Form
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

        private bool bArial
        {
            get
            {
                return arialToolStripMenuItem.Checked;
            }
            set
            {
                arialToolStripMenuItem.Checked = value;
            }
        }
        private bool bMSGothic
        {
            get
            {
                return mSGothixToolStripMenuItem.Checked;
            }
            set
            {
                mSGothixToolStripMenuItem.Checked = value;
            }
        }
        private bool bSmallFont
        {
            get
            {
                return smallFontSize925ptToolStripMenuItem.Checked;
            }
            set
            {
                smallFontSize925ptToolStripMenuItem.Checked = value;
            }
        }
        private bool bNormalFont
        {
            get
            {
                return normalFontSize10ptToolStripMenuItem.Checked;
            }
            set
            {
                normalFontSize10ptToolStripMenuItem.Checked = value;
            }
        }
        private bool bBigFont
        {
            get
            {
                return bigFontSize12ptToolStripMenuItem.Checked;
            }
            set
            {
                bigFontSize12ptToolStripMenuItem.Checked = value;
            }
        }

        /// <summary>
        /// Construct a GUI logger with a customized title.
        /// </summary>
        /// <param name="customPrefix">set your willing prefix.</param>
        /// <param name="title">The form title you want.</param>
        public GuiConsole(string customPrefix, string title = "ArHShRn GUI Logger")
        {
            InitializeComponent();

            Prefix = customPrefix;

            this.Text = title;
            tbLogs.WordWrap = true;
            btWordWarp.Checked = true;

            //Font
            tbLogs.Font = new System.Drawing.Font("Arial", 10.0f);
            bArial = true;
            bNormalFont = true;
        }

        private void GuiConsole_Load(object sender, EventArgs e)
        {
            tbLogs.Clear();
            AppenLine(UserDefinedStr.sAuthor);
            AppenLine(UserDefinedStr.sHelperCpr);
            AppenLine(UserDefinedStr.sLibCpr);
            AppenLine("");
            AddLog("GUI Logger Initialized.");
            AddLog("You can change to WordWarp mode in'View'.");
            AddLog("You can change the font style in 'View'.");
        }

        /// <summary>
        /// Add a log into textbox
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="bAllowTracing"></param>
        public void AddLog(string msg, bool bAllowTracing = true)
        {
            string appendMsg = Prefix.Trim() + "[" + DateTime.Now.ToString("HH:mm:ss") + "] " + msg + Environment.NewLine;
            tbLogs.AppendText(appendMsg);

            if (bAllowTracing)
            {
                tbLogs.SelectionStart = tbLogs.TextLength;
                tbLogs.ScrollToCaret();
            }
        }

        private void AppenLine(string msg)
        {
            tbLogs.Text = tbLogs.Text + msg + Environment.NewLine;
            return;
        }

        private void btDispose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you really want to dispose GUI console?" + Environment.NewLine +
                "In order to keep the application running," + Environment.NewLine +
                "A new CUI console will be created to record the left logs."
                , "Confirmation",
                MessageBoxButtons.OKCancel)
                == DialogResult.OK)
                Dispose();
            else
                return;
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
            AppenLine(UserDefinedStr.sAuthor);
            AppenLine(UserDefinedStr.sHelperCpr);
            AppenLine(UserDefinedStr.sLibCpr);
            AppenLine("");
            AddLog("You can change to WordWarp mode in'View'.");
            AddLog("You can change the font style in 'View'.");
        }

        private void btWordWarp_Click(object sender, EventArgs e)
        {
            btWordWarp.Checked = !btWordWarp.Checked;
            tbLogs.WordWrap = !tbLogs.WordWrap;

            tbLogs.SelectionStart = tbLogs.TextLength;
            tbLogs.ScrollToCaret();
        }

        private void mSGothixToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (bMSGothic) return;
            tbLogs.Font = new System.Drawing.Font("MS Gothic", tbLogs.Font.Size);

            bArial = !bArial;
            bMSGothic = !bMSGothic;
        }

        private void arialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (bArial) return;
            tbLogs.Font = new System.Drawing.Font("Arial", tbLogs.Font.Size);

            bArial = !bArial;
            bMSGothic = !bMSGothic;
        }

        private void smallFontSize925ptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (bSmallFont) return;
            tbLogs.Font = new System.Drawing.Font(tbLogs.Font.FontFamily, 9.25f);

            bSmallFont = true;
            bNormalFont = false;
            bBigFont = false;
        }

        private void normalFontSize10ptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (bNormalFont) return;
            tbLogs.Font = new System.Drawing.Font(tbLogs.Font.FontFamily, 10.0f);

            bSmallFont = false;
            bNormalFont = true;
            bBigFont = false;
        }

        private void bigFontSize12ptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (bBigFont) return;
            tbLogs.Font = new System.Drawing.Font(tbLogs.Font.FontFamily, 12f);

            bSmallFont = false;
            bNormalFont = false;
            bBigFont = true;
        }
    }
}
