/// <reference path="jquery.min.js" />
/// <reference path="cookies.jquery.js" />
var bookAmount = 0;
$(function () {
    //加入购物车功能，需要将数据传递到ShoppingCartAjax.ashx---购物车处理页面
    var bookId = window.location.href.substring(window.location.href.indexOf("=")+1);
    var bookName = $(".publish-info").find("li").eq(0).find("span").text();
    var bookIntroduce = $(".book-intro").text();
    var bookISBN = $(".publish-info").find("li").eq(2).find("span").text();
    var bookPrice = $(".publish-info").find("li").eq(3).find("span").text();
    
    //点击加入购物车执行
    $("#dbnJoin").one("click", function () {
        bookAmount++;
        //alert(bookAmount);
        var bookTotalPrice = bookAmount * bookPrice;//初始图书总价
        //传递到一般处理程序
        $.post("../ajax/ShoppingCartAjax.ashx?cmd=AddInCart", {
            "bookId": bookId,
            "bookName": bookName,
            "bookIntroduce": bookIntroduce,
            "bookISBN": bookISBN,
            "bookPrice": bookPrice,
            "bookAmount": bookAmount,
            "bookTotalPrice": bookTotalPrice
        }, function (data) {
            var data = eval('(' + data + ')');
            if (!data.Success) {
                //提示层
                layer.msg(data.Info); 
            }
        });
    });

});

//评论-图书评论的取消按钮
function PostCancel(BookRemarkId) {
    var iBookRemarkId = Number(BookRemarkId);
    $("#comment-btn-" + iBookRemarkId).css({ "display": "none" });
}
//评论-图书评论的回复按钮显示
function PostCommentShow(BookRemarkId) {
    var iBookRemarkId = Number(BookRemarkId);
    $("#comment-btn-" + iBookRemarkId).css({ "display": "block" });
}

//发表评论
function PostComment() {
    //如果已经登录
    $.post("/ajax/HeaderAjax.ashx?cmd=checkislogin", "", function (data) {
        var data = eval('(' + data + ')');
        var iBookId = window.location.href.substring(window.location.href.indexOf("=") + 1);
        var sBookRemarks = $("#txtBodyComment").val();
        if (data.Success) {
            if (sBookRemarks == "") {
                layer.alert("各项不能为空", {
                    icon: 5,
                    title: "提示"
                });
                return;
            }
            else {
                $.post("/ajax/BookDetailsAjax.ashx?cmd=addBookComment", { "iBookId": iBookId, "sBookRemarks": sBookRemarks }, function (data) {
                    var data = eval('(' + data + ')');
                    if (data.Success == true) {
                        layer.msg(data.Info, {
                            icon: 6,
                            time: 1000
                        }, function () { window.location.href = "bookDetails.aspx?BookId=" + iBookId; });
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

//回复-图书评论的取消按钮
function PostReplyCancel(BookRemarkReplyId) {
    var iBookRemarkReplyId = Number(BookRemarkReplyId);
    $("#comment-reply-btn-" + iBookRemarkReplyId).css({ "display": "none" });
}
//回复-图书评论的回复按钮显示
function PostCommentReplyShow(BookRemarkReplyId) {
    var iBookRemarkReplyId = Number(BookRemarkReplyId);
    $("#comment-reply-btn-" + iBookRemarkReplyId).css({ "display": "block" });
}

//添加图书评论的回复1
function PostCommentReply(BookRemarkId) {
    $.post("/ajax/HeaderAjax.ashx?cmd=checkislogin", "", function (data) {
        var data = eval('(' + data + ')');
        var iBookRemarkId = Number(BookRemarkId);
        var iBookId = window.location.href.substring(window.location.href.indexOf("=") + 1);
        var sBookRemarksReply = $("#comment-txtarea-" + iBookRemarkId).val();
        alert(iBookRemarkId);
        if (data.Success) {
            if (sBookRemarksReply == "") {
                layer.alert("各项不能为空", {
                    icon: 5,
                    title: "提示"
                });
                return;
            }
            else {
                $.post("/ajax/BookDetailsAjax.ashx?cmd=addBookCommentReply",
                    {
                        "iBookRemarkId": iBookRemarkId,
                        "iBookId": iBookId,
                        "sBookRemarksReply": sBookRemarksReply,
                    },
                    function (data) {
                        var data = eval('(' + data + ')');
                        if (data.Success == true) {
                            layer.msg(data.Info, {
                                icon: 6,
                                time: 1000
                            }, function () {
                                window.location.href = "bookDetails.aspx?BookId=" + iBookId;
                            });
                        }
                        else {
                            layer.msg(data.Info, { time: 1000, icon: 5 });
                        }
                    });
            }
        }
        else {
            layer.alert("只有已登录用户才能评论，请确保您已登录", {
                icon: 5,
                title: "提示"
            });
        }
    });
}

//添加图书评论的回复2
function PostCommentReplyAgain(BookRemarkReplyId) {
    $.post("/ajax/HeaderAjax.ashx?cmd=checkislogin", "", function (data) {
        var data = eval('(' + data + ')');
        var iBookRemarkReplyId = Number(BookRemarkReplyId);
        var iBookId = window.location.href.substring(window.location.href.indexOf("=") + 1);
        var sBookRemarksReplyAgain = $("#reply-txtarea-" + iBookRemarkReplyId).val();
        if (data.Success) {
            if (sBookRemarksReplyAgain == "") {
                layer.alert("各项不能为空", {
                    icon: 5,
                    title: "提示"
                });
                return;
            }
            else {
                $.post("/ajax/BookDetailsAjax.ashx?cmd=addBookCommentReplyAgain",
                    {
                        "iBookRemarkReplyId": iBookRemarkReplyId,
                        "sBookRemarksReplyAgain": sBookRemarksReplyAgain,
                    },
                    function (data) {
                        var data = eval('(' + data + ')');
                        if (data.Success == true) {
                            layer.msg(data.Info, {
                                icon: 6,
                                time: 1000
                            }, function () {
                                window.location.href = "bookDetails.aspx?BookId=" + iBookId;
                            });
                        }
                        else {
                            layer.msg(data.Info, { time: 1000, icon: 5 });
                        }
                    });
            }
        }
        else {
            layer.alert("只有已登录用户才能评论，请确保您已登录", {
                icon: 5,
                title: "提示"
            });
        }
    });
}