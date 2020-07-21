<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/EmployeeLayout.Master" CodeBehind="Employeeregister.aspx.vb" Inherits="CRSWeb.Employeeregister" %>
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
                                <label id="previewtitle1" runat="server" for="input-small" class=" form-control-label font-headersize">Header title</label>
                            </div>
                        </div>
                        <div class="row mt-3">
                            <div class="col-md-12">
                                <div class="p-2 text-center" id="previewbody1" runat="server" style="background-color: forestgreen">
                                    <div class="row">
                                        <div class="col-md-2">                                           
                                            <asp:Image ID="previewimgEmp21" runat="server" />                                           
                                        </div>
                                        <div class="col-md-10 text-left">
                                            <div class="row">
                                                <label id="previewtxt1" runat="server" for="input-small" class=" form-control-label font-detailsize">ชื่อ นามสกุล</label>
                                            </div>
                                            <div class="row">
                                                <label id="previewtxt2" runat="server"  for="input-small" class=" form-control-label font-detailsize">ตำแหน่ง ส่วน</label>
                                            </div>
                                            <div class="row">
                                                <label id="previewtxt3" runat="server"  for="input-small" class=" form-control-label font-detailsize">ฝ่าย/สำนัก สายงาน</label>
                                            </div>
                                            <div class="row">
                                                <label id="previewtxt4" runat="server"  for="input-small" class=" form-control-label font-detailsize">อีเมล</label>
                                            </div>
                                            <div class="row">
                                                <label id="previewtxt5" runat="server"  for="input-small" class=" form-control-label font-detailsize">เบอร์ต่อ</label>
                                            </div>
                                        </div>
                                    </div>
                                    <hr style="border-top: 1px solid White;" />
                                    <div class="row">
                                        <div class="col-md-12 text-left">
                                            <label id="previewtxt6" runat="server" for="input-small" class=" form-control-label font-detailsize2">กรุณาระบุสิ่งที่ท่านคาดหวังจากโครงการนี้</label>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12 text-left">
                                            <asp:TextBox ID="tbAttendExpected" runat="server" class="form-control" TextMode="MultiLine" Height="100"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-12 text-center">
                                        <div class="p-2 text-center">
                                            <asp:CheckBox ID="cbConfrim" runat="server" class="form-check-input" />
                                            <label id="previewtxt7" runat="server" for="input-small" class=" form-control-label font-detailsize2">ยืนยันการสมัครของท่าน</label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row mt-2">
                            <div class="col-md-12 text-center">
                              <%--  <button type="button" class="btn btn-primary font-buttonsize">ลงทะเบียนสมัครเข้าร่วมโครงการ</button>
                                <button type="button" class="btn btn-primary font-buttonsize">ยกเลิกการทำรายการ</button>--%>
                                  <asp:Button ID="btRegister" runat="server" Text="ลงทะเบียนสมัครเข้าร่วมโครงการ" CssClass="btn btn-primary font-buttonsize" />
                                <asp:Button ID="btClose" runat="server" Text="ยกเลิกการทำรายการ" CssClass="btn btn-primary font-buttonsize" Visible="false" />
                            </div>
                        </div>
                    </div>
                    <%--<div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    </div>--%>
                </div>
            </form>
       <%-- </div>--%>
    </asp:Panel>
</asp:Content>
