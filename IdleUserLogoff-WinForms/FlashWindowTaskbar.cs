using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

// Resources
// https://docs.microsoft.com/en-us/windows/desktop/api/winuser/nf-winuser-flashwindowex
// https://www.pinvoke.net/default.aspx/user32.FlashWindowEx

namespace IdleUserLogoff_WinForms
{
  internal static class FlashWindowTaskbar
  {
    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    static extern bool FlashWindowEx(ref FLASHWINFO pwfi);

    public static bool FlashWindowEx(Form form, FlashWindow flags)
    {
      FLASHWINFO fInfo = new FLASHWINFO();

      fInfo.cbSize = Convert.ToUInt32(Marshal.SizeOf(fInfo));
      fInfo.hwnd = form.Handle;
      fInfo.dwFlags = (uint) flags;
      fInfo.uCount = UInt32.MaxValue;
      fInfo.dwTimeout = 0;

      return FlashWindowEx(ref fInfo);
    }
  }

  [StructLayout(LayoutKind.Sequential)]
  public struct FLASHWINFO
  {
    public UInt32 cbSize;
    public IntPtr hwnd;
    public UInt32 dwFlags;
    public UInt32 uCount;
    public UInt32 dwTimeout;
  }

  [Flags]
  public enum FlashWindow : uint
  {
    /// <summary>
    /// Stop flashing. The system restores the window to its original state.
    /// </summary>
    FLASHW_STOP = 0,

    /// <summary>
    /// Flash the window caption
    /// </summary>
    FLASHW_CAPTION = 1,

    /// <summary>
    /// Flash the taskbar button.
    /// </summary>
    FLASHW_TRAY = 2,

    /// <summary>
    /// Flash both the window caption and taskbar button.
    /// This is equivalent to setting the FLASHW_CAPTION | FLASHW_TRAY flags.
    /// </summary>
    FLASHW_ALL = 3,

    /// <summary>
    /// Flash continuously, until the FLASHW_STOP flag is set.
    /// </summary>
    FLASHW_TIMER = 4,

    /// <summary>
    /// Flash continuously until the window comes to the foreground.
    /// </summary>
    FLASHW_TIMERNOFG = 12
  }
}