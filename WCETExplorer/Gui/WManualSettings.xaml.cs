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

        

        private WDllChooser wdll;
        private WAlgorithmSettings WAlgo;
        esFunction esf;


        //private static WAlgorithmSettings WAlgo = new WAlgorithmSettings();

        /// <summary>
        /// Aufruf von Window
        /// </summary>
        public WManualSettings(WDllChooser wdll, WAlgorithmSettings WAlgo)
        {
            InitializeComponent();
            this.WAlgo = WAlgo;
            this.wdll = wdll;
            //esf = wdll.getSelectedFunction();
            binaries = new CheckBox[40] { c1, c2, c3, c4, c5, c6, c7, c8, c9, c10, c11, c12, c13, c14, c15, c16, c17, c18, c19, c20, c21, c22, c23, c24, c25, c26, c27, c28, c29, c30, c31, c32, c33, c34, c35, c36, c37, c38, c39, c40 };
            floats = new Slider[10] { f1, f2, f3, f4, f5, f6, f7, f8, f9, f10 };
            enums = new ComboBox[10] { e1, e2, e3, e4, e5, e6, e7, e8, e9, e10 };

            foreach (Slider f in floats)
            {
                f.Minimum = 0;
                f.Maximum = 1;
                f.IsSnapToTickEnabled = true;
                f.TickFrequency = 0.01;
            }

            setPreconfig(wdll.getSelectedFunction());

            // Insert code required on object creation below this point.
            //esFunction func;
            

            
        }

        public delegate void finishedManual_delegate(Genom gn);

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
            WR.manual_mod = true;
            EvolutionAlgo.finishedManual_delegate bla = WR.finishedManual;

            Parameter p = this.getParameter();
            EvolutionAlgo.EvolutionAlgo algo = new EvolutionAlgo.EvolutionAlgo(p, bla, esf.f);
            algo.go();
        }

        
        /// <summary>
        /// Switch to Automatic
        /// Author: Philipp Klein
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Automatic_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            WAlgo.Show();
        }

        private void LoadConf_Click(object sender, RoutedEventArgs e)
        {
            string loadPath;
            Parameter param;
            AlgoSettings sa;
            string funcName;
            string dllPath;

            Nullable<bool> result = WAlgo.dlg.ShowDialog();
            if (result != true)
            {
                return;
            }
            loadPath = WAlgo.dlg.FileName;
            LoadSaveSettings loadsave = new LoadSaveSettings();
            loadsave.load(loadPath, out dllPath, out funcName, out param, out sa);
            
            DllLoader dllLoad = new DllLoader();
            String[] funcs = dllLoad.loadDll(dllPath);
            esFunction esf = dllLoad.loadFunction(funcName);
            WAlgo.dllPath = dllPath;
            this.setPreconfig(esf);
            WAlgo.setParameter(sa);
            this.setParamter(param);

            funcname.Content = funcName;
            WAlgo.funcname.Content = funcName;
        }

        

        /// <summary>
        /// LoadES
        /// Author: Philipp Klein
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadES_Click(object sender, RoutedEventArgs e)
        {
            wdll.Show();
            setPreconfig(wdll.getSelectedFunction());
        }

        /// <summary>
        /// Author: Philipp Klein
        /// </summary>
        public Parameter getParameter()
        {

            float[] analog = new float[esf.floats.Length];
            bool[] digital = new bool[esf.binaries.Length];
            int[] enumses = new int[esf.enums.Length];

            for (int i = 0; i < digital.Length; i++)
                digital[i] = binaries[i].IsChecked.Value;

            for (int i = 0; i < analog.Length; i++)
                analog[i] = (float)floats[i].Value;

            for (int i = 0; i < enumses.Length; i++)
                enumses[i] = enums[i].SelectedIndex;

            Parameter param = new Parameter(analog, digital, enumses);
            return param;
        }

        /// <summary>
        /// Author: Philipp Klein
        /// </summary>
        public void setParamter(Parameter param)
        {

            for (int i = 0; i < binaries.Length; i++)
                if (binaries[i].IsEnabled == true)
                    binaries[i].IsChecked = param.digital[i];

            for (int i = 0; i < floats.Length; i++)
                if (floats[i].IsEnabled == true)
                    floats[i].Value = param.analog[i];

            for (int i = 0; i < enums.Length; i++)
                if (enums[i].IsEnabled == true)
                    enums[i].SelectedIndex = param.enums[i];

        }

        /// <summary>
        /// Authos: Philipp Klein
        /// </summary>
        public void setPreconfig(esFunction func)
        {
            int sizeB, sizeE, sizeF;
            esf = func;
            sizeB = func.binaries.Length;
            sizeE = func.enums.Length;
            sizeF = func.floats.Length;

            //Beschreibt label mit function Name für WAlgo und WManual
            //da es von WDLL chooser aufgerufen wird
            funcname.Content = wdll.getSelectedFunction().name;
            WAlgo.funcname.Content = wdll.getSelectedFunction().name;

            int i;
            for (i = 0; i < sizeB; i++)
            {
                binaries[i].IsEnabled = true;
                binaries[i].IsChecked = false;
            }
            for (; i < binaries.Length; i++)
            {
                binaries[i].IsEnabled = false;
            }

            for (i = 0; i < sizeE; i++)
            {
                enums[i].Items.Clear();
                enums[i].IsEnabled = true;
                for (int j = 0; j < func.enums[i].value.Length; j++)
                {
                    enums[i].Items.Add(func.enums[i].value[j]);
                }
                enums[i].SelectedIndex = 0;
            }
            for (; i < enums.Length; i++)
            {
                enums[i].Items.Clear();
                enums[i].IsEnabled = false;
            }
            for (i = 0; i < sizeF; i++)
            {
                floats[i].IsEnabled = true;
            }
            for (; i < floats.Length; i++)
            {
                floats[i].IsEnabled = false;
            }

        }

        private void f1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ((Slider)sender).ToolTip = ((Slider)sender).Value;
        }

        private void SaveConf_Click_1(object sender, RoutedEventArgs e)
        {
            AlgoSettings param;
            if ((param = WAlgo.getParameter()) == null)
                return;

            string savePath;
            Nullable<bool> result = WAlgo.sfd.ShowDialog();
            if (result != true)
            {
                return;
            }
            savePath = WAlgo.sfd.FileName;
            LoadSaveSettings loadsave = new LoadSaveSettings();
            loadsave.save(savePath, WAlgo.dllPath, WAlgo.functionName, getParameter(), param, 0);
        }
    }
}
