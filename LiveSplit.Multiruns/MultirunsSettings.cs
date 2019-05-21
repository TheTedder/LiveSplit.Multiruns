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
        public bool On { get; set; }
        private readonly MultirunsComponent Comp;
        private int row;

        public MultirunsSettings(MultirunsComponent mc)
        {
            InitializeComponent();
            Comp = mc;

            chkEnable.DataBindings.Add(nameof(CheckBox.Checked), this, nameof(On), false);
            diaSplitsFile.FileOk += DiaSplitsFile_FileOk;

            Control[] controls = new Control[]
            {
                gbSplits, flpSplits, this
            };

            foreach (Control c in controls)
            {
                c.SuspendLayout();
            }

            for (int i = 0; i < 2; i++)
            {
                Button b = new Button()
                {
                    AutoSize = true,
                    Dock = DockStyle.Left,
                    Location = new Point(0, 0),
                    Name = "btnSelect" + i.ToString(),
                    Size = new Size(52, 20),
                    TabIndex = 0,
                    Text = "Open...",
                };

                b.Click += BtnClick;

                TextBox tb = new TextBox()
                {

                    Dock = DockStyle.Top,
                    Enabled = false,
                    Location = new Point(52, 0),
                    Name = "tbSplitsFile"+ i.ToString(),
                    Size = new Size(380, 20),
                    TabIndex = 1,
                    WordWrap = false,
                    Text = ""
                };

                Panel p = new Panel();
                using (p) {
                    SuspendLayout();
                    AutoSizeMode = AutoSizeMode.GrowAndShrink;
                    Location = new Point(3, (23 * i) + 3);
                    Name = "pSplitsFile" + i.ToString();
                    Size = new Size(432, 20);
                    TabIndex = i;
                    Controls.Add(tb);
                    Controls.Add(b);
                    ResumeLayout(false);
                    PerformLayout();
                };
                flpSplits.Controls.Add(p);
            }

            foreach (Control c in controls)
            {
                c.ResumeLayout(false);
            }

            gbSplits.PerformLayout();
        }

        private void BtnClick(object sender, EventArgs e)
        {
            Control panel = ((Control)sender).Parent;
            row = flpSplits.Controls.GetChildIndex(panel);
            diaSplitsFile.ShowDialog();
        }

        public string this[int i] {
            get
            {
                return flpSplits.Controls[i].Controls[1].Text;
            }
            set
            {
                flpSplits.Controls[i].Controls[1].Text = value;
            }
        }

        private void DiaSplitsFile_FileOk(object sender, CancelEventArgs e)
        {
            if (!e.Cancel)
            {
                Control c = flpSplits.Controls[row].Controls[1];
                c.Text = diaSplitsFile.FileName;
                if (On && row == 0)
                {
                    Comp.LoadSplits(0);
                }
            }
        }

        public Stream Open(int i)
        {
            diaSplitsFile.FileName = flpSplits.Controls[i].Controls[1].Text;
            return diaSplitsFile.OpenFile();
        }
    }
}
