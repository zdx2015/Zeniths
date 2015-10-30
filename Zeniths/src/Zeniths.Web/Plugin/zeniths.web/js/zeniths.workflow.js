var zeniths = zeniths || {};

/**
 * 工作流
 */
zeniths.workflow = function () {

    var self = this;
    self.$formElement = null;
    self.workflow_execute_params = null;
    self.workflow_client_model = null;
    self.selectStepDialogIndex = -1;

    self.saveCallback = null;
    self.sendCallback = null;
    self.backCallback = null;
    self.completedCallback = null;

    self.flowSave = function () {
        if (!self.$formElement.valid()) {
            return;
        }
        self.workflow_execute_params.type = "save";
        self.workflow_execute_params.steps = [];
        self._flowCommit();
    };

    self.flowSend = function () {
        //验证表单
        if (!self.valid()) {
            return;
        }

        self.selectStep();
    };

    self.flowBack = function () {
        //验证表单
        if (!self.valid()) {
            return;
        }
    };

    self.flowCompleted = function () {
        //验证表单
        if (!self.valid()) {
            return;
        }

        zeniths.util.confirm('确定要完成流程吗?', function (index) {
            self.workflow_execute_params.type = "completed";
            self.workflow_execute_params.steps = [];
            self._flowCommit();
        });
    };

    self._flowCommit = function () {
        if (!self.workflow_execute_params || !self.workflow_execute_params.type || !self.workflow_execute_params.steps) {
            zeniths.util.alert("参数不足!");
            return false;
        }
        if (self.workflow_execute_params.type.toLowerCase() != "save" && self.workflow_execute_params.type.toLowerCase() != "completed" && self.workflow_execute_params.steps.length == 0) {
            zeniths.util.alert("没有要处理的步骤!");
            return false;
        }
        self.showProcessing(self.workflow_execute_params.type);
        top.layer.close(self.selectStepDialogIndex);
        //window.setTimeout('', 100);

        //"title":null,"opinion":null,"isAudit":null,"type":null,"steps":[]

        self.$formElement.ajaxSubmit({
            data: {
                _workflow_execute_params: JSON.stringify(self.workflow_execute_params)
            },
            success: function (result) {
                $(top.document.body).unmask();
                zeniths.util.alert(result.message, { icon: 1 }, function (idx) {
                    top.layer.close(idx);

                    if (self.workflow_execute_params.type = "save" && self.saveCallback) {
                        self.saveCallback(self, result);
                    }

                    if (self.workflow_execute_params.type = "submit" && self.sendCallback) {
                        self.sendCallback(self, result);
                    }

                    if (self.workflow_execute_params.type = "back" && self.backCallback) {
                        self.backCallback(self, result);
                    }

                    if (self.workflow_execute_params.type = "completed" && self.completedCallback) {
                        self.completedCallback(self, result);
                    }

                });
            },
            error: function (result) {
                $(top.document.body).unmask();
                var msg = result.responseJSON.message;
                zeniths.util.alert(msg);
            }
        });
    };

    self.showProcessing = function (type) {
        var title = "正在处理...";
        switch (type) {
            case "save": title = "正在保存..."; break;
            case "submit": title = "正在发送..."; break;
            case "back": title = "正在退回..."; break;
            case "redirect": title = "正在转交..."; break;
        }
        $(top.document.body).mask(title + ',请稍等...');
    };

    self.selectStep = function () {
        $(top.document.body).mask('正在准备数据,请稍等亲...');
        var data = {
            flowId: self.workflow_client_model.flowId,
            stepId: self.workflow_client_model.stepId,
            taskId: self.workflow_client_model.taskId,
            flowInstanceId: self.workflow_client_model.flowInstanceId,
            businessId: self.workflow_client_model.businessId
        };
        $.extend(data, zeniths.util.getFormData(self.$formElement));

        var showDialog = function (result) {
            top.index.execute(function (window, document, $) {
                var $body = $(document.body);
                var $dom = $('<div id="_workflow_send" style="display: none;"></div>');
                $body.append($dom);
                var $target = $body.find('#_workflow_send');
                $target.empty();
                $target.append(result);

                var _flowSubmit = function () {
                    self.workflow_execute_params.type = "submit";
                    self.workflow_execute_params.steps = [];
                    var isSubmit = true;
                    $target.find(':checked[name="step"]').each(function () {
                        var step = $(this).val();
                        var member = $target.find("#user_" + step).data('id') || '';
                        if (member.length == 0) {
                            zeniths.util.alert($(this).next().text() + ' 没有选择处理人员!');
                            isSubmit = false;
                            return false;
                        }
                        self.workflow_execute_params.steps.push({ id: step, member: member });
                    });
                    if (self.workflow_execute_params.steps.length == 0) {
                        zeniths.util.alert("没有选择要处理的步骤!");
                        return false;
                    }
                    if (isSubmit) {
                        self._flowCommit();
                    }
                };

                self.selectStepDialogIndex = top.layer.open({
                    type: 1,
                    closeBtn: 2,
                    skin: 'layui-layer-zdx',
                    title: ' ',
                    area: '500px',
                    content: $target,
                    success: function (layero, idx) {
                        $body.unmask();
                        selectmember.init($body);
                        $target.find('form').dataform().initiCheck();

                        $target.find('#btnWorkFlowSendOK').click(function () {
                            _flowSubmit();
                        });
                        $target.find('#btnWorkFlowSendCancel').click(function () {
                            top.layer.close(self.selectStepDialogIndex);
                        });
                    }
                });
            });
        };

        var url = '/WorkFlow/FlowRun/Send';
        zeniths.util.post(url, data, showDialog);
    };

    self.valid = function () {
        if (!self.$formElement.valid()) {
            return false;
        }

       self.workflow_execute_params.opinion = self.$formElement.find('[name=workflow_opinion]').val();
       
        if (self.workflow_client_model.isNeedSignature &&
            self.workflow_execute_params.isSignature == false) {
            self.workflow_execute_params.isSignature =
                self.$formElement.find('#workflow_signature').val()
                || self.$formElement.find('[name=workflow_signature]').val();
        }


        if (self.workflow_client_model.isNeedOpinion
            && $.trim(self.workflow_execute_params.opinion).length == 0) {
            zeniths.util.msg('请填写处理意见');
            return false;
        }
        if (self.workflow_client_model.isNeedSignature
            && self.workflow_execute_params.isSignature == false) {
            zeniths.util.msg('请先签章');
            return false;
        }
        return true;
    };

    /**
     * 绑定按钮事件
     * @returns {} 
     */
    self.initButton = function () {
        $('.btnWorkFlowSave').click(self.flowSave);
        $('.btnWorkFlowSend').click(self.flowSend);
        $('.btnWorkFlowBack').click(self.flowBack);
        $('.btnWorkFlowCompleted').click(self.flowCompleted);
    };

    self.initFormSubmit = function () {
        if (!self.$formElement.dataform().options.validOptions) return;

        var bsFn = self.$formElement.dataform().options.validOptions.beforeSubmit;
        if (bsFn) {
            self.$formElement.dataform().options.validOptions.beforeSubmit = function () {
                bsFn(self.$formElement.dataform());
                return false;
            };
        } else {
            self.$formElement.dataform().options.validOptions.beforeSubmit = function () {
                return false;
            };
        }
    };

    return {
        /**
         * 页面初始化
         * @returns {} 
         */
        init: function (_workflow_execute_params, _workflow_client_model) {
            self.workflow_execute_params = _workflow_execute_params;
            self.workflow_client_model = _workflow_client_model;

            self.initButton();
        },
        /**
         * 初始化流程表单
         * @param {} $formElement 
         * @param {} ops 
         * @returns {} 
         */
        initForm: function ($formElement, ops) {
            self.$formElement = $formElement;
            self.initFormSubmit();

            self.saveCallback = ops.saveCallback;
            self.sendCallback = ops.sendCallback;
            self.backCallback = ops.backCallback;
            self.completedCallback = ops.completedCallback;
        },
        redirect: function (url) {
            if (url) {
                $(document.body).mask('正在前往下一步骤,请稍等...');
                window.location.href = url;
            }
        },
        /**
         * 设置流程参数
         * @param {} params 
         * @returns {} 
         */
        setExecuteParam: function (params) {
            $.extend(self.workflow_execute_params, params);
        },

        /**
         * 流程表单保存
         * @returns {} 
         */
        flowSave: function () {
            self.flowSave();
        },
        /**
         * 流程表单发送
         * @returns {} 
         */
        flowSend: function () {
            self.flowSend();
        },
        /**
         * 流程表单退回
         * @returns {} 
         */
        flowBack: function () {
            self.flowBack();
        },
        /**
         * 流程表单完成
         * @returns {} 
         */
        flowCompleted: function () {
            self.flowCompleted();
        }
    };
}();