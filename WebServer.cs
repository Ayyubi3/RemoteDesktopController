using System;
using System.Net;
using System.Text;

class WebServer
{
    public void Start(string URL, int PORT)
    {
        try
        {
            HttpListener listener = new HttpListener();
            listener.Prefixes.Add(URL + ":" + PORT + "/");
            listener.Start();
            Console.WriteLine("Listening...");
            HttpListenerContext context = listener.GetContext();
            HttpListenerResponse response = context.Response;
            string responseString = html.Replace("VARIABLE", URL.Substring(7) + $":{PORT + 1}");
            byte[] buffer = Encoding.UTF8.GetBytes(responseString);
            response.ContentLength64 = buffer.Length;
            System.IO.Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            output.Close();
            listener.Stop();
        }catch(Exception e)
        {
            Console.WriteLine(e.Message);
        }

    }
    static string html = @"<!doctype html>
<html onmouseup='EndInterval()' ontouchstop='EndInterval()'>

<head>
    <style>
        .btn {
            background-color: rgb(95, 0, 124);
            padding: 10px;
        }

        .disable-select {
            -webkit-touch-callout: none;
            -webkit-user-select: none;
            -khtml-user-select: none;
            -moz-user-select: none;
            -ms-user-select: none;
            user-select: none;
            -webkit-tap-highlight-color: rgba(0, 0, 0, 0);
        }
    </style>
    <meta name='viewport' content='width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no' />
</head>

<body>
    <button onmousedown='startsend(`sub1y`);' ontouchstart='startsend(`sub1y`);' class='btn'>Up</button>
    <button onmousedown='startsend(`sub1x`);' ontouchstart='startsend(`sub1x`);' class='btn'>Left</button>
    <button onmousedown='startsend(`add1x`);' ontouchstart='startsend(`add1x`);' class='btn'>Right</button>
    <button onmousedown='startsend(`add1y`);' ontouchstart='startsend(`add1y`);' class='btn'>Down</button>
    <button onmousedown='startsend(`click`);' ontouchstart='startsend(`click`);' class='btn'>OK</button>
    <script>
        let counter;

        //#region websocket
        let wsUri = 'ws://VARIABLE',
            websocket = new WebSocket(wsUri);

        websocket.onopen = function (e) {
            writeToScreen('CONNECTED');
            doSend('WebSocket rocks');
        };

        websocket.onclose = function (e) {
            writeToScreen('DISCONNECTED');
        };

        websocket.onmessage = function (e) {
            writeToScreen(e.data);
        };

        websocket.onerror = function (e) {
            writeToScreen(e.data);
        };

        function startsend(message) {
            counter = setInterval(() => {
                websocket.send(message);
            })
        }
        function EndInterval() {
            clearInterval(counter);
        }
        //#endregion

    </script>
</body>

</html>";

}
