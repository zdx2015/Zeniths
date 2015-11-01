/// <reference path="../../jquery/jquery.js" />
(function ($) {

    /*****************************私有函数*****************************/
    /**
     * 组件初始化
     * @param {DataGrid} instance 表格实例
     * @returns {} 
     */
    function _init(instance) {
        //绑定表单查询事件
        if (!instance.options.queryForm) {
            return;
        }
        var $queryForm = $(instance.options.queryForm);
        $queryForm.find(':submit:first').on('click', function () {
            var ps = zeniths.util.getFormData($queryForm);
            instance.search(ps);
            return false;
        });
    }

    /**
     * 加载数据
     * @param {DataGrid} instance 表格实例
     * @returns {} 
     */
    function _loadData(instance) {
        function _load() {

            var res = __onBeforeLoad(instance, instance.options)
            if (!res) return;//返回false 则不发起请求

            //instance.$element.mask('正在请求数据,请稍等...');
            zeniths.util.showLoading(instance.$element, '正在请求数据,请稍等...');
            var ajaxParam = $.extend({}, {
                page: instance.options.page,
                order: instance.options.order,
                dir: instance.options.dir
            }, instance.options.queryParams);

            $.ajax({
                url: instance.options.url,
                type: "post",
                data: ajaxParam,
                success: function (result) {
                    //instance.$element.unmask();
                    zeniths.util.hideLoading(instance.$element);
                    _callback(result);
                    __onLoadSuccess(result, instance.options);
                },
                error: function (result) {
                    //instance.$element.unmask();
                    zeniths.util.hideLoading(instance.$element);
                    __onLoadError(result, instance.options, instance);
                }
            });
        }

        function _callback(result) {
            instance.$element.empty();
            instance.$element.append(result);
            instance.$table = instance.$element.find('table');
            instance.$paginate = instance.$element.find('.paginateArea');
            instance.$checkboxs = instance.$table.find('tbody .checkbox-check')

            instance.$checkboxs.on('change', function () {
                var checked = $(this).prop('checked');
                if (checked) {
                    $(this).closest('tr').addClass('warning');
                    __onCheck($(this), instance.options);
                } else {
                    $(this).closest('tr').removeClass('warning');
                    __onUncheck($(this), instance.options);
                }
            });

            instance.$table.find('thead .checkbox-check').on('change', function () {
                var checked = $(this).prop('checked');
                instance.$checkboxs.each(function () {
                    if (checked) {
                        $(this).prop("checked", true);
                        $(this).closest('tr').addClass('warning');
                        __onCheck($(this), instance.options);
                    } else {
                        $(this).prop("checked", false);
                        $(this).closest('tr').removeClass('warning');
                        __onUncheck($(this), instance.options);
                    }
                });
            });

            //分页事件
            instance.$paginate.find('ul>li:not(.active,.disabled)>a').on('click', function () {
                instance.options.page = $(this).data('page');
                _load();
            }).css('cursor', 'pointer');

            //排序事件
            instance.$table.find('thead>tr>th[data-order]').each(function () {
                var order = $(this).data('order');
                if (instance.options.order === order) {
                    if (instance.options.dir === 'asc') {
                        $(this).append(' <i class="fa fa-long-arrow-down" title="升序排序"></i>');
                    }
                    else if (instance.options.dir === 'desc') {
                        $(this).append(' <i class="fa fa-long-arrow-up" title="倒序排序"></i>');
                    }
                } else {
                    $(this).append(' <i class="glyphicon glyphicon-sort" style="opacity: 0.2" title="点击排序"></i>');
                }
            }).on('click', function () {
                instance.options.order = $(this).data('order');
                if (instance.options.dir) {
                    if (instance.options.dir === 'asc') {
                        instance.options.dir = 'desc';
                    } else {
                        instance.options.dir = 'asc';
                    }
                } else {
                    instance.options.dir = 'asc';
                }
                __onSort(instance.options);
                _load();
            }).css('cursor', 'pointer');

            //添加没有数据提示
            if (instance.options.showAlert) {
                _noDataAlert(instance);
            }

            _setTextSplitWidth(instance.$table);
        }

        _load();
    }

    function _setTextSplitWidth($table) {
        function _core() {
            $table.find('tbody .text-split').each(function () {
                var w = $(this).parent().width();
                $(this).css('width', (w - 100) + 'px');
            });
        }
        window.onresize = function () {
            _core();
        }
        _core();
    }

    function _noDataAlert(instance) {
        var hasData = instance.$table.find('tbody tr').length > 0;
        if (!hasData) {
            zeniths.util.showAlertWarning(instance.$element, '<i class="fa-lg fa fa-warning"></i> 没有查询到数据!', '', false);
        }
    }

    /*****************************事件函数*****************************/

    function __onBeforeLoad(instance, options) {
        if (options.onBeforeLoad) {
            return options.onBeforeLoad(instance);
        }
        return true;
    }

    function __onLoadSuccess(result, options) {
        if (options.onLoadSuccess) {
            options.onLoadSuccess(result);
        }
    }

    function __onLoadError(result, options, instance) {
        if (options.onLoadError) {
            options.onLoadError(result);
        } else {
            zeniths.util.showAlertDanger(instance.$element, '<i class="fa-lg fa fa-warning"></i> 数据加载失败!', result.responseJSON.message, true);
            //alert(result.responseJSON.message);
        }
    }

    function __onSort(options) {
        if (options.onSort) {
            options.onSort(options.order, options.dir);
        }
    }

    function __onCheck($checkbox, options) {
        if (options.onCheck) {
            options.onCheck($checkbox.val());
        }
    }

    function __onUncheck($checkbox, options) {
        if (options.onUncheck) {
            options.onUncheck($checkbox.val());
        }
    }

    /*****************************构造函数*****************************/
    /**
     * 组件构造函数
     * @param {} $element 
     * @param {} options 
     * @returns {} 
     */
    var DataGrid = function ($element, options) {
        this.$element = $element;
        this.$table = null;
        this.$paginate = null;
        this.$checkboxs = null;
        this.options = options;
        _init(this);
        if (this.options.autoLoad === true) {
            _loadData(this);
        }
    };

    /*****************************公共函数*****************************/
    /**
    * 获取勾选的主键数组
    * @returns {} 
    */
    DataGrid.prototype.getSelectedIds = function () {
        var sz = [];
        this.$checkboxs.each(function () {
            if (this.checked) {
                sz.push(this.value);
            }
        });
        return sz;
    }

    /**
     * 数据排序
     * @param {String} name 排序字段名称
     * @param {String} dir 排序方式(asc,desc)
     * @returns {None} 
     */
    DataGrid.prototype.sort = function (name, dir) {
        if (!dir) {
            dir = 'asc';
        }
        this.options.order = name;
        this.options.dir = dir;
        __onSort(this.options);
        _loadData(this);
    }

    /**
     * 查询数据(更改页面为第一页)
     * @param {} extraQueryParams 传递的参数
     * @returns {} 
     */
    DataGrid.prototype.search = function (extraQueryParams) {
        if (!extraQueryParams) {
            extraQueryParams = {};
        }
        this.options.page = 1;
        $.extend(this.options.queryParams, extraQueryParams);
        _loadData(this);
    }

    /**
     * 重新加载数据
     * @returns {} 
     */
    DataGrid.prototype.reload = function (extraQueryParams) {
        if (!extraQueryParams) {
            extraQueryParams = {};
        }
        $.extend(this.options.queryParams, extraQueryParams);
        _loadData(this);
    };

    /**
     * 清除当前表格数据行
     * @returns {} 
     */
    DataGrid.prototype.clearRows = function () {
        this.$table.find('tbody').empty();
        this.$element.find('.paginateArea').empty();
    };

    /*****************************表格插件*****************************/
    /**
     * 表格插件
     * @param {Object} options 配置对象
     * @returns {DataGrid} 返回第一个表格对象实例
     */
    $.fn.datagrid = function (options) {
        var instances = [];
        options = options || {};

        this.each(function () {
            var $this = $(this);
            var instance = $this.data('datagrid');
            if (instance) {
                $.extend(instance.options, options);
            } else {
                var ops = $.extend({}, $.fn.datagrid.defaults, $.fn.datagrid.parseOptions($this), options);
                //$.each($.fn.datagrid.parseOptions($this), function (k, v) {
                //    console.log('k={0},v={1}'.format(k, v));
                //});
                instance = new DataGrid($this, ops);
                $this.data('datagrid', instance);
            }

            instances.push(instance);
        });
        return instances[0];
    };

    /**
     * 解析配置项
     * @param {JQuery} $element 目标对象
     * @returns {Options} 配置选项
     */
    $.fn.datagrid.parseOptions = function ($element) {
        var properties = ['url', 'order', 'dir', 'alert'];
        var options = {};
        $.each(properties, function (index, value) {
            var v = $element.data(value);
            if (v) {
                options[value] = v;
            }
        });
        var _value = $element.data('query-form');
        if (_value) {
            options['queryForm'] = _value;
        }
         
        _value = $element.data('query-params');
        if (_value) {
            options['queryParams'] = (new Function('return ' + _value))();
        }

        options['autoLoad'] = $element.data('auto-load');
        options['showAlert'] = $element.data('show-alert');
        return options;
    };

    /*****************************配置默认值*****************************/
    /**
     * 表格配置默认值
     */
    $.fn.datagrid.defaults = {
        /**
         * 自动加载数据
         */
        autoLoad: true,

        /**
         * 没有数据时自动添加提示
         */
        showAlert: true,

        /**
         * 表格地址
         */
        url: null,

        /**
         * 页码
         */
        page: 1,

        /**
         * 排序字段名称
         */
        order: null,

        /**
         * 排序方式:asc,desc
         */
        dir: null,

        /**
         * 表单对象
         */
        queryForm: null,

        /**
         * 查询参数
         */
        queryParams: {},

        /**
         * 数据加载前执行
         */
        onBeforeLoad: function (instance) { return true; },

        /**
         * 数据加载成功后执行
         */
        onLoadSuccess: null,

        /**
         * 数据加载失败后执行
         */
        onLoadError: null,

        /**
         * 复选框选中时执行
         * @param {String} id 
         * @returns {} 
         */
        onCheck: null,

        /**
         * 复选框未选中时执行
         * @param {String} id 
         * @returns {} 
         */
        onUncheck: null,

        /**
         * 数据排序前执行
         * @param {String} name 
         * @param {String} dir 
         * @returns {} 
         */
        onSort: null
    };

})(jQuery);

/**
 * 默认初始化表格对象
 */
$(function () {
    $('.datagrid').datagrid();
});
