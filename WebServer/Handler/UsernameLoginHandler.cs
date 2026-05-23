using March7thHoney.WebServer.Objects;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace March7thHoney.WebServer.Handler;

public class UsernameLoginHandler
{
    public JsonResult HandleShieldLogin(string account, string password, IReadOnlyCollection<string> identityKeys)
    {
        LoginSessionFactory.LoginFailure failure;
        if (!LoginSessionFactory.TryCreateSession(account, password, identityKeys, out var session, out failure))
            return new JsonResult(LoginResJson.Error(failure.Retcode, failure.Message));

        return new JsonResult(LoginResJson.Success(
            session.Uid,
            session.Username,
            session.Email,
            session.IsEmailVerified,
            session.DispatchToken));
    }

    public JsonResult HandlePassportLogin(string account, string password, IReadOnlyCollection<string> identityKeys)
    {
        LoginSessionFactory.LoginFailure failure;
        if (!LoginSessionFactory.TryCreateSession(account, password, identityKeys, out var session, out failure))
            return new JsonResult(new { retcode = failure.Retcode, message = failure.Message, data = (object?)null });

        return new JsonResult(BuildPassportLoginBody(session));
    }

    public ContentResult HandleLegacyPassportLogin(string account, string password, IReadOnlyCollection<string> identityKeys)
    {
        LoginSessionFactory.LoginFailure failure;
        if (!LoginSessionFactory.TryCreateSession(account, password, identityKeys, out var session, out failure))
        {
            return new ContentResult
            {
                ContentType = "application/json",
                Content = JsonConvert.SerializeObject(PassportLoginResJson.Error(failure.Retcode, failure.Message))
            };
        }

        return new ContentResult
        {
            ContentType = "application/json",
            Content = JsonConvert.SerializeObject(BuildPassportLoginBody(session))
        };
    }

    private static object BuildPassportLoginBody(LoginSessionFactory.LoginSession session)
    {
        return new
        {
            retcode = 0,
            message = "OK",
            data = new
            {
                bind_email_action_ticket = "",
                reactivate_action_token = "",
                user_info = new
                {
                    aid = session.Uid,
                    mid = session.Uid,
                    account_name = session.Username,
                    email = session.Email,
                    is_email_verify = session.IsEmailVerified ? 1 : 0,
                    area_code = "**",
                    mobile = "",
                    safe_area_code = "",
                    safe_mobile = "",
                    realname = "",
                    identity_code = "",
                    rebind_area_code = "",
                    rebind_mobile = "",
                    rebind_mobile_time = "1",
                    links = Array.Empty<object>(),
                    country = "CN",
                    password_time = "1",
                    is_adult = 0,
                    unmasked_email = session.Email,
                    unmasked_email_type = string.IsNullOrWhiteSpace(session.Email) ? 0 : 1
                },
                token = new { token_type = 1, token = session.DispatchToken },
                ext_user_info = new { guardian_email = "", birth = "0" }
            }
        };
    }
}
