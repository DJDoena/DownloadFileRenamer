using System.Diagnostics;

namespace DoenaSoft.DownloadRenamer;

public static class Program
{
    [STAThread]
    public static void Main()
    {
        var processes = Process.GetProcessesByName("DownloadRenamer");

        if (processes.Length > 1)
        {
            MessageBox.Show("Already running!", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);

            return;
        }

        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new MainForm());
    }
}