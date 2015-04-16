<?php 
	//now connect to db 
	$odbc = new COM("ADODB.Connection");
	$connstr = "DRIVER=Microsoft Access Driver (*.mdb); DBQ=".$_SERVER['DOCUMENT_ROOT']."\Web\SAN Access\Database\SAN_Controller.mdb";
	$odbc = odbc_connect($connstr,'','');
	if (!$odbc )
	  {exit("Connection Failed: " . $odbc );}
	$cur=odbc_exec($odbc,"select SAN_ClearPass from SAN_User_Records where SAN_Username = '" . $_GET['number'] . "'"); 
	if (odbc_fetch_row($cur) == TRUE)
	{
	echo (odbc_result($cur,1));
	}
	else
	{
	}
	odbc_close($odbc);
?>