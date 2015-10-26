using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Zeniths.WorkFlow.Entity;
using Zeniths.Collections;
using Zeniths.Data;
using Zeniths.Extensions;
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
        private readonly Repository<FlowTask> repos = new Repository<FlowTask>();

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
        /// <param name="id">流程任务主键</param>
        /// <returns>返回流程任务对象</returns>
        public FlowTask Get(int id)
        {
            return repos.Get(id);
        }

        /// <summary>
        /// 获取流程任务列表
        /// </summary>
        /// <returns>返回流程任务列表</returns>
        public List<FlowTask> GetList()
        {
            var query = repos.NewQuery.OrderBy(p => p.Id);
            return repos.Query(query).ToList();
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
        /// <param name="taskId"></param>
        /// <param name="readDateTime"></param>
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
        /// 获取流程指定步骤的处理者Id集合
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

        ///// <summary>
        ///// 得到一个流程实例前一步骤的处理者
        ///// </summary>
        ///// <param name="flowID"></param>
        ///// <param name="groupID"></param>
        ///// <returns></returns>
        //public List<Guid> GetPrevSnderID(Guid flowID, Guid stepID, Guid groupID)
        //{
        //    string sql = "SELECT ReceiveID FROM WorkFlowTask WHERE ID=(SELECT PrevID FROM WorkFlowTask WHERE FlowID=@FlowID AND StepID=@StepID AND GroupID=@GroupID)";
        //    SqlParameter[] parameters = new SqlParameter[]{
        //        new SqlParameter("@FlowID", SqlDbType.UniqueIdentifier){ Value = flowID },
        //        new SqlParameter("@StepID", SqlDbType.UniqueIdentifier){ Value = stepID },
        //        new SqlParameter("@GroupID", SqlDbType.UniqueIdentifier){ Value = groupID }
        //    };
        //    DataTable dt = dbHelper.GetDataTable(sql, parameters);
        //    List<Guid> senderList = new List<Guid>();
        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        Guid senderID;
        //        if (Guid.TryParse(dr[0].ToString(), out senderID))
        //        {
        //            senderList.Add(senderID);
        //        }
        //    }
        //    return senderList;
        //}

        ///// <summary>
        ///// 完成一个任务
        ///// </summary>
        ///// <param name="taskID"></param>
        ///// <param name="comment"></param>
        ///// <param name="isSign"></param>
        ///// <returns></returns>
        //public int Completed(Guid taskID, string comment = "", bool isSign = false, int status = 2, string note = "")
        //{
        //    string sql = "UPDATE WorkFlowTask SET Comment=@Comment,CompletedTime1=@CompletedTime1,IsSign=@IsSign,Status=@Status" + (note.IsNullOrEmpty() ? "" : ",Note=@Note") + " WHERE ID=@ID";
        //    SqlParameter[] parameters = new SqlParameter[]{
        //        comment.IsNullOrEmpty() ? new SqlParameter("@Comment", SqlDbType.VarChar){ Value = DBNull.Value } : new SqlParameter("@Comment", SqlDbType.VarChar){ Value = comment },
        //        new SqlParameter("@CompletedTime1", SqlDbType.DateTime){ Value = RoadFlow.Utility.DateTimeNew.Now },
        //        new SqlParameter("@IsSign", SqlDbType.Int){ Value = isSign?1:0 },
        //        new SqlParameter("@Status", SqlDbType.Int){ Value = status },
        //        note.IsNullOrEmpty()?new SqlParameter("@Note", SqlDbType.NVarChar){ Value = DBNull.Value }:new SqlParameter("@Note", SqlDbType.NVarChar){ Value = note },
        //        new SqlParameter("@ID", SqlDbType.UniqueIdentifier){ Value = taskID }
        //    };
        //    return dbHelper.Execute(sql, parameters);
        //}

        ///// <summary>
        ///// 更新一个任务后后续任务状态
        ///// </summary>
        ///// <param name="taskID"></param>
        ///// <param name="comment"></param>
        ///// <param name="isSign"></param>
        ///// <returns></returns>
        //public int UpdateNextTaskStatus(Guid taskID, int status)
        //{
        //    string sql = "UPDATE WorkFlowTask SET Status=@Status WHERE PrevID=@PrevID";
        //    SqlParameter[] parameters = new SqlParameter[]{
        //        new SqlParameter("@Status", SqlDbType.Int){ Value = status },
        //        new SqlParameter("@PrevID", SqlDbType.UniqueIdentifier){ Value = taskID }
        //    };
        //    return dbHelper.Execute(sql, parameters);
        //}


        ///// <summary>
        ///// 得到一个流程实例一个步骤的任务
        ///// </summary>
        ///// <param name="flowID"></param>
        ///// <param name="groupID"></param>
        ///// <returns></returns>
        //public List<Model.WorkFlowTask> GetTaskList(Guid flowID, Guid stepID, Guid groupID)
        //{
        //    string sql = "SELECT * FROM WorkFlowTask WHERE FlowID=@FlowID AND StepID=@StepID AND GroupID=@GroupID";
        //    SqlParameter[] parameters = new SqlParameter[]{
        //        new SqlParameter("@FlowID", SqlDbType.UniqueIdentifier){ Value = flowID },
        //        new SqlParameter("@StepID", SqlDbType.UniqueIdentifier){ Value = stepID },
        //        new SqlParameter("@GroupID", SqlDbType.UniqueIdentifier){ Value = groupID }
        //    };
        //    SqlDataReader dataReader = dbHelper.GetDataReader(sql, parameters);
        //    List<RoadFlow.Data.Model.WorkFlowTask> List = DataReaderToList(dataReader);
        //    dataReader.Close();
        //    return List;
        //}

        ///// <summary>
        ///// 得到一个流程实例一个步骤一个人员的任务
        ///// </summary>
        ///// <param name="flowID"></param>
        ///// <param name="stepID"></param>
        ///// <param name="groupID"></param>
        ///// <param name="userID"></param>
        ///// <returns></returns>
        //public List<Model.WorkFlowTask> GetUserTaskList(Guid flowID, Guid stepID, Guid groupID, Guid userID)
        //{
        //    string sql = "SELECT * FROM WorkFlowTask WHERE FlowID=@FlowID AND StepID=@StepID AND GroupID=@GroupID AND ReceiveID=@ReceiveID";
        //    SqlParameter[] parameters = new SqlParameter[]{
        //        new SqlParameter("@FlowID", SqlDbType.UniqueIdentifier){ Value = flowID },
        //        new SqlParameter("@StepID", SqlDbType.UniqueIdentifier){ Value = stepID },
        //        new SqlParameter("@GroupID", SqlDbType.UniqueIdentifier){ Value = groupID },
        //        new SqlParameter("@ReceiveID", SqlDbType.UniqueIdentifier){ Value = userID }
        //    };
        //    SqlDataReader dataReader = dbHelper.GetDataReader(sql, parameters);
        //    List<RoadFlow.Data.Model.WorkFlowTask> List = DataReaderToList(dataReader);
        //    dataReader.Close();
        //    return List;
        //}


        ///// <summary>
        ///// 得到一个实例的任务
        ///// </summary>
        ///// <param name="flowID"></param>
        ///// <param name="groupID"></param>
        ///// <returns></returns>
        //public List<RoadFlow.Data.Model.WorkFlowTask> GetTaskList(Guid flowID, Guid groupID)
        //{
        //    string sql = string.Empty;
        //    SqlParameter[] parameters;
        //    if (flowID == null || flowID.IsEmptyGuid())
        //    {
        //        sql = "SELECT * FROM WorkFlowTask WHERE GroupID=@GroupID";
        //        parameters = new SqlParameter[]{
        //            new SqlParameter("@GroupID", SqlDbType.UniqueIdentifier){ Value = groupID }
        //        };
        //    }
        //    else
        //    {
        //        sql = "SELECT * FROM WorkFlowTask WHERE FlowID=@FlowID AND GroupID=@GroupID";
        //        parameters = new SqlParameter[]{
        //            new SqlParameter("@FlowID", SqlDbType.UniqueIdentifier){ Value = flowID },
        //            new SqlParameter("@GroupID", SqlDbType.UniqueIdentifier){ Value = groupID }
        //        };
        //    }
        //    SqlDataReader dataReader = dbHelper.GetDataReader(sql, parameters);
        //    List<RoadFlow.Data.Model.WorkFlowTask> List = DataReaderToList(dataReader);
        //    dataReader.Close();
        //    return List;
        //}

        ///// <summary>
        ///// 得到和一个任务同级的任务
        ///// </summary>
        ///// <param name="taskID">任务ID</param>
        ///// <param name="isStepID">是否区分步骤ID，多步骤会签区分的是上一步骤ID</param>
        ///// <returns></returns>
        //public List<RoadFlow.Data.Model.WorkFlowTask> GetTaskList(Guid taskID, bool isStepID = true)
        //{
        //    var task = Get(taskID);
        //    if (task == null)
        //    {
        //        return new List<Model.WorkFlowTask>() { };
        //    }
        //    string sql = string.Format("SELECT * FROM WorkFlowTask WHERE PrevID=@PrevID AND {0}", isStepID ? "StepID=@StepID" : "PrevStepID=@StepID");
        //    SqlParameter[] parameters1 = new SqlParameter[]{
        //        new SqlParameter("@PrevID", SqlDbType.UniqueIdentifier){ Value = task.PrevID },
        //        new SqlParameter("@StepID", SqlDbType.UniqueIdentifier){ Value = isStepID ? task.StepID : task.PrevStepID }
        //    };
        //    SqlDataReader dataReader = dbHelper.GetDataReader(sql, parameters1);
        //    List<RoadFlow.Data.Model.WorkFlowTask> List = DataReaderToList(dataReader);
        //    dataReader.Close();
        //    return List;
        //}

        ///// <summary>
        ///// 得到一个任务的前一任务
        ///// </summary>
        ///// <param name="flowID"></param>
        ///// <param name="groupID"></param>
        ///// <returns></returns>
        //public List<Model.WorkFlowTask> GetPrevTaskList(Guid taskID)
        //{
        //    string sql = "SELECT * FROM WorkFlowTask WHERE ID=(SELECT PrevID FROM WorkFlowTask WHERE ID=@ID)";
        //    SqlParameter[] parameters = new SqlParameter[]{
        //        new SqlParameter("@ID", SqlDbType.UniqueIdentifier){ Value = taskID }
        //    };
        //    SqlDataReader dataReader = dbHelper.GetDataReader(sql, parameters);
        //    List<RoadFlow.Data.Model.WorkFlowTask> List = DataReaderToList(dataReader);
        //    dataReader.Close();
        //    return List;
        //}

        /// <summary>
        /// 获取指定任务的后续任务
        /// </summary>
        /// <param name="taskId">任务Id</param>
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


        ///// <summary>
        ///// 查询待办任务
        ///// </summary>
        ///// <param name="userID"></param>
        ///// <param name="pager"></param>
        ///// <param name="query"></param>
        ///// <param name="title"></param>
        ///// <param name="flowid"></param>
        ///// <param name="date1"></param>
        ///// <param name="date2"></param>
        ///// <param name="type">0待办 1已完成</param>
        ///// <returns></returns>
        //public List<RoadFlow.Data.Model.WorkFlowTask> GetTasks(Guid userID, out string pager, string query = "", string title = "", string flowid = "", string sender = "", string date1 = "", string date2 = "", int type = 0)
        //{
        //    List<SqlParameter> parList = new List<SqlParameter>();
        //    StringBuilder sql = new StringBuilder("SELECT *,ROW_NUMBER() OVER(ORDER BY " + (type == 0 ? "ReceiveTime DESC" : "CompletedTime1 DESC") + ") AS PagerAutoRowNumber FROM WorkFlowTask WHERE ReceiveID=@ReceiveID");
        //    sql.Append(type == 0 ? " AND Status IN(0,1)" : " AND Status IN(2,3)");
        //    parList.Add(new SqlParameter("@ReceiveID", SqlDbType.UniqueIdentifier) { Value = userID });
        //    if (!title.IsNullOrEmpty())
        //    {
        //        sql.Append(" AND CHARINDEX(@Title,Title)>0");
        //        parList.Add(new SqlParameter("@Title", SqlDbType.NVarChar, 2000) { Value = title });
        //    }
        //    if (flowid.IsGuid())
        //    {
        //        sql.Append(" AND FlowID=@FlowID");
        //        parList.Add(new SqlParameter("@FlowID", SqlDbType.UniqueIdentifier) { Value = flowid.ToGuid() });
        //    }
        //    else if (!flowid.IsNullOrEmpty() && flowid.IndexOf(',') >= 0)
        //    {
        //        sql.Append(" AND FlowID IN(" + RoadFlow.Utility.Tools.GetSqlInString(flowid) + ")");
        //    }
        //    if (sender.IsGuid())
        //    {
        //        sql.Append(" AND SenderID=@SenderID");
        //        parList.Add(new SqlParameter("@SenderID", SqlDbType.UniqueIdentifier) { Value = sender.ToGuid() });
        //    }
        //    if (date1.IsDateTime())
        //    {
        //        sql.Append(" AND ReceiveTime>=@ReceiveTime");
        //        parList.Add(new SqlParameter("@ReceiveTime", SqlDbType.DateTime) { Value = date1.ToDateTime().Date });
        //    }
        //    if (date2.IsDateTime())
        //    {
        //        sql.Append(" AND ReceiveTime<=@ReceiveTime1");
        //        parList.Add(new SqlParameter("@ReceiveTime1", SqlDbType.DateTime) { Value = date2.ToDateTime().AddDays(1).Date });
        //    }

        //    long count;
        //    int size = RoadFlow.Utility.Tools.GetPageSize();
        //    int number = RoadFlow.Utility.Tools.GetPageNumber();
        //    string sql1 = dbHelper.GetPaerSql(sql.ToString(), size, number, out count, parList.ToArray());
        //    pager = RoadFlow.Utility.Tools.GetPagerHtml(count, size, number, query);


        //    SqlDataReader dataReader = dbHelper.GetDataReader(sql1, parList.ToArray());
        //    List<RoadFlow.Data.Model.WorkFlowTask> List = DataReaderToList(dataReader);
        //    dataReader.Close();
        //    return List;
        //}

        ///// <summary>
        ///// 得到流程实例列表
        ///// </summary>
        ///// <param name="flowID"></param>
        ///// <param name="senderID"></param>
        ///// <param name="receiveID"></param>
        ///// <param name="pager"></param>
        ///// <param name="query"></param>
        ///// <param name="title"></param>
        ///// <param name="flowid"></param>
        ///// <param name="date1"></param>
        ///// <param name="date2"></param>
        ///// <param name="status">是否完成 0:全部 1:未完成 2:已完成</param>
        ///// <returns></returns>
        //public List<RoadFlow.Data.Model.WorkFlowTask> GetInstances(Guid[] flowID, Guid[] senderID, Guid[] receiveID, out string pager, string query = "", string title = "", string flowid = "", string date1 = "", string date2 = "", int status = 0)
        //{
        //    List<SqlParameter> parList = new List<SqlParameter>();
        //    StringBuilder sql = new StringBuilder(@"SELECT a.*,ROW_NUMBER() OVER(ORDER BY a.SenderTime DESC) AS PagerAutoRowNumber FROM WorkFlowTask a
        //        WHERE a.ID=(SELECT TOP 1 ID FROM WorkFlowTask WHERE FlowID=a.FlowID AND GroupID=a.GroupID ORDER BY Sort DESC)");

        //    if (status != 0)
        //    {
        //        if (status == 1)
        //        {
        //            sql.Append(" AND a.Status IN(0,1,5)");
        //        }
        //        else if (status == 2)
        //        {
        //            sql.Append(" AND a.Status IN(2,3,4)");
        //        }
        //    }

        //    if (flowID != null && flowID.Length > 0)
        //    {
        //        sql.Append(string.Format(" AND a.FlowID IN({0})", RoadFlow.Utility.Tools.GetSqlInString(flowID)));
        //    }
        //    if (senderID != null && senderID.Length > 0)
        //    {
        //        if (senderID.Length == 1)
        //        {
        //            sql.Append(" AND a.SenderID=@SenderID");
        //            parList.Add(new SqlParameter("@SenderID", SqlDbType.UniqueIdentifier) { Value = senderID[0] });
        //        }
        //        else
        //        {
        //            sql.Append(string.Format(" AND a.SenderID IN({0})", RoadFlow.Utility.Tools.GetSqlInString(senderID)));
        //        }
        //    }
        //    if (receiveID != null && receiveID.Length > 0)
        //    {
        //        if (senderID.Length == 1)
        //        {
        //            sql.Append(" AND a.ReceiveID=@ReceiveID");
        //            parList.Add(new SqlParameter("@ReceiveID", SqlDbType.UniqueIdentifier) { Value = receiveID[0] });
        //        }
        //        else
        //        {
        //            sql.Append(string.Format(" AND a.ReceiveID IN({0})", RoadFlow.Utility.Tools.GetSqlInString(receiveID)));
        //        }
        //    }
        //    if (!title.IsNullOrEmpty())
        //    {
        //        sql.Append(" AND CHARINDEX(@Title,a.Title)>0");
        //        parList.Add(new SqlParameter("@Title", SqlDbType.NVarChar, 2000) { Value = title });
        //    }
        //    if (flowid.IsGuid())
        //    {
        //        sql.Append(" AND a.FlowID=@FlowID");
        //        parList.Add(new SqlParameter("@FlowID", SqlDbType.UniqueIdentifier) { Value = flowid.ToGuid() });
        //    }
        //    else if (!flowid.IsNullOrEmpty() && flowid.IndexOf(',') >= 0)
        //    {
        //        sql.Append(" AND a.FlowID IN(" + RoadFlow.Utility.Tools.GetSqlInString(flowid) + ")");
        //    }
        //    if (date1.IsDateTime())
        //    {
        //        sql.Append(" AND a.SenderTime>=@SenderTime");
        //        parList.Add(new SqlParameter("@SenderTime", SqlDbType.DateTime) { Value = date1.ToDateTime().Date });
        //    }
        //    if (date2.IsDateTime())
        //    {
        //        sql.Append(" AND a.SenderTime<=@SenderTime1");
        //        parList.Add(new SqlParameter("@SenderTime1", SqlDbType.DateTime) { Value = date1.ToDateTime().AddDays(1).Date });
        //    }

        //    long count;
        //    int size = RoadFlow.Utility.Tools.GetPageSize();
        //    int number = RoadFlow.Utility.Tools.GetPageNumber();
        //    string sql1 = dbHelper.GetPaerSql(sql.ToString(), size, number, out count, parList.ToArray());
        //    pager = RoadFlow.Utility.Tools.GetPagerHtml(count, size, number, query);

        //    SqlDataReader dataReader = dbHelper.GetDataReader(sql1, parList.ToArray());
        //    List<RoadFlow.Data.Model.WorkFlowTask> List = DataReaderToList(dataReader);
        //    dataReader.Close();
        //    return List;
        //}


        /*
            /// <summary>
            /// 获取启用的流程任务列表
            /// </summary>
            /// <returns>返回启用的流程任务列表</returns>
            public List<FlowTask> GetEnabledList()
            {
                var query = repos.NewQuery.Where(p => p.IsEnabled == true).OrderBy(p => p.Id);
                return repos.Query(query).ToList();
            }

            /// <summary>
            /// 获取流程任务DataTable
            /// </summary>
            /// <returns>返回流程任务DataTable</returns>
            public DataTable GetTable()
            {
                var query = repos.NewQuery.OrderBy(p => p.Id);
                return repos.GetTable(query);
            }


            /// <summary>
            /// 获取流程任务分页列表
            /// </summary>
            /// <param name="pageIndex">页面索引</param>
            /// <param name="pageSize">分页大小</param>
            /// <param name="orderName">排序列名</param>
            /// <param name="orderDir">排序方式</param>
            /// <param name="name">查询关键字</param>
            /// <returns>返回流程任务分页列表</returns>
            public PageList<FlowTask> GetPageList(int pageIndex, int pageSize, string orderName, string orderDir, string name)
            {
                orderName = orderName.IsEmpty() ? nameof(FlowTask.Id) : orderName;//默认使用主键排序
                orderDir = orderDir.IsEmpty() ? nameof(OrderDir.Desc) : orderDir;//默认使用倒序排序
                var query = repos.NewQuery.Take(pageSize).Page(pageIndex).OrderBy(orderName, orderDir.IsAsc());

                if (name.IsNotEmpty())
                {
                    name = name.Trim();
                    query.Where(p => p.Name.Contains(name));
                }

                return repos.Page(query);
            }
           */

        #region 私有方法


        #endregion
    }
}