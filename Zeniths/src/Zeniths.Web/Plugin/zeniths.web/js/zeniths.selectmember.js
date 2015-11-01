var selectmember = function () {

    var self = this;

    /**
     * 初始化控件
     * @returns {} 
     */
    self.init = function () {
        var $controls = $('.selectmember');
        if (self.$parent) {
            $controls = self.$parent.find('.selectmember');
        }
        $controls.each(function () {
            var tmpl = '<div class="input-group">' +
	                        '<span class="input-group-btn">' +
		                        '<button class="btn btn-default" type="button">' +
			                        '<i class="fa fa-search"></i> 选择' +
		                        '</button>' +
	                        '</span>' +
                        '</div>';
            var $tmpl = $(tmpl);
            var $element = $(this);
            var $btn = $tmpl.find('button');
            if ($element.prop("disabled")) {
                $btn.prop("disabled", true);
            }

            //绑定按钮事件
            $btn.on('click', function () {
                var dialogIndex = zeniths.util.dialog('/WorkFlow/SelectMember', 500, 660, {

                    callback: function (data) {

                        if (data.initData) {
                            return { id: $element.data('id') };
                        }
                        else if (data.accpetData) {
                            //console.log(data.data);
                            $element.data('id', data.data.id);
                            $element.data('text', data.data.text);
                            $element.val(data.data.text);
                            zeniths.util.layerClose(dialogIndex);
                        }
                    }
                });

            });


            $element.attr('readonly', true).css('background-color', 'white');
            $element.before($tmpl);
            $tmpl.prepend($element);
            setTimeout(function () {
                self.setText($element);
            }, 200);
        });
    };

    self.setText = function ($element) {
        var text = $element.data('text');
        if (!text) { //没有指定文本值,则使用ajax获取
            var id = $element.data('id');
            if (id) {
                $element.val('读取中...');
                zeniths.util.post('/WorkFlow/SelectMember/GetNames', { ids: id }, function (result) {
                    $element.val(result);
                    $element.data('text', result);
                });
            }
        } else {
            $element.val(text);
        }
    };

    return {
        /**
         * 页面初始化
         * @returns {} 
         */
        init: function ($parent) {
            self.$parent = $parent;
            self.init();
        },
        getNames: function ($element) {
            self.setText($element);
        }
    };
}();

$(function () {
    selectmember.init();
});