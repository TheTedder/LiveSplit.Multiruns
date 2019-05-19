using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LiveSplit.Multiruns
{
    public partial class MultirunsSettings : UserControl
    {
        public bool enabled;
        public MultirunsSettings()
        {
            InitializeComponent();
            chkEnable.DataBindings.Add(nameof(chkEnable.Checked), this, nameof(enabled),false,DataSourceUpdateMode.OnPropertyChanged);
        }
    }
}
