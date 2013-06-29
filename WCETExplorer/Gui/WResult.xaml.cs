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
using System.Threading;
using System.ComponentModel;

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
        private bool ptrue = false;

        private List<KeyValuePair<int, double>> WCETValue = new List<KeyValuePair<int, double>>();
        private List<KeyValuePair<int, double>> AVGValue = new List<KeyValuePair<int, double>>();
        private List<KeyValuePair<int, double>> CountPointList = new List<KeyValuePair<int, double>>();

        private List<KeyValuePair<int, double>> tempWCETValue = new List<KeyValuePair<int, double>>();
        private List<KeyValuePair<int, double>> tempAVGValue = new List<KeyValuePair<int, double>>();

        private List<Genom> GWCETList = new List<Genom>();

        private Microsoft.Win32.SaveFileDialog sfd { get; set; }

        private Genom Manual_Genom = null;

        private WCETInfo wi = null;

        private int i = 0;

        private int forcount = 0;

        BackgroundWorker bW = new BackgroundWorker();

        //falls WCET unrealistisch :: 12h
        private double dayborder = 86400000 / 2;
        private double fittness = 0;

        public WResult()
        {
            this.InitializeComponent();

            WCET.DataContext = null;

            tempAVGValue.Clear();
            tempWCETValue.Clear();

            i = 0;

            sfd = new Microsoft.Win32.SaveFileDialog();
            sfd.FileName = "Result";
            sfd.DefaultExt = ".txt";
            progressBar1.Value = 0;

            // To report progress from the background worker we need to set this property
            bW.WorkerReportsProgress = true;
            // This event will be raised on the worker thread when the worker starts
            bW.DoWork += new DoWorkEventHandler(bW_DoWork);
            // This event will be raised when we call ReportProgress
            bW.ProgressChanged += new ProgressChangedEventHandler(bW_ProgressChanged);

            bW.RunWorkerAsync();
        }
        /// <summary>
        /// </summary>
        /// Author Marcus Eiswirt
        /// <param name="g1"></param>
        /// <param name="g2">best Genom</param>
        public void printResult(Generation g1, Genom g2)
        {

            //get best Genom from one Generation
            GWCETList.Add(g2);

            //ADD AVG and WCET from GENERATION to List for print
            i = WCETValue.Count;

            WCETValue.Add(new KeyValuePair<int, double>(i, g2.fittness));

            i = AVGValue.Count;
            AVGValue.Add(new KeyValuePair<int, double>(i, g1.getAverageFitness()));
            System.Threading.Thread.Sleep(10);

            i = 0;
        }

        /// <summary>
        /// Author Marcus Eiswirt
        /// </summary>
        /// <param name="gn"></param>
        public void finishedWCET(Genom gn)
        {
            try
            {
               ptrue = true;

                fittness = gn.fittness;
                if (fittness > dayborder)
                    Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action<string>(setStatus), fittness.ToString());
                else
                {
                    fittness = Math.Round(fittness, 6);
                    Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action<string>(setStatus), fittness.ToString());
                }

                //print <WCET/AVG line
                KeyValuePair<int, double> t = new KeyValuePair<int,double>(0,0);

                int size = 0;

                foreach (KeyValuePair<int, double> tmp in AVGValue)
                {
                    if (t.Value != tmp.Value)
                    {
                        t = tmp;
                        size = tempAVGValue.Count;
                        tempAVGValue.Add(new KeyValuePair<int, double>(size + 1, t.Value));

                    }
                }

                Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action<List<KeyValuePair<int, double>>>(printAVG), tempAVGValue);

                t = new KeyValuePair<int, double>(0, 0);
                size = 0;
                foreach (KeyValuePair<int, double> tmp in WCETValue)
                {
                    if (t.Value != tmp.Value)
                    {
                        t = tmp;
                        size = tempWCETValue.Count;
                        tempWCETValue.Add(new KeyValuePair<int, double>(size + 1, t.Value));
                    }

                }

                KeyValuePair<int, double> templast = new KeyValuePair<int, double>(tempAVGValue[tempAVGValue.Count - 1].Key, tempWCETValue[tempWCETValue.Count - 1].Value);

                tempWCETValue.Insert(tempWCETValue.Count, templast);

                Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action<List<KeyValuePair<int, double>>>(printWCET), tempWCETValue);

            }
            catch (NotSupportedException e)
            {
                MessageBox.Show(e.ToString());
            }

        }

        /// <summary>
        /// Author Marcus Eiswirt
        /// </summary>
        /// <param name="gn"></param>
        public void finishedManual(Genom gn)
        {
            try
            {
                Manual_Genom = gn;
                fittness = gn.fittness;
                if (fittness < 0)
                    Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action<string>(setStatus), "Value < 0");
                else
                {
                    fittness = Math.Round(fittness, 6);
                    Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action<string>(setStatus), fittness.ToString());

                }
            }
            catch (NotSupportedException e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        /// <summary>
        /// Author Marcus Eiswirt
        /// </summary>
        /// <param name="msg">WCET value</param>
        private void setStatus(string msg)
        {
            string stemp = "Value < 0";
            if (msg.Equals(stemp))
                fitt.Content = msg;
            else
            {
                if (msg.Length > 19)
                {
                    fitt.FontSize = 17;
                    fitt.Content = msg;
                }
                else
                {
                    fitt.FontSize = 21;
                    fitt.Content = msg + " ms";
                }
            }
        }

        /// <summary>
        /// Author Marcus Eiswirt
        /// </summary>
        /// <param name="tmp">LineSeries List</param>
        private void printWCET(List<KeyValuePair<int, double>> tmp)
        {
            WCET.DataContext = tmp;
            
            WCETValue.Clear();
        }


        /// <summary>
        /// Author Marcus Eiswirt
        /// </summary>
        /// <param name="tmp">LineSeries List</param>
        private void printAVG(List<KeyValuePair<int, double>> tmp)
        {
            AVG.DataContext = tmp;

            AVGValue.Clear();
        }


        /// <summary>
        /// Author Marcus Eiswirt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void line(object sender, MouseEventArgs e)
        {
            bool windowop = false;

            System.Windows.Controls.DataVisualization.Charting.DataPoint temp = sender as System.Windows.Controls.DataVisualization.Charting.DataPoint;
            String i = temp.ActualDependentValue.ToString();
            double j = Convert.ToDouble(i);
            int sch = 0;
            bool zi = true;
            //int k = 0;

            foreach (KeyValuePair<int, double> ts in tempWCETValue)
            {
                if (ts.Value.ToString() == j.ToString())
                {
                    j = ts.Value;

                }
            }

            foreach (Genom ts in GWCETList)
            {
                if (Math.Round(ts.fittness, 4) == Math.Round(j,4))
                {
                    if(zi)
                    sch++;
                    else
                    {
                        sch++;
                        zi = false;
                    }
                }
            }

            foreach (Genom b in GWCETList)
            {

                if (Math.Round(b.fittness, 4) == Math.Round(j, 4))
                {
                    if (!windowop)
                    {
                        wi = new WCETInfo();
                        wi.Show();
                        wi.Title = sch.ToString() + " Generation";
                        wi.setListbox(b);
                        windowop = true;
                    }
                }
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
            if (!sms)
            {
                fitt.Content = fittness / 1000 + " s";
                sms = false;
            }
            else if (sms)
            {
                fitt.Content = fittness * 1000 + " ms";
                sms = true;
            }
        }

        void bW_DoWork(object sender, DoWorkEventArgs e)
        {
            forcount = 0;
            // Your background task goes here
            for (; forcount <= 101; forcount++)
            {
                if (ptrue == true)
                {
                    bW.ReportProgress(100);
                    return;
                }

                // Report progress to 'UI' thread
                bW.ReportProgress(forcount);
                // Simulate long task
                System.Threading.Thread.Sleep(30);
            }
        }
        // Back on the 'UI' thread so we can update the progress bar
        void bW_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // The progress percentage is a property of e
            if (progressBar1.Value == progressBar1.Maximum)
            {
                progressBar1.Value = 0;
                forcount = 0;
            }
            else
                progressBar1.Value = e.ProgressPercentage;
        }


    }
}