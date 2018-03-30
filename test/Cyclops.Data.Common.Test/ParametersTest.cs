using System;
using Xunit;

namespace Cyclops.Data.Common.Test
{
    public class ParametersTest
    {
        private const string testKey = "key";

        #region GetValue
        [Fact]
        public void GetValue_ReturnsInt32_WhenSet()
        {
            // Arrange
            Int32 testValue = 99;
            // Act
            var parameters = new Parameters(testKey, testValue);
            var result = parameters.GetValue<Int32>(testKey);
            // Assert
            Assert.Equal(testValue, result);
        }
        [Fact]
        public void GetValue_ReturnsString_WhenSet()
        {
            // Arrange
            String testValue = "red";
            // Act
            var parameters = new Parameters(testKey, testValue);
            var result = parameters.GetValue<String>(testKey);
            // Assert
            Assert.Equal(testValue, result);
        }

        [Fact]
        public void GetValue_ReturnsDateTime_WhenSet()
        {
            // Arrange
            DateTime testValue = DateTime.Now;
            // Act
            var parameters = new Parameters(testKey, testValue);
            var result = parameters.GetValue<DateTime>(testKey);
            // Assert
            Assert.Equal(testValue, result);

        }

        #endregion

        #region TryGetValue
        [Fact]
        public void TryGetValue_ReturnsInt32_WhenSet()
        {
            // Arrange
            Int32 testValue = 99;
            var parameters = new Parameters(testKey, testValue);
            // Act
            bool isSuccessful = parameters.TryGetValue<Int32>(testKey, out Int32 result);
            // Assert
            Assert.Equal(true, isSuccessful);
            Assert.Equal(testValue, result);
        }


        [Fact]
        public void TryGetValue_ReturnsString_WhenSet()
        {
            // Arrange
            String testValue = "red";
            var parameters = new Parameters(testKey, testValue);
            // Act
            bool isSuccessful = parameters.TryGetValue<String>(testKey, out String result);
            // Assert
            Assert.Equal(true, isSuccessful);
            Assert.Equal(testValue, result);
        }

        [Fact]
        public void TryGetValue_ReturnsDateTime_WhenSet()
        {
            // Arrange
            DateTime testValue = DateTime.Now;
            var parameters = new Parameters(testKey, testValue);
            // Act
            bool isSuccessful = parameters.TryGetValue<DateTime>(testKey, out DateTime result);
            // Assert
            Assert.Equal(true, isSuccessful);
            Assert.Equal(testValue, result);
        }

        #endregion

        #region GetStrategy

        [Fact]
        public void GetStrategy_WhenSet()
        {
            // Arrange
            String testValue = "by-color";
            String result = String.Empty;
            var parameters = Parameters.SetStrategy(testKey, testValue);

            // Act
            var hasStrategy = parameters.HasStrategy();
            var strategyKey = parameters.GetStrategyKey();
            if (hasStrategy)
            {
                result = parameters.GetValue<String>(strategyKey);
            }

            // Assert
            Assert.Equal(true, hasStrategy);
            Assert.Equal(testValue, result);
        }

        #endregion


    }
}
