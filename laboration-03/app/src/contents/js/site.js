'use strict';

var site = {};

// Main function for application
site.run = function() {
	var mapService = new site.MapService($("#map-canvas")[0]);
	var trafficMessageService = new site.TrafficMessageService();
	
	var messages = trafficMessageService.getTrafficMessages();
	
	site.renderMessages(messages);
	mapService.showTrafficMessages(messages);
};

site.renderMessages = function(trafficMessages) {
	var HTMLstring = "";
	for (var i = trafficMessages.length - 1; i >= 0; i -= 1) {
		var trafficMessage = trafficMessages[i];
		
		var dateString = trafficMessage.date.toLocaleString();
		HTMLstring += "<li>" +
							"<div class='panel panel default'>" +
								"<div class='panel-body trafficMessage'>" +
									"<h2><i>" + dateString + "</i>, " + trafficMessage.title + "</h2>" +
									"<p>" + trafficMessage.exactlocation + "</p>" + 
									"<p>" + trafficMessage.description + "</p>" +
								"<div>" +
		 					"<div>" +
	 					"</li>";
	}
	$("#trafficMessages").html(HTMLstring);
	
	site.initiateJPages();
};

site.initiateJPages = function() {
	$(function() {
	    /* initiate plugin */
	    $("#paginationHolder").jPages({
	        containerID : "trafficMessages",
	        perPage : 6
	    });
	    
	    /* on select change */
	    $("select").change(function(){
	      
	        /* get new nº of items per page */
	      var newPerPage = parseInt( $(this).val() );
	      
	      /* destroy jPages and initiate plugin again */
	      $("#paginationHolder").jPages("destroy").jPages({
	            containerID   : "trafficMessages",
	            perPage       : newPerPage
	        });
	    });
	});
};

site.MapService = function(canvasNode) {
	// private
	var mapOptions = {
		center : new google.maps.LatLng(63, 17),
		zoom : 5
	};
	
	// private
	var infoWindow = new google.maps.InfoWindow();
	
	// private
	var markers = [];
	
	// private
	var map = new google.maps.Map(canvasNode, mapOptions);

	// private
	function showMarkers(category) {
		hideMarkers();
		
		category = category != undefined ? category : null;
		for (var object in markers) {
			var marker = markers[object];
			if (category == null || category == marker.category) {
				marker.setMap(map);
			}
		}
	};
	
	// private
	function hideMarkers() {
		for (var object in markers) {
			var marker = markers[object];
			marker.setMap(null);
		}
	}
	
	// private
	function createInfoWindowContent(title, date, category, exactlocation, description) {
		var dateString = date.toLocaleString();
		return '<div id="content">' + 
					'<h1 id="firstHeading" class="firstHeading">' + title + '</h1>' + 
					'<div id="bodyContent">' + 
						'<p>' + category + ', ' + dateString + '</p>' + 
						'<p>' + exactlocation + '</p>' + 
						'<p>' + description + '</p>' + 
					'</div>' + 
				'</div>';
	};
	
	// private
	function createMarker(trafficMessage) {
		var marker = new google.maps.Marker({
			map : null,
			position : new google.maps.LatLng(
				trafficMessage.latitude, 
				trafficMessage.longitude
			),
			title : trafficMessage.title
		});
		marker.infoWindowContent = createInfoWindowContent(
			trafficMessage.title, 
			trafficMessage.date, 
			trafficMessage.category, 
			trafficMessage.exactlocation, 
			trafficMessage.description
		);
		marker.category = trafficMessage.category;
		
		google.maps.event.addListener(marker, 'click', function() {
			infoWindow.setContent(this.infoWindowContent);
			infoWindow.open(map, this);
		});
		
		return marker;
	}

	// public
	this.showTrafficMessages = function(trafficMessages) {
		for (var i in trafficMessages) {
			var message = trafficMessages[i];
			markers.push(createMarker(message));
		}
		showMarkers();
	};
};

site.TrafficMessageService = function() {
	// private
	var that = this;
	
	// private
	var trafficMessages = [];

	// private
	function fetchNewTrafficMessages() {
		$.ajax({
			url : "index.php?action=getNewTrafficMessages",
			async : false
		}).done(function(data) {
			var data = jQuery.parseJSON(data);
			for (var object in data.messages) {
				var message = data.messages[object];
				
				trafficMessages.push(new site.TrafficMessage(
					message.title, 
					message.latitude, 
					message.longitude, 
					message.exactlocation, 
					message.createddate, 
					message.category, 
					message.description
				));
			}
		}).fail(function() {
			//TODO: add proper error handling
			alert("Ett fel intfräffade");
		});
	};

	// public
	this.getTrafficMessages = function() {
		if (trafficMessages.length < 1) {
			fetchNewTrafficMessages();
		}

		return trafficMessages;
	};
};

// Constructor
site.TrafficMessage = function(title, latitude, longitude, exactlocation, date, category, description) {
	this.title = title;
	this.latitude = latitude;
	this.longitude = longitude;
	this.exactlocation = exactlocation;
	this.date = new Date(parseInt(date.match(/(\d+)/)[0]));
	switch(category) {
	case 0:
		this.category = "Vägtrafik";
		break;
	case 1:
		this.category = "Kollektivtrafik";
		break;
	case 2:
		this.category = "Planerad störning";
		break;
	default:
		this.category = "Övrigt";

	}
	this.description = description;
};