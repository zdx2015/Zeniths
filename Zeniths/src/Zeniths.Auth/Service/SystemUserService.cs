using System;
using System.Collections.Generic;
using System.Linq;
using Zeniths.Auth.Entity;
using Zeniths.Collections;
using Zeniths.Data;
using Zeniths.Data.Extensions;
using Zeniths.Extensions;
using Zeniths.Helper;
using Zeniths.Utility;

namespace Zeniths.Auth.Service
{
    public class SystemUserService
    {
        private readonly AuthRepository<SystemUser> repos = new AuthRepository<SystemUser>();

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="account">账号</param>
        /// <param name="password">密码(明文)</param>
        /// <returns>操作成功返回True</returns>
        public BoolMessage Login(string account, string password)
        {
            var entity = repos.Get(p => p.Account.Equals(account));
            if (entity == null)
            {
                return new BoolMessage(false, "无效的账号");
            }
            if (entity.Password == null)
            {
                entity.Password = string.Empty;
            }

            #region 检测条件

            var encryptedPwd = GenerateEncryptPassword(account, password);
            if (!entity.Password.Equals(encryptedPwd))
            {
                return new BoolMessage(false, "账号密码错误");
            }

            if (!entity.IsEnabled)
            {
                return new BoolMessage(false, "账号已经被禁用,请联系管理员");
            }

            if (!entity.IsAudit)
            {
                return new BoolMessage(false, "账号还没有通过审核,请联系管理员");
            }

            if (IsUserExpire(entity))
            {
                return new BoolMessage(false, "账号使用期限失效,请联系管理员");
            }

            //logonMessage = "用户登陆系统成功";
            //SystemLogManage.Current.Message(logonMessage, Constant.LogonLogCategory);

            #endregion

            #region 更新状态

            entity.LastVisitDateTime = DateTime.Now;
            entity.LoginCount += 1;
            if (!entity.FirstVisitDateTime.HasValue)
            {
                entity.FirstVisitDateTime = DateTime.Now;
            }

            repos.Update(entity, p => p.Id == entity.Id, p => p.FirstVisitDateTime, p => p.LastVisitDateTime, p => p.LoginCount);

            #endregion

            return new BoolMessage(true);
        }

        /// <summary>
        /// 用户注销
        /// </summary>
        /// <param name="userId">用户主键</param>
        /// <returns>操作成功返回True</returns>
        public BoolMessage Logout(int userId)
        {
            //var entity = Repository.Get(p => p.Id.Equals(userId) && p.Deleted == false, p => p.Id);
            //if (entity == null)
            //{
            //    return new BoolMessage(false, "无效的用户主键");
            //}
            //entity.IsOnline = false;
            //return Repository.Update(entity, p => p.Id == entity.Id, p => p.IsOnline);

            //SystemOnlineUserManage.Current.LogoutUser(userId);
            //SystemLogManage.Current.Message("用户退出系统成功", Constant.LogonLogCategory);
            return new BoolMessage(true);
        }

        /// <summary>
        /// 生成用户加密后密码
        /// </summary>
        /// <param name="account">账号</param>
        /// <param name="password">密码(明文)</param>
        /// <returns>返回加密后的密码</returns>
        public string GenerateEncryptPassword(string account, string password)
        {
            if (string.IsNullOrEmpty(account))
            {
                throw new ArgumentNullException(nameof(account), "请指定用户账号");
            }
            return StringHelper.EncryptString($"{account}-@{password}");
        }

        /// <summary>
        /// 用户是否过期
        /// </summary>
        /// <param name="entity">用户对象</param>
        /// <returns></returns>
        public bool IsUserExpire(SystemUser entity)
        {
            return (entity.AllowStartDateTime.HasValue && entity.AllowStartDateTime.Value >= DateTime.Now)
                   || (entity.AllowEndDateTime.HasValue && entity.AllowEndDateTime.Value <= DateTime.Now);
        }


        /// <summary>
        /// 检测是否存在指定用户
        /// </summary>
        /// <param name="entity">用户实体</param>
        /// <returns>存在返回true</returns>
        public BoolMessage Exists(SystemUser entity)
        {
            var has = repos.Exists(p => p.Name == entity.Name && p.Id != entity.Id);
            return has ? new BoolMessage(false, "输入用户账号已经存在") : BoolMessage.True;
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="entity">用户实体</param>
        public BoolMessage Insert(SystemUser entity)
        {
            try
            {
                entity.Password = GenerateEncryptPassword(entity.Account, entity.Password);
                repos.Insert(entity);
                return BoolMessage.True;
            }
            catch (Exception e)
            {
                return new BoolMessage(false, e.Message);
            }
        }

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="entity">用户实体</param>
        public BoolMessage Update(SystemUser entity)
        {
            try
            {
                //更新的列数组
                var updateCols = new[]
                {
                    nameof(SystemUser.Account),
                    nameof(SystemUser.Name),
                    nameof(SystemUser.NameSpell),
                    nameof(SystemUser.AllowStartDateTime),
                    nameof(SystemUser.AllowEndDateTime),
                    nameof(SystemUser.IsAdmin),
                    nameof(SystemUser.IsEnabled),
                    nameof(SystemUser.HintQuestion),
                    nameof(SystemUser.HintAnswer),
                    nameof(SystemUser.SortIndex),
                    nameof(SystemUser.Note)
                };

                repos.Update(entity, p => p.Id == entity.Id, updateCols);
                return BoolMessage.True;
            }
            catch (Exception e)
            {
                return new BoolMessage(false, e.Message);
            }
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="ids">用户主键数组</param>
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
        /// 获取用户对象
        /// </summary>
        /// <param name="id">用户主键</param>
        /// <returns>用户对象</returns>
        public SystemUser Get(int id)
        {
            return repos.Get(id);
        }

        /// <summary>
        /// 根据账号获取用户对象
        /// </summary>
        /// <param name="account">账号</param>
        public SystemUser GetByAccount(string account)
        {
            return repos.Get(p => p.Account.Equals(account));
        }

        /// <summary>
        /// 根据账号获取用户姓名
        /// </summary>
        /// <param name="account">账号</param>
        public string GetNameByAccount(string account)
        {
            var entity = repos.Get(p => p.Account.Equals(account), p => p.Name);
            return entity != null ? entity.Name : string.Empty;
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="userId">用户主键</param>
        /// <param name="newPassword">新密码(明文)</param>
        public BoolMessage UpdatePassword(int userId, string newPassword)
        {
            var entity = repos.Get(p => p.Id == userId, p => p.Account);
            if (entity == null)
            {
                throw new ArgumentException("无效的用户主键", nameof(userId));
            }
            entity.Password = GenerateEncryptPassword(entity.Account, newPassword);
            try
            {
                repos.Update(entity, p => p.Id == userId, p => p.Password);
                return BoolMessage.True;
            }
            catch (Exception e)
            {
                return new BoolMessage(false, e.Message);
            }
        }

        /// <summary>
        /// 获取启用的用户列表
        /// </summary>
        /// <returns>返回启用的用户列表</returns>
        public List<SystemUser> GetEnabledList()
        {
            var query = repos.NewQuery.Where(p => p.IsEnabled == true).OrderBy(p => p.SortIndex);
            return repos.Query(query).ToList();
        }

        /// <summary>
        /// 获取用户列表(包括禁用记录)
        /// </summary>
        /// <param name="pageIndex">页面索引</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="orderName">排序列名</param>
        /// <param name="orderDir">排序方式</param>
        /// <param name="name">用户账号/姓名/简拼</param>
        /// <param name="departmentIds">部门主键数组</param>
        /// <returns>用户分页列表</returns>
        public PageList<SystemUser> GetPageList(int pageIndex, int pageSize, string orderName,
            string orderDir, string name, int[] departmentIds)
        {
            orderName = orderName.IsEmpty() ? nameof(SystemUser.SortIndex) : orderName;
            orderDir = orderDir.IsEmpty() ? nameof(OrderDir.Asc) : orderDir;
            var query = repos.NewQuery.Take(pageSize).Page(pageIndex).
                OrderBy(orderName, orderDir.IsAsc());
            if (name.IsNotEmpty())
            {
                name = name.Trim();
                query.Where(p => p.Account.Contains(name) || p.Name.Contains(name) || p.NameSpell.Contains(name));
            }
            if (departmentIds.Length > 0)
            {
                query.Where(p => p.DepartmentId.In(departmentIds));
            }
            return repos.Page(query);
        }
    }
}