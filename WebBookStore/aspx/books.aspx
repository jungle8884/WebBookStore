<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="books.aspx.cs" Inherits="WebBookStore.aspx.books" %>

<!DOCTYPE html>
<%--所有图书页面--%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>
        <!--#include file="../include/HeaderTitle.html"-->
    </title>
    <!--#include file="../include/baseCss.html"-->
    <link href="../css/books.css" rel="stylesheet" />
</head>
<body>
    <%--导航栏--%>
    <!--#include file="../include/header.html"-->

    <%--自定义标签分类--%>
    <!--#include file="../include/customizedPage.html"-->

    <%--图书列表和公告栏--%>
    <!--#include file="../include/bookLists_bulletinBoard.html"-->

    <%--分页控件--%>
    <%=GetPagerDivided()%>

    <%--底部页面--%>
    <!--#include file="../include/footer.html" -->

    <%--基础js文件--%>
    <!--#include file="../include/baseJavaScript.html"-->
    <script src="../js/books.js"></script>
</body>
</html>
