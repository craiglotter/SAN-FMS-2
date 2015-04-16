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

echo "<h1>My SAN | File Rename</h1>\n";

echo "<ul>\n";
echo "<li>Working Dir: " . $_GET['workingdir'] . "</li>\n";
echo "<li>Old Name: " . $_GET['oldname'] . "</li>\n";
echo "<li>New Name: " . $_GET['newname'] . "</li>\n";
echo "</ul>\n";
if (strlen($_GET['newname']) < 1)
{
?>
<form name=renamefiles method=get action=rename_file.php>Enter new file name: <input type=text name=newname size=40 value="<?php echo $_GET['oldname']; ?>"><input type=hidden name=oldname value="<?php echo $_GET['oldname']; ?>"><input type=hidden name=workingdir value="<?php echo $_GET['workingdir']; ?>"> <input type=submit name=b1 value="Rename File"></form>
<?php
}
else
{
$oldn = $base_upload_folder . $_SESSION['staffswitch'] . "\\" . $_GET['workingdir'] . "\\" . $_GET['oldname'];
$newn = $base_upload_folder . $_SESSION['staffswitch'] . "\\" . $_GET['workingdir'] . "\\" . strip_File($_GET['newname']);
if (rename($oldn,$newn) == TRUE)
{
	header("location: my_SAN_File_List.php?folder=" . $_SESSION['folderswitch']);
}
else
{
echo "<p>Sorry, the file rename you requested failed. Most likely your suggested file name already exists, or you entered some invalid characters for the new item's name.</p>";
}


		if (isset($_SESSION['folderswitch']) == false)
{
echo "<p><a href=\"my_SAN.php\"><< Go Back to My SAN</a></p>\n";
}
else
{
echo "<p><a href=\"my_SAN_File_List.php?folder=" . $_SESSION['folderswitch'] . "\"><< Go Back to the working Folder</a></p>\n";
}
	
}

//Remove some chars that are not allowed in filenames
function strip_File($FileName) {
	//$arrItems = array(" ", "`", "~", "!","@","#","$","^","&","*","(",")","+","=","{","}","[","]","|","'",";","\\","<",">","&","\"","'");
	$arrItems = array("`", "~", "!","@","#","$","^","&","*","+","=","{","}","[","]","|","'",";","\\","<",">","&","\"","'");
	$FileName = str_replace($arrItems, "_", $FileName);
	$FileName = urldecode($FileName);
	$FileName = str_replace("%", "_", $FileName);
	return $FileName;
}

require_once('C:\Inetpub\wwwroot\SAN\Web\SAN Access\Includes\Layout\main_page_bottom.php'); 
?>