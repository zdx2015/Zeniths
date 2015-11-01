var flowDesign = function () {

    var self = this;
    self.$flowPanel = $('#flowPanel');
    self.flowPanelObject = null;
    self.flowId = '';
    self.designJson = {
        property: {},
        flow: {},
        step: {},
        line: {}
    };

    /**
     * 加载流程图
     * @returns {} 
     */
    self.loadFlow = function () {
        self.flowPanelObject = $.createGooFlow($flowPanel, {
            width: $(window).width() - 18,
            height: $(window).height() - 65,
            haveHead: true,
            headBtns: ['undo', 'redo'],
            haveTool: true,
            toolBtns: ['startround', 'endround', 'stepnode', 'shuntnode', 'confluencenode'],
            haveGroup: true,
            useOperStack: true,
            lineSetting: self.lineSetting,
            stepSetting: self.stepSetting
        });
        self.flowPanelObject.setNodeRemarks({
            cursor: '选择指针',
            direct: '步骤连线',
            startround: '开始节点',
            endround: '结束节点',
            stepnode: '普通节点',
            shuntnode: '分流节点',
            confluencenode: '合流节点',
            group: '区域规划'
        });

        if (self.flowId) {
            zeniths.util.mask('正在加载流程图...');
            $.ajax({
                url: $('#loadSettingUrl').val(),
                type: 'post',
                dataType: 'json',
                data: { id: self.flowId },
                async: false,
                success: function (data) {
                    zeniths.util.unmask();
                    self.designJson = data;
                    var json = data.flow;
                    if (json) {
                        self.flowPanelObject.loadData(json);
                    }
                },
                error: function (result) {
                    zeniths.util.unmask();
                    var msg = result.responseJSON.message;
                    zeniths.util.alert(msg);
                }
            });
        }
    };

    /**
     * 保存验证
     * @returns {} 
     */
    self.validFlow = function () {
        if (!self.designJson.property.id) {
            self.flowSetting();
            zeniths.util.msg('请配置流程属性');
            return false;
        }
        if (!self.designJson.property.name) {
            self.flowSetting();
            zeniths.util.msg('请配置流程名称');
            return false;
        }

        return true;
    };

    /**
     * 保存流程图
     * @returns {} 
     */
    self.saveFlow = function () {

        if (!validFlow()) return;

        zeniths.util.mask('正在保存数据...');
        window.setTimeout(function () {
            self.designJson.flow = self.flowPanelObject.exportData();

            $.each(self.designJson.step, function (k, v) { //删除已经被删除掉的步骤配置信息
                if (!self.designJson.flow.nodes[k]) {
                    delete self.designJson.step[k];
                }
            });
            $.each(self.designJson.line, function (k, v) {//删除已经被删除掉的线配置信息
                if (!self.designJson.flow.lines[k]) {
                    delete self.designJson.line[k];
                }
            });

            var postData = {
                json: JSON.stringify(self.designJson)
            }
            zeniths.util.post($('#saveSettingUrl').val(), postData, function (result) {
                zeniths.util.unmask();
                if (result.success) {
                    zeniths.util.msg('保存成功');
                } else {
                    zeniths.util.msg(result.message);
                }
            });
        }, 200);
    };

    /**
     * 属性设置
     * @returns {} 
     */
    self.flowSetting = function () {
        var url = new URI($('#flowSettingUrl').val()).addSearch({ id: self.flowId, windowId: window.name }).toString();
        zeniths.util.dialog(url, 800, 615);
    };

    /**
     * 步骤设置
     * @returns {} 
     */
    self.stepSetting = function () {
        var stepId = $('.item_focus').attr('id');
        var nodeData = self.flowPanelObject.$nodeData[stepId];
        if (!self.designJson.step[stepId]) {
            self.designJson.step[stepId] = {
                uid: nodeData.uid,
                name: nodeData.name
            };
        }
        var url = new URI($('#stepSettingUrl').val()).addSearch({ id: self.flowId, stepId: stepId, windowId: window.name }).toString();
        zeniths.util.dialog(url, 800, 690);
    };

    /**
     * 线设置
     * @returns {} 
     */
    self.lineSetting = function (lineId) {
        if (!self.designJson.line[lineId]) {
            self.designJson.line[lineId] = {};
        }
        var url = new URI($('#lineSettingUrl').val()).addSearch({ id: self.flowId, lineId: lineId, windowId: window.name }).toString();
        zeniths.util.dialog(url, 800, 625);
    };

    /**
     * 绑定按钮事件
     * @returns {} 
     */
    self.initButton = function () {

        $('#saveSetting').click(function () {
            self.saveFlow();
        });

        $('#flowSetting').click(function () {
            self.flowSetting();
        });

        $('#flowUndo').click(function () {
            $('.GooFlow_head_btn').find('.ico_undo').trigger('click');
        });

        $('#flowRedo').click(function () {
            $('.GooFlow_head_btn').find('.ico_redo').trigger('click');
        });

        $('#flowReresh').click(function () {
            window.location.reload();
        });

        $('#flowBack').click(function () {
            zeniths.util.callDialogCallback(window);
            zeniths.util.closeFrameDialog(window);
        });
    };

    return {
        /**
         * 页面初始化
         * @returns {} 
         */
        init: function (flowId) {
            document.onselectstart = function () { return false; };
            self.flowId = flowId;
            self.initButton();
            self.loadFlow();
        },
        getProperty: function () {
            return self.designJson.property;
        },
        setProperty: function (data) {
            $.extend(self.designJson.property, data);
        },
        getStep: function (stepId) {
            return self.designJson.step[stepId];
        },
        setStep: function (stepId, data) {
            $.extend(self.designJson.step[stepId], data);
            $('.item_focus table tr:eq(0) td:eq(1)').text(data.name);
            self.flowPanelObject.setName(stepId, data.name, 'node');
        },
        getLine: function (lineId) {
            return self.designJson.line[lineId];
        },
        setLine: function (lineId, data) {
            $.extend(self.designJson.line[lineId], data);
            $('#' + lineId + ' text').text(data.name);
            self.flowPanelObject.setName(lineId, data.name, 'line');
        },
        getFlow: function () {
            return self.flowPanelObject.exportData();
        }
    };
}();

