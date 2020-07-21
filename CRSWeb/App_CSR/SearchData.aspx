<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/EmployeeLayout.Master" CodeBehind="SearchData.aspx.vb" Inherits="CRSWeb.SearchData" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="Panel1" runat="server">
       <%-- <div class="login-form">--%>
            <form runat="server">
                <div class="modal-content">
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-12">
                                <label id="previewtitle" runat="server" for="input-small" class=" form-control-label font-headersize">รายชื่อ</label>
                            </div>
                        </div>
                        <div class="row mt-1">
                            <div class="col-md-12">
                                <div class="p-2 text-center" id="previewbody" runat="server">
                                    <asp:GridView ID="GvEmployeeList" runat="server" CssClass="table table-bordered table-hover m-0 w-100 dataTable no-footer" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" AllowPaging="True" AllowSorting="True" EmptyDataText="ไม่พบข้อมูล">
                                        <Columns>
                                             <asp:TemplateField ShowHeader="True">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="Selected" runat="server" CausesValidation="False" CommandName="Select" Text="Select" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>"></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle Font-Underline="True" />
                                                </asp:TemplateField>
                                            <asp:BoundField DataField="empcode" HeaderText="รหัสพนักงาน">
                                                <HeaderStyle Font-Italic="False" Font-Underline="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="fullname"  HeaderText="ชื่อพนักงาน">
                                                <HeaderStyle Font-Underline="False" />
                                            </asp:BoundField>                                           
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
                        <div class="row mt-2 text-center">
                            <div class="col-md-3 text-center"></div>
                            <div class="col-md-7 text-center">
                                <%--<button type="button" class="btn btn-primary font-buttonsize"></button>--%>
                                <asp:Button ID="btRegister" runat="server" Text="เลือก" CssClass="btn btn-primary font-buttonsize" Visible="False" />
                                <asp:Button ID="btback" runat="server" Text="กลับหน้าหลัก" CssClass="btn btn-primary font-buttonsize" />
                            </div>
                            <div class="col-md-2 text-center"></div>
                        </div>
                    </div>
                </div>
            </form>
       <%-- </div>--%>
    </asp:Panel>
</asp:Content>
