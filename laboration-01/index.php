<?php

require_once dirname(__FILE__) . "/src/helpers/Autoload.php";

$autoload = new Autoload();
$autoload -> loadClasses();

var_dump("Scriptet exekverades");