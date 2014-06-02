<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="HelloWebClientAspNet.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="font-family:'Microsoft YaHei UI'">
        <p>
            This is a test of Wcf web service.<br />
            If you enter your name in the text box this Asp.NET application will sent it to
            <br />
            an WPF web service witch will send back a responce message.<br />
        <br />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="TextBox1" runat="server" Height="19px" Width="193px"></asp:TextBox>
        &nbsp;&nbsp;
        <asp:Button ID="Button1" runat="server" Height="26px" Text="Ok" Width="29px" OnClick="Button1_Click" />
        </p>
    </div>
        <p style="margin-left: 280px">
            <asp:Label ID="Label1" runat="server" Font-Size="15pt"></asp:Label>
        </p>
    </form>
</body>
</html>
