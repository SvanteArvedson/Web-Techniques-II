'use strict';

var site = {};

// Application root
site = function() {
	var that = this;
	
	// private
	var trafficMessageService = new site.TrafficMessageService();
	
	// private
	var trafficMessages = trafficMessageService.getTrafficMessages();
	
	// private
	var trafficMessageRenderer = new site.trafficMessageRenderer(trafficMessages); 
	
	// private
	var mapService = new site.MapService($("#map-canvas")[0], trafficMessages);
	
	// private
	function removeClassActive() {
		$("#sortButtons").children("a").each(function() {
			if ($(this).attr("class") == "btn btn-default active") {
				$(this).attr("class", "btn btn-default");
			}
		});
	};
	
	// private
	function initializeSorting() {
		$("#sortButtons").children("a").each(function() {
			var node = $(this);
			var category;
			
			switch(node.attr("id")) {
				case "sortAllCat" :
					category = null;
					break;
				case "sortRoadCat" :
					category = "Vägtrafik";
					break;
				case "sortPublicTransportCat" :
					category = "Kollektivtrafik";
					break;
				case "sortPlannedCat" :
					category = "Planerad störning";
					break;
				default :
					category ="Övrigt";
			}
			
			node.click(function() {
				if (node.attr("class") != "btn btn-default active") {
					removeClassActive();
					node.attr("class", "btn btn-default active");
					renderTrafficMessages(category);
					
					return false;
				}
			});
		});
	}
	
	// private
	function renderTrafficMessages(category) {
		trafficMessageRenderer.renderTrafficMessages(category);
		mapService.renderTrafficMessages(category);
	};
	
	// public, main function for application
	this.run = function() {
		initializeSorting();
		renderTrafficMessages();
	};
};

site.trafficMessageRenderer = function(trafficMessages) {
	//private
	var trafficMessagePanels = createTrafficMessagePanels(trafficMessages);
	
	//private
	function createTrafficMessagePanels(trafficMessages) {
		var ret = [];
		for (var index in trafficMessages) {
			var trafficMessage = trafficMessages[index];	
			var dateString = trafficMessage.date.toLocaleString();
			
			var trafficMessagePanel = {
				category: trafficMessage.category,
				
				html: "<li>" +
							"<div class='panel panel default'>" +
								"<div class='panel-body trafficMessage'>" +
									"<h2><i>" + dateString + "</i>, " + trafficMessage.title + "</h2>" +
									"<p><i>" + trafficMessage.category + " - " + trafficMessage.subcategory + "</i></p>" +
									"<p>" + trafficMessage.exactlocation + "</p>" + 
									"<p>" + trafficMessage.description + "</p>" +
								"<div>" +
		 					"<div>" +
	 					"</li>"
			};
		 	ret.push(trafficMessagePanel);
		}
		return ret;
	};
	
	// private
	function initiateJPages() {
		$(function() {
		    /* initiate plugin */
		    $("#paginationHolder").jPages({
		        containerID : "trafficMessages",
		        perPage : 5
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
	
	// private
	function removeTrafficMessages() {
		$("#trafficMessages").html("");
	}
	
	// public
	this.renderTrafficMessages = function(category) {
		removeTrafficMessages();
		
		category = category != undefined ? category : null;
		
		var HTMLstring = "";
		
		for (var i = trafficMessagePanels.length - 1; i >= 0; i -= 1) {
			var trafficMessagePanel = trafficMessagePanels[i];
			
			if (category == null || category == trafficMessagePanel.category) {
				HTMLstring += trafficMessagePanel.html;
			}
		}
		$("#trafficMessages").html(HTMLstring);
		initiateJPages();
	};
};

site.MapService = function(canvasNode, trafficMessages) {
	// private
	var mapOptions = {
		center : new google.maps.LatLng(63, 17),
		zoom : 5
	};
	
	// private
	var infoWindow = new google.maps.InfoWindow();
	
	// private
	var markers = createMarkers(trafficMessages);
	
	// private
	var map = new google.maps.Map(canvasNode, mapOptions);
	
	// private
	function hideMarkers() {
		for (var object in markers) {
			var marker = markers[object];
			marker.setMap(null);
		}
	}
	
	// private
	function createInfoWindowContent(trafficMessage) {
		var dateString = trafficMessage.date.toLocaleString();
		return "<div id='content'>" + 
					"<h1 id='firstHeading' class='firstHeading'>" + dateString + "</i>, " + trafficMessage.title + "</h1>" + 
					"<div id='bodyContent'>" + 
						"<p><i>" + trafficMessage.category + " - " + trafficMessage.subcategory + "</i></p>" +
						"<p>" + trafficMessage.exactlocation + "</p>" + 
						"<p>" + trafficMessage.description + "</p>" +
					"</div>" + 
				"</div>";
	};
	
	// private
	function createMarkers(trafficMessages) {
		var ret = [];
		
		for (var index in trafficMessages) {
			var trafficMessage = trafficMessages[index];
			
			var marker = new google.maps.Marker({
				map : null,
				position : new google.maps.LatLng(
					trafficMessage.latitude, 
					trafficMessage.longitude
				),
				title : trafficMessage.title
			});
			marker.infoWindowContent = createInfoWindowContent(trafficMessage);
			marker.category = trafficMessage.category;
			
			google.maps.event.addListener(marker, 'click', function() {
				infoWindow.setContent(this.infoWindowContent);
				infoWindow.open(map, this);
			});
			
			ret.push(marker);
		}
		
		return ret;
	}

	// public
	this.renderTrafficMessages = function(category) {
		hideMarkers();
		category = category != undefined ? category : null;
		
		for (var object in markers) {
			var marker = markers[object];
			if (category == null || category == marker.category) {
				marker.setMap(map);
			}
		}
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
					message.subcategory,
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
site.TrafficMessage = function(title, latitude, longitude, exactlocation, date, category, subcategory, description) {
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
	this.subcategory = subcategory;
	this.description = description;
};