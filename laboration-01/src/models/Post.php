<?php

namespace models;

class Post {
    
    private $title;
    private $writer;
    private $timeForPost;
    
    public function __construct($title, $writer, $timeForPost) {
        $this -> title = $title;
        $this -> writer = $writer;
        $this -> timeForPost = $timeForPost;
    }
    
    public function toArray() {
        return array(
            "title" => $this -> title,
            "writer" => $this -> writer,
            "timeForPost" => $this -> timeForPost
        );
    }
}