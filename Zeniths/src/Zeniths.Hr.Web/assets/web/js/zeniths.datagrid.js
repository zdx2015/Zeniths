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
            var ps = {};
            var formParams = $queryForm.serializeArray();
            $.each(formParams, function (i, v) {
                ps[v.name] = v.value;
            });

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
                    zeniths.util.hideLoading(instance.$element);
                    _callback(result);
                    __onLoadSuccess(result,instance.options);
                },
                error: function (result) {
                    zeniths.util.hideLoading(instance.$element);
                    __onLoadError(result, instance.options);
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
            instance.$table.find('thead>tr>th[data-order]').on('click', function () {
                instance.options.order = $(this).data('order');
                var dir = 'asc';
                if (instance.options.dir) {
                    if (instance.options.dir === 'asc') {
                        dir = 'desc';
                    } else {
                        dir = 'asc';
                    }
                }
                instance.options.dir = dir;

                __onSort(instance.options);
                _load();
            }).css('cursor', 'pointer');
        }

        _load();
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

    function __onLoadError(result, options) {
        if (options.onLoadError) {
            options.onLoadError(result);
        } else {
            alert(result.responseJSON.message);
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
        _loadData(this);
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
    DataGrid.prototype.reload = function () {
        _loadData(this);
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
                instance = new DataGrid($this, $.extend({}, $.fn.datagrid.defaults, $.fn.datagrid.parseOptions($this), options));
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
        var properties = ['url', 'order', 'dir'];
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
        return options;
    };

    /*****************************配置默认值*****************************/
    /**
     * 表格配置默认值
     */
    $.fn.datagrid.defaults = {
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
        onLoadSuccess: function (result) { },

        /**
         * 数据加载失败后执行
         */
        onLoadError: function (result) { },

        /**
         * 复选框选中时执行
         * @param {String} id 
         * @returns {} 
         */
        onCheck: function (id) { },

        /**
         * 复选框未选中时执行
         * @param {String} id 
         * @returns {} 
         */
        onUncheck: function (id) { },

        /**
         * 数据排序前执行
         * @param {String} name 
         * @param {String} dir 
         * @returns {} 
         */
        onSort: function (name, dir) { }
    };

})(jQuery);

/**
 * 默认初始化表格对象
 */
$(function () {
    $('.datagrid').datagrid();
});
