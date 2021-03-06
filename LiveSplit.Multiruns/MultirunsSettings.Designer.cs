﻿namespace LiveSplit.Multiruns
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
            System.Windows.Forms.GroupBox gbRunInfo;
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label label2;
            this.chkAutostart = new System.Windows.Forms.CheckBox();
            this.gbSplits = new System.Windows.Forms.GroupBox();
            this.flpSplits = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAdd = new System.Windows.Forms.Button();
            this.chkEnable = new System.Windows.Forms.CheckBox();
            this.ofdSplitsFile = new System.Windows.Forms.OpenFileDialog();
            this.sfdSplitsFile = new System.Windows.Forms.SaveFileDialog();
            this.tbGame = new System.Windows.Forms.TextBox();
            this.tbCategory = new System.Windows.Forms.TextBox();
            panel1 = new System.Windows.Forms.Panel();
            gbRunInfo = new System.Windows.Forms.GroupBox();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            panel1.SuspendLayout();
            this.gbSplits.SuspendLayout();
            gbRunInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(gbRunInfo);
            panel1.Controls.Add(this.chkAutostart);
            panel1.Controls.Add(this.gbSplits);
            panel1.Controls.Add(this.chkEnable);
            panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            panel1.Location = new System.Drawing.Point(0, 0);
            panel1.Name = "panel1";
            panel1.Padding = new System.Windows.Forms.Padding(3);
            panel1.Size = new System.Drawing.Size(476, 398);
            panel1.TabIndex = 1;
            // 
            // chkAutostart
            // 
            this.chkAutostart.AutoSize = true;
            this.chkAutostart.Checked = true;
            this.chkAutostart.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutostart.Location = new System.Drawing.Point(6, 29);
            this.chkAutostart.Name = "chkAutostart";
            this.chkAutostart.Size = new System.Drawing.Size(68, 17);
            this.chkAutostart.TabIndex = 5;
            this.chkAutostart.Text = "Autostart";
            this.chkAutostart.UseVisualStyleBackColor = true;
            // 
            // gbSplits
            // 
            this.gbSplits.Controls.Add(this.flpSplits);
            this.gbSplits.Controls.Add(this.btnAdd);
            this.gbSplits.Location = new System.Drawing.Point(6, 142);
            this.gbSplits.Name = "gbSplits";
            this.gbSplits.Size = new System.Drawing.Size(464, 242);
            this.gbSplits.TabIndex = 4;
            this.gbSplits.TabStop = false;
            this.gbSplits.Text = "Splits";
            // 
            // flpSplits
            // 
            this.flpSplits.AutoScroll = true;
            this.flpSplits.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpSplits.Location = new System.Drawing.Point(6, 19);
            this.flpSplits.Name = "flpSplits";
            this.flpSplits.Size = new System.Drawing.Size(452, 188);
            this.flpSplits.TabIndex = 5;
            this.flpSplits.WrapContents = false;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(6, 213);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(45, 23);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            // 
            // chkEnable
            // 
            this.chkEnable.AutoSize = true;
            this.chkEnable.Location = new System.Drawing.Point(6, 6);
            this.chkEnable.Name = "chkEnable";
            this.chkEnable.Size = new System.Drawing.Size(59, 17);
            this.chkEnable.TabIndex = 0;
            this.chkEnable.Text = "Enable";
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
            // gbRunInfo
            // 
            gbRunInfo.AutoSize = true;
            gbRunInfo.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            gbRunInfo.Controls.Add(this.tbCategory);
            gbRunInfo.Controls.Add(label2);
            gbRunInfo.Controls.Add(this.tbGame);
            gbRunInfo.Controls.Add(label1);
            gbRunInfo.Location = new System.Drawing.Point(6, 52);
            gbRunInfo.MinimumSize = new System.Drawing.Size(464, 0);
            gbRunInfo.Name = "gbRunInfo";
            gbRunInfo.Size = new System.Drawing.Size(464, 84);
            gbRunInfo.TabIndex = 6;
            gbRunInfo.TabStop = false;
            gbRunInfo.Text = "Run Info";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(6, 22);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(35, 13);
            label1.TabIndex = 0;
            label1.Text = "Game";
            // 
            // tbGame
            // 
            this.tbGame.Location = new System.Drawing.Point(61, 19);
            this.tbGame.Name = "tbGame";
            this.tbGame.Size = new System.Drawing.Size(200, 20);
            this.tbGame.TabIndex = 0;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(6, 48);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(49, 13);
            label2.TabIndex = 8;
            label2.Text = "Category";
            // 
            // tbCategory
            // 
            this.tbCategory.Location = new System.Drawing.Point(61, 45);
            this.tbCategory.Name = "tbCategory";
            this.tbCategory.Size = new System.Drawing.Size(200, 20);
            this.tbCategory.TabIndex = 1;
            // 
            // MultirunsSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(panel1);
            this.Name = "MultirunsSettings";
            this.Size = new System.Drawing.Size(476, 398);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            this.gbSplits.ResumeLayout(false);
            gbRunInfo.ResumeLayout(false);
            gbRunInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox chkEnable;
        private System.Windows.Forms.OpenFileDialog ofdSplitsFile;
        private System.Windows.Forms.GroupBox gbSplits;
        private System.Windows.Forms.FlowLayoutPanel flpSplits;
        private System.Windows.Forms.SaveFileDialog sfdSplitsFile;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.CheckBox chkAutostart;
        private System.Windows.Forms.TextBox tbCategory;
        private System.Windows.Forms.TextBox tbGame;
    }
}
