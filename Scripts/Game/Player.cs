using System;

public class Player : Object
{
	private const ConsoleColor defaultColor = ConsoleColor.Cyan;

	private const uint shootInterval = 8;
	private const uint boostInterval = 1;

	private const uint boostPowerInterval = 90;
	private const uint triplePowerInterval = 150;

	private uint shootCooldown;
	private uint powerCooldown;

	private bool boost;
	private bool triple;

	private bool HasPower => boost || triple;

	#region Player

	public Player() : base(GetStartPosition())
	{
		symbol = '^';
		color = defaultColor;
	}

	private static Vector2 GetStartPosition()
	{
		return Vector2.right * (Screen.size.x / 2);
	}

	#endregion

	// ----------------------------------------------------------------------------------------------------

	#region Update

	public override void Update()
	{
		if (Game.IsRunning)
		{
			UpdateInput();
			UpdatePower();
			UpdateCooldowns();
		}
	}

	private void UpdateInput()
	{
		if (Input.GetKey(Input.Key.A)) Move(Vector2.left);
		if (Input.GetKey(Input.Key.D)) Move(Vector2.right);
		if (Input.GetKey(Input.Key.Space)) Shoot();
	}

	private void Move(Vector2 direction)
	{
		position += direction;
	}

	private void Shoot()
	{
		if (shootCooldown == 0)
		{
			SpawnBullets();

			shootCooldown = boost ? boostInterval : shootInterval;
		}
	}

	private void SpawnBullets()
	{
		new Bullet('|', position, Vector2.up);

		if (triple)
		{
			new Bullet('\\', position, new Vector2(-1, 1));
			new Bullet('/', position, new Vector2(1, 1));
		}
	}

	private void UpdatePower()
	{
		if (HasPower && powerCooldown == 0)
		{
			ResetPower();
		}
	}

	private void UpdateCooldowns()
	{
		UpdateCooldown(ref shootCooldown);
		UpdateCooldown(ref powerCooldown);
	}

	private void UpdateCooldown(ref uint cooldown)
	{
		if (cooldown > 0) cooldown--;
	}

	#endregion

	// ----------------------------------------------------------------------------------------------------

	#region Powers

	public void SetBoost(ConsoleColor color)
	{
		boost = true;
		powerCooldown = boostPowerInterval;

		base.color = color;
	}

	public void SetTriple(ConsoleColor color)
	{
		triple = true;
		powerCooldown = triplePowerInterval;

		base.color = color;
	}

	private void ResetPower()
	{
		boost = false;
		triple = false;

		color = defaultColor;
	}

	#endregion

}