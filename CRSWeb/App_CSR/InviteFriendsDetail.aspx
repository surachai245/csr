<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/EmployeeLayout.Master" CodeBehind="InviteFriendsDetail.aspx.vb" Inherits="CRSWeb.InviteFriendsDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <script type="text/javascript">
        function alertMessage(type = "success", title = "", desc = "บันทึกข้อมูลเรียบร้อย") {
            Swal.fire(
                title,
                desc,
                type
            );
        }
    </script>
    <asp:Panel ID="Panel1" runat="server">
        <%--<div class="login-form">--%>
            <form runat="server">
                <div class="modal-content">
                    <div id="body" runat="server" class="modal-body">
                        <div class="row">
                            <div class="col-md-12">
                                <label id="previewtitle4" runat="server" for="input-small" class=" form-control-label font-headersize">Header title</label>
                            </div>
                        </div>
                        <div class="row mt-2">
                            <div class="col-md-12">
                                <div class="p-2 text-left" id="previewbody4" runat="server" style="background-color: forestgreen">
                                    <div class="col-md-12 text-left">
                                        <label id="previewdetail4" runat="server" for="input-small" class=" form-control-label font-detailsize2">กรอกรหัสพนักงานของเพื่อนที่จะชวนเข้าร่วมโครงการ > กดตรวจสอบข้อมูล > กด Select</label>
                                    </div>
                                    <div class="col-md-12 text-left">
                                        <div class="row">
                                            <div class="col-md-4 text=center">
                                                <asp:TextBox ID="tbSearch" runat="server" class="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-6">
                                                <%--<button type="button" class="btn btn-primary">ตรวจสอบข้อมูล</button>--%>
                                                <asp:Button ID="btInsert" runat="server" Text="ตรวจสอบข้อมูล " CssClass="btn btn-primary font-buttonsize" />
                                            </div>
                                        </div>
                                    </div>
                                    <div id="div1" runat="server" class="col-md-12 text-left" style="display: none;">
                                        <label id="previewtxt_1" runat="server" for="input-small" class=" form-control-label font-detailsize2"></label>
                                        <asp:LinkButton ID="lb1" runat="server" Font-Underline="True" ForeColor="#CC3300">ยกเลิก</asp:LinkButton>
                                    </div>
                                    <div id="div2" runat="server" class="col-md-12 text-left" style="display: none;">
                                        <label id="previewtxt_2" runat="server" for="input-small" class=" form-control-label font-detailsize2"></label>
                                        <asp:LinkButton ID="lb2" runat="server" Font-Underline="True" ForeColor="#CC3300">ยกเลิก</asp:LinkButton>
                                    </div>
                                    <div id="div3" runat="server" class="col-md-12 text-left" style="display: none;">
                                        <label id="previewtxt_3" runat="server" for="input-small" class=" form-control-label font-detailsize2"></label>
                                        <asp:LinkButton ID="lb3" runat="server" Font-Underline="True" ForeColor="#CC3300">ยกเลิก</asp:LinkButton>
                                    </div>
                                    <div id="div4" runat="server" class="col-md-12 text-left" style="display: none;">
                                        <label id="previewtxt_4" runat="server" for="input-small" class=" form-control-label font-detailsize2"></label>
                                        <asp:LinkButton ID="lb4" runat="server" Font-Underline="True" ForeColor="#CC3300">ยกเลิก</asp:LinkButton>
                                    </div>
                                    <div id="div5" runat="server" class="col-md-12 text-left" style="display: none;">
                                        <label id="previewtxt_5" runat="server" for="input-small" class=" form-control-label font-detailsize2"></label>
                                        <asp:LinkButton ID="lb5" runat="server" Font-Underline="True" ForeColor="#CC3300">ยกเลิก</asp:LinkButton>
                                    </div>
                                    <div id="div6" runat="server" class="col-md-12 text-left" style="display: none;">
                                        <label id="previewtxt_6" runat="server" for="input-small" class=" form-control-label font-detailsize2"></label>
                                        <asp:LinkButton ID="lb6" runat="server" Font-Underline="True" ForeColor="#CC3300">ยกเลิก</asp:LinkButton>
                                    </div>
                                    <div id="div7" runat="server" class="col-md-12 text-left" style="display: none;">
                                        <label id="previewtxt_7" runat="server" for="input-small" class=" form-control-label font-detailsize2"></label>
                                        <asp:LinkButton ID="lb7" runat="server" Font-Underline="True" ForeColor="#CC3300">ยกเลิก</asp:LinkButton>
                                    </div>
                                    <div id="div8" runat="server" class="col-md-12 text-left" style="display: none;">
                                        <label id="previewtxt_8" runat="server" for="input-small" class=" form-control-label font-detailsize2"></label>
                                        <asp:LinkButton ID="lb8" runat="server" Font-Underline="True" ForeColor="#CC3300">ยกเลิก</asp:LinkButton>
                                    </div>
                                    <div id="div9" runat="server" class="col-md-12 text-left" style="display: none;">
                                        <label id="previewtxt_9" runat="server" for="input-small" class=" form-control-label font-detailsize2"></label>
                                        <asp:LinkButton ID="lb9" runat="server" Font-Underline="True" ForeColor="#CC3300">ยกเลิก</asp:LinkButton>
                                    </div>
                                    <div id="div10" runat="server" class="col-md-12 text-left" style="display: none;">
                                        <label id="previewtxt_10" runat="server" for="input-small" class=" form-control-label font-detailsize2"></label>
                                        <asp:LinkButton ID="lb10" runat="server" Font-Underline="True" ForeColor="#CC3300">ยกเลิก</asp:LinkButton>
                                    </div>
                                    <hr style="border-top: 1px solid White;" />
                                    <div class="col-md-12 text-left">
                                        <label id="previewtxt17" runat="server" for="input-small" class=" form-control-label font-detailsize2">ข้อความที่อยากบอกเพื่อน</label>
                                    </div>
                                    <div class="col-md-12 text-left">
                                        <asp:TextBox ID="txtMsg" runat="server" class="form-control" TextMode="MultiLine" Height="100"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row mt-2">
                            <div class="col-md-12 text-center">
                                <asp:Button ID="btSendMail" runat="server" Text="ส่งเชิญเพื่อนเพื่อร่วมสมัคร" CssClass="btn btn-primary font-buttonsize" />
                                <asp:Button ID="btClose" runat="server" Text="ปิดหน้าต่าง" CssClass="btn btn-primary font-buttonsize" Visible="false" />
                            </div>
                        </div>
                    </div>
                </div>
            </form>
       <%-- </div>--%>
    </asp:Panel>
</asp:Content>
