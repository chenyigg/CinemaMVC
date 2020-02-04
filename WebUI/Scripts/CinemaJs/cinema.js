$(function () {
    var vm = new Vue({
        el: '#content',
        data: {
            //影厅类型
            ListOffice: [{
                OfficeID: "",
                OfficeName: "",
                CinemaID: ""
            }],
            ListCinema: [{
                CinemaID: "",
                CinemaName: "",
                CinemaAddress: "",
                CinemaArea: ""
            }],
            Cinema: [{
                CinemaID: "",
                CinemaName: "",
                CinemaAddress: "",
                CinemaArea: ""
            }]
        },
        mounted() {
            this.LoadInfo();
        },
        methods: {
            //加载
            LoadInfo() {
                $.post("/Cinema/FindOfficeName", function (data) {
                    vm.ListOffice = data;
                }, "json")
                $.post("/Cinema/FindCinema", function (data) {
                    vm.ListCinema = data;
                    vm.Cinema = data;
                }, "json")
            },
            //点击更换样式并筛选
            AreaChange(event) {
                $('.content--select__district ul li').removeClass("content--select__district--active");
                var AreaChange = event.currentTarget;
                $(AreaChange).addClass("content--select__district--active");
                $.post("/Cinema/Filtrate", {
                    CinemaArea: $(AreaChange).children("span").text(),
                    OfficeName: $('.content--select__special--active').children("span").text()
                },
                    function (data) {
                        console.log(data);
                        vm.Cinema = data;
                    },
                    "json"
                )
            },
            //点击更换样式并筛选
            TypeChange(event) {
                $('.content--select__special ul li').removeClass("content--select__special--active");
                var TypeChange = event.currentTarget;
                $(TypeChange).addClass("content--select__special--active");
                $.post("/Cinema/Filtrate", {
                    CinemaArea: $('.content--select__district--active').children("span").text(),
                    OfficeName: $(TypeChange).children("span").text()
                }, function (data) { vm.Cinema = data; }, "json"
                )
            }
        }
    })

    $('.header-nav__link--div').mouseover(function () {
        $('.div').css({
            "opacity": "1",
            "transition": "all 600ms linear 0s"
        });
    }).mouseout(function () {
        $('.div').css({
            "opacity": "0",
            "transition": "all 600ms linear 0s"
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
});