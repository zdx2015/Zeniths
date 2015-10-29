using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zeniths.Collections;
using Zeniths.Extensions;
using Zeniths.Hr.Entity;
using Zeniths.Utility;

namespace Zeniths.Hr.Service
{
    public class GoOutRegistrationService
    {
        /// <summary>
        /// 存储器
        /// </summary>
        private readonly HrRepository<GoOutRegistration> repos = new HrRepository<GoOutRegistration>();


        ///// <summary>
        ///// 检测是否存在指定外出登记
        ///// </summary>
        ///// <param name="entity">外出登记实体</param>
        ///// <returns>存在返回true</returns>
        //public BoolMessage Exists(GoOutRegistration entity)
        //{
        //    var has = repos.Exists(p => p.EmployeeId == entity.EmployeeId && p.Id != entity.Id);
        //    return has ? new BoolMessage(false, "输入外出登记已经存在") : BoolMessage.True;
        //}

        /// <summary>
        /// 添加外出登记
        /// </summary>
        /// <param name="entity">外出登记实体</param>
        public BoolMessage Insert(GoOutRegistration entity)
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
        /// 更新外出登记
        /// </summary>
        /// <param name="entity">外出登记实体</param>
        public BoolMessage Update(GoOutRegistration entity)
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
        /// 删除外出登记
        /// </summary>
        /// <param name="ids">外出登记主键数组</param>
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
        /// 获取外出登记对象
        /// </summary>
        /// <param name="id">外出登记主键</param>
        /// <returns>外出登记对象</returns>
        public GoOutRegistration Get(int id)
        {
            return repos.Get(id);
        }

        /// <summary>
        /// 获取外出登记列表
        /// </summary>
        /// <returns>外出登记列表</returns>
        public List<GoOutRegistration> GetList()
        {
            var query = repos.NewQuery.OrderBy(p => p.Id);
            return repos.Query(query).ToList();
        }

        /// <summary>
        /// 获取所有外出登记列表(分页)
        /// </summary>
        /// <param name="pageIndex">页面索引</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="orderName">排序列名</param>
        /// <param name="orderDir">排序方式</param>
        /// <returns></returns>
        public PageList<GoOutRegistration> GetAllPageList(int pageIndex, int pageSize, string orderName,
            string orderDir)
        {
            orderName = orderName.IsEmpty() ? nameof(GoOutRegistration.Id) : orderName;
            orderDir = orderDir.IsEmpty() ? nameof(OrderDir.Desc) : orderDir;
            var query = repos.NewQuery.Take(pageSize).Page(pageIndex).
                OrderBy(orderName, orderDir.IsAsc());
            return repos.Page(query);
        }

        /// <summary>
        /// 获取指定条件的外出登记列表(分页)
        /// </summary>
        /// <param name="pageIndex">页面索引</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="orderName">排序列名</param>
        /// <param name="orderDir">排序方式</param>
        /// <param name="buttonName">按钮名称</param>
        /// <returns></returns>
        public PageList<GoOutRegistration> GetPageList(int pageIndex, int pageSize, string orderName,
            string orderDir, string buttonName)
        {
            orderName = orderName.IsEmpty() ? nameof(GoOutRegistration.Id) : orderName;
            orderDir = orderDir.IsEmpty() ? nameof(OrderDir.Desc) : orderDir;
            var query = repos.NewQuery.Take(pageSize).Page(pageIndex).
                OrderBy(orderName, orderDir.IsAsc());
            if (buttonName.IsNotEmpty())
            {
                buttonName = buttonName.Trim();
                query.Where(p => p.EmployeeName.Contains(buttonName));
            }
            return repos.Page(query);
        }

    }
}
