<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShoppingCart.aspx.cs" Inherits="WebBookStore.aspx.ShoppingCart" %>

<!DOCTYPE html>
<%--购物车页面--%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title><!--#include file="../include/HeaderTitle.html"--><%--标题--%></title>
    <!--#include file="../include/baseCss.html"--><%--基础样式--%>
    <link href="../css/reset.css" rel="stylesheet" />
    <link href="../css/carts.css" rel="stylesheet" />
</head>
<body>
    <!--#include file="../include/header.html"--><%--导航栏--%>
    <section class="cartMain">
        <div id="cart-title"><h1>全部商品</h1></div>
	    <div class="cartMain_hd">
		    <ul class="order_lists cartTop">
			    <li class="list_chk">
				    <!--所有商品全选-->
				    <input type="checkbox" id="all" class="whole_check"/>
				    <label for="all"></label>
				    全选
			    </li>
			    <li class="list_con">商品信息</li>
			    <li class="list_info">商品参数</li>
			    <li class="list_price">单价</li>
			    <li class="list_amount">数量</li>
			    <li class="list_sum">金额</li>
			    <li class="list_op">操作</li>
		    </ul>
	    </div>
        <%--购物车列表--%> 
        <%=GetShoppingCartHtmls() %>
        <%=GetPagerDivided() %>
	    <!--底部-->
	    <div class="bar-wrapper">
		    <div class="bar-right">
			    <div class="piece">已选商品<strong class="piece_num">0</strong>件</div>
			    <div class="totalMoney">共计: <strong class="total_text">0.00</strong></div>
			    <div class="calBtn"><a href="javascript:;">结算</a></div>
		    </div>
	    </div>
    </section>
    <section class="model_bg"></section>
    <section class="my_model">
	    <p class="title">删除宝贝<span class="closeModel">X</span></p>
	    <p>您确认要删除该宝贝吗？</p>
	    <div class="opBtn"><a href="javascript:;" class="dialog-sure">确定</a><a href="javascript:;" class="dialog-close">关闭</a></div>
    </section>

    <!--#include file="../include/baseJavaScript.html"--> 
    <script src="../js/carts.js"></script>
    <script src="../js/SamePagerDal.js"></script>
</body>
</html>
