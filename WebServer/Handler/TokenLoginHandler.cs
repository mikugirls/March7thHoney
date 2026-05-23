using March7thHoney.Database.Account;
using March7thHoney.WebServer.Objects;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace March7thHoney.WebServer.Handler;

public class TokenLoginHandler
{
    public JsonResult HandleShieldVerify(string uid, string token, IReadOnlyCollection<string> identityKeys)
    {
        if (!int.TryParse(uid, out var parsedUid))
            return new JsonResult(LoginResJson.Error(-201, "Game account cache information error"));

        var account = AccountData.GetAccountByUid(parsedUid);
        if (account == null)
            return new JsonResult(LoginResJson.Error(-201, "Game account cache information error"));

        var isDispatchToken = account.ValidateDispatchToken(token);
        var isComboToken = account.ValidateComboToken(token);
        if (!isDispatchToken && !isComboToken)
            return new JsonResult(LoginResJson.Error(-201, "Game account cache information error"));

        if (account.GetBanStatus().IsActive)
            account.AddKnownIdentityKeys(identityKeys);

        if (!LoginSessionFactory.CanAuthenticate(account, identityKeys, out var failure))
            return new JsonResult(LoginResJson.Error(failure.Retcode, failure.Message));

        account.AddKnownIdentityKeys(identityKeys);
        var dispatchToken = isDispatchToken ? token : account.GenerateDispatchToken();
        var username = account.Username ?? uid;
        return new JsonResult(LoginResJson.Success(
            account.Uid.ToString(),
            username,
            account.Email ?? string.Empty,
            account.IsEmailVerified,
            dispatchToken));
    }

    public ContentResult HandlePassportVerify(string mid, string token, bool refresh,
        IReadOnlyCollection<string> identityKeys)
    {
        if (!int.TryParse(mid, out var uid))
            return BuildJson(PassportLoginResJson.Error(-201, "Account not found"));

        var account = AccountData.GetAccountByUid(uid);
        if (account == null)
            return BuildJson(PassportLoginResJson.Error(-201, "Account not found"));

        var isDispatchToken = account.ValidateDispatchToken(token);
        var isComboToken = account.ValidateComboToken(token);
        if (!isDispatchToken && !isComboToken)
            return BuildJson(PassportLoginResJson.Error(-201, "Game account cache information error"));

        if (account.GetBanStatus().IsActive)
            account.AddKnownIdentityKeys(identityKeys);

        if (!LoginSessionFactory.CanAuthenticate(account, identityKeys, out var failure))
            return BuildJson(PassportLoginResJson.Error(failure.Retcode, failure.Message));

        account.AddKnownIdentityKeys(identityKeys);
        var dispatchToken = refresh || !isDispatchToken ? account.GenerateDispatchToken() : token;
        return BuildJson(PassportLoginResJson.Success(
            account.Uid.ToString(),
            account.Username ?? uid.ToString(),
            account.Email ?? string.Empty,
            account.IsEmailVerified,
            dispatchToken));
    }

    private static ContentResult BuildJson(PassportLoginResJson payload)
    {
        return new ContentResult
        {
            ContentType = "application/json",
            Content = JsonConvert.SerializeObject(payload)
        };
    }
}
