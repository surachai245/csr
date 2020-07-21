<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MainLayout.Master" CodeBehind="AdminList.aspx.vb" Inherits="CRSWeb.AdminList" %>
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
            <div class="card border-primary mb-3">
                <div class="card mb-4">
                    <div class="card-body">
                        <div class="row form-group">
                            <div class="col-sm-12 col-md-8 "></div>
                            <div class="col-sm-12 col-md-4 ">
                                <div class="input-group">
                                    <%--<input type="text" id="input1-group2" name="input1-group2" placeholder="Username" class="form-control">--%>
                                    <asp:TextBox ID="tbSearch" runat="server" CssClass="form-control" placeholder="รหัส , ชื่อ , เบอร์"></asp:TextBox>
                                    <div class="input-group-btn">
                                         <asp:Button ID="btSharch" runat="server" Text="Search" CssClass=" btn btn-secondary"/>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="card-header">
                            <div class="row">
                                <div class="col-11">
                                    <i class="fa fa-table"></i>&nbsp;Admin 
                                </div>
                                <div >
                                    <asp:Button ID="btInsert" runat="server" Text="Create" CssClass="btn btn-info" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="table-responsive">
                            <%--<asp:PlaceHolder ID = "PlaceHolder1" runat="server" />--%>
                            <asp:GridView ID="GvAdmin" runat="server" CssClass="table table-bordered table-hover m-0 w-100 dataTable no-footer" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" AllowPaging="True" AllowSorting="True" >
                                <Columns>
                                    <asp:TemplateField HeaderText="No.">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                            <asp:Label ID="lbUserId" runat="server" Text='<%# Eval("UserId") %>' Visible="false" ></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="60px" />
                                        <HeaderStyle HorizontalAlign="Center" CssClass="custom-th" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Empcode" HeaderText="Code">
                                        <HeaderStyle Font-Italic="False" Font-Underline="False" HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center"  />
                                          <HeaderStyle HorizontalAlign="Center" CssClass="custom-th" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="FullName" HeaderText="Name">
                                        <HeaderStyle Font-Underline="False" HorizontalAlign="Center" CssClass="custom-th"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Username" HeaderText="User Login">
                                        <HeaderStyle Font-Underline="False" HorizontalAlign="Center" CssClass="custom-th" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Phone" HeaderText="Phone">
                                        <HeaderStyle Font-Underline="False" HorizontalAlign="Center" CssClass="custom-th" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                     <asp:TemplateField HeaderText="สถานะ" SortExpression="ActiveFlag">
                                        <ItemTemplate>
                                            <asp:Panel ID="plsuccess" runat="server">
                                                <i class="fa fa-check text-success" style="font-size: 25px"></i>
                                            </asp:Panel>
                                            <asp:Panel ID="pldanger" runat="server">
                                                <i class="fa fa-times text-danger" style="font-size: 25px"></i>
                                            </asp:Panel>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" CssClass="custom-th" Font-Underline="False" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="แก้ไข">
                                        <ItemTemplate>
                                            <%--<button type="button" class="btn btn-outline-warning btn-sm">Edit</button>--%>
                                        <asp:Button ID="btEdit" runat="server" Text="Edit" class="btn btn-outline-warning btn-sm" OnClick="btEdit_Click"/>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" CssClass="custom-th" Width="80px" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ลบ">
                                        <ItemTemplate>
                                            <asp:Button ID="btDelete" runat="server" Text="Delete" class="btn btn-outline-danger btn-sm" OnClick="btDelete_Click" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" CssClass="custom-th" />
                                        <ItemStyle HorizontalAlign="Center" Width="80px" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                <HeaderStyle BackColor="#17a2b8" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#f1ede2" ForeColor="Black" HorizontalAlign="Right" CssClass="table-footer" />
                                <RowStyle ForeColor="Black" />
                                <%--<SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White"  />--%>
                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                <SortedAscendingHeaderStyle BackColor="#0000A9" />
                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                <SortedDescendingHeaderStyle BackColor="#000065" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
