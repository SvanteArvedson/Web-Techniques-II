var MessageBoard = {

    messages: [],
    textField: null,
    messageArea: null,

    init:function(e)
    {
	
		    MessageBoard.textField = document.getElementById("inputText");
		    MessageBoard.nameField = document.getElementById("inputName");
            MessageBoard.messageArea = document.getElementById("messagearea");
    
            // Add eventhandlers    
            document.getElementById("inputText").onfocus = function(e){ this.className = "focus"; };
            document.getElementById("inputText").onblur = function(e){ this.className = "blur"; };
            document.getElementById("buttonSend").onclick = function(e) {MessageBoard.sendMessage(); return false;};
    
            MessageBoard.textField.onkeypress = function(e){ 
                                                    if(!e) var e = window.event;
                                                    
                                                    if(e.keyCode == 13 && !e.shiftKey){
                                                        MessageBoard.sendMessage(); 
                                                       
                                                        return false;
                                                    }
                                               };
    
    },
    getAllMessages:function() {
        $.ajax({
			type: "GET",
			url: "get.php",
			data: {function: "getAllMessages"},
			success: function(data) { // called when the AJAX call is ready
				data = JSON.parse(data);
				for(var mess in data) {
					var obj = data[mess];
				    var text = obj.name +" said:\n" +obj.message;
					var date = obj.timestamp != null ? new Date(obj.timestamp*1000) : new Date();
					var mess = new Message(text, date);
	                var messageID = MessageBoard.messages.push(mess)-1;
	                MessageBoard.renderMessage(messageID);
				}
				document.getElementById("nrOfMessages").innerHTML = MessageBoard.messages.length;
			},
			complete: MessageBoard.getNewMessages
		});
	

    },
    getNewMessages: function() {
    	var lastMessage = $(".message").first();
    	var timestring = $("span", lastMessage).text().replace(new RegExp("-", "g"), "/");
    	var timestamp = Math.floor((Date.parse(timestring)).valueOf()/1000);
    	
        $.ajax({
			type: "GET",
			url: "get.php",
			data: {function: "getNewMessages", timestamp: timestamp},
			success: function(data) { // called when the AJAX call is ready
				if (data != "") {
					data = JSON.parse(data);
					for(var mess in data) {
						var obj = data[mess];
					    var text = obj.name +" said:\n" +obj.message;
						var date = obj.timestamp != null ? new Date(obj.timestamp*1000) : new Date();
						var mess = new Message(text, date);
		                var messageID = MessageBoard.messages.push(mess)-1;
		                MessageBoard.renderMessage(messageID);
					}
					document.getElementById("nrOfMessages").innerHTML = MessageBoard.messages.length;
				}
			},
			complete: MessageBoard.getNewMessages
		});
	

    },
    sendMessage:function(){
        
        if(MessageBoard.textField.value == "") return;
        
        // Make call to ajax
        $.ajax({
			type: "GET",
		  	url: "post.php",
		  	data: {function: "add", name: MessageBoard.nameField.value, message: MessageBoard.textField.value}
		}).done(function(data) {
			$("#inputText").val("");
		});
    
    },
    renderMessages: function(){
        // Remove all messages
        MessageBoard.messageArea.innerHTML = "";
     
        // Renders all messages.
        for(var i=0; i < MessageBoard.messages.length; ++i){
            MessageBoard.renderMessage(i);
        }        
        
        document.getElementById("nrOfMessages").innerHTML = MessageBoard.messages.length;
    },
    renderMessage: function(messageID){
        // Message div
        var div = document.createElement("div");
        div.className = "message";
       
        // Clock button
        aTag = document.createElement("a");
        aTag.href="#";
        aTag.onclick = function(){
			MessageBoard.showTime(messageID);
			return false;			
		};
        
        var imgClock = document.createElement("img");
        imgClock.src="pic/clock.png";
        imgClock.alt="Show creation time";
        
        aTag.appendChild(imgClock);
        div.appendChild(aTag);
       
        // Message text
        var text = document.createElement("p");
        text.innerHTML = MessageBoard.messages[messageID].getHTMLText();        
        div.appendChild(text);
            
        var spanDate = document.createElement("span");
        spanDate.appendChild(document.createTextNode(MessageBoard.messages[messageID].getDateText()));

        div.appendChild(spanDate);        
        
        var spanClear = document.createElement("span");
        spanClear.className = "clear";

        div.appendChild(spanClear);        
        
        $("#messagearea").prepend(div);       
    },
    removeMessage: function(messageID){
		if(window.confirm("Vill du verkligen radera meddelandet?")){
        
			MessageBoard.messages.splice(messageID,1); // Removes the message from the array.
        
			MessageBoard.renderMessages();
        }
    },
    showTime: function(messageID){
         
         var time = MessageBoard.messages[messageID].getDate();
         
         var showTime = "Created "+time.toLocaleDateString()+" at "+time.toLocaleTimeString();

         alert(showTime);
    }
};

function Message(message, date){

	this.getText = function() {
		return message;
	};

	this.setText = function(_text) {
		message = text;
	};

	this.getDate = function() {
		return date;
	};

	this.setDate = function(_date) {
		date = date;
	};

}

Message.prototype.toString = function(){
	return this.getText()+" ("+this.getDate()+")";
};

Message.prototype.getHTMLText = function() {
      
    return this.getText().replace(/[\n\r]/g, "<br />");
};

Message.prototype.getDateText = function() {
    return this.getDate().toLocaleString();
};

window.onload = MessageBoard.init;