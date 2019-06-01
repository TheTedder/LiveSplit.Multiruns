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
using System.Reflection;

namespace LiveSplit.Multiruns
{
    public partial class MultirunsSettings : UserControl
    {
        private bool On_private = false;
        public bool On {
            get
            {
                return On_private;
            }
            set {
                if (value && !On)
                {
                    Comp.LoadSplits(0,false);
                }

                On_private = value;
            }
        }
        public bool Autostart { get; set; }
        private readonly MultirunsComponent Comp;
        internal List<Control> Clickables;
        private List<Control> Suspendibles;
        private int row;
        public int Count => flpSplits.Controls.Count;

        public MultirunsSettings(MultirunsComponent mc)
        {
            InitializeComponent();
            Clickables = new List<Control>() { chkEnable, btnAdd, chkAutostart };
            Suspendibles = new List<Control>(3)
            {
                gbSplits, this
            };
            Comp = mc;

            chkEnable.DataBindings.Add(nameof(CheckBox.Checked), this, nameof(On), false, DataSourceUpdateMode.OnPropertyChanged);
            chkAutostart.DataBindings.Add(nameof(CheckBox.Checked), this, nameof(Autostart), false, DataSourceUpdateMode.OnPropertyChanged);
            ofdSplitsFile.FileOk += DiaSplitsFile_FileOk;
            btnAdd.Click += (dingus,bingus) => Add();
            Add();

            flpSplits.Controls[0].Controls[1].TextChanged += Tb0_TextChanged;
        }

        public bool RemoveAt(int i)
        {
            foreach (Control c in Suspendibles)
            {
                c.SuspendLayout();
            }

            if (i == 0 || i >= Count)
            {
                return false;
            }

            foreach(Control control in new List<Control>(Clickables.Skip(3)?.Where(c => IndexOf(c) == i)))
            {
                Clickables.Remove(control);
            }

            foreach (Control control in flpSplits.Controls[i].Controls)
            {
                flpSplits.Controls[i].Controls.Clear();
                control.Dispose();
            }

            Control panel = flpSplits.Controls[i];
            flpSplits.Controls.RemoveAt(i);
            panel.Dispose();

            foreach (Control c in Suspendibles)
            {
                c.ResumeLayout(true);
            }
            gbSplits.PerformLayout();

            return true;
        }

        private int IndexOf(Control control) => flpSplits.Controls.IndexOf(control.Parent);

        public void Add(string text = "")
        {
            foreach (Control c in Suspendibles)
            {
                c.SuspendLayout();
            }

            bool first = Count == 0;

            Button bOpen = new Button()
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowOnly,
                Dock = DockStyle.Left,
                Location = new Point(0, 0),
                Name = "btnOpen",
                Size = new Size(52, 20),
                TabIndex = 0,
                Text = "Open...",
            };
            bOpen.Click += BOpen_Click;
            Clickables.Add(bOpen);

            Button bClear = new Button()
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowOnly,
                Dock = DockStyle.Right,
                Name = "btnClear",
                Size = new Size(52, 20),
                TabIndex = 1,
                Text = "Clear"
            };
            bClear.Click += BClear_Click;
            Clickables.Add(bClear);

            TextBox tb = new TextBox()
            {
                Dock = DockStyle.None,
                Location = new Point(53, 0),
                Name = "tbSplitsFile",
                Size = new Size(269, 20),
                WordWrap = false,
                Text = text,
                ReadOnly = true
            };
            if (first)
            {
                tb.Size = new Size(326, 20);
            }

            
            Button bRemove = new Button();
            if (!first)
            {
                bRemove = new Button()
                {
                    AutoSize = true,
                    AutoSizeMode = AutoSizeMode.GrowOnly,
                    Dock = DockStyle.Right,
                    Name = "btnRemove",
                    Size = new Size(52, 20),
                    TabIndex = 2,
                    Text = "Remove"
                };
                bRemove.Click += BRemove_Click;
                Clickables.Add(bRemove);
            }

            Panel p = new Panel();
            p.SuspendLayout();
            p.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            p.Name = "pSplitsFile";
            p.Size = new Size(432, 20);
            p.TabIndex = Count;
            p.Controls.Add(bOpen);
            p.Controls.Add(tb);
            p.Controls.Add(bClear);
            if (!first)
            {
                p.Controls.Add(bRemove);
            }
            p.ResumeLayout(false);
            p.PerformLayout();
            flpSplits.Controls.Add(p);

            foreach (Control c in Suspendibles)
            {
                c.ResumeLayout(true);
            }
            gbSplits.PerformLayout();
        }

        private void BRemove_Click(object sender, EventArgs e)
        {
            RemoveAt(IndexOf((Control)sender));
        }

        private void Tb0_TextChanged(object sender, EventArgs e)
        {
            if (IndexOf((Control)sender) == 0)
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
            row = IndexOf((Control)sender);
            ofdSplitsFile.FileName = this[row];
            ofdSplitsFile.ShowDialog();
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
                c.Text = ofdSplitsFile.FileName;
            }
        }

        public Stream Open(int i)
        {

            ofdSplitsFile.FileName = this[i];
            return ofdSplitsFile.OpenFile();
        }

        public Stream Save(int i)
        {
            sfdSplitsFile.FileName = this[i];
            return sfdSplitsFile.OpenFile();
        }
    }
}