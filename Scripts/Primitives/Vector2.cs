using System;

public struct Vector2
{
	public int x;
	public int y;

	public static readonly Vector2 zero = new Vector2(0, 0);
	public static readonly Vector2 one = new Vector2(1, 1);
	public static readonly Vector2 right = new Vector2(1, 0);
	public static readonly Vector2 left = new Vector2(-1, 0);
	public static readonly Vector2 up = new Vector2(0, 1);
	public static readonly Vector2 down = new Vector2(0, -1);

	public int Magnitude => (int)Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));

	public Vector2(int x, int y)
	{
		this.x = x;
		this.y = y;
	}

	public static Vector2 operator +(Vector2 a, Vector2 b) => new Vector2(a.x + b.x, a.y + b.y);

	public static Vector2 operator -(Vector2 a, Vector2 b) => new Vector2(a.x - b.x, a.y - b.y);

	public static Vector2 operator *(Vector2 a, int b) => new Vector2(a.x * b, a.y * b);

	public static Vector2 operator /(Vector2 a, int b) => new Vector2(a.x / b, a.y / b);

	public static bool operator ==(Vector2 a, Vector2 b) => a.x == b.x && a.y == b.y;

	public static bool operator !=(Vector2 a, Vector2 b) => a.x != b.x || a.y != b.y;

	public override bool Equals(object obj) => obj is Vector2 vector && vector == this;

	public override int GetHashCode() => base.GetHashCode();
}