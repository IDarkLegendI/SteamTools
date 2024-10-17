using SteamTools;

namespace SteamTests;

[TestFixture]
public class SteamIdConverterTests
{
    [Test]
    public void ConvertSteamID64To32_ValidSteamID64_ReturnsCorrectSteamID32()
    {
        // Arrange
        ulong steamIdD64 = 76561198000000000; // Пример steamID64
        uint expectedSteamId32 = 39734272;    // Правильный результат для этого ID
 
        // Act
        uint actualSteamId32 = SteamIdConverter.ConvertSteamId64To32(steamIdD64);

        // Assert
        Assert.That(actualSteamId32, Is.EqualTo(expectedSteamId32));
    }
}