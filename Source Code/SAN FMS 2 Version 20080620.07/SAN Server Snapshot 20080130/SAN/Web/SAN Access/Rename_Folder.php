<?php 
require_once('C:\Inetpub\wwwroot\SAN\Web\SAN Access\Includes\Layout\main_page_top.php');
require_once('C:\Inetpub\wwwroot\SAN\Web\SAN Access\Includes\Variables\global_variables.php');

function dirmv($source, $dest, $overwrite = false, $funcloc = NULL){

  if(is_null($funcloc)){
    $dest .= '/' . strrev(substr(strrev($source), 0, strpos(strrev($source), '/')));
    $funcloc = '/';
  }

  if(!is_dir(loc1 . $dest . $funcloc))
    mkdir(loc1 . $dest . $funcloc); // make subdirectory before subdirectory is copied

  if($handle = opendir(loc1 . $source . $funcloc)){ // if the folder exploration is sucsessful, continue
    while(false !== ($file = readdir($handle))){ // as long as storing the next file to $file is successful, continue
      if($file != '.' && $file != '..'){
        $path  = $source . $funcloc . $file;
        $path2 = $dest . $funcloc . $file;

        if(is_file(loc1 . $path)){
          if(!is_file(loc1 . $path2)){
            if(!@rename(loc1 . $path, loc1 . $path2)){
              echo '<font color="red">File ('.$path.') could not be moved, likely a permissions problem.</font>';
            }
          } elseif($overwrite){
            if(!@unlink(loc1 . $path2)){
              echo 'Unable to overwrite file ("'.$path2.'"), likely to be a permissions problem.';
            } else
              if(!@rename(loc1 . $path, loc1 . $path2)){
                echo '<font color="red">File ('.$path.') could not be moved while overwritting, likely a permissions problem.</font>';
              }
          }
        } elseif(is_dir(loc1 . $path)){
          dirmv($source, $dest, $overwrite, $funcloc . $file . '/'); //recurse!
          rmdir(loc1 . $path);
        }
      }
    }
    closedir($handle);
  }
} // end of dirmv()

//mAXIMUM IDLE TIME = 15 MINUTES
set_time_limit(900);

session_start();
if (isset($_SESSION['studentnumber']) == false)
{
	header('location:login.asp');							
	exit();
}

echo "<h1>My SAN | Folder Rename</h1>\n";

echo "<ul>\n";
echo "<li>Working Dir: " . $_GET['workingdir'] . "</li>\n";
echo "<li>Old Folder Name: " . $_GET['oldname'] . "</li>\n";
echo "<li>New Folder Name: " . $_GET['newname'] . "</li>\n";
echo "</ul>\n";
if (strlen($_GET['newname']) < 1)
{
?>
<form name=renamefiles method=get action=Rename_Folder.php>Enter new folder name: <input type=text name=newname size=40 value="<?php echo $_GET['oldname']; ?>"><input type=hidden name=oldname value="<?php echo $_GET['oldname']; ?>"><input type=hidden name=workingdir value="<?php echo $_GET['workingdir']; ?>"> <input type=submit name=b1 value="Rename File"></form>
<?php
}
else
{
$oldn = $base_upload_folder . $_SESSION['staffswitch'] . "\\" . $_GET['workingdir'] . "\\" . $_GET['oldname'];
$newn = $base_upload_folder . $_SESSION['staffswitch'] . "\\" . $_GET['workingdir'] . "\\" . strip_File($_GET['newname']);

define('loc1', $base_upload_folder . $_SESSION['staffswitch'] , true);
dirmv($_GET['workingdir'] . "\\" . $_GET['oldname'],$_GET['workingdir'] . "\\" . $_GET['newname'],false);

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