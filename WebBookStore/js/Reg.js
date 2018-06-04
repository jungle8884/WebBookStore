
/*验证用户名*/
var regusername = /^[a-z|A-Z|0-9]{4,16}$/; //验证用户名，由数字和字母组成长度为4~16
var regpwd = /^(?![0-9]+$)(?![a-zA-Z]+$)[0-9A-Za-z]{6,12}$/ //验证密码，由数字和字母组成长度为6~12
var regemail = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/; //验证邮箱
var regphone = /^0?(13[0-9]|15[012356789]|17[678]|18[0-9]|14[57])[0-9]{8}$/;//验证手机号
var regqq = /^\d{5,11}$/;//验证qq号
var checkcode;//验证码
var username;//用户名
var pwd;//密码
var email;//邮箱
var phonenum;//手机号码
var qq;//qq号
var b = true; //总开关，一项不对不通过。
//验证用户名
function CheckRegUsername() {
    $(".inforerror").eq(0).text("");
    username = $.trim($("#txtusername").val());
    if (username == ""){
        $(".inforerror").eq(0).text("用户名不能为空");
        b = false;
        return b;
    }
    if (!regusername.test(username)) {
        $(".inforerror").eq(0).text("用户名由字母，数字组成，且长度为4~16");
        b = false;
        return b;
    }
    //验证数据库中是否已存在此用户
    $.post("../ajax/RegAjax.ashx?cmd=checkusername", { "username": username }, function (data) {
        var data = eval('(' + data + ')');
        if (!data.Success) {
            $(".inforerror").eq(0).text(data.Info);
            b = false;
            return b;
        }
    });
}
/*验证密码*/
function CheckRegPwd() {
    $(".inforerror").eq(1).text("");
    pwd = $.trim($("#txtpwd").val());
    if (pwd == "") {
        $(".inforerror").eq(1).text("密码不能为空");
        b = false;
        return b;
    }
    if (!regpwd.test(pwd)) {
        $(".inforerror").eq(1).text("密码由数字和字母组成，且长度为6~12");
        b = false;
        return b;
    }
}
//验证邮箱
function CheckRegEmail() {
    $(".inforerror").eq(2).text("");
    email = $.trim($("#txtemail").val());
    if (email == "") {
        $(".inforerror").eq(2).text("邮箱不能为空");
        b = false;
        return b;
    }
    if (!regemail.test(email)) {
        $(".inforerror").eq(2).text("邮箱格式不正确");
        b = false;
        return b;
    }
}
/*验证手机号*/
function CheckRegPhone() {
    $(".inforerror").eq(3).text("");
    phonenum = $.trim($("#txtphonenum").val());
    if (phonenum == "") {
        $(".inforerror").eq(3).text("手机号不能为空");
        b = false;
        return b;
    }
    if (!regphone.test(phonenum)) {
        $(".inforerror").eq(3).text("手机号格式输入不正确");
        b = false;
        return b;
    }
}
/*验证qq号*/
function CheckRegQQ() {
    $(".inforerror").eq(4).text("");
    qq = $.trim($("#txtqq").val());
    if (qq == "") {
        $(".inforerror").eq(4).text("QQ号不能为空");
        b = false;
        return b;
    }
    if (!regqq.test(qq)) {
        $(".inforerror").eq(4).text("QQ格式不正确");
        b = false;
        return b;
    }
}
//验证验证码
function CheckRegCheckCode() {
    $(".inforerror").eq(5).text("");
    checkcode = $.trim($("#txtCheckCode").val().toUpperCase());
    if (checkcode == "") {
        $(".inforerror").eq(5).text("验证码不能为空");
        b = false;
        return b;
    }
}
$(function () {
    $("#txtusername").blur(function () { CheckRegUsername(); });
    $("#txtpwd").blur(function () { CheckRegPwd(); });
    $("#txtemail").blur(function () { CheckRegEmail(); });
    $("#txtphonenum").blur(function () { CheckRegPhone(); });
    $("#txtqq").blur(function () { CheckRegQQ(); });
    $("#txtCheckCode").blur(function () { CheckRegCheckCode(); });
    $("#btnreg").click(function () {
        b = true;
        CheckRegUsername();
        CheckRegPwd();
        CheckRegEmail();
        CheckRegPhone();
        CheckRegQQ();
        CheckRegCheckCode();
        if (b) {
            $.post("../ajax/RegAjax.ashx?cmd=reguser",
                { "username": username, "pwd": pwd, "email": email, "phonenum": phonenum, "qq": qq, "checkcode": checkcode },
                function (data) {
                    var data = eval('(' + data + ')');
                    if (data.Success == true) {
                        layer.msg(data.Info, {
                            icon: 6,
                            time: 1000
                        }, function () { window.location.href = "index.aspx"; });
                    }
                    else {
                        if (data.Info == "验证码输入不正确") {
                            $(".inforerror").eq(3).text("验证码输入不正确");
                        }
                        else {
                            layer.alert(data.Info);
                        }
                    }
                });
        }
    });
});
function RefreshCkc() {
    var timenow = new Date().getTime();
    var ccImg = document.getElementById("imgcheckcode");
    if (ccImg) {
        ccImg.src = "/mg/image.aspx?" + timenow;
    }
}