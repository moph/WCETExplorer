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

        private WDllChooser wdll;
        public WAlgorithmSettings(WDllChooser wdll)
        {
            InitializeComponent();
			// Insert code required on object creation below this point.
            this.wdll = wdll;
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
            WManualSettings WManual = new WManualSettings(wdll);
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

            sele.SelectedValue = algoSettings.strategy;
            popu.Value = algoSettings.populationSize;
            cross.Text = algoSettings.crossoverCount.ToString();
            muta.Value = algoSettings.mutationRate;

            maxGeneration m = new maxGeneration(1);
            Runtime r = new Runtime(1);
            Fitness f = new Fitness(1);

            for (int i = 0; i < algoSettings.stop.Length; i++)
            {
                if (algoSettings.stop[i].GetType().IsAssignableFrom(m.GetType()))
                {
                    Number_of_generations.IsChecked = true;
                    numGen.Text = ((maxGeneration)algoSettings.stop[i]).maxGen.ToString();
                }
                if (algoSettings.stop[i].GetType().IsAssignableFrom(r.GetType()))
                {
                    Runtime__s_.IsChecked = true;

                    runTime.Text = ((Runtime)algoSettings.stop[i]).runtime.ToString();

                }
                if (algoSettings.stop[i].GetType().IsAssignableFrom(f.GetType()))
                {
                    Fitness__ms_.IsChecked = true;
                    fitness.Text = ((Fitness)algoSettings.stop[i]).fitness.ToString();
                }
            }

        }


        /// <summary>
        /// Author: Philipp Klein
        /// </summary>
        public void getParameter()
        {
            int count = 0;


            StopCriterion[] stop = new StopCriterion[3];
            AlgoSettings algoSettings = new AlgoSettings();

            algoSettings.strategy = (SelectionStrategy)sele.SelectedValue;
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
                Runtime run = new Runtime(Convert.ToUInt32(runTime.Text));
                stop[count] = run;
                count++;
            }
            if (Fitness__ms_.IsChecked == true)
            {
                Fitness fit = new Fitness(Convert.ToUInt32(fitness.Text));
                stop[count] = fit;
                count++;
            }
            StopCriterion[] s = new StopCriterion[count];

            for (int i = 0; i < count; i++)
                s[i] = stop[i];

            algoSettings.stop = s;

        }
    }
}
