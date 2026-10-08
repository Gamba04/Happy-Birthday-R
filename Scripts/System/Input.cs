using System;
using System.Threading;

public static class Input
{
	public enum Key
	{
		W = 0x57,
		A = 0x41,
		S = 0x53,
		D = 0x44,
		Space = 0x20,
		R = 0x52
	}

	public static bool GetKey(Key key)
	{
		short value = Win32.GetAsyncKeyState(key);

		return (value & 0x8000) != 0;
	}

	#region Debug

	public static void Debug()
	{
		while (true)
		{
			Reset();
			Draw();
			Wait();
		}
	}

	private static void Reset()
	{
		Console.SetCursorPosition(0, 0);
	}

	private static void Draw()
	{
		Draw(" W\n", Key.W);
		Draw("A", Key.A);
		Draw("S", Key.S);
		Draw("D", Key.D);
	}

	private static void Draw(string text, Key key)
	{
		bool isPressed = GetKey(key);

		Console.ForegroundColor = isPressed ? ConsoleColor.Yellow : ConsoleColor.DarkGray;
		Console.Write(text);
	}

	private static void Wait()
	{
		Thread.Sleep(16);
	}

	#endregion

}