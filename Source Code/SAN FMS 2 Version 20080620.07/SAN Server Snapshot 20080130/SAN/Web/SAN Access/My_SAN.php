<?php 
require_once('C:\Inetpub\wwwroot\SAN\Web\SAN Access\Includes\Layout\main_page_top.php');
require_once('C:\Inetpub\wwwroot\SAN\Web\SAN Access\Includes\Variables\global_variables.php');
require_once('C:\Inetpub\wwwroot\SAN\Web\SAN Access\Includes\Functions\functions.php');
require_once('C:\Inetpub\wwwroot\SAN\Web\SAN Access\Includes\Functions\database_calls.php');

echo "<h1>My SAN</h1>\n";

session_start();


//If Session variables are already active, cheat and set the _post variables to the session values
if (isset($_SESSION['studentnumber']) == true)
{
	$_POST["username"] = $_SESSION['studentnumber'];
}
if (isset($_SESSION['password']) == true)
{
	$_POST["password"] = $_SESSION['password'];
}
if (isset($_SESSION['context']) == true)
{
	$_POST["context"] = $_SESSION['context'];
}

//Just in case we still don't details for these two crucial variables
if ($_POST["password"] == "") {
	$_POST["password"] = "default";
}
	
if ($_POST["username"] == "") {
	$_POST["username"] = "default";
}

$_POST["username"] = strtoupper($_POST["username"]);

//Run authentication code
$ldap_server = $global_ldap_server;

if($connect=@ldap_connect($ldap_server))
{
	//echo "<p>Connecting to UCT LDAP server...<font color=\"#008000\">Success</font><br>";
	$justthese = array("ou","dn", "cn", "groupmembership", "aliasedobjectname");
	$sr=ldap_search($connect, $_POST["context"], "cn=".$_POST["username"]); 

	if (ldap_count_entries($connect, $sr) > 0)	
	{
		//echo "Locating User Account...<font color=\"#008000\">Success</font><br>";
		$info = ldap_get_entries($connect, $sr);
   		if (strlen($info[0]["aliasedobjectname"][0]) > 0) 
   		{
			$auth_user = $info[0]["aliasedobjectname"][0];
		}
		else 
		{
			$auth_user = $info[0]["dn"];
		}
		$auth_pass = $_POST["password"];
		$context = str_replace($info[0]["cn"][0],"", $auth_user);
		$context = str_replace("cn=,","",$context);
		if($bind=@ldap_bind($connect, $auth_user, $auth_pass))
		{
			//echo "Authenticating User...<font color=\"#008000\">Success</font><br>";
			$_SESSION['studentnumber'] = strtoupper($_POST["username"]);
			$_SESSION['password'] = $_POST["password"];
			$_SESSION['context'] = $_POST["context"];
			if (isset($_SESSION['studentnumber']) == false)
			{
				header('location:login.asp?str_username='.$_POST["username"].'&errorcase=99&site='. $_GET["site"] .'&URL='. $_GET["URL"]);							
			}
			//Store Session Variables
			$staffswitch = 'Staff';
			if (strlen($_SESSION['studentnumber']) == 9)
			{
				$staffswitch = 'Students';
			}
			$_SESSION['staffswitch'] = $staffswitch;

			
			
			$_SESSION['MD5password'] = md5($_SESSION['password']);
					//Update Stored MD5 Password 
							if(!isset($_SERVER["DOCUMENT_ROOT"])){
							$_SERVER["DOCUMENT_ROOT"]=str_replace('\\','/',getcwd());
							}
							
					
							//now connect to db 
							$odbc = new COM("ADODB.Connection");
							$connstr = "DRIVER=Microsoft Access Driver (*.mdb); DBQ=".$_SERVER['DOCUMENT_ROOT']."\Web\SAN Access\Database\SAN_Controller.mdb";





							$odbc = odbc_connect($connstr,'','');
							if (!$odbc )
						  {exit("Connection Failed: " . $odbc );}
						  
									  
						  
						  
							$cur=odbc_exec($odbc,"select SAN_Username from SAN_User_Records where SAN_Username = '" . $_SESSION['studentnumber'] . "'"); 
							$comboexists = false;
							while(odbc_fetch_row($cur)){ 
							$comboexists = true; 
							} 
							$redirect = false;
							if ($comboexists == true)
							{

							$cur=odbc_exec($odbc,"select SAN_ClearPass from SAN_User_Records where SAN_Username = '" . $_SESSION['studentnumber'] . "'"); 
							odbc_fetch_row($cur);
							if (odbc_result($cur, "SAN_ClearPass") != $_SESSION['password']) 
							{
							$strSQL = "UPDATE SAN_User_Records SET SAN_PreviousPassword = '" . md5(odbc_result($cur, "SAN_ClearPass")) . "', SAN_PreviousClearPass = '" . odbc_result($cur, "SAN_ClearPass") . "' WHERE SAN_Username = '" . $_SESSION['studentnumber'] . "'";							
							odbc_exec( $odbc, $strSQL);							
							}

							
							
							$date = getDate();
					         $second = $date["seconds"];
					         if (strlen($second) == 1)
							$second = "0" . $second;
				       		  $minute = $date["minutes"];
    				 		   if (strlen($minute ) == 1)
							$minute = "0" . $minute ;         
    		     		    $hour = $date["hours"];
     					    if (strlen($hour ) == 1)
							$hour = "0" . $hour ;
     		    		     $day = $date["mday"];
			    		     if (strlen($day ) == 1)
							$day = "0" . $day ;
				   		      $month = $date["mon"];
				   		      if (strlen($month ) == 1)
							$month = "0" . $month ;
				      		   $year = $date["year"];
			     		    if (strlen($year ) == 1)
							$year = "0" . $year ;         
							$strSQL = "UPDATE SAN_User_Records SET SAN_Password = '" . $_SESSION['MD5password'] ."', SAN_ClearPass = '" . $_SESSION['password'] . "', SAN_Last_Login = '" . $year . $month . $day . $hour . $minute . $second . "' WHERE SAN_Username = '" . $_SESSION['studentnumber'] . "'";
							odbc_exec( $odbc, $strSQL);
							
							}
							else
							{
							$redirect = true;
							}
							odbc_close($odbc);
				if ($redirect == true)
				{
				header('location:register.php');
				}
							//-------------------------------------------			
			
			
			
			
			
			//Personalize welcome message
			echo "<p>Welcome back ";
			$ldap_server = $global_ldap_server;
			$connect=@ldap_connect($ldap_server);
			ldap_set_option($connect, LDAP_OPT_PROTOCOL_VERSION, 3);
			$filter="(&(cn=" . $_POST["username"] . "))"; 
			$sr=ldap_search($connect, "o=uct", $filter); 
			$info = ldap_get_entries($connect, $sr);
			$temp = strval($info[0]["fullname"][0]);
			$temp = strtolower($temp);
			echo strtotitle($temp) . " (" . $_POST["username"] . ")</p>\n";
			@ldap_close($connect);

			$staffswitch = 'Staff';
			if (strlen($_SESSION['studentnumber']) == 9)
			{
				$staffswitch = 'Students';
			}

			$dir = $base_upload_folder . $staffswitch . "\\" . $_SESSION['studentnumber'];
			if (is_dir($dir)) 
			{
				echo "<p>Please select the SAN folder that you wish to access from the list below:</p>";
				echo "<ul><li><a href=\"My_SAN_File_List.php?folder=" . $_SESSION['studentnumber'] . "\">" . $_SESSION['studentnumber'] . "</a> <font color=\"#B08A59\" size=\"1\">(" .  str_replace(" ","",formatfilesize(UsageStat($base_upload_folder . $_SESSION['staffswitch'] . "\\" . $_SESSION['studentnumber']))) . "/" . str_replace(" ","",formatfilesize(Get_Size_Limit($_SESSION['studentnumber']))) .  ")</font></li></ul>";
			}
			else
			{
				echo "<p>Sorry, but it appears that you don't have a home directory on the SAN system.</p>";
				
				
								if(!isset($_SERVER["DOCUMENT_ROOT"])){
							$_SERVER["DOCUMENT_ROOT"]=str_replace('\\','/',getcwd());
							}
							
					
							//now connect to db 
							$odbc = new COM("ADODB.Connection");
							$connstr = "DRIVER=Microsoft Access Driver (*.mdb); DBQ=".$_SERVER['DOCUMENT_ROOT']."\Web\SAN Access\Database\SAN_Controller.mdb";


							$odbc = odbc_connect($connstr,'','');
							if (!$odbc )
						  {exit("Connection Failed: " . $odbc );}
						  

							$cur=odbc_exec($odbc,"select SAN_Username, SAN_Active from SAN_User_Records where SAN_Username = '" . $_SESSION['studentnumber'] . "'"); 
							$comboexists = false;
							while(odbc_fetch_row($cur)){ 
							$comboexists = true; 
							$accountstatus = odbc_result($cur, "SAN_Active");
							} 
							$redirect = false;
							if ($comboexists == true)
							{
							if ($accountstatus == "Removed")
							{
							echo "<p>Sorry, but it appears that your SAN root directory has been removed. This is most likely due to the fact that you were deregistered from the Faculty before. Please contact the relevant IT support staff.</p>";
							}
							else
							{							
							echo "<p>Please note that the account you are trying to use has already been registered on this system and is currently awaiting the generation of its root directory. Please be patient as this process can take up to anything between 15 and 30 minutes to complete.</p>";
							}
							}
							else
							{
								$redirect = true;
							}
							odbc_close($odbc);
				
				if ($redirect == true)
				{
				header('location:register.php');
				}
			}
  		}
		else 
		{
			//echo "Authenticating User...<font color=\"#FF0000\">Fail</font></p>";  
			header('location:login.asp?str_username='.$_POST["username"].'&errorcase=3&site='. $_GET["site"] .'&URL='. $_GET["URL"]);
		}
	}
	else 
	{
  		//echo "Locating User Account...<font color=\"#FF0000\">Fail</font></p>";
		header('location:login.asp?str_username='.$_POST["username"].'&errorcase=2&site='. $_GET["site"] .'&URL='. $_GET["URL"]);  			
  	}
	@ldap_close($connect);
}
else 
{
	//echo "<p>Connecting to UCT LDAP server...<font color=\"#FF0000\">Fail</font></p>";
	header('location:login.asp?str_username='.$_POST["username"].'&errorcase=1&site='. $_GET["site"] .'&URL='. $_GET["URL"]);							
	@ldap_close($connect);
}

require_once('C:\Inetpub\wwwroot\SAN\Web\SAN Access\Includes\Layout\main_page_bottom.php'); 
?>