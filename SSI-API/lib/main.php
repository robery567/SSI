<?php

namespace SSI\Lib\Main;

Class Actiune {
  public function __construct($DB, $mongo_db = false) {
  		$this->db = $DB;
      $this->mongo_db = $mongo_db;
  }

  private function get_id ($email = NULL) {

    $data = $this->db->query("SELECT * FROM users WHERE email = '{$email}'")->fetch_array(MYSQLI_ASSOC);
    return $data['user_id'];
  }

  public function user_exists($email = NULL, $fbid = NULL) {

    return ($this->db->query("SELECT * FROM users WHERE email='{$email}'")->num_rows) ? (($this->db->query("SELECT * FROM users WHERE fbid='{$fbid}'")->num_rows) ? 2 : 1) : 0;
  }

  public function check_database($database = NULL) {

    return $this->db->query("SELECT COUNT(DISTINCT `table_name`) FROM `information_schema`.`columns` WHERE `table_schema` = '{$database}'")->num_rows;
 }

  public function get_user_details($email = NULL) {

    return $this->db->query("SELECT * FROM users WHERE email='{$email}'")->fetch_array(MYSQLI_ASSOC);
  }

  public function get_user_events($email = NULL, $date = NULL, $mongo = false) {
  	  $user_id = (is_null($email)) ? 0 : $this->get_id($email);
      return (is_null($date)) ? $this->db->query("SELECT * FROM events WHERE user_id='{$user_id}'")->fetch_array(MYSQLI_ASSOC) : $this->db->query("SELECT * FROM events WHERE user_id='{$user_id}' AND date='{$date}'")->fetch_array(MYSQLI_ASSOC);
  }

  public function get_user_event($email = NULL , $date = NULL, $mongo = true) {
    $user_id = $this->get_id($email);
    if ($mongo && class_exists('MongoClient') && !is_null($date)) {
      $collection_name  = $user_id;
      $collection_name .= "_event";
      $collection_name .= $date;

      $collection = $this->mongo_db->$collection_name;

      $cursor = $collection->find();
      if($this->event_exists($email,$date))
	  {
      $info = $this->db->query("SELECT * FROM events WHERE user_id = '{$user_id}' AND date='{$date}'")->fetch_array(MYSQLI_ASSOC);
	  	  foreach ($cursor as $document)
	      $info['data']=stripslashes($document['data']);
	  return $info;
	  /*$data['num'] = $info['num'];
      $data['user_id'] = $user_id;
      $data['date'] = $date;
	  foreach ($cursor as $document)
	      $data['data']=$document['data'];

      return json_encode($data);*/
	  }
	  else
		  return null;
    } else {
     return $this->db->query("SELECT * FROM events WHERE user_id='{$user_id}' AND date='{$date}'")->fetch_array(MYSQLI_ASSOC);
   }
  }
  public function insert_user($fbid = NULL, $email = NULL, $name = NULL, $image = NULL, $password = NULL) {

    return $this->db->query("INSERT INTO users (fbid, email, password, name, image) VALUES ('{$fbid}', '{$email}', '{$password}', '{$name}', '{$image}')");
  }

  public function insert_event($email = NULL, $date = NULL, $data = NULL , $num = NULL) {

    $user_id = $this->get_id($email);

    return $this->db->query("INSERT INTO events (num,user_id, date, data) VALUES ('{$num}','{$user_id}', '{$date}', '{$data}')");
  }

  public function check_password($email = NULL, $password = NULL) {

    return $this->db->query("SELECT * FROM users WHERE email='{$email}' AND password ='{$password}'")->num_rows;
  }


  public function event_exists($email = NULL, $date = "12-03-2015") {
  	$user_id = $this->get_id($email);
  	return ($this->db->query("SELECT * FROM events WHERE user_id = '{$user_id}' AND date= '{$date}'")->num_rows) ? 1 : 0;
  }

  public function alter_event($email = NULL, $date = "12-03-2015", $data = NULL, $num = 1) {
  	$user_id = $this->get_id($email);

  	return $this->db->query("UPDATE events
  										SET
  											data = '{$data}',
  											num = '{$num}'
  												WHERE
  													user_id = '{$user_id}' AND date = '{$date}'
  							");
  }

  public function get_last_user_event_id($email = NULL) {
    $user_id = $this->get_id($email);
    $data = $this->db->query("SELECT MAX(id) AS last_id FROM events WHERE user_id = '{$user_id}'")->fetch_array("MYSQLI_ASSOC");

    return (!is_null($data['last_id'])) ? $data['last_id'] : 0;
  }

  public function get_user_id($email = NULL) {
    return $this->get_id($email);
  }

  public function update_user($what = "password", $password, $email, $new_mail = NULL) {

    switch ($what) {
        case "password":
          return $this->db->query("UPDATE users SET password = '{$password}' WHERE email = '{$email}'");
        break;

        case "email":
          return $this->db->query("UPDATE users SET email = '{$new_mail}' WHERE email = '{$email}'");
        break;

        default:
          return 0;
    }
  }
}
