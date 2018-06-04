
var bookIndexStart = 0; //显示书籍的起始索引
var bookIndexEnd = 7; //显示书籍的末端索引
var bookShowSize = 8; //一页显示多少本书籍
var bookSize = $(".block-item").get().length;//数据库查询到多少条本书
var pageBackwardFlag = false;//关于上一页的标志符

var i = 0; //轮播图索引
var timer; //轮播图的定时器
var picMax = 4;//轮播图最大索引

$(function () {
    //初始状态，只显示前bookShowSize本书
    //alert(bookSize);
    $(".block-item").each(function (i) {
        if (i >= bookIndexStart && i <= bookIndexEnd) {
            $(this).show();
        }
        else {
            $(this).hide();
        }
    });

    //上一页
    $("#backward").click(function () {

        //一页数据不够8本书的处理
        if (pageBackwardFlag) {
            //alert(bookIndexEnd);
            bookIndexEnd = bookIndexEnd + (bookShowSize - (bookIndexEnd % bookShowSize)) - 1;
            pageBackwardFlag = false;
            //alert(bookIndexEnd);
        }

        //数据溢出判断
        if (bookIndexStart <= 0) {
            return;
        }
        else {
            //设置显示区间
            bookIndexStart -= bookShowSize;
            bookIndexEnd -= bookShowSize;
        }
     
        //调试代码
        //alert(bookIndexStart); alert(bookIndexEnd);
        //上一页的执行代码
        $(".block-item").each(function (i) {
            if (i >= bookIndexStart) {
                if (i <= bookIndexEnd) {
                    $(this).show();
                }
                else {//上一页要注意处理前面的数据，所以要放里面
                    $(this).hide();
                }
            }
        });
    });

    //下一页
    $("#forward").click(function () {
        //数据溢出判断
        if (bookIndexEnd + 1 >= bookSize) {
            return;
        }
        else {
            //设置显示区间
            bookIndexStart += bookShowSize;
            bookIndexEnd += bookShowSize;
        }
        
        //一页数据不够8本书的处理
        if (bookSize >= bookIndexStart && bookSize <= bookIndexEnd) {
            bookIndexEnd = bookSize - 1;
            pageBackwardFlag = true;
        }
        //调试代码
        //alert(bookIndexStart); alert(bookIndexEnd);
        //下一页的执行代码
        $(".block-item").each(function (i) {
            if (i >= bookIndexStart) {
                if (i <= bookIndexEnd) {
                    $(this).show();
                }  
            }
            else {//下一页要注意处理后面的数据，所以放外面
                $(this).hide();
            }
        });
    });

    //榜上有名---图书轮播
    $(".ig").eq(0).show().siblings().hide();
    BeginShow();
    $(".tab").hover(function () {
        i = $(this).index();
        ShowPicTab();
        clearInterval(timer);
    }, function () {
        BeginShow();
    });
    $(".btnL").click(function () {
        clearInterval(timer);
        i--;
        if (i == -1) {
            i = 4;
        }
        ShowPicTab();
        BeginShow();
    });

    $(".btnR").click(function () {
        clearInterval(timer);
        i++;
        if (i == 4) {
            i = 0;
        }
        ShowPicTab();
        BeginShow();
    });

    $("#bookShow").hover(function () {
        $(".btnShow").show();
    }, function () {
        $(".btnShow").hide();
    });
});
//当前图片显示，其他隐藏
function ShowPicTab() {
    $(".ig").eq(i).stop(true, true).fadeIn(300).siblings().fadeOut(300);
    $(".tab").eq(i).addClass("bg").siblings().removeClass("bg");
}
//开始定时显示轮播图
function BeginShow() {
    timer = setInterval(function () {
        i++;
        if (i == 4) {
            i = 0;
        }
        ShowPicTab();
    }, 3000);
}