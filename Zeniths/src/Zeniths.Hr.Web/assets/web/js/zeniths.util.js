var zeniths = zeniths || {};
zeniths.util = zeniths.util || {};

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
        html = '<div class="loading-message ' + (options.boxed ? 'loading-message-boxed' : '') + '"><img src="/assets/web/css/images/loading-spinner-blue.gif" align=""></div>';
    } else if (options.textOnly) {
        html = '<div class="loading-message ' + (options.boxed ? 'loading-message-boxed' : '') + '"><span>&nbsp;&nbsp;' + (options.message ? options.message : '正在加载...') + '</span></div>';
    } else {
        html = '<div class="loading-message ' + (options.boxed ? 'loading-message-boxed' : '') + '"><img src="/assets/web/css/images/loading-spinner-blue.gif" align=""><span>&nbsp;&nbsp;' + (options.message ? options.message : '正在加载...') + '</span></div>';
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
        //shift:-1,
        icon: 0
    }
    var ops = $.extend({}, defaults);
    if (typeof options === 'object') {
        $.extend(ops, options);
    }
    else if (typeof options === 'function') {
        callback = options;
        options = undefined;
    }
    if (ops.parent) {
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
        //shift:-1,
        icon: 3
    }
    var ops = $.extend({}, defaults);
    if (typeof options === 'object') {
        $.extend(ops, options);
    }
    else if (typeof options === 'function') {
        yesCallback = options;
        options = undefined;
    }
    if (ops.parent) {
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
    layer.msg(message, callback);
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
        //shift:-1,
        icon: 3
    }
    var ops = $.extend({}, defaults);
    if (typeof options === 'object') {
        $.extend(ops, options);
    }
    else if (typeof options === 'function') {
        callback = options;
        options = undefined;
    }
    if (ops.parent) {
        window.top.layer.prompt(ops, callback);
    } else {
        layer.prompt(ops, callback);
    }
};

/**
 * 对话框
 * 如果需要在顶层窗口显示,则需要在选项中增加{parent:true}.
 * success :对话框弹出后的成功回调
 * end :对话框销毁后触发的回调
 * fit:默认大小自适应,需要在子页面的body标签设置宽高,如果要手动指定大小,需要把fit设为false,同时使用area: ['500px', '300px']指定
 * @param {String} title 标题
 * @param {String} url 加载路径
 * @param {Object(可选)} options 选项配置
 * @returns {void} 
 */
zeniths.util.dialog = function (url, options) {
    var defaults = {
        type: 2, //0信息框 1页面层 2iframe层 3加载层 4tips层
        skin: 'layui-layer-rim', //加上边框
        scrollbar: false,
        moveOut: true,
        shift: 3,
        fix: true,
        closeBtn: 2,
        title: '',
        content: url
    };
    var ops = $.extend({}, defaults);
    if (typeof options === 'object') {
        $.extend(ops, options);
    }
    //if (ops.fit !== false) {
    //    ops.success = function (layero, index) {
    //        zeniths.util.dialogIframeCenter(layero, ops.parent);
    //        if (options && options.success) {
    //            options.success(layero, index);
    //        }
    //    };
    //}

    if (ops.parent) {
        window.top.layer.open(ops);
    } else {
        layer.open(ops);
    }
};

/**
 * 重新调整对话框的宽高位置,使之居中
 * @param {window} win window对象
 * @param {Number} index 对话框索引号
 * @returns {void} 
 */
zeniths.util.dialogIframeCenter = function (layero, isParent) {
    var body = $(layero.find('iframe').contents().find('body'));
    var wid = body.outerWidth();
    var heg = body.outerHeight();

    var titHeight = layero.find('.layui-layer-title').outerHeight() || 0;
    var btnHeight = layero.find('.layui-layer-btn').outerHeight() || 0;
    var borderWidth = 6;

    var scrollWid = zeniths.util.getScrollbarWidth();
    var $win = isParent ? $(window.top) : $(window);
    var top = ($win.height() - heg) / 2;
    var left = ($win.width() - wid) / 2;

    //console.log('scrollWid=' + scrollWid);

    //console.log('layero:top={0},left={1},width={2},height={3}'
    //    .format(top, left, (wid + scrollWid + (borderWidth * 2)), (heg + titHeight + btnHeight + scrollWid + (borderWidth * 2))));
    //console.log('iframe:width={0},height={1}'.format( wid + scrollWid,  heg + scrollWid));

    //$.each(layero.find('iframe').contents(),function(k,v) {
    //    console.log('key={0},value={1}'.format(k,v));
    //});
    //console.log($(layero.find('iframe').contents().find('body')).outerWidth());
    //console.log('window:width={0},height={1}'.format( , ));

    if (isParent) {
        layero.css({ top: top, left: left, width: wid + scrollWid, height: heg + titHeight + btnHeight + scrollWid });
    } else {
        layero.css({ top: top, left: left, width: wid + scrollWid + (borderWidth * 2), height: heg + titHeight + btnHeight + scrollWid + (borderWidth * 2) });
    }
    layero.find('iframe').css({ width: wid + scrollWid, height: heg + scrollWid });

};

