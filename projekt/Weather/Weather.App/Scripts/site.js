var Site = {
    keyPlaces: "visitedPlaces",
    maxNbrPlaces: 20,

    isOnline: function () {
        return navigator.onLine;
    },

    modifyForOnline: function () {
        // hide offline-info, show account-bar and enable forms
        $("#account-bar").show();
        $("input").prop("disabled", false);
        $("#offline-info").hide();
        $("#viewedPlaces").hide();
    },

    modifyForOffline: function () {
        // show offline-info, hide account-bar and disable forms
        $("#account-bar").hide();
        $("input").prop("disabled", true);
        $("<div id='offline-info' class='col-xs-12'><div class='alert alert-warning alert-dismissable' role='alert'><button type='button' class='close' data-dismiss='alert'><span>&times;</span><span class='sr-only'>Close</span></button><p>Du är just nu offline. Du kan endast se de sidor som du tidigare har varit på. Alla funktionerna är inte tillgängliga i offlineläge, exemelvis inloggning och utloggning.</p></div></div>").prependTo(".container");

        // show vired places if view is index
        if (Site.viewIsIndex) {
            var viewedForecastsHTML = "<div id='viewedPlaces' class='row'><div class='text-center col-xs-12'><h2>Tidigare besökta sidor</h2><ul class='list-unstyled'>";

            if (localStorage.getItem(Site.keyPlaces)) {
                var urlArray = JSON.parse(localStorage.getItem(Site.keyPlaces));
                urlArray.forEach(function (element, index, array) {
                    element = decodeURI(element);
                    viewedForecastsHTML += "<li><a href='" + element + "'>" + element + "</a></li>";
                });
            } else {
                viewedForecastsHTML += "<li>Inga besökta sidor</li>";
            }

            viewedForecastsHTML += "</ul>";
            $("main").append(viewedForecastsHTML);
        }
    },

    saveWeatherUrl: function (weatherUrl) {
        var urlArray;

        if (localStorage.getItem(Site.keyPlaces)) {
            urlArray = JSON.parse(localStorage.getItem(Site.keyPlaces));
        } else {
            urlArray = [];
        }

        if ($.inArray(weatherUrl, urlArray) == -1) {
            urlArray.unshift(weatherUrl);
        }

        if (urlArray.length > Site.maxNbrPlaces) {
            var nbrToRem = urlArray.length - Site.maxNbrPlaces;
            urlArray.splice(Site.maxNbrPlaces, nbrToRem);
        }

        localStorage.setItem(Site.keyPlaces, JSON.stringify(urlArray));
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
    if (Site.isOnline()) {
        if (Site.viewIsWeatherForecast) {
            Site.saveWeatherUrl(location.href);
        }
    } else {
        Site.modifyForOffline();
    }
}