using System.Diagnostics;

namespace IdleUserLogoff_WinForms
{
  internal class RunningInstances
  {
    private readonly Process _self = Process.GetCurrentProcess();

    public void TerminateAll()
    {
      try
      {
        foreach (Process process in Process.GetProcessesByName(_self.ProcessName))
        {
          if (process.Id == _self.Id) continue;

          process.Kill();
          process.WaitForExit();
        }
      }
      catch (System.NullReferenceException)
      {
      }
    }
  }
}