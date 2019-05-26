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
            this.gbSplits = new System.Windows.Forms.GroupBox();
            this.flpSplits = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAdd = new System.Windows.Forms.Button();
            this.chkEnable = new System.Windows.Forms.CheckBox();
            this.ofdSplitsFile = new System.Windows.Forms.OpenFileDialog();
            this.sfdSplitsFile = new System.Windows.Forms.SaveFileDialog();
            panel1 = new System.Windows.Forms.Panel();
            panel1.SuspendLayout();
            this.gbSplits.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(this.gbSplits);
            panel1.Controls.Add(this.chkEnable);
            panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            panel1.Location = new System.Drawing.Point(0, 0);
            panel1.Name = "panel1";
            panel1.Padding = new System.Windows.Forms.Padding(3);
            panel1.Size = new System.Drawing.Size(450, 300);
            panel1.TabIndex = 1;
            // 
            // gbSplits
            // 
            this.gbSplits.Controls.Add(this.flpSplits);
            this.gbSplits.Controls.Add(this.btnAdd);
            this.gbSplits.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbSplits.Location = new System.Drawing.Point(3, 26);
            this.gbSplits.Name = "gbSplits";
            this.gbSplits.Size = new System.Drawing.Size(444, 271);
            this.gbSplits.TabIndex = 4;
            this.gbSplits.TabStop = false;
            this.gbSplits.Text = "Splits";
            // 
            // flpSplits
            // 
            this.flpSplits.AutoScroll = true;
            this.flpSplits.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpSplits.Location = new System.Drawing.Point(9, 19);
            this.flpSplits.Name = "flpSplits";
            this.flpSplits.Size = new System.Drawing.Size(429, 217);
            this.flpSplits.TabIndex = 5;
            this.flpSplits.WrapContents = false;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(9, 242);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(45, 23);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            // 
            // chkEnable
            // 
            this.chkEnable.AutoSize = true;
            this.chkEnable.Dock = System.Windows.Forms.DockStyle.Top;
            this.chkEnable.Location = new System.Drawing.Point(3, 3);
            this.chkEnable.Name = "chkEnable";
            this.chkEnable.Padding = new System.Windows.Forms.Padding(3);
            this.chkEnable.Size = new System.Drawing.Size(444, 23);
            this.chkEnable.TabIndex = 0;
            this.chkEnable.Text = "enable";
            this.chkEnable.UseVisualStyleBackColor = true;
            // 
            // ofdSplitsFile
            // 
            this.ofdSplitsFile.Filter = "Splits Files (*.lss)|*.lss";
            // 
            // sfdSplitsFile
            // 
            this.sfdSplitsFile.AddExtension = false;
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
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox chkEnable;
        private System.Windows.Forms.OpenFileDialog ofdSplitsFile;
        private System.Windows.Forms.GroupBox gbSplits;
        private System.Windows.Forms.FlowLayoutPanel flpSplits;
        private System.Windows.Forms.SaveFileDialog sfdSplitsFile;
        private System.Windows.Forms.Button btnAdd;
    }
}
