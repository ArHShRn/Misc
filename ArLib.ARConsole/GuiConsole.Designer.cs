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
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btNewLog = new System.Windows.Forms.ToolStripMenuItem();
            this.btSaveLog = new System.Windows.Forms.ToolStripMenuItem();
            this.selectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btToTop = new System.Windows.Forms.ToolStripMenuItem();
            this.btToButtom = new System.Windows.Forms.ToolStripMenuItem();
            this.btDispose = new System.Windows.Forms.ToolStripMenuItem();
            this.stripMenu = new System.Windows.Forms.MenuStrip();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btWordWarp = new System.Windows.Forms.ToolStripMenuItem();
            this.viewSeperator = new System.Windows.Forms.ToolStripSeparator();
            this.tbLogs = new System.Windows.Forms.RichTextBox();
            this.stripMenu.SuspendLayout();
            this.SuspendLayout();
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
            this.btNewLog.Size = new System.Drawing.Size(130, 22);
            this.btNewLog.Text = "New Log";
            this.btNewLog.Click += new System.EventHandler(this.btNewLog_Click);
            // 
            // btSaveLog
            // 
            this.btSaveLog.Enabled = false;
            this.btSaveLog.Name = "btSaveLog";
            this.btSaveLog.Size = new System.Drawing.Size(130, 22);
            this.btSaveLog.Text = "Save Log...";
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
            this.btToTop.Size = new System.Drawing.Size(130, 22);
            this.btToTop.Text = "To top";
            this.btToTop.Click += new System.EventHandler(this.btToTop_Click);
            // 
            // btToButtom
            // 
            this.btToButtom.Name = "btToButtom";
            this.btToButtom.Size = new System.Drawing.Size(130, 22);
            this.btToButtom.Text = "To buttom";
            this.btToButtom.Click += new System.EventHandler(this.btToButtom_Click);
            // 
            // btDispose
            // 
            this.btDispose.Name = "btDispose";
            this.btDispose.Size = new System.Drawing.Size(60, 20);
            this.btDispose.Text = "Dispose";
            this.btDispose.Click += new System.EventHandler(this.btDispose_Click);
            // 
            // stripMenu
            // 
            this.stripMenu.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.stripMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.selectionToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.btDispose});
            this.stripMenu.Location = new System.Drawing.Point(0, 0);
            this.stripMenu.Name = "stripMenu";
            this.stripMenu.Size = new System.Drawing.Size(884, 24);
            this.stripMenu.TabIndex = 1;
            this.stripMenu.Text = "menuStrip1";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btWordWarp,
            this.viewSeperator});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // btWordWarp
            // 
            this.btWordWarp.Name = "btWordWarp";
            this.btWordWarp.Size = new System.Drawing.Size(180, 22);
            this.btWordWarp.Text = "Word Warp";
            this.btWordWarp.Click += new System.EventHandler(this.btWordWarp_Click);
            // 
            // viewSeperator
            // 
            this.viewSeperator.Name = "viewSeperator";
            this.viewSeperator.Size = new System.Drawing.Size(177, 6);
            // 
            // tbLogs
            // 
            this.tbLogs.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbLogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbLogs.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbLogs.Location = new System.Drawing.Point(0, 24);
            this.tbLogs.Name = "tbLogs";
            this.tbLogs.Size = new System.Drawing.Size(884, 437);
            this.tbLogs.TabIndex = 2;
            this.tbLogs.Text = "";
            // 
            // GuiConsole
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(884, 461);
            this.Controls.Add(this.tbLogs);
            this.Controls.Add(this.stripMenu);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(20, 20);
            this.MainMenuStrip = this.stripMenu;
            this.Name = "GuiConsole";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gui Logger";
            this.stripMenu.ResumeLayout(false);
            this.stripMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem btDispose;
        private System.Windows.Forms.MenuStrip stripMenu;
        private System.Windows.Forms.ToolStripMenuItem btNewLog;
        private System.Windows.Forms.ToolStripMenuItem btSaveLog;
        private System.Windows.Forms.ToolStripMenuItem btToTop;
        private System.Windows.Forms.ToolStripMenuItem btToButtom;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem btWordWarp;
        private System.Windows.Forms.RichTextBox tbLogs;
        private System.Windows.Forms.ToolStripSeparator viewSeperator;
    }
}