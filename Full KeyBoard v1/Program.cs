using System;
using System.Windows.Forms;

namespace KeyBoard
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Create form manually and keep it referenced
            var form = new Form1();

            // Run the app without forcing it to exit on form close
            Application.Run(form);
        }
    }
}