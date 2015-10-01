// ===============================================================================
// Copyright (c) 2015 正得信集团股份有限公司
// ===============================================================================
using System;
using System.Windows.Forms;
using Zeniths.Helper;

namespace Zeniths.Utility
{
    /// <summary>
    /// 常规宏变量
    /// </summary>
    public class SystemMacro
    {
        /// <summary>
        /// 执行文件名称
        /// </summary>
        [MacroVariable("执行文件名称")]
        public string GuidString
        {
            get { return StringHelper.GetGuid(); }
        }

        /// <summary>
        /// 执行文件名称
        /// </summary>
        [MacroVariable("执行文件名称")]
        public string ExeFileName
        {
            get { return Application.ExecutablePath.Substring(Application.ExecutablePath.LastIndexOf('\\') + 1); }
        }

        /// <summary>
        /// 执行文件目录
        /// </summary>
        [MacroVariable("执行文件目录")]
        public string ExeFileDir { get { return Application.StartupPath + "\\"; } }

        /// <summary>
        /// 执行文件路径
        /// </summary>
        [MacroVariable("执行文件路径")]
        public string ExeFilePath
        {
            get { return Application.ExecutablePath; }
        }

        /// <summary>
        /// 当前年
        /// </summary>
        [MacroVariable("当前年")]
        public int CurrentYear
        {
            get { return DateTime.Now.Year; }
        }

        /// <summary>
        /// 当前月
        /// </summary>
        [MacroVariable("当前月")]
        public int CurrentMonth
        {
            get { return DateTime.Now.Month; }
        }

        /// <summary>
        /// 当前日
        /// </summary>
        [MacroVariable("当前日")]
        public int CurrentDay
        {
            get { return DateTime.Now.Day; }
        }

        /// <summary>
        /// 当前小时
        /// </summary>
        [MacroVariable("当前小时")]
        public int CurrentHour
        {
            get { return DateTime.Now.Hour; }
        }

        /// <summary>
        /// 当前分钟
        /// </summary>
        [MacroVariable("当前分钟")]
        public int CurrentMinute
        {
            get { return DateTime.Now.Minute; }
        }

        /// <summary>
        /// 当前秒
        /// </summary>
        [MacroVariable("当前秒")]
        public int CurrentSecond
        {
            get { return DateTime.Now.Second; }
        }

        /// <summary>
        /// 当前日期
        /// </summary>
        [MacroVariable("当前日期")]
        public string CurrentDate { get { return DateTimeHelper.FormatDate(DateTime.Now); } }

        /// <summary>
        /// 当前日期时间
        /// </summary>
        [MacroVariable("当前日期时间")]
        public string CurrentDateTime { get { return DateTimeHelper.FormatDateHasSecond(DateTime.Now); } }

        /// <summary>
        /// 当前时间
        /// </summary>
        [MacroVariable("当前时间")]
        public string CurrentTime { get { return DateTimeHelper.FormatTime(DateTime.Now); } }

        /// <summary>
        /// 当前时分
        /// </summary>
        [MacroVariable("当前时分")]
        public string CurrentHourMinute { get { return DateTimeHelper.FormatDate(DateTime.Now, "HH:mm"); } }

        /// <summary>
        /// 系统桌面目录
        /// </summary>
        [MacroVariable("系统桌面目录")]
        public string DesktopDir { get { return Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\"; } }
        
    }
}