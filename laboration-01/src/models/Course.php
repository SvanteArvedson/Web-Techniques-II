<?php

class Course {

    private $name;
    private $url;
    private $code;
    private $description;
    private $timeFotLatestPost;

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
