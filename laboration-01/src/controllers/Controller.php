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
                $this -> doNewScraping($view);
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

        $viewModel = new \models\ViewModel($model -> getScrapeResult());
        $view -> display($viewModel);
    }
    
    private function doNewScraping(\views\View $view) {
        $model = new \models\LaborationModel();
        $model -> doScraping();
        
        $view -> redirectToFront();
    }
}