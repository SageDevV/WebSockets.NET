using System.Net;
using System.Net.WebSockets;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Map("/", async httpContext =>
{
    if (httpContext.WebSockets.IsWebSocketRequest is false)
        httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

    else
    {
        using var webSocket = await httpContext.WebSockets.AcceptWebSocketAsync();

        while (true)
        {
            var data = Encoding.ASCII.GetBytes($"Data {DateTime.Now}");
            await webSocket.SendAsync(data, WebSocketMessageType.Text, false, CancellationToken.None);
            await Task.Delay(5000);
        }
    }
});


app.UseWebSockets();
app.Run();
