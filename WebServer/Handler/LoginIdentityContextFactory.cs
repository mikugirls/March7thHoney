using March7thHoney.Database.Account;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;

namespace March7thHoney.WebServer.Handler;

internal static class LoginIdentityContextFactory
{
    private static readonly string[] DeviceHeaderNames =
    [
        "x-rpc-device_fp",
        "x-rpc-device_id",
        "x-rpc-device_model",
        "x-rpc-device_name",
        "x-rpc-app_id"
    ];

    private static readonly HashSet<string> DevicePropertyNames = new(StringComparer.OrdinalIgnoreCase)
    {
        "device",
        "device_id",
        "deviceId",
        "device_fp",
        "deviceFp",
        "fingerprint",
        "fp",
        "idfa",
        "oaid",
        "aaid",
        "uuid"
    };

    public static IReadOnlyCollection<string> Build(HttpRequest request, params string?[] rawDeviceValues)
    {
        var deviceValues = new List<string?>();

        foreach (var headerName in DeviceHeaderNames)
        {
            if (request.Headers.TryGetValue(headerName, out var value))
                deviceValues.Add(value.ToString());
        }

        foreach (var rawValue in rawDeviceValues)
        {
            if (!LooksLikeJson(rawValue))
                deviceValues.Add(rawValue);

            ExtractDeviceValues(rawValue, deviceValues);
        }

        var ipAddress = request.HttpContext.Connection.RemoteIpAddress?.ToString();
        return AccountBanHelper.BuildIdentityKeys(deviceValues, ipAddress);
    }

    private static void ExtractDeviceValues(string? rawValue, List<string?> deviceValues)
    {
        if (string.IsNullOrWhiteSpace(rawValue))
            return;

        rawValue = rawValue.Trim();
        if (!LooksLikeJson(rawValue))
        {
            return;
        }

        try
        {
            var token = JToken.Parse(rawValue);
            ExtractDeviceValues(token, deviceValues);
        }
        catch
        {
            
        }
    }

    private static bool LooksLikeJson(string? rawValue)
    {
        rawValue = rawValue?.Trim();
        return !string.IsNullOrWhiteSpace(rawValue) &&
               (rawValue.StartsWith("{", StringComparison.Ordinal) ||
                rawValue.StartsWith("[", StringComparison.Ordinal));
    }

    private static void ExtractDeviceValues(JToken token, List<string?> deviceValues)
    {
        if (token is JObject obj)
        {
            foreach (var property in obj.Properties())
            {
                if (DevicePropertyNames.Contains(property.Name))
                    deviceValues.Add(property.Value.Type == JTokenType.String
                        ? property.Value.Value<string>()
                        : property.Value.ToString(Newtonsoft.Json.Formatting.None));

                ExtractDeviceValues(property.Value, deviceValues);
            }

            return;
        }

        if (token is JArray array)
        {
            foreach (var item in array)
                ExtractDeviceValues(item, deviceValues);
        }
    }
}
