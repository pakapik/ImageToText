using System;
using System.Reflection;
using System.Windows.Forms;

namespace ImageToText
{
    public class Program
    {    
        [STAThread]
        public static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MessageHandlerForm());
        }
    }
}
