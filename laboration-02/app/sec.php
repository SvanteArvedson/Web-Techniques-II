<?php

/**
Just some simple scripts for session handling
*/
function sec_session_start() {
    $session_name = 'sec_session_id'; // Set a custom session name
    $secure = false; // Set to true if using https.
    ini_set('session.use_only_cookies', 1); // Forces sessions to only use cookies.
    $cookieParams = session_get_cookie_params(); // Gets current cookies params.
    session_set_cookie_params(3600, $cookieParams["path"], $cookieParams["domain"], $secure, false);
    $httponly = true; // This stops javascript being able to access the session id.
    session_name($session_name); // Sets the session name to the one set above.
    session_start(); // Start the php session
    session_regenerate_id(); // regenerated the session, delete the old one.
}

function sec_session_end() {
    if(!session_id()) {
        sec_session_start();
    }
    
    setcookie('sec_session_id', '', time() - 10000);
    unset($_SESSION["username"]);
    unset($_SESSION["login_string"]);
    unset($_SESSION["CSRFtoken"]);
}

function checkUser($CSRFtoken = null) {
	if(!session_id()) {
		sec_session_start();
	}

	if(!isset($_SESSION["username"])) {header('HTTP/1.1 401 Unauthorized'); die("401 Unauthorized");}

	$user = getUser($_SESSION["username"]);
	$un = $user[0]["username"];

	if(isset($_SESSION['login_string'])) {
		if($_SESSION['login_string'] !== hash('sha512', "123456" + $un) ) {
		    header('HTTP/1.1 401 Unauthorized'); die("401 Unauthorized");
		}
        if($CSRFtoken !== null) {
            if($CSRFtoken !== $_SESSION['CSRFtoken']) {
                header('HTTP/1.1 401 Unauthorized'); die("401 Unauthorized");
            }
        }
	}
	else {
		header('HTTP/1.1 401 Unauthorized'); die("401 Unauthorized");
	}
    
    session_write_close();
    
	return true;
}

function isUser($u, $p) {
    try {
        $users = getUser($u);
        
        if ($users) {
            $user = $users[0];
            $cryptP = crypt($p, $user['password']);
    
            if ($cryptP === $user['password']) {
                return true;
            } else {
                return false;
            }
        } else {
            return false;
        }
	}
	catch(PDOException $e) {
		echo("Ett serverfel inträffade.");
		return false;
	}
}

function getUser($user) {
    $db = null;
    
	try {
		$db = new PDO("sqlite:data/db.db");
		$db->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
	}
	catch(PDOEception $e) {
		die("Ett serverfel inträffade.");
	}
	
    try {
    	$q = "SELECT * FROM users WHERE username = ?";
        $param = array($user);
    
		$stm = $db->prepare($q);
		$stm->execute($param);
        
		$result = $stm->fetchAll();
        
        $db = null;
        
        return $result;
	}
	catch(PDOException $e) {
	    $db = null;
		echo("Ett serverfel inträffade.");
		return false;
	}

	
}

function createSalt(){
    return "_" . substr(md5(rand()), 0, 8);
}

function createCSRFtoken() {
    return base64_encode(substr(md5(rand()), 0, 50));
}