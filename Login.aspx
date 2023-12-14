<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Login.aspx.vb" Inherits="Authentication_login"
    MasterPageFile="~/MasterPages/Base.master" EnableEventValidation="false" %>

<%@ MasterType VirtualPath="~/MasterPages/Base.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href="App_Themes/Bank_Theme2/css/custom/style.css" rel="stylesheet" />

    <!-- CSS only -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/css/bootstrap.min.css" rel="stylesheet" />

    <!--------------------------------------------------font------------------------------------------------------------------------>
    <link href="https://fonts.googleapis.com/css2?family=Noto+Sans+JP:wght@300&display=swap" rel="stylesheet" />
    <!-------------------------------------------------Icons------------------------------------------------------------------------------>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.10.3/font/bootstrap-icons.css" />
    <!------------------------------------------------------------------------------------------------------------------------------->
    <script>
        $(document).ready(function () {
            //Side Menu Toggle
            //$('.content').addClass('col-md-10 col-md-offset-2');

            //Theming
            //$('header').remove();
            $('footer').hide();
            var p = localStorage.getItem("themeName");
            if (p == null || p == undefined) {
                $('body').addClass('default-theme');
            }
            else {
                $('body').removeClass('default-theme t1 t2 t3 t4 t5');
                $('body').addClass(p);
            }

        });
    </script>
    <script type="text/javascript">
        $(function () {
           <%-- $("#<%=Login1.ClientID%>_UserName").focus();--%>

            $("#ctl00_ContentPlaceHolder1_UserName").focus();
            $("#UserName").focus();

            $("#LoginButton").click(function () {
                if (typeof (Page_ClientValidate) == 'function') {
                    if (Page_ClientValidate() == false) { return false; }
                }
                freezeScreen();
            });
        });

        function OpenModal(url, diaHeight, dialogWidth) {
            //debugger;
            this.disabled = true;
            //freezeScreen();
            var vReturnValue;
            if (diaHeight == null || diaHeight == "") diaHeight = "300";

            if (url != null) {
                vReturnValue = window.showModalDialog(url, "#1", "dialogHeight: " + diaHeight + "px; dialogWidth: " + dialogWidth + "px; edge: Raised; center: Yes; help: No; resizable: Yes; status: No;");
            }
            else {
                alert("No URL passed to open");
            }
            if (vReturnValue != null && vReturnValue == true)
                return true;
            else {
                //unFreezeScreen();
                return false;
            }

        }


    </script>
    <script>
        //        function show() {
        //  var x = document.getElementById("ctl00_ContentPlaceHolder1_Password");
        //  if (x.type === "password") {
        //    x.type = "text";
        //    document.querySelector("#showimg_eye")
        //  } else {
        //    x.type = "password";
        //    document.querySelector("#showimg_eye")
        //  }
        //}
        document.addEventListener('DOMContentLoaded', function () {
            // Your code here
            function show() {
                var x = document.getElementById("ctl00_ContentPlaceHolder1_Password");
                if (x.type === "password") {
                    x.type = "text";
                } else {
                    x.type = "password";
                }
            }

            // Attach the onclick event handler to the relevant element
            var showImgEye = document.querySelector("#showimg_eye");
            showImgEye.onclick = show;
        });

    </script>
    <style>
        .lang_chang_div {
            margin-left: 351px;
            margin-top: 10px;
            width: 100px;
            border: none;
            background-color: #FFCFAF;
        }
    </style>
    <div class="sk-login row">
        <div>
            <div class="wrapper">
                <div class="cardShadow">
                    <div class="col-xs-12 text-center lang_chang_div">
                        <select id="uxlang" class="form-control" style="width:100px">
                            <option Value="es">Espanol</option>
                            <option Value="en">English</option>
                        </select>
                    </div>
                    <div class="circle circle1"></div>
                    <div class="circle circle2"></div>
                    <div class="block1">
                        <div class="logo athena_logo_div">
                            <img src="App_Themes/Bank_Theme2/images/logo/logo_Athena_new.png" class="athena_logo_img" />
                            <h3 class="athena_login">
                                <asp:Literal ID="Literal1" runat="server" Text="<%$Resources:login_Login %>"> </asp:Literal></h3>
                        </div>
                        <div class="athena_user_pass">
                            <asp:TextBox ID="UserName" runat="server" class="input username_textbox" placeholder="<%$Resources:login_UserName%>"></asp:TextBox>

                            <%-- <input type="text" name="" id="" placeholder="Email">--%>

                            <asp:TextBox ID="Password" runat="server" type="password" TextMode="Password" class="input password_textbox" placeholder="<%$Resources:login_Password%>"></asp:TextBox>
                            <i class="ion-eye" onclick="show()" id="showimg_eye"></i>

                            <%-- <input type="password" name="" id="" placeholder="Password">--%>
                            <%--   <a href="#" style="font-size:11px;">Forgot password ?</a>--%>
                        </div>
                        <div class="login_btn_div">
                            <asp:Button ID="Login1" runat="server" DisplayRememberMe="False" FailureText=" " Text="<%$Resources:login_Log_In %>" class="login_btn" ValidationGroup="login" />
                        </div>
                    </div>
                    <div class="col-xs-12 text-center mac_changepass_login">
                        <asp:TextBox ID="uxMacAddress" runat="server" Width="0px" Text="hhjhjk" Visible="true"></asp:TextBox>
                        <asp:LinkButton ID="uxChangePasswordlnk" runat="server" Text="<%$Resources:login_changePassword %>"></asp:LinkButton>
                    </div>

                </div>

                <div class="trust-powered" style="text-align: center; position: absolute; bottom: 0; left: 0; right: 0;">
                    <span id="uxpoweredlabel" runat="server" visible="false"></span>
                    <span>
                        <asp:Label ID="uxpowered" runat="server" Visible="false" Style="visibility: hidden;"></asp:Label></span>
                    <p style="font-size: 11px;"></p>
                </div>
            </div>
            <div class="col-xs-12 connectioninfo">
                <asp:Label ID="uxSecureInfo" runat="server" Text="" EnableViewState="false" Visible="false"></asp:Label>

                <asp:Label ID="uxException" runat="server" Text="" EnableViewState="false"></asp:Label>
            </div>
        </div>
        <asp:HiddenField ID="ux_rc4_rnd_key" runat="server" />
        <%---------------------------------------------------------------------------------------------------------%>

        <!-- JavaScript Bundle with Popper -->
        <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/js/bootstrap.bundle.min.js"></script>


        <script>
            function show() {
                var x = document.getElementById("txt_password");
                if (x.type === "password") {
                    x.type = "text";
                    document.querySelector("#showimg_eye")
                } else {
                    x.type = "password";
                    document.querySelector("#showimg_eye")
                }
            }

    </script>





        <script id="clientEventHandlersJS" type="text/javascript">  
         <!--  
    if (document.getElementById('<%=uxMacAddress.ClientId%>').value == '') {
                var locator = new ActiveXObject("WbemScripting.SWbemLocator");
                var service = locator.ConnectServer(".");

                // Get the info  
                var properties = service.ExecQuery("SELECT * FROM Win32_NetworkAdapterConfiguration");
                var e = new Enumerator(properties);

                // Iterate
                for (; !e.atEnd(); e.moveNext()) {
                    var p = e.item();
                    if (p.MACAddress != null && p.Caption.indexOf('WAN Miniport') == -1) {
                        document.getElementById('<%=uxMacAddress.ClientId%>').value = p.MACAddress;
                    }
                }
            }
            document.getElementById('<%=uxMacAddress.ClientId%>').style.display = 'none';
        //-->  
    </script>
        <script type="text/javascript">
            $(document).ready(function () {
                $(".head").hide();
                //$("header").addClass("sk-login-header")
                $('.kb-clearhack').hide();
                $('.kb-head-clearhack').hide();
                $('input[type=submit]').addClass("btn c-btn text-center");
                $('input[type=password]').addClass("form-control");
                $('input[type=submit]').css('text-align: center;');

                localStorage["window_scroll"] = 0;
                localStorage['indexid'] = "";
                localStorage['controlID'] = "";
                //TODO
                $(".copyright-row").hide();
            });
    </script>
    </div>
</asp:Content>
