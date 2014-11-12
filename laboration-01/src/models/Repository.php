<?php

namespace models;

/**
 * Responsible for saving and retrieving JSON file with courses
 *
 * @author Svante Arvedson
 */
class Repository {

    /**
     * @var $filePath String name of JSON file
     */
    private $filePath = "courses.json";

    /**
     * Reads file, creates and resturn a ScrapeResult object
     *
     * @return mixed A ScrapeResult object or NULL
     */
    public function getScrapeResult() {
        if (file_exists($this -> filePath)) {
            $savedResult = json_decode(file_get_contents($this -> filePath));

            $courses = array();
            foreach ($savedResult -> courses as $course) {
                $lastestPost = $course -> latestPost;
                $courses[] = new Course($course -> name, $course -> url, $course -> code, $course -> description, $lastestPost -> title, $lastestPost -> writer, $lastestPost -> timeForPost);
            }
            return new ScrapeResult($savedResult -> timeLastScraping, $savedResult -> numberOfCourses, $courses);
        }
        return null;
    }

    /**
     * Saves a ScrapeResult as a JSON file
     *
     * @param $scrapeResult \models\ScrapeResult Object to be saved
     */
    public function saveScrapeResult(\models\ScrapeResult $scrapeResult) {
        file_put_contents($this -> filePath, json_encode($scrapeResult -> toArray()));
    }
}