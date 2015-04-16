<?php
	function Get_Size_Limit($currentusername) 
	{
		//now connect to db 
		$limit_bytes = 0;
		$odbc = new COM("ADODB.Connection");
		$connstr = "DRIVER=Microsoft Access Driver (*.mdb); DBQ=".$_SERVER['DOCUMENT_ROOT']."\Web\SAN Access\Database\SAN_Controller.mdb";
		$odbc = odbc_connect($connstr,'','');
		if (!$odbc )
		{
			exit("Connection Failed: " . $odbc );
		}
 		$cur=odbc_exec($odbc,"select SAN_Additional_Limit, SAN_Account_Type from SAN_User_Records where SAN_Username = '" . $currentusername . "'"); 
		if (odbc_fetch_row($cur))
		{ 
			$limit_bytes = odbc_result($cur,"SAN_Additional_Limit");
		}
		$sqlrequest = "";
		if (odbc_result($cur,"SAN_Account_Type") == "Staff")
		{
		$sqlrequest = "select SAN_Staff_Limit as Limit from SAN_Global_Settings where SAN_Globals_Identifier = 'SAN1'";
		}
		else
		{
		$sqlrequest = "select SAN_Student_Limit as Limit from SAN_Global_Settings where SAN_Globals_Identifier = 'SAN1'";		
		}
		$cur=odbc_exec($odbc,$sqlrequest);
		if (odbc_fetch_row($cur))
		{ 
			$limit_bytes = $limit_bytes + odbc_result($cur,"Limit");
		}
		odbc_close($odbc);
		return $limit_bytes;
	}
?>							