<?php

namespace helpers;

/**
 * Helper class for configuration
 * 
 * @author Svante Arvedson
 */
class Configure {
    
    /**
     * Sets application to swedish locals
     */
    public function setLocals() {
        date_default_timezone_set("Europe/Stockholm");
        setlocale(LC_ALL, array("swedish", "sv_SE", "swe"));
    }
    
    /**
     * Configures PHP
     */
    public function configurePHP() {
        set_time_limit(0);
    }
}