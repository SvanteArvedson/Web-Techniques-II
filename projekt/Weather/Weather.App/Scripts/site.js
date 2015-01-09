var Site = {
    keyPlaces: "visitedPlaces",
    maxNbrPlaces: 20,

    isOnline: function () {
        return navigator.onLine;
    },

    modifyForOnline: function () {
        // hide offinfo, show account-bar and enable forms
        $("#account-bar").show();
        $("input").prop("disabled", false);
    },

    modifyForOffline: function () {
        // show offline info, hide account-bar and disable forms
        $("#account-bar").hide();
        $("input").prop("disabled", true);
        $("<div id='offline-info' class='col-xs-12'><div class='alert alert-warning alert-dismissable' role='alert'><button type='button' class='close' data-dismiss='alert'><span>&times;</span><span class='sr-only'>Close</span></button><p>Du är just nu offline. Du kan endast se de sidor som du tidigare har varit på. Alla funktionerna är inte tillgängliga i offlineläge, exemelvis inloggning och utloggning.</p></div></div>").prependTo(".container");
    },

    localPlaceObject: function (country, region, name) {
        this.country = country;
        this.region = region;
        this.name = name;
    },

    saveLocalWeather: function(placeObject) {
        if (!localStorage.getItem(Site.keyPlaces)) {
            localStorage.setItem(Site.keyPlaces, new Array(20));
        }

        localStorage.getItem(Site.keyPlaces).unshift(JSON.stringify(placeObject));

        if (localStorage.getItem(Site.keyPlaces).length > Site.maxNbrPlaces) {
            var nbrToRem = localStorage.getItem(Site.keyPlaces).length - Site.maxNbrPlaces;
            localStorage.getItem(Site.keyPlaces).splice(Site.maxNbrPlaces, nbrToRem);
        }
    },

    addLinksToVisitedPlaces: function() {
        // Fix tomorrow...
        // Render links to visited places here...
        // also, fix fallback page.
    }
};

window.applicationCache.onupdateready = function (e) {
    applicationCache.swapCache();
    window.location.reload();
}

window.ononline = function (e) {
    Site.modifyForOnline();
}

window.onoffline = function (e) {
    Site.modifyForOffline();
}

window.onload = function (e) {
    if (Site.isOnline) {
        if (Site.viewIsWeatherForecast) {
            var placeString = $("#place").text();
            var placeArray = placeString.split(", ");
            var placeObject = new Site.localPlaceObject("Sverige", placeArray[1], placeArray[0]);
            Site.saveLocalWeather(placeObject);
        }
        else if (Site.viewIsIndex) {
            Site.addLinksToVisitedPlaces();
        }
    } else {
        Site.modifyForOffline();
    }
}