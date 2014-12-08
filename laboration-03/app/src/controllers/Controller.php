<?php

namespace controllers;

/**
 * Controller class for application
 * 
 * @author Svante Arvedson
 */
class Controller {
    
    /**
     * Check GET-parameters and run action-method
     */
    public function doAction() {
        $view = new \views\View();

        switch ($view -> getAction()) {
            case null :
                $this -> displayPage($view);
                break;
            case \views\Action::NEW_TRAFIC_MESSAGES :
                $this -> sendNewTrafficMessages();
                break;
            default:
                die("Ogiltig URL");
                break;
        }
    }
    
    /**
     * @param $view \views\View
     */
    private function displayPage(\views\View $view) {
        $viewModel = null;
        $view -> display($viewModel);
    }

    private function sendNewTrafficMessages() {
        //TODO: Fetch string from service class
        echo file_get_contents("data/result.json");
    }
}