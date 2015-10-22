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

        if (!instance.options.validOptions.rules) {
            instance.options.validOptions.rules = {};
        }

        //if (instance.options.validOptions.autoAddRequired === true) {
        $.each(instance.options.validOptions.rules, function (k, v) {
            if (v.required === true) {
                $('form [name=' + k + ']').closest('td').prev('th').prepend('<label class="required"></label>');
            }
            
            //var parent = $('form [name=' + k + ']').parent();
            ////console.log(parent);
            //if (parent.is('.input-group')) {
            //    parent.parent().siblings('label').addClass('required');
            //} if (parent.is('.iradio_square-blue') || parent.is('.icheckbox_square-blue')) {
            //    parent.closest('.radio-list,.checkbox-list').parent().siblings('label').addClass('required');
            //}
            //else {
            //    parent.siblings('label').addClass('required');
            //}
        });
        //}

        var defaults = {
            errorElement: 'span',
            errorClass: 'help-block',
            focusInvalid: false,
            ignore: '',
            invalidHandler: function (event, validator) {
            },
            highlight: function (element) {
                //$(element).closest('.error-container,.form-group').addClass('has-error');
                //$(element).closest('tr').addClass('has-error');
                $(element).closest('td').removeClass("has-success").addClass('has-error');
                $(element).closest('td').prev('th').removeClass("has-success").addClass('has-error');
            },
            unhighlight: function (element) {
                //$(element).closest('.error-container,.form-group').removeClass('has-error');
                //$(element).closest('tr').removeClass('has-error');
            },
            success: function (label, element) {
                //label.closest('.error-container,.form-group').removeClass('has-error');
                //label.remove();
                var icon = $(element).parent('.input-icon').children('i');
                icon.removeClass("fa-warning").addClass("fa-check");
                $(element).closest('td').removeClass('has-error').addClass('has-success');
                $(element).closest('td').prev('th').removeClass('has-error').addClass('has-success');
            },
            errorPlacement: function (error, element) {
                //if (element.parent(".input-group").size() > 0) {
                //    error.insertAfter(element.parent(".input-group"));
                //} else if (element.parents('.radio-list').size() > 0) {
                //    error.appendTo(element.parents('.radio-list'));
                //} else if (element.parents('.checkbox-list').size() > 0) {
                //    error.appendTo(element.parents('.checkbox-list'));
                //} else if (element.is('.select2-control') || element.is('.chosen-control') || element.is('.select-control')) {
                //    error.appendTo(element.parent());
                //} else {
                //    error.insertAfter(element);
                //}
                //var errorElement = element.siblings('.help-block');
                //if (errorElement.length == 0) {
                //    error.appendTo(element.parent());
                //}

                //error.appendTo(element.parent());

                var icon = $(element).parent('.input-icon').children('i');
                if (icon.length == 0) {
                    icon = $('<i class="fa"></i>');
                    $(element).parent('.input-icon').prepend(icon[0]);
                }
                icon.removeClass('fa-check').addClass("fa-warning");
                icon.attr('data-original-title', error.text())
                    .attr('data-placement', 'left')
                    .tooltip({ 'container': 'body' });
            },
            submitHandler: function (form) {
                if (instance.options.validOptions.beforeSubmit) {
                    var result = instance.options.validOptions.beforeSubmit(instance);
                    if (result == undefined || result == true) {
                        if (instance.options.ajaxOptions) {
                            zeniths.util.mask('正在提交数据,请稍等...');
                            if (!instance.options.ajaxOptions.data) {
                                instance.options.ajaxOptions.data = {};
                            }
                            instance.$element.find('input[type=checkbox]').each(function () {
                                var name = $(this).attr('name');
                                var checked = $(this).prop('checked');
                                if (!checked) {
                                    instance.options.ajaxOptions.data[name] = checked;
                                }
                            });
                            instance.$element.ajaxSubmit(instance.options.ajaxOptions);
                        }
                    } else {
                        return false;
                    }
                }
            }
        };
        var ops = $.extend({}, defaults, instance.options.validOptions);
        return instance.$element.validate(ops);
    }

    /*****************************事件函数*****************************/


    /*****************************构造函数*****************************/
    /**
     * 组件构造函数
     * @param {JQuery} $element 
     * @param {Object} options 
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

        //var bugTools =
        //[
        //    'formatblock', 'fontname', 'fontsize', '|', 'forecolor', 'hilitecolor', 'bold', 'italic', 'underline', '|',
        //    'justifyleft', 'justifycenter', 'justifyright', 'insertorderedlist', 'insertunorderedlist', '|',
        //    'emoticons', 'image', 'code', 'link', '|', 'removeformat', 'undo', 'redo', 'fullscreen', 'source', 'about'
        //];

        //var simpleTools =
        //[
        //    'formatblock', 'fontname', 'fontsize', '|', 'forecolor', 'hilitecolor', 'bold', 'italic', 'underline', '|',
        //    'justifyleft', 'justifycenter', 'justifyright', 'insertorderedlist', 'insertunorderedlist', '|',
        //    'emoticons', 'image', 'code', 'link', '|', 'removeformat', 'undo', 'redo', 'fullscreen', 'source', 'about'
        //];

        //var fullTools =
        //[
        //    'formatblock', 'fontname', 'fontsize', 'lineheight', '|', 'forecolor', 'hilitecolor', '|', 'bold', 'italic', 'underline', 'strikethrough', '|',
        //    'justifyleft', 'justifycenter', 'justifyright', 'justifyfull', '|',
        //    'insertorderedlist', 'insertunorderedlist', '|',
        //    'emoticons', 'image', 'insertfile', 'hr', '|', 'link', 'unlink', '/',
        //    'undo', 'redo', '|', 'selectall', 'cut', 'copy', 'paste', '|', 'plainpaste', 'wordpaste', '|', 'removeformat', 'clearhtml', 'quickformat', '|',
        //    'indent', 'outdent', 'subscript', 'superscript', '|',
        //    'table', 'code', '|', 'pagebreak', 'anchor', '|',
        //    'fullscreen', 'source', 'preview', 'about'
        //];

        var ops = $.extend({}, {
            cssPath: ['/plugin/bootstrap/css/bootstrap.css'],
            resizeType: 1,
            allowPreviewEmoticons: true,
            allowImageUpload: true,
            uploadJson: '/plugin/kindeditor/asp.net/upload_json.ashx',
            fileManagerJson: '/plugin/kindeditor/asp.net/file_manager_json.ashx',
            allowFileManager: true,
            //bodyClass: 'article-content',
            afterBlur: function () {
                this.sync();
                $editor.prev('.ke-container').removeClass('focus');
            },
            afterFocus: function () { $editor.prev('.ke-container').addClass('focus'); },
            afterChange: function () {
                $editor.change().hide();
            }
            //,items: fullTools
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
     * 初始化IP地址控件
     * @param {Object} options 控件配置
     * @returns {} 
     */
    DataForm.prototype.initIpAddress = function(options) {
        $('.ip-control').ipAddress(options);
    };

    /**
     * 初始化下拉选择Select2控件
     * @param {Object} options 控件配置
     * @returns {DataForm} 
     */
    DataForm.prototype.initSelect2 = function (options) {
        //select2-bootstrap
        var self = this;
        var ops = $.extend({}, {
            theme: "bootstrap",
            allowClear: true,
            language: 'zh-CN'
        }, options);
        $('.select2-control').select2(ops);
        $('.select2-control').on("change", function () {
            self.$element.validate().element($(this));
        });

        return this;
    };

    /**
     * 初始化TagsInput控件
     * @param {Object} options 控件配置
     * @returns {DataForm} 
     */
    DataForm.prototype.initTagsInput = function (options) {
        var self = this;
        var ops = $.extend({}, {
            'defaultText': '',
            'minChars': 1
        }, options);
        $('.tag-control').tagsInput(ops);

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
        $('.date-control').next('span.input-group-addon').on('click', function () {
            $(this).prev().datepicker('show');
        });


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
        //    .on("dp.change", function (e) {
        //    //$(this).data("DateTimePicker").hide();
        //});
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
     * 初始化颜色控件
     * @param {Object} options 控件配置
     * @returns {DataForm} 
     */
    DataForm.prototype.initColorPicker = function (options) {
        //colorpickersliders
        var self = this;
        var ops = $.extend({}, {
            size: 'sm',
            hsvpanel: true,
            sliders: false,
            previewformat: 'hex',
            order: {
                rgb: 1,
                preview: 2,
                //hsl: 3,
                opacity: 3
            },
            onchange: function (container, color) {
                //console.log(container);
                //self.$element.validate().element($(this));
            }
        }, options);

        $('.color-control').ColorPickerSliders(ops);

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
        this.initColorPicker();
        return this;
    };

    /**
     * 表单验证
     * @param {Object} options 表单验证配置 
     * @returns {DataForm} 
     */
    DataForm.prototype.initValidate = function (options) {

        if (!this.options.validOptions) {
            this.options.validOptions = {};
        }

        $.extend(this.options.validOptions, options);

        _validform(this);
        return this;
    };

    /**
     * 表单异步提交
     * @param {Object} options 异步提交配置 
     * @returns {DataForm} 
     */
    DataForm.prototype.initAjax = function (options) {

        if (!this.options.ajaxOptions) {
            this.options.ajaxOptions = {};
        }
        if (!this.options.ajaxOptions.error) {
            this.options.ajaxOptions.error = function (request) {
                var msg = result.responseJSON.message;
                zeniths.util.alert(msg);
            };
        }

        if (!this.options.ajaxOptions.success) {
            this.options.ajaxOptions.success = function (result) {
                zeniths.util.unmask();
                if (!result.success) {
                    zeniths.util.alert(result.message);
                }
            };
        }

        $.extend(this.options.ajaxOptions, options);
        return this;
    };

    /*****************************表单插件*****************************/
    /**
     * 表单插件
     * @param {Object} options 配置对象
     * @returns {DataForm} 返回第一个表单实例
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
        ajaxOptions: null
    };

})(jQuery);