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
	
	this.startAnimation = function(trafficMessageId) {
		mapService.startAnimation(trafficMessageId);
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
				id: trafficMessage.id,
				
				category: trafficMessage.category,
				
				html: "<li>" +
							"<div id='" + trafficMessage.id + "' class='panel panel-default'>" +
								"<div class='panel-heading'>" +
									"<h2>" + dateString + ", " + trafficMessage.title + "</h2>" +
								"</div>" +
								"<div class='panel-body trafficMessage'>" +
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
	function createTrafficMessagePanelClickEvents(trafficMessagePanels) {
		for (var index in trafficMessagePanels) {
			var trafficMessagePanel = trafficMessagePanels[index];
			
			$("#" + trafficMessagePanel.id).click(function(event){
				site.startAnimation($(this).attr("id"));
			});
		}
	}
	
	// private
	function initiateJPages() {
		$(function() {
		    /* initiate plugin */
		    $("#paginationHolder").jPages({
		        containerID : "trafficMessages",
		        perPage : 10
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
		
		createTrafficMessagePanelClickEvents(trafficMessagePanels);
		
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
			
			marker.id = trafficMessage.id;
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

	// private
	function stopAnimation(marker) {
		setTimeout(function() {
			marker.setAnimation(null);
		}, 2200);	
	}

	// public
	this.startAnimation = function(trafficMessageId) {
		for (var index in markers) {
			var marker = markers[index];
			if (marker.id == trafficMessageId) {
				marker.setAnimation(google.maps.Animation.BOUNCE);
				stopAnimation(marker);
			}
		}
	};

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
	var trafficMessages = [];
	
	// private
	function fetchNewTrafficMessages() {
		$.ajax({
			url : "index.php?action=getNewTrafficMessages",
			async : false
		}).done(function(rawData) {
			console.log(rawData);
			
			var data = jQuery.parseJSON(rawData);
			
			console.log(data);
			
			for (var object in data.messages) {
				var trafficMessageRaw = data.messages[object];
				trafficMessages.push(new site.TrafficMessage(trafficMessageRaw));
			}
		}).fail(function() {
			//TODO: add proper error handling
			alert("Ett fel intfräffade");
		});
	};

	// public
	this.getTrafficMessages = function() {
		fetchNewTrafficMessages();
		return trafficMessages;
	};
};

// Constructor
site.TrafficMessage = function(trafficMessageRaw) {
	this.id = trafficMessageRaw.id;
	this.title = trafficMessageRaw.title;
	this.latitude = trafficMessageRaw.latitude;
	this.longitude = trafficMessageRaw.longitude;
	this.exactlocation = trafficMessageRaw.exactlocation;
	var dateAsString = trafficMessageRaw.createddate.match(/(\d+)/)[0];
	this.date = new Date(parseInt(dateAsString));
	switch(trafficMessageRaw.category) {
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
	this.subcategory = trafficMessageRaw.subcategory;
	this.description = trafficMessageRaw.description;
};