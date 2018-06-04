
var editor;
$(function () {
    //设置留言板插件
    editor = CKEDITOR.replace('txtBodyComment',
        {
            filebrowserImageUploadUrl: "/ajax/AjaxUpImage.ashx?cmd=uploadImage",
        });
    //如果没有登录，编辑器则禁用
    $.post("/ajax/HeaderAjax.ashx?cmd=checkislogin", "", function (data) {
        var data = eval('(' + data + ')');
        if (data.Success) {
            $("#txtTitle").removeAttr("disabled");
        }
        else {
            CKEDITOR.on('instanceReady', function (ev) {
                editor = ev.editor;
                editor.setReadOnly(true);
            });//设置编辑器只能读不能写
            $("#txtTitle").attr("disabled", "disabled");
        }
    });

    //动态设置留言板的上传图片的高度
    $(".commentItem").each(function (i) {
        var commentItemHeight = 150;//board.css里设置初始值为150px
        var p_Height = 0;
        $("#commentImgId_" + i).children().each(function () {
            p_Height += this.offsetHeight;
        });
        $("#commentId_" + i).css({ "height": commentItemHeight + p_Height });
    });
    
});

//发表评论
function PostComment() {
    //如果已经登录
    $.post("/ajax/HeaderAjax.ashx?cmd=checkislogin", "", function (data) {
        var data = eval('(' + data + ')');
        if (data.Success) {
            var title = $.trim($("#txtTitle").val());
            var text = editor.getData();//获取到编辑器里面的内容
            if (title.length > 40 || title.length < 6) {
                layer.alert("标题长度在6~40之间", {
                    icon: 5,
                    title: "提示"
                });
                return;
            }
            if (title == "" || text == "") {
                layer.alert("各项不能为空", {
                    icon: 5,
                    title: "提示"
                });
                return;
            }
            else {
                $.post("/ajax/CommentAjax.ashx?cmd=add", { "title": title, "text": text }, function (data) {
                    var data = eval('(' + data + ')');
                    if (data.Success == true) {
                        layer.msg(data.Info, {
                            icon: 6,
                            time: 1000
                        }, function () { window.location.href = "Board.aspx"; });
                    }
                    else {
                        layer.msg(data.Info, { time: 1000, icon: 5 });
                    }
                });
            }
        }
        else {
            layer.alert("只有已登录用户才有发帖权限，且确保您已登录", {
                icon: 5,
                title: "提示"
            });
        }
    });
}