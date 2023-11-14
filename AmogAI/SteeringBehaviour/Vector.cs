namespace AmogAI.SteeringBehaviour;
public class Vector {
    public float X { get; set; }
    public float Y { get; set; }

    public Vector() : this(0, 0) { }

    public Vector(float x, float y) {
        X = x;
        Y = y;
    }

    public Vector(Vector v) {
        X = v.X;
        Y = v.Y;
    }

    public float Length() {
        return MathF.Sqrt(X * X + Y * Y);
    }

    public float LengthSquared() {
        return X * X + Y * Y;
    }

    public static Vector operator +(Vector v1, Vector v2) {
        Vector v = new Vector(v1.X, v1.Y);
        v.X += v2.X;
        v.Y += v2.Y;
        return v;
    }

    public static Vector operator -(Vector v1, Vector v2) {
        Vector v = new Vector(v1.X, v1.Y);
        v.X -= v2.X;
        v.Y -= v2.Y;
        return v;
    }

    public static Vector operator *(Vector vec, float val) {
        Vector vector = new Vector(vec.X, vec.Y);
        vector.X *= val;
        vector.Y *= val;
        return vector;
    }

    public static Vector operator *(float val, Vector vec) {
        return vec * val;
    }

    public static Vector operator /(Vector vec, float val) {
        Vector vector = new Vector(vec.X, vec.Y);
        vector.X /= val;
        vector.Y /= val;
        return vector;
    }

    public void Reset() {
        X = 0;
        Y = 0;
    }

    public float Dot(Vector v) {
        return X * v.X + Y * v.Y;
    }

    public Vector Normalize() {
        float magnitude = Length();
        Vector normalizedVector = new Vector(X, Y);
        normalizedVector.X /= magnitude;
        normalizedVector.Y /= magnitude;

        return normalizedVector;
    }

    public Vector Truncate(float max) {
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

    public Vector PerpNeg() {
        return new Vector(Y, -X);
    }

    public Vector Clone() {
        return new Vector(X, Y);
    }

    public override string ToString() {
        return string.Format("({0},{1})", X, Y);
    }

    public static bool LineIntersection(Vector a, Vector b, Vector c, Vector d, ref float dist, ref Vector point) {
        float rTop = (a.Y - c.Y) * (d.X - c.X) - (a.X - c.X) * (d.Y - c.Y);
        float rBot = (b.X - a.X) * (d.Y - c.Y) - (b.Y - a.Y) * (d.X - c.X);
        float sTop = (a.Y - c.Y) * (b.X - a.X) - (a.X - c.X) * (b.Y - a.Y);
        float sBot = (b.X - a.X) * (d.Y - c.Y) - (b.Y - a.Y) * (d.X - c.X);

        if ((rBot == 0) || (sBot == 0))
            return false;

        float r = rTop / rBot;
        float s = sTop / sBot;

        if ((r > 0) && (r < 1) && (s > 0) && (s < 1)) {
            dist = a.Distance(b) * r;

            point = a + r * (b - a);

            return true;
        } else {
            dist = 0;
            return false;
        }
    }

    public float Distance(Vector targetPos) {
        Vector vector2D = new Vector(targetPos.X - X, targetPos.Y - Y);
        return vector2D.Length();
    }

    public float DistanceSq(Vector targetPos) {
        Vector vector2D = new Vector(X - targetPos.X, Y - targetPos.Y);
        return vector2D.LengthSquared();
    }

    public static Vector PointToWorldSpace(Vector point, Vector heading, Vector side, Vector pos) {
        Vector transPoint = point;

        Matrix transformMatrix = new Matrix();
        transformMatrix *= Matrix.RotateMatrix(heading, side);
        transformMatrix *= Matrix.Translate(pos);
        transformMatrix.TransformVector(transPoint);

        return transPoint;
    }

    public static Vector RotateAroundOrigin(Vector vector, float degrees) {
        float radians = (float)Math.PI * degrees / 180.0f;
        float cosTheta = (float)Math.Cos(radians);
        float sinTheta = (float)Math.Sin(radians);

        float newX = vector.X * cosTheta - vector.Y * sinTheta;
        float newY = vector.X * sinTheta + vector.Y * cosTheta;

        return new Vector(newX, newY);
    }

    // Used for wrapping around the screen, probably not needed for this project but can stay here for now
    public static void WrapAround(Vector pos, int maxX, int maxY) {
        if (pos.X > maxX)
            pos.X = 0.0f;

        if (pos.X < 0)
            pos.X = maxX;

        if (pos.Y < 0)
            pos.Y = maxY;

        if (pos.Y > maxY)
            pos.Y = 0.0f;
    }

    public override bool Equals(Object? obj) {
        //Check for null and compare run-time types.
        if ((obj == null) || !this.GetType().Equals(obj.GetType())) {
            return false;
        } else {
            Vector vec = (Vector)obj;
            return this.X == vec.X && this.Y == vec.Y;
        }
    }
}