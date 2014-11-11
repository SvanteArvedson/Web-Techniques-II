<?php

namespace models;

/**
 * Sended to view with information to display
 */
class ViewModel {
    
    /**
     * @var $timeForLastScraping int Timestamp for last scraping
     */
    private $timeForLastScraping;
    
    private $pathToResultFile;

    /**
     * Constructor function
     */
    public function __construct(\models\ScrapeResult $scrapeResult) {
        // TODO: Load time from result file
        $this -> timeForLastScraping = $scrapeResult -> getTimeLastScraping();
        $this -> pathToResultFile = dirname($_SERVER['PHP_SELF']) . "/courses.json";
    }

    public function getPathToResultFile() {
        return $this -> pathToResultFile;
    }
    
    /**
     * @return int Timestamp for last scraping
     */
    public function getTimeLastScraping() {
        return $this -> timeForLastScraping;
    }
}