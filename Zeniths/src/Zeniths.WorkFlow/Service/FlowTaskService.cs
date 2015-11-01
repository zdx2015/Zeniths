using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Zeniths.WorkFlow.Entity;
using Zeniths.Collections;
using Zeniths.Data;
using Zeniths.Data.Extensions;
using Zeniths.Entity;
using Zeniths.Extensions;
using Zeniths.Helper;
using Zeniths.Utility;

namespace Zeniths.WorkFlow.Service
{
    /// <summary>
    /// 流程任务服务
    /// </summary>
    public class FlowTaskService
    {
        /// <summary>
        /// 流程任务存储器
        /// </summary>
        private readonly WorkFlowRepository<FlowTask> repos = new WorkFlowRepository<FlowTask>();

        /// <summary>
        /// 新增流程任务
        /// </summary>
        /// <param name="entity">流程任务实体</param>
        /// <returns>执行成功返回BoolMessage.True</returns>
        public BoolMessage Insert(FlowTask entity)
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
        /// 更新流程任务
        /// </summary>
        /// <param name="entity">流程任务实体</param>
        /// <returns>执行成功返回BoolMessage.True</returns>
        public BoolMessage Update(FlowTask entity)
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
        /// 删除流程任务
        /// </summary>
        /// <param name="ids">流程任务主键数组</param>
        /// <returns>执行成功返回BoolMessage.True</returns>
        public BoolMessage Delete(string[] ids)
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
        /// 获取流程任务对象
        /// </summary>
        /// <param name="taskId">流程任务主键</param>
        /// <returns>返回流程任务对象</returns>
        public FlowTask Get(string taskId)
        {
            return repos.Get(taskId);
        }

        /// <summary>
        /// 删除流程实例
        /// </summary>
        /// <param name="flowId">流程Id</param>
        /// <param name="flowInstanceId">流程实例Id</param>
        /// <returns></returns>
        public BoolMessage Delete(string flowId, string flowInstanceId)
        {
            try
            {
                repos.Delete(p => p.FlowId == flowId && p.FlowInstanceId == flowInstanceId);
                return BoolMessage.True;
            }
            catch (Exception e)
            {
                return new BoolMessage(false, e.Message);
            }
        }

        /// <summary>
        /// 更新读取时间
        /// </summary>
        /// <param name="taskId">任务主键</param>
        /// <param name="readDateTime">读取时间</param>
        public BoolMessage UpdateReadDateTime(string taskId, DateTime readDateTime)
        {
            try
            {
                var sql = "UPDATE FlowTask SET ReadDateTime=@ReadDateTime,Status=1 " +
                          "WHERE Id=@Id AND ReadDateTime IS NULL";
                repos.Database.Execute(sql, new object[] { readDateTime, taskId });
                return BoolMessage.True;
            }
            catch (Exception e)
            {
                return new BoolMessage(false, e.Message);
            }
        }

        /// <summary>
        /// 更新任务标题
        /// </summary>
        /// <param name="taskId">任务主键</param>
        /// <param name="taskTitle">任务标题</param>
        /// <returns></returns>
        public BoolMessage UpdateTaskTitle(string taskId, string taskTitle)
        {
            try
            {
                repos.Update(new FlowTask { Title = taskTitle }, p => p.Id == taskId, p => p.Title);
                return BoolMessage.True;
            }
            catch (Exception e)
            {
                return new BoolMessage(false, e.Message);
            }
        }


        /// <summary>
        /// 获取流程任务发起者Id
        /// </summary>
        /// <param name="flowId">流程Id</param>
        /// <param name="flowInstanceId">流程实例Id</param>
        /// <returns></returns>
        public string GetFirstSenderId(string flowId, string flowInstanceId)
        {
            string sql = "SELECT SenderId FROM FlowTask " +
                         "WHERE FlowId=@FlowId AND FlowInstanceId=@FlowInstanceId AND PrevId IS NULL";
            return repos.Database.ExecuteScalar<string>(sql, new object[] { flowId, flowInstanceId });
        }

        /// <summary>
        /// 获取流程实例指定步骤的处理者Id集合
        /// </summary>
        /// <param name="flowId">流程Id</param>
        /// <param name="stepId">步骤Id</param>
        /// <param name="flowInstanceId">流程实例Id</param>
        /// <returns></returns>
        public List<string> GetStepSenderIdList(string flowId, string stepId, string flowInstanceId)
        {
            string sql = "SELECT ReceiveId FROM WorkFlowTask " +
                         "WHERE FlowId=@FlowId AND StepId=@StepId AND FlowInstanceId=@FlowInstanceId";
            return repos.Database.Query<string>(sql, new object[] { flowId, stepId, flowInstanceId }).ToList();
        }

        /// <summary>
        /// 获取流程实例前一步骤的处理者主键集合
        /// </summary>
        /// <param name="flowId">流程主键</param>
        /// <param name="stepId">步骤主键</param>
        /// <param name="flowInstanceId">流程实例Id</param>
        /// <returns></returns>
        public List<string> GetPrevSenderIdList(string flowId, string stepId, string flowInstanceId)
        {
            string sql = "SELECT ReceiveId FROM FlowTask WHERE Id = " +
                         "(SELECT PrevId FROM FlowTask WHERE FlowId=@FlowId AND StepId=@StepId AND FlowInstanceId=@FlowInstanceId)";
            return repos.Database.Query<string>(sql, new object[] { flowId, stepId, flowInstanceId }).ToList();
        }

        /// <summary>
        /// 完成指定任务
        /// </summary>
        /// <param name="taskId">任务主键</param>
        /// <param name="opinion">意见</param>
        /// <param name="isAudit">是否审核通过</param>
        /// <param name="status">状态 0待处理1打开2完成3退回4他人已处理5他人已退回</param>
        /// <param name="note">备注</param>
        /// <returns></returns>
        public BoolMessage Completed(string taskId, string opinion = null, bool? isAudit = null, int status = 2, string note = null)
        {
            try
            {
                string sql = "UPDATE FlowTask SET Opinion=@Opinion,ActualFinishDateTime" +
                             "=@ActualFinishDateTime,IsAudit=@IsAudit,Status=@Status,Note=@Note WHERE Id=@Id";
                repos.Database.Execute(sql, new object[] { opinion, DateTime.Now, isAudit, status, note, taskId });
                return BoolMessage.True;
            }
            catch (Exception e)
            {
                return new BoolMessage(false, e.Message);
            }
        }

        /// <summary>
        /// 更新一个任务后续任务状态
        /// </summary>
        /// <param name="taskId">任务主键</param>
        /// <param name="status">状态 0待处理1打开2完成3退回4他人已处理5他人已退回</param>
        /// <returns></returns>
        public BoolMessage UpdateNextTaskStatus(string taskId, int status)
        {
            try
            {
                string sql = "UPDATE FlowTask SET Status=@Status WHERE PrevID=@PrevId";
                repos.Database.Execute(sql, new object[] { status, taskId });
                return BoolMessage.True;
            }
            catch (Exception e)
            {
                return new BoolMessage(false, e.Message);
            }
        }

        /// <summary>
        /// 获取流程实例指定步骤的任务
        /// </summary>
        /// <param name="flowId">流程主键</param>
        /// <param name="stepId">步骤主键</param>
        /// <param name="flowInstanceId">流程实例Id</param>
        /// <returns></returns>
        public List<FlowTask> GetTaskList(string flowId, string stepId, string flowInstanceId)
        {
            return repos.Query(p => p.FlowId == flowId && p.StepId == stepId
            && p.FlowInstanceId == flowInstanceId).ToList();
        }

        /// <summary>
        /// 得到一个实例的任务
        /// </summary>
        /// <param name="flowId">流程主键</param>
        /// <param name="flowInstanceId">流程实例主键</param>
        /// <returns></returns>
        public List<FlowTask> GetTaskList(string flowId, string flowInstanceId)
        {
            return repos.Query(p => p.FlowId == flowId && p.FlowInstanceId == flowInstanceId).ToList();
        }

        /// <summary>
        /// 得到一个实例的任务
        /// </summary>
        /// <param name="flowInstanceId">流程实例主键</param>
        /// <returns></returns>
        public List<FlowTask> GetTaskListByInstanceId(string flowInstanceId)
        {
            var query = repos.NewQuery.Where(p => p.FlowInstanceId == flowInstanceId)
                .OrderBy(p => p.SortIndex);
            return repos.Query(query).ToList();
        }

        /// <summary>
        /// 获取流程实例指定步骤指定人员的任务
        /// </summary>
        /// <param name="flowId">流程主键</param>
        /// <param name="stepId">步骤主键</param>
        /// <param name="flowInstanceId">流程实例Id</param>
        /// <param name="userId">用户主键</param>
        /// <returns></returns>
        public List<FlowTask> GetUserTaskList(string flowId, string stepId, string flowInstanceId, string userId)
        {
            return repos.Query(p => p.FlowId == flowId && p.StepId == stepId
            && p.FlowInstanceId == flowInstanceId && p.ReceiveId == userId).ToList();
        }


        /// <summary>
        /// 获取和一个任务同级的任务
        /// </summary>
        /// <param name="taskId">任务主键</param>
        /// <param name="isStepId">是否区分步骤Id，多步骤会签区分的是上一步骤Id</param>
        /// <returns></returns>
        public List<FlowTask> GetSiblingTaskList(string taskId, bool isStepId = true)
        {
            var task = Get(taskId);
            if (task == null)
            {
                return new List<FlowTask>();
            }
            var cols = StringHelper.ConvertArrayToString(EntityMetadata.ForType(typeof(FlowTask)).QueryColumns);
            string sql =
                $"SELECT {cols} FROM FlowTask WHERE PrevId=@PrevId AND {(isStepId ? "StepId=@StepId" : "PrevStepId=@StepId")}";

            return repos.Database.Query<FlowTask>(sql,
                new object[] { task.PrevId, isStepId ? task.StepId : task.PrevStepId }).ToList();
        }

        /// <summary>
        /// 获取指定任务的前一任务
        /// </summary>
        /// <param name="taskId">任务主键</param>
        /// <returns></returns>
        public List<FlowTask> GetPrevTaskList(string taskId)
        {
            var cols = StringHelper.ConvertArrayToString(EntityMetadata.ForType(typeof(FlowTask)).QueryColumns);
            string sql = $"SELECT {cols} FROM FlowTask WHERE Id=(SELECT PrevId FROM FlowTask WHERE Id=@Id)";
            return repos.Database.Query<FlowTask>(sql, new object[] { taskId }).ToList();
        }

        /// <summary>
        /// 获取指定任务的后续任务
        /// </summary>
        /// <param name="taskId">任务主键</param>
        /// <returns></returns>
        public List<FlowTask> GetNextTaskList(string taskId)
        {
            return repos.Query(p => p.PrevId == taskId).ToList();
        }

        /// <summary>
        /// 查询指定流程是否有任务数据
        /// </summary>
        /// <param name="flowId">流程Id</param>
        /// <returns>如果有任务数据返回true</returns>
        public bool GetFlowHasTasks(Guid flowId)
        {
            string sql = "SELECT TOP 1 Id FROM FlowTask WHERE FlowId=@FlowId";
            var result = repos.Database.ExecuteScalar<int?>(sql, new object[] { flowId });
            return result.HasValue;
        }

        /// <summary>
        /// 查询一个用户在一个步骤中是否有未完成任务
        /// </summary>
        /// <param name="flowId">流程Id</param>
        /// <param name="stepId">步骤Id</param>
        /// <param name="flowInstanceId">流程实例Id</param>
        /// <param name="userId">用户Id</param>
        /// <returns>如果指定用户有未完成任务返回true</returns>
        public bool GetUserHasNoCompletedTasks(string flowId, string stepId, string flowInstanceId, string userId)
        {
            string sql = "SELECT TOP 1 ID FROM FlowTask WHERE FlowId=@FlowId " +
                         "AND StepId=@StepId AND FlowInstanceId=@FlowInstanceId " +
                         "AND ReceiveId=@ReceiveId AND Status IN(0,1)";
            var result = repos.Database
                .ExecuteScalar<int?>(sql, new object[] { flowId, stepId, flowInstanceId, userId });
            return result.HasValue;
        }

        /// <summary>
        /// 获取任务状态
        /// </summary>
        /// <param name="taskId">任务Id</param>
        /// <returns></returns>
        public int GetTaskStatus(string taskId)
        {
            string sql = "SELECT Status FROM FlowTask WHERE Id=@Id";
            return repos.Database.ExecuteScalar<int>(sql, new object[] { taskId });
        }


        /// <summary>
        /// 获取流程任务分页列表
        /// </summary>
        /// <param name="pageIndex">页面索引</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="orderName">排序列名</param>
        /// <param name="orderDir">排序方式</param>
        /// <param name="receiveId">接收人主键</param>
        /// <param name="senderId">发送人主键</param>
        /// <param name="flowId">流程主键</param>
        /// <param name="title">任务标题</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="type">查询类型:0:待办 1:已办</param>
        /// <returns>返回流程任务分页列表</returns>
        public PageList<FlowTask> GetTaskPageList(int pageIndex, int pageSize, string orderName, string orderDir,
            string receiveId, string senderId, string flowId, string title, string startDate, string endDate, int type = 0)
        {
            orderName = orderName.IsEmpty() ? (type == 0 ? nameof(FlowTask.SenderDateTime) : nameof(FlowTask.ActualFinishDateTime)) : orderName;
            orderDir = orderDir.IsEmpty() ? nameof(OrderDir.Desc) : orderDir;
            var query = repos.NewQuery.Take(pageSize).Page(pageIndex).
                OrderBy(orderName, orderDir.IsAsc());

            if (type == 0)
            {
                query.Where(p => p.Status == 0 || p.Status == 1);
            }
            else
            {
                query.Where(p => p.Status == 2 || p.Status == 3);
            }

            if (receiveId.IsNotEmpty())
            {
                query.Where(p => p.ReceiveId == receiveId);
            }

            if (senderId.IsNotEmpty())
            {
                query.Where(p => p.SenderId == senderId);
            }
            if (flowId.IsNotEmpty())
            {
                query.Where(p => p.FlowId == flowId);
            }
            if (title.IsNotEmpty())
            {
                query.Where(p => p.Title.Contains(title));
            }
            if (startDate.IsNotEmpty())
            {
                var _startDate = startDate.ToDateTime().Date;
                query.Where(p => p.SenderDateTime >= _startDate);
            }
            if (endDate.IsNotEmpty())
            {
                var _endDate = endDate.ToDateTime().AddDays(1).Date;
                query.Where(p => p.SenderDateTime <= _endDate);
            }

            return repos.Page(query);
        }

        /// <summary>
        /// 得到流程业务列表(每条业务记录一条记录)
        /// </summary>
        /// <param name="pageIndex">页面索引</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="orderName">排序列名</param>
        /// <param name="orderDir">排序方式</param>
        /// <param name="receiveId">接收人主键</param>
        /// <param name="senderId">发送人主键</param>
        /// <param name="flowId">流程主键</param>
        /// <param name="title">任务标题</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns></returns>
        public PageList<FlowTask> GetBusinessTaskPageList(int pageIndex, int pageSize, string orderName, string orderDir,
            string receiveId, string senderId, string flowId, string title, string startDate, string endDate)
        {
            orderName = orderName.IsEmpty() ? nameof(FlowTask.ActualFinishDateTime) : orderName;
            orderDir = orderDir.IsEmpty() ? nameof(OrderDir.Desc) : orderDir;
            var query = repos.NewQuery.Take(pageSize).Page(pageIndex)
                .SetTableName("FlowTask a")
                .Where(
                    "a.Id=(SELECT TOP 1 ID FROM FlowTask WHERE FlowId=a.FlowId AND FlowInstanceId=a.FlowInstanceId ORDER BY SortIndex DESC)")
                .OrderBy(orderName, orderDir.IsAsc());
             
            if (receiveId.IsNotEmpty())
            {
                query.Where(p => p.ReceiveId == receiveId);
            }

            if (senderId.IsNotEmpty())
            {
                query.Where(p => p.SenderId == senderId);
            }

            if (flowId.IsNotEmpty())
            {
                query.Where(p => p.FlowId == flowId);
            }
            if (title.IsNotEmpty())
            {
                query.Where(p => p.Title.Contains(title));
            }
            if (startDate.IsNotEmpty())
            {
                var _startDate = startDate.ToDateTime().Date;
                query.Where(p => p.SenderDateTime >= _startDate);
            }
            if (endDate.IsNotEmpty())
            {
                var _endDate = endDate.ToDateTime().AddDays(1).Date;
                query.Where(p => p.SenderDateTime <= _endDate);
            }
            return repos.Page(query);
        }



        #region 私有方法


        #endregion
    }
}