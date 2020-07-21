<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MainLayout.Master" CodeBehind="AdminDetail.aspx.vb" Inherits="CRSWeb.AdminDetail" %>
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
        <div>
            <div>
                <div class="card border-primary mb-3">
                    <div class="col-10">
                        <div class="row form-group"></div>
                        <div class="row form-group">
                            <div></div>
                            <div class="col col-sm-2 pr-0">
                                <label for="input-small" class=" form-control-label">Code</label>
                                <label for="input-small" class="color-red">*</label>
                            </div>
                            <div class="col col-sm-6">
                                <%-- <input type="text" id="input-small" name="input-small" placeholder="ชื่อโครงการ" class="input-sm form-control-sm form-control">--%>
                                <asp:TextBox ID="tbEmpCode" runat="server" CssClass="input-sm form-control-sm form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row form-group">
                            <div class="col col-sm-2 pr-0">
                                <label for="input-small" class=" form-control-label">User Login</label>
                                <label for="input-small" class="color-red">*</label>
                            </div>
                            <div class="col col-sm-6">
                                <%-- <input type="text" id="input-small" name="input-small" placeholder="ชื่อโครงการ" class="input-sm form-control-sm form-control">--%>
                                <asp:TextBox ID="tbUserName" runat="server" CssClass="input-sm form-control-sm form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row form-group">
                            <div class="col col-sm-2 pr-0">
                                <label for="input-small" class=" form-control-label">Name</label>
                            </div>
                            <div class="col col-sm-6">
                                <%-- <input type="text" id="input-small" name="input-small" placeholder="ชื่อโครงการ" class="input-sm form-control-sm form-control">--%>
                                <asp:TextBox ID="tbFullName" runat="server" CssClass="input-sm form-control-sm form-control" Enabled="False" ReadOnly="True"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row form-group">
                            <div class="col col-sm-2 pr-0">
                                <label for="input-small" class=" form-control-label">Phone</label>
                            </div>
                            <div class="col col-sm-6">
                                <%-- <input type="text" id="input-small" name="input-small" placeholder="ชื่อโครงการ" class="input-sm form-control-sm form-control">--%>
                                <asp:TextBox ID="tbPhone" runat="server" CssClass="input-sm form-control-sm form-control" Enabled="False"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row form-group">
                            <div class="col col-sm-2 pr-0">
                                <label for="input-small" class=" form-control-label">Email</label>
                            </div>
                            <div class="col col-sm-6">
                                <%-- <input type="text" id="input-small" name="input-small" placeholder="ชื่อโครงการ" class="input-sm form-control-sm form-control">--%>
                                <asp:TextBox ID="tbMail" runat="server" CssClass="input-sm form-control-sm form-control" Enabled="False"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row form-group">
                            <div class="col-2">
                                <asp:Label ID="Label4" runat="server" Text="สถานะ"></asp:Label>
                                <label for="input-small" class="color-red">*</label>
                            </div>
                            <div class="col-md-3">
                                <div class="form-check-inline col-form-label">
                                    <asp:RadioButtonList ID="rdActiveFlag" runat="server" RepeatDirection="Horizontal" class="form-check-input">
                                        <asp:ListItem Value="1" Selected="True">&nbsp;ใช้งาน&nbsp;</asp:ListItem>
                                        <asp:ListItem Value="0">&nbsp;ไม่ใช้งาน&nbsp;</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
                <div class="card-footer d-flex justify-content-between">
                    <div class="row form-group ">
                        <div class="col col-sm-7">
                        </div>
                        <div>
                            <asp:Button ID="btInsert" runat="server" Text=" Save " CssClass="btn btn-info" />
                            <asp:Button ID="btCancel" runat="server" Text="Cancel" CssClass="btn btn-secondary" />
                        </div>
                    </div>
                </div>
            </div>
            <asp:HiddenField ID="hfUserId" runat="server" Value="0" />
        </div>
    </asp:Panel>
</asp:Content>
