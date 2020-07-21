<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MainLayout.Master" CodeBehind="Report.aspx.vb" Inherits="CRSWeb.Report" EnableEventValidation="false" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692FBEA5521E1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        window.$ = jQuery
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

        function alertMessage(type = "NoData", title = "", desc = "ไม่พบข้อมูล") {
            Swal.fire(
                title,
                desc,
                type
            );
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

        function popup(url, name, windowWidth, windowHeight) {
            myleft = (screen.width) ? (screen.width - windowWidth) / 2 : 100;
            mytop = (screen.height) ? (screen.height - windowHeight) / 2 : 100;
            properties = "width=" + windowWidth + ",height=" + windowHeight;
            properties += ",scrollbars=yes, top=" + mytop + ",left=" + myleft;
            window.open(url, name, properties);
        }

    </script>

    <asp:Panel ID="Panel1" runat="server">

        <div class="row">
        </div>

        <div class="card border-primary mb-3">
            <%--<div class="card-header">Search</div>--%>
            <div class="card-body text-primary">
                <div class="row align-items-start pb-2">
                    <div class="col col-sm-2">
                        <asp:Label ID="Label1" runat="server" Text="ชื่อโครงการ"></asp:Label>
                    </div>
                    <div class="col-md-3">
                        <%--<asp:TextBox ID="txtProjectName" runat="server" CssClass="form-control"></asp:TextBox>--%>
                        <asp:DropDownList ID="ddlProject" runat="server" class="form-control" Width="450px"></asp:DropDownList>
                    </div>
                </div>
                <div class="row align-items-start">
                    <div class="col col-sm-2">
                        <asp:Label ID="Label2" runat="server" Text="ระยะเวลาโครงการ"></asp:Label>
                    </div>
                    <div class="col-md-2">
                        <asp:TextBox ID="txtDateFr" runat="server" CssClass="datepicker" />

                    </div>
                    <div>
                        <asp:Label ID="Label3" runat="server" Text="ถึง"></asp:Label>
                    </div>
                    <div class="col-md-2">
                        <asp:TextBox ID="txtDateTo" runat="server" CssClass="datepicker2" />
                    </div>

                    <div class="col-3">
                        <asp:Button ID="btsearch" runat="server" Text="Search" CssClass="btn btn-success" />
                    </div>
                </div>
                <div class="row align-items-start pb-2"></div>
                <div class="row align-items-start pb-2">
                    <div class="col col-sm-2">
                        <asp:Label ID="Label4" runat="server" Text="รูปแบบรายงาน"></asp:Label>
                    </div>
                    <div class="col-md-3">
                        <div class="row form-group">
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="PDF" runat="server" Text="Export PDF" CssClass="btn btn-info" />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="Excel" runat="server" Text="Export Excel" CssClass="btn btn-info" />
                            &nbsp;
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <%-- <div class="row form-group ">
                        <div class="col col-sm-4">
                        </div>
                        <div class="col-3">
                            <asp:Button ID="btsearch" runat="server" Text="search" CssClass="btn btn-success" />
                        </div>
                    </div>--%>
        <div class="card mb-4">
            <div class="card-body">
                <div class="table-responsive">
                    <asp:Panel ID="Panel2" runat="server" ScrollBars="Auto" Height="600px" Width="1360px">
                        <asp:GridView ID="GvProjectList" runat="server" CssClass="table table-bordered table-hover m-0 w-100 dataTable no-footer" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" AllowPaging="True" AllowSorting="True">
                            <%--                                <AlternatingRowStyle BackColor="White" />--%>
                            <Columns>
                                <asp:TemplateField HeaderText="No.">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                        <asp:Label ID="lbProjectId" runat="server" Text='<%# Eval("ProjectId") %>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="60px" />
                                    <HeaderStyle Font-Size="Small" HorizontalAlign="Center" CssClass="custom-th" />
                                    <ItemStyle HorizontalAlign="Center" Font-Size="Small" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="ProjectName" HeaderText="ชื่อโครงการ" SortExpression="ProjectName">
                                    <HeaderStyle Font-Italic="False" Font-Underline="True" Font-Size="Small" HorizontalAlign="Center" CssClass="custom-th" />
                                    <ItemStyle Font-Size="Small" />
                                </asp:BoundField>

                                <asp:BoundField DataField="CreateDate" HeaderText="วันที่สมัคร" DataFormatString="{0:dd/MM/yyyy}" SortExpression="CreateDate">
                                    <HeaderStyle Font-Italic="False" Font-Underline="True" Font-Size="Small" HorizontalAlign="Center" CssClass="custom-th" />
                                    <ItemStyle Font-Size="Small" />
                                </asp:BoundField>
                                <asp:BoundField DataField="FullName" HeaderText="ชื่อ-นามสกุล">
                                    <HeaderStyle Font-Italic="False" Font-Underline="True" Font-Size="Small" HorizontalAlign="Center" CssClass="custom-th" />
                                    <ItemStyle Font-Size="Small" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Position" HeaderText="ตำแหน่ง">
                                    <HeaderStyle Font-Italic="False" Font-Underline="True" Font-Size="Small" HorizontalAlign="Center" CssClass="custom-th" />
                                    <ItemStyle Font-Size="Small" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Section" HeaderText="ส่วน">
                                    <HeaderStyle Font-Italic="False" Font-Underline="True" Font-Size="Small" HorizontalAlign="Center" CssClass="custom-th" />
                                    <ItemStyle Font-Size="Small" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Department" HeaderText="ฝ่าย/สำนักงาน">
                                    <HeaderStyle Font-Italic="False" Font-Underline="True" Font-Size="Small" HorizontalAlign="Center" CssClass="custom-th" />
                                    <ItemStyle Font-Size="Small" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Division" HeaderText="สายงาน">
                                    <HeaderStyle Font-Italic="False" Font-Underline="True" Font-Size="Small" HorizontalAlign="Center" CssClass="custom-th" />
                                    <ItemStyle Font-Size="Small" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Phone" HeaderText="เบอร์โทร">
                                    <HeaderStyle Font-Italic="False" Font-Underline="True" Font-Size="Small" HorizontalAlign="Center" CssClass="custom-th" />
                                    <ItemStyle Font-Size="Small" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="รูป">
                                    <ItemTemplate>
                                        <asp:Image ID="lbPhoto" runat="server"></asp:Image>

                                    </ItemTemplate>
                                    <ItemStyle Width="80px" />
                                    <HeaderStyle HorizontalAlign="Center" CssClass="custom-th" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <%--<asp:BoundField DataField="PictureURL" HeaderText="รูป" SortExpression="PictureURL">
                                                <HeaderStyle Font-Italic="False" Font-Underline="True" />
                                            </asp:BoundField>--%>
                                <asp:BoundField DataField="AttendExpected" HeaderText="สิ่งที่คาดหวังจากการเข้าร่วมโครงการ">
                                    <HeaderStyle Font-Italic="False" Font-Underline="True" Font-Size="Small" HorizontalAlign="Center" />
                                    <ItemStyle Font-Size="Small" />
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
                    </asp:Panel>
                </div>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
