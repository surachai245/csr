<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/EmployeeLayout.Master" CodeBehind="ThanksGiving.aspx.vb" Inherits="CRSWeb.ThanksGiving" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="Panel1" runat="server">
       <%-- <div class="login-form">--%>
            <form runat="server">
                <div class="modal-content">
                    <div id="body" runat="server" class="modal-body">
                        <div class="row">
                            <div class="col-md-12">
                                <%--<h1 id="preview-title1">Header title</h1>--%>
                                <label id="previewtitle2" runat="server" for="input-small" class=" form-control-label font-headersize">Header title</label>
                            </div>
                        </div>
                        <div class="row mt-3">
                            <div class="col-md-12">
                                <div class="p-2 text-center" id="previewbody2" runat="server" style="background-color: forestgreen">
                                    <div class="row">
                                        <div class="col-md-2">
                                           <%-- <img id="previewimg1" runat="server" src="../Images/EmployeeDefault.png" />--%>
                                              <asp:Image ID="previewimgEmp21" runat="server" />  
                                        </div>
                                        <div class="col-md-10 text-left">
                                            <div class="row">
                                                <label id="previewtxt8" runat="server" for="input-small" class=" form-control-label font-detailsize">ชื่อ นามสกุล</label>
                                            </div>
                                            <div class="row">
                                                <label id="previewtxt9" runat="server" for="input-small" class=" form-control-label font-detailsize">ตำแหน่ง ส่วน</label>
                                            </div>
                                            <div class="row">
                                                <label id="previewtxt10"  runat="server" for="input-small" class=" form-control-label font-detailsize">ฝ่าย/สำนัก สายงาน</label>
                                            </div>
                                            <div class="row">
                                                <label id="previewtxt11" runat="server" for="input-small" class=" form-control-label font-detailsize">อีเมล</label>
                                            </div>
                                            <div class="row">
                                                <label id="previewtxt12" runat="server" for="input-small" class=" form-control-label font-detailsize">เบอร์ต่อ</label>
                                            </div>
                                        </div>
                                    </div>
                                    <hr style="border-top: 1px solid White;" />
                                    <div class="row">
                                        <div class="col-md-12 text-center">
                                            <img id="previewimg2" runat="server"  src="../Images/defaultthank.png" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div id="divTxt" runat="server" class="col-md-12 p-3">
                                            <%--<label id="previewdetail" runat="server"  for="input-small" class=" form-control-label font-headersize">detail...</label>--%>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row mt-2">
                            <div class="col-md-12 text-center">
                                <%--<button type="button" class="btn btn-primary font-buttonsize">แนะนำเพื่อนเข้าร่วมโครงการ</button>
                                <button type="button" class="btn btn-primary font-buttonsize">ปิดหน้าต่าง</button>--%>
                                <asp:Button ID="btGiving" runat="server" Text="แนะนำเพื่อนเข้าร่วมโครงการ" CssClass="btn btn-primary font-buttonsize" />
                                <asp:Button ID="btClose" runat="server" Text="ปิดหน้าต่าง" CssClass="btn btn-primary font-buttonsize" Visible="false" />
                            </div>

                        </div>
                    </div>
                </div>
                <%--<div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                </div>--%>
            </form>
        <%--</div>--%>
    </asp:Panel>
</asp:Content>
