using System;
using System.Windows.Forms;
using IdleUserLogoff_WinForms.Properties;

namespace IdleUserLogoff_WinForms
{
  public partial class MainForm : Form
  {
    private readonly Timer _timer = new Timer();
    private uint _secondsLeft;
    private readonly bool _debugMode;

    public MainForm(uint secondsUntilLogoff, bool debugMode)
    {
      _secondsLeft = secondsUntilLogoff;
      _debugMode = debugMode;

      InitializeComponent();

      UpdateMessage();

      FlashWindowTaskbar.FlashWindowEx(this, FlashWindow.FLASHW_CAPTION | FlashWindow.FLASHW_TRAY);

      _timer.Tick += OnTick;
      _timer.Interval = 1000;
      _timer.Start();
    }

    private void OnTick(object sender, EventArgs args)
    {
      if (_secondsLeft == 1)
      {
        MainMessage.Text = Resources.LoggingOff_US_EN;

        if (_debugMode == false)
          PowerUtilities.ExitWindows(
            ExitWindows.LogOff | ExitWindows.Force,
            ShutdownReason.MinorOther, true);
      }
      else
      {
        --_secondsLeft;
        UpdateMessage();
      }
    }

    private void UpdateMessage()
    {
      MainMessage.Text = string.Format(
        Resources.LoggoffMessage_US_EN,
        _secondsLeft
      );
    }
  }
}