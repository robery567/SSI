<?php

namespace SSI\Lib\Main;

Class Actiune {
  public function __construct($DB) {
  		$this->db = $DB;
  }

  public function user_exists($user_id = NULL) {
    $DB = $this->db;
    return $DB->query("SELECT * FROM users WHERE user_id='{$user_id}'")->num_rows;
  }

  public function check_database($database = NULL) {
    $DB = $this->db;
    return $DB->query("SELECT COUNT(DISTINCT `table_name`) FROM `information_schema`.`columns` WHERE `table_schema` = '{$database}'")->num_rows;
 }

  public function get_user_details($user_id = NULL) {
    $DB = $this->db;
    return $DB->query("SELECT * FROM users WHERE user_id='{$user_id}'")->fetch_array(MYSQLI_ASSOC);
  }

  public function get_user_events($user_id = NULL) {
    $DB = $this->db;
    return $DB->query("SELECT * FROM events WHERE user_id='{$user_id}'")->fetch_array(MYSQLI_ASSOC);
  }

  public function insert_user($fbid = NULL, $email = NULL, $name = NULL, $image = NULL, $password = NULL) {
    return $DB->query("INSERT INTO users (fbid, email, password, name, image) VALUES ('{$fbid}', '{$email}', '{$password}', '{$name}', '{$image}')")
  }
}
