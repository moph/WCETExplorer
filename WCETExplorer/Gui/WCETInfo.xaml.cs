using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using EvolutionAlgo;

namespace Gui
{
    /// <summary>
    /// Author Marcus Eiswirt
    /// </summary>
    public partial class WCETInfo : Window
    {
        private WResult wResult;

        private List<float> _lfloat = new List<float>();
        private List<bool> _lbool = new List<bool>();
        private List<int> _lstring = new List<int>();

        public WCETInfo()
        {
            InitializeComponent();
        }

        public WCETInfo(WResult wResult)
        {
            this.wResult = wResult;
        }

        public void setListbox(Genom Glist)
        {
                for (int j = 0; j < Glist._param.analog.Count(); j++)
                {
                    listBox_a.Items.Add( Glist._param.analog[j]);
                }

                for (int j = 0; j < Glist._param.digital.Count(); j++)
                {
                    int i = 0;
                    if (Glist._param.digital[j] == true)
                        i = 1;

                    listBox_d.Items.Add(i);
                }

                for (int j = 0; j < Glist._param.enums.Count(); j++)
                {
                    listBox_e.Items.Add(Glist._param.enums[j]);
                }
        }

    }
}
