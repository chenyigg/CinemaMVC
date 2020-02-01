$(function () {
    var timer = setInterval(function () {
        var $Mint = $('#Mint').html();
        var $Sild = $('#Sild').html();

        //秒减一
        $Sild = parseInt($Sild) - 1;
        if ($Sild < 0) {
            $Mint = parseInt($Mint) - 1;
            if ($Mint < 0) {
                clearInterval(timer, 1000);
                $('#Mint').html(0);
                $('#Sild').html(0);
            } else {
                $('#Mint').html($Mint);
                $('#Sild').html(59);
            }
        } else {
            $('#Sild').html($Sild)
        }

        if ($('#Mint').html() == "0" && $('#Sild').html() == "0") {
            alert("超时，该订单已取消!");
            setTimeout(function () {
                window.location.href = "OrderDetails.aspx";
            }, 1000)
        }
    }, 1000)

    $('#Confirm').click(function () {
        console.log($('#OrderID').attr("order"));
        var page = window.open("UrlPage.html?OrderID=" + $('#OrderID').attr("order"), "支付页面", "height=500, width=700, top=260, left=700,resizable=no");
        var timer = setInterval(function () {
            $.ajax({
                url: "OrderPay.aspx?Method=IsPay",
                async: false,
                data: { OI: $('#OrderID').attr("order") },
                success: function (data) {
                    if (data.state == "ok") {
                        clearInterval(timer, 1000);
                        alert("付款成功！");
                        setTimeout(function () {
                            window.location.href = "OrderDetails.aspx";
                        }, 1500)

                        page.close();
                    }
                },
                dataType: "json"
            })
        }, 3000);
    })
})