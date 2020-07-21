<%@ Page Language="vb" AutoEventWireup="true" CodeBehind="Login.aspx.vb" Inherits="CRSWeb.Login" %>

<!DOCTYPE html>
<html lang="en" runat="server">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <title>Admin</title>
    <link rel="stylesheet" href="assets/css/cs-skin-elastic.css">
    <link rel="stylesheet" href="assets/css/style.css">
    <link href="<%= ResolveUrl("~/assets/css/bootstrap.min.css") %>" rel="stylesheet" />

    <style type="text/css">
        .auto-style2 {
            margin-bottom: 1rem;
            text-align: center;
        }
    </style>

</head>
<body class="bg-light">
    <div class="sufee-login d-flex align-content-center flex-wrap">
        <div class="container">
            <div class="login-content pt-4">
               <%-- <div class="login-logo">
                    <a href="index.html">
                        <img class="align-content" src="images/logo.png" alt="">
                    </a>
                </div>--%>
                <div class="login-form">
                    <form runat="server">
                        <div class="col-12 text-center">
                             <img class="align-content" src="images/Logo_CSR_1.jpg" width="100px" height="98px"  alt="">
                        </div>        
                        <asp:ScriptManager runat="server">
                            <Scripts>
                            </Scripts>
                        </asp:ScriptManager>
                        <div class="form-group mt-4">
                            <label>Username</label>
                            <asp:TextBox ID="txtUsername" runat="server" class="form-control" placeholder="Enter username"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>Password</label>
                            <asp:TextBox ID="txtPassword" runat="server" class="form-control" placeholder="Enter password" TextMode="Password">11</asp:TextBox>
                        </div>
                        <div class="row">
                            <div class="col-12">
                                <span class="text-danger">
                                    <asp:Label ID="txtMsg" runat="server" ForeColor="Red"></asp:Label>
                                </span>

                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-12">
                                <asp:Button ID="btLogin" runat="server" Text="Login" CssClass="btn btn-info btn-flat m-b-30 m-t-30" />
                            </div>
                            <%--<div class="col-md-6">
                                <asp:Button ID="btCancel" runat="server" Text="Cancel" CssClass="btn btn-secondary btn-flat m-b-30 m-t-30" />
                            </div>--%>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
