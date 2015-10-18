var zeniths = zeniths || {};
zeniths.util = zeniths.util || {};
zeniths.tree = zeniths.tree || {};

/**
 * 获取当前操作系统信息
 * @return {}
 */
zeniths.util.getOS = function () {
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
        var isWin10 = sUserAgent.indexOf("Windows NT 6.4") > -1 || sUserAgent.indexOf("Windows 10") > -1;
        if (isWin10) return "Windows10";
    }
    return "未知";
};

/**
 * 获取当前浏览器信息
 * @return {}
 */
zeniths.util.getBrowser = function () {
    var sys = {};
    var ua = navigator.userAgent.toLowerCase();
    var s;
    (s = ua.match(/msie ([\d.]+)/)) ? sys.ie = s[1] :
    (s = ua.match(/firefox\/([\d.]+)/)) ? sys.firefox = s[1] :
    (s = ua.match(/chrome\/([\d.]+)/)) ? sys.chrome = s[1] :
    (s = ua.match(/opera.([\d.]+)/)) ? sys.opera = s[1] :
    (s = ua.match(/version\/([\d.]+).*safari/)) ? sys.safari = s[1] : 0;

    if (sys.ie) return ('IExplorer' + sys.ie);
    if (sys.firefox) return ('Firefox' + sys.firefox);
    if (sys.chrome) return ('Chrome' + sys.chrome);
    if (sys.opera) return ('Opera' + sys.opera);
    if (sys.safari) return ('Safari' + sys.safari);
    return '未知';
};

/**
 * 是否IE浏览器
 * @returns {Boolean} 
 */
zeniths.util.isIE = navigator.userAgent.toLowerCase().match(/msie ([\d.]+)/);

/**
 * 是否Chrome浏览器
 * @returns {Boolean} 
 */
zeniths.util.isChrome = navigator.userAgent.toLowerCase().match(/chrome\/([\d.]+)/);

/**
 * 是否火狐浏览器
 * @returns {Boolean} 
 */
zeniths.util.isFirefox = navigator.userAgent.toLowerCase().match(/firefox\/([\d.]+)/);

/**
 * 获取当前系统分辨率
 * @return {}
 */
zeniths.util.getScreen = function () {
    return window.screen.width + '×' + window.screen.height;
};

/**
 * 获取浏览器滚动条宽度
 * @returns {} 
 */
zeniths.util.getScrollbarWidth = function () {
    var $node = $('<p style="width:100px;height:100px;overflow-y:scroll"></p>');
    var node = $node[0];
    $(document.body).append(node);
    var scrollbarWidth = node.offsetWidth - node.clientWidth;
    $node.remove();
    return scrollbarWidth;
}

/**
 * 显示成功信息
 * @param {JQuery} $target 显示位置
 * @param {String} title 标题
 * @param {String} msg 消息内容
 * @param {Boolean} allowClose 是否允许关闭
 * @returns {} 
 */
zeniths.util.showAlertSuccess = function ($target, title, msg, allowClose) {
    zeniths.util.showAlert($target, title, msg, allowClose, 'alert-success');
}

/**
 * 显示提示信息
 * @param {JQuery} $target 显示位置
 * @param {String} title 标题
 * @param {String} msg 消息内容
 * @param {Boolean} allowClose 是否允许关闭
 * @returns {} 
 */
zeniths.util.showAlertInfo = function ($target, title, msg, allowClose) {
    zeniths.util.showAlert($target, title, msg, allowClose, 'alert-info');
}

/**
 * 显示警告信息
 * @param {JQuery} $target 显示位置
 * @param {String} title 标题
 * @param {String} msg 消息内容
 * @param {Boolean} allowClose 是否允许关闭
 * @returns {} 
 */
zeniths.util.showAlertWarning = function ($target, title, msg, allowClose) {
    zeniths.util.showAlert($target, title, msg, allowClose, 'alert-warning');
}

/**
 * 显示错误信息
 * @param {JQuery} $target 显示位置
 * @param {String} title 标题
 * @param {String} msg 消息内容
 * @param {Boolean} allowClose 是否允许关闭
 * @returns {} 
 */
zeniths.util.showAlertDanger = function ($target, title, msg, allowClose) {
    zeniths.util.showAlert($target, title, msg, allowClose, 'alert-danger');
}

/**
 * 显示数据加载失败错误信息
 * @param {JQuery} $target 显示位置
 * @param {String} msg 消息内容
 * @returns {} 
 */
zeniths.util.showAlertDangerLoadFailure = function ($target, msg) {
    zeniths.util.showAlertDanger($target, '<i class="fa-lg fa fa-warning"></i> 数据加载失败!', msg, true);
}

/**
 * 显示信息
 * @param {JQuery} $target 显示位置
 * @param {String} title 标题
 * @param {String} msg 消息内容
 * @param {Boolean} allowClose 是否允许关闭
 * @param {String} className 类名
 * @returns {} 
 */
zeniths.util.showAlert = function ($target, title, msg, allowClose, className) {

    var html = '<div class="alert {0} alert-dismissible" role="alert">'.format(className);
    if (allowClose) {
        html += '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>';
    }
    html += '<strong>{0}</strong> {1}'.format(title, msg);
    html += '</div>';
    $target.append(html);
}

/**
 * 显示加载层
 * @param {String} msg 提示消息
 * @returns {} 
 */
zeniths.util.mask = function (msg) {
    if (!msg) {
        msg = '正在提交数据,请稍等...';
    }
    $(document.body).mask(msg);
};

/**
 * 隐藏加载层
 * @returns {} 
 */
zeniths.util.unmask = function () {
    $(document.body).unmask();
};

/**
 * 显示加载层
 * @param {$Object} target 目标
 * @param {String} msg 消息内容
 * @returns {} 
 */
zeniths.util.showLoading = function (target, msg) {

    var options = $.extend(true, {}, {
        target: target,
        message: msg,
        boxed: true
    });
    var html = '';
    if (options.animate) {
        html = '<div class="loading-message ' + (options.boxed ? 'loading-message-boxed' : '') + '">' + '<div class="block-spinner-bar"><div class="bounce1"></div><div class="bounce2"></div><div class="bounce3"></div></div>' + '</div>';
    } else if (options.iconOnly) {
        html = '<div class="loading-message ' + (options.boxed ? 'loading-message-boxed' : '') + '"><img src="/plugin/zeniths.web/css/images/loading-spinner-blue.gif" align=""></div>';
    } else if (options.textOnly) {
        html = '<div class="loading-message ' + (options.boxed ? 'loading-message-boxed' : '') + '"><span>&nbsp;&nbsp;' + (options.message ? options.message : '正在加载...') + '</span></div>';
    } else {
        html = '<div class="loading-message ' + (options.boxed ? 'loading-message-boxed' : '') + '"><img src="/plugin/zeniths.web/css/images/loading-spinner-blue.gif" align=""><span>&nbsp;&nbsp;' + (options.message ? options.message : '正在加载...') + '</span></div>';
    }

    if (options.target) { // element blocking
        var el = $(options.target);
        if (el.height() <= ($(window).height())) {
            options.cenrerY = true;
        }
        el.block({
            message: html,
            baseZ: options.zIndex ? options.zIndex : 1000,
            centerY: options.cenrerY !== undefined ? options.cenrerY : false,
            css: {
                top: '10%',
                border: '0',
                padding: '0',
                backgroundColor: 'none'
            },
            overlayCSS: {
                backgroundColor: options.overlayColor ? options.overlayColor : '#555',
                opacity: options.boxed ? 0.05 : 0.1,
                cursor: 'wait'
            }
        });
    } else { // page blocking
        $.blockUI({
            message: html,
            baseZ: options.zIndex ? options.zIndex : 1000,
            css: {
                border: '0',
                padding: '0',
                backgroundColor: 'none'
            },
            overlayCSS: {
                backgroundColor: options.overlayColor ? options.overlayColor : '#555',
                opacity: options.boxed ? 0.05 : 0.1,
                cursor: 'wait'
            }
        });
    }
};

/**
 * 关闭加载层
 * @param {} target 目标
 * @returns {} 
 */
zeniths.util.hideLoading = function (target) {
    if (target) {
        $(target).unblock({
            onUnblock: function () {
                $(target).css('position', '');
                $(target).css('zoom', '');
            }
        });
    } else {
        $.unblockUI();
    }
};

/**
 * 信息警告框.如果需要在顶层窗口显示,则需要在选项中增加{parent:true}.
 * @param {String} message 消息内容
 * @param {Object(可选)} options 选项配置
 * @param {Function(可选)} callback 回调函数
 * @returns {void} 
 */
zeniths.util.alert = function (message, options, callback) {
    var defaults = {
        title: '系统提示',
        shift: zeniths.util.isChrome ? -1 : 0,
        icon: 0
    }
    if (typeof options === 'function') {
        callback = options;
        options = {};
    }
    var ops = $.extend({}, defaults, options);

    if (window.top) {
        window.top.layer.alert(message, ops, callback);
    } else {
        layer.alert(message, ops, callback);
    }
};

/**
 * 询问框.如果需要在顶层窗口显示,则需要在选项中增加{parent:true}.
 * @param {String} message 消息内容
 * @param {Object(可选)} options 选项配置
 * @param {Function(可选)} yesCallback 确定回调函数
 * @param {Function(可选)} cancelCallback 取消回调函数
 * @returns {void} 
 */
zeniths.util.confirm = function (message, options, yesCallback, cancelCallback) {
    var defaults = {
        title: '系统提示',
        shift: zeniths.util.isChrome ? -1 : 0,
        icon: 3
    }
    if (typeof options === 'function') {
        cancelCallback = yesCallback;
        yesCallback = options;
        options = {};
    }
    var ops = $.extend({}, defaults, options);

    if (window.top) {
        window.top.layer.confirm(message, ops, yesCallback, cancelCallback);
    } else {
        layer.confirm(message, ops, yesCallback, cancelCallback);
    }
};

/**
 * 提示框.如果需要在顶层窗口显示,则需要在选项中增加{parent:true}.
 * @param {String} message 消息内容
 * @param {Function(可选)} callback 回调函数
 * @returns {void} 
 */
zeniths.util.msg = function (message, callback) {
    if (window.top) {
        window.top.layer.msg(message, callback);
    } else {
        layer.msg(message, callback);
    }
};

/**
 * 输入框 prompt参数: formType: 0文本,1密码,2多行文本 value: '' 初始时的值 icon:-1 图标序号
 * 如果需要在顶层窗口显示,则需要在选项中增加{parent:true}.
 * @param {String} message 输入标题
 * @param {Object(可选)} options 选项配置
 * @param {Function(可选)} callback 回调函数
 * @returns {void} 
 */
zeniths.util.prompt = function (message, options, callback) {
    var defaults = {
        title: message,
        shift: zeniths.util.isChrome ? -1 : 0,
        icon: 3
    }
    if (typeof options === 'function') {
        callback = options;
        options = {};
    }
    var ops = $.extend({}, defaults, options);
    if (window.top) {
        window.top.layer.prompt(ops, callback);
    } else {
        layer.prompt(ops, callback);
    }
};

/**
 * 对话框
 * @param {String} url 加载路径
 * @param {Int} width 宽度
 * @param {Int} height 高度
 * @param {Object(可选)} options 选项配置
 * @returns {void} 
 */
zeniths.util.dialog = function (url, width, height, options) {
    if (typeof height === 'number' && zeniths.util.isFirefox) {
        height = height + 40;
    }

    //alert(window.top.frames.length);
    if (typeof height === 'number' && window.top.frames.length === 0) {
        height = height + 10;
    }

    var _w = width;
    var _h = height;
    if (typeof width === 'number') {
        _w = width + 'px';
    }
    if (typeof height === 'number') {
        _h = height + 'px';
    }
    var defaults = {
        type: 2, //0信息框 1页面层 2iframe层 3加载层 4tips层
        skin: 'layui-layer-zdx', //加上边框
        scrollbar: false,
        moveOut: true,
        shift: zeniths.util.isChrome ? -1 : 3,
        fix: true,
        closeBtn: 2,
        title: ' ',
        area: [_w, _h],
        content: url
    };
    var ops = $.extend({}, defaults, options);
    if (window.top) {
        var index = window.top.layer.open(ops);
        //console.log('options123=' + options);
        if (options && options.callback) {
            top.window['dialog' + index] = options.callback;
            //console.log('dialog index=' + index + ',callback=' + options.callback);
        }
    } else {
        var index = layer.open(ops);
        if (options && options.callback) {
            top.window['dialog' + index] = options.callback;
        }
    }
};

/**
 * 执行对话框回调函数
 * @param {Window} win Frame窗口对象
 * @returns {} 
 */
zeniths.util.callDialogCallback = function (win) {
    var index = top.layer.getFrameIndex(win.name);
    var callback = top.window['dialog' + index];
    //console.log('callDialogCallback index=' + index + ',callback=' + callback);
    if (callback) {
        callback();
        top.window['dialog' + index] = undefined;
    }
};

/**
 * 关闭窗口
 * @param {Int} index 
 * @returns {} 
 */
zeniths.util.layerClose = function (index) {
    if (window.top) {
        window.top.layer.close(index);
    } else {
        layer.close(index);
    }
}

/**
 * 关闭窗口
 * @param {Window} win Frame窗口对象
 * @returns {} 
 */
zeniths.util.closeFrameDialog = function (win) {
    var index = window.top.layer.getFrameIndex(win.name);
    window.top.layer.close(index);
}

///**
// * 重新调整对话框的宽高位置,使之居中
// * @param {window} win window对象
// * @param {Number} index 对话框索引号
// * @returns {void} 
// */
//zeniths.util.dialogIframeCenter = function (layero, isParent) {
//    var body = $(layero.find('iframe').contents().find('body'));
//    var wid = body.outerWidth();
//    var heg = body.outerHeight();

//    var titHeight = layero.find('.layui-layer-title').outerHeight() || 0;
//    var btnHeight = layero.find('.layui-layer-btn').outerHeight() || 0;
//    var borderWidth = 6;

//    var scrollWid = zeniths.util.getScrollbarWidth();
//    var $win = isParent ? $(window.top) : $(window);
//    var top = ($win.height() - heg) / 2;
//    var left = ($win.width() - wid) / 2;

//    //console.log('scrollWid=' + scrollWid);

//    //console.log('layero:top={0},left={1},width={2},height={3}'
//    //    .format(top, left, (wid + scrollWid + (borderWidth * 2)), (heg + titHeight + btnHeight + scrollWid + (borderWidth * 2))));
//    //console.log('iframe:width={0},height={1}'.format( wid + scrollWid,  heg + scrollWid));

//    //$.each(layero.find('iframe').contents(),function(k,v) {
//    //    console.log('key={0},value={1}'.format(k,v));
//    //});
//    //console.log($(layero.find('iframe').contents().find('body')).outerWidth());
//    //console.log('window:width={0},height={1}'.format( , ));

//    if (isParent) {
//        layero.css({ top: top, left: left, width: wid + scrollWid, height: heg + titHeight + btnHeight + scrollWid });
//    } else {
//        layero.css({ top: top, left: left, width: wid + scrollWid + (borderWidth * 2), height: heg + titHeight + btnHeight + scrollWid + (borderWidth * 2) });
//    }
//    layero.find('iframe').css({ width: wid + scrollWid, height: heg + scrollWid });
//};


/**
 * Post异步提交数据
 * @param {String} url 提交地址
 * @param {Object} data 提交数据
 * @param {Function} callback 成功回调
 * @param {Function} errorCallback 错误回调
 * @returns {} 
 */
zeniths.util.post = function (url, data, callback, errorCallback) {
    if (typeof (data) === 'function') {
        callback = data;
        data = undefined;
    }

    $.ajax({
        url: url,
        type: "post",
        data: data,
        success: function (result) {
            if (callback) {
                callback(result);
            }
        },
        error: function (result) {
            if (errorCallback) {
                errorCallback(result);
            } else {
                var msg = result.responseJSON.message;
                zeniths.util.alert(msg);
            }
        }
    });
};

/**
 * 删除数据记录
 * @param {String} url 删除地址
 * @param {Object} data 数据
 * @param {Function} callback 成功回调函数
 * @returns {} 
 */
zeniths.util.delete = function (url, data, callback) {
    if (typeof (data) === 'function') {
        callback = data;
        data = undefined;
    }
    zeniths.util.confirm('确定要删除当前记录吗?', function (index) {
        zeniths.util.post(url, data, function (result) {
            zeniths.util.layerClose(index);
            if (result.success) {
                if (callback) {
                    callback();
                }
            } else {
                var msg = result.message;
                zeniths.util.alert(msg);
            }
        });
    });
};

/**
 * 批量删除数据
 * @param {String} url 删除地址
 * @param {Array} ids 主键数组
 * @param {Function} callback 成功回调函数
 * @returns {} 
 */
zeniths.util.deleteBatch = function (url, ids, callback) {
    if (ids.length == 0) {
        zeniths.util.msg('请选择需要删除的记录');
        return;
    }
    zeniths.util.confirm('确定要删除选中的 ' + ids.length + ' 条记录吗?', function (index) {
        zeniths.util.post(url, { id: ids.join() }, function (result) {
            zeniths.util.layerClose(index);
            if (result.success) {
                if (callback) {
                    callback();
                }
            } else {
                var msg = result.message;
                zeniths.util.alert(msg);
            }
        });
    });
};

/**
 * 返回至少指定长度的字符串
 * @param {String} str 字符串
 * @param {Int} totalLength 字符串总长度
 * @param {String} defaultStr 字符不够时的默认字符
 * @returns {} 
 */
zeniths.util.FixLengthString = function (str, totalLength, defaultStr) {
    var result = str;
    var times = totalLength - (result.length);
    for (var i = 1; i <= times; i++) {
        result = defaultStr + result;
    }
    return result;
};

/**
 * 获取表单数据对象
 * @param {JQuery} $form JQuery表单对象
 * @returns {Object}  
 */
zeniths.util.getFormData = function ($form) {
    var ps = {};
    var formParams = $form.serializeArray();
    $.each(formParams, function (i, v) {
        ps[v.name] = v.value;
    });
    return ps;
}

/**
 * 获取Ajax服务器返回的错误信息
 * @param {XMLHttpRequest} result XMLHttpRequest 对象
 * @returns {String}  
 */
zeniths.util.getAjaxErrorMessage = function (result) {
    return result.responseJSON.message;
}

/**
 * 表单Ajax提交成功回调默认函数
 * @param {Object} result 
 * @returns {} 
 */
zeniths.util.formAjaxSuccess = function (result) {
    zeniths.util.unmask();
    if (!result.success) {
        zeniths.util.alert(result.message);
    } else {
        zeniths.util.callDialogCallback(window);
        zeniths.util.closeFrameDialog(window);
    }
}

/**
 * 标准表格绑定
 * @param {Object} options 配置参数
 * @returns {} 
 */
zeniths.util.standardGridBind = function (options) {
    var gridSelector = '.datagrid';
    var editWidth = options.editWidth;
    var editHeight = options.editHeight;
    var viewWidth = options.viewWidth;
    var viewHeight = options.viewHeight;
    if (options.gridSelector) {
        gridSelector = options.gridSelector;
    }

    //初始化表格
    function initGrid() {
        $(gridSelector).datagrid({
            onLoadSuccess: function () {

                //编辑记录事件
                $(gridSelector + ' .btnRecordEdit').on('click', function () {
                    editData($(this).data('url'));
                });

                //删除记录事件
                $(gridSelector + ' .btnRecordDelete').on('click', function () {
                    zeniths.util.delete($(this).data('url'), function () {
                        reloadData();
                    });
                });

                //查看记录事件
                $(gridSelector + ' .btnRecordView').on('click', function () {
                    var url = $(this).data('url');
                    viewData(url);
                });

                //行双击事件
                $(gridSelector + ' table>tbody>tr').on('dblclick', function () {
                    var url = $(this).data('url');
                    viewData(url);
                });
            }
        });
    }

    //刷新数据
    function reloadData() {
        $(gridSelector).datagrid().reload();
    }

    //数据编辑
    function editData(url) {
        zeniths.util.dialog(url, editWidth, editHeight, {
            callback: function () {
                reloadData();
            }
        });
    }

    //数据查看
    function viewData(url) {
        zeniths.util.dialog(url, viewWidth, viewHeight);
    }

    initGrid();

    $('.search-form').dataform().initSelect2();

    //绑定刷新事件
    $('#btnRefresh').on('click', function () {
        reloadData();
    });

    //绑定新建事件
    $('#btnCreate').on('click', function () {
        editData($(this).data('url'));
    });

    //绑定批量删除事件
    $('#btnDelete').on('click', function () {
        zeniths.util.deleteBatch($(this).data('url'), $(gridSelector).datagrid().getSelectedIds(), function () {
            reloadData();
        });
    });
}

/************************************Tree*************************************************************/

/**
 * 刷新树控件
 * @returns {} 
 */
zeniths.tree.reloadTree = function ($tree) {
    $tree.tree('reload');
};


/**
 * 删除树节点
 * @param {tree} $tree 树对象
 * @param {String} url 删除地址
 * @param {Object} data 提交的数据对象
 * @param {String} name 数据名称
 * @param {Function} callback 回调函数
 * @returns {} 
 */
zeniths.tree.deleteTreeNode = function ($tree, url, data, msg, callback) {

    var node = zeniths.tree.getTreeNodeSelected($tree);
    var ids = [];
    ids.push(node.id);

    if (node.children && node.children.length > 0) {
        var childs = $tree.tree('getChildren', node.target);
        $.each(childs, function (i, v) {
            ids.push(v.id);
        });
    }
    if (!msg) {
        msg = '确定要删除选中的 {0} 个节点吗(包括当前节点及其子节点)?';
    }
    msg = msg.format(ids.length);
    $.extend(data, { id: ids.join() });
    zeniths.util.confirm(msg, function (index) {
        zeniths.util.post(url, data, function (result) {
            zeniths.util.layerClose(index);
            if (result.success) {
                $tree.tree('remove', node.target);
                if (callback) {
                    callback();
                }
                //self.refreshTree();
            } else {
                var msg = result.message;
                zeniths.util.alert(msg);
            }
        });
    });
};


/**
 * 获取选中的树节点对象
 * @param {tree} $tree 树对象
 * @returns {Node} 
 */
zeniths.tree.getTreeNodeSelected = function ($tree) {
    return $tree.tree('getSelected');
};

/**
 * 获取数节点的排序路径
 * @param {EasyUITree} $tree 树控件
 * @param {String} id 节点主键
 * @returns {} 
 */
zeniths.tree.getTreeNodeSortPath = function ($tree, id) {
    if (!id) {
        return '';
    }
    var options = $tree.tree('options');
    var pkName = options.idField;
    var parentNode = $tree.tree('getParent', id);
    if (parentNode != null) {
        var pstring = zeniths.tree.getTreeNodeSortPath($tree, parentNode[pkName]);
        return pstring + zeniths.util.FixLengthString(zeniths.tree.getTreeNodeIndex(id, parentNode.children, pkName), 4, '0');
    } else {
        return zeniths.util.FixLengthString(zeniths.tree.getTreeNodeIndex(id, $tree.tree('getRoots'), pkName), 4, '0');
    }
};

/**
 * 获取节点序号
 * @param {String} id 节点主键
 * @param {Array} nodes 待查找的节点集合
 * @param {String} pkName 主键字段名
 * @returns {} 
 */
zeniths.tree.getTreeNodeIndex = function (id, nodes, pkName) {
    var nodeIndex = -1;
    $.each(nodes, function (index, value) {
        if (value[pkName] == id) {
            nodeIndex = index;
            return false;
        }
    });
    return nodeIndex.toString();
}