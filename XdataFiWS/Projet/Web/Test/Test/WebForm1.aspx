<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="Test.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="height: 524px">
    <form id="form1" runat="server">
    <div id="test" style="font-family:'Microsoft YaHei UI'">
        <p>
            Info Actif.<br />
            Entrez le nom d'un actif
            <br />
        <br />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="TextBox1" runat="server" Height="19px" Width="193px"></asp:TextBox>
        &nbsp;&nbsp;
        <asp:Button ID="Button1" runat="server" Height="28px" Text="Historique" Width="90px" OnClick="Button1_Click" />
        &nbsp;&nbsp;
        <asp:Button ID="Button2" runat="server" Height="28px" Text="Interest Rate" Width="90px" OnClick="Button2_Click" />
        &nbsp;&nbsp;
        <asp:Button ID="Button3" runat="server" Height="28px" Text="Change Rate" Width="90px" OnClick="Button3_Click" />
        &nbsp;&nbsp;
        <asp:Button ID="Button4" runat="server" Height="28px" Text="XML" Width="90px" OnClick="Button4_Click" />
        </p>
    </div>
        <p style="margin-left: 280px">
            <asp:Label ID="Label1" runat="server" Font-Size="15pt"></asp:Label>
        </p>
        <p style="margin-left: 20px">
            <asp:Table ID="Table1" runat="server" Width="400px" Height="63px">
            </asp:Table>
        </p>
        
    </form>
</body>
</html>
