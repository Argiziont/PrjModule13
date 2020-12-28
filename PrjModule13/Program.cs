using System;
using System.Collections;
using BitsetLibrary;

namespace PrjModule13
{
    internal static class Program
    {
        private static void Main()
        {
            // Creates and initializes several BitArrays.


            var myBa1 = new BitArray(new byte[] {255});

            var myBa2 = new BitArray(new byte[] {255});

            var result1 = IntegerBitsetUtils.BitArrayAdder(myBa1, myBa2, new BitArray(1, false), out _);
            var rem = new BitArray(1, false);
            var result2 = IntegerBitsetUtils.BitArrayMultiply(myBa1, myBa2, new BitArray(1, false), out rem);

            IntegerBitsetUtils.PrintValues(result2);

            var set1 = new IntegerBitset(75005);
            var set2 = new IntegerBitset("00000000000000001000100001000000");
            var tmp1 = set2.GetInt32();
            var m1 = new AdvancedIntegerBitsetMatrix(2, 2);
            var m2 = new AdvancedIntegerBitsetMatrix(2, 2);

            m1.Matrix[0][0] = new IntegerBitset(101);
            m1.Matrix[0][1] = new IntegerBitset(54);
            m1.Matrix[1][0] = new IntegerBitset(757);
            m1.Matrix[1][1] = new IntegerBitset(550);

            Console.WriteLine("\nMatrix 1: 2x2 in integer");
            m1.PrintInt32();
            Console.WriteLine("\nMatrix 1: 2x2 in bits");
            m1.PrintInBits();

            m2.Matrix[0][0] = new IntegerBitset(207);
            m2.Matrix[0][1] = new IntegerBitset(54);
            m2.Matrix[1][0] = new IntegerBitset(530);
            m2.Matrix[1][1] = new IntegerBitset(147);

            Console.WriteLine("\nMatrix 2: 2x2 in integer");
            m2.PrintInt32();
            Console.WriteLine("\nMatrix 2: 2x2 in bits");
            m2.PrintInBits();

            m1.AddMatrix(m2);

            Console.WriteLine("\nMatrix Add result: 2x2 in integer");
            m1.PrintInt32();
            Console.WriteLine("\nMatrix Add result: 2x2 in bits");
            m1.PrintInBits();

            m1.MultiplyByNumber(new IntegerBitset(5));

            Console.WriteLine("\nMatrix Multiply by 5 result: 2x2 in integer");
            m1.PrintInt32();
            Console.WriteLine("\nMatrix Multiply by 5 result: 2x2 in bits");
            m1.PrintInBits();

            m1.MultiplyByMatrix(m2);

            Console.WriteLine("\nMatrix1 Multiply by Matrix2 result: 2x2 in integer");
            m1.PrintInt32();
            Console.WriteLine("\nMatrix1 Multiply by Matrix2 result: 2x2 in bits");
            m1.PrintInBits();

            m2.RaiseMatrixToPower(new IntegerBitset(3));

            Console.WriteLine("\nMatrix2 Raise in power 3 result: 2x2 in integer");
            m2.PrintInt32();
            Console.WriteLine("\nMatrix2 Raise in power 3 result: 2x2 in bits");
            m2.PrintInBits();

            m2.Transpose();

            Console.WriteLine("\nMatrix2 Transposed result: 2x2 in integer");
            m2.PrintInt32();
            Console.WriteLine("\nMatrix2 Transposed result: 2x2 in bits");
            m2.PrintInBits();
        }
    }
}