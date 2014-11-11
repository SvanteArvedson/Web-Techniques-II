<!DOCTYPE html>
<html lang="sv">
    <head>
        <meta charset="utf-8">
        <title>Laboration 1</title>
        <meta name="viewport" content="width=device-width; initial-scale=1.0">
        <link rel="stylesheet" href="<?php echo dirname($_SERVER['PHP_SELF']) . "/contents/css/site.css"; ?>" />
    </head>
    <body>
        <main>
            <header>
                <h1>Laboration 1</h1>
            </header>
            <aside>
                <a href="<?php echo $_SERVER['PHP_SELF'] . "?" . \views\Action::KEY . "=" . \views\Action::NEW_SCRAPING; ?>">Gör ny skrapning</a>
            </aside>
            <section>
                <h2>Senaste skrapningen</h2>
                <p><?php echo date("Y-m-d H:i:s", $model -> getTimeLastScraping()); ?></p>
                <h2>JSON-fil</h2>
                <p><a href="<?php echo $model->getPathToResultFile(); ?>" target="_blank"><?php echo $model->getPathToResultFile(); ?></a></p>
                <h2>Skrapade kurser</h2>
                <ul>
                <?php foreach ($model -> getScrapedUrls() as $scrapedUrl): ?>
                     <li><a href="<?php echo $scrapedUrl; ?>"><?php echo $scrapedUrl; ?></a></li>
                <?php endforeach; ?>
                </ul>
            </section>
        </main>
    </body>
</html>