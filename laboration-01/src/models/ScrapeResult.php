<?php

namespace models;

class ScrapeResult {
	
    private $timeLastScraping;
    private $numberOfCourses;
    private $courses;
    
	public function __construct($timeLastScraping, $numberOfCourses, array $courses) {
	    $this -> timeLastScraping = $timeLastScraping;
        $this -> numberOfCourses = $numberOfCourses;
        $this -> courses = $courses;
	}
    
    public function getTimeLastScraping() {
        return $this -> timeLastScraping;
    }
    
    public function toJSON() {
        $courses = array();
        foreach ($this -> courses as $course) {
            $courses[] = $course -> toArray();
        }
        
        $array = array (
            "timeLastScraping" => $this -> timeLastScraping,
            "numberOfCourses" => $this -> numberOfCourses,
            "courses" => $courses
        );
        
        return json_encode($array);
    }
}
