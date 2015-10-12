/**
* 模块管理
*/
var Menu = function () {

    //私有变量区域
    var treeControl = $("#ModuleGrid");
    var dialogControl = $("#ModuleDialog");
    var formControl = $("#ModuleEditForm");

    var getListUrl = '/Auth/SystemMenu/GetList';
    var saveUrl = '/Auth/SystemMenu//Save';
    var getUrl = '/Auth/SystemMenu//Get';
    var deleteUrl = '/Auth/SystemMenu//Delete';
    var exportUrl = '/Auth/SystemMenu//Export';
    var saveParentUrl = '/Auth/SystemMenu//SaveParent';
    var updateSortPathUrl = '/Auth/SystemMenu//UpdateSortPath';
    var idField = "Id";
    var dropParentId;
     
    /**
    * 表格初始化
    */
    var initGrid = function () {
        xci.app.initTreeGrid(treeControl, idField, function (selectedRow) {
            if (selectedRow.children) {
                treeControl.treegrid('toggle', selectedRow[idField]);
            } else {
                editData();
            }
        }, null, true, {
            url: getListUrl,
            toolbar: '#ModuleGridToolbar',
            columns: [
                [
                    { field: 'Name', title: '菜单名称', width: 200 },
                    { field: 'Code', title: '菜单编码', width: 150 },
                    { field: 'WebCls', title: '图标', width: 200 },
                    { field: 'WebUrl', title: '链接地址', width: 250 },
                    { field: 'IsEnabled', title: '有效', align: 'center', fixed: true, formatter: xci.app.boolColumnFormatter, width: 50 },
                    { field: 'IsPublic', title: '公共', align: 'center', fixed: true, formatter: xci.app.boolColumnFormatter, width: 50 },
                    { field: 'IsExpand', title: '展开', align: 'center', fixed: true, formatter: xci.app.boolColumnFormatter, width: 50 },
                    { field: 'WebOpenMode', title: '打开方式', align: 'center', fixed: true, width: 100 },
                    { field: 'Note', title: '备注', width: 300 }
                ]
            ],
            onBeforeDrop: function (targetRow, sourceRow) {
                dropParentId = sourceRow._parentId;
            },
            onDrop: function (targetRow, sourceRow, point) {
                xci.app.onTreeGridDrop(treeControl, targetRow, sourceRow, point,
                    saveParentUrl, updateSortPathUrl, dropParentId);
            }
        });
    };

    /**
     * 初始化按钮事件
     */
    var initButtonEvent = function () {
        $('#ModuleBtnSave').on('click', saveData);
        $('#ModuleBtnClose').on('click', closeDialog);
        $('#ModuleSelectIcon').on('click', showIconDialog);
 
        $('#ModuleBtnNewRoot').on('click', createRootData);
        $('#ModuleBtnNew').on('click', createData);
        $('#ModuleBtnEdit').on('click', editData);
        $('#ModuleBtnDelete').on('click', deleteData);
        $('#ModuleBtnRefresh').on('click', refreshData);
        $('#ModuleBtnExport').on('click', exportData);
    };
    
    /**
    * 新建数据
    */
    var createData = function () {
        xci.app.createData(dialogControl, '新建模块', function () {
            xci.app.resetFormData(formControl);
            setParentName(treeControl.treegrid("getSelected"));
            setSortPath(formControl.find('input[name=ParentId]').val());
            xci.app.loadFormData(formControl, { IsEnabled: true });
            setEditIconCls();
        });
    };

    /**
    * 新建根节点数据
    */
    var createRootData = function () {
        xci.app.createData(dialogControl, '新建根模块', function () {
            xci.app.resetFormData(formControl);
            setParentName(null);
            setSortPath(formControl.find('input[name=ParentId]').val());
            xci.app.loadFormData(formControl, { IsEnabled: true });
            setEditIconCls();
        });
    };

    /**
    * 编辑数据
    */
    var editData = function () {
        xci.app.editDataGridData(treeControl, dialogControl, '编辑模块', function (selectedRow) {
            xci.app.resetFormData(formControl);
            setParentName(treeControl.treegrid("getParent", selectedRow.Id));
            xci.app.postData(getUrl, { id: selectedRow.Id }, function (result) {
                xci.app.loadFormData(formControl, result.data);
                setEditIconCls(result.data);
            });
        });
    };


    /**
    * 删除数据
    */
    var deleteData = function () {
        xci.app.deleteTreeGridData(treeControl, deleteUrl, function () {
            refreshData();
        });
    };

    /**
    * 导出excel
    */
    var exportData = function () {
        xci.app.exportData(exportUrl);
    };

    /**
    * 刷新数据
    */
    var refreshData = function () {
        xci.app.refreshTreeGridData(treeControl);
    };

    /**
    * 保存表单数据
    */
    var saveData = function () {
        xci.app.submitData(formControl, saveUrl, true, function (data) {
            xci.app.addCheckBoxValue(formControl, data);
            formControl.find('input[name=WebCls]').val($(formControl.find('input[name=iconCls]')).val());
        }, function () {
            closeDialog();
            refreshData();
        });
    };

    /**
    * 关闭编辑对话框
    */
    var closeDialog = function () {
        xci.app.closeDialog(dialogControl);
    };

    /**
    * 设置编辑窗口父节点
    * @param parentRow 父节点行对象
    */
    var setParentName = function (parentRow) {
        xci.app.setParentName(parentRow, treeControl,
            '#ModuleParentName', '#ModuleEditForm input[name=ParentId]');
    };

    /**
    * 设置新增项的排序路径
    * @param parentId
    */
    var setSortPath = function (parentId) {
        xci.app.setSortPath(parentId, treeControl, '#ModuleEditForm input[name=SortPath]');
    };

    /**
    * 显示图标对话框
    */
    var showIconDialog = function () {
        var selectBtn = formControl.find('#ModuleSelectIcon');
        xci.app.showIconDialog(function(cls) {
            selectBtn.linkbutton({ iconCls: cls });
            formControl.form('load', { iconCls: cls });
        });
    };
    
    /**
    * 设置选择图标按钮样式
    */
    var setEditIconCls = function (data) {
        var cls = '';
        if (data) {
            cls = data.iconCls;
        }
        if (!cls) {
            cls = 'icon-zoom';
        }
        formControl.find('#ModuleSelectIcon').linkbutton({ iconCls: cls });
    }

    return {
        init: function () {
            initGrid();
            initButtonEvent();
        }
    };
}();

$(function () {
    Menu.init();
});