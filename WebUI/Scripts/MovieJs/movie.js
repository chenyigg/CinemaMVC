$(function () {
    //初始化数据
    var vm1 = new Vue({
        el: "#movie_content",
        data: {
            PageNow: "",
            PageCount: "",
            List: [{
                MovieCover: "",
                MovieName: "",
                MovieScore: "",
                MovieID: ""
            }]
        },
        mounted() {
            this.loadMovieInfo();
        },
        methods: {
            //返回当前页及总页数
            getPage() {
                $.ajax({
                    url: "/AllMovie/GetPageInfo",
                    type: "post",
                    dataType: "json",
                    success: function (datainfo) {
                        vm1.PageNow = datainfo.PageNow;
                        vm1.PageCount = datainfo.PageCount;
                    }
                })
            },
            //页面加载时分页
            loadMovieInfo() {
                var _this = this;
                $.ajax({
                    type: "Post",
                    url: "/AllMovie/LoadMovieInfo",
                    data: {
                        MovieType: $('.SaixuanActive1').children('span').html() == "全部" ? "" : $('.SaixuanActive1').children('span').html(),
                        MovieArea: $('.SaixuanActive2').children('span').html() == "全部" ? "" : $('.SaixuanActive2').children('span').html(),
                        MovieYears: $('.SaixuanActive3').children('span').html() == "全部" ? "" : Number($('.SaixuanActive3').children('span').html()),
                    },
                    async: false,
                    dataType: "json",
                    success: function (data) {
                        _this.List = data;
                        _this.$options.methods.getPage();
                    }
                });
            },
            //首页电影
            FirstCommentInfo() {
                if (vm1.PageNow == 0) {
                    alert("当前无信息！");
                    return;
                }
                if (vm1.PageNow === 1) {
                    alert("已经是第一页了哦");
                    return;
                }
                $.ajax({
                    type: "Post",
                    url: "/AllMovie/FirstPageMovieInfo",
                    dataType: "json",
                    data: {
                        MovieType: $('.SaixuanActive1').children('span').html() == "全部" ? "" : $('.SaixuanActive1').children('span').html(),
                        MovieArea: $('.SaixuanActive2').children('span').html() == "全部" ? "" : $('.SaixuanActive2').children('span').html(),
                        MovieYears: $('.SaixuanActive3').children('span').html() == "全部" ? "" : Number($('.SaixuanActive3').children('span').html()),
                    },
                    success: function (data) {
                        vm1.List = data;
                        vm1.$options.methods.getPage();
                    }
                })
            },
            //末页电影
            EndCommentInfo() {
                if (vm1.PageNow == 0) {
                    alert("当前无信息！");
                    return;
                }
                if (vm1.PageNow === vm1.PageCount) {
                    alert("已经是最后一页了哦");
                    return;
                }
                $.ajax({
                    type: "Post",
                    url: "/AllMovie/EndPageMovieInfo",
                    dataType: "json",
                    data: {
                        MovieType: $('.SaixuanActive1').children('span').html() == "全部" ? "" : $('.SaixuanActive1').children('span').html(),
                        MovieArea: $('.SaixuanActive2').children('span').html() == "全部" ? "" : $('.SaixuanActive2').children('span').html(),
                        MovieYears: $('.SaixuanActive3').children('span').html() == "全部" ? "" : Number($('.SaixuanActive3').children('span').html()),
                    },
                    success: function (data) {
                        vm1.List = data;
                        vm1.$options.methods.getPage();
                    }
                })
            },
            //上一页电影
            PrevCommentInfo() {
                if (vm1.PageNow == 0) {
                    alert("当前无信息！");
                    return;
                }
                if (vm1.PageNow === 1) {
                    alert("已经是第一页了哦");
                    return;
                }
                $.ajax({
                    type: "Post",
                    url: "/AllMovie/PrevPageMovieInfo",
                    dataType: "json",
                    data: {
                        MovieType: $('.SaixuanActive1').children('span').html() == "全部" ? "" : $('.SaixuanActive1').children('span').html(),
                        MovieArea: $('.SaixuanActive2').children('span').html() == "全部" ? "" : $('.SaixuanActive2').children('span').html(),
                        MovieYears: $('.SaixuanActive3').children('span').html() == "全部" ? "" : Number($('.SaixuanActive3').children('span').html()),
                    },
                    success: function (data) {
                        vm1.List = data;
                        vm1.$options.methods.getPage();
                    }
                })
            },
            //下一页电影
            NextCommentInfo() {
                if (vm1.PageNow == 0) {
                    alert("当前无信息");
                    return;
                }
                if (vm1.PageNow === vm1.PageCount) {
                    alert("已经是最后一页了哦");
                    return;
                }
                $.ajax({
                    type: "Post",
                    url: "/AllMovie/NextPageMovieInfo",
                    dataType: "json",
                    data: {
                        MovieType: $('.SaixuanActive1').children('span').html() == "全部" ? "" : $('.SaixuanActive1').children('span').html(),
                        MovieArea: $('.SaixuanActive2').children('span').html() == "全部" ? "" : $('.SaixuanActive2').children('span').html(),
                        MovieYears: $('.SaixuanActive3').children('span').html() == "全部" ? "" : Number($('.SaixuanActive3').children('span').html()),
                    },
                    success: function (data) {
                        console.log(data);
                        vm1.List = data;
                        vm1.$options.methods.getPage();
                    }
                })
            },
            //正在热映
            zjry() {
                this.loadMovieInfo();
            },
            //即将上映
            jjsy() {
                $.ajax({
                    url: "/AllMovie/jjsy",
                    dataType: "json",
                    success: function (data) {
                        vm1.List = data;
                        vm1.$options.methods.getPage();
                    }
                })
            },
            //经典电影
            jddy() {
                $.ajax({
                    url: "/AllMovie/jddy",
                    dataType: "json",
                    success: function (data) {
                        vm1.List = data;
                        vm1.$options.methods.getPage();
                    }
                })
            }
        }
    })

    $('.Sradio:first').attr("checked", "checked");

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