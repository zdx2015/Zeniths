/// <reference path="util.js" />
zeniths.index = function () {

    return {

        /**
         * 绑定树形菜单
         * @returns {} 
         */
        bindTreeMenu: function () {
            var self = this;
            var menus = [{ 'id': 'menu2', text: '连接串管理', 'iconCls': 'icon-ok', 'url': 'demo/edit'},
            { 'id': 'menu3', text: '流程按钮管理', 'iconCls': 'icon-ok', 'url': 'WorkFlowButton'},
            { 'id': 'menu4', text: '流程表单管理', 'iconCls': 'icon-ok', 'url': 'WorkFlowForm'}];

            $('.treeMenu').tree({
                //lines: true,
                animate: true,
                data: menus,
                onClick: function (node) {
                    $(this).tree('toggle', node.target);
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
                //$("#tabs").tabs('add', {
                //    title: title,
                //    content: '<iframe style="width:100%;height:100%;" scrolling="auto" frameborder="0" src="' + url + '"></iframe>',
                //    closable: true,
                //    bodyCls: 'iframenosco',
                //    icon: icon
                //});

                $('#tabs').tabs('addIframeTab', {
                    tab: {
                        title: title,
                        closable: true,
                        icon: icon
                    },
                    //iframe参数用于设置iframe信息，包含：
                    //src[iframe地址],frameBorder[iframe边框,，默认值为0],delay[淡入淡出效果时间]
                    //height[iframe高度，默认值为100%],width[iframe宽度，默认值为100%]
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
    zeniths.index.init();
})