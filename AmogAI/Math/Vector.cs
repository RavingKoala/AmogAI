namespace AmogAI.Math;
public class Vector {
	public double X { get; set; }
	public double Y { get; set; }

	public Vector() : this(0, 0) { }

	public Vector(double x, double y) {
		X = x;
		Y = y;
	}

	public Vector(Vector v) {
		X = v.X;
		Y = v.Y;
	}

	public double Length() {
		return System.Math.Sqrt(X * X + Y * Y);
	}

	public double LengthSquared() {
		return (X * X + Y * Y);
	}

	public static Vector operator +(Vector v1, Vector v2) {
		v1.X += v2.X;
		v1.Y += v2.Y;
		return v1;
	}

	public static Vector operator -(Vector v1, Vector v2) {
		v1.X -= v2.X;
		v1.Y -= v2.Y;
		return v1;
	}

	public static Vector operator *(Vector v, double value) {
		v.X *= value;
		v.Y *= value;
		return v;
	}

	public static Vector operator /(Vector v, double value) {
		v.X /= value;
		v.Y /= value;
		return v;
	}

	public void Reset() {
		X = 0;
		Y = 0;
	}

	public double Dot(Vector v) {
		return X * v.X + Y * v.Y;
	}

	public Vector Normalize() {
		double magnitude = Length();
		Vector normalizedVector = new Vector(X, Y);
		normalizedVector.X /= magnitude;
		normalizedVector.Y /= magnitude;

		return normalizedVector;
	}

	public Vector Truncate(double max) {
		if (Length() > max) {
			Vector v = Normalize();
			X = v.X * max;
			Y = v.Y * max;
		}
		return this;
	}

	public Vector Perp() {
		return new Vector(-Y, X);
	}

	public Vector Clone() {
		return new Vector(X, Y);
	}

	public override string ToString() {
		return string.Format("({0},{1})", X, Y);
	}

	public double Distance(Vector targetPos) {
		Vector vector2D = new Vector(X - targetPos.X, Y - targetPos.Y);
		return vector2D.Length();
	}

	public double DistanceSq(Vector targetPos) {
		Vector vector2D = new Vector(X - targetPos.X, Y - targetPos.Y);
		return vector2D.LengthSquared();
	}

	public static void WrapAround(Vector pos, int maxX, int maxY) {
		if (pos.X > maxX)
			pos.X = 0.0;

		if (pos.X < 0)
			pos.X = maxX;

		if (pos.Y < 0)
			pos.Y = maxY;

		if (pos.Y > maxY)
			pos.Y = 0.0;
	}
}
