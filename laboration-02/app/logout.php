<?php

require_once("sec.php");

if (checkUser()) {
    logout();
}

function logout() {
    sec_session_end();
    header('Location: index.php');
}