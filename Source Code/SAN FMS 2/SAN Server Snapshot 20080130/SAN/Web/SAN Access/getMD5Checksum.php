<?php 
require_once('C:\Inetpub\wwwroot\SAN\Web\SAN Access\Includes\Variables\global_variables.php');
require_once('C:\Inetpub\wwwroot\SAN\Web\SAN Access\Includes\Functions\functions.php');

set_time_limit(900);

$dir = $base_upload_folder;
$UploadPath = $dir . "\\";
echo MD5_Checksum(str_replace("\\\\","\\",$UploadPath.$_GET['filename'])) . "\n";
echo str_replace("\\\\","\\",$UploadPath.$_GET['filename']);

?>