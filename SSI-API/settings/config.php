<?php

define ('development_mode', true);
define ('mongodb_store', true); // If enabled the images will be stored in a mongodb table

$mysql = [
    'driver' => 'mysqli',  // Available types: mysqli
    'hostname' => 'localhost',
    'username' => 'root',
    'password' => '',
    'database' => 'ssipdb'
];

?>
