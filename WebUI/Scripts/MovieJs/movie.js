$(function () {
    $('.Sradio:first').attr("checked", "checked");

    //删除所有的选项卡类
    //$('.movie_content_type li').click(function () {
    //    $('.movie_content_type li').removeClass('SaixuanActive1');
    //})
    //给当前的选项卡添加类

    $(".movie_content_type ul li").click(function () {
        $('.movie_content_type li').removeClass('SaixuanActive1');
        $(this).addClass('SaixuanActive1');
        vm1.loadMovieInfo();
    });

    $(".movie_content_area ul li").click(function () {
        $('.movie_content_area li').removeClass('SaixuanActive2');
        $(this).addClass('SaixuanActive2');
        vm1.loadMovieInfo();
    });

    $(".movie_content_years ul li").click(function () {
        $('.movie_content_years li').removeClass('SaixuanActive3');
        $(this).addClass('SaixuanActive3');
        vm1.loadMovieInfo();
    });
});