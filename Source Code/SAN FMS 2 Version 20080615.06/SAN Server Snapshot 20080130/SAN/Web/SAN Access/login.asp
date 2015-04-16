<!--#Include virtual="/Web/SAN Access/Includes/Layout/main_page_top.asp" -->

<%
Dim str_site
Dim str_URL
Dim str_login
str_site = request.querystring("site")
str_URL = request.querystring("URL")
str_login = request.form("login_status")
Dim str_username
str_username = request.querystring("str_username")
if str_username = "" then
str_username = Session("str_currentuser")
end if
str_currentcontext = Session("str_currentcontext")

Dim errorcase
errorcase = request.querystring("errorcase")
Dim errorcaseExtra
errorcaseExtra= request.querystring("errorcaseExtra")
%>

	<h1>Login</h1>
            <p>The section you are trying to access is protected. 
			You are required to authenticate yourself by logging in using your 
			Novell network username (Student/Staff number) and password 
			combination. </p>
	<p><font color="#808080">You may also need to change the user 
	context option to that which most accurately describes your role in the University.</font> </p>
            <% if error_detected = true then %>
            <p><font color="#FF0000">Your login attempt failed. 
            Either the username or password is incorrect.</font></p>
            <% end if %>
            <% if errorcase = 1 then %>
            <p><font color="#FF0000">Your login attempt failed. 
            The system was unable to connect to the LDAP server to verify
			your account details. Please wait a while until this connectivity issue has been resolved.</font></p>
            <% end if %>            
            <% if errorcase = 2 then %>
            <p><font color="#FF0000">Your login attempt failed. 
            No user account with the supplied username could be located on 
			the system for the context selected. Please check that your username 
			and context entered is correct.</font></p>
            <% end if %>    
            <% if errorcase = 3 then %>
            <p><font color="#FF0000">Your login attempt failed. 
            The password entered is incorrect for supplied username. This 
			could be because your account has expired or because you simply mistyped the password 
			when entering it.</font></p>
			<% if not errorcaseExtra = "" then %>
			<p><% if Instr(1, errorcaseExtra, "0 (of", 1) > 0 then %><font color="#CC3333"><% else %><font color="#339933"><% end if %>Currently your grace login count stands at <% response.write errorcaseExtra %>. 
			Zero grace logins means that your account has expired and that you need to contact the helpdesk in order for them to change your password.</font></p>
			<% end if %> 
            <% end if %> 
            <% if errorcase = 4 then %>
            <p><font color="#FF0000">Your login attempt failed. 
            Although your username/password combination is correct, 
			your account doesn't have access to this course's pages. Please check that you are 
			indeed correctly registered for this course with the faculty.</font></p>
            <% end if %>                              
            <div align="center">
              <center>
              <table border="0" width="40%" >
                <tr>
                  <td class="content" width="40%"  align="left" valign="top">
                  <b>Username:</b></td>
                  <td width="60%"  align="left" valign="top">
                    <!--webbot BOT="GeneratedScript" PREVIEW=" " startspan --><script Language="JavaScript" Type="text/javascript"><!--
function FrontPage_Form1_Validator(theForm)
{

  if (theForm.context.selectedIndex < 0)
  {
    alert("Please select one of the \"Login Context\" options.");
    theForm.context.focus();
    return (false);
  }
  return (true);
}
//--></script><!--webbot BOT="GeneratedScript" endspan --><form method="POST" action="my_SAN.php?site=<% response.write str_site %>&URL=<% response.write str_URL %>" name="FrontPage_Form1" onsubmit="return FrontPage_Form1_Validator(this)" language="JavaScript">
                      <input type="text" name="username" tabindex=1 size="20" value="<% response.write trim(str_username) %>">
                    </td>
                  </tr>
                  <tr>
                    <td class="content" width="40%"  align="left" valign="top">
                    <b>Password:</b></td>
                    <td width="60%"  align="left" valign="top">
                    <input type="password" name="password" tabindex=2 size="20">
                    </td>
                  </tr>
                  <tr>
                    <td class="content" width="40%"  align="left" valign="top">
                    <b>Context:</b></td>
                    <td width="60%"  align="left" valign="top">
                    <!--webbot bot="Validation" s-display-name="Login Context" b-value-required="TRUE" --><select size="1" name="context" tabindex=3>
					<option <% if str_currentcontext = "Student" then response.write "selected" %> value="ou=Students,ou=com,ou=main,o=uct">Commerce Student</option>
					<option <% if str_currentcontext = "Staff" then response.write "selected" %> value="ou=Staff,ou=com,ou=main,o=uct">Commerce Staff</option>
					<!--<option value="ou=Students,ou=hum,ou=main,o=uct">Humanities Student</option>
					<option value="ou=Students,ou=ebe,ou=main,o=uct">Engineering Student</option>
					<option value="ou=Students,ou=sci,ou=main,o=uct">Science Student</option>
					<option value="ou=Students,ou=law,ou=main,o=uct">Law Student</option>-->
					</select>
					</td>
                  </tr>
                  <% if len(trim(str_site)) > 0 then %>
                  <tr>
                                  <td class="content" width="40%"  align="left" valign="top">
                  <b>Site:</b></td>
                      <td width="60%"  align="left" valign="top">
                      <select size="1" name="site" tabindex=4>
                      <%
						arrResult = Split(trim(str_site), ";") 
						For iCounter = 0 to Ubound(arrResult)
							if len(arrResult(iCounter)) > 0 then
							Select Case lcase(arrResult(iCounter))
								Case "cf_allstaff"
									response.write "<option value=""" & arrResult(iCounter) & """>" & "Commerce IT Portal" & "</option>" & vbcrlf 		
								Case "tutors"
									response.write "<option value=""" & arrResult(iCounter) & """>" & "Helpdesk Tutor Portal" & "</option>" & vbcrlf 
								Case "fin_operations_finance"
									response.write "<option value=""" & arrResult(iCounter) & """>" & "Finance and Operations Portal" & "</option>" & vbcrlf 
								Case Else
									response.write "<option value=""" & arrResult(iCounter) & """>" & arrResult(iCounter) & "</option>" & vbcrlf 
							End Select
	                       	end if
						Next
                      %>
					</select>
                    </td>
                  </tr>
                  <% end if %>
                </table>
                <input type="hidden" name="login_status" value="yes" >                             
                <center><br>
				<input type="submit" value="   Log in   " tabindex=5></form>
           			
                </font>
			
                </center>
              </div>
			

			
<!--#Include virtual="/Web/SAN Access/Includes/Layout/main_page_bottom.asp" -->