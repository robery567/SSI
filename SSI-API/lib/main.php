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


}
