using System;
using System.Collections;
using Xunit;

namespace BitsetLibrary.Tests
{
    public class IntegerBitsetTests
    {
        #region snippet_SetValueBitArray_Passes_InputIsCorrect

        [Fact]
        public void SetValueBitArray_Passes_InputIsCorrect()
        {
            // Arrange
            var bitset = new IntegerBitset();

            // Act
            bitset.SetValue(new BitArray(new []{8}));

            // Assert
            Assert.Equal(8,bitset.GetInt32());

        }

        #endregion

        #region snippet_SetValueBitArray_ThrowsArgumentNullException_InputIsNull

        [Fact]
        public void SetValueBitArray_ThrowsArgumentNullException_InputIsNull()
        {
            // Arrange
            var bitset = new IntegerBitset();

            // Act
            void Result() => bitset.SetValue((BitArray)null);

            // Assert
            Assert.Throws<ArgumentNullException>(Result);

        }

        #endregion

        #region snippet_SetValueBitArray_ThrowsOperationCanceledException_InputLengthIsIncorrect 

        [Fact]
        public void SetValueBitArray_ThrowsOperationCanceledException_InputLengthIsIncorrect()
        {
            // Arrange
            var bitset = new IntegerBitset();

            // Act
            void Result() => bitset.SetValue(new BitArray(new byte[]{7}));

            // Assert
            Assert.Throws<OperationCanceledException>(Result);

        }

        #endregion
        

        #region snippet_SetValueString_Passes_InputIsCorrect

        [Fact]
        public void SetValueString_Passes_InputIsCorrect()
        {
            // Arrange
            var bitset = new IntegerBitset();

            // Act
            bitset.SetValue("00000000000000000000000000000111");

            // Assert
            Assert.Equal(7, bitset.GetInt32());

        }

        #endregion

        #region snippet_SetValueString_ThrowsArgumentNullException_InputIsNull

        [Fact]
        public void SetValueString_ThrowsArgumentNullException_InputIsNull()
        {
            // Arrange
            var bitset = new IntegerBitset();

            // Act
            void Result() => bitset.SetValue((string)null);

            // Assert
            Assert.Throws<ArgumentNullException>(Result);

        }

        #endregion

        #region snippet_SetValueString_ThrowsOperationCanceledException_InputLengthIsIncorrect 

        [Fact]
        public void SetValueString_ThrowsOperationCanceledException_InputLengthIsIncorrect()
        {
            // Arrange
            var bitset = new IntegerBitset();

            // Act
            void Result() => bitset.SetValue("10110101");

            // Assert
            Assert.Throws<OperationCanceledException>(Result);

        }

        #endregion
    }
}