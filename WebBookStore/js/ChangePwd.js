/// <reference path="_references.js" />
var regpwd = /^[A-Za-z0-9]+$/ //只能由字母、汉字、数字组成
$(function () {
    $("#btnsavepwd").click(function () {
        var pwd = $.trim($("#txtpwd").val());
        if (pwd == "") {
            layer.msg("密码不能为空", { time: 1000, icon: 5 });
        }
        if (!regpwd.test(pwd)) {
            layer.msg("密码只能由字母、数字组成", { time: 1000, icon: 5 });
        }
        if (pwd.length < 6 || pwd.length > 12) {
            layer.msg("密码为6~12个长度", { time: 1000, icon: 5 });
        }
        $.post("../ajax/ChangePwdAajx.ashx?cmd=pwdchange", {
            "pwd": pwd
        }, function (data) {
            var data = eval('(' + data + ')');
            layer.msg(data.Info, { time: 1000, icon: 6 });
        });
    });
});