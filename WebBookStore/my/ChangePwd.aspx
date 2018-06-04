<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangePwd.aspx.cs" Inherits="web.my.ChangePwd" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>
        <!--#include file="../include/HeaderTitle.html"-->
    </title>
    <!--#include file="../include/baseCss.html"--> <%--基础样式--%>
    <link href="../css/MyIndex.css" rel="stylesheet" />
    <!--#include file="../include/baseJavaScript.html"--><%--基础js文件--%>
    
    <script src="../js/ChangePwd.js"></script>
</head>
<body>
    <!--#include file="../include/header.html"-->
    <div id="mymain">
        <!--#include file="../include/myleft.html"-->
        <div id="myright">
            <div class="ucenter-right-head">
                <h3>信息管理</h3>
            </div>
            <div class="inforbox">
                <div class="form-item">
                    <div class="item-label">
                        <label>新密码：</label>
                    </div>
                    <div class="item-cont">
                        <input type="password" class="txt sm" value="" maxlength="100" id="txtpwd" />
                    </div>
                </div>
                <div class="form-item">
                    <div class="item-cont">
                        <input type="button" value="保存密码" class="btn btn-primary infoBtn" id="btnsavepwd" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
