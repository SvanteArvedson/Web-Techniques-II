<?php

require_once dirname(__FILE__) . "/src/helpers/Autoload.php";

// Requires all classes
$autoload = new \helpers\Autoload();
$autoload -> loadClasses();

// Sets configuration to swedish locals
$configure = new \helpers\Configure();
$configure -> setLocals();

// Runs application
$controller = new \controllers\Controller();
$controller -> doAction();