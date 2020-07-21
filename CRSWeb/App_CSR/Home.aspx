<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MainLayout.Master" CodeBehind="Home.aspx.vb" Inherits="CRSWeb.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">

        window.onload = function () {
            createLoadingDialog();
            try {
                document.getElementById('focus').focus();
            } catch (ex) {

            }
        }
        function setLoading(inputType) {
            var input = $(":" + inputType);
            input.each(function (index, value) {
                input[index].onmouseup = function () { $("#dialog").dialog("open"); }
            });
        }
        function BeginRequestHandler(sender, args) {
            $("#dialog").dialog("open");
        }
        function EndRequestHandler(sender, args) {
            $("#dialog").dialog("close");
        }
        function createLoadingDialog() {
            $("#dialog").dialog({
                autoOpen: false,
                modal: true,
                width: "auto"
            });
            $(".ui-dialog-titlebar").hide();
        }
        function popupdialog() {
            $("#popup").dialog({
                autoOpen: true,
                modal: true,
                width: "300px"
            });

        }
        jQuery(document).ready(function () {
            $(".datepicker").datepicker({
                uiLibrary: 'bootstrap4',
                format: 'dd/mm/yyyy'

            });
            $(".datepicker2").datepicker({
                uiLibrary: 'bootstrap4',
                format: 'dd/mm/yyyy'

            });
        });


        function ConfirmDelete() {
            var x = confirm("Are you sure you want to delete?");
            if (x)
                return true;
            else
                return false;
        }

        jQuery(document).ready(function () {
            $("#<%= txtDateFr.ClientID %>").keyup(function (e) {
                //console.log($(this).val(),e);
                var t = $(this).val();
                $(this).val(t.replace(e.key, ''));
            });
            $("#<%= txtDateTo.ClientID %>").keyup(function (e) {
                var t = $(this).val();
                $(this).val(t.replace(e.key, ''));
            });
        });

        function openModal() {
            $('#smallmodal').modal();
        }
    </script>
    <asp:Panel ID="Panel1" runat="server">
        <div>
            <div class="row">
            </div>
            <div class="card border-primary mb-3">
                <%--<div class="card-header">Search</div>--%>
                <div class="card-body text-primary">
                    <div class="row align-items-start pb-2">
                        <div class="col-md-2 pr-0">
                            <asp:Label ID="Label1" runat="server" Text="ชื่อโครงการ"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtProjectName" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row align-items-start">
                        <div class="col-md-2 pr-0">
                            <asp:Label ID="Label2" runat="server" Text="ระยะเวลาโครงการ"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtDateFr" runat="server" CssClass="datepicker" />

                        </div>
                        <div>
                            <asp:Label ID="Label3" runat="server" Text="ถึง"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtDateTo" runat="server" CssClass="datepicker2" />
                        </div>

                        <div class="col-3">
                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-success" />
                        </div>
                    </div>
                    <div class="row align-items-start pb-2">
                        <div class="col-2">
                            <asp:Label ID="Label4" runat="server" Text="สถานะ"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <div class="form-check-inline col-form-label">
                                <asp:RadioButtonList ID="rdActiveFlag" runat="server" RepeatDirection="Horizontal" class="form-check-input">
                                    <asp:ListItem Value="1">&nbsp;ใช้งาน&nbsp;</asp:ListItem>
                                    <asp:ListItem Value="0">&nbsp;ไม่ใช้งาน&nbsp;</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="card mb-4">
                <div class="card-body">
                    <div class="card-header">
                        <div class="row">
                            <div class="col-6">
                                <i class="fa fa-table"></i>&nbsp;Project 
                            </div>
                            <div class="col-6 text-right">
                                <asp:Button ID="btInsert" runat="server" Text="Create" CssClass="btn btn-info" />
                            </div>
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="table-responsive">
                            <%--<asp:PlaceHolder ID = "PlaceHolder1" runat="server" />--%>
                            <asp:GridView ID="GvProjectList" runat="server" CssClass="table table-bordered table-hover m-0 w-100 dataTable no-footer" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" AllowPaging="True" AllowSorting="True" PageSize="15">
                                <%--                                <AlternatingRowStyle BackColor="White" />--%>
                                <Columns>
                                    <asp:TemplateField HeaderText="No.">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                        <ItemStyle Width="60px" />
                                        <HeaderStyle HorizontalAlign="Center" CssClass="custom-th" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="ProjectName" HeaderText="ชื่อโครงการ" SortExpression="ProjectName">
                                        <HeaderStyle Font-Italic="False" Font-Underline="True" HorizontalAlign="Center" CssClass="custom-th" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DateFr" DataFormatString="{0:dd/MM/yyyy}" HeaderText="วันเริ่มต้น" SortExpression="DateFr">
                                        <HeaderStyle Font-Underline="True" HorizontalAlign="Center" CssClass="custom-th" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DateTo" DataFormatString="{0:dd/MM/yyyy}" HeaderText="วันสิ้นสุด" SortExpression="DateTo">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle Font-Underline="True" HorizontalAlign="Center" CssClass="custom-th" />
                                    </asp:BoundField>

                                    <asp:BoundField DataField="UserNameCreate" HeaderText="ผู้สร้างโครงการ" SortExpression="UserNameCreate">
                                         <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle Font-Italic="False" Font-Underline="True" HorizontalAlign="Center" CssClass="custom-th" />
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
                                        <ItemStyle HorizontalAlign="Center" CssClass="custom-th" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="แก้ไข">
                                        <ItemTemplate>
                                            <%--<button type="button" class="btn btn-outline-warning btn-sm">Edit</button>--%>
                                            <asp:Button ID="btEdit" runat="server" Text="Edit" class="btn btn-outline-warning btn-sm" OnClick="btEdit_Click" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" CssClass="custom-th" Width="80px" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ลบ">
                                        <ItemTemplate>
                                            <%--<button type="button" class="btn btn-outline-danger btn-sm">Delete</button>--%>
                                            <%-- <asp:Button ID="btDelete" runat="server" Text="Delete" class="btn btn-outline-danger btn-sm" OnClick="btDelete_Click" />
                                            <br />--%>
                                            <%--<asp:Button ID="Button1" runat="server" Text="Delete"  class="btn btn-outline-danger btn-sm" data-toggle="modal" data-target="#smallmodal" />--%>
                                            <%--<button type="button" id="btdelete" runat="server"  class="btn btn-outline-danger btn-sm" data-toggle="modal" data-target="#smallmodal">Delete</button>--%>
                                            <asp:Button ID="btdelete" data-toggle="modal" CommandName="Select" UseSubmitBehavior="false" runat="server" class="btn btn-outline-danger btn-sm" Text="Delete" />
                                            <asp:Label ID="lbProjectId" runat="server" Text='<%# Eval("ProjectId") %>' Visible="false"></asp:Label>
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
                    <div style="height: 60vh;"></div>
                    <%--<div class="card mb-4"><div class="card-body">When scrolling, the navigation stays at the top of the page. This is the end of the static navigation demo.</div></div>
                                </div>--%>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hdfNotifyInterval" runat="server" Value="1500" />
        <asp:HiddenField ID="hdfNodeGrpID" runat="server" Value="0" />
        <asp:HiddenField ID="hdfBranchID" runat="server" Value="0" />
        <asp:HiddenField ID="hdfProjectId_Modal" runat="server" Value="0" />
        <asp:HiddenField ID="hdfSuccess_Modal" runat="server" Value="0" />
    </asp:Panel>
    <div class="modal fade" id="smallmodal" tabindex="-1" role="dialog" aria-labelledby="smallmodalLabel" aria-hidden="true">
        <div class="modal-dialog modal-sm" role="document">
            <div class="modal-content">
                <%-- <div class="modal-header">
                            <h5 class="modal-title" id="smallmodalLabel">Small Modal</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>--%>
                <div class="modal-body">
                    <p>
                        ต้องการลบข้อมูลใช่หรือไม่
                           
                    </p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancel</button>
                    <asp:Button ID="btDelete1" runat="server" Text="Confirm" class="btn btn-primary" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
