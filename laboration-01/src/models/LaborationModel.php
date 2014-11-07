<?php

namespace models;

class LaborationModel {
    
    public function doScraping() {
        $startUrl = \settings\AppSettings::FIRST_URL_TO_SCRAPE;

        var_dump($this -> getCurlRequest($startUrl));
        die();
    }
    
    private function getCurlRequest($url) {
        $request = curl_init();
    
        $options = array(CURLOPT_URL => $url, CURLOPT_RETURNTRANSFER => true);
        curl_setopt_array($request, $options);
        
        $result = curl_exec($request);
        curl_close($request);
        
        return $result;
    }
}