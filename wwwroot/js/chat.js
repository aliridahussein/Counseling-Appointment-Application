
"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/Home/LiveChat").build();
connection.on('ReceiveMessage', addMessageToChat);

connection.start()
    .catch(error => {
        console.error(error.message);
    });

function sendMessageToHub(message) {
    connection.invoke('SendMessage', message);
}

function connect() {
    

   connection.on("ReceiveMessage", function (user, message) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var encodedMsg = user+":" + " " + msg;
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);

    
   });
    
  connection.start().then(function () {
    document.getElementById("sendButton").type = "button";
    document.getElementById("Status").innerText = "Connected";
    document.getElementById("connectionon").type = "hidden";
    document.getElementById("connectionoff").type = "button";
  }).catch(function (err) {
    return console.error(err.toString());
  });
}
function disconnect() {

    window.location.reload();
}

document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    var foruser = document.getElementById("foruser").value;
    connection.invoke("SendMessage",foruser, user, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});