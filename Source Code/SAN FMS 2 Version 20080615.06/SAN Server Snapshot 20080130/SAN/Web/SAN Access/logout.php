<?php 
require_once('C:\Inetpub\wwwroot\SAN\Web\SAN Access\Includes\Layout\main_page_top.php');
require_once('C:\Inetpub\wwwroot\SAN\Web\SAN Access\Includes\Variables\global_variables.php');

session_start();
unset($_SESSION['studentnumber']);
unset($_SESSION['password']);
unset($_SESSION['context']);
unset($_SESSION['folderswitch']);
unset($_SESSION['staffswitch']);
unset($_SESSION['lastfileuploaded']);					
?>
	<h1>Logged Out</h1>
<p align="left">You have been successfully logged out of the system. Thank you for using the SAN Access system.</p>
	
<?php 
header("location: login.asp");
require_once('C:\Inetpub\wwwroot\SAN\Web\SAN Access\Includes\Layout\main_page_bottom.php'); 
?>