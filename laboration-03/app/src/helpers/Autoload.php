<?php

namespace helpers;

/**
 * Class for autoloading all php classes
 * 
 * @author Svante Arvedson 
 */
class Autoload {

    /**
     * Loads all classes in src-folder
     */
    public function loadClasses() {
        $folder = dirname(__FILE__) . "/../";
        $paths = $this -> getDirectories($folder);
        $this -> load($paths);
    }
    
    /**
     * Private helper function scans src folder for php files
     *
     * @param $folder String A path to folder to scan
     * @return array A array with paths to all files
     */
    private function getDirectories($folder) {
        $ret = array();
        $paths = scandir($folder);
        
        foreach ($paths as $path) {
            if (!in_array($path, array(".", ".."))) {
                if (is_dir($folder . $path)) {
                    // If path goes to another directory
                    $ret[] = $this -> getDirectories($folder . $path);
                } else {
                    $path_parts = pathinfo($path);
                    // Only paths to php files
                    if (@$path_parts['extension'] === "php") {
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
                // If path goes to another directory
                $this -> load($path);
            } else {
                require_once($path);
            }
        }
    }
}