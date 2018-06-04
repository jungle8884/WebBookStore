<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CommentReply.aspx.cs" Inherits="WebBookStore.aspx.CommentReply" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title><!--#include file="../include/HeaderTitle.html"--></title>
    <!--#include file="../include/baseCss.html"-->
    <link href="../css/CommentReply.css" rel="stylesheet" />
</head>
<body>
    <!--#include file="../include/header.html"-->
    <form id="formCommentReply" runat="server">

        <%--当前用户评论--%>
        <%=GetCurrentCommentbyCommentId() %>

        <%--回复评论--%>
        <div id="commentReplyList">
           <%=GetCurrentCommentReplybyCommentId() %>
        </div>

        <%--回复插件--%>
        <div id="comment">
            <div id="re1">发表评论(只有已登录的用户才有发帖权限)</div>
            <div class="input-group reitem">
                <textarea id="txtBodyReplyComment"></textarea>
            </div>
            <div class="input-group reitem">
                <button type="button" class="btn" id="btnsubmit" data-toggle="modal" onclick="PostReplyComment()">提交</button>
            </div>
        </div>
        <input type="hidden" value="0" id="hiddenWebCommentId"  runat="server"/>
    </form>
    <!--#include file="../include/footer.html" -->
    <!--#include file="../include/baseJavaScript.html"-->
    <script src="../ckeditor/ckeditor.js"></script>
    <script src="../js/CommentReply.js"></script>
</body>
</html>
