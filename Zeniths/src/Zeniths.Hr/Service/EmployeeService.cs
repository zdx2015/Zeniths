// ===============================================================================
// 正得信股份 版权所有
// ===============================================================================

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Zeniths.Hr.Entity;
using Zeniths.Collections;
using Zeniths.Data;
using Zeniths.Extensions;
using Zeniths.Utility;
using Zeniths.Auth.Service;
using Zeniths.Auth.Entity;

namespace Zeniths.Hr.Service
{
    /// <summary>
    /// 员工表服务
    /// </summary>
    public class EmployeeService
    {
        /// <summary>
        /// 员工表存储器
        /// </summary>
        private readonly Repository<Employee> repos = new Repository<Employee>();

        /// <summary>
        /// 检测是否存在指定员工表
        /// </summary>
        /// <param name="entity">员工表实体</param>
        /// <returns>如果存在指定记录返回BoolMessage.False</returns>
        public BoolMessage Exists(Employee entity)
        {
            return BoolMessage.True;
            //var has = repos.Exists(p => p.Name == entity.Name && p.Id != entity.Id);
            //return has ? new BoolMessage(false, "输入名称已经存在") : BoolMessage.True;
        }

        /// <summary>
        /// 新增员工表
        /// </summary>
        /// <param name="entity">员工表实体</param>
        /// <returns>执行成功返回BoolMessage.True</returns>
        public BoolMessage Insert(Employee entity)
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
        /// 更新员工表
        /// </summary>
        /// <param name="entity">员工表实体</param>
        /// <returns>执行成功返回BoolMessage.True</returns>
        public BoolMessage Update(Employee entity)
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
        /// 删除员工表
        /// </summary>
        /// <param name="ids">员工表主键数组</param>
        /// <returns>执行成功返回BoolMessage.True</returns>
        public BoolMessage Delete(int[] ids)
        {
            try
            {
                if (ids.Length==1)
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
        /// 获取员工表对象
        /// </summary>
        /// <param name="id">员工表主键</param>
        /// <returns>返回员工表对象</returns>
        public Employee Get(int id)
        {
            return repos.Get(id);
        }

        /// <summary>
        /// 根据员工Id获取系统用户对象
        /// </summary>
        /// <param name="account">员工账号</param>
        /// <returns>返回员工对象</returns>
        public SystemUser GetSystemUser(int employeeId)
        {
            Employee empEntity = repos.Get(employeeId, p => p.UserId);
            SystemUserService userService = new SystemUserService();
            return userService.Get(empEntity.UserId);
        }

        /// <summary>
        /// 根据用户Id获取员工对象
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public Employee GetEmployeeByUser(int userId)
        {
            return repos.Get(p => p.UserId==userId);
        }

        /// <summary>
        /// 获取员工表列表
        /// </summary>
        /// <returns>返回员工表列表</returns>
        public List<Employee> GetList()
        {
            var query = repos.NewQuery.OrderBy(p => p.Id);
            return repos.Query(query).ToList();
        }
        
        /*
        /// <summary>
        /// 获取启用的员工表列表
        /// </summary>
        /// <returns>返回启用的员工表列表</returns>
        public List<Employee> GetEnabledList()
        {
            var query = repos.NewQuery.Where(p => p.IsEnabled == true).OrderBy(p => p.Id);
            return repos.Query(query).ToList();
        }
        
        /// <summary>
        /// 获取员工表DataTable
        /// </summary>
        /// <returns>返回员工表DataTable</returns>
        public DataTable GetTable()
        {
            var query = repos.NewQuery.OrderBy(p => p.Id);
            return repos.GetTable(query);
        }
        */
        
        /// <summary>
        /// 获取员工表分页列表
        /// </summary>
        /// <param name="pageIndex">页面索引</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="orderName">排序列名</param>
        /// <param name="orderDir">排序方式</param>
        /// <param name="name">查询关键字</param>
        /// <returns>返回员工表分页列表</returns>
        public PageList<Employee> GetPageList(int pageIndex, int pageSize, string orderName,string orderDir, string name)
        {
            orderName = orderName.IsEmpty() ? nameof(Employee.Id) : orderName;//默认使用主键排序
            orderDir = orderDir.IsEmpty() ? nameof(OrderDir.Desc) : orderDir;//默认使用倒序排序
            var query = repos.NewQuery.Take(pageSize).Page(pageIndex).OrderBy(orderName, orderDir.IsAsc());
            /*
            if (name.IsNotEmpty())
            {
                name = name.Trim();
                query.Where(p => p.Name.Contains(name));
            }
            */
            return repos.Page(query);
        }

        #region 私有方法


        #endregion
    }
}