using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gui.Classes
{
    class DllChooser
    {
        string xmlPath = null;
        Microsoft.Win32.OpenFileDialog dlg;


        /// <summary>
        /// Constructor
        /// </summary>
        public DllChooser()
        {
            dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = "Document";
            dlg.DefaultExt = ".xml";
            dlg.Filter = "Extensible Markup Language (.xml)|*.xml";
        }

        /// <summary>
        /// Opens Explorer and reads XML-Path;
        /// </summary>
        /// <returns>XML-File Path</returns>
        public string openFileDialog()
        {
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                xmlPath = dlg.FileName;
            }
            else
            {
                xmlPath = null;
            }

            return xmlPath;
        }
    }
}
