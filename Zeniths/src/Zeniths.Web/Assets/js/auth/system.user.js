var systemUser = function () {

    var self = this;
    self.$grid = $('.datagrid');
    self.$tree = $('.zenithsTree');
    self.$searchForm = $('.search-form');
    self.gridObject = null;

    /*******************************************树操作*******************************************/

    /**
     * 初始化树控件
     * @returns {} 
     */
    self.initTree = function () {
        self.$tree.tree({
            url: self.$tree.data('url'),
            animate: false,
            dnd: true,
            onBeforeLoad: function (node, param) {
                $(this).parent().parent().mask('正在加载部门...');
            },
            onLoadSuccess: function (_node, _data) {
                $(this).parent().parent().unmask();

                //加载节点
                var root = self.$tree.tree('getRoot');
                if (root) {
                    zeniths.tree.expandRoot(self.$tree);

                    self.$tree.tree('select', root.target);
                    self.reloadGrid();
                }
            },
            onLoadError: function (result) {
                $(this).parent().parent().unmask();
                zeniths.util.showAlertDangerLoadFailure($(this), zeniths.util.getServerErrorMessage(result));
            },
            onClick: function (node) {
                self.$tree.tree('expand', node.target);
                self.searchGrid();
            }
        });
    };

    /**
     * 是否选中的父节点
     * @returns {Boolean} 如果选中返回true,否则返回false
     */
    self.hasSelectedTreeNode = function () {
        var node = zeniths.tree.getTreeNodeSelected(self.$tree);
        if (!node) {
            zeniths.util.msg('请选择部门');
            return false;
        }
        return true;
    };

    /*******************************************表格操作*******************************************/

    /**
     * 初始化表格控件
     * @returns {} 
     */
    self.initGrid = function () {
        self.gridObject = self.$grid.datagrid({
            onLoadSuccess: function () {
                //编辑记录事件
                $('.btnRecordEdit').on('click', function () {
                    self.edit($(this));
                });

                //删除记录事件
                $('.btnRecordDelete').on('click', function () {
                    zeniths.util.delete($(this).data('url'), function () {
                        self.reloadGrid();
                    });
                });

                //查看记录事件
                $('.btnRecordView').on('click', function () {
                    var url = $(this).data('url');
                    self.showViewDialog(url);
                });

                //行双击事件
                $('table>tbody>tr').on('dblclick', function () {
                    var url = $(this).data('url');
                    self.showViewDialog(url);
                });
            }
        });
    };

    /**
     * 初始化查询表单(绑定查询方法)
     * @returns {} 
     */
    self.initSearchForm = function () {
        self.$searchForm.find(':submit:first').on('click', function () {
            self.searchGrid();
            return false;
        });
    };

    /**
     * 搜索表格
     * @returns {} 
     */
    self.searchGrid = function () {
        var hasSelected = self.hasSelectedTreeNode();
        if (hasSelected === true) {
            var node = zeniths.tree.getTreeNodeSelected(self.$tree);
            var searchData = zeniths.util.getFormData(self.$searchForm);
            var childIds = zeniths.tree.getChildrenIds(self.$tree, node, true);//获取当前部门和全部子部门
            $.extend(searchData, { departmentIds: childIds.join() });
            self.gridObject.search(searchData);
        }
    };

    /**
     * 刷新表格
     * @returns {} 
     */
    self.reloadGrid = function () {
        var hasSelected = self.hasSelectedTreeNode();
        if (hasSelected === true) {
            var node = zeniths.tree.getTreeNodeSelected(self.$tree);
            var childIds = zeniths.tree.getChildrenIds(self.$tree, node, true);//获取当前部门和全部子部门
            self.gridObject.reload({ departmentIds: childIds.join() });
        }
    };

    /**
     * 显示数据编辑对话框
     * @param {String} url 页面Url 
     * @param {Boolean} isCreate 是否新建
     * @returns {} 
     */
    self.showEditDialog = function (url,isCreate) {
        var height = 500;
        if (isCreate) {
            height = 560;
        }
        zeniths.util.dialog(url, 1000, height, {
            callback: function () {
                self.reloadGrid();
            }
        });
    };

    /**
     * 显示数据查看对话框
     * @param {String} url 页面Url 
     * @returns {} 
     */
    self.showViewDialog = function (url) {
        zeniths.util.dialog(url, 1000, 480);
    };

    /**
     * 创建数据
     * @returns {} 
     */
    self.create = function ($btnItem) {
        var hasSelected = self.hasSelectedTreeNode();
        if (hasSelected === false) return;

        var node = zeniths.tree.getTreeNodeSelected(self.$tree);
        var url = new URI($btnItem.data('url')).query({ departmentId: node.id}).toString();
        self.showEditDialog(url,true);
    };

    /**
     * 编辑数据
     * @returns {} 
     */
    self.edit = function ($btnItem) {
        var hasSelected = self.hasSelectedTreeNode();
        if (hasSelected === false) return;

        var url = $btnItem.data('url');
        self.showEditDialog(url,false);
    };

    /**
     * 删除数据
     * @returns {} 
     */
    self.delete = function ($btnItem) {
        var hasSelected = self.hasSelectedTreeNode();
        if (hasSelected === false) return;

        zeniths.util.deleteBatch($btnItem.data('url'), self.gridObject.getSelectedIds(), function () {
            self.reloadGrid();
        });
    };

    /**
     * 绑定按钮事件
     * @returns {} 
     */
    self.initButton = function () {

        $('#btnCreate').on('click', function () {
            self.create($(this));
        });

        $('#btnDelete').on('click', function () {
            self.delete($(this));
        });

        $('#btnRefresh').on('click', function () {
            self.reloadGrid();
        });

    };

    return {
        /**
         * 页面初始化
         * @returns {} 
         */
        init: function () {
            self.initGrid();
            self.initSearchForm();
            self.initButton();
            self.initTree();
        }
    };
}();

$(function () {
    systemUser.init();
});