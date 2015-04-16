<?php

$ldap_server = "ldap://127.0.0.1:389";
$connect=@ldap_connect($ldap_server);
ldap_set_option($connect, LDAP_OPT_PROTOCOL_VERSION, 3);
$filter="(&(cn=" . $_GET["SAN_Username"] . "))"; 
$sr=ldap_search($connect, $_GET["SAN_Context"], $filter); 
ldap_sort($connect, $sr, "cn");
$info = ldap_get_entries($connect, $sr);

if (strlen($_GET["SAN_Context"]) < 1)
{
echo -1;
}
else
{
if (strlen($_GET["SAN_Context"]) > 0)
{
echo $info["count"];
}
else
{
echo -1;
}
}

@ldap_close($connect);
?>         

