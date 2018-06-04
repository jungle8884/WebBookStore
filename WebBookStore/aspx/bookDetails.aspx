<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="bookDetails.aspx.cs" Inherits="WebBookStore.aspx.bookDetails" %>

<!DOCTYPE html>
<%--图书详情页面--%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title><!--#include file="../include/HeaderTitle.html"--></title>
    <!--#include file="../include/baseCss.html"--> 
    <link href="../css/bookDetails.css" rel="stylesheet" />
</head>
<body>

    <%--导航栏--%>
    <!--#include file="../include/header.html"--> 
     <form id="formBookDetails" runat="server">
          <div class="container-fluid" id="book_details">

                <%--图书介绍--%>
                <%=GetBooksDetailHtml() %> 

                <%--作者介绍--%>
                <%=GetBookAuthorIntroHtml() %> 

                <%--图书评价--%>
                <div class="row"> 

                    <div class="col-md-9 ">
                         <div class="block">
                             <div class="block-header">
                                 <h3>书评</h3>
                             </div>
                             <div id="comment-block">
                                 <div id="comment-list" class="comment-list">
                                     <ul>
                                         <%=GetBookRemarksHtml() %>
                                     </ul>
                                  </div>
                             </div>
                         </div>
                    </div>

                    <div class="col-md-2 col-md-offset-1">
                        <div class="block" id="publisher">
                            <div class="block-header">
                                <h3>出版信息</h3>
                            </div>
                            <%=GetBookPublisherHtml() %>
                        </div>
                    </div>

                </div> 

                 <%--回复插件--%>
                 <div id="comment">
                    <div id="re1">发表评论(只有已登录的用户才有发帖权限)</div>
                    <div class="input-group reitem">
                        <textarea id="txtBodyComment"></textarea>
                    </div>
                    <div class="input-group reitem">
                        <button type="button" class="btn" id="btnsubmit" data-toggle="modal" onclick="PostComment()">提交</button>
                    </div>
                 </div>
        </div>
     </form>
    <!--#include file="../include/baseJavaScript.html"--> <%--基础js文件--%>
    <script src="../ckeditor/ckeditor.js"></script>
    <script src="../js/BookDetails.js"></script>
</body>
</html>
