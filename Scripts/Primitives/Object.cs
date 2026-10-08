using System;

public abstract class Object
{
	public Vector2 position;
	public char symbol;
	public ConsoleColor color = ConsoleColor.Gray;

	#region Object

	public Object() => Scene.Add(this);

	public Object(Vector2 position) : this() => this.position = position;

	public virtual void Destroy() => Scene.Remove(this);

	#endregion

	// ----------------------------------------------------------------------------------------------------

	#region Update

	public virtual void Update() { }

	public void Render() => Screen.Draw(position, symbol, color);

	#endregion

	// ----------------------------------------------------------------------------------------------------

	#region Other

	protected bool Overlap<O>(out O result)
		where O : Object
	{
		return Scene.Overlap(position, out result);
	}

	public static implicit operator bool(Object obj) => obj != null;

	#endregion

}