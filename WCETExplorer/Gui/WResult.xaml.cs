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
using Gui.Classes;

namespace Gui
{
    /// <summary>
    /// logic for WResult.xaml
    /// Author: Marcus Eiswirt
    /// </summary>
    public partial class WResult : Window
    {
        private FunctionResult fr = new FunctionResult();

        public bool manual_mod, sms = false;

        private List<KeyValuePair<int, double>> WCETValue = new List<KeyValuePair<int, double>>();
        private List<KeyValuePair<int, double>> AVGValue = new List<KeyValuePair<int, double>>();

        private List<Genom> GWCETList = new List<Genom>();

        private Microsoft.Win32.SaveFileDialog sfd { get; set; }

        private Genom Manual_Genom = null;

        private WCETInfo wi = null;

        private int i = 0;

        //falls WCET unrealistisch :: 12h
        private double dayborder = 86400000 / 2;
        private double fittness = 0;

        public WResult()
        {
            this.InitializeComponent();

            i = 0;

            WCETValue.Clear();
            AVGValue.Clear();

            sfd = new Microsoft.Win32.SaveFileDialog();
            sfd.FileName = "Result";
            sfd.DefaultExt = ".txt";
        }
        /// <summary>
        /// </summary>
        /// Author Marcus Eiswirt
        /// <param name="g1"></param>
        /// <param name="g2">best Genom</param>
        public void printResult(Generation g1, Genom g2)
        {
            //get best Genom from one Generation
            Genom dummy = g2;
            //Dünner machen
            GWCETList.Add(dummy);

            //ADD AVG and WCET from GENERATION to List for print
            i = WCETValue.Count;

            WCETValue.Add(new KeyValuePair<int, double>(i, dummy.fittness));

            i = AVGValue.Count;
            AVGValue.Add(new KeyValuePair<int, double>(i, g1.getAverageFitness()));

            i = 0;
        }

        /// <summary>
        /// Author Marcus Eiswirt
        /// </summary>
        /// <param name="gn"></param>
        public void finishedWCET(Genom gn)
        {
            fittness = gn.fittness;
            if (fittness >= dayborder)
                Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action<string>(setStatus), "termination failed");
            else
            {
                fittness = Math.Round(fittness, 6);
                Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action<string>(setStatus), fittness.ToString());
            }

            //print <WCET/AVG line
            Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action<List<KeyValuePair<int, double>>>(printWCET), WCETValue);
            Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action<List<KeyValuePair<int, double>>>(printAVG), AVGValue);
        }

        /// <summary>
        /// Author Marcus Eiswirt
        /// </summary>
        /// <param name="gn"></param>
        public void finishedManual(Genom gn)
        {
            Manual_Genom = gn;
            fittness = gn.fittness;
            if (fittness >= dayborder || fittness < 0)
                Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action<string>(setStatus), "termination failed");
            else
            {
                fittness = Math.Round(fittness, 6);
                Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action<string>(setStatus), fittness.ToString());
                
            }
        }

        /// <summary>
        /// Author Marcus Eiswirt
        /// </summary>
        /// <param name="msg">WCET value</param>
        private void setStatus(string msg)
        {
            string stemp = "termination failed";
            if (msg.Equals(stemp))
                fitt.Content = msg;
            else
            {
                fitt.FontSize = 23;
                fitt.Content = msg + " ms";
            }
        }

        /// <summary>
        /// Author Marcus Eiswirt
        /// </summary>
        /// <param name="tmp">LineSeries List</param>
        private void printWCET(List<KeyValuePair<int, double>> tmp)
        {
            WCET.DataContext = tmp;
        }


        /// <summary>
        /// Author Marcus Eiswirt
        /// </summary>
        /// <param name="tmp">LineSeries List</param>
        private void printAVG(List<KeyValuePair<int, double>> tmp)
        {
            AVG.DataContext = tmp;
        }


        /// <summary>
        /// Author Marcus Eiswirt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void line(object sender, MouseEventArgs e)
        {
            System.Windows.Controls.DataVisualization.Charting.DataPoint temp = sender as System.Windows.Controls.DataVisualization.Charting.DataPoint;
            String i = temp.ActualIndependentValue.ToString();
            int j = Convert.ToInt32(i);

            int h = 0;

            foreach (Genom b in GWCETList)
            {
                
                if (h == j)
                {
                    wi = new WCETInfo();
                    wi.Show();
                    wi.setListbox(b);
                }
                h++;
            }

        }

        private void Save_Result(object sender, RoutedEventArgs e)
        {
            string savePath;

            Nullable<bool> result = sfd.ShowDialog();
            
            if (result != true)
            {
                return;
            }

            savePath = sfd.FileName;
            if (manual_mod == false)
                fr.Save_Result(savePath, GWCETList, null);
            else
                fr.Save_Result(savePath, null, Manual_Genom); 
        }

        private void labeltrans(object sender, MouseButtonEventArgs e)
        {

            if (sms)
            {
                fitt.Content = fittness / 1000 + " s";
                sms = false;
            }
            else if (!sms)
            {
                fitt.Content = fittness * 1000 + " ms";
                sms = true;
            }
        }
    }
}