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
    
    /**
     * @var $pathToResult String URL to file with result of scraping
     */
    private $pathToResult;
    
    /**
     * Constructor function
     */
    public function __construct() {
        $this -> pathToResult = \settings\AppSettings::PATH_TO_RESULT_FILE;
        // TODO: Load time from result file
        $this -> timeForLastScraping = time();
    }
    
    /**
     * @return String URL to file with result of scraping
     */
    public function getPathToResult() {
        return $this -> pathToResult;
    }
    
    /**
     * @return int Timestamp for last scraping
     */
    public function getTimeLastScraping() {
        return $this -> timeForLastScraping;
    }
}