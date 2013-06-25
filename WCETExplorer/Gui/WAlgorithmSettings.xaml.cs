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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Windows.Controls.Ribbon;
using EvolutionAlgo;
using Gui.Classes;
using System.Collections;

namespace Gui
{
    /// <summary>
    /// Interaction logic for WAlgorithmSettings.xaml
    /// </summary>
    public partial class WAlgorithmSettings : RibbonWindow
    {

        public string dllPath {get;set;}
        public string functionName {get;set;}
        public WDllChooser wdll {get;set;}
        public WManualSettings WManual {get; set;}
        public Microsoft.Win32.OpenFileDialog dlg {get;set;}
        public Microsoft.Win32.SaveFileDialog sfd { get; set; }

        public WAlgorithmSettings(WDllChooser wdll)
        {
            InitializeComponent();
            this.wdll = wdll;
            dllPath = wdll.getXmlPath();
            functionName = wdll.getSelectedFunction().name;
            WManual = new WManualSettings(wdll, this);

            //Selection Strategien Auswählen
            sele.Items.Add(new Elitismus().toString());
            sele.Items.Add(new RangSelection().toString());
            sele.Items.Add(new FittPropSelection().toString());
            

            funcname.Content = functionName;

            popu.Minimum = 10;
            popu.Maximum = 1000;
            popu.IsSnapToTickEnabled = true;
            popu.TickFrequency = 1;
            muta.Minimum = 0;
            muta.Maximum = 1;
            muta.IsSnapToTickEnabled = true;
            muta.TickFrequency=0.01;


            //Load
            dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = "Document";
            dlg.DefaultExt = ".xml";
            dlg.Filter = "Extensible Markup Language (.xml)|*.xml";

            //Save
            sfd = new Microsoft.Win32.SaveFileDialog();
            sfd.FileName = "Document";
            sfd.DefaultExt = ".xml";
            sfd.Filter = "Extensible Markup Language (.xml)|*.xml";
 
        }

        /// <summary>
        /// Author: Philipp Klein
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveConf_Click(object sender, RoutedEventArgs e)
        {
            string savePath;
            Nullable<bool> result = sfd.ShowDialog();
            if (result != true)
            {
                return;
            }
            savePath = sfd.FileName;
            LoadSaveSettings loadsave = new LoadSaveSettings();
            loadsave.save(savePath, dllPath, functionName, WManual.getParameter(), getParameter());

        }

        /// <summary>
        /// Author: Philipp Klein
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadConf_Click(object sender, RoutedEventArgs e)
        {
            string loadPath;
            Parameter param;
            AlgoSettings sa;
            string funcName;
            string dllPath;

            Nullable<bool> result = dlg.ShowDialog();
            if (result != true)
            {
                return;
            }
            loadPath = dlg.FileName;
            LoadSaveSettings loadsave = new LoadSaveSettings();
            loadsave.load(loadPath, out dllPath, out funcName, out param, out sa);

            DllLoader dllLoad = new DllLoader();
            String[] funcs = dllLoad.loadDll(dllPath);
            esFunction esf = dllLoad.loadFunction(funcName);
            this.dllPath = dllPath;
            WManual.setPreconfig(esf);

            Number_of_generations.IsChecked = false;
            Runtime__s_.IsChecked = false;
            Fitness__ms_.IsChecked = false;

            setParameter(sa);
            WManual.setParamter(param);

            funcname.Content = funcName;
            WManual.funcname.Content = funcName;
        }

        /// <summary>
        /// Switch to Algo
        /// Author: Philipp Klein
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Manual_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            this.WManual.Show();
        }

        /// <summary>
        /// Run
        /// Author: Philipp Klein
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Run_Click(object sender, RoutedEventArgs e)
        {
            Gui.WResult WRun = new Gui.WResult();
            WRun.Show();
            EvolutionAlgo.printResult_delegate pR = WRun.printResult;
            EvolutionAlgo.finishedWCET_delegate fW = WRun.finishedWCET;
            EvolutionAlgo.EvolutionAlgo evo = new EvolutionAlgo.EvolutionAlgo(WManual.getParameter(), getParameter(), pR, fW, wdll.getSelectedFunction().f);
            evo.go();
            

            
        }

        /// <summary>
        /// LoadES
        /// Author: Philipp Klein
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadES_Click(object sender, RoutedEventArgs e)
        {
            dllPath = wdll.getXmlPath();
            functionName = wdll.getSelectedFunction().name;
            wdll.Show();
            
        }


        /// <summary>
        /// Author: Philipp Klein
        /// </summary>
        /// <param name="algoSettings"></param>
        public void setParameter(AlgoSettings algoSettings)
        {

            //sele.SelectedValue = algoSettings.strategy;

            if (algoSettings.strategy is Elitismus)
                sele.SelectedIndex = 0;
            else if (algoSettings.strategy is RangSelection)
                sele.SelectedIndex = 1;
            else if (algoSettings.strategy is FittPropSelection)
                sele.SelectedIndex = 2;

            popu.Value = algoSettings.populationSize;
            cross.Text = algoSettings.crossoverCount.ToString();
            muta.Value = algoSettings.mutationRate;

            for (int i = 0; i < algoSettings.stop.Length; i++)
            {
                if (algoSettings.stop[i] is maxGeneration)
                {
                    Number_of_generations.IsChecked = true;
                    numGen.Text = ((maxGeneration)algoSettings.stop[i]).maxGen.ToString();
                }
                if (algoSettings.stop[i] is Runtime)
                {
                    Runtime__s_.IsChecked = true;

                    runTime.Text = ((Runtime)algoSettings.stop[i]).runtime.ToString();

                }
                if (algoSettings.stop[i] is Fitness)
                {
                    Fitness__ms_.IsChecked = true;
                    fitness.Text = ((Fitness)algoSettings.stop[i]).fitness.ToString();
                }
            }

        }


        /// <summary>
        /// Author: Philipp Klein
        /// </summary>
        public AlgoSettings getParameter()
        {
            try
            {
                int count = 0;
                StopCriterion[] stop = new StopCriterion[3];
                AlgoSettings algoSettings = new AlgoSettings();

                //wenn sele items direkt objekte bekommt
                //algoSettings.strategy = (SelectionStrategy)sele.SelectedValue;

                //sele to string test
                if (sele.SelectedValue.Equals(new Elitismus().toString()))
                    algoSettings.strategy = (SelectionStrategy)new Elitismus();
                else if (sele.SelectedValue.Equals(new RangSelection().toString()))
                    algoSettings.strategy = (SelectionStrategy)new RangSelection();
                else if (sele.SelectedValue.Equals(new FittPropSelection().toString()))
                    algoSettings.strategy = (SelectionStrategy)new FittPropSelection();


                algoSettings.populationSize = (uint)popu.Value;
                algoSettings.crossoverCount = Convert.ToUInt32(cross.Text);
                algoSettings.mutationRate = (float)muta.Value;


                if (Number_of_generations.IsChecked == true)
                {
                    maxGeneration gen = new maxGeneration(Convert.ToUInt32(numGen.Text));
                    stop[count] = gen;
                    count++;
                }
                if (Runtime__s_.IsChecked == true)
                {
                    Runtime run = new Runtime(Convert.ToDouble(runTime.Text));
                    stop[count] = run;
                    count++;
                }
                if (Fitness__ms_.IsChecked == true)
                {
                    Fitness fit = new Fitness(Convert.ToDouble(fitness.Text));
                    stop[count] = fit;
                    count++;
                }
                StopCriterion[] s = new StopCriterion[count];

                for (int i = 0; i < count; i++)
                    s[i] = stop[i];

                algoSettings.stop = s;
                return algoSettings;
            }catch(FormatException ex){
                throw ex;
            }
            
            
        }

        
        private void popu_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            popsize.Content= popu.Value;
        }

        private void muta_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            mutasize.Content = muta.Value;
        }

        private void NumericOnly(System.Object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = IsTextNumeric(e.Text);
        }

        private static bool IsTextNumeric(string str)
        {
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex("[^0-9]");
            return reg.IsMatch(str);
        }


        private void NumericPointOnly(System.Object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = IsTextNumericPoint(e.Text);
        }

        private static bool IsTextNumericPoint(string str)
        {
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex("[^0-9.]");
            return reg.IsMatch(str);
        }



    }
}
