<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Address.aspx.cs" Inherits="web.my.Address" %>

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
                <h3>收货地址设置</h3>
            </div>
            <div class="inforbox" id="setaddr">
                <div class="form-item">
                    <div class="item-label">
                        <label>收货人：</label>
                    </div>
                    <div class="item-cont">
                        <input type="text" class="txt sm w-sm" value="" maxlength="100" id="Recipient" />
                    </div>
                </div>
                <div class="form-item">
                    <div class="item-label">
                        <label>收货地址：</label>
                    </div>
                    <div class="item-cont">
                        <input type="text" class="" value="省" maxlength="100" id="prov" style="width:10%;"/>
                        <input type="text" class="" value="城市" maxlength="100" id="city" style="width:10%;"/>
                        <input type="text" class="" value="区/县" maxlength="100" id="dist" style="width:10%;"/>
                        <input type="text" class="" value="镇" maxlength="100" id="town" style="width:10%;"/>
                        <input type="text" class="" value="街道" maxlength="100" id="street" style="width:38%;"/>
                        <textarea class="" maxlength="200" id="Address" style="display:block; margin-top:5px; width:80%;">
                            省-市-区/县-镇-街道
                        </textarea>
                    </div>
                </div>
                <div class="form-item">
                    <div class="item-label">
                        <label>收货号码：</label>
                    </div>
                    <div class="item-cont">
                        <input type="text" class="txt sm w-sm" value="" maxlength="100" id="Tel" />
                    </div>
                </div>
                <div class="form-item">
                    <div class="item-cont">
                        <input type="button" value="保存收货地址" class="btn btn-primary infoBtn" id="btnSaveAddress" />
                    </div>
                </div>
                <div class="form-item">
                    <div class="item-label">
                        <label>默认地址：</label>
                    </div>
                    <select class="txt sm w-sm" id="selAddr">
                        <option class="txt sm w-sm" value="0">请选择默认收货地址</option>
                        <%=getSelectionHTML() %>
                    </select>
                </div>
                <div class="form-item">
                    <div class="item-cont">
                        <input type="button" value="设置为默认收货地址" class="btn btn-primary infoBtn" id="btnSetDefaultAddress" />
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
