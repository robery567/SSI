<?php

namespace SSI\Lib\Main;

Class Actiune {
  public function __construct($DB) {
  		$this->db = $DB;
  }

  private function get_id ($email = NULL) {
    $DB = $this->db;
    $data = $DB->query("SELECT * FROM users WHERE email = '{$email}'")->fetch_array(MYSQLI_ASSOC);
    return $data['user_id'];
  }

  public function user_exists($email = NULL, $fbid = NULL) {
    $DB = $this->db;
    return ($DB->query("SELECT * FROM users WHERE email='{$email}'")->num_rows) ? (($DB->query("SELECT * FROM users WHERE fbid='{$fbid}'")->num_rows) ? 2 : 1) : 0;
  }

  public function check_database($database = NULL) {
    $DB = $this->db;
    return $DB->query("SELECT COUNT(DISTINCT `table_name`) FROM `information_schema`.`columns` WHERE `table_schema` = '{$database}'")->num_rows;
 }

  public function get_user_details($email = NULL) {
    $DB = $this->db;
    return $DB->query("SELECT * FROM users WHERE email='{$email}'")->fetch_array(MYSQLI_ASSOC);
  }

  public function get_user_events($email = NULL) {
    $DB = $this->db;
    return $DB->query("SELECT * FROM events WHERE email='{$email}'")->fetch_array(MYSQLI_ASSOC);
	
  }
  public function get_user_event($email = NULL , $date = NULL) {
    $DB = $this->db;
	$user_id = $this->get_id($email);
    return $DB->query("SELECT * FROM events WHERE user_id='{$user_id}' AND date='{$date}'")->fetch_array(MYSQLI_ASSOC);
  }
  public function insert_user($fbid = NULL, $email = NULL, $name = NULL, $image = NULL, $password = NULL) {
    $DB = $this->db;
    return $DB->query("INSERT INTO users (fbid, email, password, name, image) VALUES ('{$fbid}', '{$email}', '{$password}', '{$name}', '{$image}')");
  }

  public function insert_event($email = NULL, $date = NULL, $text = NULL, $image = NULL, $settings = NULL) {
    $DB = $this->db;
    $user_id = $this->get_id($email);
	$data = {"{$text}", "{$settings}", "{$image}"};
    return $DB->query("INSERT INTO events (user_id, date, data) VALUES ('{$user_id}', '{$date}', '{$data}')");
  }

  public function check_password($email = NULL, $password = NULL) {
    $DB = $this->db;
    return $DB->query("SELECT * FROM users WHERE email='{$email}' AND password ='{$password}'")->num_rows;
  }

  public function update_user($what = "password", $password, $email, $new_mail = NULL) {
    $DB = $this->db;
    switch ($what) {
        case "password":
          return $DB->query("UPDATE users SET password = '{$password}' WHERE email = '{$email}'");
        break;

        case "email":
          return $DB->query("UPDATE users SET email = '{$new_mail}' WHERE email = '{$email}'");
        break;

        default:
          return 0;
    }
  }
}
