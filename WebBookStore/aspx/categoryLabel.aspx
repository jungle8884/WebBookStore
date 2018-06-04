<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="categoryLabel.aspx.cs" Inherits="WebBookStore.aspx.categoryLabel" %>

<!DOCTYPE html>
<%--分类显示页面--%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title><!--#include file="../include/HeaderTitle.html"--></title>
    <!--#include file="../include/baseCss.html"--> 
    <link href="../css/books.css" rel="stylesheet" />
</head>
<body>
    <!--#include file="../include/header.html"--> 

    <!--#include file="../include/customizedPage.html"-->

    <!--#include file="../include/bookLists_bulletinBoard.html"-->

    <%=GetPagerDivided()%>

    <!--#include file="../include/footer.html" -->

</body>
    <!--#include file="../include/baseJavaScript.html"--><%--基础js文件--%>
    <script src="../js/books.js"></script>
</html>
