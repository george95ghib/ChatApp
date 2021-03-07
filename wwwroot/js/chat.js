"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

// disable send button until connection on
document.getElementById("sendButton").disabled = true;


connection.on("ReceiveMessage", function (data)
{

    console.log(data);
    var messageDiv = document.createElement("div");
    messageDiv.classList.add("message");

    document.getElementById("chat-body").appendChild(messageDiv);

    var header = document.createElement("HEADER");
    header.innerHTML = data.name;

    messageDiv.appendChild(header);

    
    var paragraph = document.createElement("P");
    paragraph.innerHTML = data.text;

    messageDiv.appendChild(paragraph);

    var footer = document.createElement("FOOTER");
    footer.innerHTML = data.sentAt;

    messageDiv.appendChild(footer);

});

// enable send button when connection on
connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err)
{
    return console.error(err.toString());
});


$(function () {

    var message = document.getElementById("message-input").value;
    var chatId = document.getElementById("chatId").value;

    $('sendButton').click(function (event) {   
           
      $.post(
        '/Home/SendButton', { 
          chatId: chatId, 
          message: message
        }, function(response) {
                alert('Got this from the server: ' + response);
        });
      event.preventDefault();
    });

    
});



// document.getElementById("sendButton").addEventListener("click", function (event)
// {
//     var message = document.getElementById("message-input").value;
//     var chatId = document.getElementById("chatId").value;


//     const url = "/Home/SendMessage";
//     const msgBuild = {
//         chatId : chatId,
//         message : message
//     }

    
//     $.post(url, msgBuild, 
//        function(data, status, jqXHR) {
//                 console.log(status);
//         });


//     //connection.invoke("SendMessage", message).catch(function (err)
//    // {
//      //   return console.error(err.toString());
//    // });

//     // clear input
//     document.getElementById("message-input").value = "";

//     event.preventDefault();

    
    
    
// });

