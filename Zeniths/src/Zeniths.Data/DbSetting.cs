// ===============================================================================
// Copyright (c) 2015 正得信集团股份有限公司
// ===============================================================================
namespace Zeniths.Data
{
    /// <summary>
    /// 数据库配置
    /// </summary>
    public class DbSetting
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 实现类
        /// </summary>
        public string Provider { get; set; }

        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// 加密连接串
        /// </summary>
        public bool Encrypted { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 复制
        /// </summary>
        /// <returns>返回新的实例</returns>
        public DbSetting Clone()
        {
            return (DbSetting)this.MemberwiseClone();
        }
    }
}