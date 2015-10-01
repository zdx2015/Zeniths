﻿// ===============================================================================
// Copyright (c) 2015 正得信集团股份有限公司
// ===============================================================================
using System;

namespace Zeniths.Helper
{
    /// <summary>
    /// Timer操作类
    /// </summary>
    public static class TimerHelper
    {
        /// <summary>
        /// 延迟执行
        /// </summary>
        /// <param name="delayTime">延迟时间,毫秒</param>
        /// <param name="execute">执行函数</param>
        public static void DelayExecuteWin(int delayTime, Action execute)
        {
            var time = new System.Windows.Forms.Timer();
            time.Interval = delayTime;
            time.Tick += (s, e) =>
            {
                time.Enabled = false;
                if (execute != null)
                {
                    execute();
                }
                time.Dispose();
            };
            time.Enabled = true;
        }

        /// <summary>
        /// 延迟执行
        /// </summary>
        /// <param name="delayTime">延迟时间,毫秒</param>
        /// <param name="execute">执行函数</param>
        public static void DelayExecute(int delayTime, Action execute)
        {
            var time = new System.Timers.Timer();
            time.Interval = delayTime;
            time.Elapsed += (s, e) =>
            {
                time.Enabled = false;
                if (execute != null)
                {
                    execute();
                }
                time.Dispose();
            };
            time.Enabled = true;
        }
 
    }
}