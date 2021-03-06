﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;

/// Author: Luisa Andre

namespace Gui.Classes
{
    /// <summary>
    /// Reads out Function-Names of chosen .xml File
    /// </summary>
    class FunctionChooser
    {
        private DllLoader dl = new DllLoader();


        /// <summary>
        /// Returns function-names
        /// </summary>
        /// <param name="filePath">Path of .xml-file</param>
        /// <returns>List<string> of function-names</string></returns>
        public string[] getFunctions(string filePath)
        {
            string[] functions = null;

            try
            {
                functions = dl.loadDll(filePath);
            }
            catch(Exception ex){
                MessageBox.Show("Not a valid File selected: " + ex.Message);
            }

            return functions;
        }

        public esFunction getFunction(string fname) {
            return dl.loadFunction(fname);
        }
       
    }
}
