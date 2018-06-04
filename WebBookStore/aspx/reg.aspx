<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="reg.aspx.cs" Inherits="WebBookStore.webPage.reg" %>

<!DOCTYPE html>
<%--注册页面--%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title><!--#include file="../include/HeaderTitle.html"--></title>
    <!--#include file="../include/baseCss.html"--><%--基础样式--%>
    <link href="../css/reg.css" rel="stylesheet" />
</head>
<body>
    <!--#include file="../include/header.html"--><%--导航栏--%>
    <form id="formreg" runat="server">
        <div id="regmain">
            <div id="regleft">
                <div>
                    <img src="../imgs/login-pic.png" />
                </div>
                <div class="begininfor">
                    注册图书帐号<p class="l_f">简单几步，即可开启您的图书之旅！</p>
                </div>
            </div>
            <div id="regright">
                <div class="inforerror regitem"></div>
                <div class="input-group regitem">
                    <span class="input-group-addon"><span class="glyphicon glyphicon-user"></span></span>
                    <input type="text" class="form-control" placeholder="请输入用户名，一旦注册不可更改" id="txtusername" />
                </div>
                <div class="inforerror regitem"></div>
                <div class="input-group regitem">
                    <span class="input-group-addon"><span class="glyphicon glyphicon-lock"></span></span>
                    <input type="password" class="form-control" placeholder="请输入密码，密码由数字和字母组成" id="txtpwd" />
                </div>
                <div class="inforerror regitem"></div>
                <div class="input-group regitem">
                    <span class="input-group-addon"><span class="glyphicon glyphicon-envelope"></span></span>
                    <input type="text" class="form-control" placeholder="请输入电子邮箱" id="txtemail" />
                </div>
                <div class="inforerror regitem"></div>
                <div class="input-group regitem">
                    <span class="input-group-addon"><span class="glyphicon glyphicon-phone"></span></span>
                    <input type="text" class="form-control" placeholder="请输入手机号" id="txtphonenum" />
                </div>
                <div class="inforerror regitem"></div>
                <div class="input-group regitem">
                    <span class="input-group-addon"><img src="../imgs/qqico.png"/></span>
                    <input type="text" class="form-control" placeholder="请输入QQ号" id="txtqq" />
                </div>
                <div class="inforerror regitem"></div>
                 <div class="input-group regitem">
                    <span class="input-group-addon"><span class="glyphicon glyphicon-check"></span></span>
                    <input type="text" class="form-control" placeholder="请输入验证码" id="txtCheckCode" /><img src="/mg/image.aspx" id="imgcheckcode" onclick="RefreshCkc()"/>
                </div>
                <div class="inforerror regitem"></div>
                <div class="regitem">
                    <button type="button" class="btn" id="btnreg" data-toggle="modal">立即注册</button>
                </div>
            </div>
        </div>
        <!--#include file="../include/footer.html" -->
        <!--#include file="../include/baseJavaScript.html"-->
        <script src="../js/Reg.js"></script>
    </form>
</body>
</html>
