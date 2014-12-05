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
}