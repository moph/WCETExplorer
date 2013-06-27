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
        private List<KeyValuePair<int, double>> WCETValue = new List<KeyValuePair<int, double>>();
        private List<KeyValuePair<int, double>> AVGValue = new List<KeyValuePair<int, double>>();

        private List<Genom> GWCETList = new List<Genom>();

        private Microsoft.Win32.SaveFileDialog sfd { get; set; }

        private WCETInfo wi = null;

        private int i = 0;

        //falls WCET unrealistisch :: 12h
        private double dayborder = 86400000 / 2;

        public WResult()
        {
            i = 0;

            WCETValue.Clear();
            AVGValue.Clear();

            sfd = new Microsoft.Win32.SaveFileDialog();
            sfd.FileName = "Result";
            sfd.DefaultExt = ".txt";

            this.InitializeComponent();
        }
        /// <summary>
        /// Wird einzeln aufgerufen liste erstellen
        /// </summary>
        /// Author Marcus Eiswirt
        /// <param name="g1"></param>
        /// <param name="g2"></param>
        public void printResult(Generation g1, Genom g2)
        {
            //get best Genom from one Generation
            Genom dummy = g1.getBestGenom();

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
            double temp = gn.fittness;
            if (temp >= dayborder)
                Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action<string>(setStatus), "termination failed");
            else
            {
                temp = Math.Round(temp, 6);
                Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action<string>(setStatus), temp.ToString());
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
            double temp = gn.fittness;
            if (temp >= dayborder || temp < 0)
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
            int i = 1;
            Nullable<bool> result = sfd.ShowDialog();
            
            if (result != true)
            {
                return;
            }

            savePath = sfd.FileName;

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(savePath, true))
            {
                foreach (Genom tmp in GWCETList)
                {
                    file.WriteLine();
                    file.WriteLine(i + ". Genom");
                    file.WriteLine();
                    file.Write("WCET :");
                    file.Write(" "+ tmp.fittness);

                    file.WriteLine(" ");
                    file.WriteLine("Parameter:");
                    file.WriteLine(" ");

                    file.Write("Analog :");
                    
                    for(int j = 0; j < tmp._param.analog.Length; j++)
                    {
                        file.Write(" "+tmp._param.analog[j]);
                    }

                    file.WriteLine();
                    file.Write("Digital :");
                    for (int j = 0; j < tmp._param.digital.Length; j++)
                    {
                        file.Write(" " + tmp._param.digital[j]);
                    }

                    file.WriteLine();

                    file.Write("Enum :");
                    for (int j = 0; j < tmp._param.enums.Length; j++)
                    {
                        file.Write(" " + tmp._param.enums[j]);
                    }
                    file.WriteLine();
                    file.WriteLine();

                    i++;
                }
                
            }

        }
    }
}