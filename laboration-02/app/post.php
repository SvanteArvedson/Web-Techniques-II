<?php

/**
* Called from AJAX to add stuff to DB
*/
function addToDB($message, $user) {
	$db = null;

	try {
		$db = new PDO("sqlite:db.db");
		$db->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
        $q = "INSERT INTO messages (message, name) VALUES(?, ?)";
        $param = array($message, $user);
        $stm = $db->prepare($q);
        $stm->execute($param);
	}
	catch(PDOEception $e) {
		die("Ett serverfel inträffade.");
	}
}

