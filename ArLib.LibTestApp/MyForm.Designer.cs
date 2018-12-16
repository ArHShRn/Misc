namespace ArLib.LibTestApp
{
    partial class MyForm
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
            this.btCreateLog = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btCreateLog
            // 
            this.btCreateLog.Location = new System.Drawing.Point(12, 12);
            this.btCreateLog.Name = "btCreateLog";
            this.btCreateLog.Size = new System.Drawing.Size(260, 237);
            this.btCreateLog.TabIndex = 0;
            this.btCreateLog.Text = "Click Me!";
            this.btCreateLog.UseVisualStyleBackColor = true;
            this.btCreateLog.Click += new System.EventHandler(this.btCreateLog_Click);
            // 
            // MyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.btCreateLog);
            this.Name = "MyForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MyForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btCreateLog;
    }
}