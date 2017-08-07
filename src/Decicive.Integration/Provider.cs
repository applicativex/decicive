using System;
using MathNet.Numerics.LinearAlgebra;

namespace Decicive.Integration
{
    public class Provider
    {
        public void GetEigenSystem(Matrix<double> matrix)
        {
            var evd = matrix.Evd();
        }
    }
}
