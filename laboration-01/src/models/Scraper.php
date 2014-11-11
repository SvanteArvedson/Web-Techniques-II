<?php

namespace models;

class Scraper {
    
    private static $domain = "https://coursepress.lnu.se";
    
    public function scrape() {
        $courses = array();
        
        $doc = $this -> getCurlRequest(self::$domain . "/kurser/");
        
        // Gets all links to courses
        $courseURLs = $this -> getCourseURLs($doc);
        
        // Call each URL and create course object
        foreach ($courseURLs as $courseURL) {
            if (strpos($courseURL, "/kurs/") != 0) {
                $courseDoc = $this -> getCurlRequest($courseURL);
                $courses[] = $this -> getCourse($courseDoc);
            }
        }
        
        return new ScrapeResult(time(), count($courses), $courses);
    }
    
    private function getCurlRequest($url) {
        $request = curl_init();

        $options = array(CURLOPT_URL => $url, 
                        CURLOPT_RETURNTRANSFER => true, 
                        CURLOPT_FOLLOWLOCATION => true, 
                        CURLOPT_AUTOREFERER => true, 
                        CURLOPT_SSL_VERIFYPEER => false,
                        CURLOPT_USERAGENT => "ba222ec, IDV449 - laboration 01");
        curl_setopt_array($request, $options);

        $result = curl_exec($request);
        curl_close($request);

        $doc = new \DomDocument();
        @$doc -> loadHTML($result);

        return $doc;
    }
    
    private function getCourse($domDoc) {
        $xpath = new \DOMXPath($domDoc);
        
        // Gets name
        $courseNameNode = $xpath -> query('//div[@id = "header-wrapper"]/h1/a');
        $courseName = trim($courseNameNode -> item(0) -> nodeValue);
        if ($this -> checkNodeList($courseNameNode)) {
            $courseName = trim($courseNameNode -> item(0) -> nodeValue);
        } else {
            $courseName = "no information";
        }

        // Gets code and url
        $courseCodeNode = $xpath -> query('//div[@id = "header-wrapper"]/ul/li[3]/a');
        if ($this -> checkNodeList($courseCodeNode)) {
            $courseCode = trim($courseCodeNode -> item(0) -> nodeValue);
            $courseURL = trim($courseCodeNode -> item(0) -> getAttribute("href"));
        } else {
            $courseCode = "no information";
            $courseURL = "no information";
        }

        // Gets description
        $courseDescNode = $xpath -> query('//article[contains(concat(" ", @class, " "), " start-page ")]');
        if ($this -> checkNodeList($courseDescNode)) {
            $courseDesc = preg_replace('/\s{2,}/', ' ', trim($courseDescNode -> item(0) -> nodeValue));
        } else {
            $courseDesc = "no information";
        }

        // Gets last blog post information
        $courseTimeLastPostHeaderNode = $xpath -> query('//article[contains(concat(" ", @class, " "), " hentry ")][1]/header/h1/a');
        if ($this -> checkNodeList($courseTimeLastPostHeaderNode)) {
            $courseLastPostTitle = $courseTimeLastPostHeaderNode -> item(0) -> nodeValue;
        } else {
            $courseLastPostTitle = "no information";
        }
        
        $courseTimeLastPostWriterNode = $xpath -> query('//article[contains(concat(" ", @class, " "), " hentry ")][1]/header/p/strong');
        if ($this -> checkNodeList($courseTimeLastPostWriterNode)) {
            $courseLastPostWriter = $courseTimeLastPostWriterNode -> item(0) -> nodeValue;
        } else {
            $courseLastPostWriter = "no information";
        }
        
        $courseTimeLastPostNode = $xpath -> query('//article[contains(concat(" ", @class, " "), " hentry ")][1]/header/p');
        if ($this -> checkNodeList($courseTimeLastPostNode)) {
            $nodeVal = $courseTimeLastPostNode -> item(0) -> nodeValue;
            preg_match('/\d{4}-\d{2}-\d{2}\s{1}\d{2}:\d{2}/', $nodeVal, $match);
            $courseTimeLastPost = $match[0];
        } else {
            $courseTimeLastPost = "no information";
        }
        
        return new Course($courseName, $courseURL, $courseCode, $courseDesc, $courseLastPostTitle, $courseLastPostWriter, $courseTimeLastPost);
    }

    private function checkNodeList($nodeList) {
        return ($nodeList != false && $nodeList -> length != 0) ? true : false; 
    }
    
    private function getCourseURLs($domDoc) {
        $ret = array();
        
        $xpath = new \DOMXPath($domDoc);
        $courseItems = $xpath -> query('//ul[@id = "blogs-list"]//div[@class = "item-title"]/a');
        
        foreach ($courseItems as $item) {
            $ret[] = $item -> getAttribute("href");
        }
        
        // Check if there is more pages paginated
        $isMorePages = $xpath -> query('//div[@class = "pagination"]//div[@class = "pagination-links"]//a[@class = "next page-numbers"]');
        if ($isMorePages -> length != 0) {
            $linksNextPages = $xpath -> query('//div[@class = "pagination"]//div[@class = "pagination-links"]//a[@class = "next page-numbers"]');
            $nextHTMLPage = $this ->getCurlRequest(self::$domain . $linksNextPages -> item(0) -> getAttribute("href"));
            $ret = array_merge($ret, $this ->getCourseURLs($nextHTMLPage));
        }
        
        return $ret;
    }
}