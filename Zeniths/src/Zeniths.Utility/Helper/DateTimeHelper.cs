// ===============================================================================
// Copyright (c) 2015 �����ż��Źɷ����޹�˾
// ===============================================================================
using System;
using System.Globalization;
using System.Text;

namespace Zeniths.Helper
{
    /// <summary>
    /// ���ڲ�����
    /// </summary>
    /// <example>
    /// <code>
    /// using XCI.Helper;
    /// 
    /// DateTime date1 = new DateTime(2012, 11, 6);
    /// DateTime date2 = new DateTime(2012, 2, 4);
    /// DateTime dateNow = new DateTime(2012, 11, 6, 16, 31, 20, 456);
    /// 
    /// Console.WriteLine("date1 = {0}", date1);//date1 = 2012/11/6 0:00:00
    /// Console.WriteLine("date2 = {0}", date2);//date2 = 2012/2/4 0:00:00
    /// Console.WriteLine("dateNow = {0}", dateNow);//dateNow = 2012/11/6 16:31:20
    /// Console.WriteLine();
    /// 
    /// Console.WriteLine(DateTimeHelper.FormatDate(date1)); //2012-11-06 //��ʽ������(��ʽ yyyy-MM-dd)  
    /// Console.WriteLine(DateTimeHelper.FormatDate(date1, "yyyy��MM��dd��")); //2012��11��06�� //��ʽ������  
    /// Console.WriteLine(DateTimeHelper.FormatDateHasMilliSecond(dateNow)); //2012-11-06 16:31:20.456 //��ʽ������(��ʽ yyyy-MM-dd HH:mm:ss.FFF)  
    /// Console.WriteLine(DateTimeHelper.FormatDateHasSecond(dateNow)); //2012-11-06 16:31:20 //��ȡ��ʽ���������ַ���(�� yyyy-MM-dd HH:mm:ss)  
    /// Console.WriteLine(DateTimeHelper.GetMonthLastDay(date1)); //30 //��ȡ�·����һ��  
    /// Console.WriteLine(DateTimeHelper.GetMonthLastDay(date2)); //29 //��ȡ�·����һ��  
    /// Console.WriteLine(DateTimeHelper.GetTimeString(new TimeSpan(1, 2, 3, 4)));//1��2Сʱ3���� //��ȡʱ����ַ�������(xx��xxСʱxx����)  
    /// Console.WriteLine(DateTimeHelper.GetDateDiffString(new DateTime(2012, 11, 6, 16, 17, 5), dateNow)); //14����ǰ //��ȡʱ�����ַ�������(xxСʱǰ����xx����ǰ)  
    /// Console.WriteLine(DateTimeHelper.GetWeekDay());//���ڶ� //��ȡ��ǰ��������  
    /// Console.WriteLine(DateTimeHelper.GetWeekDay(date2)); //������ //��ȡ���ڶ�Ӧ��������  
    /// Console.WriteLine(DateTimeHelper.IsLeapYear(date1.Year)); //True //�ж�ָ������Ƿ������� 
    /// Console.WriteLine(DateTimeHelper.IsLeapYear(2009)); //False //�ж�ָ������Ƿ������� 
    /// Console.WriteLine(DateTimeHelper.IsWeekend(date1)); //False //�ж�ָ�������Ƿ�����ĩ(������������)  
    /// Console.WriteLine(DateTimeHelper.IsWeekend(date2)); //True //�ж�ָ�������Ƿ�����ĩ(������������)
    /// </code>
    /// </example>
    public static class DateTimeHelper
    {
        /// <summary>
        /// �ж�ָ������Ƿ�������
        /// </summary>
        /// <param name="year">���</param>
        /// <returns>���ָ�����������귵��true</returns>
        public static bool IsLeapYear(int year)
        {
            return year % 4 == 0 && (year % 100 != 0 || year % 400 == 0);
        }

        /// <summary>
        /// �ж�ָ�������Ƿ�����ĩ(������������)
        /// </summary>
        /// <param name="date">����</param>
        /// <returns>���ָ�������������������շ���true</returns>
        public static bool IsWeekEnd(DateTime date)
        {
            return date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;
        }

        /// <summary>
        /// ��ȡ��ǰ�µ�һ��
        /// </summary>
        public static DateTime GetMonthFirstDate()
        {
            return GetMonthFirstDate(DateTime.Now);
        }

        /// <summary>
        /// ��ȡָ�����������µĵ�һ��
        /// </summary>
        /// <param name="date">ָ������</param>
        public static DateTime GetMonthFirstDate(DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }

        /// <summary>
        /// ��ȡ��ǰ���һ��
        /// </summary>
        public static DateTime GetYearFirstDate()
        {
            return GetYearFirstDate(DateTime.Now);
        }

        /// <summary>
        /// ��ȡ��ǰ���һ��
        /// </summary>
        /// <param name="year">���</param>
        public static DateTime GetYearFirstDate(int year)
        {
            return new DateTime(year, 1, 1);
        }

        /// <summary>
        /// ��ȡָ������������ĵ�һ��
        /// </summary>
        public static DateTime GetYearFirstDate(DateTime date)
        {
            return new DateTime(date.Year, 1, 1);
        }

        /// <summary>
        /// ��ȡ��ǰ�����һ��
        /// </summary>
        public static DateTime GetYearLastDate()
        {
            return GetYearLastDate(DateTime.Now);
        }

        /// <summary>
        /// ��ȡָ��������һ��
        /// </summary>
        /// <param name="year">ָ�����</param>
        public static DateTime GetYearLastDate(int year)
        {
            return new DateTime(year, 12, GetYearLastDay(year));
        }

        /// <summary>
        /// ��ȡָ����������������һ��
        /// </summary>
        /// <param name="date">ָ������</param>
        public static DateTime GetYearLastDate(DateTime date)
        {
            return new DateTime(date.Year, 12, GetYearLastDay(date.Year));
        }

        /// <summary>
        /// ��ȡ�·����һ��
        /// </summary>
        /// <param name="year">���</param>
        /// <returns>�����·����һ��</returns>
        public static int GetYearLastDay(int year)
        {
            int day = new GregorianCalendar().GetDaysInMonth(year, 12);
            return day;
        }

        /// <summary>
        /// ��ȡ��ǰ�����һ��
        /// </summary>
        public static DateTime GetMonthLastDate()
        {
            return GetMonthLastDate(DateTime.Now);
        }

        /// <summary>
        /// ��ȡָ�����������µ����һ��
        /// </summary>
        public static DateTime GetMonthLastDate(DateTime date)
        {
            return new DateTime(date.Year, date.Month, GetMonthLastDay(date));
        }

        /// <summary>
        /// ��ȡ�·����һ��
        /// </summary>
        /// <param name="date">ָ������</param>
        /// <returns>�����·����һ��</returns>
        public static int GetMonthLastDay(DateTime date)
        {
            int year = date.Year;
            int month = date.Month;
            int day = new GregorianCalendar().GetDaysInMonth(year, month);//��ȡָ���µ�����
            DateTime lastDay = new DateTime(year, month, day);
            return lastDay.Day;
        }

        /// <summary>
        /// ��ȡʱ�������ַ�������(xx��xxСʱxx����)
        /// </summary>
        /// <param name="ts">ʱ����</param>
        /// <param name="hasMilliseconds">��������</param>
        /// <returns>����ʱ�������ַ�������(xx��xxСʱxx����)</returns>
        public static string GetTimeString(TimeSpan ts, bool hasMilliseconds = true)
        {
            StringBuilder sb = new StringBuilder();
            var newts = ts;
            if (newts.Days >= 1)
            {
                sb.AppendFormat("{0}��", ts.Days);
                newts -= new TimeSpan(ts.Days, 0, 0, 0);
            }
            if (newts.Hours >= 1)
            {
                sb.AppendFormat("{0}Сʱ", ts.Hours);
                newts -= new TimeSpan(ts.Hours, 0, 0);
            }
            if (newts.Minutes >= 1)
            {
                sb.AppendFormat("{0}����", ts.Minutes);
                newts -= new TimeSpan(0, ts.Minutes, 0);
            }
            if (newts.Seconds >= 1)
            {
                sb.AppendFormat("{0}��", ts.Seconds);
                newts -= new TimeSpan(0, 0, ts.Seconds);
            }
            if (hasMilliseconds && newts.Milliseconds >= 1)
            {
                sb.AppendFormat("{0}����", ts.Milliseconds);
            }
            return sb.ToString();
        }

        /// <summary>
        /// ��ȡʱ�����ַ�������(xxСʱǰ����xx����ǰ),�������һ������ʾxx��xx��
        /// </summary>
        /// <param name="dateTime">����1</param>
        /// <param name="nowDateTime">����2</param>
        /// <returns>xxСʱǰ,xx����ǰ</returns>
        /// <remarks>�������һ������ʾxx��xx��</remarks>
        public static string GetDateDiffString(DateTime dateTime, DateTime nowDateTime)
        {
            string dateDiff;
            TimeSpan ts = nowDateTime - dateTime;
            if (ts.Days >= 1)
            {
                dateDiff = dateTime.Month + "��" + dateTime.Day + "��";
            }
            else
            {
                if (ts.Hours > 1)
                {
                    dateDiff = ts.Hours + "Сʱǰ";
                }
                else
                {
                    dateDiff = ts.Minutes + "����ǰ";
                }
            }
            return dateDiff;
        }

        /// <summary>
        /// ��ȡ��ǰ�����ڼ�
        /// </summary>
        /// <returns>���ص�ǰ�����ڼ�</returns>
        public static string GetWeekDay()
        {
            return GetWeekDay(DateTime.Now);
        }

        /// <summary>
        /// ��ȡ���ڶ�Ӧ��������(����)
        /// </summary>
        /// <param name="date">����</param>
        /// <returns>�������ڶ�Ӧ��������(������,����һ...)</returns>
        public static string GetWeekDay(DateTime date)
        {
            string[] weekDay = { "������", "����һ", "���ڶ�", "������", "������", "������", "������" };
            return weekDay[(int)(date.DayOfWeek)];
        }

        /// <summary>
        /// ��ȡ���ڶ�Ӧ��������(Ӣ��)
        /// </summary>
        /// <param name="date">����</param>
        /// <returns>�������ڶ�Ӧ��������(������,����һ...)</returns>
        public static string GetWeekDayEnglish(DateTime date)
        {
            string[] weekDay = { "Sunday", "Monday", "Tuesday", "Wednesday", "Thurday", "Friday", "Saturday" };
            return weekDay[(int)(date.DayOfWeek)];
        }

        /// <summary>
        /// ��ȡ�·ݶ�Ӧ��Ӣ���·�
        /// </summary>
        /// <param name="date">����</param>
        public static string GetMonthEnglish(DateTime date)
        {
            string[] monthDay = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
            return monthDay[date.Month];
        }


        /// <summary>
        /// ��ʽ������
        /// </summary>
        /// <param name="datetime">����</param>
        /// <param name="format">��ʽ�ַ���</param>
        /// <returns>�������ڸ�ʽ������ַ���</returns>
        /// <remarks>��ʽ�ַ���:
        ///     <list type="table">
        ///         <item>
        ///             <term>d</term>
        ///             <description>���������ڱ�ʾΪ�� 1 �� 31 �����֡�һλ���ֵ���������Ϊ����ǰ����ĸ�ʽ���й�ʹ�õ�����ʽ˵�����ĸ�����Ϣ����μ�ʹ�õ����Զ����ʽ˵������</description>
        ///         </item>
        ///         <item>
        ///             <term>dd</term>
        ///             <description>���������ڱ�ʾΪ�� 01 �� 31 �����֡�һλ���ֵ���������Ϊ��ǰ����ĸ�ʽ��</description>
        ///         </item>
        ///         <item>
        ///             <term>h</term>
        ///             <description>��Сʱ��ʾΪ�� 1 �� 12 �����֣���ͨ�� 12 Сʱ�Ʊ�ʾСʱ������ҹ�����翪ʼ����Сʱ��������ˣ���ҹ�󾭹���ĳ�ض�Сʱ��������������ͬСʱ���޷��������֡�Сʱ�����������룬һλ���ֵ�Сʱ������Ϊ����ǰ����ĸ�ʽ�����磬����ʱ��Ϊ 5:43����˸�ʽ˵������ʾ��5����</description>
        ///         </item>
        ///         <item>
        ///             <term>hh, hh��������������ġ�h��˵������</term>
        ///             <description>��Сʱ��ʾΪ�� 01 �� 12 �����֣���ͨ�� 12 Сʱ�Ʊ�ʾСʱ������ҹ�����翪ʼ����Сʱ��������ˣ���ҹ�󾭹���ĳ�ض�Сʱ��������������ͬСʱ���޷��������֡�Сʱ�����������룬һλ���ֵ�Сʱ������Ϊ��ǰ����ĸ�ʽ�����磬����ʱ��Ϊ 5:43����˸�ʽ˵������ʾ��05����</description>
        ///         </item>
        ///         <item>
        ///             <term>H</term>
        ///             <description>��Сʱ��ʾΪ�� 0 �� 23 �����֣���ͨ�����㿪ʼ�� 24 Сʱ�Ʊ�ʾСʱ������ҹ��ʼ��Сʱ������һλ���ֵ�Сʱ������Ϊ����ǰ����ĸ�ʽ��</description>
        ///         </item>
        ///         <item>
        ///             <term>HH, HH��������������ġ�H��˵������</term>
        ///             <description>��Сʱ��ʾΪ�� 00 �� 23 �����֣���ͨ�����㿪ʼ�� 24 Сʱ�Ʊ�ʾСʱ������ҹ��ʼ��Сʱ������һλ���ֵ�Сʱ������Ϊ��ǰ����ĸ�ʽ��</description>
        ///         </item>
        ///         <item>
        ///             <term>K</term>
        ///             <description>��ʾ DateTime.Kind ���ԵĲ�ֵͬ������Local������Utc����Unspecified������˵�������ı���ʽѭ������ Kind ֵ������ʱ������� Kind ֵΪ��Local�������˵������Ч�ڡ�zzz��˵������������ʾ����ʱ��ƫ���������硰-07:00�������ڡ�Utc������ֵ����˵������ʾ�ַ���Z���Ա�ʾ UTC ���ڡ����ڡ�Unspecified������ֵ����˵������Ч�ڡ��������κ����ݣ���</description>
        ///         </item>
        ///         <item>
        ///             <term>m</term>
        ///             <description>�����ӱ�ʾΪ�� 0 �� 59 �����֡����ӱ�ʾ��ǰһСʱ�󾭹�������������һλ���ֵķ���������Ϊ����ǰ����ĸ�ʽ��</description>
        ///         </item>
        ///         <item>
        ///             <term>mm, mm��������������ġ�m��˵������</term>
        ///             <description>�����ӱ�ʾΪ�� 00 �� 59 �����֡����ӱ�ʾ��ǰһСʱ�󾭹�������������һλ���ֵķ���������Ϊ��ǰ����ĸ�ʽ��</description>
        ///         </item>
        ///         <item>
        ///             <term>M</term>
        ///             <description>���·ݱ�ʾΪ�� 1 �� 12 �����֡�һλ���ֵ��·�����Ϊ����ǰ����ĸ�ʽ��</description>
        ///         </item>
        ///         <item>
        ///             <term>MM</term>
        ///             <description>���·ݱ�ʾΪ�� 01 �� 12 �����֡�һλ���ֵ��·�����Ϊ��ǰ����ĸ�ʽ��</description>
        ///         </item>
        ///         <item>
        ///             <term>s</term>
        ///             <description>�����ʾΪ�� 0 �� 59 �����֡����ʾ��ǰһ���Ӻ󾭹�����������һλ���ֵ���������Ϊ����ǰ����ĸ�ʽ��</description>
        ///         </item>
        ///         <item>
        ///             <term>ss, ss��������������ġ�s��˵������</term>
        ///             <description>�����ʾΪ�� 00 �� 59 �����֡����ʾ��ǰһ���Ӻ󾭹�����������һλ���ֵ���������Ϊ��ǰ����ĸ�ʽ��</description>
        ///         </item>
        ///         <item>
        ///             <term>t</term>
        ///             <description>
        ///                 ��ʾ��ǰ <see cref="System.Globalization.DateTimeFormatInfo.AMDesignator" /> ��
        ///                 <see
        ///                     cref="System.Globalization.DateTimeFormatInfo.PMDesignator" />
        ///                 �����ж���� A.M./P.M. ָʾ���ĵ�һ���ַ���������ڸ�ʽ����ʱ���е�Сʱ��С�� 12����ʹ�� A.M. ָʾ��������ʹ�� P.M. ָʾ����
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <term>tt, tt��������������ġ�t��˵������</term>
        ///             <description>�� A.M./P.M. ָʾ����ʾΪ��ǰ System.Globalization.DateTimeFormatInfo.AMDesignator �� System.Globalization.DateTimeFormatInfo.PMDesignator �����ж�������ݡ�������ڸ�ʽ����ʱ���е�Сʱ��С�� 12����ʹ�� A.M. ָʾ��������ʹ�� P.M. ָʾ����</description>
        ///         </item>
        ///         <item>
        ///             <term>y</term>
        ///             <description>����ݱ�ʾΪ�����λ���֡������ݶ�����λ���������н���ʾ��λ��λ����������������λ���������������Ϊ����ǰ����ĸ�ʽ��</description>
        ///         </item>
        ///         <item>
        ///             <term>yy</term>
        ///             <description>����ݱ�ʾΪ��λ���֡������ݶ�����λ���������н���ʾ��λ��λ����������������λ��������ǰ������������ʹ֮�ﵽ��λ����</description>
        ///         </item>
        ///         <item>
        ///             <term>yyy</term>
        ///             <description>
        ///                 ����ݱ�ʾΪ��λ���֡������ݶ�����λ���������н���ʾ��λ��λ����������������λ��������ǰ������������ʹ֮�ﵽ��λ����
        ///                 <para>��ע�⣬������ݿ���Ϊ��λ����̩���������˸�ʽ˵��������ʾȫ����λ����</para>
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <term>yyyy</term>
        ///             <description>
        ///                 ����ݱ�ʾΪ��λ���֡������ݶ�����λ���������н���ʾ��λ��λ����������������λ��������ǰ������������ʹ֮�ﵽ��λ����
        ///                 <para>��ע�⣬������ݿ���Ϊ��λ����̩���������˸�ʽ˵����������ȫ����λ����</para>
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <term>yyyyy��������������ġ�y��˵������</term>
        ///             <description>
        ///                 ����ݱ�ʾΪ��λ���֡������ݶ�����λ���������н���ʾ��λ��λ����������������λ��������ǰ������������ʹ֮�ﵽ��λ����
        ///                 <para>������ڶ���ġ�y��˵�������������������ǰ������������ʹ֮�ﵽ��y��˵��������Ŀ��</para>
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <term>z</term>
        ///             <description>
        ///                 ��ʾϵͳʱ����������ʱ�� (GMT) ��СʱΪ��λ�����Ĵ�����ʱ��ƫ���������磬λ��̫ƽ���׼ʱ���еļ������ƫ����Ϊ��-8����
        ///                 <para>ƫ����ʼ����ʾΪ����ǰ�����š��Ӻ� (+) ָʾСʱ������ GMT������ (-) ָʾСʱ������ GMT��ƫ������ΧΪ �C12 �� +13��һλ���ֵ�ƫ��������Ϊ����ǰ����ĸ�ʽ��ƫ��������ʱ��Ӱ�졣</para>
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <term>zz</term>
        ///             <description>
        ///                 ��ʾϵͳʱ����������ʱ�� (GMT) ��СʱΪ��λ�����Ĵ�����ʱ��ƫ���������磬λ��̫ƽ���׼ʱ���еļ������ƫ����Ϊ��-08����
        ///                 <para>ƫ����ʼ����ʾΪ����ǰ�����š��Ӻ� (+) ָʾСʱ������ GMT������ (-) ָʾСʱ������ GMT��ƫ������ΧΪ �C12 �� +13��һλ���ֵ�ƫ��������Ϊ��ǰ����ĸ�ʽ��ƫ��������ʱ��Ӱ�졣</para>
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <term>zzz, zzz��������������ġ�z��˵������</term>
        ///             <description>
        ///                 ��ʾϵͳʱ����������ʱ�� (GMT) ��Сʱ�ͷ���Ϊ��λ�����Ĵ�����ʱ��ƫ���������磬λ��̫ƽ���׼ʱ���еļ������ƫ����Ϊ��-08:00����
        ///                 <para>ƫ����ʼ����ʾΪ����ǰ�����š��Ӻ� (+) ָʾСʱ������ GMT������ (-) ָʾСʱ������ GMT��ƫ������ΧΪ �C12 �� +13��һλ���ֵ�ƫ��������Ϊ��ǰ����ĸ�ʽ��ƫ��������ʱ��Ӱ�졣</para>
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <term>�κ������ַ�</term>
        ///             <description>���������ַ������Ƶ�����ַ����У����Ҳ�Ӱ���ʽ����</description>
        ///         </item>
        ///         <item>
        ///             <term>f</term>
        ///             <description>
        ///                 ��ʾ�벿�ֵ������Чλ��
        ///                 <para>��ע�⣬�����f����ʽ˵��������ʹ�ã�û��������ʽ˵���������˵�����������ǡ�f����׼ DateTime ��ʽ˵��������������/ʱ��ģʽ�����й�ʹ�õ�����ʽ˵�����ĸ�����Ϣ����μ�ʹ�õ����Զ����ʽ˵���������˸�ʽ˵������ ParseExact �� TryParseExact ����һ��ʹ��ʱ�����á�f����ʽ˵��������ĿָʾҪ�������벿�ֵ������Чλλ����</para>
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <term>ff</term>
        ///             <description>��ʾ�벿�ֵ����������Чλ��</description>
        ///         </item>
        ///         <item>
        ///             <term>fff</term>
        ///             <description>��ʾ�벿�ֵ����������Чλ��</description>
        ///         </item>
        ///         <item>
        ///             <term>ffff</term>
        ///             <description>��ʾ�벿�ֵ��ĸ������Чλ��</description>
        ///         </item>
        ///         <item>
        ///             <term>fffff</term>
        ///             <description>��ʾ�벿�ֵ���������Чλ��</description>
        ///         </item>
        ///         <item>
        ///             <term>ffffff</term>
        ///             <description>��ʾ�벿�ֵ����������Чλ��</description>
        ///         </item>
        ///         <item>
        ///             <term>fffffff</term>
        ///             <description>��ʾ�벿�ֵ��߸������Чλ��</description>
        ///         </item>
        ///         <item>
        ///             <term>F</term>
        ///             <description>
        ///                 ��ʾ�벿�ֵ������Чλ�������λΪ�㣬����ʾ�κ���Ϣ���й�ʹ�õ�����ʽ˵�����ĸ�����Ϣ����μ�ʹ�õ����Զ����ʽ˵������
        ///                 <para>���˸�ʽ˵������ ParseExact �� TryParseExact ����һ��ʹ��ʱ�����á�F����ʽ˵��������ĿָʾҪ�������벿�ֵ������Чλ���λ����</para>
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <term>FF</term>
        ///             <description>��ʾ�벿�ֵ����������Чλ��������ʾβ���㣨��������λ����</description>
        ///         </item>
        ///         <item>
        ///             <term>FFF</term>
        ///             <description>��ʾ�벿�ֵ����������Чλ��������ʾβ���㣨��������λ����</description>
        ///         </item>
        ///         <item>
        ///             <term>FFFF</term>
        ///             <description>��ʾ�벿�ֵ��ĸ������Чλ��������ʾβ���㣨���ĸ���λ����</description>
        ///         </item>
        ///         <item>
        ///             <term>FFFFF</term>
        ///             <description>��ʾ�벿�ֵ���������Чλ��������ʾβ���㣨�������λ����</description>
        ///         </item>
        ///         <item>
        ///             <term>FFFFFF</term>
        ///             <description>��ʾ�벿�ֵ����������Чλ��������ʾβ���㣨��������λ����</description>
        ///         </item>
        ///         <item>
        ///             <term>FFFFFFF</term>
        ///             <description>��ʾ�벿�ֵ��߸������Чλ��������ʾβ���㣨���߸���λ����</description>
        ///         </item>
        ///     </list>
        /// </remarks>
        public static string FormatDate(DateTime datetime, string format)
        {
            if (datetime == DateTime.MinValue || datetime == DateTime.MaxValue)
            {
                return string.Empty;
            }
            return datetime.ToString(format);
        }

        /// <summary>
        /// ��ʽ������(Ĭ�ϸ�ʽyyyy-MM-dd)
        /// </summary>
        /// <param name="datetime">����</param>
        /// <returns>�������ڸ�ʽ������ַ���</returns>
        public static string FormatDate(DateTime datetime)
        {
            return datetime.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// ��ʽ������(Ĭ�ϸ�ʽ yyyy-MM-dd HH:mm:ss.FFF)
        /// </summary>
        /// <param name="datetime">����</param>
        /// <returns>�������ڸ�ʽ������ַ���</returns>
        public static string FormatDateHasMilliSecond(DateTime datetime)
        {
            return FormatDate(datetime, "yyyy-MM-dd HH:mm:ss.FFF");
        }

        /// <summary>
        /// ��ȡ��ʽ���������ַ���(Ĭ�ϸ�ʽ yyyy-MM-dd HH:mm:ss)
        /// </summary>
        /// <param name="datetime">ָ��������</param>
        /// <returns>�������ڸ�ʽ������ַ���</returns>
        public static string FormatDateHasSecond(DateTime datetime)
        {
            return FormatDate(datetime, "yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// ��ȡ��ʽ���������ַ���(Ĭ�ϸ�ʽ yyyy-MM-dd HH:mm:ss)
        /// </summary>
        /// <param name="datetime">ָ��������</param>
        /// <returns>�������ڸ�ʽ������ַ���</returns>
        public static string FormatDateHasSecond(DateTime? datetime)
        {
            if (datetime.HasValue)
            {
                return FormatDate(datetime.Value, "yyyy-MM-dd HH:mm:ss");
            }
            return string.Empty;
        }

        /// <summary>
        /// ��ȡ��ʽ���������ַ���(Ĭ�ϸ�ʽ yyyy-MM-dd)
        /// </summary>
        /// <param name="datetime">ָ��������</param>
        /// <returns>�������ڸ�ʽ������ַ���</returns>
        public static string FormatDateHasThird(DateTime? datetime)
        {
            if (datetime.HasValue)
            {
                return FormatDate(datetime.Value, "yyyy-MM-dd");
            }
            return string.Empty;
        }

        /// <summary>
        /// ��ȡ��ʽ���������ַ���(Ĭ�ϸ�ʽ HH:mm:ss)
        /// </summary>
        /// <param name="datetime">ָ��������</param>
        /// <returns>�������ڸ�ʽ������ַ���</returns>
        public static string FormatTime(DateTime datetime)
        {
            return FormatDate(datetime, "HH:mm:ss");
        }

    }
}