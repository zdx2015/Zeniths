var systemDictionary = function () {

    var self = this;
    self.$grid = $('.datagrid');
    self.$tree = $('.treeDic');
    self.$searchForm = $('.search-form');
    self.$menu = $('.treeMenu');
    self.gridObject = null;

    /*******************************************字典树操作*******************************************/

    /**
     * 初始化字典树控件
     * @returns {} 
     */
    self.initTree = function () {
        var url = self.$tree.data('url');
        self.$tree.tree({
            url: url,
            animate: true,
            onBeforeLoad: function () {
                $(this).parent().parent().mask('正在加载数据字典...');
            },
            onLoadSuccess: function () {
                $(this).parent().parent().unmask();
            },
            onLoadError: function (result) {
                $(this).parent().parent().unmask();
                zeniths.util.showAlertDanger($(this), '<i class="fa-lg fa fa-warning"></i> 数据加载失败!', result.responseJSON.message, true);
            },
            onContextMenu: function (e, node) {
                e.preventDefault();
                $(this).tree('select', node.target);
                $('#dictionary-menu').menu('show', {
                    left: e.pageX,
                    top: e.pageY
                });
            },
            onClick: function (node) {
                $(this).tree('toggle', node.target);
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
                var $menuEdit = $('#menuEdit');
                var $menuDelete = $('#menuDelete');
                var node = self.getSelectedTreeNode();
                if (node && node.id === '-1') {
                    $(this).menu('disableItem', $menuEdit[0]);
                    $(this).menu('disableItem', $menuDelete[0]);
                } else {
                    $(this).menu('enableItem', $menuEdit[0]);
                    $(this).menu('enableItem', $menuDelete[0]);
                }
            }
        });


    };

    /**
     * 刷新字典树控件
     * @returns {} 
     */
    self.refreshTree = function () {
        self.$tree.tree('reload');
    };

    /**
     * 获取选中的字典节点对象
     * @returns {Node} 
     */
    self.getSelectedTreeNode = function () {
        return self.$tree.tree('getSelected');
    };

    /**
     * 是否选中的字典节点
     * @returns {Boolean} 如果选中返回true,否则返回false
     */
    self.hasSelectedTreeNode = function () {
        var node = self.getSelectedTreeNode();
        if (!node) {
            zeniths.util.msg('请选择数据字典');
            return false;
        }
        return true;
    };

    /**
     * 显示字典编辑对话框
     * @param {String} url 页面Url 
     * @returns {} 
     */
    self.showDictionaryDialog = function (url) {
        zeniths.util.dialog(url, 600, 370, {
            callback: function () {
                self.refreshTree();
            }
        });
    };

    /**
     * 创建数据字典
     * @returns {} 
     */
    self.createDictionary = function (menuItem) {
        var node = self.getSelectedTreeNode();
        var id = node.id;
        var sortPath = zeniths.util.getTreeNodeSortPath(self.$tree, id);
        var url = $(menuItem.target).data('url') + '?parentId=' + id + '&sortPath=' + sortPath;
        self.showDictionaryDialog(url);
    };

    /**
     * 编辑数据字典
     * @returns {} 
     */
    self.editDictionary = function (menuItem) {
        var node = self.getSelectedTreeNode();
        var id = node.id;
        var url = $(menuItem.target).data('url') + '?id=' + id;
        self.showDictionaryDialog(url);
    };

    /**
     * 删除数据字典
     * @returns {} 
     */
    self.deleteDictionary = function () {
        var ids = [];
        ids.push(node.id);
        var msg = '确定要删除选中的节点吗?';

        if (node.children && node.children.length > 0) {
            var childs = self.$tree.tree('getChildren', node.target);
            msg = '选中的节点包含{0}个子节点,确定要删除当前节点及其全部子节点吗?'.format(childs.length);
            $.each(childs, function (i, v) {
                ids.push(v.id);
            });
        }

        zeniths.util.confirm(msg, function (index) {
            zeniths.util.post(url, { id: ids.join() }, function (result) {
                zeniths.util.layerClose(index);
                if (result.success) {
                    self.refreshTree();
                } else {
                    var msg = result.message;
                    zeniths.util.alert(msg);
                }
            });
        });
    };

    /**
     * 刷新数据字典树
     * @returns {} 
     */
    self.refreshDictionary = function () {
        self.refreshTree();
    };

    /*******************************************字典明细操作*******************************************/

    /**
     * 初始化字典明细表格控件
     * @returns {} 
     */
    self.initGrid = function () {
        $('.datagrid').datagrid({
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
            var node = self.getSelectedTreeNode();
            var searchData = zeniths.util.getFormData(self.$searchForm);
            $.extend(searchData, { dictionaryId: node.id });
            self.$grid.datagrid().search(searchData);
        }
    };

    /**
     * 刷新表格
     * @returns {} 
     */
    self.reloadGrid = function () {
        var hasSelected = self.hasSelectedTreeNode();
        if (hasSelected === true) {
            self.$grid.datagrid().reload();
        }
    };

    /**
     * 显示字典明细编辑对话框
     * @param {String} url 页面Url 
     * @returns {} 
     */
    self.showDetailsDialog = function (url) {
        zeniths.util.dialog(url, 600, 450, {
            callback: function () {
                self.reloadGrid();
            }
        });
    };

    /**
     * 创建数据字典明细
     * @returns {} 
     */
    self.createDetails = function ($btnItem) {
        var hasSelected = self.hasSelectedTreeNode();
        if (hasSelected === false) return;

        var node = self.getSelectedTreeNode();
        var url = URI($btnItem.data('url')).search({ dictionaryId: node.id });
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

        zeniths.util.deleteBatch($btnItem.data('url'), self.$grid.datagrid().getSelectedIds(), function () {
            self.reloadGrid();
        });
    };

    /**
     * 绑定按钮事件
     * @returns {} 
     */
    self.initButton = function() {

        //$('#btnTreeCreate').on('click', );
        //$('#btnTreeEdit').on('click', );
        //$('#btnTreeDelete').on('click', );
        //$('#btnTreeRefresh').on('click', );
        
        //$('#btnDetailsCreate').on('click', );
        //$('#btnDetailsDelete').on('click', );
        //$('#btnDetailsRefresh').on('click', );

    };

    return {
        /**
         * 页面初始化
         * @returns {} 
         */
        init: function () {
            self.initTree();
            self.initTreeMenu();
            self.initGrid();
            self.initSearchForm();
            self.initButton();
        }
    };
}();

$(function () {
    systemDictionary.init();
});