<!DOCTYPE html>
<html lang="sv">
    <head>
        <meta charset="utf-8">
        <title>Laboration 01</title>
        <meta name="viewport" content="width=device-width; initial-scale=1.0">
    </head>
    <body>
        <div>
            <header>
                <h1>Laboration 01</h1>
            </header>
            <div class="actions">
                <nav>
                    <ul>
                        <li>
                            <a href="<?php echo $_SERVER['PHP_SELF'] . "?" . \views\Action::KEY . "=" . \views\Action::NEW_SCRAPING; ?>">
                                Gör ny skrapning
                            </a>
                        </li>
                    </ul>
                </nav>
            </div>
            
            <div class="result">
                <h2>Senaste skrapningen</h2>
                <p><?php echo date("Y-m-d H:i:s", $model -> getTimeLastScraping()); ?></p>
                <h2>Resultat</h2>
                <p>Klicka <a href="<?php echo $model->getPathToResultFile(); ?>" target="_blank">här</a> för att ladda ner resultatet</p>
            </div>
        </div>
    </body>
</html>