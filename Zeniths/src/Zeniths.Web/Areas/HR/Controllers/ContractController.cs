// ===============================================================================
// 正得信股份 版权所有
// ===============================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zeniths.Hr.Entity;
using Zeniths.Hr.Service;
using Zeniths.Entity;
using Zeniths.Extensions;
using Zeniths.Helper;
using Zeniths.Utility;
using Zeniths.Hr.Utility;
using Zeniths.Auth.Utility;
namespace Zeniths.Web.Areas.Hr.Controllers
{
    /// <summary>
    /// 流程按钮控制器
    /// </summary>
    [Authorize]
    public class ContractController : HrBaseController
    {
        /// <summary>
        /// 服务对象
        /// </summary>
        private readonly ContractService service = new ContractService();

        /// <summary>
        /// 主视图
        /// </summary>
        /// <returns>视图模板</returns>
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult IndexComplated()
        {
            return View();
        }

        /// <summary>
        /// 表格视图
        /// </summary>
        /// <param name="name">按钮名称</param>
        /// <returns>视图模板</returns>
        public ActionResult Grid(string name, string SendDateTime, string SendDateTimeEnd, string state, string UndertakeDepartment, string sender)
        {
            var pageIndex = GetPageIndex();
            var pageSize = GetPageSize();
            var orderName = GetOrderName();
            var orderDir = GetOrderDir();
            var list = service.GetPageList(pageIndex, pageSize, orderName, orderDir, name, SendDateTime, SendDateTimeEnd, state, UndertakeDepartment, sender);
            return View(list);
        }
        /// <summary>
        /// 合同台帐视图
        /// </summary>
        /// <param name="name">查询条件</param>
        /// <returns>视图模板</returns>
        public ActionResult GridComplated(string name, string SendDateTime, string SendDateTimeEnd, string UndertakeDepartment, string sender)
        {
            var pageIndex = GetPageIndex();
            var pageSize = GetPageSize();
            var orderName = GetOrderName();
            var orderDir = GetOrderDir();
            var list = service.GetPageList_Complated(pageIndex, pageSize, orderName, orderDir, name, SendDateTime, SendDateTimeEnd, UndertakeDepartment, sender);
            return View(list);
        }

        /// <summary>
        /// 新增视图
        /// </summary>
        /// <returns>视图模板</returns>
        public ActionResult Create()
        {
            return EditCore(new Contract());
        }

        /// <summary>
        /// 编辑视图
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>视图模板</returns>
        public ActionResult Edit(string id)
        {
            var entity = service.Get(id.ToInt());
            return EditCore(entity);
        }


        /// <summary>
        /// 查看视图
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>视图模板</returns>
        public ActionResult Details(string id)
        {
            var entity = service.Get(id.ToInt());
            return View(entity);
        }

        /// <summary>
        /// 数据编辑视图
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>视图模板</returns>
        private ActionResult EditCore(Contract entity)
        {
            return View("Edit", entity);
        }
        /// <summary>
        /// 确定合同级别
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>视图模版</returns>
        public ActionResult EditLevel(string id)
        {
            var entity = service.Get(id.ToInt());
            return View("EditLevel", entity);
        }
        /// <summary>
        /// 确定合同级别
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>视图模板</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditLevelCore(Contract entity)
        {
            var currentUser = OrganizeHelper.GetCurrentUser();
            entity.LegalDepartmentManagerSetID = currentUser.Id;
            entity.LegalDepartmentManagerSetSignDateTime = DateTime.Now;
            entity.StepDateTime = DateTime.Now;//最后处理时间
            entity.StateId = "";
            entity.StateName = "";
            var result = service.UpdateContractLevel(entity);
            return Json(result);
            //return View("EditLevel", entity);
        }
        /// <summary>
        /// 合同审计基础信息View
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>视图模板</returns>
        public ActionResult EditAudit(string id)
        {
            var entity = service.Get(id.ToInt());
            return View("EditAudit", entity);
        }
        /// <summary>
        /// 董事会审批基础信息VIew
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult EditBoard(string id)
        {
            var entity = service.Get(id.ToInt());
            return View("EditBoard", entity);
        }
        /// <summary>
        /// 承办部门审核合同
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>视图模板</returns>
        [HttpPost]
        public ActionResult EditAuditCore_Undertake(Contract entity)
        {
            var currentUser = OrganizeHelper.GetCurrentUser();
            entity.UndertakeDepartmentManagerID = currentUser.Id;
            entity.UndertakeDepartmentManagerSign = currentUser.Name;
            entity.UndertakeDepartmentManagerSignDateTime = DateTime.Now;
            entity.UndertakeDepartmentManagerIsAudit = entity.UndertakeDepartmentManagerIsAudit;
            entity.UndertakeDepartmentManagerOpinion = entity.UndertakeDepartmentManagerOpinion;

            entity.StepDateTime = DateTime.Now;//最后处理时间

            var result = service.UpdateUndertake(entity);
            return Json(result);
        }
        /// <summary>
        /// 法务部审核合同
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditCore_Legal(Contract entity)
        {
            var currentUser = OrganizeHelper.GetCurrentUser();
            entity.LegalDepartmentManagerID = currentUser.Id;
            entity.LegalDepartmentManagerSign = currentUser.Name;
            entity.LegalDepartmentManagerSignDateTime = DateTime.Now;
            entity.LegalDepartmentManagerIsAudit = entity.LegalDepartmentManagerIsAudit;
            entity.LegalDepartmentManagerOpinion = entity.LegalDepartmentManagerOpinion;

            entity.StepDateTime = DateTime.Now;//最后处理时间

            var result = service.UpdateLegal(entity);
            return Json(result);
        }
        /// <summary>
        /// 财务部审核合同
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditCore_Financial(Contract entity)
        {
            var currentUser = OrganizeHelper.GetCurrentUser();
            entity.FinancialDepartmentManagerID = currentUser.Id;
            entity.FinancialDepartmentManagerSign = currentUser.Name;
            entity.FinancialDepartmentManagerSignDateTime = DateTime.Now;
            entity.FinancialDepartmentManagerIsAudit = entity.FinancialDepartmentManagerIsAudit;
            entity.FinancialDepartmentManagerOpinion = entity.FinancialDepartmentManagerOpinion;

            entity.StepDateTime = DateTime.Now;//最后处理时间

            var result = service.UpdateFinancial(entity);
            return Json(result);
        }
        /// <summary>
        /// 审计巡查部审核合同
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditCore_Audit(Contract entity)
        {
            var currentUser = OrganizeHelper.GetCurrentUser();
            entity.AuditDepartmentManagerID = currentUser.Id;
            entity.AuditDepartmentManagerSign = currentUser.Name;
            entity.AuditDepartmentManagerSignDateTime = DateTime.Now;
            entity.AuditDepartmentManagerIsAudit = entity.AuditDepartmentManagerIsAudit;
            entity.AuditDepartmentManagerOpinion = entity.AuditDepartmentManagerOpinion;

            entity.StepDateTime = DateTime.Now;//最后处理时间

            var result = service.UpdateAudit(entity);
            return Json(result);
        }
        /// <summary>
        /// 总经理审核合同
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditCore_General(Contract entity)
        {
            var currentUser = OrganizeHelper.GetCurrentUser();
            entity.GeneralManagerId = currentUser.Id;
            entity.GeneralManagerSign = currentUser.Name;
            entity.GeneralManagerSignDateTime = DateTime.Now;
            entity.GeneralManagerIsAudit = entity.GeneralManagerIsAudit;
            entity.GeneralManagerOpinion = entity.GeneralManagerOpinion;

            entity.StepDateTime = DateTime.Now;//最后处理时间

            var result = service.UpdateGeneral(entity);
            return Json(result);
        }
        /// <summary>
        /// 董事长审核合同
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditCore_Chairman(Contract entity)
        {
            var currentUser = OrganizeHelper.GetCurrentUser();
            entity.ChairmanId = currentUser.Id;
            entity.ChairmanSign = currentUser.Name;
            entity.ChairmanSignDateTime = DateTime.Now;
            entity.ChairmanIsAudit = entity.ChairmanIsAudit;
            entity.ChairmanOpinion = entity.ChairmanOpinion;

            entity.StepDateTime = DateTime.Now;//最后处理时间

            var result = service.UpdateChairman(entity);
            return Json(result);
        }
        /// <summary>
        /// 董事会审批
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>视图模板</returns>
        [HttpPost]
        public ActionResult EditBoardCore(Contract entity)
        {
            var currentUser = OrganizeHelper.GetCurrentUser();
            entity.BoardManagerID = currentUser.Id;
            entity.BoardManagerSign = currentUser.Name;
            entity.BoardManagerSignDateTime = DateTime.Now;
            entity.BoardManagerIsAudit = entity.BoardManagerIsAudit;
            entity.BoardManagerOpinion = entity.BoardManagerOpinion;

            entity.StepDateTime = DateTime.Now;//最后处理时间

            var result = service.UpdateChairman(entity);
            return Json(result);
        }
        /// <summary>
        /// 最终完善合同信息VIEW
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>视图模型</returns>
        public ActionResult EditComplete(string id)
        {
            var entity = service.Get(id.ToInt());
            return View("EditComplete", entity);
        }
        /// <summary>
        /// 最终完善合同信息
        /// </summary>
        /// <param name="entity">实体模型</param>
        /// <returns>视图模型</returns>
        [HttpPost]
        public ActionResult EditCompleteCore(Contract entity)
        {
            entity.StepDateTime = DateTime.Now;//最后处理时间

            var result = service.UpdateComplated(entity);
            return Json(result);
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>返回JsonMessage</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Contract entity)
        {
            //上传合同附件

            string type = "";
            if (Request.Form["Save"]!=null)
            {
                type = "1";
            }
            else
            {
                return Commit(entity);
            }

            if (entity.IsChange)
            {
                var haschangeno = service.ExistsContractNo(entity);//检测原合同编号是否符合要求
                if (haschangeno.Failure)
                {
                    return Json(haschangeno);
                }
            }
            var hasResult = service.Exists(entity);//存在返回true
            if (hasResult.Failure)
            {
                OrganizeHelper.SetCurrentUserCreateInfo(entity);
                entity.UndertakeDepartmentID = entity.CreateDepartmentId;//承办部门默认与起草人登陆用户所在部门相同
                entity.UndertakeDepartmentName = entity.CreateDepartmentName;
                entity.SenderID = entity.CreateUserId;//送审人默认与创建人相同
                entity.SenderName = entity.CreateUserName;
            }
            entity.StepDateTime = DateTime.Now;
            var result = entity.Id == 0 ? service.Insert(entity) : service.Update(entity);
            return Json(result);
        }
        /// <summary>
        /// 提交合同-保存后提交或者直接提交
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>返回JsonMessage</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Commit(Contract entity)
        {
            if (entity.IsChange)
            {
                var haschangeno = service.ExistsContractNo(entity);//检测原合同编号是否符合要求
                if (haschangeno.Failure)
                {
                    return Json(haschangeno);
                }
            }
            var hasResult = service.Exists(entity);
            if (hasResult.Failure)
            {
                OrganizeHelper.SetCurrentUserCreateInfo(entity);
                entity.UndertakeDepartmentID = entity.CreateDepartmentId;//承办部门默认与起草人登陆用户所在部门相同
                entity.UndertakeDepartmentName = entity.CreateDepartmentName;
                entity.SenderID = entity.CreateUserId;//送审人默认与创建人相同
                entity.SenderName = entity.CreateUserName;
                entity.SendDateTime = DateTime.Now;
                entity.StateId = "Auditing";
                entity.StateName = "审核中";
            }
            else//已经存在，修改状态和时间
            {
                entity.SendDateTime = DateTime.Now;
                entity.StateId = "Auditing";
                entity.StateName = "审核中";
            }

            entity.StepDateTime = DateTime.Now;//最后操作时间
            var result = entity.Id == 0 ? service.Insert(entity) : service.Update(entity);
            return Json(result);
        }
        /// <summary>
        /// 上传合同附件
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private bool UploadAttachment(string url)
        {
            return false; 
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>返回JsonMessage</returns>
        [HttpPost]
        public ActionResult Delete(string id)
        {
            var result = service.Delete(StringHelper.ConvertToArrayInt(id));
            return Json(result);
        }
        
        /// <summary>
        /// 数据导出
        /// </summary>
        /// <returns>返回文件下载流</returns>
        public ActionResult Export()
        {
            return Export(service.GetList());
        }
    }
}
