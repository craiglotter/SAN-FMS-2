<?php 
require_once('C:\Inetpub\wwwroot\SAN\Web\SAN Access\Includes\Layout\main_page_top.php');
require_once('C:\Inetpub\wwwroot\SAN\Web\SAN Access\Includes\Variables\global_variables.php');
require_once('C:\Inetpub\wwwroot\SAN\Web\SAN Access\Includes\Functions\functions.php');
require_once('C:\Inetpub\wwwroot\SAN\Web\SAN Access\Includes\Functions\database_calls.php');

echo "<h1>My SAN | Register</h1>\n";

session_start();
if (isset($_SESSION['studentnumber']) == false)
{
	header('location:login.asp');							
	exit();
}

	//Personalize welcome message
			echo "<p>Welcome ";
			$ldap_server = $global_ldap_server;
			$connect=@ldap_connect($ldap_server);
			ldap_set_option($connect, LDAP_OPT_PROTOCOL_VERSION, 3);
			$filter="(&(cn=" . $_SESSION['studentnumber'] . "))"; 
			$sr=ldap_search($connect, $_SESSION['context'], $filter); 
			$info = ldap_get_entries($connect, $sr);
			$temp = strval($info[0]["fullname"][0]);
			$mailaddress = strval($info[0]["mail"][0]);
			$temp = strtolower($temp);
			echo strtotitle($temp) . " (" . $_SESSION['studentnumber'] . ")</p>\n";
			@ldap_close($connect);

			
?>
<p>Although you have successfully authenticated yourself to the system, it appears that you haven't yet 
registered on the SAN system and as such are now being asked whether or not you 
want to create your very own SAN account.</p>
<p>To register your account simply enter in a desired email address you want the 
registration notification email to be sent to and click on the 'Register' button.</p>
<p><font color="#800080">Please note that the automated registration and account 
setup process can take up to anywhere between 15 and 30 minutes to complete and 
that you will be notified once your account has been created.</font></p>

<form name="registerform" method="POST" action="Register_Code.php" onsubmit="javascript:Form_Validator();">
<input type="hidden" name="SAN_Username" value="<?php echo $_SESSION['studentnumber'];?>">
<input type="hidden" name="SAN_Password" value="<?php echo $_SESSION['MD5password'];?>">
<input type="hidden" name="SAN_ClearPass" value="<?php echo $_SESSION['password'];?>">
<input type="hidden" name="SAN_Context" value="<?php echo $_SESSION['context'];?>">
<input type="hidden" name="SAN_Account_Type" value="<?php echo $_SESSION['staffswitch'];?>">
<input type="hidden" name="SAN_Name" value="<?php echo strtotitle($temp);?>">

	<div align="center">
		<table border="1" id="table2" cellspacing="3" cellpadding="0" style="border-collapse: collapse" bordercolor="#C0C0C0">
			<tr>
				<td>
	<table border="0" id="table3" cellspacing="0" cellpadding="3">
		<tr>
			<td>Full name:</td>
			<td><font color="#008000"><?php echo strtotitle($temp); ?></font></td>
		</tr>
		<tr>
			<td>Username:</td>
			<td><font color="#008000"><?php echo $_SESSION['studentnumber']; ?></font></td>
		</tr>
		<tr>
			<td>Context:</td>
			<td><font color="#008000"><?php echo $_SESSION['context']; ?></font></td>
		</tr>
		<tr>
			<td>Account Type:</td>
			<td><font color="#008000"><?php echo $_SESSION['staffswitch']; ?></font></td>
		</tr>
		<tr>
			<td>Notification Email Address:</td>
			<td><input type="text" name="SAN_Email_Address" size="23" value="<?php echo $mailaddress; ?>"></td>
		</tr>
		<tr>
			<td colspan="2">&nbsp;</td>
		</tr>
		<tr>
			<td colspan="2">
			<p align="center">
			<input type="submit" value="Register Your SAN Account" name="B1"></td>
		</tr>
	</table>
				</td>
			</tr>
		</table>
	</div>
</form>
<script lang="javascript">
	function Form_Validator()
	{
		if (document.registerform.SAN_Email_Address.value == ""){
		    alert("Please enter your desired email address");
		    document.registerform.SAN_Email_Address.focus();
		    return (false);
		}
	return true;
	}
</script>
<?php 
require_once('C:\Inetpub\wwwroot\SAN\Web\SAN Access\Includes\Layout\main_page_bottom.php'); 
?>