// ===============================================================================
// 正得信股份 版权所有
// ===============================================================================

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using Zeniths.Auth.Entity;
using Zeniths.Collections;
using Zeniths.Data;
using Zeniths.Extensions;
using Zeniths.Helper;
using Zeniths.Utility;

namespace Zeniths.Auth.Service
{
    /// <summary>
    /// 系统序列服务
    /// </summary>
    public class SystemSequenceService
    {
        /// <summary>
        /// 系统序列存储器
        /// </summary>
        private readonly AuthRepository<SystemSequence> repos = new AuthRepository<SystemSequence>();
        private static readonly object lockObject = new object();

        /// <summary>
        /// 获取序列值,完成后更新序列值
        /// </summary>
        /// <param name="name">序列名称</param>
        public string GetNextValue(string name)
        {
            Monitor.Enter(lockObject);
            try
            {
                var entity = Get(name);
                if (entity == null)
                {
                    entity = new SystemSequence();
                    entity.Name = name;
                    entity.Value = 2;
                    entity.Step = 1;
                    entity.Length = 0;
                    repos.Insert(entity);
                    entity.Value = 1;
                }
                else //更新
                {
                    var sql = "update SystemSequence set value=value+Step where Name=@Name";
                    repos.Database.Execute(sql, new object[] { name });
                }
                return FormatValue(entity);
            }
            catch
            {
                return string.Empty;
            }
            finally
            {
                Monitor.Exit(lockObject);
            }
        }


        /// <summary>
        /// 获取当前序列值,完成后不更新序列值
        /// </summary>
        /// <param name="name">序列名称</param>
        public string GetValue(string name)
        {
            var entity = Get(name);
            if (entity != null)
            {
                return FormatValue(entity);
            }
            return string.Empty;
        }

        /// <summary>
        /// 是否存在指定名称的序列
        /// </summary>
        /// <param name="name">序列名称</param>
        public bool Contain(string name)
        {
            return repos.Exists(p => p.Name == name);
        }

        /// <summary>
        /// 检测是否存在指定系统序列
        /// </summary>
        /// <param name="entity">系统序列实体</param>
        /// <returns>如果存在指定记录返回BoolMessage.False</returns>
        public BoolMessage Exists(SystemSequence entity)
        {
            var has = repos.Exists(p => p.Name == entity.Name && p.Id != entity.Id);
            return has ? new BoolMessage(false, "输入序列名称已经存在") : BoolMessage.True;
        }

        /// <summary>
        /// 新增系统序列
        /// </summary>
        /// <param name="entity">系统序列实体</param>
        /// <returns>执行成功返回BoolMessage.True</returns>
        public BoolMessage Insert(SystemSequence entity)
        {
            try
            {
                repos.Insert(entity);
                return BoolMessage.True;
            }
            catch (Exception e)
            {
                return new BoolMessage(false, e.Message);
            }
        }

        /// <summary>
        /// 更新系统序列
        /// </summary>
        /// <param name="entity">系统序列实体</param>
        /// <returns>执行成功返回BoolMessage.True</returns>
        public BoolMessage Update(SystemSequence entity)
        {
            try
            {
                repos.Update(entity);
                return BoolMessage.True;
            }
            catch (Exception e)
            {
                return new BoolMessage(false, e.Message);
            }
        }

        /// <summary>
        /// 获取系统序列对象
        /// </summary>
        /// <param name="id">序列主键</param>
        /// <returns>返回系统序列对象</returns>
        public SystemSequence Get(int id)
        {
            return repos.Get(id);
        }

        /// <summary>
        /// 获取系统序列对象
        /// </summary>
        /// <param name="name">序列名称</param>
        /// <returns>返回系统序列对象</returns>
        public SystemSequence Get(string name)
        {
            return repos.Get(p => p.Name == name);
        }

        /// <summary>
        /// 删除系统序列
        /// </summary>
        /// <param name="ids">系统序列主键数组</param>
        /// <returns>执行成功返回BoolMessage.True</returns>
        public BoolMessage Delete(int[] ids)
        {
            try
            {
                if (ids.Length == 1)
                {
                    repos.Delete(ids[0]);
                }
                else
                {
                    repos.Delete(ids);
                }
                return BoolMessage.True;
            }
            catch (Exception e)
            {
                return new BoolMessage(false, e.Message);
            }
        }


        /// <summary>
        /// 获取序列列表
        /// </summary>
        /// <returns>序列列表</returns>
        public List<SystemSequence> Query()
        {
            return repos.Query().ToList();
        }

        /// <summary>
        /// 获取系统序列分页列表
        /// </summary>
        /// <param name="pageIndex">页面索引</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="orderName">排序列名</param>
        /// <param name="orderDir">排序方式</param>
        /// <param name="name">查询关键字</param>
        /// <returns>返回系统序列分页列表</returns>
        public PageList<SystemSequence> GetPageList(int pageIndex, int pageSize,
            string orderName, string orderDir, string name)
        {
            orderName = orderName.IsEmpty() ? nameof(SystemSequence.Id) : orderName;//默认使用主键排序
            orderDir = orderDir.IsEmpty() ? nameof(OrderDir.Desc) : orderDir;//默认使用倒序排序
            var query = repos.NewQuery.Take(pageSize).Page(pageIndex).OrderBy(orderName, orderDir.IsAsc());

            if (name.IsNotEmpty())
            {
                name = name.Trim();
                query.Where(p => p.Name.Contains(name));
            }

            return repos.Page(query);
        }

        /// <summary>
        /// 格式化序列值
        /// </summary>
        /// <param name="sequence">序列对象</param>
        /// <returns></returns>
        private string FormatValue(SystemSequence sequence)
        {
            var placeholder = string.IsNullOrEmpty(sequence.Placeholder) ? "0" : sequence.Placeholder;
            string value = sequence.Value.ToString();
            if (sequence.Length > 0)
            {
                value = StringHelper.GetRepeatString(placeholder, sequence.Length - (value.Length)) + value;
            }
            var tpl = sequence.Template;
            if (!string.IsNullOrEmpty(tpl))
            {
                tpl = MacroHelper.Convert(tpl, new SystemMacro());
                return string.Format(tpl, value);
            }
            return value;
        }
    }
}