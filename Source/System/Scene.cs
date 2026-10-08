using System.Collections.Generic;

public static class Scene
{
	private static readonly List<Object> objects = new List<Object>();

	#region Objects

	public static void Add(Object obj) => objects.Add(obj);

	public static void Remove(Object obj) => objects.Remove(obj);

	public static void Flush() => objects.Clear();

	#endregion

	// ----------------------------------------------------------------------------------------------------

	#region Update

	public static void Update()
	{
		Game.Update();

		foreach (Object obj in objects.ToArray())
		{
			obj.Update();
			obj.Render();
		}

		UI.Render();
	}

	#endregion

	// ----------------------------------------------------------------------------------------------------

	#region Other

	public static bool Overlap<O>(Vector2 position, out O result)
	where O : Object
	{
		foreach (Object obj in objects.ToArray())
		{
			if (obj.position == position && obj is O o)
			{
				return result = o;
			}
		}

		return result = null;
	}

	#endregion

}