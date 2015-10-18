var systemDictionary = function () {

    var self = this;
    self.$grid = $('.datagrid');
    self.$tree = $('.treeDic');
    self.$searchForm = $('.search-form');
    self.$menu = $('.treeMenu');
    self.$btnTreeEdit = $('#btnTreeEdit');
    self.$btnTreeDelete = $('#btnTreeDelete');
    self.gridObject = null;
    self.selectedNodeId = null;

    /*******************************************字典树操作*******************************************/

    /**
     * 初始化字典树控件
     * @returns {} 
     */
    self.initTree = function () {
        self.$tree.tree({
            url: self.$tree.data('url'),
            animate: true,
            onBeforeLoad: function () {
                $(this).parent().parent().mask('正在加载数据字典...');
            },
            onLoadSuccess: function () {
                $(this).parent().parent().unmask();

                //加载节点字典明细
                var node = self.$tree.tree('find', self.selectedNodeId);
                if (!node) {
                    node = self.$tree.tree('getRoot');
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
            onClick: function (node) {
                self.searchGrid();
            }
        });
    };

    /**
     * 初始化字典树邮件菜单
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
     * 是否选中的字典节点
     * @returns {Boolean} 如果选中返回true,否则返回false
     */
    self.hasSelectedTreeNode = function () {
        var node = zeniths.tree.getTreeNodeSelected(self.$tree);
        if (!node) {
            zeniths.util.msg('请选择数据字典');
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
        if (node && node.id === '-1') {
            return true;
        }
        return false;
    };

    /**
     * 显示字典编辑对话框
     * @param {String} url 页面Url 
     * @returns {} 
     */
    self.showDictionaryDialog = function (url) {
        zeniths.util.dialog(url, 600, 370, {
            callback: function () {
                self.reloadDictionary();
            }
        });
    };

    /**
     * 创建数据字典
     * @returns {} 
     */
    self.createDictionary = function ($menuItem) {
        var node = zeniths.tree.getTreeNodeSelected(self.$tree);
        var id = node.id;
        var sortPath = zeniths.tree.getTreeNodeSortPath(self.$tree, id);
        var url = $menuItem.data('url') + '?parentId=' + id + '&sortPath=' + sortPath;
        self.showDictionaryDialog(url);
    };

    /**
     * 编辑数据字典
     * @returns {} 
     */
    self.editDictionary = function ($menuItem) {
        var node = zeniths.tree.getTreeNodeSelected(self.$tree);
        var id = node.id;
        var url = $menuItem.data('url') + '?id=' + id;
        self.showDictionaryDialog(url);
    };

    /**
     * 删除数据字典
     * @returns {} 
     */
    self.deleteDictionary = function ($menuItem) {
        zeniths.tree.deleteTreeNode(self.$tree, $menuItem.data('url'), {},
            '选择的 {0} 个数据字典(包括当前节点及其子节点),对应的数据字典明细也会删除,确定要删除吗?');
    };

    /**
     * 刷新数据字典树
     * @returns {} 
     */
    self.reloadDictionary = function () {
        var node = zeniths.tree.getTreeNodeSelected(self.$tree);
        if (node) {
            self.selectedNodeId = node.id;
        }
        zeniths.tree.reloadTree(self.$tree);
    };

    /*******************************************字典明细操作*******************************************/

    /**
     * 初始化字典明细表格控件
     * @returns {} 
     */
    self.initGrid = function () {
        self.gridObject = self.$grid.datagrid({
            onLoadSuccess: function () {
                //编辑记录事件
                $('.btnRecordEdit').on('click', function () {
                    self.editDetails($(this));
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
                    self.showDetailsViewDialog(url);
                });

                //行双击事件
                $('table>tbody>tr').on('dblclick', function () {
                    var url = $(this).data('url');
                    self.showDetailsViewDialog(url);
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
     * 搜索字典明细表
     * @returns {} 
     */
    self.searchGrid = function () {
        var hasSelected = self.hasSelectedTreeNode();
        if (hasSelected === true) {
            var node = zeniths.tree.getTreeNodeSelected(self.$tree);
            var searchData = zeniths.util.getFormData(self.$searchForm);
            $.extend(searchData, { dictionaryId: node.id });
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
            self.gridObject.reload({ dictionaryId: node.id });
        }
    };

    /**
     * 导出字典明细项表格
     * @returns {} 
     */
    self.ExportGrid = function ($btnItem) {
        var hasSelected = self.hasSelectedTreeNode();
        if (hasSelected === true) {
            var node = zeniths.tree.getTreeNodeSelected(self.$tree);
            var url = new URI($btnItem.data('url')).query({ dictionaryId: node.id }).toString();
            window.location.href = url;
        }
    };

    /**
     * 显示字典明细编辑对话框
     * @param {String} url 页面Url 
     * @returns {} 
     */
    self.showDetailsDialog = function (url) {
        zeniths.util.dialog(url, 600, 455, {
            callback: function () {
                self.reloadGrid();
            }
        });
    };

    /**
     * 显示字典明细查看对话框
     * @param {String} url 页面Url 
     * @returns {} 
     */
    self.showDetailsViewDialog = function (url) {
        zeniths.util.dialog(url, 600, 420);
    };

    /**
     * 创建数据字典明细
     * @returns {} 
     */
    self.createDetails = function ($btnItem) {
        var hasSelected = self.hasSelectedTreeNode();
        if (hasSelected === false) return;

        var node = zeniths.tree.getTreeNodeSelected(self.$tree);
        var url = new URI($btnItem.data('url')).query({ dictionaryId: node.id }).toString();
        self.showDetailsDialog(url);
    };

    /**
     * 编辑数据字典明细
     * @returns {} 
     */
    self.editDetails = function ($btnItem) {
        var hasSelected = self.hasSelectedTreeNode();
        if (hasSelected === false) return;

        var url = $btnItem.data('url');
        self.showDetailsDialog(url);
    };

    /**
     * 删除数据字典明细
     * @returns {} 
     */
    self.deleteDetails = function ($btnItem) {
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

        $('#btnTreeCreate').on('click', function () {
            self.createDictionary($(this));
        });

        $('#btnTreeEdit').on('click', function () {
            self.editDictionary($(this));
        });

        $('#btnTreeDelete').on('click', function () {
            self.deleteDictionary($(this));
        });

        $('#btnTreeRefresh').on('click', function () {
            self.reloadDictionary();
        });


        $('#btnDetailsCreate').on('click', function () {
            self.createDetails($(this));
        });

        $('#btnDetailsDelete').on('click', function () {
            self.deleteDetails($(this));
        });

        $('#btnDetailsRefresh').on('click', function () {
            self.reloadGrid();
        });

        $('#btnDetailsExport').on('click', function () {
            self.ExportGrid($(this));
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
    systemDictionary.init();
});