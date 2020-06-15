<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sendMail.aspx.cs" Inherits="TempMail.sendMail" %>

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

				<style>
					.w3-container:before {
						display: none;
					}
				</style>
				<div class="w3-container w3-card-4">
					<p>
						<label class="w3-text-grey">Email To</label>
						<asp:TextBox ID="EmailTo" runat="server" CssClass="w3-input w3-border"></asp:TextBox>
					</p>
					<p>
						<label class="w3-text-grey">Email Cc</label>
						<asp:TextBox ID="EmailCc" runat="server" CssClass="w3-input w3-border"></asp:TextBox>
					</p>
					<p>
						<label class="w3-text-grey">Email Bcc</label>
						<asp:TextBox ID="EmailBcc" runat="server" CssClass="w3-input w3-border"></asp:TextBox>
					</p>
					<p>
						<label class="w3-text-grey">Subject</label>
						<asp:TextBox ID="Subject" runat="server" CssClass="w3-input w3-border"></asp:TextBox>
					</p>
					<p>
						<label class="w3-text-grey">Text</label>
						<asp:TextBox ID="Text" runat="server" CssClass="w3-input w3-border" Height="200px" TextMode="MultiLine"></asp:TextBox>
					</p>
					<p class="w3-center">
						<asp:Button ID="SendMail" runat="server" Text="Send Mail" CssClass="w3-button w3-white w3-border w3-border-blue w3-round-large" style="width: 40%;" OnClick="SendMail_Click" />
					</p>
				</div>
			</div>
		</div>
		<div class="footer"><p class="w3-text-white">Created by students FICT IV-71<br />Copyright &copy; IV-71 2020 All rights reserved</p></div>
    </form>
</body>
</html>
