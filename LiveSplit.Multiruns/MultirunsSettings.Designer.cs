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
            this.panel2 = new System.Windows.Forms.Panel();
            this.tbSplitsFile = new System.Windows.Forms.TextBox();
            this.btnSelect = new System.Windows.Forms.Button();
            this.chkEnable = new System.Windows.Forms.CheckBox();
            this.diaSplitsFile = new System.Windows.Forms.OpenFileDialog();
            panel1 = new System.Windows.Forms.Panel();
            panel1.SuspendLayout();
            this.gbSplits.SuspendLayout();
            this.flpSplits.SuspendLayout();
            this.panel2.SuspendLayout();
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
            // gbSplits
            // 
            this.gbSplits.AutoSize = true;
            this.gbSplits.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gbSplits.Controls.Add(this.flpSplits);
            this.gbSplits.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbSplits.Location = new System.Drawing.Point(0, 17);
            this.gbSplits.Name = "gbSplits";
            this.gbSplits.Size = new System.Drawing.Size(444, 277);
            this.gbSplits.TabIndex = 4;
            this.gbSplits.TabStop = false;
            this.gbSplits.Text = "Splits";
            // 
            // flpSplits
            // 
            this.flpSplits.AutoSize = true;
            this.flpSplits.Controls.Add(this.panel2);
            this.flpSplits.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpSplits.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpSplits.Location = new System.Drawing.Point(3, 16);
            this.flpSplits.Name = "flpSplits";
            this.flpSplits.Size = new System.Drawing.Size(438, 258);
            this.flpSplits.TabIndex = 5;
            // 
            // panel2
            // 
            this.panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel2.Controls.Add(this.tbSplitsFile);
            this.panel2.Controls.Add(this.btnSelect);
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(432, 20);
            this.panel2.TabIndex = 4;
            // 
            // tbSplitsFile
            // 
            this.tbSplitsFile.Dock = System.Windows.Forms.DockStyle.Top;
            this.tbSplitsFile.Enabled = false;
            this.tbSplitsFile.Location = new System.Drawing.Point(52, 0);
            this.tbSplitsFile.Name = "tbSplitsFile";
            this.tbSplitsFile.Size = new System.Drawing.Size(380, 20);
            this.tbSplitsFile.TabIndex = 2;
            this.tbSplitsFile.WordWrap = false;
            // 
            // btnSelect
            // 
            this.btnSelect.AutoSize = true;
            this.btnSelect.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnSelect.Location = new System.Drawing.Point(0, 0);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(52, 20);
            this.btnSelect.TabIndex = 3;
            this.btnSelect.Text = "Open...";
            this.btnSelect.UseVisualStyleBackColor = true;
            // 
            // chkEnable
            // 
            this.chkEnable.Dock = System.Windows.Forms.DockStyle.Top;
            this.chkEnable.Location = new System.Drawing.Point(0, 0);
            this.chkEnable.Name = "chkEnable";
            this.chkEnable.Size = new System.Drawing.Size(444, 17);
            this.chkEnable.TabIndex = 0;
            this.chkEnable.Text = "enable";
            this.chkEnable.UseVisualStyleBackColor = true;
            // 
            // diaSplitsFile
            // 
            this.diaSplitsFile.Filter = "Splits Files (*.lss)|*.lss";
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
            this.flpSplits.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox chkEnable;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.OpenFileDialog diaSplitsFile;
        private System.Windows.Forms.GroupBox gbSplits;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.FlowLayoutPanel flpSplits;
        private System.Windows.Forms.TextBox tbSplitsFile;
    }
}
