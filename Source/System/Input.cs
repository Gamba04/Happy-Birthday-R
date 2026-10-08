using System;

public static class Input
{
	public enum Key
	{
		A = 0x41,
		D = 0x44,
		Space = 0x20,
		R = 0x52,
		Ctrl = 0x11,
		W = 0x57,
		Alt = 0x12,
		F4 = 0x73
	}

	public static bool GetKey(Key key)
	{
		if (IsFocused())
		{
			short value = Win32.GetAsyncKeyState(key);

			return (value & 0x8000) != 0;
		}

		return false;
	}

	private static bool IsFocused()
	{
		IntPtr focus = Win32.GetForegroundWindow();

		return Screen.window == focus;
	}
}