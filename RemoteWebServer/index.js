let IPC = require("./IPC");
let Express = require("express");
let app = Express();


app.get("/", (req, res) => {
});

const PATH = process.argv[2];

//#region app.get paths
app.get("/add1x", (req, res) => {
    IPC.write("add1x", PATH);
});
app.get("/sub1x", (req, res) => {
    IPC.write("sub1x", PATH);
});
app.get("/add1y", (req, res) => {
    IPC.write("add1y", PATH);
});
app.get("/sub1y", (req, res) => {
    IPC.write("sub1y", PATH);
});
//#endregion
app.listen(3000, () => {});

