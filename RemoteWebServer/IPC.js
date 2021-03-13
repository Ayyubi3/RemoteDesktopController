let fs = require('fs');
let array = fs.readFileSync('../Settings.txt').toString().split("\n");

let spawn = require('child_process').spawn;

let ipc = spawn(array[0]);
ipc.stdin.setEncoding("utf8");

ipc.stderr.on('data', function (data) {
    process.stdout.write(data.toString());
});

ipc.stdout.on('data', function (data) {
    process.stdout.write(data.toString());
});

let stdin = process.openStdin();

stdin.on('data', function (data) {
    ipc.stdin.write(data);
});

module.exports.write = (message) => {
    ipc.stdin.write(message);
}