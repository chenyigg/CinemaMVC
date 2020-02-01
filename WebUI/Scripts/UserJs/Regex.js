$(function () {
    if ($("#input_1").val() != "") {
        $(".input__label-content--hoshi").hide();
        $(".sss").show();
    }

    document.oncontextmenu = function (e) {
        return false;
    }

    //禁止刷新提交表单
    if (window.history.replaceState) {
        window.history.replaceState(null, null, window.location.href);
    }

    //禁用回车刷新页面
    $(document).on("keypress", "form", function (event) {
        return event.keyCode != 13;
    });

    //判断输入的字符是否为中文
    function IsChinese(str) {
        if (str.length != 0) {
            reg = /^[\u0391-\uFFE5]+$/;
            if (!reg.test(str)) {
                return false;
            }
            return true;
        }
    }

    //判断输入的字符是否为英文字母
    function IsEnglish(str) {
        if (str.length != 0) {
            reg = /^[a-zA-Z]+$/;
            if (!reg.test(str)) {
                return false;
            }
            return true;
        }
    }

    //判断输入的字符是否为手机号
    function IsTel(str) {
        var myreg = /^[1][3,4,5,7,8][0-9]{9}$/;
        if (!myreg.test(str)) {
            return false;
        }
        return true;
    }

    //判断输入的字符是否为身份证号
    function IsIDCard(str) {
        var idcardReg = /^[1-9]\d{7}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])\d{3}$|^[1-9]\d{5}[1-9]\d{3}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])\d{3}([0-9]|X)$/;
        if (!idcardReg.test(str)) {
            return false;
        }
        return true;
    }
    $("#FileUpload1").change(function () {
        var $file = $(this);
        var fileObj = $file[0];
        var windowURL = window.URL || window.webkitURL;
        var dataURL;
        var $img = $("#ContentPlaceHolder1_serverImg");

        if (fileObj && fileObj.files && fileObj.files[0]) {
            dataURL = windowURL.createObjectURL(fileObj.files[0]);
            $img.attr('src', dataURL);
        } else {
            dataURL = $file.val();
            var imgObj = document.getElementById("ContentPlaceHolder1_serverImg");
            // 两个坑:
            // 1、在设置filter属性时，元素必须已经存在在DOM树中，动态创建的Node，也需要在设置属性前加入到DOM中，先设置属性在加入，无效；
            // 2、src属性需要像下面的方式添加，上面的两种方式添加，无效；
            imgObj.style.filter =
                "progid:DXImageTransform.Microsoft.AlphaImageLoader(sizingMethod=scale)";
            imgObj.filters.item("DXImageTransform.Microsoft.AlphaImageLoader").src = dataURL;
        }
    });

    $(".Info").blur(function () {
        if ($(this).val() != "") {
            $(this)
                .next()
                .children()
                .hide();
            $(this)
                .prev()
                .show();
        } else {
            $(this)
                .next()
                .children()
                .show();
            $(this)
                .prev()
                .hide();
        }
    });

    //判断Name格式
    $("#input_1").blur(function () {
        if ($(this).val() != "") {
            if (!IsChinese($(this).val()) && !IsEnglish($(this).val())) {
                $(this)
                    .parent()
                    .next()
                    .show();
                $(this).focus();
                $('#input-6').attr("disabled", "disabled");
                return false;
            } else {
                $(this)
                    .parent()
                    .next()
                    .hide();
                $('#input-6').removeAttr("disabled");
            }
        }
    });

    //判断Sex格式
    $("#input_2").blur(function () {
        if ($(this).val() != "") {
            if ($(this).val() != "男" && $(this).val() != "女") {
                $(this)
                    .parent()
                    .next()
                    .show();
                $(this).focus();
                $('#input-6').attr("disabled", "disabled");
                return false;
            } else {
                $(this)
                    .parent()
                    .next()
                    .hide();
                $('#input-6').removeAttr("disabled");
            }
        }
    });

    //判断Tel格式
    $("#input_3").blur(function () {
        if ($(this).val() != "") {
            if (!IsTel($(this).val())) {
                $(this)
                    .parent()
                    .next()
                    .show();
                $(this).focus();
                $('#input-6').attr("disabled", "disabled");
                return false;
            } else {
                $(this)
                    .parent()
                    .next()
                    .hide();
                $('#input-6').removeAttr("disabled");
            }
        }
    });

    //判断身份证格式
    $("#input_4").blur(function () {
        if ($(this).val() != "") {
            if (!IsIDCard($(this).val())) {
                $(this)
                    .parent()
                    .next()
                    .show();
                $(this).focus();
                $('#input-6').attr("disabled", "disabled");
                return false;
            } else {
                $(this)
                    .parent()
                    .next()
                    .hide();
                $('#input-6').removeAttr("disabled");
            }
        }
    });

    //判断地址格式
    $("#input_5").blur(function () {
        if ($(this).val() != "") {
            if (!IsChinese($(this).val())) {
                $(this)
                    .parent()
                    .next()
                    .show();
                $(this).focus();
                $('#input-6').attr("disabled", "disabled");
                return false;
            } else {
                $(this)
                    .parent()
                    .next()
                    .hide();
                $('#input-6').removeAttr("disabled");
            }
        }
    });
});