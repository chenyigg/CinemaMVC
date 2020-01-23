//获取浏览器传过来的值
function GetQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]);
    return null;
}
var chipInfoID = GetQueryString("ChipInfoID");
var officeID = GetQueryString("OfficeID");
var Language = GetQueryString("Language");
if (Language == 'english') Language = "英语";
else Language = "国语";
var Money = GetQueryString("Money");
//初始化数据
var vm = new Vue({
    el: "#chooseSeat_section",
    data: {
        List: [{
            SeatID: "",
            OfficeID: "",
            IsSeat: ""
        }],
        konglie1: 5,
        konglie2: 12,
        SeatIDarray: []
    },
    mounted() {
        this.loadMovieInfo(),
            this.loatSeat(),
            this.loadCinema(),
            this.loadOffice()
    },
    methods: {
        sureChooseSeat() {
            $.ajax({
                type: "Post",
                async: false,
                dataType: "json",
                url: "/ChooseSeat/SureChooseSeat",
                data: {
                    'SeatIDarray': JSON.stringify(vm.SeatIDarray),
                    ChipInfoID: chipInfoID,
                    Money: Money,
                    officeID: officeID
                },
                success: function (data) {
                    alert("选座成功!");
                    setTimeout(function () {
                        window.location.href = "OrderPay.aspx?OrderID=" + data.OrderID;
                    }, 1000)
                },
                Error: function () {
                    alert("购买失败!");
                }
            })
        },
        loatSeat: function () {
            $.ajax({
                type: "Post",
                url: "/ChooseSeat/LoadSeatInfo",
                dataType: "json",
                data: {
                    OfficeID: officeID,
                    chipInfoID: chipInfoID
                },
                success: function (jsondata) {
                    //alert("转化成功");
                    vm.List = jsondata;
                    //alert(vm.List);
                    $.each(jsondata,
                        function (index, value) {
                            if (value.IsSeat == 0 && value.SeatID % 160 % 16 < 8) {
                                this.konglie1 = value.SeatID % 160 % 16;
                                //alert(this.konglie1);
                            } else if (value.IsSeat == 0 && value.SeatID % 160 %
                                16 > 8) {
                                this.konglie2 = value.SeatID % 160 % 16;
                                //alert(this.konglie2);
                            }
                        })
                }
            });
        },
        loadMovieInfo: function () {
            $.ajax({
                type: "Post",
                dataType: "json",
                url: "/ChooseSeat/LoadMovieInfo",
                data: {
                    ChipInfoID: chipInfoID
                },
                success: function (jsondata) {
                    //var jsondata = JSON.parse(data);
                    console.log(jsondata.MovieType);
                    $('.chooseSeat_section_body_right_MovieInfo_topImg').children('img').attr("src", "../images/MovieCover/" + jsondata.MovieCover);
                    $('.chooseSeat_section_body_right_MovieInfo_topText_title').html(jsondata.MovieName);
                    $('.chooseSeat_section_body_right_MovieInfo_topText_type').html("类型：" + jsondata.MovieType);
                    $('.chooseSeat_section_body_right_MovieInfo_topText_des').html("时长：" + jsondata.MovieDuration + "分钟");
                    $('.chooseSeat_section_body_right_MovieInfo_text_banben').html(Language);
                    $('.chooseSeat_section_body_right_MovieInfo_text_piaojia').html(Money);
                }
            });
        },
        loadCinema: function () {
            $.ajax({
                type: "Post",
                //async: false,
                dataType: "json",
                url: "/ChooseSeat/LoadCinema",
                data: {
                    ChipInfoID: chipInfoID
                },
                success: function (jsondata) {
                    $('.chooseSeat_section_body_right_MovieInfo_text_yingyuan').html(jsondata.CinemaName);
                    $('.chooseSeat_section_body_right_MovieInfo_text_changci').html(jsondata.CinemaName);
                }
            });
        },
        loadOffice: function () {
            $.ajax({
                type: "Post",
                dataType: "json",
                url: "/ChooseSeat/LoadOffice",
                data: {
                    ChipInfoID: chipInfoID
                },
                success: function (jsondata) {
                    $('.chooseSeat_section_body_right_MovieInfo_text_yingting').html(jsondata.OfficeName);
                }
            });
        },
        changeSeatBg(event, SeatID) {
            /*5代表第五列，空列，也就是非座位列*/
            /*12代表第十二列，空列，也就是非座位列*/
            //alert(konglie1);
            //alert(konglie2);
            var i = SeatID;
            var zuowei = ""
            if (i % 160 % 16 < vm.konglie1) {
                //alert("1"+SeatID);
                zuowei = (parseInt(SeatID % 160 / 16) + 1) + "排" + (i % 160 % 16) + "座";
                if (i % 160 % 16 == 0) {
                    zuowei = (parseInt(SeatID % 160 / 16)) + "排" + "14" + "座";
                }
                //alert(zuowei);
            } else if (i % 160 % 16 < vm.konglie2) {
                //alert("2"+SeatID);
                zuowei = (parseInt(SeatID % 160 / 16) + 1) + "排" + (i % 160 % 16 - 1) + "座";
                //alert(zuowei);
            } else if (i % 160 % 16 < 16) {
                //alert("3"+SeatID);
                zuowei = (parseInt(SeatID % 160 / 16) + 1) + "排" + (i % 160 % 16 - 2) + "座";
                //alert(zuowei);
            }
            var el = event.currentTarget;
            //alert("当前对象的内容：" + el.src);
            var host = "https://" + window.location.host;

            var piao = $('.seatCheckInfo').html();
            if (el.src == host + "/images/ChooseSeatImg/seat.png") {
                el.src = host + "/images/ChooseSeatImg/seatChecked.png";
                piao = piao + "  " + zuowei;
                $('.seatCheckInfo').html(piao);
                vm.SeatIDarray.push(SeatID);
            } else if (el.src == host + "/images/ChooseSeatImg/seatChecked.png") {
                el.src = host + "/images/ChooseSeatImg/seat.png";
                piao = piao.replace(zuowei, "");
                $('.seatCheckInfo').html(piao);
                //删除数组中指定元素
                vm.SeatIDarray.splice($.inArray(SeatID, vm.SeatIDarray), 1);
            }
        }
    }
})

var timer = setInterval(function () {
    if (vm.SeatIDarray != null) {
        $('#Sum').html("￥" + (vm.SeatIDarray.length * Money));
    }
}, 500);