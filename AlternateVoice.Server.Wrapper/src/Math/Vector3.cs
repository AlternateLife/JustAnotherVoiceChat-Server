#region copyright 
/*
 * File: Vector3.cs
 * Date: 29.1.2018,
 *
 * MIT License
 *
 * Copyright (c) 2018 AlternateVoice
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */
#endregion
namespace AlternateVoice.Server.Wrapper.Math
{
    public class Vector3
    {

        public float X { get; set; }

        public float Y { get; set; }

        public float Z { get; set; }
        
        public Vector3()
        {
            X = 0f;
            Y = 0f;
            Z = 0f;
        }

        public Vector3(float coordinate)
        {
            X = coordinate;
            Y = coordinate;
            Z = coordinate;
        }

        public Vector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

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

        public double Length()
        {
            return System.Math.Sqrt(
                X * X +
                Y * Y +
                Z * Z
            );
        }

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
            return new Vector3(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
        }

        public static Vector3 operator -(Vector3 left, Vector3 right)
        {
            return new Vector3(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
        }

        public static Vector3 operator *(Vector3 left, Vector3 right)
        {
            return new Vector3(left.X * right.X, left.Y * right.Y, left.Z * right.Z);
        }

        public static Vector3 operator /(Vector3 left, Vector3 right)
        {
            return new Vector3(left.X / right.X, left.Y / right.Y, left.Z / right.Z);
        }

    }
}