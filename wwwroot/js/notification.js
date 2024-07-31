"use strict";

const connection = new signalR.HubConnectionBuilder().withUrl("/notificationHub").build();

connection.on("NewPost", (message) => {
    alert(message)
    location.reload();
});

connection.start().catch(err => console.error(err.toString()));