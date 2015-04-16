<?php 
require_once('C:\Inetpub\wwwroot\SAN\Web\SAN Access\Includes\Layout\main_page_top.php');
require_once('C:\Inetpub\wwwroot\SAN\Web\SAN Access\Includes\Variables\global_variables.php');
require_once('C:\Inetpub\wwwroot\SAN\Web\SAN Access\Includes\Functions\functions.php');
require_once('C:\Inetpub\wwwroot\SAN\Web\SAN Access\Includes\Functions\database_calls.php');

session_start();
if (isset($_SESSION['studentnumber']) == false)
{
	header('location:login.asp');							
	exit();
}


echo "<h1>My SAN | File Listing</h1>\n";

$folderswitch = $_GET["folder"];

$oldfolderswitch = "";
if (strrpos($folderswitch,"\\") <> -1)
{
$oldfolderswitch = substr($folderswitch,0, strrpos($folderswitch,"\\"));
}


$folderswitch = $_GET["folder"];

if (strlen($folderswitch) < 1) 
{
$folderswitch = $_SESSION['studentnumber'];
}
$_SESSION['folderswitch'] = $folderswitch;


echo "<h2>My SAN\\" . $_SESSION['folderswitch'] . "</h2>\n";


if (($_SESSION['folderswitch'] == ($_SESSION['studentnumber'] . "\\")) || ($_SESSION['folderswitch'] == ($_SESSION['studentnumber'])))
{
echo "<p><a href=\"my_SAN.php\"><< Go Back to My SAN</a></p>\n";
}
else
{
echo "<p><a href=\"my_SAN_File_List.php?folder=" . $oldfolderswitch . "\"><< Go Back to the previous Folder</a></p>\n";
}



if ((isset($_SESSION['folderswitch']) == false) || (strlen($_SESSION['folderswitch']) < 1))
{
header('location:My_SAN.php');		
}
else
{
$dir = $base_upload_folder . $_SESSION['staffswitch'] . "\\" . $_SESSION['folderswitch'];


echo "<p><font color=\"#49874E\">You are currenly using " . formatfilesize(UsageStat($base_upload_folder . $_SESSION['staffswitch'] . "\\" .$_SESSION['studentnumber'])) . " of your " . formatfilesize(Get_Size_Limit($_SESSION['studentnumber'])) . " limit</font></p>";


if (is_dir($dir)) 
{
    if ($dh = opendir($dir)) 
    {
	    echo "<p><b>Sub Folders:</b></p>";
	    $foldercount = 0;
	    echo "<ul>\n";
        while (($file = readdir($dh)) !== false) 
        {
        	if (($file != ".") & ($file != "..") & (substr($file,0,1) != "_"))
        	{
	        	if (is_dir($dir . "\\" . $file) == true) 
	        	{
			       	echo "<li><a href=\"my_SAN_File_List.php?folder=" . $_SESSION['folderswitch'] . "\\" . $file . "\">" . $file . "</a> <a class=\"func\" href=\"rename_folder.php?workingdir=" . $_SESSION['folderswitch'] . "&oldname=" . $file . "\">[R]</a><a class=\"func\" href=\"delete_folder.php?filename=" . $file . "\">[D]</a></li>";
	       		    $foldercount = $foldercount + 1;
		       	}
			}
		}
	    echo "</ul>\n";
	    if ($foldercount == 1)
	    {
	    echo "<p><i><font size=1>1 folder was located in the current directory</font></i></p>";
	    }
	    else
	    {
	    echo "<p><i><font size=1>" . $foldercount . " folders were located in the current directory</font></i></p>";	    
	    }
	    closedir($dir);
		$dh = opendir($dir);
	    echo "<p><b>Files:</b></p>";
	    $foldercount = 0;

	    echo "<ul>\n";
        while (($file = readdir($dh)) !== false) 
        {
        	if (($file != ".") & ($file != ".."))
        	{
        	if (is_dir($dir . "\\" . $file) == false) 
	        	{
			       	echo "<li><a href=\"file_download.php?user=" . $_SESSION['studentnumber'] . "&folder=" . $_SESSION['folderswitch'] . "&file="  . $file . "\">" . $file . "</a> <font color=\"#B78758\" size=\"1\">["  . formatfilesize(filesize($dir . "\\" . $file)). "]</font><a class=\"func\" href=\"rename_file.php?workingdir=" . $_SESSION['folderswitch'] . "&oldname=" . $file . "\">[R]</a><a class=\"func\" href=\"delete_file.php?filename=" . $file . "\">[D]</a></li>";
			       	$foldercount = $foldercount + 1;

		       	}
			}
		}
	    echo "</ul>\n";		
	    			    if ($foldercount == 1)
	    {
	    echo "<p><i><font size=1>1 file was located in the current directory</font></i></p>";
	    }
	    else
	    {
	    echo "<p><i><font size=1>" . $foldercount . " files were located in the current directory</font></i></p>";	    
	    }
		closedir($dir);

		?>
		<p>&nbsp;</p>
<SPAN onclick="doSection(Sec1)" STYLE="cursor:hand" onMouseover="self.status='Click here for details' " onMouseout="self.status=''; return true"><font face="Webdings" color="9A9A9A" size="1">56</font><font face="Verdana" color="#9A9A9A" size="1" colspan = 3><b> Toggle HTTP Uploader:</b> <i>(In case the normal uploader component is not available)</i></font></SPAN>		
<DIV CLASS="off" ID="Sec1">
<h2>File Upload</h2>
<p>You can use the following upload form to upload a file to the current directory. <i>(Please note that you cannot upload folders using this form)</i></p>
		<form enctype="multipart/form-data" action="Upload_File.php" method="POST">
		<input type="hidden" name="folderswitch" value="<?php echo $_SESSION['folderswitch']; ?>">
<input type="hidden" name="staffswitch" value="<?php echo $_SESSION['staffswitch']; ?>">
		<input type="hidden" name="URL" value="<?php echo $dir; ?>">
<div align="center">
<table border="0" cellpadding="0" cellspacing="0"><tr><td>File to upload: 
	<input name="userfile[]" type="file" size="31" /></td></tr>
<!--<tr><td>File 2 to upload: <input name="userfile[]" type="file" size="31" /></td></tr>-->

<tr><td align="center"><br>
	<input type="submit" value="Upload your file" /></td></tr></table>
		</div>  
		</form>
</DIV>
		<?php
	}
}
else
{
 echo "<p>Requested folder does not exist, or you simply do not have access to it.</p>\n";
}

}

require_once('C:\Inetpub\wwwroot\SAN\Web\SAN Access\Includes\Layout\main_page_bottom.php'); 
?>