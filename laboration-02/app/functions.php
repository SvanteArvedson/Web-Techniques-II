<?php

require_once("post.php");
require_once("sec.php");
require_once("get.php");
sec_session_start();
//set_time_limit(0);

/*
* It's here all the ajax calls goes
*/
if(isset($_GET['function'])) {

	if($_GET['function'] == 'logout') {
		logout();
    } 
    elseif($_GET['function'] == 'add') {
        $name = $_GET["name"] != null ? strip_tags($_GET["name"]) : "";
        $message = $_GET["message"] != null ? strip_tags($_GET["message"]) : "";
        
        if ($name != "" && $message != "" && checkUser()) {
    	    addToDB($message, $name);
        }
    }
    elseif($_GET['function'] == 'getAllMessages') {
        echo(json_encode(getAllMessages()));    
    }
    elseif($_GET['function'] == 'getNewMessages') {
        
        $timestamp = $_GET['timestamp'] != null ? $_GET['timestamp'] : time();
        
        while (true) {
            $result = getNewMessages($timestamp);
            
            if ($result) {
                echo(json_encode($result));
                break;
            } else {
                usleep(1000000);
            }
        }
    }
}


