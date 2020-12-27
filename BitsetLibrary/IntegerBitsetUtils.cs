using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BitsetLibrary
{
    public static class IntegerBitsetUtils
    {
        public static void BitAdder(BitArray array1, BitArray array2, BitArray inRemainder, out BitArray outRemainder,out BitArray sum)
        {
            sum = new BitArray(new BitArray(array1).Xor(array2)).Xor(inRemainder);
            outRemainder = new BitArray(new BitArray(new BitArray(array1).Xor(array2)).And(inRemainder)).Or(new BitArray(array1).And(array2));
        }

        public static BitArray BitArrayAdder(BitArray array1, BitArray array2, BitArray inRemainder,out BitArray outReminder)
        {
            if (array1 == null) throw new ArgumentNullException(nameof(array1));
            if (array2 == null) throw new ArgumentNullException(nameof(array2));
            
            inRemainder ??= new BitArray(1, false);

            if (array1.Length< array2.Length) throw new OperationCanceledException(nameof(array1) + "Lengths of array 1 must be greater of equal to array 2");

            outReminder = inRemainder;
            var totalSum = new BitArray(array1.Length, false);
            for (var i = 0; i < array2.Length; i++)
            {
                IntegerBitsetUtils.BitAdder(new BitArray(1, array1.Get(i)), new BitArray(1, array2.Get(i)), outReminder,
                    out outReminder, out var sum);
                
                if (sum[0].Equals(true)) totalSum.Set(i, true);
            }
            return totalSum;
        }

        public static BitArray BitArrayMultiply(BitArray array1, BitArray array2, BitArray inRemainder, out BitArray outReminder)
        {
            if (array1 == null) throw new ArgumentNullException(nameof(array1));
            if (array2 == null) throw new ArgumentNullException(nameof(array2));
            
            if (array1.Length != array2.Length) throw new OperationCanceledException(nameof(array1) + "Lengths of array 1 must be equal to array 2");
            inRemainder ??=new BitArray(1, false);

            var carry = inRemainder;
            var shiftBuffer = new BitArray(array1.Length, false);
            var multiplicand = new BitArray(array1);
            var multiplier = new BitArray(array2);
            
            for (var i = 0; i < array1.Length; i++)
            {
                if (multiplier.Get(0).Equals(true))
                {
                    shiftBuffer=BitArrayAdder(multiplicand, shiftBuffer, carry, out carry);
                }
                multiplier.RightShift(1);
                multiplier.Set(array2.Length - 1, shiftBuffer.Get(0));
                shiftBuffer.RightShift(1);
                shiftBuffer.Set(shiftBuffer.Length - 1, carry.Get(0));
                carry.RightShift(1);
            }

            outReminder = carry;
            var result = new BitArray(array1.Length * 2, false);
            for (var i = 0; i < array1.Length*2; i++)
            {
                result.Set(i, i < multiplier.Length ? multiplier.Get(i) : shiftBuffer.Get(i - multiplier.Length));
            }
            return result;
        }

        public static BitArray BitArrayTruncate(BitArray array, int number)
        {
            var result = new BitArray(number, false);
            for (var i = 0; i < number; i++)
            {
                result.Set(i,array.Get(i));
            }

            return result;
        }

        public static void PrintValues(BitArray myList)
        {
            for (var i = myList.Length - 1; i >= 0; i--)
            {
                Console.Write(myList.Get(i) ? "1" : "0");
            }
        }

        public static string Reverse(string s)
        {
            var charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}
