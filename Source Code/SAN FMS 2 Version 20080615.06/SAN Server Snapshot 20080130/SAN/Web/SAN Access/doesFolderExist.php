<?php 
require_once('C:\Inetpub\wwwroot\SAN\Web\SAN Access\Includes\Variables\global_variables.php');
session_start();
$dir = $base_upload_folder;
$UploadPath = $dir . "\\";
if (is_dir(str_replace("\\\\","\\",$UploadPath.$_GET['filename'])) == TRUE)
{
echo "TRUE\n";
}
else
{
echo "FALSE\n";
}
echo str_replace("\\\\","\\",$UploadPath.$_GET['filename']);
?>