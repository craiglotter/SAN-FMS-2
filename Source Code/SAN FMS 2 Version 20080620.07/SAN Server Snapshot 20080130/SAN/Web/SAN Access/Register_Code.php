<?php 
require_once('C:\Inetpub\wwwroot\SAN\Web\SAN Access\Includes\Layout\main_page_top.php');
require_once('C:\Inetpub\wwwroot\SAN\Web\SAN Access\Includes\Variables\global_variables.php');
require_once('C:\Inetpub\wwwroot\SAN\Web\SAN Access\Includes\Functions\functions.php');
require_once('C:\Inetpub\wwwroot\SAN\Web\SAN Access\Includes\Functions\database_calls.php');

echo "<h1>My SAN | Registration Complete</h1>\n";

session_start();
if (isset($_SESSION['studentnumber']) == false)
{
	header('location:login.asp');							
	exit();
}



							if(!isset($_SERVER["DOCUMENT_ROOT"])){
							$_SERVER["DOCUMENT_ROOT"]=str_replace('\\','/',getcwd());
							}
							
					
							//now connect to db 
							$odbc = new COM("ADODB.Connection");
							$connstr = "DRIVER=Microsoft Access Driver (*.mdb); DBQ=".$_SERVER['DOCUMENT_ROOT']."\Web\SAN Access\Database\SAN_Controller.mdb";


							$odbc = odbc_connect($connstr,'','');
							if (!$odbc )
						  {exit("Connection Failed: " . $odbc );}
						  

							$cur=odbc_exec($odbc,"select SAN_Username from SAN_User_Records where SAN_Username = '" . $_POST['SAN_Username'] . "'"); 
							$comboexists = false;
							while(odbc_fetch_row($cur)){ 
							$comboexists = true; 
							} 
							if ($comboexists == true)
							{
							echo "<p>Sorry but the account you are trying to register has already been registered on this system.</p>";
							}
							else
							{

							$cur=odbc_exec($odbc,"Insert into SAN_User_Records (SAN_Username,SAN_Password,SAN_ClearPass,SAN_Active,SAN_Account_Type,SAN_Email_Address,SAN_Context,SAN_Additional_Limit,SAN_Name) values ('" . $_POST['SAN_Username'] . "','" . $_POST['SAN_Password'] . "','" . $_POST['SAN_ClearPass'] . "','False','" . $_POST['SAN_Account_Type'] . "','" . $_POST['SAN_Email_Address'] . "','" . $_POST['SAN_Context'] . "','0','" . $_POST['SAN_Name'] . "')"); 						  
							echo "<p>Congratulations, your SAN account registration completed successfully. You will be notified via email once your SAN folder is automatically generated and set up for use. Please note that this process can take up to anything between 15 and 30 minutes to complete.</p>";
							}
							odbc_close($odbc);
							//-------------------------------------------			
			


require_once('C:\Inetpub\wwwroot\SAN\Web\SAN Access\Includes\Layout\main_page_bottom.php'); 
?>