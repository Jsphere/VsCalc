using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CalcExample
{
    static class Program
    {
        /// <summary>
        /// The main entry polong for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
