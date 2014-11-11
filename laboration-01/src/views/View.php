<?php

namespace views;

/**
 * View for the application
 * 
 * @author Svante Arvedson
 */
class View {
    
    /**
     * @return Mixed Returns content in GET parameter 'action' or NULL
     */
    public function getAction() {
        return isset($_GET['action']) ? $_GET['action'] : null;
    }
    
    /**
     * Redirects request fo front page
     */
    public function redirectToFront() {
        header("Location: " . $_SERVER['PHP_SELF']);
        die();
    }
    
    /**
     * Displays the HTML page
     * 
     * @param \models\ViewModel $model A model class with information to be displayed
     */
    public function display(\models\ViewModel $model) {
        include dirname(__FILE__) . "/templates/template.php";
    }
}