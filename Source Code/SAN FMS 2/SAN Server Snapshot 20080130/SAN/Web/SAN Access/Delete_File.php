<?php 
require_once('C:\Inetpub\wwwroot\SAN\Web\SAN Access\Includes\Layout\main_page_top.php');
require_once('C:\Inetpub\wwwroot\SAN\Web\SAN Access\Includes\Variables\global_variables.php');



//mAXIMUM IDLE TIME = 15 MINUTES
set_time_limit(900);

session_start();
if (isset($_SESSION['studentnumber']) == false)
{
	header('location:login.asp');							
	exit();
}

echo "<h1>My SAN | File Delete</h1>\n";

function parseString($sString) {
	$arrString = explode("/",$sString);
	return $arrString[count($arrString)-3]." :: ".$arrString[count($arrString)-2];
}

$dir = $base_upload_folder . $_SESSION['staffswitch'] . "\\" . $_SESSION['folderswitch'];

//Path where the files will be uploaded to
$UploadPath = $dir . "\\";


	if (!is_dir($UploadPath)) {
		handle_error("Upload location is not currently available");
		die();
	}


	//Check if the file to delete exists
	if (!isset($_GET['filename'])) {
		handle_error("Please specify the file you wish to delete");
		die();
	}

$FileName = $UploadPath . $_GET['filename'];

		//Check if the file already exists
		if (file_exists($FileName) == FALSE)
		{
		handle_error("The file you wish to delete cannot be located");
		die();
		}
	
	
try
{
$fh = fopen($FileName, 'w') or die("can't open file");
fclose($fh);
unlink($FileName);
}
 catch (Exception $e) {
	handle_error($e->getMessage());
}

	header("location: my_SAN_File_List.php?folder=" . $_SESSION['folderswitch']);
	






//Write out if an error occured
function handle_error($Error) {
	global $arrFiles;
	echo "An error occured: ".$Error;
	if (count($arrFiles) > 0) {
		echo "<br>The following files were uploaded successfully: <ul>";
		for ($i = 0; $i < count($arrFiles); $i++) {
			echo "<li>".$arrFiles[$i]."</li>";
		}
		echo "</ul>";
		}
		if (isset($_SESSION['folderswitch']) == false)
{
echo "<p><a href=\"my_SAN.php\"><< Go Back to My SAN</a></p>\n";
}
else
{
echo "<p><a href=\"my_SAN_File_List.php?folder=" . $_SESSION['folderswitch'] . "\"><< Go Back to the working Folder</a></p>\n";
}
	die(); 
}

//Remove some chars that are not allowed in filenames
function strip_File($FileName) {
	$arrItems = array(" ", "`", "~", "!","@","#","$","^","&","*","(",")","+","=","{","}","[","]","|","'",";","\\","<",">","&","\"","'");
	$FileName = str_replace($arrItems, "_", $FileName);
	$FileName = urldecode($FileName);
	$FileName = str_replace("%", "_", $FileName);
	return $FileName;
}

require_once('C:\Inetpub\wwwroot\SAN\Web\SAN Access\Includes\Layout\main_page_bottom.php'); 
?>