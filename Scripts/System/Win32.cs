using System;
using System.Runtime.InteropServices;

public static class Win32
{
	private const string user32 = "user32.dll";
	private const string kernel32 = "kernel32.dll";

	#region Console

	#region Window

	[DllImport(kernel32)]
	public static extern IntPtr GetConsoleWindow();

	#region Center

	public enum SystemMetric
	{
		ScreenWidth = 0,
		ScreenHeight = 1
	}

	public enum PositionFlags : uint
	{
		NoSize = 0x0001
	}

	public struct Rect
	{
		public int left;
		public int top;
		public int right;
		public int bottom;
	}

	[DllImport(user32)]
	public static extern int GetSystemMetrics(SystemMetric metric);

	[DllImport(user32)]
	public static extern bool GetWindowRect(IntPtr window, out Rect rect);

	[DllImport(user32)]
	public static extern bool SetWindowPos(IntPtr window, IntPtr previousWindow, int x, int y, int width, int height, PositionFlags flags);

	#endregion

	// ----------------------------------------------------------------------------------------------------

	#region Resize

	public enum MenuControl
	{
		Minimize = 0xF020,
		Maximize = 0xF030,
		Resize = 0xF000
	}

	[DllImport(user32)]
	public static extern IntPtr GetSystemMenu(IntPtr window, bool revert = false);

	[DllImport(user32)]
	public static extern int DeleteMenu(IntPtr menu, MenuControl control, int flags = 0);

	#endregion

	// ----------------------------------------------------------------------------------------------------

	#region Close

	public enum Message : uint
	{
		Close = 0x0010
	}

	[DllImport(user32)]
	public static extern int SendMessage(IntPtr window, Message message, int wParam = 0, int lParam = 0);

	#endregion

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
	public static extern IntPtr GetForegroundWindow();

	[DllImport(user32)]
	public static extern short GetAsyncKeyState(Input.Key key);

	#endregion

}