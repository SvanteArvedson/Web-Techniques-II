<!DOCTYPE html>
<html lang="sv">
    <head>
        <meta charset="utf-8">
        <title>Laboration 3</title>
        <meta name="viewport" content="width=device-width; initial-scale=1.0">
        <link href="//maxcdn.bootstrapcdn.com/bootstrap/3.3.1/css/bootstrap.min.css" rel="stylesheet">
        <link rel="stylesheet" href="<?php echo dirname($_SERVER['PHP_SELF']) . "/src/contents/css/site.css"; ?>" />
    </head>
    <body>
        <main class="container">
            <div class="row">
                <header class="col-xs-12">
                    <h1 class="page-header">Laboration 3 - Svante Arvedson</h1>
                </header>
            </div>
            <div class="row">
                <nav>
                    <div id="sortButtons" class="btn-group" role="group">
                        <a id="sortAllCat" class="btn btn-default active" href="#">Alla kategorier</a>
                        <a id="sortRoadCat" class="btn btn-default" href="#">Vägtrafik</a>
                        <a id="sortPublicTransportCat" class="btn btn-default" href="#">Kollektivtrafik</a>
                        <a id="sortPlannedCat" class="btn btn-default" href="#">Planerad störning</a>
                        <a id="sortOtherCat" class="btn btn-default" href="#">Övrigt</a>
                    </div>
                </nav>
            </div>
            <section class="row">
                <div id="map-canvas" class="col-xs-7">
                    <img class="ajax-loader center-block" src="<?php echo dirname($_SERVER['PHP_SELF']) . "/src/contents/img/ajax-loader.gif"; ?>" />
                </div>
                <div class="col-xs-5">
                    <div class="col-xs-12">
                        <div class="paginationDiv">
                            <ul id="paginationHolder" class="pagination"></ul>
                        </div>
                    </div>
                    <div class="col-xs-12">
                        <div id="trafficMessagesContainer">
                            <ul id="trafficMessages"></ul>
                        </div>
                    </div>
                </div>
            </section>
        </main>
        <script src="//code.jquery.com/jquery-2.1.1.min.js"></script>
        <script src="//maxcdn.bootstrapcdn.com/bootstrap/3.3.1/js/bootstrap.min.js"></script>
        <script src="//maps.googleapis.com/maps/api/js?key=<?php echo \settings\AppSettings::GOOGLE_API_KEY; ?>"></script>
        <script src="<?php echo dirname($_SERVER['PHP_SELF']) . "/src/contents/js/site.min.js"; ?>"></script>
        <script type="text/javascript">
            var site = new site();
            window.onload = site.run();
        </script>
    </body>
</html>