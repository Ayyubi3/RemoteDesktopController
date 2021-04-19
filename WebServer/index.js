let html = require('fs').readFileSync('./index.html', )
const WebSocket = require('ws')
const server = new WebSocket.Server({ port: '8080' })


let Controller = require('child_process').spawn("../Controller/bin/Release/net5.0/Controller.exe")

Controller.stdin.setEncoding("utf8");

Controller.stderr.on('data', function(data) {
    process.stdout.write(data.toString());
});

Controller.stdout.on('data', function(data) {
    process.stdout.write(data.toString());
});

server.on('connection', socket => {

    console.log("Connected")

    socket.on('message', message => {
        console.log(message + " send")

        Controller.stdin.write(message)

    });

});

const http = require('http');

http.createServer((req, res) => {
    res.writeHeader(200, { "Content-Type": "text/html" });
    res.write(html);
    res.end();
}).listen(3000)