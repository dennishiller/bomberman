var app = require('express')();
var server = require('http').Server(app);
var io = require('socket.io')(server);

server.listen(3000);

//global variables

let MAXPLAYERS = 2;
let p1SpawnPoint = [];
let p2SpawnPoint = [];
var clients = [];

// response in the browser to make sure localhost is working
app.get('/', function(req, res){
    res.send('hey you got back get "/"');
});

io.on('connection', function(){

    var currentPlayer = {};
    currentPlayer.name = 'unknown';

    socket.on('player_connect', function(){
        console.log(currentPlayer.name+"player connect");
        for (var i=0; i<clients.length;i++){
            var playerConnected = {
                name: clients[i].name,
                position: clients[i].position,
                rotation: clients[i].rotation,
                isAlive: clients[i].isAlive
            };
            socket.emit('other player connected ', playerConnected);
            console.log(currentPlayer.name + ' emit: other player connected '+ JSON.stringify(playerConnected));

        }

    });

    socket.on('play', function(data){
        console.log(currentPlayer.name + " recv: play: "+JSON.stringify(data));
        if(clients.length === 0){
            
        }
    });



});

// console output when server starts running
console.log('--- Server is running ...');