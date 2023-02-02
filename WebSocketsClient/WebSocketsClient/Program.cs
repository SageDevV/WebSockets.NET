using System.Net.WebSockets;
using System.Text;

using var webSockets = new ClientWebSocket();

await webSockets.ConnectAsync(new Uri("wss://localhost:7163"), CancellationToken.None);

var buffer = new byte[256];

while (webSockets.State == WebSocketState.Open)
{
    var result = await webSockets.ReceiveAsync(buffer, CancellationToken.None);

    if (result.MessageType == WebSocketMessageType.Close)
        await webSockets.CloseAsync(WebSocketCloseStatus.NormalClosure, "Fechamento períodico", CancellationToken.None);

    else
        Console.WriteLine(Encoding.ASCII.GetString(buffer, 0, result.Count));
}