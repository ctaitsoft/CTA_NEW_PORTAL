<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Site.Master" AutoEventWireup="true" CodeBehind="AccountAdd.aspx.cs" Inherits="CTA_NEW_PORTAL.Modules.Account.AccountAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <style>
        #ContentPlaceHolder1_AccountUpdatePanel {
            width: 100%;
            /*border: solid 1px #f00;*/
        }

        /*.ui-widget-overlay {
            z-index: 0 !important;
        }*/

        /*.RadWindow_Bootstrap .rwTitleBar .rwTitleWrapper {
            line-height: 27px;
        }*/

        /*.rwTitleWrapper {
            background-color: #808080 !important;
        }*/

        .form-group {
            margin-bottom: 0px !important;
        }

        .help-block {
            margin-top: 0px !important;
            margin-bottom: 0px !important;
        }

    </style>

    <asp:UpdatePanel ID="AccountUpdatePanel" runat="server">
        <ContentTemplate>

            <script type="text/javascript">
                <%--function openModalPopup_confirm() {
                    $find("<%=modalPopup_confirm.ClientID %>").show();
                }

                function closeModalPopup_confirm() {
                    $find("<%=modalPopup_confirm.ClientID%>").close();
                }


                function openModalPopup_Check() {
                    $find("<%=modalPopup_Check.ClientID %>").show();
                }

                function closeModalPopup_Check() {
                    $find("<%=modalPopup_Check.ClientID%>").close();
                }--%>



                function CheckMove() {
                    document.getElementById('<%= hidnMoveFlag.ClientID %>').value = "1";
                }

                var opt = {
                    autoOpen: false,
                    modal: true,
                    width: 'auto',
                    appendTo: $("#ContentPlaceHolder1_AccountUpdatePanel"),
                    buttons: {
                        //Ok: function () {
                        //    $(this).dialog("close");
                        //}
                    },
                    draggable: true,
                    resizable: false,
                    close: function (event, ui) {
                        $('a.Theme').removeClass('ui-state-focus ui-state-hover ui-state-active');
                        $('a.Theme').blur();
                    }

                };

                function ShowconfirmDialog() {
                    if (document.getElementById('<%= accCheckName.ClientID %>').value == "1") {

                    } else {
                        $('#modalPopup_confirm').dialog(opt).dialog("open");
                    }
                }

                function CloseconfirmDialog() {
                    $('#modalPopup_confirm').dialog('close');
                }

                function ShowCheckDialog() {
                    $('#modalPopup_Check').dialog(opt).dialog("open");
                }

                function CloseCheckDialog() {
                    $('#modalPopup_Check').dialog('close');
                }

                function ShowTitleDialog() {
                    $('#modalPopup_Title').dialog(opt).dialog("open");
                }

                function ShowTypeDialog() {
                    $('#modalPopup_Type').dialog(opt).dialog("open");
                }

                // Warning before leaving the page (back button, or outgoinglink)
                window.onbeforeunload = function () {
                    if (document.getElementById('<%= hidnMoveFlag.ClientID %>').value == "1") {
                        return "Do you really want to leave ?";
                    } else {

                    }
                    //if we return nothing here (just calling return;) then there will be no pop-up question at all
                    //return;
                };

            </script>

            <script>
                $(document).ready(function () {
                    //$(function() {
                    //    $('[id$=F_11]').on('keydown',
                    //        function(e) {
                    //            -1 !== $.inArray(e.keyCode, [46, 8, 9, 27, 13, 110]) ||
                    //                (/65|67|86|88/.test(e.keyCode) && (e.ctrlKey === true || e.metaKey === true)) &&
                    //                (!0 === e.ctrlKey || !0 === e.metaKey) ||
                    //                35 <= e.keyCode && 40 >= e.keyCode ||
                    //                (e.shiftKey || 48 > e.keyCode || 57 < e.keyCode) &&
                    //                (96 > e.keyCode || 105 < e.keyCode) &&
                    //                e.preventDefault();
                    //        });
                    //});

                    function getUrlVars() {
                        var vars = [], hash;
                        var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
                        for (var i = 0; i < hashes.length; i++) {
                            hash = hashes[i].split('=');
                            vars.push(hash[0]);
                            vars[hash[0]] = hash[1];
                        }
                        return vars;
                    }


                    function Checkname(type, name) {
                        var rio = $('[id$=accUsr]').val();
                        //var name = $('[id$=txtFullName]').val();
                        $.ajax({
                            url: '../../Services/Account/Srvc_CheckName.asmx/UserNameExist',
                            method: 'post',
                            data: { AccName: name, suario: rio, searchType: type },
                            dataType: 'json',
                            success: function (data) {
                                var lblElement = $('[id$=AccExist]');
                                var icon = $('[id$=existIcon]');
                                var message = $('[id$=helpBlock2]');

                                var obj = jQuery.parseJSON(data);
                                //var cuenta = getUrlVars()["s"];

                                //debugger;
                                if (obj !== 0) {
                                    if ($("[id$=lblAccQ]").val() == obj || $('[id$=lblAccId]').text() == obj) {
                                        //Dont check and remove all classes
                                        lblElement.removeClass('has-error');
                                        lblElement.removeClass('has-success');
                                        icon.removeClass('glyphicon-remove');
                                        icon.removeClass('glyphicon-ok');
                                        message.text("");
                                    } else {
                                        document.getElementById('<%= accCheckName.ClientID %>').value = "1";
                                        lblElement.addClass('has-error');
                                        lblElement.removeClass('has-success');
                                        icon.addClass('glyphicon-remove');
                                        icon.removeClass('glyphicon-ok');
                                        message.text("This name isn't allowed. Try again.");
                                    }

                                } else {
                                    document.getElementById('<%= accCheckName.ClientID %>').value = "0";
                                    lblElement.addClass('has-success');
                                    lblElement.removeClass('has-error');
                                    icon.removeClass('glyphicon-remove');
                                    icon.addClass('glyphicon-ok');
                                    message.text("This name is allowed");
                                }
                            },
                            error: function (err) {
                                alert(err.statusText + err.responseText);
                            }
                        });
                    }

                    function checkMobile(type, name) {
                        var rio = $('[id$=accUsr]').val();
                        //var name = $('[id$=txtFullName]').val();
                        $.ajax({
                            url: '../../Services/Account/Srvc_CheckName.asmx/UserNameExist',
                            method: 'post',
                            data: { AccName: name, suario: rio, searchType: type },
                            dataType: 'json',
                            success: function (data) {
                                var lblElement = $('[id$=AccMobExist]');
                                var icon = $('[id$=MobExistIcon]');
                                var message = $('[id$=helpBlock3]');

                                var obj = jQuery.parseJSON(data);

                                if (obj !== 0) {
                                    lblElement.addClass('has-warning');
                                    icon.addClass('glyphicon-warning-sign');
                                    message.text("This Mobile number is already exist. Please check !!");
                                } else {
                                    lblElement.removeClass('has-warning');
                                    icon.removeClass('glyphicon-warning-sign');
                                    message.text("");
                                }
                            },
                            error: function (err) {
                                alert(err.statusText + err.responseText);
                            }
                        });
                    }

                    //tooltip
                    Tipped.create('.fa-plus-circle', function (element) {
                        return {
                            //title: $(element).data('title'),
                            content: $(element).data('content')
                        };
                    }, {
                            skin: 'light',
                            position: 'left'
                        });

                    //append to textbox
                    $('[id$=txtFullName], [id$=txtFirstName], [id$=txtSecondName], [id$=txtThirdName], [id$=txtForthName]').keyup(function () {
                        //debugger;
                        $('[id$=dllAccountType]').attr('disabled', 'disabled');
                        $('[id$=dllAccountType]').attr('readonly', 'readonly');

                        if ($('[id$=dllAccountType]').val() == '1') {
                            var first = $('[id$=txtFirstName]').val();
                            var second = $('[id$=txtSecondName]').val();
                            var third = $('[id$=txtThirdName]').val();
                            var forth = $('[id$=txtForthName]').val();

                            $('[id$=txtFullName]').val(first + " " + second + " " + third + " " + forth);
                        }
                    });

                    //remove append from textbox and check if exist
                    $('[id$=txtFullName], [id$=txtFirstName], [id$=txtSecondName], [id$=txtThirdName], [id$=txtForthName]').blur(function () {

                        if ($('[id$=dllAccountType]').val() == '1') {
                            var names_arr = [$("[id$=txtFirstName]").val(), $("[id$=txtSecondName]").val(), $("[id$=txtThirdName]").val(), $("[id$=txtForthName]").val()];
                            var status = 0;
                            //check text box is filled or not (minimum 2) 
                            function checkValue(arr) {
                                for (var i = 0; i < arr.length; i++) {
                                    var name = arr[i];
                                    name.replace(/\s+/g, '');
                                    if (name != "") {
                                        status++;
                                    }
                                }
                            }

                            checkValue(names_arr);

                            if (status < 2) {
                                document.getElementById('<%= accCheckName.ClientID %>').value = "1";
                                var lblElement = $('[id$=AccExist]');
                                var icon = $('[id$=existIcon]');
                                var message = $('[id$=helpBlock2]');

                                lblElement.removeClass('has-error');
                                lblElement.removeClass('has-success');
                                icon.removeClass('glyphicon-remove');
                                icon.removeClass('glyphicon-ok');
                                message.text("");
                                if ($('[id$=dllAccountType]').val() == '1') {
                                    var first = $('[id$=txtFirstName]').val();
                                    var second = $('[id$=txtSecondName]').val();
                                    var third = $('[id$=txtThirdName]').val();
                                    var forth = $('[id$=txtForthName]').val();

                                    $('[id$=txtFullName]').val(first + " " + second + " " + third + " " + forth);
                                }

                            } else {
                                Checkname("Basic", $('[id$=txtFullName]').val());
                            }

                        }
                        else {
                            if ($.trim($('[id$=txtFullName]').val()) === '') {

                            } else {
                                Checkname("Basic", $('[id$=txtFullName]').val());
                            }
                        }


                    });


                    $('[id$=ddlTitle]').change(function () {
                        $('[id$=txtFirstName]').focus();
                        $('[id$=dllAccountType]').attr('disabled', 'disabled');
                        $('[id$=dllAccountType]').attr('readonly', 'readonly');
                    });

                    $('[id$=ddlGender]').change(function () {
                        $('[id$=dllAccountType]').attr('disabled', 'disabled');
                        $('[id$=dllAccountType]').attr('readonly', 'readonly');
                    });

                    //Next for group
                    $(".nxt").keypress(function (event) {
                        if (event.keyCode === 13) {
                            var textboxes = $("input.nxt, select.nxt");
                            //debugger;
                            var currentBoxNumber = textboxes.index(this);

                            if (textboxes[currentBoxNumber + 1] != null) {
                                //check if the next field id disabled and skip from it
                                if (textboxes[currentBoxNumber + 1].disabled) {
                                    var nextBox1 = textboxes[currentBoxNumber + 2];
                                    nextBox1.focus();
                                    nextBox1.select();
                                    event.preventDefault();
                                    return false;
                                }

                                var nextBox = textboxes[currentBoxNumber + 1];
                                nextBox.focus();
                                nextBox.select();
                                event.preventDefault();
                                return false;
                            }
                        }
                    });

                    $('[id$=F_11]').blur(function () {
                        //debugger;
                        if ($.trim($('[id$=F_11]').val()) === '') {

                        } else {
                            checkMobile("Mobile", $('[id$=F_11]').val());
                        }
                    });
                    //Next for all
                    //$('input').keypress(function (e) {
                    //    if(e.keyCode === 13) {
                    //        $(this).next().focus();
                    //    }
                    //});


                });

                var parameter = Sys.WebForms.PageRequestManager.getInstance();
                //jquery code again for working after postback
                parameter.add_endRequest(function () {
                    function getUrlVars() {
                        var vars = [], hash;
                        var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
                        for (var i = 0; i < hashes.length; i++) {
                            hash = hashes[i].split('=');
                            vars.push(hash[0]);
                            vars[hash[0]] = hash[1];
                        }
                        return vars;
                    }

                    function Checkname(type, name) {
                        var rio = $('[id$=accUsr]').val();
                        //var name = $('[id$=txtFullName]').val();
                        $.ajax({
                            url: '../../Services/Account/Srvc_CheckName.asmx/UserNameExist',
                            method: 'post',
                            data: { AccName: name, suario: rio, searchType: type },
                            dataType: 'json',
                            success: function (data) {
                                var lblElement = $('[id$=AccExist]');
                                var icon = $('[id$=existIcon]');
                                var message = $('[id$=helpBlock2]');

                                var obj = jQuery.parseJSON(data);
                                //var cuenta = getUrlVars()["s"];

                                //debugger;
                                if (obj !== 0) {
                                    if ($("[id$=lblAccQ]").val() == obj || $('[id$=lblAccId]').text() == obj) {
                                        //Dont check and remove all classes
                                        lblElement.removeClass('has-error');
                                        lblElement.removeClass('has-success');
                                        icon.removeClass('glyphicon-remove');
                                        icon.removeClass('glyphicon-ok');
                                        message.text("");
                                    } else {
                                        document.getElementById('<%= accCheckName.ClientID %>').value = "1";
                                        lblElement.addClass('has-error');
                                        lblElement.removeClass('has-success');
                                        icon.addClass('glyphicon-remove');
                                        icon.removeClass('glyphicon-ok');
                                        message.text("This name isn't allowed. Try again.");
                                    }

                                } else {
                                    document.getElementById('<%= accCheckName.ClientID %>').value = "0";
                                    lblElement.addClass('has-success');
                                    lblElement.removeClass('has-error');
                                    icon.removeClass('glyphicon-remove');
                                    icon.addClass('glyphicon-ok');
                                    message.text("This name is allowed");
                                }
                            },
                            error: function (err) {
                                alert(err.statusText + err.responseText);
                            }
                        });
                    }

                    function checkMobile(type, name) {
                        var rio = $('[id$=accUsr]').val();
                        //var name = $('[id$=txtFullName]').val();
                        $.ajax({
                            url: '../../Services/Account/Srvc_CheckName.asmx/UserNameExist',
                            method: 'post',
                            data: { AccName: name, suario: rio, searchType: type },
                            dataType: 'json',
                            success: function (data) {
                                var lblElement = $('[id$=AccMobExist]');
                                var icon = $('[id$=MobExistIcon]');
                                var message = $('[id$=helpBlock3]');

                                var obj = jQuery.parseJSON(data);

                                if (obj !== 0) {
                                    lblElement.addClass('has-warning');
                                    icon.addClass('glyphicon-warning-sign');
                                    message.text("This Mobile number is already exist. Please check !!");
                                } else {
                                    lblElement.removeClass('has-warning');
                                    icon.removeClass('glyphicon-warning-sign');
                                    message.text("");
                                }
                            },
                            error: function (err) {
                                alert(err.statusText + err.responseText);
                            }
                        });
                    }

                    //tooltip
                    Tipped.create('.fa-plus-circle', function (element) {
                        return {
                            //title: $(element).data('title'),
                            content: $(element).data('content')
                        };
                    }, {
                            skin: 'light',
                            position: 'left'
                        });

                    //append to textbox
                    $('[id$=txtFullName], [id$=txtFirstName], [id$=txtSecondName], [id$=txtThirdName], [id$=txtForthName]').keyup(function () {
                        //debugger;
                        $('[id$=dllAccountType]').attr('disabled', 'disabled');
                        $('[id$=dllAccountType]').attr('readonly', 'readonly');

                        if ($('[id$=dllAccountType]').val() == '1') {
                            var first = $('[id$=txtFirstName]').val();
                            var second = $('[id$=txtSecondName]').val();
                            var third = $('[id$=txtThirdName]').val();
                            var forth = $('[id$=txtForthName]').val();

                            $('[id$=txtFullName]').val(first + " " + second + " " + third + " " + forth);
                        }
                    });

                    //remove append from textbox and check if exist
                    $('[id$=txtFullName], [id$=txtFirstName], [id$=txtSecondName], [id$=txtThirdName], [id$=txtForthName]').blur(function () {

                        if ($('[id$=dllAccountType]').val() == '1') {
                            var names_arr = [$("[id$=txtFirstName]").val(), $("[id$=txtSecondName]").val(), $("[id$=txtThirdName]").val(), $("[id$=txtForthName]").val()];
                            var status = 0;
                            //check text box is filled or not (minimum 2) 
                            function checkValue(arr) {
                                for (var i = 0; i < arr.length; i++) {
                                    var name = arr[i];
                                    name.replace(/\s+/g, '');
                                    if (name != "") {
                                        status++;
                                    }
                                }
                            }

                            checkValue(names_arr);

                            if (status < 2) {
                                document.getElementById('<%= accCheckName.ClientID %>').value = "1";
                                var lblElement = $('[id$=AccExist]');
                                var icon = $('[id$=existIcon]');
                                var message = $('[id$=helpBlock2]');

                                lblElement.removeClass('has-error');
                                lblElement.removeClass('has-success');
                                icon.removeClass('glyphicon-remove');
                                icon.removeClass('glyphicon-ok');
                                message.text("");
                                if ($('[id$=dllAccountType]').val() == '1') {
                                    var first = $('[id$=txtFirstName]').val();
                                    var second = $('[id$=txtSecondName]').val();
                                    var third = $('[id$=txtThirdName]').val();
                                    var forth = $('[id$=txtForthName]').val();

                                    $('[id$=txtFullName]').val(first + " " + second + " " + third + " " + forth);
                                }

                            } else {
                                Checkname("Basic", $('[id$=txtFullName]').val());
                            }

                        }
                        else {
                            if ($.trim($('[id$=txtFullName]').val()) === '') {

                            } else {
                                Checkname("Basic", $('[id$=txtFullName]').val());
                            }
                        }


                    });


                    $('[id$=ddlTitle]').change(function () {
                        $('[id$=txtFirstName]').focus();
                        $('[id$=dllAccountType]').attr('disabled', 'disabled');
                        $('[id$=dllAccountType]').attr('readonly', 'readonly');
                    });

                    $('[id$=ddlGender]').change(function () {
                        $('[id$=dllAccountType]').attr('disabled', 'disabled');
                        $('[id$=dllAccountType]').attr('readonly', 'readonly');
                    });

                    //Next for group
                    $(".nxt").keypress(function (event) {
                        if (event.keyCode === 13) {
                            var textboxes = $("input.nxt, select.nxt");
                            //debugger;
                            var currentBoxNumber = textboxes.index(this);

                            if (textboxes[currentBoxNumber + 1] != null) {
                                //check if the next field id disabled and skip from it
                                if (textboxes[currentBoxNumber + 1].disabled) {
                                    var nextBox1 = textboxes[currentBoxNumber + 2];
                                    nextBox1.focus();
                                    nextBox1.select();
                                    event.preventDefault();
                                    return false;
                                }

                                var nextBox = textboxes[currentBoxNumber + 1];
                                nextBox.focus();
                                nextBox.select();
                                event.preventDefault();
                                return false;
                            }
                        }
                    });

                    $('[id$=F_11]').blur(function () {
                        if ($.trim($('[id$=F_11]').val()) === '') {

                        } else {
                            checkMobile("Mobile", $('[id$=F_11]').val());
                        }
                    });


                    //Next for all
                    //$('input').keypress(function (e) {
                    //    if(e.keyCode === 13) {
                    //        $(this).next().focus();
                    //    }
                    //});

                });
            </script>

            <div id="Sub_Content">

                <div class="subject-row">
                    <h4>Accounts &nbsp;<i class="fa fa-caret-right" aria-hidden="true"></i>&nbsp; <asp:Label ID="lblSubject" runat="server"></asp:Label></h4>
                    <%--<i id="NewAcc" class="fa fa-plus-circle" aria-hidden="true" data-content="New" title="New" data-title='New'></i>--%>
                    <%--<asp:LinkButton runat="server" ID="NewAcc" OnClick="NewAcc_OnClick" CausesValidation="False"><i class="fa fa-plus-circle tltip" aria-hidden="true" data-content="New" title="New" data-title='New'></i></asp:LinkButton>--%>
                    <asp:Panel runat="server" ID="btnsPanel" CssClass="btns_Panel">
                        <asp:LinkButton runat="server" ID="B_11" Text="Inquiry" CssClass="mainBtns" CausesValidation="False" Enabled="False" OnClick="B_11_OnClick" /><%--Inquiry 1--%>
                        <asp:LinkButton runat="server" ID="B_12" Text="View" CssClass="mainBtns" Enabled="False" CausesValidation="False" OnClick="B_12_OnClick" /><%--View 2--%>
                        <asp:LinkButton runat="server" ID="B_13" Text="Print" CssClass="mainBtns" Enabled="False" /><%--Print 3--%>
                        <span style="width: 30px; display: inline-block"></span>
                        <asp:LinkButton runat="server" ID="B_21" Text="New" CssClass="mainBtns" OnClick="B_21_OnClick" CausesValidation="False" Enabled="False" /><%--New 4--%>
                        <asp:LinkButton runat="server" ID="B_22" Text="Update" CssClass="mainBtns" Enabled="False" OnClick="B_22_OnClick" /><%--Update 5--%>
                        <asp:LinkButton runat="server" ID="B_23" Text="Cancel" CssClass="mainBtns" Enabled="False" /><%--Cancel 6--%>
                        <asp:LinkButton runat="server" ID="B_29" Text="Save" CssClass="mainBtns" OnClientClick="if(Page_ClientValidate('save')) ShowconfirmDialog(); return false;" /><%--Save 7--%>
                        <span style="width: 30px; display: inline-block"></span>
                        <asp:LinkButton runat="server" ID="B_18" Text="Refresh" CssClass="mainBtns" CausesValidation="False" OnClick="B_18_OnClick" /><%--Refresh 8--%>
                        <asp:LinkButton runat="server" ID="B_19" Text="Clear" CssClass="mainBtns" CausesValidation="False" OnClick="B_19_OnClick" /><%--Clear 9--%>

                        <div class="btn-group">
                            <a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">More &nbsp;<span class="caret"></span></a>
                            <ul class="dropdown-menu">
                                <li>
                                    <asp:LinkButton ID="B_31" runat="server" Text="Copy" Enabled="False" /></li>
                                <%--Copy 10--%>
                                <li>
                                    <asp:LinkButton ID="B_32" runat="server" Text="Paste" Enabled="False" /></li>
                                <%--Paste 11--%>
                                <li>
                                    <asp:LinkButton ID="B_41" runat="server" Text="Import" Enabled="False" /></li>
                                <%--Import 12--%>
                                <li>
                                    <asp:LinkButton ID="B_42" runat="server" Text="Export" Enabled="False" /></li>
                                <%--Export 13--%>
                                <li role="separator" class="divider"></li>
                                <li>
                                    <asp:LinkButton ID="B_99" runat="server" Text="Special Fields" Enabled="False" /></li>
                                <%--Special Fields 14--%>
                            </ul>
                        </div>

                    </asp:Panel>

                </div>

                <div id="accHeader" class="row" runat="server" style="visibility: hidden">
                    <div class="row-trad">
                            <table class="flt-left m-r-120" border="0">
                                <tr>
                                    <td class="label-cell-width-x3">Account Code:</td>
                                    <td>
                                        <asp:Label runat="server" ID="lblAccNo"></asp:Label></td>
                                </tr>
                            </table>

                            <table class="flt-left" border="0">
                                <tr>
                                    <td class="label-cell-width-x3">Account Name:</td>
                                    <td>
                                        <asp:Label runat="server" ID="lblAccName"></asp:Label>
                                        <asp:Label runat="server" ID="lblAccCategory" CssClass="hidn"></asp:Label>
                                    </td>
                                </tr>
                            </table>

                    </div>
                </div>

                <div class="row">
                    <div class="row-content">

                        <table class="table" border="0">
                            <tr>
                                <td>Code</td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtAccCode" CssClass="form-control" Enabled="False" ReadOnly="True"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td class="label-cell-width">Type</td>
                                <td>
                                    <asp:DropDownList runat="server" ID="dllAccountType" CssClass="form-control" CausesValidation="False" OnSelectedIndexChanged="dllAccountType_OnSelectedIndexChanged" AutoPostBack="True" onchange="javascript:CheckMove();">
                                    </asp:DropDownList>
                                </td>
                            </tr>

                            <tr>
                                <td class="label-cell-width">Title</td>
                                <td>
                                    <asp:DropDownList runat="server" ID="ddlTitle" CssClass="form-control" onchange="javascript:CheckMove();"></asp:DropDownList>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="ddlTitle" Display="Dynamic" ValidationGroup="save" ErrorMessage=" * This Field is Required" ForeColor="Red" InitialValue="-1" />
                                </td>
                                <td style="border: 0 !important; width: 50px">
                                    <asp:LinkButton runat="server" ID="btnAddTitle" CssClass="btn btn-default" OnClientClick="ShowTitleDialog(); return false;"><i class="fa fa-plus" aria-hidden="true"></i></asp:LinkButton></td>
                            </tr>

                            <tr>
                                <td class="cell-vertical-align-md label-cell-width">Name</td>
                                <td>
                                    <table id="tabless" class="table table-margin-bottom table-border">
                                        <tr id="AccName" runat="server">
                                            <td>
                                                <asp:TextBox runat="server" ID="txtFirstName" CssClass="form-control txt-letter-form-control nxt" placeholder="First Name" MaxLength="15" onKeyPress="javascript:CheckMove();"></asp:TextBox>
                                                <%--<asp:RequiredFieldValidator runat="server" ID="txtFirstName_Validator" ControlToValidate="txtFirstName" Display="Dynamic" ErrorMessage=" * This Field is Required" ForeColor="Red" />--%>
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="txtSecondName" CssClass="form-control txt-letter-form-control nxt" placeholder="Second Name" MaxLength="15" onKeyPress="javascript:CheckMove();"></asp:TextBox>
                                                <%--<asp:RequiredFieldValidator runat="server" ID="txtSecondName_Validator" ControlToValidate="txtSecondName" Display="Dynamic" ErrorMessage=" * This Field is Required" ForeColor="Red" />--%>
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="txtThirdName" CssClass="form-control txt-letter-form-control nxt" placeholder="Third Name" MaxLength="15" onKeyPress="javascript:CheckMove();"></asp:TextBox>
                                                <%--<asp:RequiredFieldValidator runat="server" ID="txtThirdName_Validator" ControlToValidate="txtThirdName" Display="Dynamic" ErrorMessage=" * This Field is Required" ForeColor="Red" />--%>
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="txtForthName" CssClass="form-control txt-letter-form-control nxt" placeholder="Forth Name" MaxLength="15" onKeyPress="javascript:CheckMove();"></asp:TextBox>
                                                <%--<asp:RequiredFieldValidator runat="server" ID="txtForthName_Validator" ControlToValidate="txtForthName" Display="Dynamic" ErrorMessage=" * This Field is Required" ForeColor="Red" />--%>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td colspan="4">
                                                <div id="AccExist" class="form-group has-feedback">
                                                    <asp:TextBox runat="server" ID="txtFullName" CssClass="form-control txt-letter-form-control nxt" placeholder="Full Name" MaxLength="65" onKeyPress="javascript:CheckMove();" aria-describedby="inputSuccess4Status"></asp:TextBox>
                                                    <span id="existIcon" class="glyphicon form-control-feedback" aria-hidden="true"></span>
                                                    <span id="helpBlock2" class="help-block"></span>
                                                </div>
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="txtFullName" Display="Dynamic" ValidationGroup="save" ErrorMessage=" * This Field is Required" ForeColor="Red" />
                                            </td>
                                        </tr>

                                    </table>
                                </td>
                            </tr>

                            <tr>
                                <td class="label-cell-width">Gender</td>
                                <td>
                                    <asp:DropDownList runat="server" ID="ddlGender" CssClass="form-control nxt" onchange="javascript:CheckMove();"></asp:DropDownList>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="ddlGender" Display="Dynamic" ValidationGroup="save" ErrorMessage=" * This Field is Required" ForeColor="Red" InitialValue="-1" />
                                </td>
                            </tr>
                            <tr id="accContactType" runat="server" style="display: none;">
                                <td class="label-cell-width">Contact Type</td>
                                <td>
                                    <asp:DropDownList runat="server" ID="ddlContactType" CssClass="form-control" onchange="javascript:CheckMove();"></asp:DropDownList>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_ddlContactType" ControlToValidate="ddlGender" Display="Dynamic" ValidationGroup="save" ErrorMessage=" * This Field is Required" ForeColor="Red" InitialValue="-1" />
                                </td>
                                <td style="border: 0 !important; width: 50px">
                                    <asp:LinkButton runat="server" ID="btnAddContactTyp" CssClass="btn btn-default" OnClientClick="ShowTypeDialog(); return false;"><i class="fa fa-plus" aria-hidden="true"></i></asp:LinkButton></td>
                            </tr>

                        </table>

                        <asp:Label runat="server" ID="lblAccId" CssClass="hidn"></asp:Label>
                        <asp:Label runat="server" ID="lblAccContactId" CssClass="hidn"></asp:Label>
                        <asp:Label runat="server" ID="lblAccQ" CssClass="hidn"></asp:Label>
                        <asp:Label runat="server" ID="lblviewQ" CssClass="hidn"></asp:Label>
                    </div>
                </div>

                <div class="row">
                    <hr class="col-xs-12" />
                </div>

                <div class="row">
                    <div class="row-content">
                        <table class="table" border="0">
                            <thead>
                                <tr>
                                    <%--<th>#</th>--%>
                                    <th>Details</th>
                                    <th></th>
                                    <%--<th style="width: 170px !important"></th>--%>
                                </tr>
                            </thead>

                            <tbody>
                                <asp:PlaceHolder runat="server" ID="phAccDetails"></asp:PlaceHolder>
                                <%--<tr>
                                    <td>1</td>
                                    <td>Phone</td>
                                    <td>
                                        <input type="text" class="form-control" /></td>
                                </tr>
                                <tr>
                                    <td>2</td>
                                    <td>Email</td>
                                    <td>
                                        <input type="text" class="form-control" /></td>
                                </tr>--%>
                                <tr>
                                    <td>
                                        <%--<asp:Button runat="server" ID="btnSaveAcc_Dls" Text="Save" CssClass="btn btn-default flt-left" OnClick="btnSaveAcc_Dls_OnClick" ValidationGroup="save" OnClientClick="if(Page_ClientValidate('save')) return confirm('Are you sure?');" />--%>
                                        <%--<asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="AccountUpdatePanel" DisplayAfter="10" DynamicLayout="True">
                                            <ProgressTemplate>
                                                <img src="<%= Page.ResolveUrl("~")%>Images/GIF/3.gif" alt="wait" width="30" height="30" style="margin-left: 5px" />
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>--%>
                                    </td>
                                    <td></td>
                                    <td style="border: 0 !important"></td>
                                </tr>
                            </tbody>

                        </table>
                    </div>
                </div>

            </div>

            <div id="Navegation_Panel">
                <div id="btn_Group_Panel" class="btn-group btn-breadcrumb" runat="server">
                    <a href="../Home/Home.aspx" class="btn btn-default"><i class="glyphicon glyphicon-home"></i></a>

                </div>
            </div>

            <!-- Modal---start Confirm-->
            <div id="modalPopup_confirm" title="Message" style="display: none">
                <p>
                    <i class="fa fa-warning fa-2x" style="float: left; padding-right: 7px"></i>
                    Are you sure ?
                </p>
                <br />
                <asp:Button runat="server" ID="btnConfirm" CssClass="btn btn-default" Text="yes" Width="80" OnClick="B_29_OnClick" ValidationGroup="save" />
                <a href="#" class="btn btn-default" style="width: 80px" onclick="CloseconfirmDialog()">No</a>
            </div>
            <!-- Modal---End -->

            <!-- Modal---start Check-->
            <div id="modalPopup_Check" title="Message" style="display: none">
                <p>
                    <i class="fa fa-warning fa-2x" style="float: left; padding-right: 7px"></i>
                    Are you sure ?
                </p>
                <br />
                <asp:Button runat="server" ID="btnConfm" CssClass="btn btn-default" Text="save" OnClick="B_29_OnClick" ValidationGroup="save" />
                <asp:LinkButton ID="lnkbtndntsve" runat="server" CssClass="btn btn-default" Text="Don't save" OnClick="lnkbtndntsve_OnClick" CausesValidation="False"></asp:LinkButton>
                <a href="#" class="btn btn-default" onclick="CloseCheckDialog()">Cancel</a>
            </div>
            <!-- Modal---End -->

            <!-- Modal---start Title-->
            <div id="modalPopup_Title" title="Add new title" style="display: none">
                <table class="table">
                    <tr>
                        <td>Title Name:</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox runat="server" ID="txtTitle" CssClass="form-control txt-letter-form-control" MaxLength="7"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_txtTitle" ControlToValidate="txtTitle" Display="Dynamic" ValidationGroup="addTitle" ErrorMessage=" * This Field is Required" ForeColor="Red" />
                            <asp:Label runat="server" ID="lblTitleMsg" ForeColor="red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td dir="rtl">
                            <asp:LinkButton runat="server" ID="btnSaveTitle" CssClass="f-x6" Text="<i class='fa fa-floppy-o' aria-hidden='true'></i>" OnClick="btnSaveTitle_OnClick" ValidationGroup="addTitle" ToolTip="Save"></asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </div>
            <!-- Modal---End -->

            <!-- Modal---start Type-->
            <div id="modalPopup_Type" title="Add new type" style="display: none">
                <table class="table">
                    <tr>
                        <td>Type Name:</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox runat="server" ID="txtContactType" CssClass="form-control txt-letter-form-control" MaxLength="50"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_txtContactType" ControlToValidate="txtContactType" Display="Dynamic" ValidationGroup="addType" ErrorMessage=" * This Field is Required" ForeColor="Red" />
                            <asp:Label runat="server" ID="lblTypeMsg" ForeColor="red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td dir="rtl">
                            <asp:LinkButton runat="server" ID="btnSaveType" CssClass="f-x6" Text="<i class='fa fa-floppy-o' aria-hidden='true'></i>" OnClick="btnSaveType_OnClick" ValidationGroup="addType" ToolTip="Save"></asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </div>
            <!-- Modal---End -->



            <asp:HiddenField ID="hidnMoveFlag" runat="server" />
            <asp:HiddenField ID="accCheckName" runat="server" />
            <asp:HiddenField ID="accUsr" runat="server" />
            <%-- These fields for Update session --%>
            <asp:HiddenField ID="hidnTitle" runat="server" />
            <asp:HiddenField ID="hidnGender" runat="server" />
            <asp:HiddenField ID="hidnViewFlag" runat="server" />

        </ContentTemplate>

    </asp:UpdatePanel>

    <%--<telerik:RadWindow RenderMode="Lightweight" Behaviors="Close" Skin="Bootstrap" VisibleStatusbar="False" ID="modalPopup_confirm" runat="server" Width="300px" Height="130px" AutoSize="False" Title="Are you sure ?" VisibleOnPageLoad="false" Modal="true" OffsetElementID="main" Style="z-index: 100001;">
        <ContentTemplate>
            <div style="text-align: center">
                <br />
                <asp:Button runat="server" ID="btnConfirm" CssClass="btn btn-default" Text="yes" Width="80" OnClick="B_29_OnClick" ValidationGroup="save" />
                <a href="#" class="btn btn-default" style="width: 80px" onclick="closeModalPopup_confirm()">No</a>
            </div>

        </ContentTemplate>
    </telerik:RadWindow>--%>
</asp:Content>
