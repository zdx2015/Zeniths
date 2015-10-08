// ===============================================================================
// Copyright (c) 2015 正得信集团股份有限公司
// ===============================================================================
namespace Zeniths.Utility
{
    /// <summary>
    /// 单例对象管理
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    public static class Singleton<T> where T : new()
    {
        private readonly static T _instance = new T();

        /// <summary>
        /// 单例对象
        /// </summary>
        public static T Instance
        {
            get { return _instance; }
        }
    }
}
