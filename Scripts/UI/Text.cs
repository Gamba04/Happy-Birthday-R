using System;

public class Text
{
	private Vector2 position;
	private string text;
	private ConsoleColor color;

	public Text(Vector2 position, string text, ConsoleColor color)
	{
		this.position = position;
		this.text = text;
		this.color = color;
	}

	public void Render()
	{
		for (int i = 0; i < text.Length; i++)
		{
			Vector2 position = this.position + Vector2.right * i;
			char symbol = text[i];

			if (symbol != ' ')
			{
				Screen.Draw(position, symbol, color);
			}
		}
	}
}