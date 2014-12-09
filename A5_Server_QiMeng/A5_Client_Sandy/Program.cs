using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using A5_Client_Sandy.SS;

namespace A5_Client_Sandy
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
        public static SandyServiceInterface getClient 
        {
            get 
            {
                return new SandyServiceInterfaceClient();
            }
        }
    }
}
