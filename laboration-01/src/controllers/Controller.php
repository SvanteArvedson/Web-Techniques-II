<?php

namespace controllers;

/**
 * Controller class for application
 * 
 * @author Svante Arvedson
 */
class Controller {
    
    /**
     * Checks GET-parameters and runs suitable action-method
     */
    public function doAction() {
        $view = new \views\View();
        
        switch ($view -> getAction()) {
            case null :
                $this -> displayPage($view);
                break;
            case \views\Action::NEW_SCRAPING :
                die("Ny skrapning");
                break;
            default:
                die("Ogiltig URL");
                break;
        }
    }
    
    /**
     * Checks if a new scraping should be done
     * Displays information about last scraping
     */
    private function displayPage(\views\View $view) {
        // TODO: Check cache
        $model = new \models\LaborationModel();
        $model -> doScraping();
        
        $viewModel = new \models\ViewModel();
        $view -> display($viewModel);
    }
}