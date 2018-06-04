//======================================分页控件==========================================
$("#mypager a").click(function () {
    var strUrl = window.location.href.substring(0, window.location.href.indexOf("?"));//获取到浏览器的地址栏?前面的内容
    //alert(strUrl);
    var page = parseInt($(this).attr("title"));
    //alert(page);
    window.location.href = strUrl + "?page=" + page;
});