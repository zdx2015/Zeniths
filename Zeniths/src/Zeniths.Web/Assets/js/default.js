var index = function () {

    return {

        /**
         * 绑定树形菜单
         * @returns {} 
         */
        bindTreeMenu: function () {
            var self = this;
            var url = $('.menuTree').data('url');
            $('.menuTree').tree({
                url: url,
                animate: true,
                onBeforeLoad: function (node, param) {
                    $(this).parent().parent().mask('正在加载菜单...');
                },
                onLoadSuccess: function (node, data) {
                    $(this).parent().parent().unmask();
                    var menuId = window.location.hash.replace('#', '');
                    if (menuId) {
                        var node = $(this).tree('find', menuId);
                        if (node) {
                            $(this).tree('expandTo', node.target);
                            $(this).tree('select', node.target);
                            self.createTab(node);
                        }
                    }
                },
                onClick: function (node) {
                    $(this).tree('expand', node.target);
                    if (node.url) {
                        self.createTab(node);
                    }
                }
            });
        },

        /**
         * 初始化Tab控件
         * @returns {} 
         */
        initTab: function () {
            $('#tabs').tabs({
                onSelect: function (title, index) {
                    var panel = $(this).tabs('getTab', index);
                    var ops = $(panel).panel("options");
                    window.location.hash = ops.id;
                },
                onClose: function (title, index) {
                    window.location.hash = '';
                }
            });

        },
        /**
         * 创建并添加标签页,如果这个标题的标签存在，则选择该标签. 否则添加一个标签到标签组.
         * @param {node} node TreeNode
         * @returns {} 
         */
        createTab: function (node) {
            var id = node.id;
            var text = node.text;
            var url = node.url;
            var iconCls = node.iconCls;
            if ($("#tabs").tabs('exists', text)) {
                $("#tabs").tabs('select', text);
            } else {

                $('#tabs').tabs('addIframeTab', {
                    tab: {
                        id: id,
                        title: text,
                        closable: true,
                        icon: iconCls,
                        url: url
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
            //$('#userNameInfo').html('配置管理员(技术部)');
        },

        /**
         * 获取当前时间
         * @returns {} 
         */
        setCurrentTime: function () {
            $('#timeInfo').html(new Date().format('yyyy-MM-dd hh:mm:ss'));
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

        /**
         * 绑定按钮事件
         * @returns {} 
         */
        initButton: function () {
            $('#btnUserInfo').on('click', function (e) {
                zeniths.util.dialog($(this).data('url'), 1000, 480);
            });

            //$('btnModifyPassowrd').on('click', function (e) {

            //});

            //$('btnHelper').on('click', function (e) {

            //});

            $('#btnLogout').on('click', function (e) {
                e.preventDefault();
                e.stopPropagation();
                var url = $(this).attr('href');
                zeniths.util.confirm('确定要退出系统吗？', function () {
                    window.location.href = url;
                });
            });

            setInterval(this.setCurrentTime, 1000);
        },

        /**
         * 初始化页面
         * @returns {} 
         */
        init: function () {
            this.initTab();
            this.initButton();
            this.bindTreeMenu();
            this.bindUserInfo();
            this.bindStatusInfo();
        }
    }
}();

$(function () {
    index.init();
})