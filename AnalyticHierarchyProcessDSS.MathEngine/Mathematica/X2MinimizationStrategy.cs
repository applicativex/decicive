using System;
using System.Linq;
using AnalyticHierarchyProcessDSS.Entities;
using Wolfram.NETLink;

namespace AnalyticHierarchyProcessDSS.WolframEngine.Mathematica
{
    public class X2MinimizationStrategy : IMinimizationStrategy
    {
        private readonly Func<IMatrix<double>, double> _minimizationValue;
        private readonly Func<IMatrix<double>, double[]> _minimizationArguments;
        private readonly IKernelLink _kernel;

        public X2MinimizationStrategy(IKernelLink kernel)
        {
            _kernel = kernel;
        }

        public X2MinimizationStrategy(Func<IMatrix<double>, double> minimizationValue,
            Func<IMatrix<double>, double[]> minimizationArguments)
        {
            _minimizationValue = minimizationValue;
            _minimizationArguments = minimizationArguments;
        }

        public double MinimizationValue(IMatrix<double> matrix)
        {
            var queryString = string.Format(MathematicalConstants.OptimizationQueryTemplate, matrix, matrix.Size, MathematicalConstants.MinimumValue, MathematicalConstants.X2TargetFunction);

            var queryResult = _kernel.EvaluateToInputForm(queryString, 0);

            return double.Parse(queryResult);
        }

        public double[] MinimizationArguments(Entities.IMatrix<double> matrix)
        {
            var queryString = string.Format(
                MathematicalConstants.OptimizationQueryTemplate,
                matrix,
                matrix.Size,
                MathematicalConstants.ArgMinQuery,
                MathematicalConstants.X2TargetFunction);

            var queryResult = _kernel.EvaluateToInputForm(queryString, 0);

            var result =
                queryResult.Split(new[] { '{', '}', ',' }, StringSplitOptions.RemoveEmptyEntries).Select(double.Parse).ToArray();

            return result;
        }
    }
}