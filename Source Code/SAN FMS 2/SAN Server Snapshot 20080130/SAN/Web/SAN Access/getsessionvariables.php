<?php 
require_once('C:\Inetpub\wwwroot\SAN\Web\SAN Access\Includes\Layout\main_page_top.php');
require_once('C:\Inetpub\wwwroot\SAN\Web\SAN Access\Includes\Variables\global_variables.php');

session_start();
if (isset($_SESSION['studentnumber']) == true)
{

echo "<h1>Current Session Variables</h1>\n";
echo "<ul>\n";
session_start();
	echo "<li>Username: " . $_SESSION['studentnumber'] . "</li>\n";
	if (strlen($_SESSION['password']) > 0) 	{
		echo "<li>Password: <i>Not displayed</i></li>\n";
	} else {
		echo "<li>Password: <i>No password set</i></li>\n";
	}	
	echo "<li>MD5 Hash: " . $_SESSION['MD5password'] . "</li>\n";	
	//echo "<li>Password: " . $_SESSION['password'] . "</li>\n";
	echo "<li>Context: " . $_SESSION['context'] . "</li>\n";
	echo "<li>Current Status: " . $_SESSION['staffswitch'] . "</li>\n";	
	echo "<li>Current Folder: " . $_SESSION['folderswitch'] . "</li>\n";												
	echo "<li>Last File Uploaded: " . $_SESSION['lastfileuploaded'] . "</li>\n";					
echo "</ul>\n";

} else {

echo "<h1>Current Session Variables</h1>\n";
echo "<p>Sorry, but you need to be logged into the system in order to view this page.</p>";

}


require_once('C:\Inetpub\wwwroot\SAN\Web\SAN Access\Includes\Layout\main_page_bottom.php'); 
?>