using System;
using System.Runtime.InteropServices;

// Resources
// https://docs.microsoft.com/en-us/windows/desktop/api/winuser/nf-winuser-getlastinputinfo
// http://www.pinvoke.net/default.aspx/user32.GetLastInputInfo
// http://www.pinvoke.net/default.aspx/Structures/LASTINPUTINFO.html

namespace IdleUserLogoff_WinForms
{
  internal static class UserInputInfo
  {
    [DllImport("user32.dll")]
    static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

    public static uint GetLastInputTime()
    {
      LASTINPUTINFO lastInputInfo = new LASTINPUTINFO();
      lastInputInfo.cbSize = (uint) Marshal.SizeOf(lastInputInfo);
      lastInputInfo.dwTime = 0;

      if (!GetLastInputInfo(ref lastInputInfo))
        return 0;

      var lastInputTick = lastInputInfo.dwTime;
      var envTicks = (uint) Environment.TickCount;
      var idleTime = envTicks - lastInputTick;

      return idleTime > 0 ? idleTime / 1000 : 0;
    }
  }

  [StructLayout(LayoutKind.Sequential)]
  internal struct LASTINPUTINFO
  {
    private static readonly int SizeOf = Marshal.SizeOf(typeof(LASTINPUTINFO));

    [MarshalAs(UnmanagedType.U4)] public UInt32 cbSize;
    [MarshalAs(UnmanagedType.U4)] public UInt32 dwTime;
  }
}