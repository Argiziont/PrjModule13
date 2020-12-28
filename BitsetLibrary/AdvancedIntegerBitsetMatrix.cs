using System;
using BitsetLibrary.Interfaces;

namespace BitsetLibrary
{
    public class AdvancedIntegerBitsetMatrix : IntegerBitsetMatrix, IPrintable
    {
        public AdvancedIntegerBitsetMatrix(int rows, int columns) : base(rows, columns)
        {
        }

        /// <summary>
        ///     Prints 2d Matrix in Int32 representation
        /// </summary>
        public void PrintInt32()
        {
            foreach (var column in Matrix)
            {
                foreach (var row in column) Console.Write(row.GetInt32() + " ");
                Console.WriteLine();
            }
        }

        /// <summary>
        ///     Prints 2d Matrix in Bit representation
        /// </summary>
        public void PrintInBits()
        {
            foreach (var column in Matrix)
            {
                foreach (var row in column) Console.Write(row.GetString() + " ");
                Console.WriteLine();
            }
        }
    }
}