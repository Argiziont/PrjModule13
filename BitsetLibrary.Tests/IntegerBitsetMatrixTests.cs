using System;
using Xunit;

namespace BitsetLibrary.Tests
{
    public class IntegerBitsetMatrixTests
    {
        #region snippet_Transpose_Passes_InputIsCorrect

        [Fact]
        public void Transpose_Passes_InputIsCorrect()
        {
            // Arrange
            var bitset = new IntegerBitsetMatrix(2,5);

            // Act
            bitset.Transpose();

            // Assert
            Assert.Equal(2, bitset.GetColumns());
            Assert.Equal(5, bitset.GetRows());
        }

        #endregion

        #region snippet_AddMatrix_Passes_InputIsCorrect

        [Fact]
        public void AddMatrix_Passes_InputIsCorrect()
        {
            // Arrange
            var bitset1 = new IntegerBitsetMatrix(5,5);
            var bitset2 = new IntegerBitsetMatrix(5, 5);
            bitset2.Matrix[0][0] = new IntegerBitset(5);

            // Act
            bitset1.AddMatrix(bitset2);

            // Assert
            Assert.Equal(5, bitset1.Matrix[0][0].GetInt32());

        }

        #endregion

        #region snippet_AddMatrix_ThrowsArgumentNullException_InputIsNull

        [Fact]
        public void AddMatrix_ThrowsArgumentNullException_InputIsNull()
        {
            // Arrange
            var bitset1 = new IntegerBitsetMatrix(5, 5);

            // Act
            void Result() => bitset1.AddMatrix(null);

            // Assert
            Assert.Throws<ArgumentNullException>(Result);

        }

        #endregion

        #region snippet_AddMatrix_ThrowsOperationCanceledException_InputLengthIsIncorrect 

        [Fact]
        public void AddMatrix_ThrowsOperationCanceledException_InputLengthIsIncorrect()
        {
            // Arrange
            var bitset1 = new IntegerBitsetMatrix(5, 5);
            var bitset2 = new IntegerBitsetMatrix(3, 3);

            // Act
            void Result() => bitset1.AddMatrix(bitset2);

            // Assert
            Assert.Throws<OperationCanceledException>(Result);

        }

        #endregion

        #region snippet_MultiplyByNumber_Passes_InputIsCorrect

        [Fact]
        public void MultiplyByNumber_Passes_InputIsCorrect()
        {
            // Arrange
            var bitset1 = new IntegerBitsetMatrix(5, 5);
            bitset1.Matrix[0][0] = new IntegerBitset(5);

            // Act
            bitset1.MultiplyByNumber(new IntegerBitset(5));

            // Assert
            Assert.Equal(25, bitset1.Matrix[0][0].GetInt32());

        }

        #endregion

        #region snippet_MultiplyByNumber_ThrowsArgumentNullException_InputIsNull

        [Fact]
        public void MultiplyByNumber_ThrowsArgumentNullException_InputIsNull()
        {
            // Arrange
            var bitset1 = new IntegerBitsetMatrix(5, 5);

            // Act
            void Result() => bitset1.MultiplyByNumber(null);

            // Assert
            Assert.Throws<ArgumentNullException>(Result);

        }

        #endregion

        #region snippet_RaiseMatrixToPower_Passes_InputIsCorrect

        [Fact]
        public void RaiseMatrixToPower_Passes_InputIsCorrect()
        {
            // Arrange
            var bitset1 = new IntegerBitsetMatrix(5, 5);
            bitset1.Matrix[0][0] = new IntegerBitset(5);

            // Act
            bitset1.RaiseMatrixToPower(new IntegerBitset(2));

            // Assert
            Assert.Equal(25, bitset1.Matrix[0][0].GetInt32());

        }

        #endregion

        #region snippet_RaiseMatrixToPower_ThrowsArgumentNullException_InputIsNull

        [Fact]
        public void RaiseMatrixToPower_ThrowsArgumentNullException_InputIsNull()
        {
            // Arrange
            var bitset1 = new IntegerBitsetMatrix(5, 5);

            // Act
            void Result() => bitset1.RaiseMatrixToPower(null);

            // Assert
            Assert.Throws<ArgumentNullException>(Result);

        }

        #endregion

        #region snippet_MultiplyByMatrix_Passes_InputIsCorrect

        [Fact]
        public void MultiplyByMatrix_Passes_InputIsCorrect()
        {
            // Arrange
            var bitset1 = new IntegerBitsetMatrix(5, 5);
            var bitset2 = new IntegerBitsetMatrix(5, 5);
            bitset2.Matrix[0][0] = new IntegerBitset(5);
            bitset1.Matrix[0][0] = new IntegerBitset(1);
            // Act
            bitset1.MultiplyByMatrix(bitset2);

            // Assert
            Assert.Equal(1, bitset1.Matrix[0][0].GetInt32());

        }

        #endregion

        #region snippet_MultiplyByMatrix_ThrowsArgumentNullException_InputIsNull

        [Fact]
        public void MultiplyByMatrix_ThrowsArgumentNullException_InputIsNull()
        {
            // Arrange
            var bitset1 = new IntegerBitsetMatrix(5, 5);

            // Act
            void Result() => bitset1.MultiplyByMatrix(null);

            // Assert
            Assert.Throws<ArgumentNullException>(Result);

        }

        #endregion

        #region snippet_MultiplyByMatrix_ThrowsOperationCanceledException_InputLengthIsIncorrect 

        [Fact]
        public void MultiplyByMatrix_ThrowsOperationCanceledException_InputLengthIsIncorrect()
        {
            // Arrange
            var bitset1 = new IntegerBitsetMatrix(5, 5);
            var bitset2 = new IntegerBitsetMatrix(3, 3);

            // Act
            void Result() => bitset1.MultiplyByMatrix(bitset2);

            // Assert
            Assert.Throws<OperationCanceledException>(Result);

        }

        #endregion

    }
}