using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

/// <summary> This class allows you to manage a hotkey </summary>
public class GlobalHotkeys : IDisposable
{
    public const int MOD_ALT = 1;
    public const int MOD_CONTROL = 2;
    public const int MOD_SHIFT = 4;
    public const int MOD_WIN = 8;
    public const int WM_HOTKEY = 0x312;

    /// <summary>Handle of the current process</summary>
    public IntPtr Handle;

    public GlobalHotkeys()
    {
        Handle = Process.GetCurrentProcess().Handle;
    }

    /// <summary>The ID for the hotkey</summary>
    public short HotkeyID { get; private set; }

    public void Dispose()
    {
        UnregisterGlobalHotKey();
    }

    [DllImport("user32", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool RegisterHotKey(IntPtr hwnd, int id, uint fsModifiers, uint vk);

    [DllImport("user32", SetLastError = true)]
    public static extern int UnregisterHotKey(IntPtr hwnd, int id);

    [DllImport("kernel32", SetLastError = true)]
    public static extern short GlobalAddAtom(string lpString);

    [DllImport("kernel32", SetLastError = true)]
    public static extern short GlobalDeleteAtom(short nAtom);

    /// <summary>Register the hotkey</summary>
    public void RegisterGlobalHotKey(int hotkey, int modifiers, IntPtr handle)
    {
        UnregisterGlobalHotKey();
        Handle = handle;
        RegisterGlobalHotKey(hotkey, modifiers);
    }

    /// <summary>Register the hotkey</summary>
    public void RegisterGlobalHotKey(int hotkey, int modifiers)
    {
        UnregisterGlobalHotKey();

        try
        {
            // use the GlobalAddAtom API to get a unique ID (as suggested by MSDN)
            var atomName = Thread.CurrentThread.ManagedThreadId.ToString("X8") + GetType().FullName;
            HotkeyID = GlobalAddAtom(atomName);
            if (HotkeyID == 0)
                throw new Exception("Unable to generate unique hotkey ID. Error: " + Marshal.GetLastWin32Error());

            // register the hotkey, throw if any error
            if (!RegisterHotKey(Handle, HotkeyID, (uint) modifiers, (uint) hotkey))
                throw new Exception("Unable to register hotkey. Error: " + Marshal.GetLastWin32Error());
        }
        catch (Exception ex)
        {
            // clean up if hotkey registration failed
            Dispose();
            Console.WriteLine(ex);
        }
    }

    /// <summary>Unregister the hotkey</summary>
    public void UnregisterGlobalHotKey()
    {
        if (HotkeyID != 0)
        {
            UnregisterHotKey(Handle, HotkeyID);
            // clean up the atom list
            GlobalDeleteAtom(HotkeyID);
            HotkeyID = 0;
        }
    }
}