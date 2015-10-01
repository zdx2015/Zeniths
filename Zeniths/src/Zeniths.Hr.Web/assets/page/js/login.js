/// <reference path="../../jquery/jquery.js" />
/// <reference path="util.js" />
zeniths.login = function () {
    return {
        /**
         * 输出消息
         * @param {String} msg 消息内容
         * @returns {} 
         */
        message: function (msg) {
            $('.message').html(msg);
        },
        /**
         * 用户登录
         * @return {}
         */
        login: function () {
            var self = this;
            var username = $('#username').val();
            var password = $('#password').val();
            if (username == '') {
                self.message('用户名不能为空！');
                return;
            }
            var url = $('form').attr('action');

            $.ajax({
                type: "post",
                url: url,
                data: { username: username, password: password },
                success: function (result) {
                    if (result.success) {
                        self.message("登陆成功正在跳转，请稍候...");
                        window.location.href = '/';
                    } else {
                        self.message(result.message);
                    }
                },
                error: function (e) {
                    self.message(e.responseJSON.message);
                },
                beforeSend: function () {
                    $('form').find("input").attr("disabled", true);
                    self.message("正在登陆处理，请稍候...");
                },
                complete: function () {
                    $('form').find("input").attr("disabled", false);
                }
            });
        },

        init: function () {
            var self = this;
            $('.login_button').on('click', function () {
                self.login();
            });
        }
    }
}();

$(function () {
    zeniths.login.init();
})