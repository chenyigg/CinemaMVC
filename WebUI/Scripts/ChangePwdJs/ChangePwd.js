$(function () {
    var key = true;
    $('#OldPwd').blur(function () {
        $.post("ChangePwd.aspx?Method=HasPwd", { UserPwd: $('#OldPwd').val() }, function (data) {
            if (data.state != "ok") {
                $.messager.alert("提示", "您输入的密码与旧密码不匹配哦！", "Info");
                key = false;
                return false;
            } else {
                key = true;
            }
        }, "json")
    });

    $('input[type="submit"]').click(function () {
        if (!key) {
            $.messager.alert("提示", "您输入的密码与旧密码不匹配哦！", "Info");
            return false;
        }
        var pwd = $('.btn1').val();
        var Npwd = $('.btn2').val();
        if (pwd != Npwd) {
            $.messager.alert("提示", "请保持密码一致！", "error");
            return false;
        }
    });
})