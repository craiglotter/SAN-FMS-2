<?php
require_once('C:\Inetpub\wwwroot\SAN\Web\SAN Access\Includes\Variables\global_variables.php');
session_start();


function readfile_chunked ($filename) { 
  $chunksize = 1*(1024*1024); // how many bytes per chunk 
  $buffer = ''; 
  $handle = fopen($filename, 'rb'); 
  if ($handle === false) { 
   return false; 
  } 
  while (!feof($handle)) { 
   $buffer = fread($handle, $chunksize); 
   print $buffer; 
  } 
  return fclose($handle); 
} 


$filename = $base_upload_folder . $_SESSION['staffswitch'] . "\\" . $_SESSION['folderswitch'] . "\\" . $_GET['file'];


// required for IE, otherwise Content-disposition is ignored
if(ini_get('zlib.output_compression'))
  ini_set('zlib.output_compression', 'Off');

// addition by Jorg Weske
$file_extension = strtolower(substr(strrchr($filename,"."),1));

if( $filename == "" ) 
{
	require_once('C:\Inetpub\wwwroot\SAN\Web\SAN Access\Includes\Layout\main_page_top.php');
	require_once('C:\Inetpub\wwwroot\SAN\Web\SAN Access\Includes\Variables\global_variables.php');
echo "<h1>My SAN | File Download</h1>\n";
	  echo "<p>ERROR: download file NOT SPECIFIED.</p>";

if (isset($_SESSION['folderswitch']) == false)
{
echo "<p><a href=\"my_SAN.php\"><< Go Back to My SAN</a></p>\n";
}
else
{
echo "<p><a href=\"my_SAN_File_List.php?folder=" . $_SESSION['folderswitch'] . "\"><< Go Back to the working Folder</a></p>\n";
}


require_once('C:\Inetpub\wwwroot\SAN\Web\SAN Access\Includes\Layout\main_page_bottom.php'); 	  
  exit;
} elseif ( ! file_exists( $filename ) ) 
{

	require_once('C:\Inetpub\wwwroot\SAN\Web\SAN Access\Includes\Layout\main_page_top.php');
	require_once('C:\Inetpub\wwwroot\SAN\Web\SAN Access\Includes\Variables\global_variables.php');
echo "<h1>My SAN | File Download</h1>\n";
	  echo "<p>ERROR: File not found.</p>";

if (isset($_SESSION['folderswitch']) == false)
{
echo "<p><a href=\"my_SAN.php\"><< Go Back to My SAN</a></p>\n";
}
else
{
echo "<p><a href=\"my_SAN_File_List.php?folder=" . $_SESSION['folderswitch'] . "\"><< Go Back to the working Folder</a></p>\n";
}
require_once('C:\Inetpub\wwwroot\SAN\Web\SAN Access\Includes\Layout\main_page_bottom.php'); 	  

  exit;
};
switch( $file_extension )
{
  case "pdf": $ctype="application/pdf"; break;
  case "exe": $ctype="application/octet-stream"; break;
  case "zip": $ctype="application/zip"; break;
  case "doc": $ctype="application/msword"; break;
  case "xls": $ctype="application/vnd.ms-excel"; break;
  case "ppt": $ctype="application/vnd.ms-powerpoint"; break;
  case "gif": $ctype="image/gif"; break;
  case "png": $ctype="image/png"; break;
  case "jpeg":
  case "jpg": $ctype="image/jpg"; break;
  default: $ctype="application/force-download";
}
header("Pragma: public"); // required
header("Expires: 0");
header("Cache-Control: must-revalidate, post-check=0, pre-check=0");
header("Cache-Control: private",false); // required for certain browsers 
header("Accept-Ranges: bytes");
header("Content-Type: $ctype");
// change, added quotes to allow spaces in filenames, by Rajkumar Singh
header("Content-Disposition: attachment; filename=\"".basename($filename)."\";" );
header("Content-Transfer-Encoding: binary");
header("Content-Length: ".filesize($filename));

//readfile("$filename");
readfile_chunked("$filename");
exit();
?>
