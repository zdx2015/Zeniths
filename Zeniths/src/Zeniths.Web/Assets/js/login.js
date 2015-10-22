var login = function () {

    var self = this;
    self.$account = $('#account');
    self.$password = $('#password');
    self.$button = $(':submit');

    /**
     * 登陆过程
     */
    self.loginCore = function () {
        if (self.$account.val() === '') {
            self.msg('请输入账号')
            return;
        }

        self.$button.attr('disabled', 'disabled');
        self.$button.val('登录中..');
        $.ajax({
            url: self.$button.data('url'),
            data: { account: self.$account.val(), password: self.$password.val() },
            type: "post",
            success: function (result) {
                self.$button.val('登录');
                if (result.success) {
                    self.$button.val('登陆成功,正在跳转...');
                    window.location.href = result.url;
                } else {
                    self.$button.removeAttr('disabled');
                    self.msg(result.message);
                }
            },
            error: function (request, status) {
                self.$button.removeAttr('disabled');
                self.msg(request.responseJSON.ExceptionMessage);
            }
        });
    };
    /**
     * 提示消息
     * @param {String} msg 消息内容
     * @returns {} 
     */
    self.msg = function (msg) {
        self.$button.popover({
            animation: true,
            placement: 'bottom',
            content: '<span style="color:red;font-weight:bold;">' + msg + '</span>',
            html: true,
            trigger: 'manual',
            delay: { show: 10000, hide: 100 }
        });
        self.$button.popover('show');
        setTimeout(function () { self.$button.popover('destroy'); }, 2000);

    };
    /**
     * 绑定按钮事件
     * @returns {} 
     */
    self.initButton = function () {

        self.$button.on('click', function () {
            self.loginCore();
        });

        self.$account.on('keydown', function (e) {
            if (e.keyCode == 13) {
                if ($(this).val().length == 0) {
                    self.msg('请输入账号');
                } else {
                    self.$password.focus();
                }
            }
        });

        self.$password.on('keydown', function (e) {
            if (e.keyCode == 13) {
                self.loginCore();
            } else if (e.keyCode == 27) {
                self.$account.focus();
            }
        });

    };

    return {
        /**
         * 页面初始化
         * @returns {} 
         */
        init: function () {
            self.initButton();
            self.$account.focus();
        }
    };
}();

$(function () {
    login.init();
});