var workflow = function () {

    var self = this;
    
    /**
     * 绑定按钮事件
     * @returns {} 
     */
    self.initButton = function () {

    };

    return {
        /**
         * 页面初始化
         * @returns {} 
         */
        init: function () {
            self.initButton();
        }
    };
}();

$(function () {
    workflow.init();
});