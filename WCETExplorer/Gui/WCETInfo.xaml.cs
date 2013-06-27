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
    /// Interaktionslogik für Window1.xaml
    /// </summary>
    public partial class WCETInfo : Window
    {
        private WResult wResult;

        List<float> _lfloat = new List<float>();
        List<bool> _lbool = new List<bool>();
        List<int> _lstring = new List<int>();

        public WCETInfo()
        {
            InitializeComponent();
            
            listBox_a.Items.Clear();
            listBox_d.Items.Clear();
            listBox_e.Items.Clear();

        }

        public WCETInfo(WResult wResult)
        {
            // TODO: Complete member initialization
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
