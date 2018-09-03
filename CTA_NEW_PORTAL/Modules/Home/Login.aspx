<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="CTA_NEW_PORTAL.Modules.Home.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <!--===============================================================================================-->
    <link rel="icon" type="image/png" href="../../Images/Icons/favicon.ico" />
    <!--===============================================================================================-->
    <link href="../../Assets/bootstrap-4.0/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <!--===============================================================================================-->
    <link href="../../Assets/Fonts/font-awesome-4.7.0/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <!--===============================================================================================-->
    <link href="../../CSS/Login/util.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/Login/main.css" rel="stylesheet" type="text/css" />
    <!--===============================================================================================-->

</head>
<body>
     <%--<form id="form1" runat="server">--%>

    <div class="limiter">
        <div class="container-login100">
            <div class="wrap-login100">
                <form class="login100-form validate-form" runat="server" DefaultButton="btnLogin">

                    <span class="login100-form-title p-b-43"><img src="../../Images/Master/CTA%20logos-05.png" width="250" height="80" alt="Markazia" /></span>

                    <div id="tabsContainer">
                        
                        <div id="tabs-1" class="panel1">

                            <span class="login100-form-title p-b-43">Login to continue</span>

                            <div class="wrap-input100 validate-input" data-validate="User Name is required">
                                <%--<input id="txtEmail" runat="server" class="input100" type="text" name="email" />--%>
                                <asp:TextBox ID="txtEmail" runat="server" class="input100 inputval" name="email"></asp:TextBox>
                                <span class="focus-input100"></span>
                                <span class="label-input100">User Name</span>
                            </div>

                            <div class="wrap-input100 validate-input" data-validate="Password is required">
                                <%--<input id="txtPassword" runat="server" class="input100" type="password" name="pass" />--%>
                                <asp:TextBox ID="txtPassword" runat="server" class="input100 inputval" name="pass" TextMode="Password"></asp:TextBox>
                                <span class="focus-input100"></span>
                                <span class="label-input100">Password</span>
                            </div>

                            <div class="flex-sb-m w-full p-t-3 p-b-32">
                                <div class="contact100-form-checkbox">
                                    <input class="input-checkbox100" id="ckb1" runat="server" type="checkbox" name="remember-me" />
                                    <label class="label-checkbox100" for="ckb1">
                                        Remember me
                                    </label>
                                </div>

                                <div>
                                    <a href="#tabs-2" class="txt1 panel1">Forgot Password?</a>
                                </div>
                            </div>

                            <div class="container-login100-form-btn">
                                <%--<button id="btnLogin" runat="server" class="login100-form-btn">
                                Login
                            </button>--%>
                                <asp:Button ID="btnLogin" runat="server" CssClass="login100-form-btn" Text="Login" OnClick="btnLogin_OnClick" />
                            </div>
                            
                            <div class="text-center p-t-46 p-b-20">
                                <%--<span class="txt2">or sign up using
                            </span>--%>
                                <asp:Label ID="lblLoginMsg" runat="server" CssClass="txt3"></asp:Label>
                            </div>

                            <div class="text-center p-t-46 p-b-20">
                                <%--<span class="txt2">or sign up using
                            </span>--%>
                                <%--<a href="#" class="txt1">Register</a>--%>
                            </div>

                        </div>

                        <div id="tabs-2" class="panel1">

                            <span class="login100-form-title p-b-43">Forgot Your Password?</span>
                            
                            <div class="wrap-input100 validate-input" data-validate="User Name is required">
                                <%--<input id="txtEmail" runat="server" class="input100" type="text" name="email" />--%>
                                <asp:TextBox ID="TextBox1" runat="server" class="input100 forgotinputval" name="email"></asp:TextBox>
                                <span class="focus-input100"></span>
                                <span class="label-input100">User Name</span>
                            </div>

                            <br/>

                            <div class="container-login100-form-btn">
                                <%--<button id="btnLogin" runat="server" class="login100-form-btn">
                                Login
                            </button>--%>
                                <asp:Button ID="btnSend" runat="server" CssClass="login100-form-btn" Text="Send" />
                            </div>
                            
                            <div class="flex-sb-m w-full p-t-3 p-b-32">
                                <div class="contact100-form-checkbox">
                                    
                                </div>

                                <div>
                                    <a href="#tabs-1" class="txt1 panel1">Login</a>
                                </div>
                            </div>

                            
                        </div>
                        
                        
                        <div class="login100-form-social flex-c-m m-top">
                            <a href="https://www.facebook.com/ToyotaJordan" class="login100-form-social-item flex-c-m bg1 m-r-5" target="_blank">
                                <i class="fa fa-facebook-f" aria-hidden="true"></i>
                            </a>

                            <a href="https://twitter.com/ToyotaJordan" class="login100-form-social-item flex-c-m bg2 m-r-5" target="_blank">
                                <i class="fa fa-twitter" aria-hidden="true"></i>
                            </a>

                            <a href="https://www.pinterest.com/toyotajordan" class="login100-form-social-item flex-c-m bg3 m-r-5" target="_blank">
                                <i class="fa fa-pinterest" aria-hidden="true"></i>
                            </a>

                            <a href="https://jo.linkedin.com/pub/toyota-jordan/a6/85a/924" class="login100-form-social-item flex-c-m bg4 m-r-5" target="_blank">
                                <i class="fa fa-linkedin" aria-hidden="true"></i>
                            </a>

                            <a href="https://instagram.com/toyotajo/" class="login100-form-social-item flex-c-m bg5 m-r-5" target="_blank">
                                <i class="fa fa-instagram" aria-hidden="true"></i>
                            </a>

                            <a href="https://www.youtube.com/user/ToyotaCTAJordan" class="login100-form-social-item flex-c-m bg6 m-r-5" target="_blank">
                                <i class="fa fa-youtube" aria-hidden="true"></i>
                            </a>
                        </div>

                    </div>
                    
                    <asp:TextBox runat="server" ID="myText"></asp:TextBox>
                </form>

                <div class="login100-more" style="/*background-image: url('../Images/JPG/bg-01.jpg');*/">
                </div>
                
            </div>
        </div>
    </div>

    <!--===============================================================================================-->
    <script src="../../Scripts/JQuery/jquery-3.2.1.min.js"></script>
    <!--===============================================================================================-->
    <script src="../../Scripts/Validation/main.js"></script>
    <script src="../../Scripts/Validation/_main.js"></script>
    <script src="../../Scripts/Validation/_main2.js"></script>
    <!--===============================================================================================-->
    <script src="../../Scripts/Custom/Fliper.js"></script>
    <!--===============================================================================================-->
    <script src="../../Scripts/Custom/Panels.js"></script>

    <script type="application/javascript">
        function getIP(json) {
            document.getElementById("myText").value = json.ip;
            //document.write("My public IP address is: ", json.ip);
        }
    </script>

    <script type="application/javascript" src="https://api.ipify.org?format=jsonp&callback=getIP"></script>


    <%--</form>--%>
</body>
</html>
