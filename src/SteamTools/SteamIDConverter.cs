namespace SteamTools;

public class SteamIdConverter
{
    // Базовое значение для всех SteamID64
    private const ulong SteamId64Base = 76561197960265728;

    public static uint ConvertSteamId64To32(ulong steamId64)
    {
        if (steamId64 < SteamId64Base)
        {
            throw new ArgumentOutOfRangeException(nameof(steamId64), "steamID64 меньше минимального значения.");
        }
        return (uint)(steamId64 - SteamId64Base);
    }
}