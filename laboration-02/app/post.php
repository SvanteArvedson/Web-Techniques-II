<?php

require_once("sec.php");

$name = $_GET["name"] != null ? strip_tags($_GET["name"]) : "";
$message = $_GET["message"] != null ? strip_tags($_GET["message"]) : "";

if ($name != "" && $message != "" && checkUser()) {
    addToDB($message, $name);
}

/**
* Called from AJAX to add stuff to DB
*/
function addToDB($message, $user) {
	$db = null;

	try {
		$db = new PDO("sqlite:data/db.db");
		$db->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
        $q = "INSERT INTO messages (message, name, timestamp) VALUES(?, ?, ?)";
        $param = array($message, $user, time());
        $stm = $db->prepare($q);
        $stm->execute($param);
        $db = null;
	}
	catch(PDOEception $e) {
		$db = null;
		die("Ett serverfel inträffade.");
	}
}