<?php

/**
 * Class for autoloading all php files
 * @author Svante Arvedson 
 */
class Autoload {

    /**
     * Requires all classes in src-folder
     */
    public function loadClasses() {
        $folder = dirname(__FILE__) . "/../";
        $paths = $this -> getDirectories($folder);
        $this -> load($paths);
    }
    
    /**
     * Scans src-folder for php files
     * 
     * Algorithm created with inspiration from comments on
     * http://php.net/manual/en/function.scandir.php
     *
     * @param $folder String A path to folder to scan
     * @return array A array with paths to all classes
     */
    private function getDirectories($folder) {
        $ret = array();
        $paths = scandir($folder);
        
        foreach ($paths as $path) {
            if (!in_array($path, array(".", ".."))) {
                if (is_dir($folder . $path)) {
                    // If path goes to another directory, 
                    // the same function is called recursively
                    $ret[] = $this -> getDirectories($folder . $path);
                } else {
                    $path_parts = pathinfo($path);
                    // Only paths to php files is stored
                    if ($path_parts['extension'] === "php") {
                        $ret[] = $folder . DIRECTORY_SEPARATOR . $path;
                    }
                }
            }
        }
        
        return $ret;
    }
    
    /**
     * Do require_once on all files in $paths
     * 
     * @param $paths String An array with paths
     */
    private function load($paths) {
        foreach ($paths as $path) {
            if (is_array($path)) {
                // If path goes to another directory, 
                // the same function is called recursively
                $this -> load($path);
            } else {
                require_once($path);
            }
        }
    }
}