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

namespace Gui
{
    /// <summary>
    /// Interaction logic for RibbonWindow3.xaml
    /// </summary>
    public partial class WManualSettings : RibbonWindow
    {
        /// <summary>
        /// Aufruf von Window
        /// </summary>
        public WManualSettings()
        {
            InitializeComponent();
            
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
            Gui.WResult WRun = new Gui.WResult();
            WRun.Show();
        }

        /// <summary>
        /// Switch to Automatic
        /// Author: Philipp Klein
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Automatic_Click(object sender, RoutedEventArgs e)
        {
            Gui.WAlgorithmSettings WAuto = new Gui.WAlgorithmSettings();
            this.Hide();
            WAuto.Show();
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
        private void Run_Copy_Click(object sender, RoutedEventArgs e)
        {
            Gui.WDllChooser WDll = new Gui.WDllChooser();
            WDll.Show();
        }

        /// <summary>
        /// Author: Philipp Klein
        /// </summary>
        private void getParameters()
        {
            
        }

        /// <summary>
        /// Author: Philipp Klein
        /// </summary>
        private void setParamters()
        {

        }
    }
}
