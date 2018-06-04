var editor;
//var strLogin = $.cookie("CLoginUser");//获取用户登录的cookie值
$(function () {
    //编辑插件
    editor = CKEDITOR.replace('txtBodyReplyComment',
        {
            filebrowserImageUploadUrl: "/ajax/AjaxUpImage.ashx?cmd=uploadImage",
        });
    //如果没有登录，编辑器则禁用
    $.post("/ajax/HeaderAjax.ashx?cmd=checkislogin", "", function (data) {
        var data = eval('(' + data + ')');
        if (data.Success == false) {
            CKEDITOR.on('instanceReady', function (ev) {
                editor = ev.editor;
                editor.setReadOnly(true);
            });
        }
    });

    //动态设置回复留言的上传内容的高度
    $(".commentReplyItem").each(function (i) {
        var commentReplyItemHeight = 150;//board.css里设置初始值为150px
        var p_Height = 0;
        $("#commentReplyImgId_" + i).children().each(function () {
            p_Height += this.offsetHeight;
            //alert(this.nodeName +":"+ this.offsetHeight);
            //alert("累计高度：" + p_Height);
        });
        //alert(p_Height);
        $("#commentReplyId_" + i).css({ "height": commentReplyItemHeight + p_Height });
        //alert(commentReplyItemHeight + p_Height);
    });

});

//回复评论函数
function PostReplyComment() {
    $.post("/ajax/HeaderAjax.ashx?cmd=checkislogin", "", function (data) {
        var data = eval('(' + data + ')');
        if (data.Success == false) {
            layer.alert("只有已登录用户才有评论权限，请确保您已登录", {
                icon: 5,
                title: "提示"
            });
        }
        else {
            var text = editor.getData();
            var webCommentId = $("#hiddenWebCommentId").val();
            if (text == "") {
                layer.alert("内容不能为空", {
                    icon: 5,
                    title: "提示"
                });
            }
            else {
                if (webCommentId == "0") {
                    layer.alert("未知错误", {
                        icon: 5,
                        title: "提示"
                    });
                }
                else {
                    $.post("/ajax/CommentReplyAjax.ashx?cmd=add", { "webCommentId": webCommentId, "text": text }, function (data) {
                        var data = eval('(' + data + ')');
                        if (data.Success == true) {
                            layer.msg(data.Info, {
                                icon: 6,
                                time: 1000
                            }, function () { window.location.href = "CommentReply.aspx?webCommentId=" + webCommentId; });
                        }
                        else {
                            layer.msg(data.Info, { time: 1000, icon: 5 });
                        }
                    });
                }
            }
        }
    });
}