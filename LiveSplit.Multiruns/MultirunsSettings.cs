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
        public string NextFile { get; set; }

        public MultirunsSettings()
        {
            InitializeComponent();

            chkEnable.DataBindings.Add(nameof(CheckBox.Checked), this, nameof(On), false, DataSourceUpdateMode.OnPropertyChanged);
            tbSplitsFile.DataBindings.Add(nameof(TextBox.Text), this, nameof(NextFile), false, DataSourceUpdateMode.OnPropertyChanged);
            btnSelect.Click += BtnSelect_Click;
            diaSplitsFile.FileOk += DiaSplitsFile_FileOk;
        }

        private void DiaSplitsFile_FileOk(object sender, CancelEventArgs e)
        {
            if (!e.Cancel)
            {
                tbSplitsFile.Text = diaSplitsFile.FileName;
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
