namespace ArLib.ARConsole
{
    partial class GuiConsole
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GuiConsole));
            this.tbLogs = new System.Windows.Forms.TextBox();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btNewLog = new System.Windows.Forms.ToolStripMenuItem();
            this.btSaveLog = new System.Windows.Forms.ToolStripMenuItem();
            this.selectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btToTop = new System.Windows.Forms.ToolStripMenuItem();
            this.btToButtom = new System.Windows.Forms.ToolStripMenuItem();
            this.btDispose = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbLogs
            // 
            this.tbLogs.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.tbLogs.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbLogs.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbLogs.Cursor = System.Windows.Forms.Cursors.Default;
            this.tbLogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbLogs.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.tbLogs.Location = new System.Drawing.Point(0, 24);
            this.tbLogs.Multiline = true;
            this.tbLogs.Name = "tbLogs";
            this.tbLogs.ReadOnly = true;
            this.tbLogs.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbLogs.Size = new System.Drawing.Size(484, 259);
            this.tbLogs.TabIndex = 0;
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btNewLog,
            this.btSaveLog});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // btNewLog
            // 
            this.btNewLog.Name = "btNewLog";
            this.btNewLog.Size = new System.Drawing.Size(180, 22);
            this.btNewLog.Text = "New Log";
            this.btNewLog.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // btSaveLog
            // 
            this.btSaveLog.Enabled = false;
            this.btSaveLog.Name = "btSaveLog";
            this.btSaveLog.Size = new System.Drawing.Size(180, 22);
            this.btSaveLog.Text = "Save Log...";
            this.btSaveLog.Click += new System.EventHandler(this.saveLogToolStripMenuItem_Click);
            // 
            // selectionToolStripMenuItem
            // 
            this.selectionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btToTop,
            this.btToButtom});
            this.selectionToolStripMenuItem.Name = "selectionToolStripMenuItem";
            this.selectionToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.selectionToolStripMenuItem.Text = "Selection";
            // 
            // btToTop
            // 
            this.btToTop.Name = "btToTop";
            this.btToTop.Size = new System.Drawing.Size(180, 22);
            this.btToTop.Text = "To top";
            this.btToTop.Click += new System.EventHandler(this.topToolStripMenuItem_Click);
            // 
            // btToButtom
            // 
            this.btToButtom.Name = "btToButtom";
            this.btToButtom.Size = new System.Drawing.Size(180, 22);
            this.btToButtom.Text = "To buttom";
            this.btToButtom.Click += new System.EventHandler(this.buttomToolStripMenuItem_Click);
            // 
            // btDispose
            // 
            this.btDispose.Name = "btDispose";
            this.btDispose.Size = new System.Drawing.Size(60, 20);
            this.btDispose.Text = "Dispose";
            this.btDispose.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.selectionToolStripMenuItem,
            this.btDispose});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(484, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // GuiConsole
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(484, 283);
            this.Controls.Add(this.tbLogs);
            this.Controls.Add(this.menuStrip1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(20, 20);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "GuiConsole";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gui Logger";
            this.Load += new System.EventHandler(this.Log_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox tbLogs;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem btDispose;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem btNewLog;
        private System.Windows.Forms.ToolStripMenuItem btSaveLog;
        private System.Windows.Forms.ToolStripMenuItem btToTop;
        private System.Windows.Forms.ToolStripMenuItem btToButtom;
    }
}