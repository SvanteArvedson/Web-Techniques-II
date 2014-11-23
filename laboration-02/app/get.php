<?php

// get the specific message
function getMessages() {
	$db = null;

	try {
	    $db = new PDO("sqlite:db.db");
        $db->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
        
	    $q = "SELECT * FROM messages";
		$stm = $db->prepare($q);
		$stm->execute();
		$result = $stm->fetchAll();

        if($result) {
            return $result;
        } else {
            return false;
        }

	} catch(PDOException $e) {
		echo("Ett serverfel inträffade.");
		return false;
	}
}