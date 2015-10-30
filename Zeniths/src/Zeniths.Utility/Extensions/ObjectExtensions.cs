// ===============================================================================
// Copyright (c) 2015 正得信集团股份有限公司
// ===============================================================================
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

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

        /// <summary>
        /// 四舍五入
        /// </summary>
        /// <param name="dec"></param>
        /// <param name="decimals">小数点位数,默认2</param>
        /// <returns></returns>
        public static decimal Round(this decimal dec, int decimals = 2)
        {
            return Math.Round(dec, decimals, MidpointRounding.AwayFromZero);
        }

        public static bool IsDecimal(this string str)
        {
            decimal test;
            return decimal.TryParse(str, out test);
        }
       
        public static bool IsDouble(this string str)
        {
            double test;
            return double.TryParse(str, out test);
        }
       
        public static bool IsInt(this string str)
        {
            int test;
            return int.TryParse(str, out test);
        }
        
        /// <summary>
        /// 将数组转换为符号分隔的字符串
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="split">分隔符</param>
        /// <returns></returns>
        public static string Join<T>(this T[] arr, string split = ",")
        {
            StringBuilder sb = new StringBuilder(arr.Length * 36);
            for (int i = 0; i < arr.Length; i++)
            {
                sb.Append(arr[i].ToString());
                if (i < arr.Length - 1)
                {
                    sb.Append(split);
                }
            }
            return sb.ToString();
        }
        
        public static bool IsLong(this string str)
        {
            long test;
            return long.TryParse(str, out test);
        }
        
        public static bool IsDateTime(this string str)
        {
            DateTime test;
            return DateTime.TryParse(str, out test);
        }
        public static bool IsDateTime(this string str, out DateTime test)
        {
            return DateTime.TryParse(str, out test);
        }

        public static bool IsGuid(this string str)
        {
            Guid test;
            return Guid.TryParse(str, out test);
        }
        
        /// <summary>
        /// 判断是否为Guid.Empty
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public static bool IsEmptyGuid(this Guid guid)
        {
            return guid == Guid.Empty;
        }

        public static bool IsUrl(this string str)
        {
            if (str.IsNullOrEmpty())
                return false;
            string pattern = @"^(http|https|ftp|rtsp|mms):(\/\/|\\\\)[A-Za-z0-9%\-_@]+\.[A-Za-z0-9%\-_@]+[A-Za-z0-9\.\/=\?%\-&_~`@:\+!;]*$";
            return Regex.IsMatch(str, pattern, RegexOptions.IgnoreCase);
        }

        public static bool IsEmail(this string str)
        {
            return Regex.IsMatch(str, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }

        /// <summary>
        /// 判断一个整型是否包含在指定的值内
        /// </summary>
        /// <param name="i"></param>
        /// <param name="ints"></param>
        /// <returns></returns>
        public static bool InArray(this int i, params int[] ints)
        {
            foreach (int k in ints)
            {
                if (i == k)
                {
                    return true;
                }
            }
            return false;
        }
        
       
        public static Guid ToGuid(this string str)
        {
            Guid test;
            if (Guid.TryParse(str, out test))
            {
                return test;
            }
            else
            {
                return Guid.Empty;
            }
        }
        

        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }
        
        /// <summary>
        /// 获取左边多少个字符
        /// </summary>
        /// <param name="str"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string Left(this string str, int len)
        {
            if (str == null || len < 1) { return ""; }
            if (len < str.Length)
            { return str.Substring(0, len); }
            else
            { return str; }
        }

        /// <summary>
        /// 获取右边多少个字符
        /// </summary>
        /// <param name="str"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string Right(this string str, int len)
        {
            if (str == null || len < 1) { return ""; }
            if (len < str.Length)
            { return str.Substring(str.Length - len); }
            else
            { return str; }
        }

        /// <summary>
        /// 得到实符串实际长度
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int GetTextSize(this string str)
        {
            byte[] strArray = System.Text.Encoding.Default.GetBytes(str);
            int res = strArray.Length;
            return res;
        }
       

        /// <summary>
        /// HTML编码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string HtmlEncode(this string str)
        {
            return HttpContext.Current.Server.HtmlEncode(str);
        }

        /// <summary>
        /// URL编码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string UrlEncode(this string str)
        {
            return str.IsNullOrEmpty() ? string.Empty : HttpContext.Current.Server.UrlEncode(str);
        }

        /// <summary>
        /// URL解码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string UrlDecode(this string str)
        {
            return str.IsNullOrEmpty() ? string.Empty : HttpContext.Current.Server.UrlDecode(str);
        }
        
        /// <summary>
        /// 将int类型转为GUID格式
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static Guid ToGuid(this int i)
        {
            return i.ToString("00000000-0000-0000-0000-000000000000").ToGuid();
        }
    }
}