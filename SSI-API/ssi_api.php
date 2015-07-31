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

require_once __DIR__.'/lib/main.php';
require_once __DIR__.'/settings/config.php';;

use SSI\Lib\Main\Actiune as Actiune;

development_mode ? error_reporting(E_ALL) : error_reporting(0);

try {
	// Initializing the MongoDB Connection if enabled
	if (mongodb_store && class_exists('MongoClient')) {
		$mongo = new \MongoClient();
		$mongo_db = $mongo->users;
	}

	switch ($mysql['driver']) {
		case 'mysqli':
			$DB = @new \MySQLi ( $mysql ['hostname'], $mysql ['username'], $mysql ['password'], $mysql ['database'] );

			if ($DB->connect_error) {
				throw new \Exception("PROBLEMA_CONEXIUNE");
			} else {
				$operation = new Actiune( $DB, $mongo_db );

				if(!$operation->check_database($mysql['database'])) {
					throw new \Exception("DATABASE_CORUPT");
				} else {
					$action = isset($_GET['action']) ? $DB->real_escape_string($_GET['action']) : NULL;
					$email  = isset($_GET['email'])  ? $DB->real_escape_string($_GET['email'])  : NULL;
					$fbid   = isset($_GET['fbid'])   ? $DB->real_escape_string($_GET['fbid'])   : NULL;

					/*
						@Param: action

						@Param Type : GET

						@Catchable exceptions:
							* INVALID_ACTION if the provided action is invalid

					*/
					switch ($action) {

						/*
							@Action Name: get_info (action=get_info)

							@Params:
								* email (STRING)

							@Params Type: POST

							@Output :
								* User details for a provided email address

							@Catchable exceptions:
								* NOT_FOUND if the provided email is not in the database
						*/
						case 'get_info':
							if (!$operation->user_exists($email, $fbid))
									throw new \Exception("NOT_FOUND");
							else {
								$data = json_encode($operation->get_user_details($email));
								exit($data);
							}
						break;

						case 'status_check':
						    exit("SUCCESS");
						/*
							@Action Name: get_events (action=get_events)

							@Params:
								* email (STRING)

							@Params Type: POST

							@Output :
								* User events for a provided email address

							@Catchable exceptions:
								* NOT_FOUND if the provided email is not in the database
						*/
						case 'get_events':
							if (!$operation->user_exists($email, $fbid))
									throw new \Exception("NOT_FOUND");
							else {
								$data = json_encode($operation->get_user_events($email));
								exit($data);
							}
						break;

						/*
							@Action Name: insert_event (action=insert_event)

							@Params:
								* fbid (INT) - CAN BE NULL
								* email (STRING)
								* date (DATE)
								* data (JSON)

							@Params Type: POST

							@Output :
								* true  (1) on success
								* false (0) on failure

							@Catchable exceptions:
								* INCORRECT_CREDENTIALS if the provided (fbid AND email) are invalid
						*/
						case 'insert_event':
							$data = [
								'fbid' 			=> isset($_POST['fbid'])  ? $DB->real_escape_string($_POST['fbid'])  : NULL,
								'email'	 		=> isset($_POST['email']) ? $DB->real_escape_string($_POST['email']) : NULL,
								'date' 			=> isset($_POST['date'])  ? $DB->real_escape_string($_POST['date'])  : NULL,
								'data' 			=> isset($_POST['data'])  ? $DB->real_escape_string($_POST['data'])  : NULL,
								'num'       => isset($_POST['num'])   ? $DB->real_escape_string($_POST['num'])   : NULL,
							];

							if ($operation->user_exists($data['email'], $data['fbid']) && !$operation->event_exists($data['email'], $data['date'])) {
									if (mongodb_store && class_exists('MongoClient')) {
										$collection_name  = $operation->get_user_id($data['email']);
										$collection_name .= "_event";
										$collection_name .= $operation->get_last_user_event_id($data['email'])+1;

										$collection = $mongo_db->$collection_name;

										$document = array(
																			"data"		 => $data['data']
																		 );

										$collection->insert($document);
										$collection->save($document);
									}

									echo $operation->insert_event($data['email'], $data['date'], NULL, $operation->get_last_user_event_id($data['email'])+1) ? 1 : 0;
							} else if ($operation->user_exists($data['email'], $data['fbid']) && $operation->event_exists($data['email'], $data['date']))
								echo $operation->alter_event($data['email'], $data['date'], $data['data'], $data['num']) ? 1 : 0;

							else
								throw new \Exception ("INCORRECT_CREDENTIALS");
						break;

						/*
							@Action Name: insert (action=insert)

							@Params:
								* fbid (INT)
								* email (STRING)
								* name (STRING)
								* image (STRING)
								* password (STRING)

							@Params Type: POST

							@Output :
								* true  (1) on success
								* false (0) on failure

							@Catchable exceptions:
								* INVALID_EMAIL if the provided email is invalid
								* INVALID_NAME if the provided name is invalid
								* INVALID_FBID if the provided fbid is invalid
								* USER_EXISTS if the user already exists
						*/
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

								if (!is_null($data['name']) && filter_var($data['email'], FILTER_VALIDATE_EMAIL))	{
									if (mongodb_store && class_exists('MongoClient')) {
										echo $operation->insert_user($data['fbid'], $data['email'], $data['name'], NULL, $data['password']) ? 1 : 0;
										$collection_name = $operation->get_user_id($data['email']);
										$collection_name .= "_image";
										$collection = $mongo_db->$collection_name;

										$document = array(
																			"image"		 => $data['image']
																		 );

										$collection->insert($document);
										$collection->save($document);
									} else {
											echo $operation->insert_user($data['fbid'], $data['email'], $data['name'], $data['image'], $data['password']) ? 1 : 0;
										}
								}
							} else if ($operation->user_exists($data['email'], $data['fbid']) == 2) {
									if (is_null($data['fbid']))
										throw new \Exception("INVALID_FBID");
									else if (!filter_var($data['email'], FILTER_VALIDATE_EMAIL))
										throw new \Exception("INVALID_EMAIL");
									else if (is_null($data['name']) || strlen($data['name']) < 3)
										throw new \Exception("INVALID_NAME");

									if (!is_null($data['fbid']) && !is_null($data['name']) && filter_var($data['email'], FILTER_VALIDATE_EMAIL)) {
										echo $operation->update_user("password", $data['password'], $data['email']) ? 1 : 0;
									}
							} else {
								throw new \Exception("USER_EXISTS");
							}
						break;

						/*
							@Action Name: login (action=login)

							@Params:
								* password (STRING)
								* email (STRING)

							@Params Type: POST

							@Output :
								* SUCCESS if the provided are credentials are correct

							@Catchable exceptions:
								* INCORRECT_CREDENTIALS if the provided credentials are not correct
						*/
						case 'login':
							$email = isset($_POST['email']) ? $DB->real_escape_string($_POST['email']) : NULL;
							$password = isset($_POST['password']) ? $DB->real_escape_string($_POST['password']) : NULL;

							if($operation->check_password($email, $password))
								exit("SUCCESS");
							else
								throw new \Exception("INCORRECT_CREDENTIALS");
						break;

						/*
						 TODO:
						*/
						case 'insert_info':
							$data = [
								'fbid'  		=> isset($_POST['fbid'])     ? $DB->real_escape_string($_POST['fbid'])     : NULL,
								'email' 		=> isset($_POST['email'])    ? $DB->real_escape_string($_POST['email'])    : NULL
							];

							if (!$operation->user_exists($data['email'], $data['fbid'])) {
								throw new \Exception("ERR_404");
							} else {

							}

						break;

						/*
							@Action Name: get_user_event (action=get_user_event)

							@Params:
								* date (DATE)
								* email (STRING)

							@Params Type: GET

							@Output :
								* all the user_events from the specific date

							@Catchable exceptions:
								* NOT_FOUND if the provided email doesn't exist in the database
						*/
						case 'get_user_event':
							$email	 	= isset($_GET['email'])  ? $DB->real_escape_string($_GET['email'])  : NULL;
							$date 		= isset($_GET['date'])  ? $DB->real_escape_string($_GET['date'])  : "1.1.2015";

							if (!$operation->user_exists($email))
									throw new \Exception("NOT_FOUND");
							else {
								$data = json_encode($operation->get_user_event($email,$date));
								exit($data);
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
			exit("ERR_106"); // Invalid Name
		break;

		case "INVALID_FBID":
			exit("ERR_105"); // Invalid Facebook ID
		break;

		case "INCORRECT_CREDENTIALS":
			exit("ERR_104"); // INCORRECT USER/PASSWORD
		break;

		case "INVALID_EMAIL":
			exit("ERR_103"); // Invalid Email
		break;

		case "USER_EXISTS":
			exit("ERR_102"); // User already exists
		break;

		case "NOT_FOUND":
			exit("ERR_404"); // User not found
		break;

		case "INVALID_ACTION":
			exit("ERR_101"); // Invalid Action
		break;

		default:
			exit("ERR_500"); // Internal Server Error
	}
}

?>
