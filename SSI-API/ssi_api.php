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
					$email = isset($_POST['email']) ? $DB->real_escape_string($_POST['email']) : NULL;
					$fbid = isset($_POST['fbid']) ? $DB->real_escape_string($_POST['fbid']) : NULL;

					switch ($action) {
						case 'get_info':
							if (!$operation->user_exists($email, $fbid))
									throw new \Exception("NOT_FOUND");
							else {
								$data = json_encode($operation->get_user_details($email));
								die($data);
								exit;
							}
						break;

						case 'get_events':

							if (!$operation->user_exists($email, $fbid))
									throw new \Exception("NOT_FOUND");
							else {
								$data = json_encode($operation->get_user_events($email));
								die($data);
								exit;
							}
						break;

						case 'insert_event':
						$data = [
							'password' 	=> isset($_POST['password']) ? $DB->real_escape_string($_POST['password']) : NULL,
							'email'	 	=> isset($_POST['email'])  ? $DB->real_escape_string($_POST['email'])  : NULL,
							'date' 			=> isset($_POST['date'])  ? $DB->real_escape_string($_POST['date'])  : NULL,
							'text' 			=> isset($_POST['text'])  ? $DB->real_escape_string($_POST['text'])  : NULL,
							'image'			=> isset($_POST['image'])  ? $DB->real_escape_string($_POST['image'])  : NULL,
							'settings'	=> isset($_POST['settings'])  ? $DB->real_escape_string($_POST['settings'])  : NULL
						];

							if ($operation->user_exists($data['email'], $data['fbid']) && $operation->check_password($data['email'], $data['password']))
								insert_event($data['email'], $data['date'], $data['text'], $data['image'], $data['settings']);
							else
								throw new \Exception ("INCORRECT_CREDENTIALS");
						break;

						case 'insert':
							$data = [
								'fbid'  		=> isset($_POST['fbid'])     ? $DB->real_escape_string($_POST['fbid'])     : NULL,
								'email' 		=> isset($_POST['email'])    ? $DB->real_escape_string($_POST['email'])    : NULL,
								'name'  		=> isset($_POST['name'])     ? $DB->real_escape_string($_POST['name'])     : NULL,
								'image' 		=> isset($_POST['image'])    ? $DB->real_escape_string($_POST['image'])    : NULL,
								'password'  => isset($_POST['password']) ? $DB->real_escape_string($_POST['password']) : NULL
							];

							if (!$operation->user_exists($data['email'], $data['fbid'])) {
							  if (!filter_var($data['email'], FILTER_VALIDATE_EMAIL))
									throw new \Exception("INVALID_EMAIL");
								else if (is_null($data['name']) || strlen($data['name']) < 3)
									throw new \Exception("INVALID_NAME");

								if (!is_null($data['name']) && filter_var($data['email'], FILTER_VALIDATE_EMAIL))
									$operation->insert_user($data['fbid'], $data['email'], $data['name'], $data['image'], $data['password']);
							} else if ($operation->user_exists($data['email'], $data['fbid']) == 2) {
									if (is_null($data['fbid']))
										throw new \Exception("INVALID_FBID");
									else if (!filter_var($data['email'], FILTER_VALIDATE_EMAIL))
										throw new \Exception("INVALID_EMAIL");
									else if (is_null($data['name']) || strlen($data['name']) < 3)
										throw new \Exception("INVALID_NAME");

									if (!is_null($data['fbid']) && !is_null($data['name']) && filter_var($data['email'], FILTER_VALIDATE_EMAIL))
										$operation->update_user("password", $data['password'], $data['email']);
							} else {
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
		case "INVALID_NAME":
			die("ERR_106"); // Invalid Name
			exit;
		break;

		case "INVALID_FBID":
			die("ERR_105"); // Invalid Facebook ID
			exit;
		break;

		case "INCORRECT_CREDENTIALS":
			die("ERR_104"); // INCORRECT USER/PASSWORD
		break;

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
