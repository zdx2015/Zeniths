using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zeniths.Web.Areas.WorkFlow.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zeniths.Configuration;
using Zeniths.WorkFlow.Utility;

namespace Zeniths.Web.Areas.WorkFlow.Controllers.Tests
{
    [TestClass()]
    public class UnitTest1
    {
        [TestMethod()]
        public void SaveSettingTest()
        {
            //Dictionary<string, Zeniths.WorkFlow.Entity.Flow> dic =
            //    new Dictionary<string, Zeniths.WorkFlow.Entity.Flow>();
            //dic["test"] = new Zeniths.WorkFlow.Entity.Flow
            //{
            //    Name = "222",
            //    Category = "分组"
            //};
            //dic["到达点"] = new Zeniths.WorkFlow.Entity.Flow
            //{
            //    Name = "dddd",
            //    Category = "分sss组"
            //};
            //var str = JsonHelper.Serialize(dic, Newtonsoft.Json.Formatting.Indented);
            //File.WriteAllText("c:\\test.json",str);

            var path = "c:\\data.json";
            string json = File.ReadAllText(path);
            var obj = JsonHelper.Deserialize<WorkFlowDesign>(json);
            Assert.IsNotNull(obj);
        }
    }
}