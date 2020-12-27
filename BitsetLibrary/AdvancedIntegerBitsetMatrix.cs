using System;
using BitsetLibrary.Interfaces;

namespace BitsetLibrary
{
    public class AdvancedIntegerBitsetMatrix: IntegerBitsetMatrix,IPrintable
    {
        public void PrintInt32()
        {
            foreach (var column in Matrix)
            {
                foreach (var row in column)
                {
                    Console.Write(row.GetInt32()+" ");
                }
                Console.WriteLine();
            }
        }

        public void PrintInBits()
        {
            foreach (var column in Matrix)
            {
                foreach (var row in column)
                {
                    Console.Write(row.GetString() + " ");
                }
                Console.WriteLine();
            }
        }

        public AdvancedIntegerBitsetMatrix(int rows, int columns) : base(rows, columns)
        {
        }
    }
}