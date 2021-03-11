"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
$("#sendButton").prop("disabled", true);

connection.on("ReceiveMessage", function (data) {

  var dateFormat = new Date(data.sentAt);

  var $messageDiv = $("<div>", {class: "message"});

  $("<header></header>").text(data.name + ": ").appendTo($messageDiv);
  $("<p></p>").text(data.text).appendTo($messageDiv);
  $("<footer></footer>").text(dateFormat.toLocaleString()).appendTo($messageDiv);

  $messageDiv.appendTo("#chat-body");
});

// When user enter in a chat room, add user to the group chat
connection.start().then(function () {

  var chatId = $("#chatId").val();

  $("#sendButton").prop("disabled", false);
  connection.invoke("joinChat", chatId);

}).catch(function (err) {

  return console.error(err.toString());

});

$('#sendButton').click(function (event) {
  var inputMessage = $("#message").val();
  var chatId = $("#chatId").val();
  $.post(
    '/Home/SendMessage', {
      chatId: chatId,
      message: inputMessage
  }, function () {
      $("#message").val("");
  });
  event.preventDefault();
});

// When user change leave this chat window, remove user from group
window.addEventListener('unload', function(event) {

  var chatId = $("#chatId").val();
  connection.invoke("leaveChat", chatId);
});