using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LiveSplit.UI.Components;
using System.IO;

namespace LiveSplit.Multiruns
{
    public partial class MultirunsSettings : UserControl
    {
        private bool On_private;
        public bool On {
            get
            {
                return On_private;
            }
            set {
                if (value && !On)
                {
                    Comp.LoadSplits(0);
                }
                On_private = value;
            }
        }
        private readonly MultirunsComponent Comp;
        private int row;
        public const int rows = 4;

        public MultirunsSettings(MultirunsComponent mc)
        {
            InitializeComponent();
            Comp = mc;

            chkEnable.DataBindings.Add(nameof(CheckBox.Checked), this, nameof(On), false, DataSourceUpdateMode.OnPropertyChanged);
            diaSplitsFile.FileOk += DiaSplitsFile_FileOk;

            Control[] controls = new Control[]
            {
                gbSplits, flpSplits, this
            };

            foreach (Control c in controls)
            {
                c.SuspendLayout();
            }

            for (int i = 0; i < rows; i++)
            {
                Button bOpen = new Button()
                {
                    AutoSize = true,
                    AutoSizeMode = AutoSizeMode.GrowOnly,
                    Dock = DockStyle.Left,
                    Location = new Point(0, 0),
                    Name = "btnOpen" + i.ToString(),
                    Size = new Size(52, 20),
                    TabIndex = 0,
                    Text = "Open..."
                };
                bOpen.Click += BOpen_Click;

                Button bClear = new Button()
                {
                    AutoSize = true,
                    AutoSizeMode = AutoSizeMode.GrowOnly,
                    Dock = DockStyle.Right,
                    Name = "btnClear" + i.ToString(),
                    Size = new Size(52, 20),
                    TabIndex = 1,
                    Text = "Clear"
                };
                bClear.Click += BClear_Click;

                TextBox tb = new TextBox()
                {
                    Dock = DockStyle.None,
                    Enabled = false,
                    Location = new Point(52, 0),
                    Name = "tbSplitsFile"+ i.ToString(),
                    Size = new Size(328, 20),
                    WordWrap = false,
                    Text = ""
                };

                Panel p = new Panel();
                p.SuspendLayout();    
                p.AutoSizeMode = AutoSizeMode.GrowAndShrink;    
                p.Location = new Point(3, (23 * i) + 3);    
                p.Name = "pSplitsFile" + i.ToString();    
                p.Size = new Size(432, 20);    
                p.TabIndex = i;    
                p.Controls.Add(bOpen);
                p.Controls.Add(tb);
                p.Controls.Add(bClear);
                p.ResumeLayout(false);
                p.PerformLayout();    
                flpSplits.Controls.Add(p);
            }

            ((TextBox)flpSplits.Controls[0].Controls[1]).TextChanged += TbSplitsFile0_TextChanged;

            foreach (Control c in controls)
            {
                c.ResumeLayout(false);
            }

            gbSplits.PerformLayout();
        }

        private void TbSplitsFile0_TextChanged(object sender, EventArgs e)
        {
            if (On)
            {
                Comp.LoadSplits(0);
            }
        }

        private void BClear_Click(object sender, EventArgs e)
        {
            Control panel = ((Control)sender).Parent;
            panel.Controls[1].Text = "";
        }

        private void BOpen_Click(object sender, EventArgs e)
        {
            Control panel = ((Control)sender).Parent;
            row = flpSplits.Controls.GetChildIndex(panel);
            diaSplitsFile.FileName = this[row];
            diaSplitsFile.ShowDialog();
        }

        public string this[int i] {
            get
            {
                return flpSplits.Controls[i].Controls[1].Text;
            }
            set
            {
                if (flpSplits.Controls[i].Controls[1].Text != value) {
                    flpSplits.Controls[i].Controls[1].Text = value;
                }
            }
        }

        private void DiaSplitsFile_FileOk(object sender, CancelEventArgs e)
        {
            if (!e.Cancel)
            {
                Control c = flpSplits.Controls[row].Controls[1];
                c.Text = diaSplitsFile.FileName;
            }
        }

        public Stream Open(int i)
        {
            diaSplitsFile.FileName = this[i];
            return diaSplitsFile.OpenFile();
        }

        public Stream Save(int i)
        {
            sfdSplitsFile.FileName = this[i];
            return sfdSplitsFile.OpenFile();
        }
    }
}
