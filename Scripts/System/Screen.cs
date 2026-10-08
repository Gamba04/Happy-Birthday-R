using System;

public static class Screen
{
	public static readonly Vector2 size = new Vector2(42, 20);

	private static readonly int bufferSize = GetBufferSize();

	private static readonly char[] textBuffer = InitTextBuffer();
	private static readonly ConsoleColor[] colorBuffer = InitColorBuffer();

	#region Init

	public static void Init()
	{
		DisableResize();
		DisableSelection();
	}

	private static void DisableResize()
	{
		IntPtr window = Win32.GetConsoleWindow();
		IntPtr menu = Win32.GetSystemMenu(window);

		Win32.DeleteMenu(menu, Win32.MenuControl.Resize);
	}

	private static void DisableSelection()
	{
		Console.TreatControlCAsInput = true;

		IntPtr handle = Win32.GetStdHandle(Win32.Handle.Input);

		if (Win32.GetConsoleMode(handle, out Win32.ConsoleMode mode))
		{
			mode &= ~Win32.ConsoleMode.QuickEdit;

			Win32.SetConsoleMode(handle, mode);
		}
	}

	#endregion

	// ----------------------------------------------------------------------------------------------------

	#region Buffers

	private static char[] InitTextBuffer()
	{
		char[] buffer = new char[bufferSize];

		for (int y = 0; y < size.y; y++)
		{
			int index = GetIndex(new Vector2(size.x, y));

			buffer[index] = '\n';
		}

		return buffer;
	}

	private static ConsoleColor[] InitColorBuffer()
	{
		return new ConsoleColor[bufferSize];
	}

	public static int GetBufferSize()
	{
		return GetIndex(size + new Vector2(1, -1));
	}

	public static int GetIndex(Vector2 position)
	{
		int width = size.x + 1;

		return position.y * width + position.x;
	}

	#endregion

	// ----------------------------------------------------------------------------------------------------

	#region Draw

	public static void Draw(Vector2 position, char symbol, ConsoleColor color)
	{
		position.y = size.y - 1 - position.y;

		if (Validate(position.x, size.x) && Validate(position.y, size.y))
		{
			int index = GetIndex(position);

			textBuffer[index] = symbol;
			colorBuffer[index] = color;
		}

		static bool Validate(int position, int size) => position >= 0 && position < size;
	}

	#endregion

	// ----------------------------------------------------------------------------------------------------

	#region Render

	public static void Render()
	{
		Prepare();
		Present();
		Clear();
	}

	private static void Prepare()
	{
		Vector2 size = Screen.size + Vector2.one;

		Console.SetWindowPosition(0, 0);
		Console.SetWindowSize(size.x, size.y);

		SetBufferSize(size);

		Console.SetCursorPosition(0, 0);
		Console.CursorVisible = false;
	}

	private static void SetBufferSize(Vector2 size)
	{
		try { Console.SetBufferSize(size.x, size.y); } catch { }
	}

	private static void Present()
	{
		int currentIndex = 0;
		ConsoleColor currentColor = ConsoleColor.DarkGray;

		for (int index = 0; index < bufferSize; index++)
		{
			ConsoleColor color = colorBuffer[index];

			if (color != currentColor || index == bufferSize - 1)
			{
				Console.ForegroundColor = currentColor;
				Console.Write(textBuffer, currentIndex, index - currentIndex);

				currentIndex = index;
				currentColor = color;
			}
		}

	}

	private static void Clear()
	{
		for (int i = 0; i < bufferSize; i++)
		{
			textBuffer[i] = ' ';
			colorBuffer[i] = ConsoleColor.DarkGray;
		}
	}

	#endregion

}