/// <reference path="../../jquery/jquery.js" />
(function ($) {

    /*****************************私有函数*****************************/

    /**
     * 组件初始化
     * @param {DataForm} instance 表单实例
     * @returns {} 
     */
    function _init(instance) {

    }

    function _validform(instance) {

        if (!instance.validOptions.rules) {
            instance.validOptions.rules = {};
        }

        $.each(instance.validOptions.rules, function (k, v) {
            var parent = $('form [name=' + k + ']').parent();
            //console.log(parent);
            if (parent.is('.input-group')) {
                parent.parent().siblings('label').addClass('required');
            } if (parent.is('.iradio_square-blue') || parent.is('.icheckbox_square-blue')) {
                parent.closest('.radio-list,.checkbox-list').parent().siblings('label').addClass('required');
            }
            else {
                parent.siblings('label').addClass('required');
            }
        });

        return instance.$element.validate({
            errorElement: 'span',
            errorClass: 'help-block',
            focusInvalid: false,
            ignore: '',
            rules: instance.validOptions.rules,
            messages: instance.validOptions.messages,
            invalidHandler: function (event, validator) {
            },
            highlight: function (element) {
                $(element).closest('.error-container,.form-group').addClass('has-error');
            },
            unhighlight: function (element) {
                $(element).closest('.error-container,.form-group').removeClass('has-error');
            },
            success: function (label) {
                label.closest('.error-container,.form-group').removeClass('has-error');
                label.remove();
            },
            errorPlacement: function (error, element) {
                if (element.parent(".input-group").size() > 0) {
                    error.insertAfter(element.parent(".input-group"));
                } else if (element.parents('.radio-list').size() > 0) {
                    error.appendTo(element.parents('.radio-list'));
                } else if (element.parents('.checkbox-list').size() > 0) {
                    error.appendTo(element.parents('.checkbox-list'));
                } else if (element.is('.select2-control') || element.is('.chosen-control') || element.is('.select-control')) {
                    error.appendTo(element.parent());
                } else {
                    error.insertAfter(element);
                }
            },
            submitHandler: function (form) {
                var res = __onBeforeSubmit(instance);
                if (res && instance.options.ajaxFormOptions != null) { //表面是ajax提交
                    instance.$element.ajaxSubmit(instance.options.ajaxFormOptions);
                }
                return res;
            }
        });
    }

    /*****************************事件函数*****************************/

    function __onBeforeSubmit(instance) {
        if (instance.options.onBeforeSubmit) {
            return options.onBeforeSubmit(instance);
        }
        return true;
    }

    function __onSubmitSuccess(instance) {
        if (instance.options.onSubmitSuccess) {
            return options.onSubmitSuccess(instance);
        }
    }

    function __onSubmitError(instance) {
        if (instance.options.onSubmitError) {
            return options.onSubmitError(instance);
        }
    }


    /*****************************构造函数*****************************/
    /**
     * 组件构造函数
     * @param {} $element 
     * @param {} options 
     * @returns {} 
     */
    var DataForm = function ($element, options) {
        this.$element = $element;
        this.options = options;
    };

    /*****************************公共函数*****************************/

    /**
     * 初始化富文本编辑器控件
     * @param {Object} options 控件配置
     * @returns {DataForm}
     */
    DataForm.prototype.initKindEditor = function (options) {
        //var editor = { "id": ["desc"], "tools": "simpleTools" };

        var bugTools =
        [
            'formatblock', 'fontname', 'fontsize', '|', 'forecolor', 'hilitecolor', 'bold', 'italic', 'underline', '|',
            'justifyleft', 'justifycenter', 'justifyright', 'insertorderedlist', 'insertunorderedlist', '|',
            'emoticons', 'image', 'code', 'link', '|', 'removeformat', 'undo', 'redo', 'fullscreen', 'source', 'about'
        ];

        var simpleTools =
        [
            'formatblock', 'fontname', 'fontsize', '|', 'forecolor', 'hilitecolor', 'bold', 'italic', 'underline', '|',
            'justifyleft', 'justifycenter', 'justifyright', 'insertorderedlist', 'insertunorderedlist', '|',
            'emoticons', 'image', 'code', 'link', '|', 'removeformat', 'undo', 'redo', 'fullscreen', 'source', 'about'
        ];

        var fullTools =
        [
            'formatblock', 'fontname', 'fontsize', 'lineheight', '|', 'forecolor', 'hilitecolor', '|', 'bold', 'italic', 'underline', 'strikethrough', '|',
            'justifyleft', 'justifycenter', 'justifyright', 'justifyfull', '|',
            'insertorderedlist', 'insertunorderedlist', '|',
            'emoticons', 'image', 'insertfile', 'hr', '|', 'link', 'unlink', '/',
            'undo', 'redo', '|', 'selectall', 'cut', 'copy', 'paste', '|', 'plainpaste', 'wordpaste', '|', 'removeformat', 'clearhtml', 'quickformat', '|',
            'indent', 'outdent', 'subscript', 'superscript', '|',
            'table', 'code', '|', 'pagebreak', 'anchor', '|',
            'fullscreen', 'source', 'preview', 'about'
        ];

        var ops = $.extend({}, {
            cssPath: ['/assets/bootstrap/css/bootstrap.css'],
            resizeType: 1,
            allowPreviewEmoticons: false,
            allowImageUpload: false,
            bodyClass: 'article-content',
            afterBlur: function () {
                this.sync();
                $editor.prev('.ke-container').removeClass('focus');
            },
            afterFocus: function () { $editor.prev('.ke-container').addClass('focus'); },
            afterChange: function () {
                $editor.change().hide();
            },
            items: fullTools
        }, options);

        var editor;
        var $editor = $('.editor-control');
        KindEditor.ready(function (K) {
            editor = K.create('.editor-control', ops);
        });
        return this;
    };

    /**
     * 初始化切换控件
     * @param {Object} options 控件配置
     * @returns {DataForm} 
     */
    DataForm.prototype.initSwitch = function (options) {
        //bootstrap-switch
        $('.switch-control').bootstrapSwitch(options);
        return this;
    };

    /**
     * 初始化单选复选控件
     * @param {Object} options 控件配置
     * @returns {DataForm} 
     */
    DataForm.prototype.initiCheck = function (options) {
        //jquery-icheck
        var self = this;
        var ops = $.extend({}, {
            checkboxClass: 'icheckbox_square-blue',
            radioClass: 'iradio_square-blue',
            increaseArea: '20%'
        }, options);
        $('.icheckbox-control,.iradiobox-control').iCheck(ops);
        $('.icheckbox-control,.iradiobox-control').on('ifToggled', function (e) {
            self.$element.validate().element($(this));
        });

        return this;
    };

    /**
     * 初始化下拉选择Select2控件
     * @param {Object} options 控件配置
     * @returns {DataForm} 
     */
    DataForm.prototype.initSelect2 = function (options) {
        //select2-bootstrap
        var ops = $.extend({}, {
            theme: "bootstrap",
            language: 'zh-CN'
        }, options);
        $('.select2-control').select2(ops);
        return this;
    };

    /**
     * 初始化下拉选择Chosen控件
     * @param {Object} options 控件配置
     * @returns {DataForm} 
     */
    DataForm.prototype.initChosen = function (options) {
        //jquery-chosen
        var ops = $.extend({}, {
            placeholder_text: ' ',
            no_results_text: '没有匹配结果'
        }, options);
        $('.chosen-control').chosen(ops);
        return this;
    };

    /**
     * 初始化下拉选择Select控件
     * @param {Object} options 控件配置
     * @returns {DataForm} 
     */
    DataForm.prototype.initSelectPicker = function (options) {
        //bootstrap-select
        var ops = $.extend({}, {
            liveSearch: 'true'
        }, options);
        $('.select-control').selectpicker(ops);
        return this;
    };

    /**
     * 初始化时间控件
     * @param {Object} options 控件配置
     * @returns {DataForm} 
     */
    DataForm.prototype.initTimePicker = function (options) {
        //bootstrap-timepicker
        var self = this;
        var ops = $.extend({}, {
            //defaultTime: false,
            minuteStep: 1,
            secondStep: 1,
            showSeconds: true,
            showMeridian: false
            //showInputs: false,
            //disableFocus: setting.disableFocus && setting.disableFocus == 'true',
            //disableMousewheel: true,
            //modalBackdrop:true
        }, options);
        $('.time-control').timepicker(ops);
        $('.time-control').timepicker().on('changeTime.timepicker', function (e) {
            self.$element.validate().element($(this));
        });
        return this;
    };

    /**
     * 初始化时钟控件
     * @param {Object} options 控件配置
     * @returns {DataForm} 
     */
    DataForm.prototype.initClockPicker = function (options) {
        //bootstrap-clockpicker
        var self = this;
        var ops = $.extend({}, {
            //autoclose:true
            default: 'now'
        }, options);
        $('.clock-control').clockpicker(ops);
        $('.clock-control').clockpicker().find('input').change(function () {
            self.$element.validate().element($(this));
        });
        return this;
    };

    /**
     * 初始化日期控件
     * @param {Object} options 控件配置
     * @returns {DataForm} 
     */
    DataForm.prototype.initDatePicker = function (options) {
        //bootstrap-datepicker
        var self = this;
        var ops = $.extend({}, {
            format: "yyyy-mm-dd",
            todayBtn: "linked",
            language: "zh-CN",
            autoclose: true,
            todayHighlight: true
        }, options);
        $('.date-control').datepicker(ops);

        $('.date-control').datepicker().on('changeDate', function (e) {
            self.$element.validate().element($(this));
        });

        return this;
        //$('.input-daterange').datepicker({
        //    format: "yyyy-mm-dd",
        //    todayBtn: "linked",
        //    language: "zh-CN",
        //    autoclose: true,
        //    todayHighlight: true
        //});
    };

    /**
     * 初始化日期时间控件
     * @param {Object} options 控件配置
     * @returns {DataForm} 
     */
    DataForm.prototype.initDateTimePicker = function (options) {
        //bootstrap-datetimepicker
        var ops = $.extend({}, {
            showTodayButton: true,
            showClear: true,
            //showClose: true,
            dayViewHeaderFormat: 'YYYY年MM月',
            format: 'YYYY-MM-DD HH:mm',
            useCurrent: false,
            //sideBySide: true,
            keepOpen: true,
            useStrict: true,
            tooltips: {
                today: '今天',
                clear: '清除',
                close: '关闭',
                selectMonth: '选择月',
                prevMonth: '上个月',
                nextMonth: '下个月',
                selectYear: '选择年',
                prevYear: '前一年',
                nextYear: '下一年',
                selectDecade: '选择年代',
                prevDecade: '前一个年代',
                nextDecade: '下一个年代',
                prevCentury: '上个世纪',
                nextCentury: '下个世纪',
                selectTime: '选择时间'
            }
        }, options);
        $('.datetime-control').datetimepicker(ops);
        return this;
    };

    /**
     * 初始化日期范围控件
     * @param {Object} options 控件配置
     * @returns {DataForm} 
     */
    DataForm.prototype.initDateRangePicker = function (options) {
        //Date Range Picker
        var self = this;
        var ops = $.extend({}, {
            showDropdowns: true,
            showWeekNumbers: true,
            linkedCalendars: false,
            autoUpdateInput: false,
            //timePicker: true,
            //timePicker24Hour: true,
            ranges: {
                '今天': [moment(), moment()],
                '昨天': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
                '7天前': [moment().subtract(6, 'days'), moment()],
                '30天前': [moment().subtract(29, 'days'), moment()],
                '本月': [moment().startOf('month'), moment().endOf('month')],
                '上月': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
            },
            locale: {
                format: 'YYYY-MM-DD',
                applyLabel: '确定',
                cancelLabel: '取消',
                clearLabel: '清除',
                fromLabel: '开始',
                toLabel: '结束',
                weekLabel: '周',
                separator: ' - ',
                customRangeLabel: '自定义',
                daysOfWeek: moment.weekdaysMin(),
                monthNames: moment.monthsShort(),
                firstDay: 1
            }
        }, options);

        $('.daterange-control').daterangepicker(ops, function (start, end, label) { });
        $('.daterange-control').on('apply.daterangepicker', function (e) {
            self.$element.validate().element($(this));
        });

        return this;
    };

    /**
     * 初始化全部控件,除initKindEditor.需要的话手动调用initKindEditor(options);
     * @returns {DataForm}
     */
    DataForm.prototype.initPlugin = function () {
        this.initSwitch();
        this.initiCheck();
        this.initSelect2();
        this.initChosen();
        this.initSelectPicker();
        this.initTimePicker();
        this.initClockPicker();
        this.initDatePicker();
        this.initDateTimePicker();
        this.initDateRangePicker();
        return this;
    };

    /**
     * 表单验证
     * @param {Object} options 表单验证配置 
     * @returns {DataForm} 
     */
    DataForm.prototype.initValidate = function (options) {
        $.extend(this.options.validOptions, options);
        if (!this.options.validOptions) {
            this.options.validOptions = {};
        }
        _validform(this);
        return this;
    };

    /**
     * 表单异步提交
     * @param {Object} options 异步提交配置 
     * @returns {DataForm} 
     */
    DataForm.prototype.initAjaxForm = function (options) {
        $.extend(this.options.ajaxFormOptions, options);
        if (!this.options.ajaxFormOptions) {
            this.options.ajaxFormOptions = {};
        }
        if (this.options.ajaxFormOptions.success) {
            
        }
        return this;
    };

    /*****************************表单插件*****************************/
    /**
     * 表单插件
     * @param {Object} options 配置对象
     * @returns {DataGrid} 返回第一个表单实例
     */
    $.fn.dataform = function (options) {
        var instances = [];
        options = options || {};

        this.each(function () {
            var $this = $(this);
            var instance = $this.data('dataform');
            if (instance) {
                $.extend(instance.options, options);
            } else {
                instance = new DataForm($this, $.extend({}, $.fn.dataform.defaults));
                $this.data('dataform', instance);
            }
            instances.push(instance);
        });
        return instances[0];
    };

    /*****************************配置默认值*****************************/
    /**
     * 表单配置默认值
     */
    $.fn.dataform.defaults = {
        validOptions: null,
        ajaxFormOptions: null,
        /**
         * 表单提交前执行
         */
        onBeforeSubmit: function (instance) { return true; },
        onSubmitSuccess: function (instance) { },
        onSubmitError: function (instance) { }
    };

})(jQuery);