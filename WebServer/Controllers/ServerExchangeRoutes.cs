using March7thHoney.Database;
using March7thHoney.Database.Account;
using March7thHoney.Util;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace March7thHoney.WebServer.Controllers;

[ApiController]
[Route("/")]
public class ServerExchangeRoutes : ControllerBase
{
    [HttpGet("/get_account_info")]
    public async ValueTask<ContentResult> GetAccountInfo([FromQuery] string accountUid)
    {
        if (!ConfigManager.Config.ServerOption.ServerConfig.RunDispatch)
            return new ContentResult
            {
                StatusCode = 404
            };

        if (!IsAuthorizedServerExchangeRequest())
            return new ContentResult
            {
                StatusCode = 401,
                Content = "Unauthorized"
            };

        if (string.IsNullOrEmpty(accountUid) || !int.TryParse(accountUid, out var uid))
            return new ContentResult
            {
                StatusCode = 400
            };

        var account = DatabaseHelper.Instance?.GetInstance<AccountData>(uid);
        if (account == null)
            return new ContentResult
            {
                StatusCode = 404,
                Content = "Account not found"
            };

        await ValueTask.CompletedTask;

        return new ContentResult
        {
            Content = account.Uid.ToString(),
            StatusCode = 200,
            ContentType = "plain/text; charset=utf-8"
        };
    }

    [HttpPost("/validate_account_token")]
    public async ValueTask<ContentResult> ValidateAccountToken([FromForm] string accountUid, [FromForm] string token)
    {
        if (!ConfigManager.Config.ServerOption.ServerConfig.RunDispatch)
            return new ContentResult
            {
                StatusCode = 404
            };

        if (!IsAuthorizedServerExchangeRequest())
            return new ContentResult
            {
                StatusCode = 401,
                Content = "Unauthorized"
            };

        if (string.IsNullOrWhiteSpace(accountUid) || string.IsNullOrWhiteSpace(token) || !int.TryParse(accountUid, out var uid))
            return new ContentResult
            {
                StatusCode = 400,
                Content = "Invalid request"
            };

        var account = DatabaseHelper.Instance?.GetInstance<AccountData>(uid);
        if (account == null || !account.ValidateGameToken(token))
            return new ContentResult
            {
                StatusCode = 404,
                Content = "Account token not found"
            };

        var identityKeys = AccountBanHelper.BuildIdentityKeys([],
            HttpContext.Connection.RemoteIpAddress?.ToString());
        if (AccountBanHelper.TryGetActiveBan(account, identityKeys, out var banStatus))
        {
            account.ClearLoginTokens();
            return new ContentResult
            {
                StatusCode = 403,
                Content = banStatus.FormatLoginMessage()
            };
        }

        if (!account.CanLogin())
        {
            account.ClearLoginTokens();
            return new ContentResult
            {
                StatusCode = 403,
                Content = ConfigManager.Config.ServerOption.Auth.EmailVerificationRequiredMessage
            };
        }

        await ValueTask.CompletedTask;

        return new ContentResult
        {
            Content = account.Uid.ToString(),
            StatusCode = 200,
            ContentType = "plain/text; charset=utf-8"
        };
    }

    private bool IsAuthorizedServerExchangeRequest()
    {
        var expectedSecret = ConfigManager.Config.ServerOption.ServerConfig.ServerExchangeSecret;
        if (string.IsNullOrWhiteSpace(expectedSecret))
            return true;

        if (!Request.Headers.TryGetValue("X-March7thHoney-Server-Secret", out var providedValues))
            return false;

        var providedSecret = providedValues.ToString();
        if (string.IsNullOrWhiteSpace(providedSecret))
            return false;

        var expectedBytes = Encoding.UTF8.GetBytes(expectedSecret);
        var providedBytes = Encoding.UTF8.GetBytes(providedSecret);
        return expectedBytes.Length == providedBytes.Length &&
               CryptographicOperations.FixedTimeEquals(expectedBytes, providedBytes);
    }
}
