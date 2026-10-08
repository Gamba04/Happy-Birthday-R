using System;
using System.Diagnostics;
using System.Threading;

public static class Program
{
	public const uint framerate = 60;
	public static uint frame;

	#region Init

	private static void Main()
	{
		Console.Title = "Happy Birthday R!";

		Init();
		Start();
	}

	private static void Init()
	{
		Screen.Init();
		Game.Init();
	}

	#endregion

	// ----------------------------------------------------------------------------------------------------

	#region Start

	public static void Start()
	{
		Stopwatch stopwatch = Stopwatch.StartNew();

		for (frame = 1; true; frame++)
		{
			TimeSpan startTime = stopwatch.Elapsed;

			Scene.Update();
			Screen.Render();

			WaitForTarget(stopwatch.Elapsed - startTime);
		}
	}

	private static void WaitForTarget(TimeSpan deltaTime)
	{
		TimeSpan target = new TimeSpan((long)(10000000d / framerate));

		if (target > deltaTime)
		{
			Thread.Sleep(target - deltaTime);
		}
	}

	#endregion

	// ----------------------------------------------------------------------------------------------------

	#region Other

	public static void Reset() => frame = 0;

	public static bool CheckInterval(uint interval)
	{
		return frame % interval == 0;
	}

	#endregion

}