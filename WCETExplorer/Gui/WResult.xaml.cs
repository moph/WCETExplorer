using System;
using System.Collections.Generic;
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
using System.Collections;
using System.Windows.Threading;

namespace Gui
{
	/// <summary>
	/// Interaktionslogik für WResult.xaml
    /// Author: Marcus Eiswirt
	/// </summary>
	public partial class WResult : Window
	{
        List<KeyValuePair<int, double>> WCETValue = new List<KeyValuePair<int, double>>();
        List<KeyValuePair<int, double>> AVGValue = new List<KeyValuePair<int, double>>();

        int i = 0;

		public WResult()
		{
            i = 0;

			this.InitializeComponent();

            //showColumnChart(null, null);
		}
        /// <summary>
        /// Wird einzeln aufgerufen liste erstellen
        /// </summary>
        /// Author Marcus Eiswirt
        /// <param name="g1"></param>
        /// <param name="g2"></param>
        private void showColumnChart(Generation g1, Genom g2)
        {
            Genom dummy = new Genom(null, null);
            //get best Genom from one Generation
            dummy = g1.getBestGenom();

            //ADD AVG and WCET from GENERATION to List for print
            WCETValue.Add(new KeyValuePair<int, double>(i, dummy.fittness));
            AVGValue.Add(new KeyValuePair<int,double>(i, g1.getAverageFitness()));

            //WCETValue.Add(new KeyValuePair<int, double>(1, 12.4));
            //WCETValue.Add(new KeyValuePair<int, double>(2, 10.4));
            //WCETValue.Add(new KeyValuePair<int, double>(3, 9.6));

            //Print absolut WCET
            fitt.Content = "" + g2.fittness + " ms";

            WCET.DataContext = WCETValue;
            AVG.DataContext = AVGValue;

        }

        /// <summary>
        /// Author Marcus Eiswirt
        /// </summary>
        /// <param name="gn"></param>
        public void finishedWCET(Genom gn)
        {   // TODO wird zum schluss aufgerufen
            // -> Nach Terminierung 
            fitt.Content = "" + gn.fittness + " ms";
        }


        /// <summary>
        /// Author Marcus Eiswirt
        /// </summary>
        /// <param name="gn"></param>
        public void finishedManual(Genom gn)
        {
            DispatcherOperation op = Dispatcher.BeginInvoke(
                DispatcherPriority.Normal,
                new Action<string>(setStatus),
                gn.fittness.ToString());
        }

        private void setStatus(string msg)
        {
            fitt.Content = msg + " ms";
        }

	}
}