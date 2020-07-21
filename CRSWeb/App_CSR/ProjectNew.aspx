<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MainLayout.Master" CodeBehind="ProjectNew.aspx.vb" Inherits="CRSWeb.ProjectNew" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        window.$ = jQuery
        window.onload = function () {
            //createLoadingDialog();
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
        //PopupMessage : "CAllA", "alertMessage();"
        function alertMessage(type = "success", title = "", desc = "บันทึกข้อมูลเรียบร้อย") {
            Swal.fire(
                title,
                desc,
                type
            );
        }


        /* Strat : ข้อมูลการแสดงเมื่อกดปุ่ม preview ตั้ง id ให้ตรง * */
        function openModal() {
            var tabIndex = $("#<%= hdnSelectedTab.ClientID %>").val();
            var currentId = '#preview' + tabIndex;
            $(currentId).modal();
            previewTab(tabIndex, currentId);
        }
        function previewTab(tabIndex, tabId) {
            if (tabIndex == '0') {
                var title = $('#<%= tbProjectName_tab1.ClientID %>').val();
                var font = $('#<%= tbFontName_tab1.ClientID %>').val();
                var color = $('#<%= tbColor_tab1.ClientID %>').val();
                var bgcolor = $('#<%= tbBackgroud_tab1.ClientID %>').val();
                var Filename = $('#<%= hfFileName.ClientID %>').val();
                //console.log(Filename)
                font = font.split("+").join(" ");
                $(tabId + ' #preview-title').text(title);
                $(tabId + ' #preview-title').css('color', color);
                $(tabId + ' #preview-title').css("font-family", font);
                $(tabId + ' #preview-body').css("background-color", bgcolor);
                if (Filename != '' && Filename != '0') {
                    if (Filename.indexOf("base64") == -1) {
                        $(tabId + ' #preview-img').attr({ 'src': 'Document/temp/' + Filename });
                    }
                    else {
                        $(tabId + ' #preview-img').attr({ 'src': Filename });
                    }
                }
            }
            if (tabIndex == '1') {
                /*Header*/
                var title = $('#<%= tbProjectName_tab1.ClientID %>').val();
                var font = $('#<%= tbFontName_tab1.ClientID %>').val();
                var color = $('#<%= tbColor_tab1.ClientID %>').val();
                var colorfont = $('#<%= tbFontColor_tab2.ClientID %>').val();
                var bgcolor = $('#<%= tbBackgroud_tab2.ClientID %>').val();

                font = font.split("+").join(" ");
                $(tabId + ' #preview-title1').css("font-family", font);
                $(tabId + ' #preview-title1').css('color', color);
                $(tabId + ' #preview-title1').text(title);
                $(tabId + ' #preview-body1').css("background-color", bgcolor);
                $(tabId + ' #preview-txt1').css('color', colorfont);
                $(tabId + ' #preview-txt2').css('color', colorfont);
                $(tabId + ' #preview-txt3').css('color', colorfont);
                $(tabId + ' #preview-txt4').css('color', colorfont);
                $(tabId + ' #preview-txt5').css('color', colorfont);
                $(tabId + ' #preview-txt6').css('color', colorfont);
                $(tabId + ' #preview-txt7').css('color', colorfont);
                //preview - imgtxt1
            }

            if (tabIndex == '2') {
                /*Header*/
                var title = $('#<%= tbProjectName_tab1.ClientID %>').val();
                var font = $('#<%= tbFontName_tab1.ClientID %>').val();
                var color = $('#<%= tbColor_tab1.ClientID %>').val();

                var bgcolor = $('#<%= tbBackgroud_tab3.ClientID %>').val();
                var colorfont = $('#<%= tbFontColor_tab3.ClientID %>').val();
                var Filename = $('#<%= hfFileName3.ClientID %>').val();
                var message = $('#<%= tbTextMessage_tab3.ClientID %>').val();
                var font3 = $('#<%= tbTextFontName_tab3.ClientID %>').val();
                var color3 = $('#<%= tbTextFontColor_tab3.ClientID %>').val();

                //$.merge($.merge([], message), second);

                // var plainText = HtmlUtilities.ConvertToPlainText(message);

                //console.log(message)
                font = font.split("+").join(" ");
                $(tabId + ' #preview-title2').text(title);
                $(tabId + ' #preview-title2').css('color', color);
                $(tabId + ' #preview-title2').css("font-family", font);
                $(tabId + ' #preview-body2').css("background-color", bgcolor);
                $(tabId + ' #preview-txt8').css('color', colorfont);
                $(tabId + ' #preview-txt9').css('color', colorfont);
                $(tabId + ' #preview-txt10').css('color', colorfont);
                $(tabId + ' #preview-txt11').css('color', colorfont);
                $(tabId + ' #preview-txt12').css('color', colorfont);
                $(tabId + ' #preview-txt13').css('color', colorfont);
                $(tabId + ' #preview-detail').html(message);
                $(tabId + ' #preview-detail').css('color', color3);
                //font3 = font3.replace('+', ' ');
                font3 = font3.split("+").join(" ");
                $(tabId + ' #preview-detail').css("font-family", font3);

                if (Filename != '' && Filename != '0') {
                    if (Filename.indexOf("base64") == -1) {
                        $(tabId + ' #preview-img2').attr({ 'src': 'Document/temp/' + Filename });
                    }
                    else {
                        $(tabId + ' #preview-img2').attr({ 'src': Filename });
                    }
                }
            }
            if (tabIndex == '3') {
                /*Header*/
                var title = $('#<%= tbProjectName_tab1.ClientID %>').val();
                var font = $('#<%= tbFontName_tab1.ClientID %>').val();
                var color = $('#<%= tbColor_tab1.ClientID %>').val();
                var bgcolor = $('#<%= tbBackgroud_tab4.ClientID %>').val();
                var message = $('#<%= tbTextMessage_tab4.ClientID %>').val();
                var font4 = $('#<%= tbTextFontName_tab4.ClientID %>').val();
                var color4 = $('#<%= tbTextFontColor_tab4.ClientID %>').val();
                var Filename = $('#<%= hfFileName4.ClientID %>').val();

                font = font.split("+").join(" ");
                font4 = font4.split("+").join(" ");
                $(tabId + ' #preview-title3').text(title);
                $(tabId + ' #preview-title3').css('color', color);
                $(tabId + ' #preview-title3').css("font-family", font);
                $(tabId + ' #preview-body3').css("background-color", bgcolor);
                $(tabId + ' #preview-detail2').text(message);
                $(tabId + ' #preview-detail2').css('color', color4);
                $(tabId + ' #preview-detail2').css("font-family", font4);

                //if (Filename != '' && Filename != '0') {
                //    $(tabId + ' #preview-img3').attr({ 'src': 'Document/temp/' + Filename });
                //}
                if (Filename != '' && Filename != '0') {
                    if (Filename.indexOf("base64") == -1) {
                        $(tabId + ' #preview-img3').attr({ 'src': 'Document/temp/' + Filename });
                    }
                    else {
                        $(tabId + ' #preview-img3').attr({ 'src': Filename });
                    }
                }
            }
            if (tabIndex == '4') {
                /*Header*/
                var title = $('#<%= tbProjectName_tab1.ClientID %>').val();
                var font = $('#<%= tbFontName_tab1.ClientID %>').val();
                var color = $('#<%= tbColor_tab1.ClientID %>').val();
                var colorfont = $('#<%= tbFontColor_tab5.ClientID %>').val();
                var bgcolor = $('#<%= tbBackgroud_tab5.ClientID %>').val();

                font = font.split("+").join(" ");
                $(tabId + ' #preview-title4').text(title);
                $(tabId + ' #preview-title4').css('color', color);
                $(tabId + ' #preview-title4').css("font-family", font);
                $(tabId + ' #preview-body4').css("background-color", bgcolor);
                $(tabId + ' #preview-detail4').css('color', colorfont);
                $(tabId + ' #preview-detail4').css('color', colorfont);
                $(tabId + ' #preview-txt14').css('color', colorfont);
                $(tabId + ' #preview-txt15').css('color', colorfont);
                $(tabId + ' #preview-txt16').css('color', colorfont);
                $(tabId + ' #preview-txt17').css('color', colorfont);
            }

            if (tabIndex == '5') {
                var bgcolor = $('#<%= tbBackgroud_tab6.ClientID %>').val();
                var message = $('#<%= tbTextMessage_tab6.ClientID %>').val();
                var font5 = $('#<%= tbTextFontName_tab6.ClientID %>').val();
                var color5 = $('#<%= tbFontColor_tab6.ClientID %>').val();
                font5 = font5.split("+").join(" ");

                $(tabId + ' #preview-body5').css("background-color", bgcolor);
                $(tabId + ' #preview-detail5').html(message);
                $(tabId + ' #preview-detail5').css('color', color5);
                $(tabId + ' #preview-detail5').css("font-family", font5);


            }
        }
        /* End : ข้อมูลการแสดงเมื่อกดปุ่ม preview ตั้ง id ให้ตรง *  */

        jQuery(document).ready(function () {

            /* Start :  datepicker */
            $(".datepicker").datepicker({
                uiLibrary: 'bootstrap4',
                format: 'dd/mm/yyyy'
            });
            $(".datepicker2").datepicker({
                uiLibrary: 'bootstrap4',
                format: 'dd/mm/yyyy'
            });
            $('#cp').colorpicker();
            $('#cp2').colorpicker();
            $('#cp3').colorpicker();
            $('#cp4').colorpicker();
            $('#cp5').colorpicker();
            $('#cp6').colorpicker();
            $('#cp7').colorpicker();
            $('#cp8').colorpicker();
            $('#cp9').colorpicker();
            $('#cp10').colorpicker();
            $('#cp11').colorpicker();
            $('#cp12').colorpicker();
            $('#cp13').colorpicker();

            /* End :  datepicker */

            /* Start : แสดง tabs และ assign SelectedTab ไปใช้ใน Code Behigh */
            $("#tabs").tabs()
            $('a[data-toggle="tab"]').on('show.bs.tab', function (e) {
                $("#<%= hdnSelectedTab.ClientID %>").val($(e.target).data('tab-index'));
            })
            if ($("#<%= hdnSelectedTab.ClientID %>").val() != "") {
                var tabindex = parseInt($("#<%= hdnSelectedTab.ClientID %>").val()) + 1
                //console.log(tabindex)
                $('#tab' + tabindex).click() /*แก้ปัญหากดบันทึกแล้วเด้ง tab แรก*/
                //$("#tabs").tabs("option", "active", $("#<%= hdnSelectedTab.ClientID %>").val());
            }
            /* End : แสดง tabs และ assign SelectedTab ไปใช้ใน Code Behigh */

            var fontOption = {
                systemFonts: ['Arial', 'Helvetica+Neue', 'Courier+New', 'Times+New+Roman', 'Comic+Sans+MS', 'Verdana', 'Impact', 'Tomaha'],
                localFonts: ['LHBankBody', 'LHBankBodyBold', 'LHBankBodyBoldItalic', 'LHBankBodyItalic', 'LHBankHandwriting', 'LHBankHeader', 'LHBankHeaderBold', 'LHBankHeaderBoldItalic', 'LHBankHeaderItalic', 'LHBankHeaderThin', 'LHBankHeaderThinItalic'],
                googleFonts: [],  /*['Abel', 'Piedra', 'Questrial', 'Ribeye']*/
                localFontsUrl: '/assets/fonts/' /*Path กับ Font ที่เอาเข้ามาในโปรเจค */
                /*กรณีใช้ Font localFontsUrl ให้เรียก Class ให้ระบบรู้จัก Font @font-face(custom.css file)*/
            }
            $('#<%=tbFontName_tab1.ClientID %>').fontselect(fontOption).on('change', function () {
                //console.log(this.value)
            });

            $('#<%=tbTextFontName_tab3.ClientID %>').fontselect(fontOption).on('change', function () {
                // console.log(this.value)
            });

            $('#<%=tbTextFontName_tab4.ClientID %>').fontselect(fontOption).on('change', function () {
                // console.log(this.value)
            });

            $('#<%=tbTextFontName_tab6.ClientID %>').fontselect(fontOption).on('change', function () {
                // console.log(this.value)
            });


            $("#<%= tbDateFr_tab1.ClientID %>").keyup(function (e) {
                //console.log($(this).val(),e);
                var t = $(this).val();
                $(this).val(t.replace(e.key, ''));
            });
            $("#<%= tbDateTo_tab1.ClientID %>").keyup(function (e) {
                var t = $(this).val();
                $(this).val(t.replace(e.key, ''));
            });

            $('#<%=tbTextMessage_tab3.ClientID %>').summernote({
                toolbar: [
                    // [groupName, [list of button]]
                    ['style', ['bold', 'italic', 'underline', 'clear']],
                    //['font', ['strikethrough', 'superscript', 'subscript']],
                    ['fontsize', ['fontsize']],
                    //['color', ['color']],
                    ['para', ['ul', 'ol', 'paragraph']],
                    ['height', ['height']]
                ]
            });

            $('#<%=tbTextMessage_tab6.ClientID %>').summernote({
                toolbar: [
                    // [groupName, [list of button]]
                    ['style', ['bold', 'italic', 'underline', 'clear']],
                    //['font', ['strikethrough', 'superscript', 'subscript']],
                    ['fontsize', ['fontsize']],
                    //['color', ['color']],
                    ['para', ['ul', 'ol', 'paragraph']],
                    ['height', ['height']]
                ]
            });
        });
    </script>
    <asp:Panel ID="Panel1" runat="server">
        <asp:HiddenField ID="hdnSelectedTab" runat="server" Value="0" />
        <div id="tabs">
            <ul class="nav nav-tabs" id="myTab" role="tablist">
                <li class="nav-item">
                    <a class="nav-link active" data-tab-index="0" id="tab1" data-toggle="tab" href="#tabs-1" role="tab" aria-controls="home" aria-selected="true">
                        <asp:Label ID="lbTab1" runat="server"></asp:Label></a>
                </li>
                <li class="nav-item">
                    <a class="nav-link" data-tab-index="1" id="tab2" data-toggle="tab" href="#tabs-2" role="tab" aria-controls="profile" aria-selected="false">
                        <asp:Label ID="lbTab2" runat="server"></asp:Label></a>
                </li>
                <li class="nav-item">
                    <a class="nav-link" data-tab-index="2" id="tab3" data-toggle="tab" href="#tabs-3" role="tab" aria-controls="contact" aria-selected="false">
                        <asp:Label ID="lbTab3" runat="server"></asp:Label></a>
                </li>
                <li class="nav-item">
                    <a class="nav-link" data-tab-index="3" id="tab4" data-toggle="tab" href="#tabs-4" role="tab" aria-controls="contact" aria-selected="false">
                        <asp:Label ID="lbTab4" runat="server"></asp:Label></a>
                </li>
                <li class="nav-item">
                    <a class="nav-link" data-tab-index="4" id="tab5" data-toggle="tab" href="#tabs-5" role="tab" aria-controls="contact" aria-selected="false">
                        <asp:Label ID="lbTab5" runat="server"></asp:Label></a>
                </li>
                <li class="nav-item">
                    <a class="nav-link" data-tab-index="5" id="tab6" data-toggle="tab" href="#tabs-6" role="tab" aria-controls="contact" aria-selected="false">
                        <asp:Label ID="lbTab6" runat="server"></asp:Label></a>
                </li>
            </ul>
            <div id="tabs-1" class="tab-pane fade show active">
                <div class="row form-group">
                    <div class="col col-sm-2 pr-0">
                        <label for="input-small" class=" form-control-label">ชื่อโครงการ</label>
                        <label for="input-small" class="color-red">*</label>
                    </div>
                    <div class="col col-sm-6">
                        <asp:TextBox ID="tbProjectName_tab1" runat="server" CssClass="input-sm form-control-sm form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="row form-group">
                    <div class="col col-sm-2 pr-0">
                        <label for="input-normal" class=" form-control-label">วันที่เริ่มต้น</label>
                        <label for="input-small" class="color-red">*</label>
                    </div>
                    <div class="col-md-3">
                        <asp:TextBox ID="tbDateFr_tab1" runat="server" CssClass="datepicker" />
                    </div>
                    <div>
                        <label for="input-normal" class=" form-control-label">วันที่สิ้นสุด </label>
                        <label for="input-small" class="color-red">*</label>
                    </div>
                    <div class="col-md-3">
                        <asp:TextBox ID="tbDateTo_tab1" runat="server" CssClass="datepicker2" />
                    </div>
                </div>
                <div class="row form-group">
                    <div class="col col-sm-2">
                        <label for="input-small" class=" form-control-label">Email Subject</label>
                        <label for="input-small" class="color-red">*</label>
                    </div>
                    <div class="col col-sm-6">
                        <asp:TextBox ID="tbSubjectMail_tab1" runat="server" CssClass="input-sm form-control-sm form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="row form-group">
                    <div class="col col-sm-2">
                        <label for="input-small" class=" form-control-label">Font ชื่อโครงการ</label>
                        <label for="input-small" class="color-red">*</label>
                    </div>
                    <div class="col-12 col-md-6">
                        <div id="font" class="input-group">
                            <asp:TextBox ID="tbFontName_tab1" runat="server" CssClass="input-sm form-control-sm form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row form-group">
                    <div class="col col-sm-2">
                        <label for="input-small" class=" form-control-label">สีชื่อโครงการ</label>
                        <label for="input-small" class="color-red">*</label>
                    </div>
                    <div class="col-12 col-md-6">
                        <div id="cp" class="input-group">
                            <asp:TextBox ID="tbColor_tab1" runat="server" CssClass="input-sm form-control-sm form-control" Text="#000000"></asp:TextBox>
                            <span class="input-group-append">
                                <span class="input-group-text colorpicker-input-addon"><i></i></span>
                            </span>
                        </div>
                    </div>
                </div>
                <div class="row form-group">
                    <div class="col col-sm-2">
                        <label for="input-small" class=" form-control-label">URL</label>
                        <label for="input-small" class="color-red">*</label>
                    </div>
                    <div class="col col-sm-6">
                        <asp:TextBox ID="tbURL" runat="server" CssClass="input-sm form-control-sm form-control" ReadOnly="True"></asp:TextBox>
                    </div>
                </div>
                <div class="row form-group">
                    <div class="col col-sm-2">
                        <label for="input-small" class=" form-control-label">สถานะ</label>
                        <label for="input-small" class="color-red">*</label>
                    </div>
                    <div class="col col-sm-6">
                        <div class="form-check-inline col-form-label">
                            <asp:RadioButtonList ID="rdActiveFlag_tab1" runat="server" RepeatDirection="Horizontal" class="form-check-input">
                                <asp:ListItem Value="1" Selected="True">&nbsp;ใช้งาน&nbsp;</asp:ListItem>
                                <asp:ListItem Value="0">&nbsp;ไม่ใช้งาน&nbsp;</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                </div>
                <hr>
                <div class="card-title">
                    <h4 class="text-left flat-color-6"><i class="fa fa-book"></i>&nbsp;ข้อมูลหน้าแรก</h4>
                </div>
                <div class="row form-group">
                    <div class="col col-sm-2">
                        <label for="input-small" class=" form-control-label">สี Background</label>
                        <label for="input-small" class="color-red">*</label>
                    </div>
                    <div class="col-12 col-md-6">
                        <div id="cp2" class="input-group">
                            <asp:TextBox ID="tbBackgroud_tab1" runat="server" CssClass="input-sm form-control-sm form-control" Text="#000000"></asp:TextBox>
                            <span class="input-group-append">
                                <span class="input-group-text colorpicker-input-addon"><i></i></span>
                            </span>
                        </div>
                    </div>
                </div>
                <div class="row form-group">
                    <div class="col col-sm-2">
                        <label for="input-small" class=" form-control-label">รูป</label>
                        <label for="input-small" class="color-red">*</label>
                    </div>
                    <%-- <input type="file" id="file-input" name="file-input" class="form-control-file">--%>
                    <div class="col col-sm-2">
                        <asp:FileUpload ID="FileUpload1" runat="server" AllowMultiple="False" EnableViewState="true" ForeColor="White" class="form-control-file" />
                    </div>
                    <div>
                        <asp:Label ID="lbPicture" runat="server" Text="รูปต้องมีขนาด (1024px * 350px)" class=" form-control-label" ForeColor="#CCCCCC"></asp:Label>
                        <asp:HiddenField ID="hfFileName" runat="server" Value="0" />
                    </div>
                    <asp:Button ID="btTmp" runat="server" OnClick="btTmp_Click" Style="display: none" Text="btTmp" />
                </div>
                <%--<div class="row form-group">
                    <div class="col col-sm-2">
                    </div>
                    <div>
                        <label for="input-small" class="color-green">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;รูปต้องมีขนาด (1024px * 350px)</label>
                    </div>
                </div>--%>
                <asp:HiddenField ID="hfIdTab1" runat="server" Value="0" />
            </div>
            <div id="tabs-2" class="tab-pane fade">
                <div class="card-title">
                    <h4 class="text-left flat-color-6"><i class="fa fa-book"></i>&nbsp;ข้อมูลหน้าลงทะเบียน</h4>
                </div>
                <div class="row form-group">
                    <div class="col col-sm-2">
                        <label for="input-small" class=" form-control-label">สี Background</label>
                        <label for="input-small" class="color-red">*</label>
                    </div>
                    <div class="col col-sm-6">
                        <div id="cp3" class="input-group">
                            <asp:TextBox ID="tbBackgroud_tab2" runat="server" CssClass="input-sm form-control-sm form-control" Text="#000000"></asp:TextBox>
                            <span class="input-group-append">
                                <span class="input-group-text colorpicker-input-addon"><i></i></span>
                            </span>
                        </div>
                    </div>
                </div>
                <div class="row form-group">
                    <div class="col col-sm-2">
                        <label for="input-small" class=" form-control-label">สี Font</label>
                        <label for="input-small" class="color-red">*</label>
                    </div>
                    <div class="col col-sm-6">
                        <div id="cp4" class="input-group">
                            <asp:TextBox ID="tbFontColor_tab2" runat="server" CssClass="input-sm form-control-sm form-control" Text="#000000"></asp:TextBox>
                            <span class="input-group-append">
                                <span class="input-group-text colorpicker-input-addon"><i></i></span>
                            </span>
                        </div>
                    </div>
                </div>
                <asp:HiddenField ID="hfIdTab2" runat="server" Value="0" />
            </div>
            <div id="tabs-3" class="tab-pane fade">
                <div class="card-title">
                    <h4 class="text-left flat-color-6"><i class="fa fa-book"></i>&nbsp;ข้อมูลหน้าขอบคุณ</h4>
                    <div class="row form-group">
                    </div>
                    <div class="row form-group">
                        <div class="col col-sm-3">
                            <label for="input-small" class=" form-control-label">สี Background</label>
                            <label for="input-small" class="color-red">*</label>
                        </div>
                        <div class="col col-sm-6">
                            <%--<asp:TextBox ID="tbBackgroud_tab3" runat="server" CssClass="input-sm form-control-sm form-control"></asp:TextBox>--%>
                            <div id="cp5" class="input-group">
                                <asp:TextBox ID="tbBackgroud_tab3" runat="server" CssClass="input-sm form-control-sm form-control" Text="#000000"></asp:TextBox>
                                <span class="input-group-append">
                                    <span class="input-group-text colorpicker-input-addon"><i></i></span>
                                </span>
                            </div>
                        </div>
                    </div>
                    <div class="row form-group">
                        <div class="col col-sm-3">
                            <label for="input-small" class=" form-control-label">สี Font</label>
                            <label for="input-small" class="color-red">*</label>
                        </div>
                        <div class="col col-sm-6">
                            <%-- <asp:TextBox ID="tbFontName_tab3" runat="server" CssClass="input-sm form-control-sm form-control"></asp:TextBox>--%>
                            <div id="cp6" class="input-group">
                                <asp:TextBox ID="tbFontColor_tab3" runat="server" CssClass="input-sm form-control-sm form-control" Text="#000000"></asp:TextBox>
                                <span class="input-group-append">
                                    <span class="input-group-text colorpicker-input-addon"><i></i></span>
                                </span>
                            </div>
                        </div>
                    </div>
                    <div class="row form-group">
                        <div class="col col-sm-3">
                            <label for="input-small" class=" form-control-label">รูป</label>
                            <label for="input-small" class="color-red">*</label>
                        </div>
                        <div class="col col-sm-2">
                            <asp:FileUpload ID="FileUpload3" runat="server" AllowMultiple="False" EnableViewState="true" ForeColor="White" class="form-control-file" />
                        </div>
                        <div>
                            <asp:Label ID="lbPicture3" runat="server" Text="No file chosen" class=" form-control-label" ForeColor="#CCCCCC"></asp:Label>
                            <asp:HiddenField ID="hfFileName3" runat="server" Value="0" />
                        </div>
                        <asp:Button ID="btTmp2" runat="server" OnClick="btTmp2_Click" Style="display: none" Text="btTmp2" />
                    </div>
                    <div class="row form-group">
                        <div class="col col-sm-3">
                            <label for="input-small" class=" form-control-label">ข้อความขอบคุณ</label>
                            <label for="input-small" class="color-red">*</label>
                        </div>
                        <div class="col col-sm-6">
                            <div>
                                <asp:TextBox ID="tbTextMessage_tab3" runat="server" class="form-control" TextMode="MultiLine"></asp:TextBox>
                                <%--<textarea name="textarea-input" id="textarea-input" rows="4" placeholder="Content..." class="form-control"></textarea>--%>
                                <%-- <textarea id="summernote" runat="server" name="editordata"></textarea>--%>
                            </div>
                        </div>
                    </div>
                    <div class="row form-group">
                        <div class="col col-sm-3">
                            <label for="input-small" class=" form-control-label">Font ข้อความขอบคุณ</label>
                            <label for="input-small" class="color-red">*</label>
                        </div>
                        <div class="col col-sm-6">
                            <%--<asp:TextBox ID="tbFontName_tab3" runat="server" CssClass="input-sm form-control-sm form-control"></asp:TextBox>--%>
                            <div id="font1" class="input-group">
                                <asp:TextBox ID="tbTextFontName_tab3" runat="server" CssClass="input-sm form-control-sm form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row form-group">
                        <div class="col col-sm-3">
                            <label for="input-small" class=" form-control-label">สี ข้อความขอบคุณ</label>
                            <label for="input-small" class="color-red">*</label>
                        </div>
                        <div class="col col-sm-6">
                            <%--<asp:TextBox ID="TextBox8" runat="server" CssClass="input-sm form-control-sm form-control"></asp:TextBox>--%>
                            <div id="cp7" class="input-group">
                                <asp:TextBox ID="tbTextFontColor_tab3" runat="server" CssClass="input-sm form-control-sm form-control" Text="#000000"></asp:TextBox>
                                <span class="input-group-append">
                                    <span class="input-group-text colorpicker-input-addon"><i></i></span>
                                </span>
                            </div>
                        </div>
                    </div>
                </div>
                <asp:HiddenField ID="hfIdTab3" runat="server" Value="0" />
            </div>
            <div id="tabs-4" class="tab-pane fade">
                <div class="card-title">
                    <h4 class="text-left flat-color-6"><i class="fa fa-book"></i>&nbsp;ข้อมูลหน้าแนะนำเพื่อน(หน้าแรก)</h4>
                </div>
                <div class="row form-group">
                    <div class="col col-sm-3">
                        <label for="input-small" class=" form-control-label">สี Background</label>
                        <label for="input-small" class="color-red">*</label>
                    </div>
                    <div class="col col-sm-6">
                        <div id="cp8" class="input-group">
                            <asp:TextBox ID="tbBackgroud_tab4" runat="server" CssClass="input-sm form-control-sm form-control" Text="#000000"></asp:TextBox>
                            <span class="input-group-append">
                                <span class="input-group-text colorpicker-input-addon"><i></i></span>
                            </span>
                        </div>
                    </div>
                </div>
                <div class="row form-group">
                    <div class="col col-sm-3">
                        <label for="input-small" class=" form-control-label">รูป</label>
                        <label for="input-small" class="color-red">*</label>
                    </div>
                    <div class="col col-sm-2">
                        <asp:FileUpload ID="FileUpload4" runat="server" AllowMultiple="False" EnableViewState="true" ForeColor="White" class="form-control-file" />
                    </div>
                    <div>
                        <asp:Label ID="lbPicture4" runat="server" Text="No file chosen" class=" form-control-label" ForeColor="#CCCCCC"></asp:Label>
                        <asp:HiddenField ID="hfFileName4" runat="server" Value="0" />
                    </div>
                    <asp:Button ID="btTmp3" runat="server" OnClick="btTmp3_Click" Style="display: none" Text="btTmp3" />
                </div>
                <div class="row form-group">
                    <div class="col col-sm-3">
                        <label for="input-small" class=" form-control-label">ข้อความแนะนำ</label>
                        <label for="input-small" class="color-red">*</label>
                    </div>
                    <div class="col col-sm-6">
                        <div>
                            <asp:TextBox ID="tbTextMessage_tab4" runat="server" class="form-control" TextMode="MultiLine"></asp:TextBox>
                            <%--<textarea name="textarea-input" id="textarea-input" rows="4" placeholder="Content..." class="form-control"></textarea>--%>
                        </div>
                    </div>
                </div>
                <div class="row form-group">
                    <div class="col col-sm-3">
                        <label for="input-small" class=" form-control-label">Font ข้อความแนะนำเพื่อน</label>
                        <label for="input-small" class="color-red">*</label>
                    </div>
                    <div class="col col-sm-6">
                        <div id="font2" class="input-group">
                            <asp:TextBox ID="tbTextFontName_tab4" runat="server" CssClass="input-sm form-control-sm form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row form-group">
                    <div class="col col-sm-3">
                        <label for="input-small" class=" form-control-label">สี ข้อความแนะนำเพื่อน</label>
                        <label for="input-small" class="color-red">*</label>
                    </div>
                    <div class="col col-sm-6">
                        <div id="cp9" class="input-group">
                            <asp:TextBox ID="tbTextFontColor_tab4" runat="server" CssClass="input-sm form-control-sm form-control" Text="#000000"></asp:TextBox>
                            <span class="input-group-append">
                                <span class="input-group-text colorpicker-input-addon"><i></i></span>
                            </span>
                        </div>
                    </div>
                </div>
                <asp:HiddenField ID="hfIdTab4" runat="server" Value="0" />
            </div>
            <div id="tabs-5" class="tab-pane fade">
                <div class="card-title">
                    <h4 class="text-left flat-color-6"><i class="fa fa-book"></i>&nbsp;ข้อมูลหน้าแนะนำเพื่อน 2</h4>
                </div>
                <div class="row form-group">
                    <div class="col col-sm-2">
                        <label for="input-small" class=" form-control-label">สี Background</label>
                        <label for="input-small" class="color-red">*</label>
                    </div>
                    <div class="col col-sm-6">
                        <div id="cp10" class="input-group">
                            <asp:TextBox ID="tbBackgroud_tab5" runat="server" CssClass="input-sm form-control-sm form-control" Text="#000000"></asp:TextBox>
                            <span class="input-group-append">
                                <span class="input-group-text colorpicker-input-addon"><i></i></span>
                            </span>
                        </div>
                    </div>
                </div>
                <div class="row form-group">
                    <div class="col col-sm-2">
                        <label for="input-small" class=" form-control-label">สี Font</label>
                        <label for="input-small" class="color-red">*</label>
                    </div>
                    <div class="col col-sm-6">
                        <div id="cp11" class="input-group">
                            <asp:TextBox ID="tbFontColor_tab5" runat="server" CssClass="input-sm form-control-sm form-control" Text="#000000"></asp:TextBox>
                            <span class="input-group-append">
                                <span class="input-group-text colorpicker-input-addon"><i></i></span>
                            </span>
                        </div>
                    </div>
                </div>
                <asp:HiddenField ID="hfIdTab5" runat="server" Value="0" />
            </div>
            <div id="tabs-6" class="tab-pane fade">
                <div class="card-title">
                    <h4 class="text-left flat-color-6"><i class="fa fa-book"></i>&nbsp;ข้อมูลหน้าปิดรับสมัคร</h4>
                    <div class="row form-group">
                    </div>
                    <div class="row form-group">
                        <div class="col col-sm-3">
                            <label for="input-small" class=" form-control-label">สี Background</label>
                            <label for="input-small" class="color-red">*</label>
                        </div>
                        <div class="col col-sm-6">
                            <%--<asp:TextBox ID="tbBackgroud_tab3" runat="server" CssClass="input-sm form-control-sm form-control"></asp:TextBox>--%>
                            <div id="cp12" class="input-group">
                                <asp:TextBox ID="tbBackgroud_tab6" runat="server" CssClass="input-sm form-control-sm form-control" Text="#000000"></asp:TextBox>
                                <span class="input-group-append">
                                    <span class="input-group-text colorpicker-input-addon"><i></i></span>
                                </span>
                            </div>
                        </div>
                    </div>
                    <div class="row form-group">
                        <div class="col col-sm-3">
                            <label for="input-small" class=" form-control-label">สี Font</label>
                            <label for="input-small" class="color-red">*</label>
                        </div>
                        <div class="col col-sm-6">
                            <%-- <asp:TextBox ID="tbFontName_tab3" runat="server" CssClass="input-sm form-control-sm form-control"></asp:TextBox>--%>
                            <div id="cp13" class="input-group">
                                <asp:TextBox ID="tbFontColor_tab6" runat="server" CssClass="input-sm form-control-sm form-control" Text="#000000"></asp:TextBox>
                                <span class="input-group-append">
                                    <span class="input-group-text colorpicker-input-addon"><i></i></span>
                                </span>
                            </div>
                        </div>
                    </div>
                    <div class="row form-group">
                        <div class="col col-sm-3">
                            <label for="input-small" class=" form-control-label">ข้อความปิดรับสมัคร</label>
                            <label for="input-small" class="color-red">*</label>
                        </div>
                        <div class="col col-sm-6">
                            <div>
                                <asp:TextBox ID="tbTextMessage_tab6" runat="server" class="form-control" TextMode="MultiLine"></asp:TextBox>
                                <%--<textarea name="textarea-input" id="textarea-input" rows="4" placeholder="Content..." class="form-control"></textarea>--%>
                                <%-- <textarea id="summernote" runat="server" name="editordata"></textarea>--%>
                            </div>
                        </div>
                    </div>
                    <div class="row form-group">
                        <div class="col col-sm-3">
                            <label for="input-small" class=" form-control-label">Font ข้อความปิดรับสมัคร</label>
                            <label for="input-small" class="color-red">*</label>
                        </div>
                        <div class="col col-sm-6">
                            <%--<asp:TextBox ID="tbFontName_tab3" runat="server" CssClass="input-sm form-control-sm form-control"></asp:TextBox>--%>
                            <div id="font3" class="input-group">
                                <asp:TextBox ID="tbTextFontName_tab6" runat="server" CssClass="input-sm form-control-sm form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <asp:HiddenField ID="hfIdTab6" runat="server" Value="0" />
            </div>
            <div class="row form-group ">
                <div class="col col-sm-4">
                </div>
                <div class="col col-sm-5">
                    <%--<asp:Button ID="Button1" runat="server" Text="Preview" CssClass="btn btn-primary" />--%>
                    <button type="button" class="btn btn-primary" onclick="openModal()">Preview</button>
                    <asp:Button ID="btInsert" runat="server" Text="Save" CssClass="btn btn-info" Width="80px" />
                    <asp:Button ID="btCancel" runat="server" Text="Cancel" CssClass="btn btn-secondary" Width="80px" />
                </div>
                <asp:HiddenField ID="hfIdTab7" runat="server" Value="0" />
            </div>
        </div>

        <div class="modal fade" id="preview0" tabindex="-1" role="dialog" aria-labelledby="largeModalLabel0" aria-hidden="true">
            <div class="modal-dialog modal-lg" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title float-left" id="largeModalLabel0">Preview >> ข้อมูลหน้าแรก</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">×</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-12">
                                <%--<h1 id="preview-title">Header title</h1>--%>
                                <label id="preview-title" for="input-small" class=" form-control-label font-headersize">Header title</label>
                            </div>
                        </div>
                        <div class="row mt-2">
                            <div class="col-md-12">
                                <div class="p-5 text-center" id="preview-body" style="background-color: forestgreen">
                                    <img id="preview-img" src="../Images/default.png" />
                                </div>

                            </div>
                        </div>
                        <div class="row mt-2">
                            <div class="col-md-12 text-center">
                                <button type="button" class="btn btn-primary font-buttonsize">คลิกเพื่อสมัครเข้าร่วมโครงการ</button>
                                <button type="button" class="btn btn-primary font-buttonsize">แนะนำเพื่อนเข้าร่วมโครงการ</button>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal fade" id="preview1" tabindex="-1" role="dialog" aria-labelledby="largeModalLabel1" aria-hidden="true">
            <div class="modal-dialog modal-lg" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title float-left" id="largeModalLabel1">Preview >>ข้อมูลหน้าลงทะเบียน</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">×</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-12">
                                <%--<h1 id="preview-title1">Header title</h1>--%>
                                <label id="preview-title1" for="input-small" class=" form-control-label font-headersize">Header title</label>
                            </div>
                        </div>
                        <div class="row mt-3">
                            <div class="col-md-12">
                                <div class="p-5 text-center" id="preview-body1" style="background-color: forestgreen">
                                    <div class="row">
                                        <div class="col-md-2">
                                            <img id="preview-imgEmp2" src="../Images/EmployeeDefault.png" />
                                        </div>
                                        <div class="col-md-10 text-left">
                                            <div class="row">
                                                <label id="preview-txt1" for="input-small" class=" form-control-label font-detailsize">ชื่อ นามสกุล</label>
                                            </div>
                                            <div class="row">
                                                <label id="preview-txt2" for="input-small" class=" form-control-label font-detailsize">ตำแหน่ง ส่วน</label>
                                            </div>
                                            <div class="row">
                                                <label id="preview-txt3" for="input-small" class=" form-control-label font-detailsize">สายงาน</label>
                                            </div>
                                            <div class="row">
                                                <label id="preview-txt4" for="input-small" class=" form-control-label font-detailsize">อีเมล</label>
                                            </div>
                                            <div class="row">
                                                <label id="preview-txt5" for="input-small" class=" form-control-label font-detailsize">เบอร์ต่อ</label>
                                            </div>
                                        </div>
                                    </div>
                                    <hr style="border-top: 1px solid White;" />
                                    <div class="row">
                                        <div class="col-md-12 text-left">
                                            <label id="preview-txt6" for="input-small" class=" form-control-label font-detailsize2">กรุณาระบุสิ่งที่ท่านคาดหวังจากโครงการนี้</label>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12 text-left">
                                            <asp:TextBox ID="TextBox1" runat="server" class="form-control" TextMode="MultiLine" Height="100"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-12 text-center">
                                        <div class="p-2 text-center">
                                            <asp:CheckBox ID="cbConfrim" runat="server" class="form-check-input" />
                                            <label id="preview-txt7" for="input-small" class=" form-control-label font-detailsize2">ยืนยันการสมัครของท่าน</label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row mt-2">
                            <div class="col-md-12 text-center">
                                <button type="button" class="btn btn-primary font-buttonsize">ลงทะเบียนสมัครเข้าร่วมโครงการ</button>
                                <%--<button type="button" class="btn btn-primary font-buttonsize">ยกเลิกการทำรายการ</button>--%>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal fade" id="preview2" tabindex="-1" role="dialog" aria-labelledby="largeModalLabel2" aria-hidden="true">
            <div class="modal-dialog modal-lg" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title float-left" id="largeModalLabel2">Preview >> ข้อมูลหน้าขอบคุณ</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">×</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-12">
                                <%--<h1 id="preview-title1">Header title</h1>--%>
                                <label id="preview-title2" for="input-small" class=" form-control-label font-headersize">Header title</label>
                            </div>
                        </div>
                        <div class="row mt-3">
                            <div class="col-md-12">
                                <div class="p-5 text-center" id="preview-body2" style="background-color: forestgreen">
                                    <div class="row">
                                        <div class="col-md-2">
                                            <img id="preview-img1" src="../Images/EmployeeDefault.png" />
                                        </div>
                                        <div class="col-md-10 text-left">
                                            <div class="row">
                                                <label id="preview-txt8" for="input-small" class=" form-control-label font-detailsize">ชื่อ นามสกุล</label>
                                            </div>
                                            <div class="row">
                                                <label id="preview-txt9" for="input-small" class=" form-control-label font-detailsize">ตำแหน่ง ส่วน</label>
                                            </div>
                                            <div class="row">
                                                <label id="preview-txt10" for="input-small" class=" form-control-label font-detailsize">สายงาน</label>
                                            </div>
                                            <div class="row">
                                                <label id="preview-txt11" for="input-small" class=" form-control-label font-detailsize">อีเมล</label>
                                            </div>
                                            <div class="row">
                                                <label id="preview-txt12" for="input-small" class=" form-control-label font-detailsize">เบอร์ต่อ</label>
                                            </div>
                                        </div>
                                    </div>
                                    <hr style="border-top: 1px solid White;" />
                                    <div class="row">
                                        <div class="col-md-12 text-center">
                                            <img id="preview-img2" src="../Images/defaultthank.png" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-12 p-3" id="preview-detail"></div>
                                        <%--<div class="col-md-12 text-center">
                                            <label id="preview-detail" for="input-small" class=" form-control-label font-headersize">detail...</label>
                                        </div>--%>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row mt-2">
                            <div class="col-md-12 text-center">
                                <button type="button" class="btn btn-primary font-buttonsize">แนะนำเพื่อนเข้าร่วมโครงการ</button>
                                <%--<button type="button" class="btn btn-primary font-buttonsize">ปิดหน้าต่าง</button>--%>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal fade" id="preview3" tabindex="-1" role="dialog" aria-labelledby="largeModalLabel3" aria-hidden="true">
            <div class="modal-dialog modal-lg" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title float-left" id="largeModalLabel3">Preview >> ข้อมูลหน้าแนะนำเพื่อน(หน้าแรก)</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">×</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-12">
                                <%--<h1 id="preview-title">Header title</h1>--%>
                                <label id="preview-title3" for="input-small" class=" form-control-label font-headersize">Header title</label>
                            </div>
                        </div>
                        <div class="row mt-2">
                            <div class="col-md-12">
                                <div class="p-4 text-center" id="preview-body3" style="background-color: forestgreen">
                                    <div class="col-md-12 text-center">
                                        <label id="preview-detail2" for="input-small" class=" form-control-label font-headersize">detail...</label>
                                    </div>
                                    <div class="col-md-12 text-center">
                                        <img id="preview-img3" src="../Images/default.png" />
                                    </div>

                                </div>

                            </div>
                        </div>
                        <div class="row mt-2">
                            <div class="col-md-12 text-center">
                                <button type="button" class="btn btn-primary font-buttonsize">คลิกเพื่อเชิญเพื่อนเข้าร่วมโครงการ</button>
                                <%--<button type="button" class="btn btn-primary font-buttonsize">ปิดหน้าต่าง</button>--%>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal fade" id="preview4" tabindex="-1" role="dialog" aria-labelledby="largeModalLabel4" aria-hidden="true">
            <div class="modal-dialog modal-lg" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title float-left" id="largeModalLabel4">Preview >> ข้อมูลหน้าแนะนำเพื่อน 2</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">×</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-12">
                                <%--<h1 id="preview-title">Header title</h1>--%>
                                <label id="preview-title4" for="input-small" class=" form-control-label font-headersize">Header title</label>
                            </div>
                        </div>
                        <div class="row mt-2">
                            <div class="col-md-12">
                                <div class="p-4 text-left" id="preview-body4" style="background-color: forestgreen">
                                    <div class="col-md-12 text-left">
                                        <label id="preview-detail4" for="input-small" class=" form-control-label font-detailsize2">กรอกรหัสพนักงานของเพื่อนที่จะชวนเข้าร่วมโครงการ</label>
                                    </div>
                                    <div class="col-md-12 text-left">
                                        <div class="row">
                                            <div class="col-md-4">
                                                <asp:TextBox ID="TextBox2" runat="server" class="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-3">
                                                <button type="button" class="btn btn-primary font-buttonsize2">ตรวจสอบข้อมูล</button>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12 text-left">
                                        <label id="preview-txt14" for="input-small" class=" form-control-label font-detailsize2">1.ชื่อ - นามสกุล</label>
                                    </div>
                                    <div class="col-md-12 text-left">
                                        <label id="preview-txt15" for="input-small" class=" form-control-label font-detailsize2">2.ชื่อ - นามสกุล</label>
                                    </div>
                                    <div class="col-md-12 text-left">
                                        <label id="preview-txt16" for="input-small" class=" form-control-label font-detailsize2">3.ชื่อ - นามสกุล</label>
                                    </div>
                                    <hr style="border-top: 1px solid White;" />
                                    <div class="col-md-12 text-left">
                                        <label id="preview-txt17" for="input-small" class=" form-control-label font-detailsize2">ข้อความที่อยากบอกเพื่อน</label>
                                    </div>
                                    <div class="col-md-12 text-left">
                                        <asp:TextBox ID="TextBox3" runat="server" class="form-control" TextMode="MultiLine" Height="100"></asp:TextBox>
                                    </div>

                                </div>

                            </div>
                        </div>
                        <div class="row mt-2">
                            <div class="col-md-12 text-center">
                                <button type="button" class="btn btn-primary font-buttonsize">ส่งเชิญเพื่อนเพื่อร่วมสมัคร</button>
                                <%--<button type="button" class="btn btn-primary font-buttonsize">ปิดหน้าต่าง</button>--%>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal fade" id="preview5" tabindex="-1" role="dialog" aria-labelledby="largeModalLabel5" aria-hidden="true">
            <div class="modal-dialog modal-lg" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title float-left" id="largeModalLabel5">Preview >> ข้อมูลหน้าปิดรับสมัคร</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">×</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="row mt-1">
                            <div class="col-md-12">
                                <div class="p-4 text-left" id="preview-body5" style="background-color: forestgreen">
                                    <div class="row">
                                        <div class="col-12 p-3" id="preview-detail5"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>

        <asp:HiddenField ID="hfProjectId" runat="server" />
    </asp:Panel>
    <%--<uc1:ucpopupmessage ID="ucPopupMessage1" runat="server" />--%>
</asp:Content>
