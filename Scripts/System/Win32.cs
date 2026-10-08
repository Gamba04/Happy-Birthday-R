using System;
using System.Runtime.InteropServices;

public static class Win32
{
	private const string user32 = "user32.dll";
	private const string kernel32 = "kernel32.dll";

	#region Console

	#region Window

	public enum MenuControl
	{
		Minimize = 0xF020,
		Maximize = 0xF030,
		Resize = 0xF000
	}

	[DllImport(kernel32)]
	public static extern IntPtr GetConsoleWindow();

	[DllImport(user32)]
	public static extern IntPtr GetSystemMenu(IntPtr window, bool revert = false);

	[DllImport(user32)]
	public static extern int DeleteMenu(IntPtr menu, MenuControl control, int flags = 0);

	#endregion

	// ----------------------------------------------------------------------------------------------------

	#region Selection

	public enum Handle : uint
	{
		Input = unchecked((uint)-10)
	}

	public enum ConsoleMode
	{
		QuickEdit = 0x0040
	}

	[DllImport(kernel32)]
	public static extern IntPtr GetStdHandle(Handle handle);

	[DllImport(kernel32)]
	public static extern bool GetConsoleMode(IntPtr handle, out ConsoleMode mode);

	[DllImport(kernel32)]
	public static extern bool SetConsoleMode(IntPtr handle, ConsoleMode mode);

	#endregion

	#endregion

	// ----------------------------------------------------------------------------------------------------

	#region Input

	[DllImport(user32)]
	public static extern short GetAsyncKeyState(Input.Key key);

	#endregion

}