<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Movie_Recommendation_System.Login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <link rel="stylesheet" type="text/css" href="Content/auth.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Login</h2>
            <asp:Label ID="lblUsername" runat="server" Text="Username: " AssociatedControlID="txtUsername"></asp:Label>
            <asp:TextBox ID="txtUsername" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator
                ID="rfvUsername"
                runat="server"
                ControlToValidate="txtUsername"
                ErrorMessage="Username is required."
                ForeColor="Red"
                Display="Dynamic" />

            <asp:Label ID="lblPassword" runat="server" Text="Password: " AssociatedControlID="txtPassword"></asp:Label>
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator
                ID="rfvPassword"
                runat="server"
                ControlToValidate="txtPassword"
                ErrorMessage="Password is required."
                ForeColor="Red"
                Display="Dynamic" />
            <br />
            <br />
            <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn" OnClick="btnLogin_Click" /><br /><br />
            <asp:Label ID="lblMessage" runat="server" CssClass="message"></asp:Label>
            <asp:HyperLink ID="hlRegister" runat="server" NavigateUrl="~/Register.aspx" CssClass="register-link">Don't have an account? Register</asp:HyperLink>
        </div>
    </form>
</body>
</html>
