<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangeHeadPic.aspx.cs" Inherits="web.my.ChangeHeadPic" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>
        <!--#include file="../include/HeaderTitle.html"-->
    </title>
    <!--#include file="../include/baseCss.html"-->
    <link href="../css/MyIndex.css" rel="stylesheet" />
    <!--#include file="../include/baseJavaScript.html"-->
</head>
<body>
    <!--#include file="../include/header.html"-->
    <div id="mymain">
        <!--#include file="../include/myleft.html"-->
        <div id="myright">
            <div class="ucenter-right-head">
                <h3>头像设置</h3>
            </div>
            <div class="inforbox">
                <div class="form-item">
                    <div class="container" id="main">
                        <div class="demo">
                            <p id="swfContainer">
                            </p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript" src="scripts/swfobject.js"></script>
    <script type="text/javascript" src="scripts/fullAvatarEditor.js"></script>
    <script type="text/javascript">
        swfobject.addDomLoadEvent(function () {
            var swf = new fullAvatarEditor("fullAvatarEditor.swf", "expressInstall.swf", "swfContainer", {
                id: 'swf',
                upload_url: 'Upload.ashx?userid=999&username=looselive', //上传接口
                method: 'post', //传递到上传接口中的查询参数的提交方式。更改该值时，请注意更改上传接口中的查询参数的接收方式
                src_upload: 0, //是否上传原图片的选项，有以下值：0-不上传；1-上传；2-显示复选框由用户选择
                avatar_box_border_width: 0,
                avatar_sizes: '100*100',//100*100|50*50|32*32
                avatar_sizes_desc: '100*100像素'//100*100像素|50*50像素|32*32像素
            }, function (msg) {
                switch (msg.code) {
                    case 1:
                        //alert("页面成功加载了组件！");
                        break;
                    case 2:
                        //alert("已成功加载图片到编辑面板。");
                        document.getElementById("upload").style.display = "inline";
                        break;
                    case 3:
                        if (msg.type == 0) {
                            //alert("摄像头已准备就绪且用户已允许使用。");
                        }
                        else if (msg.type == 1) {
                            //alert("摄像头已准备就绪但用户未允许使用！");
                        }
                        else {
                            //alert("摄像头被占用！");
                        }
                        break;
                    case 5:
                        if (msg.type == 0) {
                            layer.msg("修改成功", 1, -1);
                        }
                        break;
                }
            }
            );
            document.getElementById("upload").onclick = function () {
                swf.call("upload");
            };
        });
    </script>
</body>
</html>

