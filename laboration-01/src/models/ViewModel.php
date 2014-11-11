<?php

namespace models;

/**
 * Sended to view with information to display
 * 
 * @author Svante Arvedson
 */
class ViewModel {
    
    /**
     * @var $timeForLastScraping int Timestamp for last scraping
     */
    private $timeForLastScraping;
    
    /**
     * @var Path to JSON file with ScrapeResult
     */
    private $pathToResultFile;

    /**
     * @var $scrapedUrls array An array with all scraped Urls
     */
    private $scrapedUrls;

    /**
     * @param $scrapeResult \models\ScrapeResult
     */
    public function __construct(\models\ScrapeResult $scrapeResult) {
        $this -> timeForLastScraping = $scrapeResult -> getTimeLastScraping();
        $this -> pathToResultFile = dirname($_SERVER['PHP_SELF']) . "/courses.json";
        $this -> scrapedUrls = $scrapeResult -> getScrapedUrls();
    }

    /**
     * @return String $this->pathToResultFile
     */
    public function getPathToResultFile() {
        return $this -> pathToResultFile;
    }
    
    /**
     * @return int Timestamp for last scraping
     */
    public function getTimeLastScraping() {
        return $this -> timeForLastScraping;
    }

    /**
     * @return array $this -> scrapedUrls
     */
    public function getScrapedUrls() {
        return $this -> scrapedUrls;
    }
}