<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/EmployeeLayout.Master" CodeBehind="WarningMessage.aspx.vb" Inherits="CRSWeb.WarningMessage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="Panel1" runat="server">
        <%--<div class="login-form">--%>
        <form runat="server">
            <div class="modal-content">
                <div id="body" runat="server" class="modal-body">
                    <div class="row mt-3">
                        <div class="col-md-12">
                            <div class="p-2 text-center" id="previewbody5" runat="server" style="background-color: forestgreen">
                                <div class="row">
                                    <div class="col-md-12 text-center">
                                        <div id="divTxt" runat="server" class="col-md-12 p-3">
                                            <%--<label id="previewdetail" runat="server"  for="input-small" class=" form-control-label font-headersize">detail...</label>--%>
                                        </div>
                                        <%--<label id="previewdetail5" runat="server" for="input-small" class=" form-control-label font-headersize">detail...</label>--%>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </form>
    </asp:Panel>
</asp:Content>
