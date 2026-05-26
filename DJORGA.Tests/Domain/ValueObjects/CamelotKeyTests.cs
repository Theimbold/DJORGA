using DJORGA.Domain.ValueObjects;
using Xunit;

namespace DJORGA.Tests.Domain.ValueObjects
{
    public class CamelotKeyTests
    {
        [Theory]
        [InlineData("8A", 8, 'A')]
        [InlineData("12B", 12, 'B')]
        [InlineData("1A", 1, 'A')]
        public void Parse_ValidString_ShouldReturnCorrectObject(string input, int expectedNumber, char expectedMode)
        {
            var result = CamelotKey.Parse(input);
            Assert.Equal(expectedNumber, result.Number);
            Assert.Equal(expectedMode, result.Mode);
        }

        [Theory]
        [InlineData("8A", "8A", true)]   // Identisch
        [InlineData("8A", "9A", true)]   // Nachbar +1
        [InlineData("8A", "7A", true)]   // Nachbar -1
        [InlineData("12A", "1A", true)]  // Wrap-around 12 -> 1
        [InlineData("1A", "12A", true)]  // Wrap-around 1 -> 12
        [InlineData("8A", "8B", true)]   // Relativ Major/Minor
        [InlineData("8A", "10A", false)]  // Zu weit weg
        [InlineData("8A", "7B", false)]   // Falscher Mode und falsche Nummer
        public void IsCompatibleWith_ShouldReturnCorrectResult(string key1, string key2, bool expected)
        {
            var ck1 = CamelotKey.Parse(key1);
            var ck2 = CamelotKey.Parse(key2);

            Assert.Equal(expected, ck1.IsCompatibleWith(ck2));
        }

        [Fact]
        public void GetNeighbors_ShouldReturnFourKeys()
        {
            var key = CamelotKey.Parse("8A");
            var neighbors = key.GetNeighbors();

            Assert.Contains(neighbors, k => k.ToString() == "8A"); // Selbst
            Assert.Contains(neighbors, k => k.ToString() == "8B"); // Relativ
            Assert.Contains(neighbors, k => k.ToString() == "7A"); // Links
            Assert.Contains(neighbors, k => k.ToString() == "9A"); // Rechts
        }
    }
}
