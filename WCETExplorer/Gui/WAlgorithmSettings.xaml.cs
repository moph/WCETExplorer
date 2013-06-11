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

namespace Gui
{
    /// <summary>
    /// Interaction logic for WAlgorithmSettings.xaml
    /// </summary>
    public partial class WAlgorithmSettings : RibbonWindow
    {


        //public static WAlgorithmSettings WAlgo = new WAlgorithmSettings();
        //public static WManualSettings WManual = new WManualSettings();



        public WAlgorithmSettings()
        {
            InitializeComponent();
			// Insert code required on object creation below this point.
        }

        /// <summary>
        /// Author: Philipp Klein
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveConf_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Author: Philipp Klein
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadConf_Click(object sender, RoutedEventArgs e)
        {

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
            WManualSettings WManual = new WManualSettings();
            WManual.Show();
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
        /// <param name="algoSettings"></param>
        public void setParameter(AlgoSettings algoSettings)
        {

            sele.SetValue = algoSettings.strategy.select;
            popu.setValue = algoSettings.populationSize;
            cross.SetValue = algoSettings.crossoverCount;
            muta.SetValue = algoSettings.mutationRate;


            //sele.SetValue = AlgoSettings.SelectionStrategy;
            //popu.SetValue = AlgoSettings.populationSize;
            //cross.SetValue = AlgoSettings.crossoverCount;
            //muta.SetValue = AlgoSettings.mutationRate;

            for (int i = 0; i < algoSettings.stop.Length; i++)
            {
                numGen.SetValue = algoSettings.stop[i];
            }

        }


        /// <summary>
        /// Author: Philipp Klein
        /// </summary>
        public void getParameter()
        {
            int count = 0;

            AlgoSettings algoSettings = new AlgoSettings();

            algoSettings.strategy = sele.getValue;
            algoSettings.populationSize = popu.GetValue;
            algoSettings.crossoverCount = cross.GetValue;
            algoSettings.mutationRate = muta.GetValue;


            if (Number_of_generations.IsChecked == true)
            {
                algoSettings.stop[count] = numGen.GetValue;
                count++;
            }
            if (Runtime__s_.IsChecked == true)
            {
                algoSettings.stop[count] = runTime.GetValue;
                count++;
            }
            if (Fitness__ms_.IsChecked == true)
            {
                algoSettings.stop[count] = fitness.GetValue;
                count++;
            }

        }
    }
}
