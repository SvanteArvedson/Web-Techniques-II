<?php

require_once("sec.php");

logout();

function logout() {
    sec_session_end();
    
    header('Location: index.php');
    die();
}