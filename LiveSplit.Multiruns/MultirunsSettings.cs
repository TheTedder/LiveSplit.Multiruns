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

namespace LiveSplit.Multiruns
{
    public partial class MultirunsSettings : UserControl
    {
        public bool On { get; set; }
        public string GameName { get; set; }

        public MultirunsSettings()
        {
            InitializeComponent();

            chkEnable.DataBindings.Add(nameof(CheckBox.Checked), this, nameof(On), false, DataSourceUpdateMode.OnPropertyChanged);
            
            On = true;
        }
    }
}
