using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BitsetLibrary
{
    public class IntegerBitsetMatrix
    {
        public List<List<IntegerBitset>> Matrix { get; set; }

        private int _columns;
        private int _rows;
        public IntegerBitsetMatrix(int rows, int columns)
        {
            _columns = columns;
            _rows = rows;
            Matrix = new List<List<IntegerBitset>>();
            for (var i = 0; i < _columns; i++)
            {
                Matrix.Add(new List<IntegerBitset>());
                for (var j = 0; j < _rows; j++)
                {
                    Matrix[i].Add(new IntegerBitset());
                } 
            }
        }

        public int GetColumns() => _columns;
        public int GetRows() => _rows;
        
        public void Transpose()
        {
            Matrix = Matrix.SelectMany(inner => inner.Select((item, index) => new { item, index }))
                .GroupBy(i => i.index, i => i.item)
                .Select(g => g.ToList())
                .ToList();
        }

        public void AddMatrix(IntegerBitsetMatrix additionMatrix)
        {
            if (additionMatrix == null) throw new ArgumentNullException(nameof(additionMatrix));
            if (_rows != additionMatrix.GetRows() || _columns != additionMatrix.GetColumns())
                throw new OperationCanceledException("Sizes of matrix are different");
            
            for (var i = 0; i < _columns; i++)
            {
                for (var j = 0; j < _rows; j++)
                {
                    Matrix[i][j] = new IntegerBitset(IntegerBitsetUtils.BitArrayAdder(Matrix[i][j].GetBitArray(),
                        additionMatrix.Matrix[i][j].GetBitArray(), new BitArray(1, false), out _));
                }
            }
        }

        public void MultiplyByNumber(IntegerBitset number)
        {
            for (var i = 0; i < _columns; i++)
            {
                for (var j = 0; j < _rows; j++)
                {
                    Matrix[i][j] = new IntegerBitset(
                        IntegerBitsetUtils.BitArrayTruncate(IntegerBitsetUtils.BitArrayMultiply(
                            Matrix[i][j].GetBitArray(), number.GetBitArray(),new BitArray(1,false),out _), 32));
                }
            }
        }
        
        public void RaiseMatrixToPower(IntegerBitset power)
        {
            for (var i = 0; i < _columns; i++)
            {
                for (var j = 0; j < _rows; j++)
                {
                    for (var k = 1; k < power.GetInt32(); k++)
                    {
                        Matrix[i][j] = new IntegerBitset(
                            IntegerBitsetUtils.BitArrayTruncate(IntegerBitsetUtils.BitArrayMultiply(
                                Matrix[i][j].GetBitArray(), Matrix[i][j].GetBitArray(), new BitArray(1, false), out _), 32));
                    }
                }
            }
        }

        public void MultiplyByMatrix(IntegerBitsetMatrix multiplierMatrix)
        {
            var aRows = _rows; var aCols = _columns;
            var bRows = multiplierMatrix.GetRows(); var bCols = multiplierMatrix.GetColumns();
            if (aCols != bRows)
                throw new Exception("Non-conformable matrices in MatrixProduct");
            var result = new IntegerBitsetMatrix(aRows, bCols);
            
            for (var i = 0; i < aCols; ++i) // each row of A
                for (var j = 0; j < bRows; ++j) // each col of B
                    for (var k = 0; k < aRows; ++k)
                    {
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
                    }

            Matrix = result.Matrix;
        }

    }
}