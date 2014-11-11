<?php

namespace models;

class LaborationModel {
    
    /**
     * @var $cacheLength int Length of cache in seconds 86400 = 24 hours
     */
    private static $cacheLength = 86400;
    
    private $scraper;
    private $repository;
    private $scrapeResult;
    
    public function __construct() {
        $this -> scraper = new Scraper();
        $this -> repository = new Repository();
        $this -> scrapeResult = $this -> repository -> getScrapeResult();
        if ($this -> scrapeResult == null) {
            $this -> doScraping();
        }
    }
    
    public function doScraping() {
        $scrapeResult = $this -> scraper -> scrape();
        $this -> repository -> saveScrapeResult($scrapeResult);
        $this -> scrapeResult = $scrapeResult;
    }
    
    public function getScrapeResult() {
        // caching for one hour
        if ($this -> scrapeResult -> getTimeLastScraping() < time() - self::$cacheLength) {
            $this -> doScraping();
        }

        return $this -> scrapeResult;
    }
}