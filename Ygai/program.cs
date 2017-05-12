namespace Ygai
{
    using System;
    using System.Windows.Forms;

    internal static class program
    {
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormDbConnect());
            //Application.Run(new Form1("localhost","ygai","root","2602753"));
        }
    }
}

