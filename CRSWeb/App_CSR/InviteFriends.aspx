<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/EmployeeLayout.Master" CodeBehind="InviteFriends.aspx.vb" Inherits="CRSWeb.InviteFriends" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script language="javascript" type="text/javascript">
        var redirectTimerId = 0;
        function closeWindow() {
            window.close();
        }
    </script>

    <asp:Panel ID="Panel1" runat="server">
        <%--<div class="login-form">--%>
            <form runat="server">
                <div class="modal-content">
                    <div id="body" runat="server" class="modal-body">
                        <div class="row">
                            <div class="col-md-12">
                                <label id="previewtitle3" runat="server" for="input-small" class=" form-control-label font-headersize">Header title</label>
                            </div>
                        </div>
                        <div class="row mt-2">
                            <div class="col-md-12">
                                <div class="p-2 text-center" id="previewbody3" runat="server" style="background-color: forestgreen">
                                    <div class="col-md-12 text-center">
                                        <label id="previewdetail2" runat="server" for="input-small" class=" form-control-label font-headersize">detail...</label>
                                    </div>
                                    <div class="col-md-12 text-center">
                                        <img id="previewimg3" runat="server" src="../Images/default.png" />
                                    </div>

                                </div>

                            </div>
                        </div>
                        <div class="row mt-2">
                            <div class="col-md-12 text-center">
                                <%--                                <button type="button" class="btn btn-primary font-buttonsize">คลิกเพื่อเชิญเพื่อนร่วมโครงการ</button>
                                <button type="button" class="btn btn-primary font-buttonsize">ปิดหน้าต่าง</button>--%>
                                <asp:Button ID="btFriend" runat="server" Text="คลิกเพื่อเชิญเพื่อนเข้าร่วมโครงการ" CssClass="btn btn-primary font-buttonsize" />
                                <asp:Button ID="btClose" runat="server" Text="ปิดหน้าต่าง" CssClass="btn btn-primary font-buttonsize" Visible="false" />

                            </div>
                        </div>
                        <div class="row mt-2"></div>
                        <%--<div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    </div>--%>
                    </div>
                </div>
            </form>
       <%-- </div>--%>
    </asp:Panel>
</asp:Content>
