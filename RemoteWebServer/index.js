let IPC = require("./IPC");
let Express = require("express");
let app = Express();

app.use(Express.static('public'))

app.get("/", (req, res) => {
    res.sendFile("public/index.html");
});

const PATH = process.argv[2];

//#region app.get paths
app.get("/add1x", (req, res) => {
    IPC.write("add1x", PATH);
    res.redirect("/");
});
app.get("/sub1x", (req, res) => {
    IPC.write("sub1x", PATH);
    res.redirect("/");
});
app.get("/add1y", (req, res) => {
    IPC.write("add1y", PATH);
    res.redirect("/");
});
app.get("/sub1y", (req, res) => {
    IPC.write("sub1y", PATH);
    res.redirect("/");
});
app.get("/click", (req, res) => {
    IPC.write("click", PATH);
    res.redirect("/");
});

//#endregion
app.listen(3003, () => {});

