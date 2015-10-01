// ===============================================================================
// Copyright (c) 2015 正得信集团股份有限公司
// ===============================================================================
using System;

namespace Zeniths.Extensions
{
    /// <summary>
    /// Object扩展方法
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// 测试对象不是空字符串
        /// </summary>
        /// <param name="strObject">字符串对象</param>
        /// <returns>如果对象不为空并且字符串长度大于0返回true,否则返回false</returns>
        public static bool IsEmpty(this object strObject)
        {
            return strObject == null || string.IsNullOrEmpty(strObject.ToString());
        }

        /// <summary>
        /// 测试对象不是空字符串
        /// </summary>
        /// <param name="strObject">字符串对象</param>
        /// <returns>如果对象不为空并且字符串长度大于0返回true,否则返回false</returns>
        public static bool IsNotEmpty(this object strObject)
        {
            return strObject != null && !string.IsNullOrEmpty(strObject.ToString().Trim());
        }

        /// <summary>
        /// 转为字符串(如果对象为空,返回空字符串)
        /// </summary>
        /// <param name="obj">测试对象</param>
        /// <returns>>如果对象为空,返回空字符串</returns>
        public static string ToStringOrEmpty(this object obj)
        {
            return obj == null ? string.Empty : obj.ToString();
        }

        /// <summary>
        /// 格式化字符串
        /// </summary>
        /// <param name="str">对象</param>
        /// <param name="args">替换参数</param>
        public static string FormatString(this string str, params object[] args)
        {
            return string.Format(str, args);
        }

        /// <summary>
        /// 转整数
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="defaultValue">转换失败时返回的默认值</param>
        /// <returns>如果转换失败返回0</returns>
        public static int ToInt(this object obj, int defaultValue = 0)
        {
            int result;
            if (obj == null) return defaultValue;
            return int.TryParse(obj.ToString(), out result) ? result : defaultValue;
        }

        /// <summary>
        /// 转64位整数
        /// </summary>
        /// <param name="obj">转换对象</param>
        /// <param name="defaultValue">转换失败时返回的默认值</param>
        /// <returns>如果转换失败返回0</returns>
        public static long ToLong(this object obj, long defaultValue = 0)
        {
            long result;
            if (obj == null) return defaultValue;
            return long.TryParse(obj.ToString(), out result) ? result : defaultValue;
        }

        /// <summary>
        /// 把布尔转整数
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>如果转换失败返回0</returns>
        public static int ToBoolToInt(this object obj)
        {
            if (obj == null) return 0;
            bool bresult;
            if (bool.TryParse(obj.ToString(), out bresult) && bresult)
            {
                return 1;
            }
            return 0;
        }

        /// <summary>
        /// 转十进制数
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="defaultValue">转换失败时返回的默认值</param>
        /// <returns>如果转换失败返回0</returns>
        public static decimal ToDecimal(this object obj, decimal defaultValue = 0)
        {
            decimal result;
            if (obj == null) return defaultValue;
            return decimal.TryParse(obj.ToString(), out result) ? result : defaultValue;
        }

        /// <summary>
        /// 转单精度浮点数
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="defaultValue">转换失败时返回的默认值</param>
        /// <returns>如果转换失败返回0</returns>
        public static float ToFloat(this object obj, float defaultValue = 0)
        {
            float result;
            if (obj == null) return defaultValue;
            return float.TryParse(obj.ToString(), out result) ? result : defaultValue;
        }

        /// <summary>
        /// 转双精度浮点数
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="defaultValue">转换失败时返回的默认值</param>
        /// <returns>如果转换失败返回0</returns>
        public static double ToDouble(this object obj, double defaultValue = 0)
        {
            double result;
            if (obj == null) return defaultValue;
            return double.TryParse(obj.ToString(), out result) ? result : defaultValue;
        }

        /// <summary>
        /// 转日期
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>如果转换失败返回当前时间</returns>
        public static DateTime ToDateTime(this object obj)
        {
            return ToDateTime(obj, DateTime.Now);
        }

        /// <summary>
        /// 转日期
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="defaultValue">转换失败时返回的默认值</param>
        /// <returns>如果转换失败返回当前时间</returns>
        public static DateTime ToDateTime(this object obj, DateTime defaultValue)
        {
            DateTime result;
            if (obj == null) return defaultValue;
            return DateTime.TryParse(obj.ToString(), out result) ? result : defaultValue;
        }

        /// <summary>
        /// 转时间
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>如果转换失败返回TimeSpan.Zero</returns>
        public static TimeSpan ToTime(this object obj)
        {
            return ToTime(obj, TimeSpan.Zero);
        }

        /// <summary>
        /// 转时间
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="defaultValue">转换失败时返回的默认值</param>
        /// <returns>如果转换失败返回TimeSpan.Zero</returns>
        public static TimeSpan ToTime(this object obj, TimeSpan defaultValue)
        {
            TimeSpan result;
            if (obj == null) return defaultValue;
            return TimeSpan.TryParse(obj.ToString(), out result) ? result : defaultValue;
        }

        /// <summary>
        /// 转布尔 (如果值是1,转换为true)
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="defaultValue">转换失败时返回的默认值</param>
        /// <returns>如果转换失败返回false</returns>
        public static bool ToBool(this object obj, bool defaultValue = false)
        {
            bool result;
            if (obj == null) return defaultValue;
            string str = obj.ToString().Trim().ToLower();
            if (str.Equals("1"))
            {
                return true;
            }
            return bool.TryParse(obj.ToString(), out result) ? result : defaultValue;
        }

        /// <summary>
        /// 把字符串数组转为整形数组
        /// </summary>
        /// <param name="array">字符串数组</param>
        /// <returns>返回整形数组</returns>
        public static int[] ToIntArray(this string[] array)
        {
            int[] ar = new int[array.Length];
            for (int i = 0; i < ar.Length; i++)
            {
                int _v;
                if (int.TryParse(array[i], out _v))
                {
                    ar[i] = _v;
                }
            }
            return ar;
        }

        /// <summary>
        /// 把字符串数组转为整形数组
        /// </summary>
        /// <param name="array">字符串数组</param>
        /// <returns>返回整形数组</returns>
        public static long[] ToLongArray(this string[] array)
        {
            long[] ar = new long[array.Length];
            for (int i = 0; i < ar.Length; i++)
            {
                long _v;
                if (long.TryParse(array[i], out _v))
                {
                    ar[i] = _v;
                }
            }
            return ar;
        }

        /// <summary>
        /// 替换回车换行符
        /// </summary>
        /// <param name="str">带替换的字符串</param>
        public static string ReplaceEnter(this string str)
        {
            return str.Replace("\r", "").Replace("\n", "");
        }

        /// <summary>
        /// 是否Asc方式排序(不区分大小写)
        /// </summary>
        /// <param name="orderDir">排序方式字符串</param>
        /// <returns>如果字符串为Asc,返回true</returns>
        public static bool IsAsc(this string orderDir)
        {
            return orderDir.ToLower().Equals("asc");
        }

        /// <summary>
        /// 是否Desc方式排序(不区分大小写)
        /// </summary>
        /// <param name="orderDir">排序方式字符串</param>
        /// <returns>是否Desc方式排序,返回true</returns>
        public static bool IsDesc(this string orderDir)
        {
            return orderDir.ToLower().Equals("desc");
        }

    }
}