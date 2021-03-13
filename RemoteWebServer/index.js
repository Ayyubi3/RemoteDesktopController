let IPC = require("./IPC");
let Express = require("express");
let app = Express();

app.use(Express.static('public'));

app.get("/", (req, res) => {
    res.sendFile("public/index.html");
});

app.get("/add1x", (req, res) => {
    IPC.write("add1x");
    res.redirect("/");
});
app.get("/sub1x", (req, res) => {
    IPC.write("sub1x");
    res.redirect("/");
});
app.get("/add1y", (req, res) => {
    IPC.write("add1y");
    res.redirect("/");
});
app.get("/sub1y", (req, res) => {
    IPC.write("sub1y");
    res.redirect("/");
});
app.get("/click", (req, res) => {
    IPC.write("click");
    res.redirect("/");
});

app.listen(3003, () => {});

