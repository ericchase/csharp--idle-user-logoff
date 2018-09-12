using System;
using System.Runtime.InteropServices;

// Resources
// https://stackoverflow.com/questions/24726116/when-using-exitwindowsex-logoff-works-but-shutdown-and-restart-do-not
// Thanks to meziantou for this code.

namespace IdleUserLogoff_WinForms
{
  public static class TokenAdjuster
  {
    // PInvoke stuff required to set/enable security privileges
    private const int SE_PRIVILEGE_ENABLED = 0x00000002;
    private const int TOKEN_ADJUST_PRIVILEGES = 0X00000020;
    private const int TOKEN_QUERY = 0X00000008;
    private const int TOKEN_ALL_ACCESS = 0X001f01ff;
    private const int PROCESS_QUERY_INFORMATION = 0X00000400;

    [DllImport("advapi32", SetLastError = true), System.Security.SuppressUnmanagedCodeSecurity]
    private static extern int OpenProcessToken(
      IntPtr ProcessHandle, // handle to process
      int DesiredAccess, // desired access to process
      ref IntPtr TokenHandle // handle to open access token
    );

    [DllImport("kernel32", SetLastError = true),
     System.Security.SuppressUnmanagedCodeSecurity]
    private static extern bool CloseHandle(IntPtr handle);

    [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern int AdjustTokenPrivileges(
      IntPtr TokenHandle,
      int DisableAllPrivileges,
      IntPtr NewState,
      int BufferLength,
      IntPtr PreviousState,
      ref int ReturnLength);

    [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern bool LookupPrivilegeValue(
      string lpSystemName,
      string lpName,
      ref LUID lpLuid);

    public static bool EnablePrivilege(string lpszPrivilege, bool bEnablePrivilege)
    {
      bool retval = false;
      int ltkpOld = 0;
      IntPtr hToken = IntPtr.Zero;
      TOKEN_PRIVILEGES tkp = new TOKEN_PRIVILEGES {Privileges = new int[3]};
      LUID tLUID = new LUID();
      tkp.PrivilegeCount = 1;
      if (bEnablePrivilege)
        tkp.Privileges[2] = SE_PRIVILEGE_ENABLED;
      else
        tkp.Privileges[2] = 0;
      if (LookupPrivilegeValue(null, lpszPrivilege, ref tLUID))
      {
        System.Diagnostics.Process proc = System.Diagnostics.Process.GetCurrentProcess();
        if (proc.Handle != IntPtr.Zero)
        {
          if (OpenProcessToken(proc.Handle, TOKEN_ADJUST_PRIVILEGES | TOKEN_QUERY,
                ref hToken) != 0)
          {
            tkp.PrivilegeCount = 1;
            tkp.Privileges[2] = SE_PRIVILEGE_ENABLED;
            tkp.Privileges[1] = tLUID.HighPart;
            tkp.Privileges[0] = tLUID.LowPart;
            const int bufLength = 256;
            IntPtr tu = Marshal.AllocHGlobal(bufLength);
            Marshal.StructureToPtr(tkp, tu, true);
            if (AdjustTokenPrivileges(hToken, 0, tu, bufLength, IntPtr.Zero, ref ltkpOld) != 0)
            {
              // successful AdjustTokenPrivileges doesn't mean privilege could be changed
              if (Marshal.GetLastWin32Error() == 0)
              {
                retval = true; // Token changed
              }
            }

            Marshal.FreeHGlobal(tu);
          }
        }
      }

      if (hToken != IntPtr.Zero)
      {
        CloseHandle(hToken);
      }

      return retval;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct LUID
    {
      internal int LowPart;
      internal int HighPart;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct LUID_AND_ATTRIBUTES
    {
      private LUID Luid;
      private int Attributes;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct TOKEN_PRIVILEGES
    {
      internal int PrivilegeCount;

      [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
      internal int[] Privileges;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct _PRIVILEGE_SET
    {
      private int PrivilegeCount;
      private int Control;

      [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
      // ANYSIZE_ARRAY = 1
      private LUID_AND_ATTRIBUTES[] Privileges;
    }
  }
}