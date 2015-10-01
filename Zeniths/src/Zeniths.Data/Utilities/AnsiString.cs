// ===============================================================================
// Copyright (c) 2015 正得信集团股份有限公司
// ===============================================================================
namespace Zeniths.Data.Utilities
{
	/// <summary>
    /// 包装一个DBType.AnsiString类型的字符串
	/// </summary>
	public class AnsiString
	{
		/// <summary>
        /// 初始化AnsiString新实例。
		/// </summary>
		/// <param name="str">字符串</param>
		public AnsiString(string str)
		{
			Value = str;
		}

		/// <summary>
		/// 字符串值
		/// </summary>
		public string Value 
		{ 
			get; 
			private set; 
		}
	}

}
