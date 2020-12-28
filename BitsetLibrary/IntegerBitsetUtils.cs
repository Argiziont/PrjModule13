using System;
using System.Collections;

namespace BitsetLibrary
{
    public static class IntegerBitsetUtils
    {
        /// <summary>
        ///     Adds one bit to another with carry
        /// </summary>
        /// <param name="bit1">First bit</param>
        /// <param name="bit2">Second Bit</param>
        /// <param name="inRemainder">Previous remainder</param>
        /// <param name="outRemainder">Output remainder</param>
        /// <param name="sum">Sum of bits</param>
        public static void BitAdder(BitArray bit1, BitArray bit2, BitArray inRemainder, out BitArray outRemainder,
            out BitArray sum)
        {
            sum = new BitArray(new BitArray(bit1).Xor(bit2)).Xor(inRemainder);
            outRemainder =
                new BitArray(new BitArray(new BitArray(bit1).Xor(bit2)).And(inRemainder)).Or(
                    new BitArray(bit1).And(bit2));
        }

        /// <summary>
        ///     Adds arrays of bits
        /// </summary>
        /// <param name="array1">First array</param>
        /// <param name="array2">Second array</param>
        /// <param name="inRemainder">Previous remainder</param>
        /// <param name="outRemainder">Output remainder</param>
        /// <returns>Sum of bits</returns>
        public static BitArray BitArrayAdder(BitArray array1, BitArray array2, BitArray inRemainder,
            out BitArray outRemainder)
        {
            if (array1 == null) throw new ArgumentNullException(nameof(array1));
            if (array2 == null) throw new ArgumentNullException(nameof(array2));

            inRemainder ??= new BitArray(1, false);

            if (array1.Length < array2.Length)
                throw new OperationCanceledException(nameof(array1) +
                                                     "Lengths of array 1 must be greater of equal to array 2");

            outRemainder = inRemainder;
            var totalSum = new BitArray(array1.Length, false);
            for (var i = 0; i < array2.Length; i++)
            {
                BitAdder(new BitArray(1, array1.Get(i)), new BitArray(1, array2.Get(i)), outRemainder,
                    out outRemainder, out var sum);

                if (sum[0].Equals(true)) totalSum.Set(i, true);
            }

            return totalSum;
        }

        /// <summary>
        ///     Multiplies arrays of bits
        /// </summary>
        /// <param name="array1">First array</param>
        /// <param name="array2">Second array</param>
        /// <param name="inRemainder">Previous remainder</param>
        /// <param name="outRemainder">Output remainder</param>
        /// <returns>Sum of bits</returns>
        public static BitArray BitArrayMultiply(BitArray array1, BitArray array2, BitArray inRemainder,
            out BitArray outRemainder)
        {
            if (array1 == null) throw new ArgumentNullException(nameof(array1));
            if (array2 == null) throw new ArgumentNullException(nameof(array2));

            if (array1.Length != array2.Length)
                throw new OperationCanceledException(nameof(array1) + "Lengths of array 1 must be equal to array 2");
            inRemainder ??= new BitArray(1, false);

            var carry = inRemainder;
            var shiftBuffer = new BitArray(array1.Length, false);
            var multiplicand = new BitArray(array1);
            var multiplier = new BitArray(array2);

            for (var i = 0; i < array1.Length; i++)
            {
                if (multiplier.Get(0).Equals(true))
                    shiftBuffer = BitArrayAdder(multiplicand, shiftBuffer, carry, out carry);
                multiplier.RightShift(1);
                multiplier.Set(array2.Length - 1, shiftBuffer.Get(0));
                shiftBuffer.RightShift(1);
                shiftBuffer.Set(shiftBuffer.Length - 1, carry.Get(0));
                carry.RightShift(1);
            }

            outRemainder = carry;
            var result = new BitArray(array1.Length * 2, false);
            for (var i = 0; i < array1.Length * 2; i++)
                result.Set(i, i < multiplier.Length ? multiplier.Get(i) : shiftBuffer.Get(i - multiplier.Length));
            return result;
        }

        /// <summary>
        ///     Cuts last n-bits off the array
        /// </summary>
        /// <param name="array">Array</param>
        /// <param name="number">Number of bits to left</param>
        /// <returns></returns>
        public static BitArray BitArrayTruncate(BitArray array, int number)
        {
            var result = new BitArray(number, false);
            for (var i = 0; i < number; i++) result.Set(i, array.Get(i));

            return result;
        }

        /// <summary>
        ///     Prints BitArray to screen
        /// </summary>
        /// <param name="array"></param>
        public static void PrintValues(BitArray array)
        {
            for (var i = array.Length - 1; i >= 0; i--) Console.Write(array.Get(i) ? "1" : "0");
        }

        /// <summary>
        ///     Reverses string
        /// </summary>
        /// <returns>Reverse string</returns>
        public static string Reverse(string s)
        {
            var charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}