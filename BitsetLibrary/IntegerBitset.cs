using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BitsetLibrary
{
    public class IntegerBitset
    {
        private static readonly int _bitArrayLength = 8;
        private static readonly int _bitCollectionLength = 4;
        private static readonly int _bitsNumber = 32;

        private BitArray[] _bitsCollection;

        public IntegerBitset()
        {
            SetValue(0);
        }

        public IntegerBitset(int number)
        {
            SetValue(number);
        }

        public IntegerBitset(BitArray number)
        {
            SetValue(number);
        }

        public IntegerBitset(string number)
        {
            SetValue(number);
        }

        /// <summary>
        ///     Converts int to collection of bits
        /// </summary>
        /// <param name="number">Your number that will be converted</param>
        public void SetValue(int number)
        {
            _bitsCollection = BitArrayToBitCollection(new BitArray(new[] {number}));
        }

        /// <summary>
        ///     Changes current collection of bits with given
        /// </summary>
        public void SetValue(BitArray number)
        {
            if (number == null) throw new ArgumentNullException(nameof(number));
            if (number.Length != 32) throw new OperationCanceledException(nameof(number) + "Wrong array size");

            _bitsCollection = BitArrayToBitCollection(number);
        }

        /// <summary>
        ///     Converts string like "010101" to bit array
        /// </summary>
        public void SetValue(string number)
        {
            if (number == null) throw new ArgumentNullException(nameof(number));
            if (number.Length != 32) throw new OperationCanceledException(nameof(number) + "Wrong array size");

            _bitsCollection = StringToBitCollection(number);
        }

        /// <summary>
        ///     Get BitArray representation of bit collection
        /// </summary>
        public BitArray GetBitArray()
        {
            return BitCollectionToBitArray(_bitsCollection);
        }

        /// <summary>
        ///     Get bit collection
        /// </summary>
        public BitArray[] GetBitCollection()
        {
            return _bitsCollection;
        }

        /// <summary>
        ///     Get String representation of bit collection
        /// </summary>
        public string GetString()
        {
            var bitString = "";
            for (var i = 0; i < _bitCollectionLength; i++)
            for (var j = 0; j < _bitArrayLength; j++)
                bitString += _bitsCollection[i].Get(j) ? "1" : "0";
            return IntegerBitsetUtils.Reverse(bitString);
        }

        /// <summary>
        ///     Get Int32 representation of bit collection
        /// </summary>
        public int GetInt32()
        {
            return IntegerBitsetUtils.Reverse(GetString()).Select((t, i) => t == '1' ? (int) Math.Pow(2, i) : 0).Sum();
        }

        private static BitArray BitCollectionToBitArray(IReadOnlyList<BitArray> bitCollection)
        {
            var bitArray = new BitArray(_bitsNumber);
            var element = 0;
            for (var i = 0; i < _bitCollectionLength; i++)
            for (var j = 0; j < _bitArrayLength; j++)
            {
                bitArray.Set(element, bitCollection[i].Get(j));
                element++;
            }

            return bitArray;
        }

        private static BitArray[] BitArrayToBitCollection(BitArray bitArray)
        {
            var bitCollection = new BitArray[_bitCollectionLength];
            for (var i = 0; i < _bitCollectionLength; i++) bitCollection[i] = new BitArray(8);

            var element = 0;
            for (var i = 0; i < _bitCollectionLength; i++)
            for (var j = 0; j < _bitArrayLength; j++)
            {
                bitCollection[i].Set(j, bitArray.Get(element));
                element++;
            }

            return bitCollection;
        }

        private static BitArray[] StringToBitCollection(string bitArray)
        {
            var bitCollection = new BitArray[_bitCollectionLength];
            for (var i = 0; i < _bitCollectionLength; i++) bitCollection[i] = new BitArray(_bitArrayLength);

            var reverseString = IntegerBitsetUtils.Reverse(bitArray);

            var element = 0;
            for (var i = 0; i < _bitCollectionLength; i++)
            for (var j = 0; j < _bitArrayLength; j++)
            {
                bitCollection[i].Set(j, reverseString[element] == '1');
                element++;
            }

            return bitCollection;
        }
    }
}