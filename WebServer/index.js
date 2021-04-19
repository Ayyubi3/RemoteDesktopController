const WebSocket = require('ws')
const server = new WebSocket.Server({ port: '8080' })

//Start the C# controller app
let Controller;
try {
    Controller = require('child_process').spawn("../Controller/bin/Release/net5.0/Controller.exe")
} catch (e) { Log(e.message) }


Controller.stdin.setEncoding("utf8");

Controller.stderr.on('data', function(data) {
    Log(data.toString());
});

Controller.stdout.on('data', function(data) {
    Log(data.toString());
});

server.on('connection', socket => {

    Log("Connected")

    socket.on('message', message => {
        Log(message + " send")

        Controller.stdin.write(message)

    });

});
let html
try { html = require('fs').readFileSync('./index.html') } catch (e) { Log(e.message) }

const http = require('http');

http.createServer((req, res) => {
    res.writeHeader(200, { "Content-Type": "text/html" });
    res.write(html);
    res.end();
}).listen(3000)

const Log = message => console.log("WebServer: " + message)