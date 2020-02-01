$(function () {
    (function () {
        $.post("/Home/HasFace", function (data) {
            if (data.State == "ok") {
                if (data.UserFace == "" || data.UserFace == null || data.UserFace == undefined) {
                    $('#imgFace').attr('src', '../images/UserFaceImg/无头像.png');
                }
                else {
                    $('#imgFace').attr('src', '../images/UserFaceImg/' + data.UserFace);
                }
                $('.header-tooblar__identity--login').addClass('LoginOK');
            } else {
                $('#imgFace').attr('src', '../images/UserFaceImg/暂未登录.jpg');
            }
        }, "json")
    }())

    $('.header-tooblar__search--tubiao').mouseover(function () {
        $(this).css("cursor", "pointer")
    })

    $('.header-tooblar__search--tubiao').click(function () {
        $.ajax({
            url: "Default.aspx?Method=search",
            data: { MovieName: $('#searchM').val() },
            dataType: "json",
            success: function (data) {
                if (data.State == "no") {
                    alert("未找到该电影！");
                } else if (data.State == "ok") {
                    window.location.href = "Details.aspx?MovieID=" + data.MovieID;
                }
            },
        })
    })

    $('.header-tooblar__identity--login').click(function () {
        $.post("/Login/LoginName", function (data) {
            if (data.State == "ok") {
                window.location.href = "User.aspx";
            } else {
                window.location.href = "/Login/Check";
            }
        }, "json")
    })

    $('#imgFace').mouseover(function () {
        $(this).css("cursor", "pointer");
    })

    $('.header-nav__item-text').click(function () {
        console.log($(this).get(0));
        var $src = $(this).html();
        if ($src == "电影") {
            $src = "File.aspx";
        } else if ($src == "热点") {
            $src = "HotPoint.aspx";
        }
        else if ($src == "首页") {
            $src = "/Home/Default";
        }
        else if ($src == "影院") {
            $src = "Cinema.aspx";
        }
        else if ($src == "榜单") {
            $src = "List.aspx";
        }
        else if ($src == "长沙") {
            $src = "Default.aspx";
        }
        location.href = $src;
    })

    $(document).on('mouseover', '.LoginOK', function () {
        $('.header-tooblar__xiala').css({
            "opacity": "1",
            "transition": "all 600ms linear 0s",
            "Cursor": "Pointer"
        }).show();
    })

    $('.header-tooblar__xiala ul li').click(function () {
        $('.header-tooblar__xiala').hide();
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
})