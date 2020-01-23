$(function () {
    //获取浏览器传过来的值
    function GetQueryString(name) {
        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
        var r = window.location.search.substr(1).match(reg);
        if (r != null) return unescape(r[2]); return null;
    }
    var myur1 = GetQueryString("MovieID");
    var myur2 = GetQueryString("CinemaID");
    var date = new Date();
    var M1 = date.getMonth() + 1;
    var D1 = date.getDate();
    var K1 = false;
    (function () {
        if (M1 == '1', '3', '5', '7', '8', '10', '12') {
            if (D1 == 31) {
                K1 = true;
            }
        }
        else if (M1 == '2') {
            if (D1 == 28) {
                K1 = true;
            }
        } else {
            if (D1 == 30) {
                K1 = true;
            }
        }
    })();

    var vm = new Vue({
        el: '#content',
        data: {
            //放置月、日
            Month: M1,
            Day: D1,

            //判断年月增长,保证不溢出
            K2: K1,

            //放置单独的电影信息
            MovieName: "",
            MovieType: "",
            MoviePeople: "",
            MovieDuration: "",
            MovieScore: "",

            //放置所有电影信息
            ListMovie: [{
                MovieID: "",
                MovieName: "",
                MovieType: "",
                MoviePeople: "",
                MovieDuration: "",
                MovieCover: "",
                MovieScore: ""
            }],

            //放置所有厅院上映信息
            ListShowDetails: [{
                StartTime: "",
                StopTime: "",
                Language: "",
                OfficeID: "",
                ChipInfoID: "",
                Money: "",
                OfficeName: ""
            }]
        },
        mounted() { this.LoadInfo() },
        methods: {
            LoadInfo() {
                //第一次请求返回厅院信息集合
                $.ajax({
                    url: "/ChooseOffice/FindOfficeInfo",
                    data: { MovieID: myur1, CinemaID: myur2 },
                    success: function (data) {
                        vm.ListShowDetails = data;
                    },
                    dataType: "json",
                    async: true
                })

                //第二次请求返回所有电影信息集合
                $.ajax({
                    url: "/ChooseOffice/FindMovieInfo",
                    data: { MovieID: myur1, CinemaID: myur2 },
                    success: function (data) {
                        vm.ListMovie = data;
                        vm.MovieName = vm.ListMovie[0].MovieName;
                        vm.MovieType = vm.ListMovie[0].MovieType;
                        vm.MoviePeople = vm.ListMovie[0].MoviePeople;
                        vm.MovieDuration = vm.ListMovie[0].MovieDuration;
                        vm.MovieScore = vm.ListMovie[0].MovieScore;
                    },
                    dataType: "json",
                    async: true
                })
            },
            SelectMovieInfo(event) {
                var btn = event.currentTarget;

                $.ajax({
                    url: "/ChooseOffice/FindMovieInfo",
                    data: { MovieID: $(btn).attr("alt"), CinemaID: myur2 },
                    success: function (data) {
                        vm.ListMovie = data;
                        vm.MovieName = vm.ListMovie[0].MovieName;
                        vm.MovieType = vm.ListMovie[0].MovieType;
                        vm.MoviePeople = vm.ListMovie[0].MoviePeople;
                        vm.MovieDuration = vm.ListMovie[0].MovieDuration;
                        vm.MovieScore = vm.ListMovie[0].MovieScore;
                    },
                    dataType: "json",
                    async: true
                })

                //第二次请求返回厅院信息集合
                $.post({
                    url: "/ChooseOffice/FindOfficeInfo",
                    data: { MovieID: $(btn).attr("alt"), CinemaID: myur2 },
                    success: function (data) {
                        vm.ListShowDetails = data;
                    },
                    dataType: "json",
                    async: true
                })
            },
            Tod() {
                $.ajax({
                    url: "/ChooseOffice/FindTod",
                    data: { MovieName: $('#mn').html(), CinemaID: myur2 },
                    success: function (data) {
                        vm.ListShowDetails = data;
                    },
                    dataType: "json",
                })
            },
            Tom(event) {
                $.ajax({
                    url: "/ChooseOffice/FindTom",
                    data: { MovieName: $('#mn').html(), CinemaID: myur2 },
                    success: function (data) {
                        vm.ListShowDetails = data;
                    },
                    dataType: "json",
                })
            }
        }
    })

    $('.header-tooblar__identity--login').mouseover(function () {
        $('.header-tooblar__xiala').css({
            "opacity": "1",
            "transition": "all 600ms linear 0s"
        });
    });
    $('.header-tooblar__xiala ul li').click(function () {
        $('.header-tooblar__xiala').css({
            "opacity": "0",
        });
    });

    $('.header-tooblar__search--text').focus(function () {
        var text = $(this).val();
        if (text == "找影视剧、影院、导演") {
            $(this).val("");
        }
        $(this).css("border-color", "brown");
    });
    $('.header-tooblar__search--text').blur(function () {
        var text = $(this).val();
        if (text == "") {
            $(this).val("找影视剧、影院、导演");
        }
        $(this).css("border-color", "lightgray");
    });

    $('.content--select__brand ul li').click(function () {
        $('.content--select__brand ul li').removeClass("content--select__brand--active");
        $(this).addClass("content--select__brand--active");
    });
    $('.content--select__district ul li').click(function () {
        $('.content--select__district ul li').removeClass("content--select__district--active");
        $(this).addClass("content--select__district--active");
    });
    $('.content--select__special ul li').click(function () {
        $('.content--select__special ul li').removeClass("content--select__special--active");
        $(this).addClass("content--select__special--active");
    });
    $('.content--select__date ul li').click(function () {
        $('.content--select__date ul li').removeClass("content--select__date--active");
        $(this).addClass("content--select__date--active");
    });

    $('.content--bottom__top--dl__dd4 span').click(function () {
        $('.content--bottom__top--dl__dd4 span').removeClass("content--bottom__top--dl__dd4-span");
        $(this).addClass("content--bottom__top--dl__dd4-span");
    });
    $('.content--bottom__bottom--div ul:even').css("background-color", "rgba(219, 215, 215, 0.212)");
});