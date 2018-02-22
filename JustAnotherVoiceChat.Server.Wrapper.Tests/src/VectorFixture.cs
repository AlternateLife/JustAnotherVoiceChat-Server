/*
 * File: VectorFixture.cs
 * Date: 21.2.2018,
 *
 * MIT License
 *
 * Copyright (c) 2018 JustAnotherVoiceChat
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

using System.Collections.Generic;
using JustAnotherVoiceChat.Server.Wrapper.Math;
using NUnit.Framework;

namespace JustAnotherVoiceChat.Server.Wrapper.Tests
{
    [TestFixture]
    public class VectorFixture
    {

        [Test]
        public void ParameterlessVectorWillInitializeWithDefaultZero()
        {
            var vector = new Vector3();
            
            Assert.AreEqual(0, vector.X);
            Assert.AreEqual(0, vector.Y);
            Assert.AreEqual(0, vector.Z);
        }

        [Test]
        public void SingleParameterConstructorWillUseValueOnEveryDimension()
        {
            var vector = new Vector3(10);
            
            Assert.AreEqual(10, vector.X);
            Assert.AreEqual(10, vector.Y);
            Assert.AreEqual(10, vector.Z);
        }

        [Test]
        public void FullVectorConstructorWillSetValueOnDimensions()
        {
            var vector = new Vector3(1,3,7);
            
            Assert.AreEqual(1, vector.X);
            Assert.AreEqual(3, vector.Y);
            Assert.AreEqual(7, vector.Z);
        }

        [Test]
        public void TwoEqualVectorsAreComparedCorrectly()
        {
            var vector1 = new Vector3(1, 2, 3);
            var vector2 = new Vector3(1, 2, 3);
            
            Assert.AreEqual(true, vector1 == vector2);
            Assert.AreEqual(false, vector1 != vector2);
            
            var singleVector = new Vector3(1);
            var tripleVector = new Vector3(1, 1 ,1);
            
            Assert.AreEqual(true, singleVector == tripleVector);
            Assert.AreEqual(false, singleVector != tripleVector);
        }

        [Test]
        public void TheSameVectorIsAlwaysEqualToItself()
        {
            var vector = new Vector3(7, 11, 17);
            
            Assert.AreEqual(true, vector == vector);
        }

        [Test]
        public void TwoDifferentVectorsAreComparedCorrectly()
        {
            var vector1 = new Vector3(1, 2, 3);

            var unequalVectors = new List<Vector3>
            {
                new Vector3(1, 1, 1),
                new Vector3(1, 1, 2),
                new Vector3(1, 1, 3),
                new Vector3(1, 2, 1),
                new Vector3(1, 2, 2),
                // new Vector3(1, 2, 3), // Is equal, skip
                new Vector3(1, 3, 1),
                new Vector3(1, 3, 2),
                new Vector3(1, 3, 3),
                
                new Vector3(2, 1, 1),
                new Vector3(2, 1, 2),
                new Vector3(2, 1, 3),
                new Vector3(2, 2, 1),
                new Vector3(2, 2, 2),
                new Vector3(2, 2, 3),
                new Vector3(2, 3, 1),
                new Vector3(2, 3, 2),
                new Vector3(2, 3, 3),
                
                new Vector3(3, 1, 1),
                new Vector3(3, 1, 2),
                new Vector3(3, 1, 3),
                new Vector3(3, 2, 1),
                new Vector3(3, 2, 2),
                new Vector3(3, 2, 3),
                new Vector3(3, 3, 1),
                new Vector3(3, 3, 2),
                new Vector3(3, 3, 3)
            };

            foreach (var vector in unequalVectors)
            {
                Assert.AreEqual(false, vector1 == vector);
                Assert.AreEqual(true, vector1 != vector);
            }
        }

        [Test]
        public void LengthOfVectorCalculation()
        {
            var tripleVector = new Vector3(2, 2, 2);
            var mixedVector = new Vector3(3, 7, 9);
            
            Assert.AreEqual(System.Math.Sqrt(4 * 3), tripleVector.Length());
            Assert.AreEqual(System.Math.Sqrt((3 * 3) + (7 * 7) + (9 * 9)), mixedVector.Length());
            Assert.AreEqual(tripleVector.Length(), new Vector3().Distance(tripleVector));
        }

        [Test]
        public void NegativeVectorsAreCalculatedCorrectly()
        {
            var vector = new Vector3(-1, -1, -1);
            
            Assert.AreEqual(System.Math.Sqrt(3), vector.Length());
        }

        [Test]
        public void CalculateSumOfVector()
        {
            var vector1 = new Vector3(1, 2, 3);
            var vector2 = new Vector3(4, 5, 6);

            var sum = vector1 + vector2;
            
            Assert.AreEqual(5, sum.X);
            Assert.AreEqual(7, sum.Y);
            Assert.AreEqual(9, sum.Z);
        }

        [Test]
        public void CalculateDifferenceOfVector()
        {
            var vector1 = new Vector3(1, 2, 3);
            var vector2 = new Vector3(4, 5, 6);

            var sum = vector2 - vector1;
            
            Assert.AreEqual(3, sum.X);
            Assert.AreEqual(3, sum.Y);
            Assert.AreEqual(3, sum.Z);
        }

        [Test]
        public void CalculateMultipleOfTwoVectors()
        {
            var vector1 = new Vector3(1, 3, 7);
            var vector2 = new Vector3(5, 9, 11);

            var product = vector1 * vector2;
            
            Assert.AreEqual(5, product.X);
            Assert.AreEqual(27, product.Y);
            Assert.AreEqual(77, product.Z);
        }

        [Test]
        public void CalculateMultipleOfVectorAndFloat()
        {
            var vector = new Vector3(1, 3, 7);

            var product = vector * 5;
            
            Assert.AreEqual(5, product.X);
            Assert.AreEqual(15, product.Y);
            Assert.AreEqual(35, product.Z);
        }

        [Test]
        public void CalculateDivisionOfTwoVectors()
        {
            var vector1 = new Vector3(20, 27, 33);
            var vector2 = new Vector3(10, 9, 3);

            var result = vector1 / vector2;
            
            Assert.AreEqual(2, result.X);
            Assert.AreEqual(3, result.Y);
            Assert.AreEqual(11, result.Z);
        }

        [Test]
        public void CalculateDivisionOfVectorAndFloat()
        {
            var vector = new Vector3(20, 30, 50);

            var result = vector / 10;
            
            Assert.AreEqual(2, result.X);
            Assert.AreEqual(3, result.Y);
            Assert.AreEqual(5, result.Z);
        }
        
    }
}
