//import http = require('http');
//var express = require('express');
//var sys = require('sys');
//var ws = require('express-ws');
//var port = process.env.port || 1337
//var clients = [];
//var app = express();
//ws.createServer(function (websocket) {
//    clients.push(websocket);
//    websocket.addListener("connect", function (resource) {
//        sys.debug("Connect:" + resource);
//    }).addListener("data", function (data) {
//        for (var i = 0; i < clients.length; i++) {
//            clients[i].write(data);
//        }
//    }).addListener("close", function () {
//        sys.debug("close");
//    });
//}).listen(port,function() {
//    console.log("Server started on port:" + port);
//});
////app.use(express.static(__dirname + "/assets/libs"));
////app.get('/', function (req, res) {
////    res.sendFile(__dirname + '/views/home.html');
////});
////app.listen(port, function () {
////    console.log("Server started on port:" + port);
////});
////http.createServer(function (req, res) {
////    res.writeHead(200, { 'Content-Type': 'text/plain' });
////    res.('Hello World\n');
////}).listen(port);
var express = require('express');
var app = express();
var expressWs = require('express-ws')(app);
app.use(function (req, res, next) {
    console.log('middleware');
    req.testing = 'testing';
    return next();
});
app.get('/', function (req, res, next) {
    console.log('get route', req.testing);
    res.end("Hello from streaming server");
});
app.ws('/', function (ws, req) {
    ws.on('message', function (msg) {
        console.log(msg);
    });
    console.log('socket', req.testing);
});
app.listen(3000, function () {
    console.log("Server running on port 3000.");
});
//# sourceMappingURL=server.js.map