/// <reference path="jquery.min.js" />

$(function () {
    $(".defaultTip").each(function () {
        var $pre = $(this).prev();
        //若是默认地址
        if (this.title == "1") {
            $pre.css({ "visibility": "visible" });
        }
        else {
            $pre.css({ "visibility": "hidden" });
        }

        //设置默认地址
        $(this).click(function () {
            var iSelId = $pre.attr("title");
            //alert(iSelId);
            //如果不是提示内容---value=0的选项
            if (iSelId > 0) {
                $.post("/ajax/MyIndexAjax.ashx?cmd=setDefaultAddress", { "iSelId": iSelId }, function (data) {
                    var data = eval('(' + data + ')');
                    if (data.Success == true) {
                        layer.msg(data.Info, {
                            icon: 6,
                            time: 1000
                        }, function () { window.location.href = "bookOrder.aspx"; });
                    }
                    else {
                        layer.msg(data.Info, {
                            icon: 5,
                            time: 1000
                        });
                    }
                });
            }
        });

    });

    //取消订单
    $(".delBtn").click(function () {
        var iBookOrderId = $(this).attr("title");
        //若订单存在
        if (iBookOrderId > 0) {
            $.post("/ajax/ShoppingCartAjax.ashx?cmd=DeleteOrder", { "iBookOrderId": iBookOrderId }, function (data) {
                var data = eval('(' + data + ')');
                if (data.Success == true) {
                    layer.msg(data.Info, {
                        icon: 6,
                        time: 1000
                    }, function () { window.location.href = "bookOrder.aspx"; });
                }
                else {
                    layer.msg(data.Info, {
                        icon: 5,
                        time: 1000
                    });
                }
            });
        }
    });


});