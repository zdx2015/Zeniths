
zeniths.index = function () {

    return {

        /**
         * 绑定树形菜单
         * @returns {} 
         */
        bindTreeMenu: function () {
            var self = this;
            $('.menuTree').tree({
                url: '/Default/Menu',
                animate: true,
                onBeforeLoad: function (node, param) {
                    $(this).parent().parent().mask('正在加载菜单...');
                },
                onLoadSuccess: function (node, data) {
                    $(this).parent().parent().unmask();
                },
                onClick: function (node) {
                    $(this).tree('expand', node.target);
                    if (node.url) {
                        self.createTab(node.text, node.url, node.iconCls);
                    }
                }
            });
        },

        /**
         * 创建并添加标签页,如果这个标题的标签存在，则选择该标签. 否则添加一个标签到标签组.
         * @param {String} title 标题
         * @param {String} url 地址
         * @param {String} icon 图标
         * @returns {} 
         */
        createTab: function (title, url, icon) {
            if ($("#tabs").tabs('exists', title)) {
                $("#tabs").tabs('select', title);
            } else {

                $('#tabs').tabs('addIframeTab', {
                    tab: {
                        title: title,
                        closable: true,
                        icon: icon
                    },
                    iframe: { src: url }
                });
            }
        },
        /**
         * 绑定用户信息
         * @return {}
         */
        bindUserInfo: function () {
            $('#userNameInfo').html('配置管理员(技术部)');
        },

        /**
         * 绑定状态信息
         * @return {}
         */
        bindStatusInfo: function () {
            $('#statusSystem').html(zeniths.util.getOS());
            $('#statusBrowser').html(zeniths.util.getBrowser());
            $('#statusScreen').html(zeniths.util.getScreen());
        },

        init: function () {
            this.bindTreeMenu();
            this.bindUserInfo();
            this.bindStatusInfo();
        }
    }
}();

$(function () {
    //var $body = $(document.body);
    //$body.css({ 'overflow': 'hidden', 'position': 'relative' });
    //var $mask = $('<div style="position:absolute;z-index:2;width:100%;height:100%;background:#ccc;z-index:1000;opacity:0.3;filter:alpha(opacity=30);"><div>').appendTo($body);
    //var $maskMessage = $('<div class="mask-message" style="z-index:3;width:auto;height:16px;line-height:16px;position:absolute;top:50%;left:50%;margin-top:-20px;margin-left:-92px;border:2px solid #d4d4d4;padding: 12px 5px 10px 30px;background: #ffffff url(/plugin/jquery-easyui/css/images/loading.gif) no-repeat scroll 5px center;"> 正在加载 ... </div>').appendTo($body);

    //$.parser.onComplete = function () {
    //    $([$mask[0], $maskMessage[0]]).fadeOut('fast', function () {
    //        $(this).remove();
    //    });
    //    $(document.body).css('display', '');
    //    $(document.body).layout('resize');
    //};

     zeniths.index.init();
})