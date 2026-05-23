using March7thHoney.Database.Account;
using March7thHoney.GameServer.Game.Calyx;
using March7thHoney.GameServer.Server;
using March7thHoney.Internationalization;
using March7thHoney.Kcp;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace March7thHoney.WebServer.Controllers
{
    [ApiController]
    [Route("/")]
    public class JsonRoutes : ControllerBase
    {
        [HttpPost("/sr-tools")]
        public async ValueTask<ContentResult> SrTools()
        {
            try
            {
                using var reader = new StreamReader(Request.Body, Encoding.UTF8);
                string body = await reader.ReadToEndAsync();

                var obj = Newtonsoft.Json.Linq.JToken.Parse(body);
                string username = obj["username"]?.ToString() ?? "";
                string password = obj["password"]?.ToString() ?? "";
                string dataJson = obj["data"]?.ToString(Newtonsoft.Json.Formatting.None) ?? "";

                if (!int.TryParse(username, out var uid))
                {
                    return new ContentResult()
                    {
                        StatusCode = 400,
                        Content = "Username field must be the player's UID (integer).",
                        ContentType = "text/plain"
                    };
                }

                var acc = AccountData.GetAccountByUid(uid);

                if (acc == null)
                {
                    return new ContentResult()
                    {
                        StatusCode = 400,
                        Content = "uid not found on server",
                        ContentType = "text/plain"
                    };
                }

                Connection? con = null;

                foreach (var item in March7thHoneyListener.Connections.Values)
                {
                    if (item is not Connection c)
                    {
                        continue;
                    }

                    if (c.Player?.Uid == acc.Uid)
                    {
                        con = c;
                        break;
                    }
                }

                if (con?.Player == null)
                {
                    return new ContentResult()
                    {
                        StatusCode = 400,
                        Content = "player offline",
                        ContentType = "text/plain"
                    };
                }

                if (dataJson != string.Empty)
                {
                    var player = con.Player;
                    await FreesrShared.ImportJson(dataJson, player, async (msg) =>
                    {
                        if (msg[0] == "Game.Command.Json.ImportSummary" || msg[0] == "Game.Command.Json.AvatarExcelNotFound")
                        {
                            return;
                        }
                        else
                        {
                            throw new Exception(I18NManager.Translate(msg[0], msg.Skip(1).ToArray()));
                        }
                    });
                }

                return new ContentResult()
                {
                    StatusCode = 200,
                    Content = "Successfully synced data from website!",
                    ContentType = "application/json"
                };
            }
            catch (Exception ex)
            {
                return new ContentResult()
                {
                    StatusCode = 500,
                    Content = ex.Message,
                    ContentType = "text/plain"
                };
            }
        }

        [HttpGet("/sr-tools-export")]
        public async ValueTask<EmptyResult> DownloadFreedata()
        {
            int uid = 0;
            if (Request.Query.ContainsKey("uid"))
            {
                int.TryParse(Request.Query["uid"], out uid);
            }

            if (uid == 0)
            {
                Response.StatusCode = 400;
                Response.ContentType = "text/plain";
                await Response.WriteAsync("Error: Missing valid uid parameter");
                return new EmptyResult();
            }

            var json = await FreesrShared.ExportPlayerDataAsync(uid, true);

            Response.StatusCode = 200;
            Response.ContentType = "application/octet-stream";
            Response.Headers["Content-Disposition"] = "attachment; filename=freesr-data.json; filename*=UTF-8''freesr-data.json";

            await Response.WriteAsync(json);
            return new EmptyResult();
        }
    }
}
