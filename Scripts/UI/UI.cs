using System;

public static class UI
{
	private const ConsoleColor color = ConsoleColor.Gray;

	private static Text status;
	private static Text message;

	#region UI

	public static void Set(string status, string message)
	{
		int center = Screen.size.y / 2;

		UI.status = new Text(GetPosition(center + 1, status), status, color);
		UI.message = new Text(GetPosition(center - 1, message), message, color);
	}

	public static Vector2 GetPosition(int height, string text)
	{
		int center = Screen.size.x / 2;
		int radius = text.Length / 2;

		return new Vector2(center - radius, height);
	}

	public static void Reset()
	{
		status = null;
		message = null;
	}

	#endregion

	// ----------------------------------------------------------------------------------------------------

	#region Render

	public static void Render()
	{
		status?.Render();
		message?.Render();
	}

	#endregion

}