<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyIndex.aspx.cs" Inherits="web.my.MyIndex" %>

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
                <h3>信息管理</h3>
            </div>
            <div class="inforbox">
                <div class="form-item">
                    <div class="item-label">
                        <label>头像：</label>
                    </div>
                    <div class="item-cont">
                        <img src="../upfile/HeadPic/man.GIF" id="imgheadpic" />
                    </div>
                </div>
                <div class="form-item">
                    <div class="item-label">
                        <label>用户名：</label>
                    </div>
                    <div class="item-cont">
                        <label id="labusername"></label>
                    </div>
                </div>
                <div class="form-item">
                    <div class="item-label">
                        <label>性别：</label>
                    </div>
                    <div class="item-cont">
                        <select name="gender" id="uesrgender" class="select w-xs">
                            <option value="男">纯爷们</option>
                            <option value="女">萌妹子</option>
                        </select>
                    </div>
                </div>
                <div class="form-item">
                    <div class="item-label">
                        <label>邮箱：</label>
                    </div>
                    <div class="item-cont">
                        <input type="text" class="txt sm w-sm" value="" maxlength="100" id="useremail" />
                    </div>
                </div>
                <div class="form-item">
                    <div class="item-label">
                        <label>手机号：</label>
                    </div>
                    <div class="item-cont">
                        <input type="text" class="txt sm w-sm" value="" maxlength="100" id="userphone" />
                    </div>
                </div>
                <div class="form-item">
                    <div class="item-label">
                        <label>QQ：</label>
                    </div>
                    <div class="item-cont">
                        <input type="text" class="txt sm w-sm" value="" maxlength="100" id="userqq" />
                    </div>
                </div>
                <div class="form-item">
                    <div class="item-cont">
                        <input type="button" value="保存资料" class="btn btn-primary infoBtn" id="btnsave" />
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
