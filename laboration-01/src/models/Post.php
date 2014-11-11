<?php

namespace models;

/**
 * Represents a blog post
 * 
 * @author Svante Arvedson
 */
class Post {
    
    /**
     * @var $title String
     */
    private $title;
    
    /**
     * @var $writer String
     */
    private $writer;
    
    /**
     * @var $timeForPost String Time in format YY-MM-DD HH:mm
     */
    private $timeForPost;
    
    /**
     * @param $title String
     * @param $writer String
     * @param $timeForPost String Time in format YY-MM-DD HH:mm
     */
    public function __construct($title, $writer, $timeForPost) {
        $this -> title = $title;
        $this -> writer = $writer;
        $this -> timeForPost = $timeForPost;
    }
    
    /**
     * Return an array with all properties and their values
     * 
     * @return array
     */
    public function toArray() {
        return array(
            "title" => $this -> title,
            "writer" => $this -> writer,
            "timeForPost" => $this -> timeForPost
        );
    }
}