<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="WebBookStore.index" %>

<!DOCTYPE html>
<%--首页--%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title><!--#include file="../include/HeaderTitle.html"--></title>
    <%--基础样式--%>
    <!--#include file="../include/baseCss.html"--> 
    <link href="../css/clock.css" rel="stylesheet" />
</head>
<body>
    <%--导航栏--%>
    <!--#include file="../include/header.html"--> 

    <%--自定义标签分类--%>
    <!--#include file="../include/customizedPage.html"--> 

    <%--滚动栏与时钟--%>
    <!--#include file="../include/bestSeller_Clock.html"--> 

    <%--基础js文件--%>
    <!--#include file="../include/baseJavaScript.html"--> 

    <%--时钟js文件--%>
    <!--#include file="../include/ClockJavaScript.html"--> 

    <!--#include file="../include/footer.html" -->
    <script src="../layer/layer.js"></script>
    <script src="../js/index.js"></script>
</body>
</html>
