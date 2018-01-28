namespace AlternateVoice.Server.Wrapper.Math
{
    public class Vector3
    {

        #region Fields

        public float X;

        public float Y;

        public float Z;

        #endregion

        #region Constructors

        public Vector3()
        {
            X = 0f;
            Y = 0f;
            Z = 0f;
        }

        public Vector3(float c)
        {
            X = c;
            Y = c;
            Z = c;
        }

        public Vector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        #endregion

        #region Methods

        public static double Distance(Vector3 vector1, Vector3 vector2)
        {
            return System.Math.Sqrt(
                (vector1.X - vector2.X) * (vector1.X - vector2.X) +
                (vector1.Y - vector2.Y) * (vector1.Y - vector2.Y) +
                (vector1.Z - vector2.Z) * (vector1.Z - vector2.Z)
            );
        }

        public double Distance(Vector3 vector)
        {
            return Distance(this, vector);
        }

        #endregion

        #region Overrides

        public override string ToString()
        {
            return $"[X: {X}, Y: {Y}, Z: {Z}]";
        }

        public override bool Equals(object obj)
        {
            var vector3 = obj as Vector3;
            if(vector3 != null)
                return this == vector3;
            return false;
        }

        public override int GetHashCode()
        {
            return (int) (X + Y + Z);
        }

        #endregion

        #region Operators

        public static bool operator ==(Vector3 left, Vector3 right)
        {
            return left.X == right.X &&
                   left.Y == right.Y &&
                   left.Z == right.Z;
        }

        public static bool operator !=(Vector3 left, Vector3 right)
        {
            return !(left == right);
        }

        public static Vector3 operator +(Vector3 left, Vector3 right)
        {
            left.X += right.X;
            left.Y += right.Y;
            left.Z += right.Z;
            return left;
        }

        public static Vector3 operator -(Vector3 left, Vector3 right)
        {
            left.X -= right.X;
            left.Y -= right.Y;
            left.Z -= right.Z;
            return left;
        }

        public static Vector3 operator *(Vector3 left, Vector3 right)
        {
            left.X *= right.X;
            left.Y *= right.Y;
            left.Z *= right.Z;
            return left;
        }

        public static Vector3 operator /(Vector3 left, Vector3 right)
        {
            left.X /= right.X;
            left.Y /= right.Y;
            left.Z /= right.Z;
            return left;
        }

        #endregion

    }
}