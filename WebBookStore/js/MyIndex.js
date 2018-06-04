
var regphone = /^0?(13[0-9]|15[012356789]|17[678]|18[0-9]|14[57])[0-9]{8}$/;//验证手机号
var regemail = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/; //验证邮箱
var regqq = /^\d{5,11}$/;//验证qq号
$(function () {
    $.post("/ajax/MyIndexAjax.ashx?cmd=showinfor", "", function (data) {
        var data = eval('(' + data + ')');
        $("#labusername").text(data.UserName);
        $("#useremail").val(data.Email);
        $("#userphone").val(data.Tel);
        $("#userqq").val(data.QQ);
        $("#imgheadpic").attr("src", "../upfile/HeadPic/" + data.HeadPic);
        if (data.gender == "男") {
            $("#uesrgender").eq(0).attr("selected", "selected");
        }
        else {
            $("#uesrgender").eq(1).attr("selected", "selected");
        }
    });
    $("#btnsave").click(function () {
        var email = $.trim($("#useremail").val());
        var phonenum = $.trim($("#userphone").val());
        var qq = $.trim($("#userqq").val());
        var gender = $("#uesrgender").val() == "男" ? "男" : "女";

        if (email == "" || phonenum == "" || qq == "") {
            layer.msg("各项不能为空", { time: 1000, icon: 5 });
            return;
        }
        if (!regphone.test(phonenum)) {
            layer.msg("手机号格式不正确", { time: 1000, icon: 5 });
            return;
        }
        if (!regemail.test(email)) {
            layer.msg("邮箱格式不正确", { time: 1000, icon: 5 });
            return;
        }
        if (!regqq.test(qq)) {
            layer.msg("QQ号格式不正确", { time: 1000, icon: 5 });
            return;
        }
        else {
            $.post("/ajax/MyIndexAjax.ashx?cmd=updateinfor", {
                "email": email,
                "phonenum": phonenum,
                "qq": qq,
                "gender": gender
            }, function (data) {
                var data = eval('(' + data + ')');
                layer.msg(data.Info, { time: 1000, icon: 6 });
            });
        }
    });

    //地址框内容设置
    var sProv = $("#prov").val();
    var sCity = $("#city").val();
    var sDist = $("#dist").val();
    var sTown = $("#town").val();
    var sStreet = $("#street").val();
    $("#prov").change(function () {
        sProv = $("#prov").val();
        $("#Address").val(sProv + "-" + sCity + "-" + sDist + "-" + sTown + "-" + sStreet);
    });
    $("#city").change(function () {
        sCity = $("#city").val();
        $("#Address").val(sProv + "-" + sCity + "-" + sDist + "-" + sTown + "-" + sStreet);
    });
    $("#dist").change(function () {
        sDist = $("#dist").val();
        $("#Address").val(sProv + "-" + sCity + "-" + sDist + "-" + sTown + "-" + sStreet);
    });
    $("#town").change(function () {
        sTown = $("#town").val();
        $("#Address").val(sProv + "-" + sCity + "-" + sDist + "-" + sTown + "-" + sStreet);
    });
    $("#street").change(function () {
        sStreet = $("#street").val();
        $("#Address").val(sProv + "-" + sCity + "-" + sDist + "-" + sTown + "-" + sStreet);
    });

    //设置收货地址
    $("#btnSaveAddress").click(function () {
        var sRecipient = $("#Recipient").val();
        var sAddress = $("#Address").val();
        var sTel = $("#Tel").val();
        if (sRecipient == "" || sAddress == "" || sTel == "") {
            layer.msg("各项不能为空", {
                icon: 5,
                time: 1000
            });
            return;
        }
        else{
            $.post("/ajax/MyIndexAjax.ashx?cmd=addAddress", { "sRecipient": sRecipient, "sAddress": sAddress, "sTel": sTel }, function (data) {
                var data = eval('(' + data + ')');
                if (data.Success == true) {
                    layer.msg(data.Info, {
                        icon: 6,
                        time: 1000
                    }, function () { window.location.href = "Address.aspx"; });
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

    //设置默认地址
    $("#btnSetDefaultAddress").click(function () {
        var iSelId = $("#selAddr option:selected").val();
        //如果不是提示内容---value=0的选项
        if (iSelId > 0) {
            $.post("/ajax/MyIndexAjax.ashx?cmd=setDefaultAddress", { "iSelId": iSelId }, function (data) {
                var data = eval('(' + data + ')');
                if (data.Success == true) {
                    layer.msg(data.Info, {
                        icon: 6,
                        time: 1000
                    });
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

    var iSelId = 0;//当前选择项的数据库表的ID
    //选择地址
    $("#manage_Addr").change(function () {
        //先清零
        $("#manage_Tel").val("");
        $("#manage_Recipient").val("");
        $("#manage_Address").val("");
        iSelId = $("#manage_Addr option:selected").val();
        //如果不是提示内容---value=0的选项
        if (iSelId > 0) {
            //alert(iSelId);
            $.post("/ajax/MyIndexAjax.ashx?cmd=manageAddress", { "iSelId": iSelId }, function (data) {
                var data = eval('(' + data + ')');
                $("#manage_Tel").val(data.Tel);
                $("#manage_Recipient").val(data.Recipient);
                $("#manage_Address").val(data.DetailedAddress);
            });
        }
    });

    //保存收货地址
    $("#manage_btnSaveAddress").click(function () {
        var sManage_Tel = $("#manage_Tel").val();
        var sManage_Recipient = $("#manage_Recipient").val();
        var sManage_Address = $("#manage_Address").val();
        if (sManage_Tel == "" || sManage_Recipient == "" || sManage_Address == "") {
            layer.msg("各项不能为空", { time: 1000, icon: 5 });
            return;
        }
        else {
            $.post("/ajax/MyIndexAjax.ashx?cmd=saveAddress", {
                "sManage_Tel": sManage_Tel,
                "sManage_Recipient": sManage_Recipient,
                "sManage_Address": sManage_Address,
                "iSelId": iSelId
            }, function (data) {
                var data = eval('(' + data + ')');
                if (data.Success == true) {
                    layer.msg(data.Info, {
                        icon: 6,
                        time: 1000
                    });
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

    //删除当前收货地址
    $("#manage_btnDeleteAddress").click(function () {
        if (iSelId > 0) {
            $.post("/ajax/MyIndexAjax.ashx?cmd=deleteAddress", {
                "iSelId": iSelId
            }, function (data) {
                var data = eval('(' + data + ')');
                if (data.Success == true) {
                    layer.msg(data.Info, {
                        icon: 6,
                        time: 1000
                    }, function () { window.location.href = "ManageAddress.aspx"; });
                }
                else {
                    layer.msg(data.Info, {
                        icon: 5,
                        time: 1000
                    });
                }
            });
            
        }
        else {
            layer.msg("请选择待删除选项", { time: 1000, icon: 5 });
            return;
        }
    });
});