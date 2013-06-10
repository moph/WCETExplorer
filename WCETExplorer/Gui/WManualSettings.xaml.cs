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
using Microsoft.Windows.Controls.Ribbon;
using Gui.Classes;
using System.Windows.Controls;
using EvolutionAlgo;

namespace Gui
{

    


    /// <summary>
    /// Interaction logic for RibbonWindow3.xaml
    /// </summary>
    public partial class WManualSettings : RibbonWindow
    {


        private static Slider[] floats;
        private static CheckBox[] binaries;
        private static ComboBox[] enums;


        private static WAlgorithmSettings WAlgo = new WAlgorithmSettings();

        /// <summary>
        /// Aufruf von Window
        /// </summary>
        public WManualSettings()
        {

            
            InitializeComponent();
            
            binaries = new CheckBox[40] { c1, c2, c3, c4, c5, c6, c7, c8, c9, c10, c11, c12, c13, c14, c15, c16, c17, c18, c19, c20, c21, c22, c23, c24, c25, c26, c27, c28, c29, c30, c31, c32, c33, c34, c35, c36, c37, c38, c39, c40 };
            floats = new Slider[10] { f1, f2, f3, f4, f5, f6, f7, f8, f9, f10 };
            enums = new ComboBox[10] { e1, e2, e3, e4, e5, e6, e7, e8, e9, e10 };

            // Insert code required on object creation below this point.
            esFunction func;
            

            
        }
        /// <summary>
        /// Run
        /// Author: Philipp Klein
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Run_Click(object sender, RoutedEventArgs e)
        {
            WResult WR = new WResult();
            WR.Show();
        }

        /// <summary>
        /// Switch to Automatic
        /// Author: Philipp Klein
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Automatic_Click(object sender, RoutedEventArgs e)
        {
            WAlgorithmSettings.WManual.Hide();
            WAlgorithmSettings.WAlgo.Show();
        }

        private void LoadConf_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SaveConf_Click(object sender, RoutedEventArgs e)
        {
            
        }

        /// <summary>
        /// LoadES
        /// Author: Philipp Klein
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadES_Click(object sender, RoutedEventArgs e)
        {
            Gui.WDllChooser WDll = new Gui.WDllChooser();
            WDll.Show();
        }

        /// <summary>
        /// Author: Philipp Klein
        /// </summary>
        private Parameter getParameters()
        {

            float[] analog;
            bool[] digital;
            int[] enumses;

            for (int i = 0; i < binaries.Length; i++)
                if (binaries[i].IsEnabled == true)
                    digital[i] = binaries[i].IsChecked.Value;

            for (int i = 0; i < floats.Length; i++)
                if (floats[i].IsEnabled == true)
                    analog[i] = (float)floats[i].Value;

            for (int i = 0; i < enums.Length; i++)
                if (enums[i].IsEnabled == true)
                    enumses[i] = (int)enums[i].SelectedValue;

            Parameter param; // = new Parameter(analog, digital, enums);
            return param;
        }

        /// <summary>
        /// Author: Philipp Klein
        /// </summary>
        private void setParamters(Parameter param)
        {

            for (int i = 0; i < binaries.Length; i++)
                if (binaries[i].IsEnabled == true)
                    binaries[i].IsChecked = param.digital[i];

            for (int i = 0; i < floats.Length; i++)
                if (floats[i].IsEnabled == true)
                    floats[i].Value = param.analog[i];

            for (int i = 0; i < enums.Length; i++)
                if (enums[i].IsEnabled == true)
                    enums[i].SetCurrentValue(param.enums);

        }

        /// <summary>
        /// Authos: Philipp Klein
        /// </summary>
        private void setPreconfig(/*esFunction func,*/ esFunctionBinary[] b, esFunctionEnum[] e, esFunctionFloat[] f)
        {
            int sizeB, sizeE, sizeF;
            
            //sizeB = func.binaries.Length;
            //sizeE = func.enums.Length;
            //sizeF = func.floats.Length;

            sizeB = b.Length;
            sizeE = e.Length;
            sizeF = f.Length;

            for (int i = 0; i < sizeB; i++)
            {
                binaries[i].IsEnabled = false;
                binaries[i].IsChecked = false;
            }
            for (int i = 0; i < sizeE; i++)
            {
                enums[i].IsEnabled = false;
                enums[i].Items.Add(e[i].value);
            }
            for (int i = 0; i < sizeF; i++)
            {
                floats[i].IsEnabled = false;
                floats[i].Minimum = f[i].Value[1];
                floats[i].Maximum = f[i].Value[2];
            }

        }

    }
}
