var systemDepartment = function () {

    var self = this;
    self.$grid = $('.datagrid');
    self.$tree = $('.zenithsTree');
    self.$searchForm = $('.search-form');
    self.$menu = $('.treeMenu');
    self.$btnTreeEdit = $('#btnTreeEdit');
    self.$btnTreeDelete = $('#btnTreeDelete');
    self.gridObject = null;
    self.selectedNodeId = null;

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
                if (_node != null) return;
                $(this).parent().parent().unmask();

                //加载节点
                var node = self.$tree.tree('find', self.selectedNodeId);
                if (!node) {
                    node = self.$tree.tree('getRoot');
                    zeniths.tree.expandRoot(self.$tree);
                    //展开第一个部门节点
                    if (node.children && node.children.length > 0) {
                        self.$tree.tree('expand', node.children[0].target);
                    }
                }
                self.$tree.tree('select', node.target);
                self.reloadGrid();
            },
            onLoadError: function (result) {
                $(this).parent().parent().unmask();
                zeniths.util.showAlertDangerLoadFailure($(this), zeniths.util.getServerErrorMessage(result));
            },
            onContextMenu: function (e, node) {
                e.preventDefault();
                $(this).tree('select', node.target);
                self.$menu.menu('show', {
                    left: e.pageX,
                    top: e.pageY
                });
            },
            onSelect: function (node) {
                self.selectedNodeId = node.id;
            },
            onDrop: function (target, source, point) {
                zeniths.tree.saveDrop(self.$tree, target, source);
            },
            onClick: function (node) {
                self.$tree.tree('expand', node.target);
                self.searchGrid();
            }
        });
    };

    /**
     * 初始化树右键菜单
     * @returns {} 
     */
    self.initTreeMenu = function () {
        self.$menu.menu({
            onShow: function () {
                var isRoot = self.isRootNode();
                if (isRoot) {
                    $(this).menu('disableItem', self.$btnTreeEdit[0]);
                    $(this).menu('disableItem', self.$btnTreeDelete[0]);
                } else {
                    $(this).menu('enableItem', self.$btnTreeEdit[0]);
                    $(this).menu('enableItem', self.$btnTreeDelete[0]);
                }
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
            zeniths.util.msg('请选择上级部门');
            return false;
        }
        return true;
    };

    /**
     * 判断选中的节点是否是根节点
     * @returns {} 
     */
    self.isRootNode = function () {
        var node = zeniths.tree.getTreeNodeSelected(self.$tree);
        if (node && node.parentid === '-1') {
            return true;
        }
        return false;
    };

    /**
     * 创建树节点
     * @returns {} 
     */
    self.createTreeNode = function ($menuItem) {
        var node = zeniths.tree.getTreeNodeSelected(self.$tree);
        var id = node.id;
        var sortPath = zeniths.tree.getNewNodeSortPath(self.$tree, id);
        var url = $menuItem.data('url') + '?parentId=' + id + '&sortPath=' + sortPath;
        self.showEditDialog(url);
    };

    /**
     * 编辑树节点
     * @returns {} 
     */
    self.editTreeNode = function ($menuItem) {
        if ($menuItem.hasClass('menu-item-disabled')) {
            return;
        }
        var node = zeniths.tree.getTreeNodeSelected(self.$tree);
        var id = node.id;
        var url = $menuItem.data('url') + '?id=' + id;
        self.showEditDialog(url);
    };

    /**
     * 删除树节点
     * @returns {} 
     */
    self.deleteTreeNode = function ($menuItem) {
        if ($menuItem.hasClass('menu-item-disabled')) {
            return;
        }
        zeniths.tree.deleteTreeNode(self.$tree, $menuItem.data('url'), {},
            '选择的 {0} 个部门(包括当前节点及其子节点),确定要删除吗?', function () {
                self.gridObject.clearRows();
            });
    };

    /**
     * 刷新树
     * @returns {} 
     */
    self.reloadTree = function () {
        zeniths.tree.reloadTree(self.$tree);
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
                        self.reloadTree();
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
            $.extend(searchData, { departmentId: node.id });
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
            self.gridObject.reload({ departmentId: node.id });
        }
    };

    /**
     * 显示数据编辑对话框
     * @param {String} url 页面Url 
     * @param {Function} callback 回调函数
     * @returns {} 
     */
    self.showEditDialog = function (url, callback) {
        zeniths.util.dialog(url, 600, 510, {
            callback: function () {
                self.reloadTree();
            }
        });
    };

    /**
     * 显示数据查看对话框
     * @param {String} url 页面Url 
     * @returns {} 
     */
    self.showViewDialog = function (url) {
        zeniths.util.dialog(url, 600, 450);
    };

    /**
     * 创建数据
     * @returns {} 
     */
    self.create = function ($btnItem) {
        var hasSelected = self.hasSelectedTreeNode();
        if (hasSelected === false) return;

        var node = zeniths.tree.getTreeNodeSelected(self.$tree);
        var sortPath = zeniths.tree.getNewNodeSortPath(self.$tree, node.id);
        var url = new URI($btnItem.data('url')).query({ parentId: node.id, sortPath: sortPath }).toString();
        self.showEditDialog(url);
    };

    /**
     * 编辑数据
     * @returns {} 
     */
    self.edit = function ($btnItem) {
        var hasSelected = self.hasSelectedTreeNode();
        if (hasSelected === false) return;

        var url = $btnItem.data('url');
        self.showEditDialog(url);
    };

    /**
     * 删除数据
     * @returns {} 
     */
    self.delete = function ($btnItem) {
        var hasSelected = self.hasSelectedTreeNode();
        if (hasSelected === false) return;

        zeniths.util.deleteBatch($btnItem.data('url'), self.gridObject.getSelectedIds(), function () {
            self.reloadTree();
        });
    };

    /**
     * 绑定按钮事件
     * @returns {} 
     */
    self.initButton = function () {

        $('#btnTreeCreate').on('click', function () {
            self.createTreeNode($(this));
        });

        $('#btnTreeEdit').on('click', function () {
            self.editTreeNode($(this));
        });

        $('#btnTreeDelete').on('click', function () {
            self.deleteTreeNode($(this));
        });

        $('#btnTreeRefresh').on('click', function () {
            self.reloadTree();
        });

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
            self.initTreeMenu();
            self.initGrid();
            self.initSearchForm();
            self.initButton();
            self.initTree();
        }
    };
}();

$(function () {
    systemDepartment.init();
});