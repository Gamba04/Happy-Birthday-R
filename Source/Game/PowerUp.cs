using System;

public enum Power
{
	Boost,
	Triple,
	Freeze,
	Bomb
}

public class PowerUp : Object
{
	private const uint moveInterval = 8;

	private static readonly Power[] powers = Enum.GetValues(typeof(Power)) as Power[];

	private readonly Random random = new Random();

	private readonly Power power;
	private readonly int direction;
	private readonly int limit;

	public event Action<Power, ConsoleColor, Vector2> onPower;

	#region Power Up

	public PowerUp(Action<Power, ConsoleColor, Vector2> onPower)
	{
		power = GetPower();

		symbol = '?';
		color = GetPowerColor(power);

		direction = GetDirection();
		limit = GetLimit(direction);
		position = GetStartPosition();

		this.onPower += onPower;
	}

	private Power GetPower()
	{
		int index = random.Next(powers.Length);

		return powers[index];
	}

	private ConsoleColor GetPowerColor(Power power)
	{
		return power switch
		{
			Power.Boost => ConsoleColor.Yellow,
			Power.Triple => ConsoleColor.Green,
			Power.Freeze => ConsoleColor.Cyan,
			Power.Bomb => ConsoleColor.Red,
			_ => ConsoleColor.Black
		};
	}

	private int GetDirection()
	{
		int value = random.Next(2);

		return value > 0 ? 1 : -1;
	}

	private int GetLimit(int direction)
	{
		return direction > 0 ? Screen.size.x : -1;
	}

	private Vector2 GetStartPosition()
	{
		int x = GetLimit(-direction);
		int y = random.Next(2, Screen.size.y - 2);

		return new Vector2(x, y);
	}

	#endregion

	// ----------------------------------------------------------------------------------------------------

	#region Update

	public override void Update()
	{
		if (position.x != limit) Move();
		else base.Destroy();
	}

	private void Move()
	{
		if (Program.CheckInterval(moveInterval))
		{
			position += Vector2.right * direction;
		}
	}

	#endregion

	// ----------------------------------------------------------------------------------------------------

	#region Activate

	public void Activate()
	{
		onPower?.Invoke(power, color, position);

		Destroy();
	}

	#endregion

}