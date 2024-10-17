using System;
using System.Net.Http;
using HtmlAgilityPack;

namespace SteamTools;

public class SteamIdExtractor
{
    public static async Task<ulong> GetSteamId64FromCustomUrl(string customUrl)
    {
        using HttpClient client = new HttpClient();
        string url = customUrl.EndsWith("/") ? customUrl : customUrl + "/";
        HttpResponseMessage response = await client.GetAsync(url);
        string pageContent = await response.Content.ReadAsStringAsync();

        // Используем HtmlAgilityPack для парсинга страницы
        var htmlDoc = new HtmlDocument();
        htmlDoc.LoadHtml(pageContent);

        // Находим steamID64 через анализ HTML (обычно содержится в теге <meta>)
        var metaTag = htmlDoc.DocumentNode.SelectSingleNode("//meta[@property='og:url']");
        if (metaTag != null)
        {
            string profileUrl = metaTag.GetAttributeValue("content", "");
            if (profileUrl.Contains("profiles"))
            {
                string steamId64String = profileUrl.Split(new[] { "profiles/" }, StringSplitOptions.None)[1].TrimEnd('/');
                return ulong.Parse(steamId64String);
            }
        }

        throw new Exception("SteamID64 не найден");
    }
    
    
    public static ulong ExtractSteamId64FromUrl(string url)
    {
        // Пример парсинга URL
        string steamId64String = url.Split(new[] { "profiles/" }, StringSplitOptions.None)[1].TrimEnd('/');
        return ulong.Parse(steamId64String);
    }
    
    public static async Task<ulong> GetSteamId64FromCustomUrl(string customUrl, HttpClient client)
    {
        string url = customUrl.EndsWith("/") ? customUrl : customUrl + "/";
        HttpResponseMessage response = await client.GetAsync(url);
        string pageContent = await response.Content.ReadAsStringAsync();

        var htmlDoc = new HtmlDocument();
        htmlDoc.LoadHtml(pageContent);

        var metaTag = htmlDoc.DocumentNode.SelectSingleNode("//meta[@property='og:url']");
        if (metaTag != null)
        {
            string profileUrl = metaTag.GetAttributeValue("content", "");
            if (profileUrl.Contains("profiles"))
            {
                string steamID64String = profileUrl.Split(new[] { "profiles/" }, StringSplitOptions.None)[1].TrimEnd('/');
                return ulong.Parse(steamID64String);
            }
        }

        throw new Exception("SteamID64 не найден");
    }
}