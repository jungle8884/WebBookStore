<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Board.aspx.cs" Inherits="WebBookStore.aspx.Board" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>
        <!--#include file="../include/HeaderTitle.html"-->
    </title>
    <!--#include file="../include/baseCss.html"-->
    <link href="../css/board.css" rel="stylesheet" />
</head>
<body>
    <!--#include file="../include/header.html"-->
    <form id="formComment" runat="server">

        <%--评论列表--%>
        <%=GetWebCommentHtmls()%>

        <%--分页控件--%>
        <%=GetPagerDivided() %>

        <%--评论插件--%>
        <div id="comment">
            <div id="re1">发表评论(只有已登录的用户才有发帖权限)</div>
            <div class="input-group reitem">
                <input type="text" class="form-control" placeholder="请输入标题" id="txtTitle" />
            </div>
            <div class="input-group reitem">
                <textarea id="txtBodyComment"></textarea>
            </div>
            <div class="input-group reitem">
                <button type="button" class="btn" id="btnsubmit" data-toggle="modal" onclick="PostComment()">提交</button>
            </div>
        </div>
    </form>

    <!--#include file="../include/baseJavaScript.html"-->
    <script src="../ckeditor/ckeditor.js"></script>
    <script src="../js/board.js"></script>
    <script src="../js/SamePagerDal.js"></script>

    <!--#include file="../include/footer.html" -->
</body>
</html>
