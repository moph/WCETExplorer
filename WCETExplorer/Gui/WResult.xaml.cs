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

        //falls WCET unrealistisch
        double dayborder = 86400000;

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
        public void printResult(Generation g1, Genom g2)
        {
            Genom dummy = new Genom(null, null);
            //get best Genom from one Generation
            dummy = g1.getBestGenom();

            //ADD AVG and WCET from GENERATION to List for print
            i = WCETValue.Capacity + 1;
            WCETValue.Add(new KeyValuePair<int, double>(i, dummy.fittness));

            i = AVGValue.Capacity + 1;
            AVGValue.Add(new KeyValuePair<int,double>(i, g1.getAverageFitness()));

            //Print absolut WCET
            Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action<string>(setStatus), g2.fittness.ToString());

            //print <WCET/AVG line
            Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action<List<KeyValuePair<int, double>>>(printWCET), WCETValue);
            Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action<List<KeyValuePair<int, double>>>(printAVG), AVGValue);

            i = 0;
        }

        /// <summary>
        /// Author Marcus Eiswirt
        /// </summary>
        /// <param name="gn"></param>
        public void finishedWCET(Genom gn)
        {
            double temp = gn.fittness;
            if (temp >= dayborder)
                Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action<string>(setStatus), "termination failed");
            else
            {
                temp = Math.Round(temp, 6);
                Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action<string>(setStatus), temp.ToString());
            }
        }

        /// <summary>
        /// Author Marcus Eiswirt
        /// </summary>
        /// <param name="gn"></param>
        public void finishedManual(Genom gn)
        {
            double temp = gn.fittness;
            if (temp >= dayborder && temp < 0)
                Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action<string>(setStatus), "termination failed");
            else
            {
                temp = Math.Round(temp, 6);
                Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action<string>(setStatus), temp.ToString());
            }
        }

        /// <summary>
        /// Author Marcus Eiswirt
        /// </summary>
        /// <param name="msg"></param>
        private void setStatus(string msg)
        {
            string stemp = "termination failed";
            if (msg.Equals(stemp))
                fitt.Content = msg;
            else
            {
                fitt.FontSize = 22;
                fitt.Content = msg + " ms";
            }
        }

        /// <summary>
        /// Author Marcus Eiswirt
        /// </summary>
        /// <param name="tmp"></param>
        private void printWCET(List<KeyValuePair<int, double>> tmp)
        {
            WCET.DataContext = tmp;
        }


        /// <summary>
        /// Author Marcus Eiswirt
        /// </summary>
        /// <param name="tmp"></param>
        private void printAVG(List<KeyValuePair<int, double>> tmp)
        {
            AVG.DataContext = tmp;
        }
	}
}