<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageAddress.aspx.cs" Inherits="web.my.ManageAddress" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>
        <!--#include file="../include/HeaderTitle.html"-->
    </title>
    <!--#include file="../include/baseCss.html"-->
    <link href="../css/MyIndex.css" rel="stylesheet" />
</head>
<body>
    <!--#include file="../include/header.html"-->
    <div id="mymain">
        <!--#include file="../include/myleft.html"-->
        <div id="myright">
            <div class="ucenter-right-head">
                <h3>收货地址修改</h3>
            </div>
            <div class="inforbox" id="manageaddr">
                <div class="form-item">
                    <div class="item-label">
                        <label>选择地址：</label>
                    </div>
                    <select class="txt sm w-sm" id="manage_Addr">
                        <option class="txt sm w-sm" value="0">请选择收货地址</option>
                        <%=getSelectionHTML() %>
                    </select>
                </div>
                <div class="form-item">
                    <div class="item-label">
                        <label>收货人：</label>
                    </div>
                    <div class="item-cont">
                        <input type="text" class="txt sm w-sm" value="" maxlength="100" id="manage_Recipient" />
                    </div>
                </div>
                <div class="form-item">
                    <div class="item-label">
                        <label>收货地址：</label>
                    </div>
                    <div class="item-cont">
                        <textarea class="" maxlength="200" id="manage_Address" style="display:block; margin-top:5px; width:80%;"></textarea>
                    </div>
                </div>
                <div class="form-item">
                    <div class="item-label">
                        <label>收货号码：</label>
                    </div>
                    <div class="item-cont">
                        <input type="text" class="txt sm w-sm" value="" maxlength="100" id="manage_Tel" />
                    </div>
                </div>
                <div class="form-item">
                    <div class="item-cont">
                        <input type="button" value="保存地址" class="btn btn-primary infoBtn" id="manage_btnSaveAddress" />
                        <input type="button" value="删除地址" class="btn btn-primary infoBtn" id="manage_btnDeleteAddress" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!--#include file="../include/footer.html" -->
    <!--#include file="../include/baseJavaScript.html"-->
    <script src="../js/MyIndex.js"></script>
</body>
</html>
