using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zeniths.Auth.Entity;
using Zeniths.Auth.Service;
using Zeniths.Data;
using Zeniths.Data.Expressions;
using Zeniths.Data.Tests.Entity;
using Zeniths.Data.Extensions;
using Zeniths.Data.Utilities;
using Zeniths.Extensions;

namespace Zeniths.DataTests
{
    [TestClass()]
    public class SQLQueryTest
    {
        [TestMethod()]
        public void QueryTest()
        {
            SQLQuery<TestUser> query = new SQLQuery<TestUser>();
            var name = "admin@";
            var dts = "2015-01-01 22:55";
            //var sz = new[] {"5", "6"};
            //query.Where(p => p.Name == name.TrimEnd('@') && p.Id == 5);
            //query.Where(p => p.Name == null && p.Id == 5);
            //query.Where(p => p.Name.Contains(name.TrimEnd('@')) && p.Id == 5);
            //query.Where(p => p.Name.In(new[] { "5", "6" }));
            ;
            //System.Linq.Expressions.ConstantExpression.v
            //
            query.Where(p => p.Name.Between(9,10));
            query.Where("name=(select top 1 name form test)");
            query.Where(p => p.Name.In(new[] { "5", "6" }));
            query.Where("id=now()");
            query.Where(p => p.Name == name.TrimEnd('@') && p.CreateDateTime == dts.ToDateTime());
            //query.Select(p => p.Account);
            //query.Select(p => p.Name);
            //query.Select("deptId");
            //query.Select("deptName");
            query.ExcludeSelect(p => p.Id, p => p.Name);
            query.ExcludeSelect("Account","CreateDateTime");
            var result = query.ToResult();
            var parms = result.Parameters;
            var sql = result.ToSQL();
            Database db = new Database("auth");
            var repos = new Repository<TestUser>(db);
            //var dt = db.ExecuteDataTable(sql, parms);

            //var querySql = @"select LogDate, ShareUserId, ShareUserName, ShareDepartmentName, IsFeedback, FeedbackInfomation, FeedbackDateTime
            //from[Zeniths.HR].[dbo].[OAWorkLog], [Zeniths.HR].[dbo].[OAWorkLogShare]
            //where[Zeniths.HR].[dbo].[OAWorkLog].Id = [Zeniths.HR].[dbo].[OAWorkLogShare].WorkLogId and WorkLogId=@WorkLogId
            //Order by LogDate Desc";

            //
            //repos.Page(1, 10, querySql);

            //PagingHelper.SQLParts part;
            //PagingHelper.SplitSQL(querySql, out part);
            //sql = db.Provider.BuildPageQuery(10, 10, part);

            repos.UpdateExclude(new TestUser(),p=>p.Id==5,p=>p.Account,p=>p.Name);

        }

        [TestMethod()]
        public void SequenceTest()
        {
            var seqService = new SystemSequenceService();
            var v = seqService.GetNextValue("test");
        }

    }
}