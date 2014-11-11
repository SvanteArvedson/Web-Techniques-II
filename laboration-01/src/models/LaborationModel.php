<?php

namespace models;

class LaborationModel {
    
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
        return $this -> scrapeResult;
    }
}