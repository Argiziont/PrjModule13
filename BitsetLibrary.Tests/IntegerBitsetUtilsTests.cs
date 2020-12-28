using System;
using System.Collections;
using Xunit;

namespace BitsetLibrary.Tests
{
    public class IntegerBitsetUtilsTests
    {
        #region snippet_BitAdder_Passes_InputIsCorrect

        [Fact]
        public void BitAdder_Passes_InputIsCorrect()
        {
            // Arrange
            var bit1 = new BitArray(1, false);
            var bit2 = new BitArray(1, true);

            // Act
            IntegerBitsetUtils.BitAdder(bit1,bit2,new BitArray(1,false),out var outRemainder, out var sum);

            // Assert
            Assert.True(sum.Get(0));
            Assert.False(outRemainder.Get(0));
        }

        #endregion

        #region snippet_BitAdder_ThrowsArgumentNullException_InputIsNull

        [Fact]
        public void BitAdder_ThrowsArgumentNullException_InputIsNull()
        {
            // Arrange&&Act
            static void Result() => IntegerBitsetUtils.BitAdder(null, null, new BitArray(1, false), out _, out _);

            // Assert
            Assert.Throws<ArgumentNullException>(Result);

        }

        #endregion

        #region snippet_BitArrayAdder_Passes_InputIsCorrect

        [Fact]
        public void BitArrayAdder_Passes_InputIsCorrect()
        {
            // Arrange
            var bit1 = new BitArray(new[] { 5 });
            var bit2 = new BitArray(new[] { 6 });

            // Act
            var bitset= new IntegerBitset(IntegerBitsetUtils.BitArrayAdder(bit1, bit2, new BitArray(1, false), out _)) ;

            // Assert
            Assert.Equal(11, bitset.GetInt32());
        }

        #endregion

        #region snippet_BitArrayAdder_ThrowsArgumentNullException_InputIsNull

        [Fact]
        public void BitArrayAdder_ThrowsArgumentNullException_InputIsNull()
        {
            // Arrange&&Act
            static void Result() => IntegerBitsetUtils.BitArrayAdder(null, null, new BitArray(1, false), out _);

            // Assert
            Assert.Throws<ArgumentNullException>(Result);

        }

        #endregion

        #region snippet_BitArrayMultiply_Passes_InputIsCorrect

        [Fact]
        public void BitArrayMultiply_Passes_InputIsCorrect()
        {
            // Arrange
            var bit1 = new BitArray(new[] { 5 });
            var bit2 = new BitArray(new[] { 6 });

            // Act
            var bitset =
                new IntegerBitset(IntegerBitsetUtils.BitArrayTruncate(
                    IntegerBitsetUtils.BitArrayMultiply(bit1, bit2, new BitArray(1, false), out _), 32));

            // Assert
            Assert.Equal(30, bitset.GetInt32());
        }

        #endregion

        #region snippet_BitArrayMultiply_ThrowsArgumentNullException_InputIsNull

        [Fact]
        public void BitArrayMultiply_ThrowsArgumentNullException_InputIsNull()
        {
            // Arrange&&Act
            static void Result() => IntegerBitsetUtils.BitArrayMultiply(null, null, new BitArray(1, false), out _);

            // Assert
            Assert.Throws<ArgumentNullException>(Result);

        }

        #endregion

        #region snippet_BitArrayMultiply_ThrowsOperationCanceledException_InputLengthsIsDifferent

        [Fact]
        public void BitArrayMultiply_ThrowsOperationCanceledException_InputLengthsIsDifferent()
        {
            // Arrange
            var bit1 = new BitArray(new[] { 5 });
            var bit2 = new BitArray(new byte[] { 6 });

            // Act
            void Result()=>IntegerBitsetUtils.BitArrayMultiply(bit1, bit2, new BitArray(1, false), out _);

            // Assert
            Assert.Throws<OperationCanceledException>(Result);
        }

        #endregion

        #region snippet_BitArrayTruncate_Passes_InputIsCorrect

        [Fact]
        public void BitArrayTruncate_Passes_InputIsCorrect()
        {
            // Arrange
            var bitset = new BitArray(new[] { 5 });

            // Act
            bitset = IntegerBitsetUtils.BitArrayTruncate(bitset, 16);

            // Assert
            Assert.Equal(16, bitset.Length);
        }

        #endregion

        #region snippet_BitArrayTruncate_ThrowsArgumentNullException_InputIsNull

        [Fact]
        public void BitArrayTruncate_ThrowsArgumentNullException_InputIsNull()
        {
            // Arrange&&Act
            static void Result() => IntegerBitsetUtils.BitArrayTruncate(null, 16);

            // Assert
            Assert.Throws<ArgumentNullException>(Result);

        }

        #endregion

        #region snippet_PrintValues_ThrowsArgumentNullException_InputIsNull

        [Fact]
        public void PrintValues_ThrowsArgumentNullException_InputIsNull()
        {
            // Arrange&&Act
            static void Result() => IntegerBitsetUtils.PrintValues(null);

            // Assert
            Assert.Throws<ArgumentNullException>(Result);

        }

        #endregion

        #region snippet_Reverse_ThrowsArgumentNullException_InputIsNull

        [Fact]
        public void Reverse_ThrowsArgumentNullException_InputIsNull()
        {
            // Arrange&&Act
            static void Result() => IntegerBitsetUtils.Reverse(null);

            // Assert
            Assert.Throws<ArgumentNullException>(Result);

        }

        #endregion
    }
}