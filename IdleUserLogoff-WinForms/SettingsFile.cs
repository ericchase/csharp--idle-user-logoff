using System.IO;
using System.Windows.Forms;

namespace IdleUserLogoff_WinForms
{
  static class SettingsFile
  {
    public static UserSettings Read(string filename)
    {
      string file;
      using (StreamReader reader = File.OpenText(filename))
      {
        file = reader.ReadToEnd();
        reader.Close();
      }

      UserSettings settings = new UserSettings();

      // remove any \r
      file = file.Replace('\r', ' ').Trim();

      foreach (string line in file.Split('\n'))
      {
        if (line.StartsWith("IdleTimerLength"))
        {
          settings.IdleTimerLength = TryParse(line, "IdleTimerLength", 60);
          continue;
        }

        if (line.StartsWith("LogoffCountdownLength"))
        {
          settings.LogoffCountdownLength = TryParse(line, "LogoffCountdownLength", 30);
          continue;
        }

        if (line.StartsWith("Debug"))
        {
          settings.DebugMode = true;
        }
      }

      return settings;
    }

    private static uint TryParse(string settingsValue, string settingsKey, uint defaultValue)
    {
      try
      {
        return uint.Parse(settingsValue.Split('=')[1].Trim());
      }
      catch (System.ArgumentNullException)
      {
        MessageBox.Show(
          $@"Setting '{settingsKey}' has no value.",
          Application.ProductName,
          MessageBoxButtons.OK,
          MessageBoxIcon.Exclamation
        );
      }
      catch (System.FormatException)
      {
        MessageBox.Show(
          $@"Setting '{settingsKey}' must be a single integer.",
          Application.ProductName,
          MessageBoxButtons.OK,
          MessageBoxIcon.Exclamation
        );
      }
      catch (System.OverflowException)
      {
        MessageBox.Show(
          $@"Setting '{settingsKey}' is too large.",
          Application.ProductName,
          MessageBoxButtons.OK,
          MessageBoxIcon.Exclamation
        );
      }
      catch (System.Exception)
      {
        MessageBox.Show(
          $@"Setting '{settingsKey}' caused unknown exception.",
          Application.ProductName,
          MessageBoxButtons.OK,
          MessageBoxIcon.Exclamation
        );
      }

      return defaultValue;
    }
  }

  internal class UserSettings
  {
    public bool DebugMode = false;
    public uint IdleTimerLength = 60;
    public uint LogoffCountdownLength = 30;
  }
}