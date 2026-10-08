using System;
using System.Collections.Generic;

public static class Game
{
	private static readonly string[] spawn = new string[]
	{
		"+++  +++  +++++++   +++++++   ++++++++ ",
		"+++  +++  ++++++++  ++++++++  +++++++++",
		"+++  +++  +++  +++  +++  +++  +++   +++",
		"++++++++  +++++++   +++  +++  ++++++++ ",
		"++++++++  +++++++   +++  +++  ++++++++ ",
		"+++  +++  +++  +++  +++  +++  +++   +++",
		"+++  +++  ++++++++  ++++++++  +++   +++",
		"+++  +++  +++++++   +++++++   +++   +++",
	};

	private static readonly List<Invader> invaders = new List<Invader>();
	private static Player player;

	private static readonly Random random = new Random();
	private static uint powerUpsCooldown;

	private static bool gameOver;
	private static bool confetti;

	public static bool IsRunning => !gameOver;

	#region Init

	public static void Init()
	{
		player = new Player();

		CreateInvaders();
		ResetPowerUps();
	}

	private static void CreateInvaders()
	{
		Vector2 position = Vector2.up * (Screen.size.y - 2);

		for (int y = 0; y < spawn.Length; y++)
		{
			position.x = (int)Math.Round((Screen.size.x - spawn[y].Length) / 2d);

			for (int x = 0; x < spawn[y].Length; x++)
			{
				if (spawn[y][x] != ' ')
				{
					CreateInvader(position);
				}

				position.x++;
			}

			position.y--;
		}
	}

	private static void CreateInvader(Vector2 position)
	{
		Invader invader = new Invader(position, OnDestroy, OnInvade);

		invaders.Add(invader);
	}

	private static void ResetPowerUps()
	{
		powerUpsCooldown = (uint)random.Next(400, 601);
	}

	#endregion

	// ----------------------------------------------------------------------------------------------------

	#region Update

	public static void Update()
	{
		UpdatePowerUps();
		UpdateConfetti();
		UpdateGameOver();
	}

	private static void UpdatePowerUps()
	{
		if (IsRunning)
		{
			if (powerUpsCooldown == 0)
			{
				new PowerUp(OnPower);

				ResetPowerUps();
			}
			else powerUpsCooldown--;
		}
	}

	private static void UpdateConfetti()
	{
		const int interval = 10;

		if (confetti && Program.CheckInterval(interval))
		{
			Vector2 position = GetPosition();
			ConsoleColor color = GetColor();

			new Confetti(position, color);
		}

		static Vector2 GetPosition()
		{
			return new Vector2(random.Next(Screen.size.x), Screen.size.y);
		}

		static ConsoleColor GetColor()
		{
			return (ConsoleColor)random.Next((int)ConsoleColor.Blue, (int)ConsoleColor.Yellow + 1);
		}
	}

	private static void UpdateGameOver()
	{
		if (gameOver && Input.GetKey(Input.Key.R))
		{
			Restart();
		}
	}

	private static void Restart()
	{
		gameOver = false;
		confetti = false;

		powerUpsCooldown = 0;

		invaders.Clear();
		Scene.Flush();
		UI.Reset();
		Program.Reset();

		Init();
	}

	#endregion

	// ----------------------------------------------------------------------------------------------------

	#region Invaders

	private static void OnDestroy(Invader invader)
	{
		invaders.Remove(invader);

		if (invaders.Count == 0)
		{
			gameOver = true;

			Victory();
		}
	}

	private static void OnInvade()
	{
		if (IsRunning)
		{
			gameOver = true;

			Invasion();
		}
	}

	#endregion

	// ----------------------------------------------------------------------------------------------------

	#region Powers

	private static void OnPower(Power power, ConsoleColor color, Vector2 position)
	{
		switch (power)
		{
			case Power.Boost: player.SetBoost(color); break;
			case Power.Triple: player.SetTriple(color); break;
			case Power.Freeze: Freeze(color); break;
			case Power.Bomb: Bomb(position); break;
		}
	}

	private static void Freeze(ConsoleColor color)
	{
		foreach (Invader invader in invaders)
		{
			invader.Freeze(color);
		}
	}

	private static void Bomb(Vector2 position)
	{
		const int radius = 5;

		foreach (Invader invader in invaders.ToArray())
		{
			Vector2 relativePosition = invader.position - position;
			relativePosition.x /= 2;

			if (relativePosition.Magnitude < radius)
			{
				invader.Destroy();
			}
		}
	}

	#endregion

	// ----------------------------------------------------------------------------------------------------

	#region Game Over

	private static void Victory()
	{
		UI.Set("YOU WIN", "PRESS R TO RESTART");

		confetti = true;
	}

	private static void Invasion()
	{
		UI.Set("GAME OVER", "PRESS R TO RETARD");
	}

	#endregion

}