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
            case \views\Action::NEW_SCRAPING :
                $this -> doNewScraping($view);
                break;
            default:
                die("Ogiltig URL");
                break;
        }
    }
    
    /**
     * Displays information about last scraping
     * 
     * @param $view \views\View
     */
    private function displayPage(\views\View $view) {
        $model = new \models\LaborationModel();

        $viewModel = new \models\ViewModel($model -> getScrapeResult());
        $view -> display($viewModel);
    }
    
    /**
     * Performes a new scraping
     * 
     * @param $view \views\View
     */
    private function doNewScraping(\views\View $view) {
        $model = new \models\LaborationModel();
        $model -> doScraping();
        
        $view -> redirectToFront();
    }
}