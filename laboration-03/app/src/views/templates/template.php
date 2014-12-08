<!DOCTYPE html>
<html lang="sv">
    <head>
        <meta charset="utf-8">
        <title>Laboration 3</title>
        <meta name="viewport" content="width=device-width; initial-scale=1.0">
        <!-- Stylesheets -->
        <link href="//maxcdn.bootstrapcdn.com/bootstrap/3.3.1/css/bootstrap.min.css" rel="stylesheet">
        <link rel="stylesheet" href="<?php echo dirname($_SERVER['PHP_SELF']) . "/src/contents/css/site.css"; ?>" />
    </head>
    <body>
        <main class="container">
            <header class="row">
                <h1 class="col-xs-12">Laboration 3</h1>
            </header>
            <section class="row">
                <div id="map-canvas" class="col-xs-6">
                    <img class="ajax-loader center-block" src="<?php echo dirname($_SERVER['PHP_SELF']) . "/src/contents/img/ajax-loader.gif"; ?>" />
                </div>
                <div class="col-xs-6">
                    <div class="row">
                        <div class="col-xs-12">
                            <ul id="trafficMessages"></ul>
                        </div>
                    </div>
                </div>
            </section>
        </main>
        
        <!-- Scripts -->
        <script src="//code.jquery.com/jquery-2.1.1.min.js"></script>
        <script src="//maxcdn.bootstrapcdn.com/bootstrap/3.3.1/js/bootstrap.min.js"></script>
        <script src="//maps.googleapis.com/maps/api/js?key=<?php echo \settings\AppSettings::GOOGLE_API_KEY; ?>"></script>
        <script src="<?php echo dirname($_SERVER['PHP_SELF']) . "/src/contents/js/site.js"; ?>"></script>
        <script type="text/javascript">
            window.onload = site.run();
        </script>
    </body>
</html>