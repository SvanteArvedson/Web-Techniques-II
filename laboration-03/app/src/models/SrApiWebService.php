<?php

namespace models;

class SrApiWebService {
    
    /**
     * @var $APIUrl string Url for api requests
     */
    private static $APIUrl = "http://api.sr.se/api/v2/traffic/messages?size=100&format=json";
    
    /**
     * @var $APIUrl String Path to file with request result
     */
    private static $file = "data/trafficMessages.json";

    /*
     * Private helper method for requesting new traffic messages
     */
    private function fetchNewTrafficMessages() {
        $request = curl_init();

        $options = array(CURLOPT_URL => self::$APIUrl,
                        CURLOPT_RETURNTRANSFER => true,
                        CURLOPT_USERAGENT => "Svante Arvedson, skolprojekt LNU, kurs Webbteknik II");
        curl_setopt_array($request, $options);

        $result = curl_exec($request);
        curl_close($request);

        file_put_contents(self::$file, $result);
    }

    /**
     * @return string Returns content trafficMessages file
     */
    public function getTrafficMessages() {
        // 3 minutes cache
        if (time() - filemtime(self::$file) > 180) {
            $this -> fetchNewTrafficMessages();
        }
        return file_get_contents(self::$file);
    }
}