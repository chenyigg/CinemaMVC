$(function () {
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
            K2: K1
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
});