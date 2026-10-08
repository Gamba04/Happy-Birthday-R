using System;

public class Invader : Object
{
	private const ConsoleColor defaultColor = ConsoleColor.DarkGray;

	private const uint moveInterval = 120;
	private const uint freezeInterval = 220;

	private uint freezeCooldown;

	private bool IsFrozen => freezeCooldown > 0;

	private event Action<Invader> onDestroy;
	private event Action onInvade;

	#region Invader

	public Invader(Vector2 position, Action<Invader> onDestroy, Action onInvade) : base(position)
	{
		symbol = '+';
		color = defaultColor;

		this.onDestroy += onDestroy;
		this.onInvade += onInvade;
	}

	public override void Destroy()
	{
		base.Destroy();

		onDestroy?.Invoke(this);
	}

	#endregion

	// ----------------------------------------------------------------------------------------------------

	#region Update

	public override void Update()
	{
		UpdateMovement();
		UpdateFreeze();
	}

	private void UpdateMovement()
	{
		if (Game.IsRunning && !IsFrozen)
		{
			if (position.y > 0) Move();
			else onInvade?.Invoke();
		}
	}

	private void Move()
	{
		if (Program.CheckInterval(moveInterval))
		{
			position += Vector2.down;
		}
	}

	private void UpdateFreeze()
	{
		if (IsFrozen && --freezeCooldown == 0)
		{
			color = defaultColor;
		}
	}

	#endregion

	// ----------------------------------------------------------------------------------------------------

	#region Freeze

	public void Freeze(ConsoleColor color)
	{
		this.color = color;

		freezeCooldown = freezeInterval;
	}

	#endregion

}