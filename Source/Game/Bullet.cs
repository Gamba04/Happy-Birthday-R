using System;

public class Bullet : Object
{
	private Vector2 direction;

	#region Bullet

	public Bullet(char symbol, Vector2 position, Vector2 direction) : base(position)
	{
		this.symbol = symbol;
		this.direction = direction;

		color = ConsoleColor.Yellow;
	}

	#endregion

	// ----------------------------------------------------------------------------------------------------

	#region Update

	public override void Update()
	{
		UpdateMovement();
		UpdateCollisions();
		UpdateBounds();
	}

	private void UpdateMovement()
	{
		position += direction;
	}

	private void UpdateCollisions()
	{
		if (Overlap(out Invader invader))
		{
			invader.Destroy();
		}
		else if (Overlap(out PowerUp powerUp))
		{
			powerUp.Activate();
		}
		else return;

		Destroy();
	}

	private void UpdateBounds()
	{
		if (position.y >= Screen.size.y)
		{
			Destroy();
		}
	}

	#endregion

}