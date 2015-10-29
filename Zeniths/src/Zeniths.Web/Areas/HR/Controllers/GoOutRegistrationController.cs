using System.Web.Mvc;
using Zeniths.Extensions;
using Zeniths.Helper;
using Zeniths.Hr.Entity;
using Zeniths.Hr.Service;
using Zeniths.MvcUtility;


namespace Zeniths.Web.Areas.HR.Controllers
{
    public class GoOutRegistrationController : JsonController
    {
        private readonly GoOutRegistrationService service = new GoOutRegistrationService();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Grid(string name)
        {
            var pageIndex = GetPageIndex();
            var pageSize = GetPageSize();
            var orderName = GetOrderName();
            var orderDir = GetOrderDir();
            var list = service.GetPageList(pageIndex, pageSize, orderName, orderDir, name);
            return View(list);
        }

        public ActionResult Create()
        {
            ViewBag.flag = 0;
            GoOutRegistration entity = new GoOutRegistration();
            entity.EmployeeName = "zhangsan";  //获取当前登录用户姓名
            return EditCore(entity, 0);  //new GoOutRegistration()
        }

        public ActionResult Edit(int id)
        {
            var entity = service.Get(id);
            return EditCore(entity, 1);
        }

        private ActionResult EditCore(GoOutRegistration entity, int flag)
        {
            ViewBag.flag = flag;
            return View("Edit", entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(GoOutRegistration entity)
        {
            //var hasResult = service.Exists(entity);
            //if (hasResult.Failure)
            //{
            //    return Json(hasResult);
            //}
            if (entity.Id == 0)
            {
                entity.ApplyTime = System.DateTime.Now;
            }
            var result = entity.Id == 0 ? service.Insert(entity) : service.Update(entity);
            return Json(result);
        }

        [HttpPost]
        public ActionResult Delete(string id)
        {
            var result = service.Delete(StringHelper.ConvertToArrayInt(id));
            return Json(result);
        }

        public ActionResult Details(string id)
        {
            var entity = service.Get(id.ToInt());
            return View(entity);
        }

        public ActionResult GoBack(string id)
        {
            var entity = service.Get(id.ToInt());
            return View(entity);
        }

        public ActionResult Export()
        {
            return Export(service.GetList());
        }

    }
}