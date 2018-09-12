using System;
using System.Threading;
using System.Windows.Forms;
using IdleUserLogoff_WinForms.Properties;
using Timer = System.Windows.Forms.Timer;

namespace IdleUserLogoff_WinForms
{
  static class Program
  {
    private static Mutex _mutex;

    private static readonly Timer Timer = new Timer();

    private static UserSettings _settings;
    private static MainForm _mainForm;

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);

      _mutex = new Mutex(true, "IdleUserLogoff-ApplicationMutex", out var createdNew);

      if (createdNew)
      {
        _settings = SettingsFile.Read("settings.ini");

        Timer.Tick += OnTick;
        Timer.Interval = 1000;
        Timer.Start();

        Application.Run();
      }
      else
      {
        new RunningInstances().TerminateAll();
        MessageBox.Show(
          Resources.AllProcessesTerminated_US_EN,
          Application.ProductName,
          MessageBoxButtons.OK,
          MessageBoxIcon.Exclamation
        );
      }
    }

    private static void OnTick(object sender, EventArgs e)
    {
      if (UserInputInfo.GetLastInputTime() >= _settings.IdleTimerLength)
      {
        Timer.Tick -= OnTick;
        Timer.Tick += OnTick_Open;

        _mainForm = new MainForm(_settings.LogoffCountdownLength, _settings.DebugMode);
        _mainForm.Show();
      }
    }

    private static void OnTick_Open(object sender, EventArgs e)
    {
      if (UserInputInfo.GetLastInputTime() < _settings.IdleTimerLength)
      {
        Timer.Tick -= OnTick_Open;
        Timer.Tick += OnTick;

        _mainForm.Close();
      }
    }
  }
}