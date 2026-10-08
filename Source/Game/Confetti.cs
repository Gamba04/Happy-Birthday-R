using System;

public class Confetti : Object
{
	private const uint moveInterval = 8;

	#region Confetti

	public Confetti(Vector2 position, ConsoleColor color) : base(position)
	{
		symbol = ',';
		this.color = color;
	}

	#endregion

	// ----------------------------------------------------------------------------------------------------

	#region Update

	public override void Update()
	{
		if (Program.CheckInterval(moveInterval))
		{
			position += Vector2.down;
		}
	}

	#endregion

}