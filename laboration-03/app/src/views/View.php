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
     * Displays the HTML page
     */
    public function display() {
        include dirname(__FILE__) . "/templates/template.php";
    }
}