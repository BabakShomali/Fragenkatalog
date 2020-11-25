using System;
using System.Windows.Forms;

namespace Fragenkatalog
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Applikationsweite Einstellungen
            AppStatus.Datenhaltungsadapter = new Datenhaltung.DB.MySql.Adapter();

            // Kommentar
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
