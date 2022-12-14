using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using AntC.CodeGenerate.Model;

namespace AntC.CodeGenerate
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            TemplateManager.Init();
            ConfigHelper.Load();

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var mainForm = new Forms.MainForm();
            if (!mainForm.IsDisposed)
            {
                Application.Run(mainForm);
            }

            //ConfigHelper.Save();
        }
    }
}
