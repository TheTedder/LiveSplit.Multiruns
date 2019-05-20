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

        public MultirunsSettings(MultirunsComponent mc)
        {
            InitializeComponent();
            Comp = mc;

            chkEnable.DataBindings.Add(nameof(CheckBox.Checked), this, nameof(On), false);
            btnSelect.Click += BtnSelect_Click;
            diaSplitsFile.FileOk += DiaSplitsFile_FileOk;
        }

        private void DiaSplitsFile_FileOk(object sender, CancelEventArgs e)
        {
            if (!e.Cancel)
            {
                tbSplitsFile.Text = diaSplitsFile.FileName;
                if (On)
                {
                    Comp.LoadSplits(0);
                }
            }
        }

        private void BtnSelect_Click(object sender, EventArgs e)
        {
            diaSplitsFile.ShowDialog();
        }

        public Stream Open()
        {
            diaSplitsFile.FileName = tbSplitsFile.Text;
            return diaSplitsFile.OpenFile();
        }
    }
}
