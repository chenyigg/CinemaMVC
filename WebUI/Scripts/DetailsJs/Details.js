$(function () {
    //创建实例
    var vm = new Vue({
        el: '#contentbt',
        data: {
            PageNow: "",
            PageCount: "",
            List: [{ CommentID: "", UsersID: "", UserName: "", UserFace: "", CommentInfo1: "", CommentTime: "", MovieID: "", }]
        },
        mounted() {
            this.load();
        },
        methods: {
            //返回当前页及总页数
            getPage() {
                $.ajax(
                    {
                        url: "/Details/GetPageInfo",
                        type: "post",
                        dataType: "json",
                        success: function (datainfo) {
                            console.log(datainfo);
                            vm.PageNow = datainfo.pageNow;
                            vm.PageCount = datainfo.pageCount;
                        }
                    }
                )
            },
            //插入评论
            BtnSayClick() {
                if ($('#txtSee').val().length == 0 || $('#txtSee').val() == "") {
                    return;
                }
                var timer = new Date();
                var year = timer.getFullYear() + "/";
                var month = timer.getMonth() + 1 + "/";
                var day = timer.getDate();
                var str = year + month + day;

                //alert(str);
                $.ajax(
                    {
                        url: "/Details/BtnSayClick",
                        type: "post",
                        data: { CommentInfo1: $('#txtSee').val(), CommentTime: str, MovieID: $('#imgone').attr("title") },
                        dataType: "json",
                        success: function (datainfo) {
                            if (datainfo.status) {
                            }
                            if (datainfo.state == "false") {
                                alert('插入失败');
                                $('#txtSee').val("");
                            }
                            else {
                                alert('插入成功');
                                vm.List = datainfo;
                                vm.$options.methods.getPage();
                                $('#txtSee').val("");
                            }
                        },
                        error: function (xhr, text, errors) {
                            alert("请先登录！");
                            setTimeout(
                                function () {
                                    window.location.href = "/Login/Check"
                                }, 1000
                            )
                        }
                    }
                )
            },
            //加载评论
            load() {
                $.ajax(
                    {
                        url: "/Details/SelectCommentInfo",
                        type: "post",
                        data: { MovieID: $('#imgone').attr("title") },
                        dataType: "json",
                        success: function (datainfo) {
                            console.log(datainfo);
                            vm.List = datainfo;
                            vm.$options.methods.getPage();
                        }
                    }
                )
            },
            //首页评论
            FirstCommentInfo() {
                if (vm.PageNow == 0) {
                    alert("当前无信息！");
                    return;
                }
                if (vm.PageNow === 1) {
                    alert("已经是第一页了哦");
                    return;
                }
                $.ajax({
                    url: "/Details/FirstCommentInfo",
                    data: { MovieID: $('#imgone').attr("title") },
                    dataType: "json",
                    success: function (data) {
                        vm.List = data;
                        vm.$options.methods.getPage();
                    }
                })
            },
            //末页评论
            EndCommentInfo() {
                if (vm.PageNow == 0) {
                    alert("当前无信息！");
                    return;
                }
                if (vm.PageNow === vm.PageCount) {
                    alert("已经是最后一页了哦");
                    return;
                }
                $.ajax({
                    url: "/Details/EndCommentInfo",
                    data: { MovieID: $('#imgone').attr("title") },
                    dataType: "json",
                    success: function (data) {
                        vm.List = data;
                        vm.$options.methods.getPage();
                    }
                })
            },
            //上一页评论
            PrevCommentInfo() {
                if (vm.PageNow == 0) {
                    alert("当前无信息！");
                    return;
                }
                if (vm.PageNow === 1) {
                    alert("已经是第一页了哦");
                    return;
                }
                $.ajax({
                    url: "/Details/PrevCommentInfo",
                    data: { MovieID: $('#imgone').attr("title") },
                    dataType: "json",
                    success: function (data) {
                        vm.List = data;
                        vm.$options.methods.getPage();
                    }
                })
            },
            //下一页评论
            NextCommentInfo() {
                if (vm.PageNow == 0) {
                    alert("当前无信息！");
                    return;
                }
                if (vm.PageNow === vm.PageCount) {
                    alert("已经是最后一页了哦");
                    return;
                }
                $.ajax({
                    url: "/Details/NextCommentInfo",
                    data: { MovieID: $('#imgone').attr("title") },
                    dataType: "json",
                    success: function (data) {
                        vm.List = data;
                        vm.$options.methods.getPage();
                    }
                })
            }
        }
    })

    $('#btnSay').mouseenter(function () {
        $(this).css("cursor", "pointer");
    });
    $('#txtSee').focus(function () {
        $(this).attr("placeholder", "");
        $(this).css("border", "1px solid red");
    });
    $('#txtSee').blur(function () {
        $(this).attr("placeholder", "我也想评论");
        $(this).css("border", "1px solid black");
    });
})