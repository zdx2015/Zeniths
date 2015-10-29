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
using Zeniths.Data.Extensions;
using Zeniths.Extensions;
using Zeniths.Utility;

namespace Zeniths.Hr.Service
{
    /// <summary>
    /// 外出登记表服务
    /// </summary>
    public class EmployeeGoOutRegistrationService
    {
        /// <summary>
        /// 外出登记表存储器
        /// </summary>
        private readonly Repository<EmployeeGoOutRegistration> repos = new Repository<EmployeeGoOutRegistration>();

        /// <summary>
        /// 新增外出登记
        /// </summary>
        /// <param name="entity">外出登记表实体</param>
        /// <returns>执行成功返回BoolMessage.True</returns>
        public BoolMessage Insert(EmployeeGoOutRegistration entity)
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
        /// 更新外出登记表
        /// </summary>
        /// <param name="entity">外出登记表实体</param>
        /// <returns>执行成功返回BoolMessage.True</returns>
        public BoolMessage Update(EmployeeGoOutRegistration entity)
        {
            try
            {
                repos.Update(entity, p => p.Id == entity.Id, p => p.GoOutReason, p => p.GoOutDateTime, p => p.PlanBackDateTime, p => p.Note);
                return BoolMessage.True;
            }
            catch (Exception e)
            {
                return new BoolMessage(false, e.Message);
            }
        }

        /// <summary>
        /// 更新外出登记表(主管领导意见)
        /// </summary>
        /// <param name="entity">外出登记表实体</param>
        /// <returns>执行成功返回BoolMessage.True</returns>
        public BoolMessage UpdateGeneralManagerOpinion(EmployeeGoOutRegistration entity)
        {
            try
            {
                repos.Update(entity, p => p.Id == entity.Id, p => p.GeneralManagerId, p => p.GeneralManagerIsAudit, p => p.GeneralManagerOpinion, p => p.GeneralManagerSign, p => p.GeneralManagerSignDate);
                return BoolMessage.True;
            }
            catch (Exception e)
            {
                return new BoolMessage(false, e.Message);
            }
        }

        /// <summary>
        /// 更新外出登记表(实际返回时间字段)
        /// </summary>
        /// <param name="entity">外出登记表实体</param>
        /// <returns>执行成功返回BoolMessage.True</returns>
        public BoolMessage UpdateGoBackInfo(EmployeeGoOutRegistration entity)
        {
            try
            {
                repos.Update(entity, p => p.Id == entity.Id, p => p.RealGoOutDateTime, p => p.RealBackDateTime);
                return BoolMessage.True;
            }
            catch (Exception e)
            {
                return new BoolMessage(false, e.Message);
            }
        }

        /// <summary>
        /// 删除外出登记表
        /// </summary>
        /// <param name="ids">外出登记表主键数组</param>
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
        /// 获取外出登记表对象
        /// </summary>
        /// <param name="id">外出登记表主键</param>
        /// <returns>返回外出登记表对象</returns>
        public EmployeeGoOutRegistration Get(int id)
        {
            return repos.Get(id);
        }
                
        /// <summary>
        /// 获取外出登记表列表
        /// </summary>
        /// <returns>返回外出登记表列表</returns>
        public List<EmployeeGoOutRegistration> GetList()
        {
            var query = repos.NewQuery.OrderBy(p => p.Id);
            return repos.Query(query).ToList();
        }

       
        /// <summary>
        /// 获取外出登记表DataTable
        /// </summary>
        /// <returns>返回外出登记表DataTable</returns>
        public DataTable GetTable()
        {
            var query = repos.NewQuery.OrderBy(p => p.Id);
            return repos.GetTable(query);
        }

        /// <summary>
        /// 获取所有外出登记分页列表
        /// </summary>
        /// <param name="pageIndex">页面索引</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="orderName">排序列名</param>
        /// <param name="orderDir">排序方式</param>
        /// <param name="employeeName">员工姓名</param>
        /// <param name="goOutDateTime">外出起始时间</param>
        /// <param name="goOutEndDateTime">外出结束时间</param>
        /// <returns>返回所有外出登记分页列表</returns>
        public PageList<EmployeeGoOutRegistration> GetAllPageList(int pageIndex, int pageSize, string orderName, string orderDir, string employeeName, string goOutDateTime, string goOutEndDateTime)
        {
            orderName = orderName.IsEmpty() ? nameof(EmployeeGoOutRegistration.Id) : orderName;
            orderDir = orderDir.IsEmpty() ? nameof(OrderDir.Desc) : orderDir;
            var query = repos.NewQuery.Take(pageSize).Page(pageIndex).
                OrderBy(orderName, orderDir.IsAsc());
            if (employeeName.IsNotEmpty())
            {
                query.Where(p => p.EmployeeName.Contains(employeeName));
            }
            if (goOutDateTime.IsNotEmpty() && goOutEndDateTime.IsNotEmpty())
            {
                query.Where(p => p.GoOutDateTime.Between(goOutDateTime.ToDateTime(), goOutEndDateTime.ToDateTime().AddDays(1).AddSeconds(-1)));
            }
            else if (goOutDateTime.IsNotEmpty())
            {
                DateTime goOutDatetime = goOutDateTime.ToDateTime();
                query.Where(p => p.GoOutDateTime > goOutDatetime);
            }
            else if (goOutEndDateTime.IsNotEmpty())
            {
                DateTime goOutEndDate = goOutEndDateTime.ToDateTime();
                query.Where(p => p.GoOutDateTime < goOutEndDate);
            }
            return repos.Page(query);
        }

        /// <summary>
        /// 获取本部门外出登记分页列表
        /// </summary>
        /// <param name="pageIndex">页面索引</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="orderName">排序列名</param>
        /// <param name="orderDir">排序方式</param>
        /// <param name="employeeName">员工姓名</param>
        /// <param name="goOutDateTime">外出起始时间</param>
        /// <param name="goOutEndDateTime">外出结束时间</param>
        /// <param name="departmentId">部门Id</param>
        /// <returns>返回本部门外出登记表分页列表</returns>
        public PageList<EmployeeGoOutRegistration> GetDepartmentPageList(int pageIndex, int pageSize, string orderName, string orderDir, string employeeName, string goOutDateTime, string goOutEndDateTime, int departmentId)
        {
            orderName = orderName.IsEmpty() ? nameof(EmployeeGoOutRegistration.Id) : orderName;//默认使用主键排序
            orderDir = orderDir.IsEmpty() ? nameof(OrderDir.Desc) : orderDir;//默认使用倒序排序
            var query = repos.NewQuery.Take(pageSize).Page(pageIndex).OrderBy(orderName, orderDir.IsAsc());
            if (employeeName.IsNotEmpty())
            {
                query.Where(p => p.EmployeeName.Contains(employeeName));
            }
            if (goOutDateTime.IsNotEmpty() && goOutEndDateTime.IsNotEmpty())
            {
                query.Where(p => p.GoOutDateTime.Between(goOutDateTime.ToDateTime(), goOutEndDateTime.ToDateTime().AddDays(1).AddSeconds(-1)));
            }
            else if (goOutDateTime.IsNotEmpty())
            {
                DateTime goOutDatetime = goOutDateTime.ToDateTime();
                query.Where(p => p.GoOutDateTime > goOutDatetime);
            }
            else if (goOutEndDateTime.IsNotEmpty())
            {
                DateTime goOutEndDate = goOutEndDateTime.ToDateTime();
                query.Where(p => p.GoOutDateTime < goOutEndDate);
            }
            query.Where(p => p.CreateDepartmentId == departmentId);
            return repos.Page(query);
        }

        /// <summary>
        /// 获取当前登录用户的外出登记分页列表
        /// </summary>
        /// <param name="pageIndex">页面索引</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="orderName">排序列名</param>
        /// <param name="orderDir">排序方式</param>
        /// <param name="goOutDateTime">外出起始时间</param>
        /// <param name="goOutEndDateTime">外出结束时间</param>
        /// <param name="currentUserId">当前登录用户Id</param>
        /// <returns>返回当前登录用户的外出登记分页列表</returns>
        public PageList<EmployeeGoOutRegistration> GetOneselfPageList(int pageIndex, int pageSize, string orderName, string orderDir, string goOutDateTime, string goOutEndDateTime,int CreateUserId)
        {
            orderName = orderName.IsEmpty() ? nameof(EmployeeGoOutRegistration.Id) : orderName;
            orderDir = orderDir.IsEmpty() ? nameof(OrderDir.Desc) : orderDir;
            var query = repos.NewQuery.Take(pageSize).Page(pageIndex).
                OrderBy(orderName, orderDir.IsAsc());
            if (goOutDateTime.IsNotEmpty() && goOutEndDateTime.IsNotEmpty())
            {
                query.Where(p => p.GoOutDateTime.Between(goOutDateTime.ToDateTime(), goOutEndDateTime.ToDateTime().AddDays(1).AddSeconds(-1)));
            }
            else if (goOutDateTime.IsNotEmpty())
            {
                DateTime goOutDatetime = goOutDateTime.ToDateTime();
                query.Where(p => p.GoOutDateTime > goOutDatetime);
            }
            else if (goOutEndDateTime.IsNotEmpty())
            {
                DateTime goOutEndDate = goOutEndDateTime.ToDateTime();
                query.Where(p => p.GoOutDateTime < goOutEndDate);
            }
            query.Where(p => p.CreateUserId == CreateUserId);
            return repos.Page(query);
        }

        /// <summary>
        /// 获取当前登录用户待审批的外出登记分页列表
        /// </summary>
        /// <param name="pageIndex">页面索引</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="orderName">排序列名</param>
        /// <param name="orderDir">排序方式</param>
        /// <param name="goOutDateTime">外出起始时间</param>
        /// <param name="goOutEndDateTime">外出结束时间</param>
        /// <param name="currentUserId">当前登录用户Id</param>
        /// <returns>返回当前登录用户待审批的外出登记分页列表</returns>
       // public PageList<EmployeeGoOutRegistration> GetGeneralManagerOpinionPageList(int pageIndex, int pageSize, string orderName, string orderDir, string goOutDateTime, string goOutEndDateTime, int currentUserId)
        public PageList<EmployeeGoOutRegistration> GetGeneralManagerOpinionPageList(int pageIndex, int pageSize, string orderName, string orderDir,string employeeName, string goOutDateTime, string goOutEndDateTime)
        {
            orderName = orderName.IsEmpty() ? nameof(EmployeeGoOutRegistration.Id) : orderName;
            orderDir = orderDir.IsEmpty() ? nameof(OrderDir.Desc) : orderDir;
            var query = repos.NewQuery.Take(pageSize).Page(pageIndex).
                OrderBy(orderName, orderDir.IsAsc());
            if (employeeName.IsNotEmpty())
            {
                query.Where(p => p.EmployeeName.Contains(employeeName));
            }
            if (goOutDateTime.IsNotEmpty() && goOutEndDateTime.IsNotEmpty())
            {
                query.Where(p => p.GoOutDateTime.Between(goOutDateTime.ToDateTime(), goOutEndDateTime.ToDateTime().AddDays(1).AddSeconds(-1)));
            }
            else if (goOutDateTime.IsNotEmpty())
            {
                DateTime goOutDatetime = goOutDateTime.ToDateTime();
                query.Where(p => p.GoOutDateTime > goOutDatetime);
            }
            else if (goOutEndDateTime.IsNotEmpty())
            {
                DateTime goOutEndDate = goOutEndDateTime.ToDateTime();
                query.Where(p => p.GoOutDateTime < goOutEndDate);
            }
            //query.Where(p => p.GeneralManagerId == currentUserId);
            query.Where(p => p.GeneralManagerIsAudit == null);
            return repos.Page(query);
        }

    }
}