<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="TempMail.WebForm1" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Welcome to Tempmail</title>
	<link rel="stylesheet" type="text/css" href="css/index.css" />
	<link rel="stylesheet" href="https://www.w3schools.com/w3css/4/w3.css" />
	<link rel="shotcut icon" type="image/x-icon" href="img/logo.png" />
</head>
<body>
    <form id="form1" runat="server">
		<div class="header">
			<img src="img/logo.png" height="140px" style="margin-top: 10px;margin-left: 12%; float: left;" />
			<h1 class="w3-text-white" style="margin-right: 18%; "><br />Welcome to Tempmail!</h1>
		</div>
        <div class="block">
			<div class="text">
				<p style="text-align: justify;">Nowadays an email address is a popular communication method, the practicality and other advantages of which are difficult to overestimate. Today many network resources, websites, programs allow you to activate your account or license solely by sending a control letter to an e-mail address. However, no one will ever guarantee that the email address entered anywhere will not be used by anyone to send unsolicited emails, which are called "spam". But how to be in this situation? The answer to this question is simple - your assistant will be a temporary mail, currently open to you.</p>

				<p style="text-align: justify;">How to use this service: </p>
				<ul style="text-align: justify;">
					<li>To <b>get an individual e-mail address</b> you should click on the red button "Generate". Then service generates for you temporary e-mail address for 1 hour. If you exceed your time for your temporary e-mail address, it will be deleted. </li>
					<li>To <b>extend your e-mail address</b> for an another hour you should click on the green button "Update" which located on the right side of the web-page. If you exceed your time for your temporary e-mail address and then click to this button - you will see start web-page without your individual e-mail address. </li>
					<li>To <b>check your messages</b> you should click on the red button "Update Messages" which located on the right side of the web-page. This service can provide to you only 20 new messages. If you exceed the limit of received messages, you can see only 20 recent incoming messages. If you have not messages yet, you will see message "You have no messages".</li>
					<li>To <b>clear the list of your messages</b> you should click on the blue button "Clear" which located on the right side of the web-page. Then you will see message "You have no messages".</li>
					<li>To <b>delete your individual e-mail address</b> you should click on the yellow button "Delete Mail" which located on the right side of the web-page. Then your e-mail address and messages will be deleted.</li>
				</ul>
				<table border="0" width="100%">
					<tr>
						<td width="75%"><asp:TextBox ID="Mail" runat="server" CssClass="w3-input w3-border"></asp:TextBox></td>
						<td style="padding-left: 20px;"><asp:Button CssClass="w3-btn w3-red w3-round-large" style="width: 200px;" ID="Generate" runat="server" Text="Generate" OnClick="Generate_Click" /></td>
					</tr>
				</table>
				<h2>Your messages:</h2>
                <asp:PlaceHolder ID="Messages" runat="server"></asp:PlaceHolder>
			</div>
			<div class="btn">
				<p style="margin-left: 10%;">
					<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
					<asp:UpdatePanel ID="UpdatePanel1" runat="server">
						<ContentTemplate>
							<asp:Timer ID="Timer1" runat="server" Interval="1000" ontick="Timer1_Tick"></asp:Timer>
							<asp:Label ID="Timer" runat="server"></asp:Label>
						</ContentTemplate>
					</asp:UpdatePanel>
                </p>
				<table border="0" >
					<tr>
						<td><asp:Button CssClass="w3-btn w3-red w3-round-large" ID="updateMessages" style="width: 160px;" runat="server" Text="Update Messages" OnClick="updateMessages_Click" Visible="False" /></td>
						<td><asp:Button CssClass="w3-btn w3-green w3-round-large" ID="updateMail" style="width: 80px; margin-left: 10px;" runat="server" Text="Update" OnClick="updateMail_Click" Visible="False" /></td>
					</tr>
				</table>
				<table border="0">
					<tr>
						<td><asp:Button CssClass="w3-btn w3-blue w3-round-large" ID="clearMessages" style="width: 80px;" runat="server" Text="Clear" OnClick="clearMessages_Click" Visible="False" /></td>
						<td><asp:Button CssClass="w3-btn w3-yellow w3-round-large" ID="deleteMail" style="width: 160px; margin-left: 10px;" runat="server" Text="Delete Mail" OnClick="deleteMail_Click" Visible="False" /></td>
					</tr>
				</table>
				<asp:Button CssClass="w3-btn w3-purple w3-round-large" ID="sendMessage" style="width: 254px; margin-left: 3px; margin-top: 3px;" runat="server" Text="Send message" Visible="False" PostBackUrl="~/sendMail.aspx"/>
			</div>
		</div>
		<div class="footer"><p class="w3-text-white">Created by group IV-71<br />Copyright &copy; IV-71 (tempmail.com) 2019 All rights reserved</p></div>
    </form>
</body>
</html>
