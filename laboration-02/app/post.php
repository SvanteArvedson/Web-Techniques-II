<?php

/**
* Called from AJAX to add stuff to DB
*/
function addToDB($message, $user) {
	$db = null;

	try {
		$db = new PDO("sqlite:db.db");
		$db->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
	}
	catch(PDOEception $e) {
		die("Ett serverfel inträffade.");
	}
	
    try {
    	$q = "INSERT INTO messages (message, name) VALUES(?, ?)";
    	$param = array($message, $user);
        
        $stm = $db->prepare($q);
        $stm->execute($param);
	}
	catch(PDOException $e) {}
	
    try {
    	$q = "SELECT * FROM users WHERE username = ?";
        $param = array($user);
		
		$stm = $db->prepare($q);
		$stm->execute($param);
        
		$result = $stm->fetchAll();
		if(!$result) {
			return "Could not find the user";
		}
	}
	catch(PDOException $e) {
		echo("Ett serverfel inträffade.");
		return false;
	}
	// Send the message back to the client
	echo "Message saved by user: " .json_encode($result);
}

