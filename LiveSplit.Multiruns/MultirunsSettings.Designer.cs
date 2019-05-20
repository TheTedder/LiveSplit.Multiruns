namespace LiveSplit.Multiruns
{
    partial class MultirunsSettings
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.Panel panel1;
            this.btnSelect = new System.Windows.Forms.Button();
            this.tbSplitsFile = new System.Windows.Forms.TextBox();
            this.chkEnable = new System.Windows.Forms.CheckBox();
            this.diaSplitsFile = new System.Windows.Forms.OpenFileDialog();
            this.gbSplits = new System.Windows.Forms.GroupBox();
            panel1 = new System.Windows.Forms.Panel();
            panel1.SuspendLayout();
            this.gbSplits.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(this.gbSplits);
            panel1.Controls.Add(this.chkEnable);
            panel1.Location = new System.Drawing.Point(3, 3);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(444, 294);
            panel1.TabIndex = 1;
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(6, 19);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(75, 23);
            this.btnSelect.TabIndex = 3;
            this.btnSelect.Text = "Select";
            this.btnSelect.UseVisualStyleBackColor = true;
            // 
            // tbSplitsFile
            // 
            this.tbSplitsFile.Location = new System.Drawing.Point(87, 21);
            this.tbSplitsFile.Name = "tbSplitsFile";
            this.tbSplitsFile.Size = new System.Drawing.Size(200, 20);
            this.tbSplitsFile.TabIndex = 2;
            // 
            // chkEnable
            // 
            this.chkEnable.AutoSize = true;
            this.chkEnable.Location = new System.Drawing.Point(3, 3);
            this.chkEnable.Name = "chkEnable";
            this.chkEnable.Size = new System.Drawing.Size(58, 17);
            this.chkEnable.TabIndex = 0;
            this.chkEnable.Text = "enable";
            this.chkEnable.UseVisualStyleBackColor = true;
            // 
            // gbSplits
            // 
            this.gbSplits.Controls.Add(this.tbSplitsFile);
            this.gbSplits.Controls.Add(this.btnSelect);
            this.gbSplits.Location = new System.Drawing.Point(3, 26);
            this.gbSplits.Name = "gbSplits";
            this.gbSplits.Size = new System.Drawing.Size(438, 265);
            this.gbSplits.TabIndex = 4;
            this.gbSplits.TabStop = false;
            this.gbSplits.Text = "Splits";
            // 
            // MultirunsSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(panel1);
            this.Name = "MultirunsSettings";
            this.Size = new System.Drawing.Size(450, 300);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            this.gbSplits.ResumeLayout(false);
            this.gbSplits.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox chkEnable;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.OpenFileDialog diaSplitsFile;
        private System.Windows.Forms.GroupBox gbSplits;
        public System.Windows.Forms.TextBox tbSplitsFile;
    }
}
