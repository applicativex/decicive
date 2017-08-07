using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnalyticHierarchyProcessDSS.Entities;

using Accord.Math;
using Accord.Math.Decompositions;
using AnalyticHierarchyProcessDSS.WolframEngine.Mathematica;

namespace AnalyticHierarchyProcessDSS.WolframEngine.Accord
{
    public class AccordEvaluationEngine : IEvaluationEngine
    {
        public IMinimizationStrategy CreatemMinimizationStrategy(string name)
        {
            if (name == "X2")
            {
                // todo: implement
                return new X2MinimizationStrategy(m => new double(), m => new double[0]);
            }
            else if (name == "LeastSquares")
            {
                // todo: implement
                return new LeastSquaresMinimizationStrategy(m => new double(), m => new double[0]);
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        public void Dispose()
        {
        }

        public EigenSystem GetEigenSystem(IMatrix<double> matrix)
        {
            var decomposition = GetGeneralizedEigenvalueDecomposition(matrix);
            return new EigenSystem(
                decomposition.RealEigenvalues, 
                Enumerable.Range(0, matrix.Size).Select(x => new EigenVector(decomposition.Eigenvectors.GetColumn(x))));
        }

        private static GeneralizedEigenvalueDecomposition GetGeneralizedEigenvalueDecomposition(IMatrix<double> matrix)
        {
            double[,] m = new double[matrix.Size, matrix.Size];
            for (int i = 0; i < matrix.Size; i++)
            {
                for (int j = 0; j < matrix.Size; j++)
                {
                    m[i, j] = matrix[i, j];
                }
            }
            var decomposition = new GeneralizedEigenvalueDecomposition(m, Matrix.Identity(matrix.Size));
            return decomposition;
        }

        public EigenPair GetMaxEigenPair(IMatrix<double> matrix)
        {
            var decomposition = GetGeneralizedEigenvalueDecomposition(matrix);
            (double value, double[] vector) maxEigenPair = decomposition.RealEigenvalues
                .Zip(Enumerable.Range(0, matrix.Size).Select(x => decomposition.Eigenvectors.GetColumn(x)),
                    (value, vector) => (value, vector)).OrderByDescending(x => x.Item1).FirstOrDefault();
            return new EigenPair(maxEigenPair.value, new EigenVector(maxEigenPair.vector));
        }

        public double[] IntervalPreferenceProgramming(double[,] matrix, double[] admissionParameters)
        {
            throw new NotImplementedException();
        }
    }
}
