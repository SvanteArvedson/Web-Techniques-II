<?php

namespace models;

/**
 * Facade class for namespace models
 * 
 * @author Svante Arvedson
 */
class LaborationModel {
    
    /**
     * @var $cacheLength int Length of cache in seconds 86400 = 24 hours
     */
    private static $cacheLength = 86400;
    
    /**
     * @var $scraper /models/Scraper
     */
    private $scraper;
    
    /**
     * @var $repository /models/Repository
     */
    private $repository;
    
    /**
     * Constructor function
     */
    public function __construct() {
        $this -> scraper = new Scraper();
        $this -> repository = new Repository();
    }
    
    /**
     * Performe a scraping and saves the result
     */
    public function doScraping() {
        $scrapeResult = $this -> scraper -> scrape();
        $this -> repository -> saveScrapeResult($scrapeResult);
    }
    
    /**
     * Returns a ScrapeResult, performs a new scraping if needed
     * 
     * @return /models/ScrapeResult
     */
    public function getScrapeResult() {
        $scrapeResult = $this -> repository -> getScrapeResult();

        if ($scrapeResult == null || $scrapeResult -> getTimeLastScraping() < time() - self::$cacheLength) {
            $this -> doScraping();
            $scrapeResult = $this -> repository -> getScrapeResult();
        }
        
        return $scrapeResult;
    }
}