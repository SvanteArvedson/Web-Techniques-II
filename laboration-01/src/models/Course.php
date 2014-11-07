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
    private $timeFotLatestPost;

    /**
     * Constructor function
     *
     * @param $name String
     * @param $url String
     * @param $code String
     * @param $description String
     * @param $timeFotLatestPost Mixed
     */
    public function __construct($name = 'no information', $url = 'no information', 
                                $code = 'no information', $description = 'no information', 
                                $timeFotLatestPost = 'no information') {
        $this -> name = $name;
        $this -> url = $url;
        $this -> code = $code;
        $this -> description = $description;
        $this -> timeFotLatestPost = $timeFotLatestPost;
    }
}