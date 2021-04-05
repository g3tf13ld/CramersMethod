using System;
using CramersSolution.Models;

namespace CramersSolution {
    public class LinearSystem {
        
        private double[,] initialAMatrix;
        private double[,] aMatrix;
        private double[] initialBVector;
        private double[] bVector;
        private double[] xVector;
        
        private double eps;
        private int size;
        
        public double[] XVector => xVector;

        public LinearSystem(double[,] aMatrix, double[] bVector)
            : this(aMatrix, bVector, 0.0001) {
        }

        private LinearSystem(double[,] aMatrix, double[] bVector, double eps) {
            if (aMatrix == null || bVector == null)
                throw new ArgumentNullException("One or more params are null.");

            var bLength = bVector.Length;
            var aLength = aMatrix.Length;
            if (aLength != bLength * bLength)
                throw new ArgumentException("The rows and columns number in A-matrix must match the elements number in B-vector");
            
            // Save the initial A-matrix.
            initialAMatrix = aMatrix;  
            // A-matrix copy for use in calculations.
            this.aMatrix = (double[,])aMatrix.Clone(); 
            // Save the initial B-vector.
            initialBVector = bVector;  
            // B-vector copy for use in calculations.
            this.bVector = (double[])bVector.Clone();  
            xVector = new double[bLength];
            size = bLength;
            this.eps = eps;
            
            xVector = CramerSolve(aMatrix, bVector);
        }
        
        // --------------------- Solving methods ---------------------- //

        private double[] CramerSolve(double[,] A, double[] B)
        {
            double det = GetDeterminant(A);
            double[] dets = new double[size];
            double[] el = new double[size];
            for (int j = 0; j < size; j++)
            {
                for (int i = 0; i < size; i++)
                {
                    el[i] = A[i, j];
                    A[i, j] = B[i];
                }
                dets[j] = GetDeterminant(A);
                for (int i = 0; i < size; i++)
                {
                    A[i, j] = el[i];
                }
            }
            double[] results = new double[size];
            for (int i = 0; i < size; i++)
            {
                results[i] = dets[i] / det;
            }
            return results;
        }
        
        // Determinant calculating
        private static double GetDeterminant(double[,] matrix)
        {
            if(matrix.Length == 4)
            {
                return matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];
            }
            double sign = 1, result = 0;
            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                double[,] minor = GetMinor(matrix, i);
                result += sign * matrix[0, i] * GetDeterminant(minor);
                sign = -sign;
            }
            
            return result;
        }
        
        // Minor search
        private static double[,] GetMinor(double[,] matrix, int n)
        {
            double[,] result = new double[matrix.GetLength(0) - 1, matrix.GetLength(0) - 1];
            for (int i = 1; i < matrix.GetLength(0); i++)
            {
                for (int j = 0, col = 0; j < matrix.GetLength(1); j++)
                {
                    if (j == n)
                        continue;
                    result[i - 1, col] = matrix[i, j];
                    col++;
                }
            }
            return result;
        }
    }
}
