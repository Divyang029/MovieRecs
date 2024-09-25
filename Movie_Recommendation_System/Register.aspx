<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="Movie_Recommendation_System.Register" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Register</title>
    <link rel="stylesheet" type="text/css" href="Content/auth.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Register</h2>

            <asp:Label ID="lblUsername" runat="server" Text="Username: " AssociatedControlID="txtUsername"></asp:Label>
            <asp:TextBox ID="txtUsername" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvUsername" runat="server" 
                ControlToValidate="txtUsername" 
                ErrorMessage="Username is required." 
                ForeColor="Red" 
                Display="Dynamic" />

            <asp:Label ID="lblEmail" runat="server" Text="Email: " AssociatedControlID="txtEmail"></asp:Label>
            <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvEmail" runat="server" 
                ControlToValidate="txtEmail" 
                ErrorMessage="Email is required." 
                ForeColor="Red" 
                Display="Dynamic" />
            <asp:RegularExpressionValidator ID="revEmail" runat="server" 
                ControlToValidate="txtEmail" 
                ErrorMessage="Invalid email address." 
                ForeColor="Red" 
                Display="Dynamic"
                ValidationExpression="^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$" />

            <asp:Label ID="lblPassword" runat="server" Text="Password: " AssociatedControlID="txtPassword"></asp:Label>
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvPassword" runat="server" 
                ControlToValidate="txtPassword" 
                ErrorMessage="Password is required." 
                ForeColor="Red" 
                Display="Dynamic" />

            <asp:Label ID="lblConfirmPassword" runat="server" Text="Confirm Password: " AssociatedControlID="txtConfirmPassword"></asp:Label>
            <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvConfirmPassword" runat="server" 
                ControlToValidate="txtConfirmPassword" 
                ErrorMessage="Confirm Password is required." 
                ForeColor="Red" 
                Display="Dynamic" />
            <asp:CompareValidator ID="cvPassword" runat="server" 
                ControlToCompare="txtPassword" 
                ControlToValidate="txtConfirmPassword" 
                ErrorMessage="Passwords do not match." 
                ForeColor="Red" 
                Display="Dynamic" />
            <br />
            <br />
            <asp:Button ID="btnRegister" runat="server" Text="Register" CssClass="btn" OnClick="btnRegister_Click" />
            <br /><br />
            <asp:Label ID="lblMessage" runat="server" CssClass="message"></asp:Label>
            <asp:HyperLink ID="hlLogin" runat="server" NavigateUrl="~/Login.aspx" CssClass="register-link">Already have an account? Login</asp:HyperLink>
        </div>
    </form>
</body>
</html>
