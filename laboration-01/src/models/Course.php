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
     * @var $latestPost \models\Post Latest blog post
     */
    private $latestPost;

    /**
     * @param $name String
     * @param $url String
     * @param $code String
     * @param $description String
     * @param $postTitle String
     * @param $postWriter String
     * @param $postTime String
     */
    public function __construct($name, $url, $code, $description, $postTitle, $postWriter, $postTime) {
        $this -> name = $name;
        $this -> url = $url;
        $this -> code = $code;
        $this -> description = $description;
        $this -> latestPost = new Post($postTitle, $postWriter, $postTime);
    }
    
    /**
     * @return String $this->name
     */
    public function getName() {
        return $this -> name;
    }
    
    /**
     * @return String $this->url
     */
    public function getUrl() {
        return $this -> url;
    }
    
    /**
     * Return an array with all properties and their values
     * 
     * @return array
     */
    public function toArray() {
        return array(
            "name" => $this -> name,
            "url" => $this -> url,
            "code" => $this -> code,
            "description" => $this -> description,
            "latestPost" => $this -> latestPost -> toArray()
        );
    }

    /**
     * Compare two course objects. Used to sort courses by name property. Case insentitive
     * 
     * @param $a \models\Course
     * @param $b \models\Course
     * @return int <0 if $a is smaller than $b, >0 if $a is greater than $b, 0 if $a and $b are equal
     */
    public static function compareByName(\models\Course $a, \models\Course $b) {
        return strcasecmp($a -> getName(), $b -> getName());
    }
}