<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/EmployeeLayout.Master" CodeBehind="EmployeeView.aspx.vb" Inherits="CRSWeb.EmployeeView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        //Console.log(User.Identity.Name);
        //var object = new SP.ClientContext()
        //console.log('gfhjghjgg');
    </script>
    <asp:Panel ID="Panel1" runat="server">
       <%-- <div class="login-form">--%>
            <form runat="server">
                <div  class="modal-content">
                    <div id="body" runat="server" class="modal-body">
                        <div class="row">
                            <div class="col-md-12">
                                <label id="previewtitle" runat="server" for="input-small" class=" form-control-label font-headersize">Header title</label>
                            </div>
                        </div>
                        <div class="row mt-2">
                            <div class="col-md-12">
                                <div class="p-2 text-center" id="previewbody"  runat="server">
                                    <img id="previewimg" runat="server"  src="../Images/default.png" />
                                </div>

                            </div>
                        </div>
                        <div class="row mt-2 text-center">
                            <div class="col-md-2 text-center"></div>
                            <div class="col-md-8 text-center">
                                <%--<button type="button" class="btn btn-primary font-buttonsize"></button>--%>
                                <asp:Button ID="btRegister" runat="server" Text="คลิกเพื่อสมัครเข้าร่วมโครงการ" CssClass="btn btn-primary font-buttonsize" />
                                <asp:Button ID="btAddFriend" runat="server" Text="แนะนำเพื่อนเข้าร่วมโครงการ" CssClass="btn btn-primary font-buttonsize" />
                            </div>
                             <div class="col-md-2 text-center"></div>
                        </div>
                    </div>
                </div>
            </form>
       <%-- </div>--%>
    </asp:Panel>
</asp:Content>
