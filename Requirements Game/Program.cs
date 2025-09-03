using System;
using System.Windows.Forms;

namespace Requirements_Game
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.ApplicationExit += (s, e) =>
            {
                LLMServerClient.Shutdown();
            };

            Application.Run(new Form1());
        }
    }
}