/// <reference path="jquery.min.js" />
/// <reference path="../layer/layer.js" />
$(function () {
    $.post("../ajax/HeaderAjax.ashx?cmd=checkislogin", "", function (data) {
        var data = eval('(' + data + ')');
        if (data.Success) {
            $("#nav-center").show();
            $("#nav-register").hide();
        }
        else {
            $("#nav-center").hide();
            $("#nav-register").show();
        }
        //alert($.cookie('UserId'));//测试当前UserId
    });

    //图书和公告栏的标题
    $("#bookPage").click(function () {
        $.cookie("sClassification", "所有图书");//保存cookie值
    });
    //alert($.cookie("sClassification"));
    if ($.cookie("sClassification") != null) {
        $("#classfication").text($.cookie("sClassification"));//设置大标题的值
    }
    //alert($.cookie("sClassification"));
    $(".post-tag").click(function () {
        $.cookie("sClassification", $(this).attr("title"));//设置当前cookie的值
        //alert($.cookie("sClassification"));
    });
});

//设置底部的高度
$(function () {
    //alert($(document).height());
    //alert($(window).height());
    //alert($("body").height());
    //var height = 0;
    //$("body").children().each(function () {
    //    height += this.offsetHeight;
    //});
    //alert(height);
    //$("#footer").css({ "top": height + "px"});
});

//显示登录框
function ShowLoginBox() {
    $("#loginbox").show();
    layer.open({
        type: 1,
        title: false,
        area: ['360px', '300px'],
        content: $("#loginbox")
    });
};

var username = "";
var regusername = /^[a-z|A-Z|0-9]{4,16}$/; //验证用户名，由数字和字母组成长度为4~16
var pwd = "";
var regpwd = /^(?![0-9]+$)(?![a-zA-Z]+$)[0-9A-Za-z]{6,12}$/ //验证密码，由数字和字母组成长度为6~12
var b = true;

//验证用户名
function CheckLoginUserName() {
    $(".inforerror1").eq(0).text("");
    username = $.trim($("#loginuser").val());
    if (username == "") {
        $(".inforerror1").eq(0).text("用户名不能为空");
        b = false;
        return b;
    }
    if (!regusername.test(username)) {
        $(".inforerror1").eq(0).text("用户名由字母，数字组成，且长度为4~16");
        b = false;
        return b;
    }
}
//验证密码
function CheckLoginPwd() {
    $(".inforerror1").eq(1).text("");
    pwd = $.trim($("#loginpwd").val());
    if (pwd == "") {
        $(".inforerror1").eq(1).text("密码不能为空");
        b = false;
        return b;
    }
    if (!regpwd.test(pwd)) {
        $(".inforerror1").eq(1).text("密码由数字和字母组成，且长度为6~12");
        b = false;
        return b;
    }
}
//登录(在后台页面处理是还会将UserId保存到Session中，一边后续使用。)
function Login() {
    b = true;
    CheckLoginUserName();
    CheckLoginPwd();
    if (b) {
        $.post("../ajax/HeaderAjax.ashx?cmd=userlogin", { "username": username, "pwd": pwd }, function (data) {
            var data = eval('(' + data + ')');
            if (data.Success == true) {
                location.reload();
            }
            else {
                $(".inforerror1").eq(1).text(data.Info);
            }
        });
    }
}
/*退出*/
function Exit() {
    //退出后要清除登录信息---在HeaderAjax.ashx页面（注册处理页面）
    $.cookie('CLoginUser', null);
    $.cookie('CLoginUser', null, { path: '/' });
    //退出后要清除UserId---在ShoppingCartAjax页面（用到过）和HeaderAjax.ashx页面（设置）
    $.cookie('UserId', null);
    $.cookie('UserId', null, { path: '/' });
    location.reload();
}

//购物车动画效果
var ipronum = 0;
$(function () {
    $(".dbnJoin").click(function () {
        if (!$(this).hasClass("bg")) {
            $(this).addClass("bg");
            ipronum++;
            var addImg = $(".book-img").find("img");//找到当前点击的按钮对应的图片
            var cloneImg = addImg.clone();//把图片克隆一份
            cloneImg.css({//设置克隆的图片的初始状态
                "width": "250px",
                "height": "250px",
                "position": "absolute",
                "top": addImg.offset().top,
                "left": addImg.offset().left,
                "z-index": 1000,
                "opacity": ".5"
            });
            cloneImg.appendTo($("body")).animate({//把克隆的图片加到网页中去，并执行一个动画
                "width": "50px",
                "height": "50px",
                "top": $("#dcar").offset().top,
                "left": $("#dcar").offset().left,
            }, 1000, function () {
                $("#dprocount").html(ipronum);
                $(this).remove();
            });
        }
        else {
            if (ipronum == 1) {
                //复位
                ipronum = 0;
                //提示层
                layer.msg('更多书籍请到购物车结算页面!');
            }
        }
    });
});

//搜索框
$(function () {
    //动态搜索功能
    var lastTime;//keyup延迟
    $("#search_books").keyup(function (event) {
        //我们可以用jQuery的event.timeStamp来标记时间，这样每次的keyup事件都会修改lastTime的值，lastTime必需是全局变量
        lastTime = event.timeStamp;
        setTimeout(function () {
            //如果时间差为0，也就是你停止输入0.5s之内都没有其它的keyup事件产生，这个时候就可以去请求服务器了
            if (lastTime - event.timeStamp == 0) {
                $("#search_content").html("");//先清除框里的内容
                var searchKeywords = $.trim($("#search_books").val());
                if (searchKeywords != "") {
                    $.post("/ajax/Searcher.ashx?cmd=searchBooks", { "searchKeywords": searchKeywords },
                        function (data) {
                            var data = eval('(' + data + ')');//把json字符串变成js对象
                            $("#search_content").append(data.Info);
                        });
                    //搜索完成后添加样式
                    $("#search_content").addClass("search_content");
                }
            }
        }, 500);
    });
    //光标一处搜索框去除样式
    $("#search_books").blur(function () {
        $("#search_content").removeClass("search_content");
    });
    //设置图书显示大标题
    $("#search_button").click(function () {
        $.cookie("sClassification", $("#search_books").val().toString());//保存cookie值
        //使用完后清除cookie
        $.cookie('search_books', null);
        $.cookie('search_books', null, { path: '/' });
    });
});
//点击函数(点击查询到的列表就显示到搜索框)
function SearchedContent(obj) {
    if (obj) {
        var searchedText = $(obj).text(); 
        $("#search_books").val(searchedText);
        $("#search_content").html("");
    }
}