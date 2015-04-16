<?php
require_once('C:\Inetpub\wwwroot\SAN\Web\SAN Access\Includes\Variables\global_variables.php');
require_once('C:\Inetpub\wwwroot\SAN\Web\SAN Access\Includes\Functions\functions.php');
require_once('C:\Inetpub\wwwroot\SAN\Web\SAN Access\Includes\Functions\database_calls.php');
$dir = $base_upload_folder;
$UploadPath = $dir . "\\" .$_GET['filename'];
echo usageStat($UploadPath) . "\n";
echo Get_Size_Limit($_GET['username']) . "\n";
echo $UploadPath;
?>