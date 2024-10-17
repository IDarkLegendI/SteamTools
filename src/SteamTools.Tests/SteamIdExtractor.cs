using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using SteamTools;

namespace SteamTests;

public class FakeHttpMessageHandler : HttpMessageHandler
{
    private readonly HttpResponseMessage _fakeResponse;

    public FakeHttpMessageHandler(HttpResponseMessage fakeResponse)
    {
        _fakeResponse = fakeResponse;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        return await Task.FromResult(_fakeResponse);
    }
}


[TestFixture]
public class SteamIdExtractorTests
{
    [Test]
    public async Task GetSteamID64FromCustomUrl_ValidCustomUrl_ReturnsCorrectSteamID64()
    {
        // Arrange
        string customUrl = "https://steamcommunity.com/id/username/";
        ulong expectedSteamID64 = 76561198000000000; // Ожидаемый steamID64

        // Подготавливаем поддельный HTTP-ответ
        var fakeResponse = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent("<meta property='og:url' content='https://steamcommunity.com/profiles/76561198000000000/' />")
        };

        // Используем FakeHttpMessageHandler с поддельным ответом 
        var fakeHandler = new FakeHttpMessageHandler(fakeResponse);
        var httpClient = new HttpClient(fakeHandler);

        // Act
        var actualSteamID64 = await SteamIdExtractor.GetSteamId64FromCustomUrl(customUrl, httpClient);

        // Assert
        Assert.That(actualSteamID64, Is.EqualTo(expectedSteamID64));
    }
}