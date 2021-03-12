module.exports.write = (message) => {

    // run `node server.js` with NodeIPC.exe in the same directory.

    var spawn = require('child_process').spawn;

    var ipc = spawn("../RemoteController/bin/Release/net5.0/RemoteController.exe");
    ipc.stdin.setEncoding("utf8");

    ipc.stderr.on('data', function (data) {
        process.stdout.write(data.toString());
    });

    ipc.stdout.on('data', function (data) {
        process.stdout.write(data.toString());
    });

    console.log("Sending: ", JSON.stringify(message));

    ipc.stdin.write(message);

    // to allow sending messages by typing into the console window.
    var stdin = process.openStdin();

    stdin.on('data', function (data) {
        ipc.stdin.write(data);
    });
}