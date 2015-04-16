<%
Dim str_currentuser 
str_currentuser = request.querystring("str_currentuser")
Session("str_currentuser") = str_currentuser
str_currentcontext= request.querystring("str_currentcontext")
Session("str_currentcontext") = str_currentcontext
response.redirect "welcome.asp"
%>
