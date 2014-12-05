'use strict';

var Site = {
	initialize: function() {
		var canvas = $("#map-canvas");
		
		var mapOptions = {
			center: new google.maps.LatLng(59.4249589, 24.7382414),
			zoom: 12
		};
		
		var map = new google.maps.Map(canvas[0], mapOptions);
	}
};