using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace Zeniths.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region js

            //bootstrap-js
            bundles.Add(new ScriptBundle("~/bootstrap-js").Include(
                "~/plugin/jquery/jquery.js",
                "~/plugin/bootstrap/js/bootstrap.js"));

            //easyui-tab-tree-js
            bundles.Add(new ScriptBundle("~/easyui-tab-tree-js").Include(
                "~/plugin/jquery/jquery.js",
                "~/plugin/jquery-easyui/js/jquery.parser.js",
                "~/plugin/jquery-easyui/js/jquery.resizable.js",
                "~/plugin/jquery-easyui/js/jquery.panel.js",
                "~/plugin/jquery-easyui/js/jquery.layout.js",
                "~/plugin/jquery-easyui/js/jquery.tree.js",
                "~/plugin/jquery-easyui/js/jquery.tabs.js",
                "~/plugin/jquery-easyui/js/jquery.tabs.extend.js",
                "~/plugin/jquery-loadmask/js/jquery.loadmask.js",
                "~/plugin/zeniths.web/js/zeniths.util.js"
                ));

            bundles.Add(new ScriptBundle("~/zeniths-js").Include(
               "~/plugin/jquery/jquery.js",
               "~/plugin/bootstrap/js/bootstrap.js",
               "~/plugin/bootstrap-popover-x/js/bootstrap-popover-x.js",
               "~/plugin/bootstrap-datepicker/js/bootstrap-datepicker.js",
               "~/plugin/bootstrap-datepicker/js/bootstrap-datepicker.zh-CN.js",
               "~/plugin/jquery-blockui/jquery.blockui.js",
               "~/plugin/jquery-cookie/jquery.cookie.js",
               "~/plugin/jquery-form/jquery.form.js",
               "~/plugin/jquery-icheck/js/icheck.js",
               "~/plugin/jquery-inputlimiter/js/jquery.inputlimiter.js",
               "~/plugin/jquery-maskedinput/js/jquery.maskedinput.js",
               "~/plugin/jquery-select2/js/select2.js",
               "~/plugin/jquery-select2/js/zh-CN.js",
               "~/plugin/jquery-uri/jquery.uri.js",
               "~/plugin/jquery-loadmask/js/jquery.loadmask.js",
               "~/plugin/jquery-validation/jquery.validate.js",
               "~/plugin/jquery-validation/additional-methods.js",
               "~/plugin/jquery-validation/messages_zh.js",
               "~/plugin/zeniths.web/js/date.format.js",
               "~/plugin/zeniths.web/js/string.format.js",
               "~/plugin/zeniths.web/js/zeniths.util.js",
               "~/plugin/zeniths.web/js/zeniths.dataform.js",
               "~/plugin/zeniths.web/js/zeniths.datagrid.js",
               "~/plugin/zeniths.web/js/zeniths.selectmember.js"
               ));

            bundles.Add(new ScriptBundle("~/zeniths-tree-js").Include(
                "~/plugin/jquery-easyui/js/jquery.parser.js",
                "~/plugin/jquery-easyui/js/jquery.draggable.js",
                "~/plugin/jquery-easyui/js/jquery.droppable.js",
                "~/plugin/jquery-easyui/js/jquery.resizable.js",
                "~/plugin/jquery-easyui/js/jquery.panel.js",
                "~/plugin/jquery-easyui/js/jquery.layout.js",
                "~/plugin/jquery-easyui/js/jquery.tree.js",
                "~/plugin/jquery-easyui/js/jquery.menu.js"
                ));


            //syntaxHighlighter
            bundles.Add(new ScriptBundle("~/syntaxHighlighter-js").Include(
                "~/plugin/kindeditor/plugins/syntaxHighlighter/shCore.js",
               "~/plugin/kindeditor/plugins/syntaxHighlighter/scripts/*.js"));


            bundles.Add(new ScriptBundle("~/main-js").Include(
                "~/plugin/jquery/jquery.js",
                "~/plugin/jquery-easyui/js/jquery.parser.js",
                "~/plugin/jquery-easyui/js/jquery.resizable.js",
                "~/plugin/jquery-easyui/js/jquery.panel.js",
                "~/plugin/jquery-easyui/js/jquery.layout.js",
                "~/plugin/jquery-easyui/js/jquery.tree.js",
                "~/plugin/jquery-easyui/js/jquery.tabs.js",
                "~/plugin/jquery-easyui/js/jquery.tabs.extend.js",
                "~/plugin/bootstrap/js/bootstrap.js",
                "~/plugin/bootstrap-popover-x/js/bootstrap-popover-x.js",
                "~/plugin/bootstrap-datepicker/js/bootstrap-datepicker.js",
                "~/plugin/bootstrap-datepicker/js/bootstrap-datepicker.zh-CN.js",
                "~/plugin/jquery-blockui/jquery.blockui.js",
                "~/plugin/jquery-cookie/jquery.cookie.js",
                "~/plugin/jquery-form/jquery.form.js",
                "~/plugin/jquery-icheck/js/icheck.js",
                "~/plugin/jquery-inputlimiter/js/jquery.inputlimiter.js",
                "~/plugin/jquery-maskedinput/js/jquery.maskedinput.js",
                "~/plugin/jquery-select2/js/select2.js",
                "~/plugin/jquery-select2/js/zh-CN.js",
                "~/plugin/jquery-uri/jquery.uri.js",
                "~/plugin/jquery-loadmask/js/jquery.loadmask.js",
                "~/plugin/jquery-validation/jquery.validate.js",
                "~/plugin/jquery-validation/additional-methods.js",
                "~/plugin/jquery-validation/messages_zh.js",
                "~/plugin/zeniths.web/js/date.format.js",
                "~/plugin/zeniths.web/js/string.format.js",
                "~/plugin/zeniths.web/js/zeniths.util.js",
                "~/plugin/zeniths.web/js/zeniths.dataform.js",
                "~/plugin/zeniths.web/js/zeniths.datagrid.js",
                "~/plugin/zeniths.web/js/zeniths.selectmember.js"
                ));

            #endregion

            #region css

            //bootstrap-css
            bundles.Add(new StyleBundle("~/bootstrap-css").Include(
                "~/plugin/bootstrap/css/bootstrap.css",
                "~/plugin/font-awesome/css/font-awesome.css"));

            //easyui-tab-tree-css
            bundles.Add(new StyleBundle("~/easyui-tab-tree-css").Include(
                "~/plugin/jquery-easyui/css/panel.css",
                "~/plugin/jquery-easyui/css/layout.css",
                "~/plugin/jquery-easyui/css/tree.css",
                "~/plugin/jquery-easyui/css/tabs.css",
                "~/plugin/jquery-loadmask/css/jquery.loadmask.css",
                "~/plugin/zeniths.web/css/zeniths.web.css"));

            bundles.Add(new StyleBundle("~/zeniths-css").Include(
               "~/plugin/bootstrap/css/bootstrap.css",
               "~/plugin/bootstrap-popover-x/css/bootstrap-popover-x.css",
               "~/plugin/font-awesome/css/font-awesome.css",
               "~/plugin/bootstrap-datepicker/css/bootstrap-datepicker3.css",
               "~/plugin/jquery-icheck/css/blue.css",
               "~/plugin/jquery-select2/css/select2.css",
               "~/plugin/jquery-select2/css/select2-bootstrap.css",
               "~/plugin/jquery-loadmask/css/jquery.loadmask.css",
               "~/plugin/zeniths.web/css/zeniths.web.css"));

            bundles.Add(new StyleBundle("~/zeniths-tree-css").Include(
                "~/plugin/jquery-easyui/css/panel.css",
                "~/plugin/jquery-easyui/css/layout.css",
                "~/plugin/jquery-easyui/css/tree.css",
                "~/plugin/jquery-easyui/css/menu.css"));

            bundles.Add(new StyleBundle("~/main-css").Include(
                "~/plugin/jquery-easyui/css/panel.css",
                "~/plugin/jquery-easyui/css/layout.css",
                "~/plugin/jquery-easyui/css/tree.css",
                "~/plugin/jquery-easyui/css/tabs.css",
                "~/plugin/bootstrap/css/bootstrap.css",
                "~/plugin/bootstrap-popover-x/css/bootstrap-popover-x.css",
                "~/plugin/font-awesome/css/font-awesome.css",
                "~/plugin/bootstrap-datepicker/css/bootstrap-datepicker3.css",
                "~/plugin/jquery-icheck/css/blue.css",
                "~/plugin/jquery-select2/css/select2.css",
                "~/plugin/jquery-select2/css/select2-bootstrap.css",
                "~/plugin/jquery-loadmask/css/jquery.loadmask.css",
                "~/plugin/zeniths.web/css/zeniths.web.css"
                ));
            #endregion


            BundleTable.EnableOptimizations = true;
        }
    }
}