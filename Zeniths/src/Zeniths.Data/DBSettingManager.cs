// ===============================================================================
// Copyright (c) 2015 正得信集团股份有限公司
// ===============================================================================
using System.Collections.Generic;
using System.Linq;
using Zeniths.Configuration;
using Zeniths.Data;
using Zeniths.Extensions;
using Zeniths.Helper;
using Zeniths.Utility;

namespace Zeniths.Data
{
    /// <summary>
    /// 数据库配置管理
    /// </summary>
    public static class DbSettingManager
    {
        private static List<DbSetting> Settings;
        private const string SectionName = "Database";

        /// <summary>
        /// 静态构造,读取数据库配置文件
        /// </summary>
        static DbSettingManager()
        {
            LoadSetting();
        }

        /// <summary>
        /// 是否存在指定名称的配置
        /// </summary>
        /// <param name="name">配置名称</param>
        /// <returns>如果存在指定的名称,返回true</returns>
        public static bool ExistsSetting(string name)
        {
            return Settings.Contains(p => p.Name.Equals(name));
        }

        /// <summary>
        /// 添加数据库配置
        /// </summary>
        /// <param name="name">配置名称</param>
        /// <param name="providerName">实现程序</param>
        /// <param name="connectionString">连接字符串</param>
        /// <returns>添加成功返回true</returns>
        public static BoolMessage AddSetting(string name, string providerName, string connectionString)
        {
            var setting = new DbSetting
            {
                Name = name,
                Provider = providerName,
                ConnectionString = connectionString
            };
            return AddSetting(setting);
        }

        /// <summary>
        /// 添加数据库配置
        /// </summary>
        /// <param name="setting">配置对象</param>
        /// <returns>添加成功返回true</returns>
        public static BoolMessage AddSetting(DbSetting setting)
        {
            if (ExistsSetting(setting.Name))
            {
                return new BoolMessage(false, "指定的配置名称已经存在");
            }
            Settings.Add(setting);
            return BoolMessage.True;
        }

        /// <summary>
        /// 更新数据库配置
        /// </summary>
        /// <param name="oldName">原配置名称</param>
        /// <param name="setting">配置对象</param>
        /// <returns>添加成功返回true</returns>
        public static BoolMessage UpdateSetting(string oldName, DbSetting setting)
        {
            var index = Settings.IndexOf(p => p.Name.Equals(oldName));
            if (index < 0)
            {
                return new BoolMessage(false, "无效的配置对象");
            }
            Settings[index] = setting;
            return BoolMessage.True;
        }

        /// <summary>
        /// 删除数据库配置
        /// </summary>
        /// <param name="name">配置名称</param>
        /// <returns>删除成功返回true</returns>
        public static bool DeleteSetting(string name)
        {
            return Settings.Remove(p => p.Name.Equals(name)) > 0;
        }

        /// <summary>
        /// 获取数据库配置
        /// </summary>
        /// <param name="name">配置名称</param>
        /// <returns>如果没有找到指定名称的配置对象,返回null</returns>
        public static DbSetting GetSetting(string name)
        {
            return Settings.FirstOrDefault(p => p.Name.Equals(name));
        }

        /// <summary>
        /// 获取数据库配置
        /// </summary>
        /// <param name="index">配置序号</param>
        /// <returns>如果没有找到序号的配置对象,返回null</returns>
        public static DbSetting GetSetting(int index)
        {
            if (Settings.Count - 1 >= index)
            {
                return Settings[index];
            }
            return null;
        }

        /// <summary>
        /// 获取数据库配置列表
        /// </summary>
        public static List<DbSetting> GetSettingList()
        {
            return Settings;
        }

        /// <summary>
        /// 重新读取配置文件
        /// </summary>
        public static void LoadSetting()
        {
            Settings = ConfigManager.GetSetting(SectionName, () => new List<DbSetting>());
            DecryptConnectionString(Settings);
        }

        /// <summary>
        /// 保存配置到文件中
        /// </summary>
        public static void SaveSetting()
        {
            var list = new List<DbSetting>();
            Settings.ForEach(p => list.Add(p.Clone()));
            EncryptConnectionString(list);
            ConfigManager.SetSetting(SectionName, list);
            ConfigManager.Save();
        }

        /// <summary>
        /// 解密连接字符串
        /// </summary>
        /// <param name="list">配置列表</param>
        private static void DecryptConnectionString(IEnumerable<DbSetting> list)
        {
            foreach (var setting in list)
            {
                if (setting.Encrypted)
                {
                    setting.ConnectionString = StringHelper.DecryptString(setting.ConnectionString);
                }
            }
        }

        /// <summary>
        /// 加密连接字符串
        /// </summary>
        /// <param name="list">配置列表</param>
        private static void EncryptConnectionString(IEnumerable<DbSetting> list)
        {
            foreach (var setting in list)
            {
                if (setting.Encrypted)
                {
                    setting.ConnectionString = StringHelper.EncryptString(setting.ConnectionString);
                }
            }
        }
    }
}