/**
 * xci命名空间
 */
var xci = xci || {};

/**
 * 基础类库
 */
xci.core = {
    user: {},
    /**
     * 表格分页列表
     */
    gridPageList: [1, 5, 10, 15, 20, 25, 50, 100, 200],

    /**
     * 默认分页大小
     */
    defaultPageSize: 10,

    alert: function (message, fn) {
        $.messager.alert('操作提示', message, 'info', fn);
    },

    confirm: function (message, fn) {
        $.messager.confirm("操作提示", message, fn);
    },

    showProgress: function (message) {
        $.messager.progress({ text: message });
    },

    closeProgress: function () {
        $.messager.progress('close');
    },

    /**
     * 获取当前操作系统信息
     */
    getOS: function () {
        var sUserAgent = navigator.userAgent;
        var isWin = (navigator.platform == "Win32") || (navigator.platform == "Windows");
        var isMac = (navigator.platform == "Mac68K") || (navigator.platform == "MacPPC")
            || (navigator.platform == "Macintosh") || (navigator.platform == "MacIntel");
        if (isMac) return "Mac";
        var isUnix = (navigator.platform == "X11") && !isWin;
        if (isUnix) return "Unix";
        var isLinux = (String(navigator.platform).indexOf("Linux") > -1);
        if (isLinux) return "Linux";
        if (isWin) {
            var isWin2K = sUserAgent.indexOf("Windows NT 5.0") > -1 || sUserAgent.indexOf("Windows 2000") > -1;
            if (isWin2K) return "Windows2000";
            var isWinXP = sUserAgent.indexOf("Windows NT 5.1") > -1 || sUserAgent.indexOf("Windows XP") > -1;
            if (isWinXP) return "WindowsXP";
            var isWin2003 = sUserAgent.indexOf("Windows NT 5.2") > -1 || sUserAgent.indexOf("Windows 2003") > -1;
            if (isWin2003) return "Windows2003";
            var isWinVista = sUserAgent.indexOf("Windows NT 6.0") > -1 || sUserAgent.indexOf("Windows Vista") > -1;
            if (isWinVista) return "WindowsVista";
            var isWin7 = sUserAgent.indexOf("Windows NT 6.1") > -1 || sUserAgent.indexOf("Windows 7") > -1;
            if (isWin7) return "Windows7";
            var isWin8 = sUserAgent.indexOf("Windows NT 6.2") > -1 || sUserAgent.indexOf("Windows 8") > -1;
            if (isWin8) return "Windows8";
        }
        return "未知操作系统";
    },

    /**
     * 获取当前浏览器信息
     */
    getBrowser: function () {
        var Sys = {};
        var ua = navigator.userAgent.toLowerCase();
        var s;
        (s = ua.match(/msie ([\d.]+)/)) ? Sys.ie = s[1] :
            (s = ua.match(/firefox\/([\d.]+)/)) ? Sys.firefox = s[1] :
                (s = ua.match(/chrome\/([\d.]+)/)) ? Sys.chrome = s[1] :
                    (s = ua.match(/opera.([\d.]+)/)) ? Sys.opera = s[1] :
                        (s = ua.match(/version\/([\d.]+).*safari/)) ? Sys.safari = s[1] : 0;

        //以下进行测试
        if (Sys.ie) return ('InternetExplorer' + Sys.ie);
        if (Sys.firefox) return ('Firefox' + Sys.firefox);
        if (Sys.chrome) return ('Chrome' + Sys.chrome);
        if (Sys.opera) return ('Opera' + Sys.opera);
        if (Sys.safari) return ('Safari' + Sys.safari);
        return '未知浏览器';
    },

    /**
     * 获取当前系统分辨率
     */
    getScreen: function () {
        return window.screen.width + '×' + window.screen.height;
    },

    /**
     * 获取数节点的排序路径
     * @param id 节点Id
     */
    GetNodeSortPath: function (treeControl, id, pkName) {
        if (!id) {
            return '';
        }
        var parentNode = treeControl.treegrid('getParent', id);
        if (parentNode != null) {
            var pstring = xci.core.GetNodeSortPath(treeControl, parentNode[pkName], pkName);
            return pstring + (xci.core.GetNodeIndex(id, parentNode.children, pkName).FixLengthString(4, '0'));
        } else {
            return (xci.core.GetNodeIndex(id, treeControl.treegrid('getRoots'), pkName).FixLengthString(4, '0'));
        }
    },

    GetNodeIndex: function (id, nodes, pkName) {
        var nodeIndex = -1;
        $.each(nodes, function (index, value) {
            if (value[pkName] == id) {
                nodeIndex = index;
                return false;
            }
        });
        return nodeIndex.toString();
    }
};


String.prototype.format = function (args) {
    var result = this;
    if (arguments.length > 0) {
        if (arguments.length == 1 && typeof (args) == "object") {
            for (var key in args) {
                if (args[key] != undefined) {
                    var reg = new RegExp("({" + key + "})", "g");
                    result = result.replace(reg, args[key]);
                }
            }
        }
        else {
            for (var i = 0; i < arguments.length; i++) {
                if (arguments[i] != undefined) {
                    var reg = new RegExp("({)" + i + "(})", "g");
                    result = result.replace(reg, arguments[i]);
                }
            }
        }
    }
    return result;
};

Date.prototype.format = function (format) {
    var o = {
        "M+": this.getMonth() + 1, //month
        "d+": this.getDate(),    //day
        "h+": this.getHours(),   //hour
        "m+": this.getMinutes(), //minute
        "s+": this.getSeconds(), //second
        "q+": Math.floor((this.getMonth() + 3) / 3),  //quarter
        "S": this.getMilliseconds() //millisecond
    }
    if (/(y+)/.test(format)) format = format.replace(RegExp.$1,
    (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o) if (new RegExp("(" + k + ")").test(format))
        format = format.replace(RegExp.$1,
        RegExp.$1.length == 1 ? o[k] :
        ("00" + o[k]).substr(("" + o[k]).length));
    return format;
}

String.prototype.FixLengthString = function (totalLength, defaultStr) {
    var result = this;
    var times = totalLength - (result.length);
    for (var i = 1; i <= times; i++) {
        result = defaultStr + result;
    }
    return result;
};

//检查密码和确认密码是否相同。
$.extend($.fn.validatebox.defaults.rules, {
    equals: {
        validator: function (value, param) {
            return value == $(param[0]).val();
        },
        message: '两次输入内容不一致'
    }
});

$.extend($.fn.textbox.defaults, {
    height:26
});
$.extend($.fn.datetimebox.defaults, {
    height: 26
});



/**
 * 应用程序
 */
xci.app = {

    /**
     * 数据表格初始化
     * @param gridControl 数据表格控件
     * @param idField 主键字段
     * @param contextMenuSelector 右键菜单选择器,
     * @param dblClickHandle 双击事件
     * @param options 其他选项,可以覆盖默认选项
     */
    initDataGrid: function (gridControl, idField, contextMenuSelector, dblClickHandle, options) {
        var defaults = {
            idField: idField,
            fitColumns: true,
            ctrlSelect: true,
            rownumbers: true,
            pagination: true,
            pageList: xci.core.gridPageList,
            rowStyler: function (index, row) {
                if (index % 2 == 0) {
                    return 'background-color:#FFFFFF;color:#000000';
                } else {
                    return 'background-color:#DDFFFF;color:#000000'
                }
            },
            pageSize: xci.core.defaultPageSize

        };
        var config = {};
        $.extend(config, defaults, options);
        if (dblClickHandle) {
            config.onDblClickRow = function () {
                var row = gridControl.datagrid("getSelected");
                dblClickHandle(row[idField], row);
            }
        }
        if (contextMenuSelector) {
            config.onRowContextMenu = function (e, rowIndex, rowData) {
                e.preventDefault();
                $(this).datagrid('clearSelections');
                $(this).datagrid('selectRow', rowIndex);
                $(contextMenuSelector).menu('show', {
                    left: e.pageX,
                    top: e.pageY
                });
            }
        }
        gridControl.datagrid(config);
    },

    /**
     * 树形表格初始化
     * @param treeControl 树形表格控件
     * @param idField 主键字段
     * @param dblClickHandle 双击事件
     * @param contextMenuSelector 右键菜单选择器
     * @param isDrag 是否允许拖拽
     * @param options 其他选项,可以覆盖默认选项
     */
    initTreeGrid: function (treeControl, idField, dblClickHandle, contextMenuSelector, isDrag, options) {
        var defaults = {
            idField: idField,
            treeField: 'Name',
            fitColumns: true,
            animate: true,
            rownumbers: true,
            pageList: xci.core.gridPageList,
            pageSize: xci.core.defaultPageSize
        };
        var config = {};
        $.extend(config, defaults, options);
        if (dblClickHandle) {
            config.onDblClickRow = function () {
                var row = treeControl.datagrid("getSelected");
                dblClickHandle(row);
            }
        }
        if (contextMenuSelector) {
            config.onContextMenu = function (e, row) {
                e.preventDefault();
                $(this).treegrid('select', row[idField]);
                $(contextMenuSelector).menu('show', {
                    left: e.pageX,
                    top: e.pageY
                });
            }
        }
        if (isDrag) {
            var _successBack;
            if (config.onLoadSuccess) {
                _successBack = config.onLoadSuccess;
            }
            config.onLoadSuccess = function (row,data) {
                if (_successBack)
                    _successBack(row,data);
                $(this).treegrid('enableDnd', row ? row[idField] : null);
            };
            config.onDragOver = function (targetRow, sourceRow) {
                if (sourceRow._parentId == targetRow[idField]) {
                    return false;
                }
                return true;
            };
        }
        treeControl.treegrid(config);
    },

    /**
     * 删除数据表格
     * @param gridControl 数据表格控件
     * @param deleteUrl 删除地址
     * @param sucessHandle 删除成功回调函数
     */
    deleteDataGridData: function (gridControl, deleteUrl, sucessHandle) {
        var options = gridControl.datagrid('options');
        var idField = options.idField;
        var rows = gridControl.datagrid("getChecked");
        if (rows.length == 0) {
            xci.core.alert('请先选择要操作的数据');
            return;
        }
        var ids = [];
        $.each(rows, function (i, v) {
            ids.push(v[idField]);
        });
        xci.core.confirm('确定要删除选中的条{0}数据吗?'.format(rows.length), function (r) {
            if (!r) return;
            $.post(deleteUrl, { ids: ids.join() }, function (data) {
                if (data.success && sucessHandle) {
                    sucessHandle(data);
                }
                else {
                    xci.core.alert(data.message);
                }
            });
        });
    },

    /**
     * 删除数据表格
     * @param gridControl 数据表格控件
     * @param deleteUrl 删除地址
     * @param sucessHandle 删除成功回调函数
     */
    deleteDataGridDataPostData: function (gridControl, deleteUrl, beforePostHandle, sucessHandle) {
        var options = gridControl.datagrid('options');
        var idField = options.idField;
        var rows = gridControl.datagrid("getChecked");
        if (rows.length == 0) {
            xci.core.alert('请先选择要操作的数据');
            return;
        }
        var ids = [];
        $.each(rows, function (i, v) {
            ids.push(v[idField]);
        });
        var postData = {};
        if (beforePostHandle) {
            beforePostHandle(postData, ids.join());
        }
        xci.core.confirm('确定要删除选中的条{0}数据吗?'.format(rows.length), function (r) {
            if (!r) return;
            $.post(deleteUrl, postData, function (data) {
                if (data.success && sucessHandle) {
                    sucessHandle(data);
                }
                else {
                    xci.core.alert(data.message);
                }
            });
        });
    },

    /**
     * 处理勾选的数据表格
     * @param gridControl 数据表格控件
     * @param sucessHandle 处理函数函数
     */
    processMultipleDataGridData: function (gridControl, processHandle) {
        var options = gridControl.datagrid('options');
        var idField = options.idField;
        var rows = gridControl.datagrid("getChecked");
        if (rows.length == 0) {
            xci.core.alert('请先选择要操作的数据');
            return;
        }
        var ids = [];
        $.each(rows, function (i, v) {
            ids.push(v[idField]);
        });
        if (processHandle) {
            processHandle(ids);
        }
    },

    /**
     * 删除树表格
     * @param treeControl 数据表格控件
     * @param deleteUrl 删除地址
     * @param sucessHandle 删除成功回调函数
     */
    deleteTreeData: function (treeControl, deleteUrl, sucessHandle) {
        var node = treeControl.tree("getSelected");
        if (!node) {
            xci.core.alert('请先选择要操作的数据');
            return;
        }
        var ids = [];
        ids.push(node.id);
        var msg = '确定要删除选中的数据吗?';
        if (node.children) {
            var childs = treeControl.tree("getChildren", node.target);
            msg = '选中的节点包含{0}个子节点,确定要删除当前节点及其全部子节点吗?'.format(childs.length);
            $.each(childs, function (i, v) {
                ids.push(v.id);
            });
        }
        xci.core.confirm(msg, function (r) {
            if (!r) return;
            $.post(deleteUrl, { ids: ids.join() }, function (data) {
                if (data.success && sucessHandle) {
                    sucessHandle(data);
                }
                else {
                    xci.core.alert(data.message);
                }
            });
        });
    },

    /**
     * 删除树形表格
     * @param treeControl 树形表格控件
     * @param deleteUrl 删除地址
     * @param sucessHandle 删除成功回调函数
     */
    deleteTreeGridData: function (treeControl, deleteUrl, sucessHandle) {
        var options = treeControl.treegrid('options');
        var idField = options.idField;
        var row = treeControl.treegrid("getSelected");
        if (!row) {
            xci.core.alert('请先选择要操作的数据');
            return;
        }
        var ids = [];
        ids.push(row[idField]);
        var msg = '确定要删除选中的数据吗?';
        if (row.children && row.children.length > 0) {
            var childs = treeControl.treegrid("getChildren", row[idField]);
            msg = '选中的节点包含{0}个子节点,确定要删除当前节点及其全部子节点吗?'.format(childs.length);
            $.each(childs, function (i, v) {
                ids.push(v[idField]);
            });
        }
        xci.core.confirm(msg, function (r) {
            if (!r) return;
            $.post(deleteUrl, { ids: ids.join() }, function (data) {
                if (data.success && sucessHandle) {
                    sucessHandle(data);
                }
                else {
                    xci.core.alert(data.message);
                }
            });
        });
    },

    searchDataGridData: function (gridControl, params, url) {
        if (url) {
            gridControl.datagrid('options').url = url;
        }
        gridControl.datagrid("load", params);
    },

    refreshDataGridData: function (gridControl, url) {
        if (url) {
            gridControl.datagrid('options').url = url;
        }
        gridControl.datagrid("reload");
    },

    refreshTreeGridData: function (treeControl, url) {
        if (url) {
            treeControl.datagrid('options').url = url;
        }
        treeControl.treegrid("reload");
    },

    /**
     *  导出数据
     */
    exportData: function (exportUrl, params) {
        var url = exportUrl;
        if (params) {
            url = url + '?' + $.param(params);
        }
        window.location.href = url;
    },

    /**
     *  表格布尔值格式化显示
     */
    boolColumnFormatter: function (value) {
        var className = value ? "checkok" : "checkno";
        return '<div class="' + className + '"><div>';
    },

    /**
     * 自动添加表单中复选框的值(由于传统表单,当复选框不勾选时,不提交此字段,所以在此补充提交)
     * @param formControl 表单控件
     * @param data 数据对象
     */
    addCheckBoxValue: function (formControl, data) {
        formControl.find('input[type=checkbox]').each(function () {
            var name = $(this).attr('name');
            data[name] = $(this).prop('checked');
        });
    },
    /**
     * 关闭编辑对话框
     */
    closeDialog: function (dialogControl, closeHandle) {
        dialogControl.dialog('close');
        if (closeHandle) {
            closeHandle();
        }
    },

    /**
     * 加载表单数据
     */
    loadFormData: function (formControl, data) {
        formControl.form('load', data);
    },
    /**
     * 重置表单数据
     */
    resetFormData: function (formControl) {
        formControl.form('clear');
    },

    /**
     * 新建数据
     */
    createData: function (dialogControl, title, showHandle) {
        dialogControl.dialog('open').dialog('setTitle', title);
        if (showHandle) {
            showHandle();
        }
    },
    /**
     * 编辑表格数据
     */
    editDataGridData: function (gridControl, dialogControl, title, showHandle) {
        var row = gridControl.datagrid("getSelected");
        if (row == null) {
            xci.core.alert('请先选择要操作的数据');
            return;
        }
        dialogControl.dialog('open').dialog('setTitle', title);
        if (showHandle) {
            showHandle(row);
        }
    },
    /**
     * 编辑树形表格数据
     */
    editTreeGridData: function (treeControl, dialogControl, title, showHandle) {
        var row = treeControl.treegrid("getSelected");
        if (row == null) {
            xci.core.alert('请先选择要操作的数据');
            return;
        }
        dialogControl.dialog('open').dialog('setTitle', title);
        if (showHandle) {
            showHandle(row);
        }
    },
    /**
     * 编辑树数据
     */
    editTreeData: function (treeControl, dialogControl, title, showHandle) {
        var row = treeControl.tree("getSelected");
        if (row == null) {
            xci.core.alert('请先选择要操作的数据');
            return;
        }
        dialogControl.dialog('open').dialog('setTitle', title);
        if (showHandle) {
            showHandle(row);
        }
    },
    /**
     * 保存表单数据
     */
    submitData: function (formControl, saveUrl, isValidate, submitHandle, successHandle) {
        formControl.form('submit', {
            url: saveUrl,
            onSubmit: function (param) {
                if (isValidate) {
                    if ($(this).form('validate')) {
                        xci.core.showProgress('正在提交...');
                        if (submitHandle) {
                            submitHandle(param);
                        }
                        return true;
                    }
                    else {
                        return false;
                    }
                } else {
                    xci.core.showProgress('正在提交...');
                    return true;
                }
            },
            success: function (data) {
                xci.core.closeProgress();
                var result = $.parseJSON(data);
                if (result.success && successHandle) {
                    successHandle(result);
                }
                else {
                    xci.core.alert(result.message);
                }
            }
        });
    },

    /**
     * Post提交数据
     * @param url 提交地址
     * @param data 提交数据
     * @param successHandle 成功回调
     */
    postData: function (url, data, successHandle) {
        $.post(url, data, function (result) {
            if (result.success && successHandle) {
                successHandle(result);
            }
            else {
                xci.core.alert(result.message);
            }
        });
    },


    /**
     * 设置编辑窗口父节点
     * @param parentRow 父节点 行对象
     */
    setParentName: function (parentRow, treeControl, parentNameSelector, parentIdSelector) {
        var options = treeControl.treegrid('options');
        var idField = options.idField;
        var treeField = options.treeField;

        if (parentRow) {
            $(parentNameSelector).html(parentRow[treeField]);
            $(parentIdSelector).val(parentRow[idField]);
        }
        else {
            $(parentNameSelector).html('根节点');
            $(parentIdSelector).val('');
        }
    },

    /**
     * 设置新增项的排序路径
     * @param parentId
     */
    setSortPath: function (parentId, treeControl, SortPathSelector) {
        var options = treeControl.treegrid('options');
        var idField = options.idField;
        var parentPath, currentPath;
        if (parentId) {
            parentPath = xci.core.GetNodeSortPath(treeControl, parentId, idField);
            currentPath = "0000";
            var children = treeControl.treegrid('find', parentId).children;
            if (children) {
                currentPath = children.length.toString().FixLengthString(4, '0');
            }
        }
        else {
            parentPath = "";
            currentPath = treeControl.treegrid('getRoots').length.toString().FixLengthString(4, '0');
        }
        $(SortPathSelector).val(parentPath + currentPath);
    },

    /**
     * 更新指定节点的子节点排序路径,可以传入多个节点Id
     */
    updateSortPath: function (treeControl, updateSortPathUrl, ids) {
        var options = treeControl.treegrid('options');
        var idField = options.idField;
        var data = {};
        $.each(ids, function (i, v) {
            var row = treeControl.treegrid('find', v);
            if (row == null) {
                var sz = treeControl.treegrid('getRoots');
                $.each(sz, function (index, value) {
                    data[value[idField]] = (index.toString()).FixLengthString(4, '0');
                });
            } else if (row.children) {
                $.each(row.children, function (index, value) {
                    data[value[idField]] = xci.core.GetNodeSortPath(treeControl, value[idField], idField);
                });
            }
        });
        $.post(updateSortPathUrl, data, function (result) {
            if (!result.success) {
                xci.core.alert(result.message);
            }
        });
    },
    /**
     * 树形表格控件,拖拽事件
     * @param treeControl 树形控件
     * @param targetRow 目标行
     * @param sourceRow 源行
     * @param point 拖拽点
     * @param saveParentUrl 父节点保存地址
     * @param updateSortPathUrl 排序路径更新地址
     * @param dropParentId 新的父节点
     */
    onTreeGridDrop: function (treeControl, targetRow, sourceRow, point, saveParentUrl, updateSortPathUrl, dropParentId) {
        var id = sourceRow.Id;
        var newParentId;
        if (point == 'append') {
            newParentId = targetRow.Id;
        }
        else {
            newParentId = targetRow._parentId;
        }
        if (!newParentId || newParentId == '') {
            newParentId = '0';
        }
        $.post(saveParentUrl, { id: id, oldParentId: dropParentId, newParentId: newParentId }, function (data) {
            if (!data.success) {
                xci.core.alert(data.message);
            }
        });
        var ids = [];
        ids.push(newParentId);
        xci.app.updateSortPath(treeControl, updateSortPathUrl, ids);
    },
    /**
     * 设置上一条/下一条按钮状态
     */
    setMoveButtonStatus: function (gridControl, prevBtnSelector, nextBtnSelector) {
        var row = gridControl.datagrid("getSelected");
        var rows = gridControl.datagrid('getRows');
        var index = gridControl.datagrid('getRowIndex', row);
        if (index == 0) {
            $(prevBtnSelector).linkbutton('disable');
            $(nextBtnSelector).linkbutton('enable');
        }
        else if (index == rows.length - 1) {
            $(prevBtnSelector).linkbutton('enable');
            $(nextBtnSelector).linkbutton('disable');
        }
        else {
            $(prevBtnSelector).linkbutton('enable');
            $(nextBtnSelector).linkbutton('enable');
        }
    },
    /**
     * 绑定上一条/下一条数据
     */
    moveSelectData: function (gridControl, isNext, prevBtnSelector, nextBtnSelector, loadHandle) {
        if (isNext && $(nextBtnSelector).linkbutton('options').disabled) {
            return;
        }
        if (!isNext && $(prevBtnSelector).linkbutton('options').disabled) {
            return;
        }
        var row = gridControl.datagrid("getSelected");
        var index = gridControl.datagrid('getRowIndex', row);
        var selectIndex;
        if (isNext) {
            selectIndex = index + 1;
        }
        else {
            selectIndex = index - 1;
        }
        gridControl.datagrid('clearSelections');
        gridControl.datagrid('selectRow', selectIndex);

        row = gridControl.datagrid("getSelected");
        if (row && loadHandle) {
            loadHandle(row);
        }
        xci.app.setMoveButtonStatus(gridControl, prevBtnSelector, nextBtnSelector);
    },
    /**
     * 显示图标对话框
     */
    showIconDialog: function (selectHandle) {
        $("<div/>").dialog({
            iconCls: 'icon-application_view_icons',
            href: '/Auth/System/WebIcons',
            title: '选取图标',
            width: 800,
            height: 600,
            loadingMessage: '图标正在加载中......',
            onLoad: function () {
                var dia = $(this);
                $(this).find('#iconlist li')
                    .attr('style', 'float:left;border:1px solid #fff;margin:2px;width:16px;cursor:pointer')
                    .click(function () {
                        if (selectHandle) {
                            var cls = 'icon-' + $(this).attr('title');
                            selectHandle(cls);
                            dia.dialog('destroy');
                        }
                    }).hover(function () {
                        $(this).css({ 'border': '1px solid red' });
                    }, function () {
                        $(this).css({ 'border': '1px solid #fff' });
                    });
            },
            onClose: function () {
                $(this).dialog('destroy');
            }
        });
    },
    /**
     * 获取选中树节点以及所有子节点Id,返回主键数组
     */
    getTreeSelectAndChildrenId: function (treeControl) {
        var node = treeControl.tree("getSelected");
        if (!node) {
            return null;
        }
        var ids = [];
        ids.push(node.id);
        if (node.children) {
            var childs = treeControl.tree("getChildren", node.target);
            $.each(childs, function (i, v) {
                ids.push(v.id);
            });
        }
        return ids;
    },
    /**
     * 获取字典数据
     */
    getDicItems: function (url) {
        var result = {};
        $.ajax({
            type: 'POST',
            url: url,
            async: false,
            success: function (data) {
                $.each(data, function (i, v) {
                    result[v.ItemValue] = v.ItemName;
                });
            }
        });
        return result;
    },
    /**
     * 获取模型数据
     */
    getModelItems: function (url) {
        var result = {};
        $.ajax({
            type: 'POST',
            url: url,
            async: false,
            success: function (data) {
                $.each(data.data, function (i, v) {
                    result[v.Id] = v.Name;
                });
            }
        });
        return result;
    },

    /**
     * Post同步数据
     * @param url 提交地址
     * @param data 提交数据
     * @param successHandle 成功回调
     */
    postDataSync: function (url, data, successHandle) {
        $.ajax({
            url: url,
            type: "post",
            async: false,
            data: data,
            success: function (result) {
                if (successHandle) {
                    successHandle(result);
                }
                else {
                    xci.core.alert(result.message);
                }
            }
        });
    },
    /**
    *用于验证开始时间小于结束时间
    */
    timeCheck: function (str) {
        $.extend($.fn.validatebox.defaults.rules, {
            TimeCheck: {
                validator: function (value, param) {
                    var s = $("input[name=" + param[0] + "]").val();
                    //因为日期是统一格式的所以可以直接比较字符串 否则需要Date.parse(_date)转换
                    return value >= s;
                },
                message: str
            }
        });
    }

}