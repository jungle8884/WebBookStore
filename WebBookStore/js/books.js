//分页--配合使用(地址栏)【主要是book.aspx页面用】---具体到某个页面可以根据实际情况修改
$(function () {
    $("#mypager a").click(function () {
        var strUrl = window.location.href.substring(0, window.location.href.indexOf("?"));//获取到浏览器的地址栏?前面的内容
        var page = parseInt($(this).attr("title"));
        if ($(".post-tag").attr("title") == "") {
            window.location.href = strUrl + "?page=" + page;
        }
        else {
            window.location.href = strUrl + "?cate='" + $.cookie("sClassification") + "'&page=" + page;
        }
    });
});