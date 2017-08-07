using System;
using Xunit;
using Accord.Math;
using Accord.Math.Decompositions;

namespace Decicive.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            double[,] A =
            {
                {5, 6, 3},
                {-1, 0, 1},
                {1, 2, -1}
                //{0,2 },
                //{3,5 }
            };       //     var t = Matrix.
            var ev = new GeneralizedEigenvalueDecomposition(A, Matrix.Identity(3));
        }
    }
}
