<?php

namespace models;

/**
 * Represents a course
 */
class Course {

    /**
     * @var $name String Name of the course
     */
    private $name;
    
    /**
     * @var $url String URL to course webpage
     */
    private $url;
    
    /**
     * @var $code String A unique idetification code
     */
    private $code;
    
    /**
     * @var $description String A short description of course content
     */
    private $description;
    
    /**
     * @var $timeFotLatestPost mixed Timestamp for latest blog post
     */
    private $timeForLatestPost;

    /**
     * Constructor function
     *
     * @param $name String
     * @param $url String
     * @param $code String
     * @param $description String
     * @param $timeForLatestPost Mixed
     */
    public function __construct($name, $url, $code, $description, $timeForLatestPost) {
        $this -> name = $name;
        $this -> url = $url;
        $this -> code = $code;
        $this -> description = $description;
        $this -> timeForLatestPost = $timeForLatestPost;
    }
    
    public function toArray() {
        return array(
            "name" => $this -> name,
            "url" => $this -> url,
            "code" => $this -> code,
            "description" => $this -> description,
            "timeForLatestPost" => $this -> timeForLatestPost
        );
    }
}