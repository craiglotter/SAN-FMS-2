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

echo "<h1>My SAN | File Upload</h1>\n";

function parseString($sString) {
	$arrString = explode("/",$sString);
	return $arrString[count($arrString)-3]." :: ".$arrString[count($arrString)-2];
}

$dir = $base_upload_folder . $_SESSION['staffswitch'] . "\\" . $_SESSION['folderswitch'];

//Path where the files will be uploaded to
$UploadPath = $dir . "\\";

//Track all the files uploaded successfully
$arrFiles = array();


//try {
	if (!is_dir($UploadPath)) {
		handle_error("Upload location is not currently available");
		die();
	}


	//Check if the $_FILES variable exists
	if (!isset($_FILES['userfile'])) {
		handle_error("The file you wish to upload cannot be uploaded.");
		die();
	}

	for ($h = 0; $h < count($_FILES['userfile']['name']); $h++) {
		if ($_FILES['userfile']['name'][$h] == "") continue;
		$_SESSION['lastfileuploaded'] = $_FILES['userfile']['name'][$h];
		
		//Check to see if an error occurred during the file upload

		$FileUploadError = $_FILES['userfile']['error'][$h];
		if ($FileUploadError != 0) {
			/*UPLOAD_ERR_OK
				Value: 0; There is no error, the file uploaded with success. 
				
				UPLOAD_ERR_INI_SIZE
				Value: 1; The uploaded file exceeds the upload_max_filesize directive in php.ini. 
				
				UPLOAD_ERR_FORM_SIZE
				Value: 2; The uploaded file exceeds the MAX_FILE_SIZE directive that was specified in the HTML form. 
				
				UPLOAD_ERR_PARTIAL
				Value: 3; The uploaded file was only partially uploaded. 
				
				UPLOAD_ERR_NO_FILE
				Value: 4; No file was uploaded. 
				
				UPLOAD_ERR_NO_TMP_DIR
				Value: 6; Missing a temporary folder. Introduced in PHP 4.3.10 and PHP 5.0.3. 
				
				UPLOAD_ERR_CANT_WRITE
				Value: 7; Failed to write file to disk. Introduced in PHP 5.1.0. 
			*/
				
			switch ($FileUploadError) {
				case 1:
					handle_error("The specified file is too large to be uploaded");
				break;			
				case 2:
					handle_error("The specified file is too large to be uploaded");
				break;
				case 3:
					handle_error("The specified file was not uploaded completely");
				break;
				case 4:
					handle_error("No file was specified to be uploaded");
				break;
				case 6:
					handle_error("Unable to locate the upload location");
				break;
				case 7:
					handle_error("Unable to write to the server");
				break;
			}
			die();
		}

		//Tells whether the file was uploaded via HTTP POST
		if (!is_uploaded_file($_FILES['userfile']['tmp_name'][$h])) {
			handle_error("Possible file upload attack");
			die();
		}
	
		//Remove special chars from the file
		$FileName = strip_File($_FILES['userfile']['name'][$h]);
	
		//Check if the file already exists
		$TempFilename = "";
		$arrFileName = explode(".",$FileName);
		if (count($arrFileName) > 1) {
	  	for ($i = 0; $i < count($arrFileName); $i++) {
	  		if ($i < count($arrFileName) - 1) {
	  			$TempFilename = $TempFilename.$arrFileName[$i];
	  		} else {
	  			$Extension = ".".strtolower($arrFileName[$i]);
	  			$Extension = str_replace("_","",$Extension);
	  		}
	  	}
	  } else {
	    $TempFilename = $arrFileName[0];
	  }
	
		$TestFileName = $TempFilename;
		$Count = 1;
		$iCounter = 0;
		while (file_exists($UploadPath.$TestFileName.$Extension)) {
	    if ($Count > 0) {
	  		$iCounter++;
	  		$TestFileName = $TempFilename."_".$iCounter;  
	    }
		}
		
		$TestFileName = $TestFileName.$Extension;

		if (!move_uploaded_file($_FILES['userfile']['tmp_name'][$h],$UploadPath.$TestFileName)) {
			handle_error("Unable to complete the file upload");
			die();
		} else {
			$arrFiles[count($arrFiles)] = $UploadPath.$TestFileName;
		}
	}

	header("location: my_SAN_File_List.php?folder=" . $_SESSION['folderswitch']);
	




/* catch (Exception $e) {
	handle_error($e->getMessage());
}*/

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
	//$arrItems = array(" ", "`", "~", "!","@","#","$","^","&","*","(",")","+","=","{","}","[","]","|","'",";","\\","<",">","&","\"","'");
	$arrItems = array("`", "~", "!","@","#","$","^","&","*","+","=","{","}","[","]","|","'",";","\\","<",">","&","\"","'");
	$FileName = str_replace($arrItems, "_", $FileName);
	$FileName = urldecode($FileName);
	$FileName = str_replace("%", "_", $FileName);
	return $FileName;
}

require_once('C:\Inetpub\wwwroot\SAN\Web\SAN Access\Includes\Layout\main_page_bottom.php'); 
?>