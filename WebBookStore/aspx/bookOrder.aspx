<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="bookOrder.aspx.cs" Inherits="WebBookStore.aspx.bookOrder" %>

<!DOCTYPE html>
<%--订单页--%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>
        <!--#include file="../include/HeaderTitle.html"-->
    </title>
    <!--#include file="../include/baseCss.html"-->
    <link href="../css/bookOrder.css" rel="stylesheet" />
</head>
<body>
    <!--#include file="../include/header.html"-->
    <form id="formbookOrder" runat="server">
        <%--收货地址--%>
        <div class="GoodsAddress">
            <h1 class="addrHeading">收货地址</h1>
            <div class="listAddress">
               <%=GetAddressHTML() %>
            </div>
        </div>
        
        <%--订单列表--%>
        <h1 class="orderHeading">订单列表</h1>
        <div class="cartMain_hd">
            <ul class="order_lists cartTop">
                <li class="list_con">商品信息</li>
                <li class="list_info">商品参数</li>
                <li class="list_price">单价</li>
                <li class="list_amount">数量</li>
                <li class="list_sum">金额</li>
                <li class="list_op">操作</li>
            </ul>
        </div>
        <%=GetOrderListHTML() %>
    </form>
    <!--#include file="../include/baseJavaScript.html"-->
    <script src="../js/bookOrder.js"></script>
    <script src="../js/carts.js"></script>
</body>
</html>
