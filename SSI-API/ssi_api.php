<?PHP
/**
*
* @package API SSI
* @version 1.00
* @copyright Colca Robert
* @license http://opensource.org/licenses/Apache-2.0 Apache License, Version 2.0
*
*/
namespace SSI\DB\Connection;

include 'lib/main.php';
include 'settings/config.php';

use SSI\Lib\Main\Actiune as Actiune;

development_mode ? error_reporting(E_ALL) : error_reporting(0);

try {
	switch ($mysql['connection_type']) {
		case 'mysqli':
			$DB = new \MySQLi ( $mysql ['hostname'], $mysql ['username'], $mysql ['password'], $mysql ['database'] );

			if ($DB->connect_error) {
				throw new \Exception("PROBLEMA_CONEXIUNE");
			} else {
				$operation = new Actiune( $DB );

				if(!$operation->check_database($mysql['database'])) {
					throw new \Exception("DATABASE_CORUPT");
				} else {
					$action  = isset($_GET['action'])  ? $DB->real_escape_string($_GET['action']) : NULL;
					$user_id = isset($_GET['user_id']) ? $DB->real_escape_string($_GET['user_id']) : NULL;


					switch ($action) {
						case 'get_info':
							if (!$operation->user_exists($user_id))
									throw new \Exception("NOT_FOUND");
							else {
								$data = json_encode($operation->get_user_details($user_id));
								die($data);
								exit;
							}
						break;

						case 'get_events':
							if (!$operation->user_exists($user_id))
									throw new \Exception("NOT_FOUND");
							else {
								$data = json_encode($operation->get_user_events($user_id));
								die($data);
								exit;
							}
						break;

						case 'insert_event':
							$password = isset($_GET['password']) ? $DB->real_escape_string($_GET['password']) : NULL;


						break;

						case 'insert':
							if (!$operation->user_exists($user_id)) {
								$data = [
									'fbid'  => isset($_GET['fbid']) ? $DB->real_escape_string($_GET['fb_id']) : NULL,
									'email' => isset($_GET['email']) ? $DB->real_escape_string($_GET['email']) : NULL,
									'name'  => isset($_GET['name']) ? $DB->real_escape_string($_GET['name']) : NULL,
									'image' => isset($_GET['image']) ? $DB->real_escape_string($_GET['image']) : NULL
								];

								if (is_null($data['fbid']))
									throw new \Exception("INVALID_FBID");
								else if (!filter_var($data['email'], FILTER_VALIDATE_EMAIL))
									throw new \Exception("INVALID_EMAIL");
								else if (is_null($data['name']) || strlen($data['name']) < 3)
									throw new \Exception("INVALID_NAME");

								if (!is_null($data['fbid']) && !is_null($data['name']) && filter_var($data['email'], FILTER_VALIDATE_EMAIL))
									$operation->insert_user($data['fbid'], $data['email'], $data['name'], $data['image'], $data['password']);
							}	else {
								throw new \Exception("USER_EXISTS");
							}
						break;

						default:
							throw new \Exception("INVALID_ACTION");
					}
				}
			}
		break;
	}
} catch (\Exception $e) {
	switch($e->getMessage()) {
		case "INVALID_EMAIL":
			die("ERR_103"); // Invalid Email
			exit;
		break;

		case "USER_EXISTS":
			die("ERR_102"); // User already exists
			exit;
		break;

		case "NOT_FOUND":
			die("ERR_404"); // User not found
			exit;
		break;

		case "INVALID_ACTION":
			die("ERR_101"); // Invalid Action
			exit;
		break;

		default:
			die("ERR_500"); // Internal Server Error
			exit;
	}
}

?>
