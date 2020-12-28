using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BitsetLibrary
{
    public class IntegerBitsetMatrix
    {
        private readonly int _columns;
        private readonly int _rows;

        public IntegerBitsetMatrix(int rows, int columns)
        {
            _columns = columns;
            _rows = rows;
            Matrix = new List<List<IntegerBitset>>();
            for (var i = 0; i < _columns; i++)
            {
                Matrix.Add(new List<IntegerBitset>());
                for (var j = 0; j < _rows; j++) Matrix[i].Add(new IntegerBitset());
            }
        }

        public List<List<IntegerBitset>> Matrix { get; set; }

        /// <summary>
        ///     Number of columns in matrix
        /// </summary>
        public int GetColumns()
        {
            return _columns;
        }

        /// <summary>
        ///     Number of rows in matrix
        /// </summary>
        public int GetRows()
        {
            return _rows;
        }

        /// <summary>
        ///     Transposes the matrix by it's main diagonal
        /// </summary>
        public void Transpose()
        {
            Matrix = Matrix.SelectMany(inner => inner.Select((item, index) => new {item, index}))
                .GroupBy(i => i.index, i => i.item)
                .Select(g => g.ToList())
                .ToList();
        }

        /// <summary>
        ///     Adds IntegerBitsetMatrix to this
        /// </summary>
        public void AddMatrix(IntegerBitsetMatrix additionMatrix)
        {
            if (additionMatrix == null) throw new ArgumentNullException(nameof(additionMatrix));
            if (_rows != additionMatrix.GetRows() || _columns != additionMatrix.GetColumns())
                throw new OperationCanceledException("Sizes of matrix are different");

            for (var i = 0; i < _columns; i++)
            for (var j = 0; j < _rows; j++)
                Matrix[i][j] = new IntegerBitset(IntegerBitsetUtils.BitArrayAdder(Matrix[i][j].GetBitArray(),
                    additionMatrix.Matrix[i][j].GetBitArray(), new BitArray(1, false), out _));
        }

        /// <summary>
        ///     Multiplies matrix by some number
        /// </summary>
        /// <param name="number">Multiplier</param>
        public void MultiplyByNumber(IntegerBitset number)
        {
            if (number == null) throw new ArgumentNullException(nameof(number));
            
            for (var i = 0; i < _columns; i++)
            for (var j = 0; j < _rows; j++)
                Matrix[i][j] = new IntegerBitset(
                    IntegerBitsetUtils.BitArrayTruncate(IntegerBitsetUtils.BitArrayMultiply(
                        Matrix[i][j].GetBitArray(), number.GetBitArray(), new BitArray(1, false), out _), 32));
        }

        /// <summary>
        ///     Raises matrix to some power
        /// </summary>
        /// <param name="power">Power</param>
        public void RaiseMatrixToPower(IntegerBitset power)
        {
            if (power == null) throw new ArgumentNullException(nameof(power));
            
            for (var i = 0; i < _columns; i++)
            for (var j = 0; j < _rows; j++)
            for (var k = 1; k < power.GetInt32(); k++)
                Matrix[i][j] = new IntegerBitset(
                    IntegerBitsetUtils.BitArrayTruncate(IntegerBitsetUtils.BitArrayMultiply(
                        Matrix[i][j].GetBitArray(), Matrix[i][j].GetBitArray(), new BitArray(1, false), out _), 32));
        }

        /// <summary>
        ///     Multiplies matrix by IntegerBitsetMatrix
        /// </summary>
        public void MultiplyByMatrix(IntegerBitsetMatrix multiplierMatrix)
        {
            if (multiplierMatrix == null) throw new ArgumentNullException(nameof(multiplierMatrix));
            
            var aRows = _rows;
            var aCols = _columns;
            var bRows = multiplierMatrix.GetRows();
            var bCols = multiplierMatrix.GetColumns();
            if (aCols != bRows)
                throw new Exception("Non-conformable matrices in MatrixProduct");
            var result = new IntegerBitsetMatrix(aRows, bCols);

            for (var i = 0; i < aCols; ++i) // each row of A
            for (var j = 0; j < bRows; ++j) // each col of B
            for (var k = 0; k < aRows; ++k)
                //Bit by Bit Multiplying 
                result.Matrix[i][j] = new IntegerBitset(
                    IntegerBitsetUtils.BitArrayAdder(
                        Matrix[i][j].GetBitArray(),
                        IntegerBitsetUtils.BitArrayTruncate(
                            IntegerBitsetUtils.BitArrayMultiply(
                                Matrix[i][k].GetBitArray(),
                                multiplierMatrix.Matrix[k][j].GetBitArray(),
                                new BitArray(1, false), out _), 32),
                        new BitArray(1, false), out _));

            Matrix = result.Matrix;
        }
    }
}