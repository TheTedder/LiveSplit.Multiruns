﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

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
                    Comp.LoadSplits(0, false);
                }

                On_private = value;
            }
        }
        public bool Autostart { get; set; }
        public string Game { get; set; }
        public string Category { get; set; }
        private readonly MultirunsComponent Comp;
        internal List<Control> Clickables;
        private List<Control> Suspendables;
        private int row;
        public int Count => flpSplits.Controls.Count;

        public MultirunsSettings(MultirunsComponent mc)
        {
            InitializeComponent();
            Clickables = new List<Control>() { chkEnable, btnAdd, chkAutostart };
            Suspendables = new List<Control>(3)
            {
                gbSplits, this
            };
            Comp = mc;

            chkEnable.DataBindings.Add(nameof(CheckBox.Checked), this, nameof(On), false, DataSourceUpdateMode.OnPropertyChanged);
            chkAutostart.DataBindings.Add(nameof(CheckBox.Checked), this, nameof(Autostart), false, DataSourceUpdateMode.OnPropertyChanged);
            tbGame.DataBindings.Add(nameof(TextBox.Text), this, nameof(Game), false, DataSourceUpdateMode.OnValidation);
            tbCategory.DataBindings.Add(nameof(TextBox.Text), this, nameof(Category), false, DataSourceUpdateMode.OnValidation);
            ofdSplitsFile.FileOk += DiaSplitsFile_FileOk;
            btnAdd.Click += (dingus,bingus) => Add();
            Add();

            flpSplits.Controls[0].Controls["tbSplitsFile"].TextChanged += Tb0_TextChanged;
        }

        private bool RemoveAt(int i)
        {
            foreach (Control c in Suspendables)
            {
                c.SuspendLayout();
            }

            if (i >= Count)
            {
                return false;
            }

            //use ToArray() to avoid modifying Clickables during iteration
            foreach(Control control in Clickables.Skip(3)?.Where(c => IndexOf(c) == i).ToArray())
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

            foreach (Control c in Suspendables)
            {
                c.ResumeLayout(true);
            }
            gbSplits.PerformLayout();

            if (i == 0)
            {
                Comp.LoadSplits(0);
                flpSplits.Controls[0].Controls["tbSplitsFile"].TextChanged += Tb0_TextChanged;
            }

            if(Count == 1)
            {
                flpSplits.Controls[0].Controls["btnRemove"].Enabled = false;
                Clickables.Remove(flpSplits.Controls[0].Controls["btnRemove"]);
            }

            return true;
        }

        private int IndexOf(Control control) => flpSplits.Controls.IndexOf(control.Parent);

        public void Add(string text = "")
        {
            bool first = Count == 0;
            if(Count == 1)
            {
                flpSplits.Controls[0].Controls["btnRemove"].Enabled = true;
                Clickables.Add(flpSplits.Controls[0].Controls["btnRemove"]);
            }

            foreach (Control c in Suspendables)
            {
                c.SuspendLayout();
            }

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
                Size = new Size(266, 20),
                WordWrap = false,
                Text = text,
                ReadOnly = true
            };
            
            Button bRemove = new Button();
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
            if (first)
            {
                bRemove.Enabled = false;
            }
            else
            {
                Clickables.Add(bRemove);
            }

            Panel p = new Panel();
            p.SuspendLayout();
            p.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            p.Name = "pSplitsFile";
            p.Size = new Size(429, 20);
            p.TabIndex = Count;
            p.Controls.Add(bOpen);
            p.Controls.Add(tb);
            p.Controls.Add(bClear);
            p.Controls.Add(bRemove);
            p.ResumeLayout(false);
            p.PerformLayout();
            flpSplits.Controls.Add(p);

            foreach (Control c in Suspendables)
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
            Comp.LoadSplits(0);
        }

        private void BClear_Click(object sender, EventArgs e)
        {
            Control panel = ((Control)sender).Parent;
            panel.Controls["tbSplitsFile"].Text = "";
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
                return flpSplits.Controls[i].Controls["tbSplitsFile"].Text;
            }
            set
            {
                if (flpSplits.Controls[i].Controls["tbSplitsFile"].Text != value) {
                    flpSplits.Controls[i].Controls["tbSplitsFile"].Text = value;
                }
            }
        }

        private void DiaSplitsFile_FileOk(object sender, CancelEventArgs e)
        {
            if (!e.Cancel)
            {
                Control c = flpSplits.Controls[row].Controls["tbSplitsFile"];
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