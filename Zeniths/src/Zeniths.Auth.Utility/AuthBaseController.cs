using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using Zeniths.Extensions;
using Zeniths.Helper;
using Zeniths.Auth.Entity;
using Zeniths.Auth.Service;
using Zeniths.MvcUtility;
using Zeniths.Utility;

namespace Zeniths.Auth.Utility
{
    public class AuthBaseController : JsonController
    {
        /// <summary>
        /// 当前登录用户主键
        /// </summary>
        public static int CurrentUserId
        {
            get { return CurrentUser.Id; }
        }

        /// <summary>
        /// 当前登录用户账号
        /// </summary>
        public static string CurrentUserAccount
        {
            get { return CurrentUser.Account; }
        }

        /// <summary>
        /// 当前登录用户姓名
        /// </summary>
        public static string CurrentUserName
        {
            get { return CurrentUser.Name; }
        }

        /// <summary>
        /// 当前登录用户对象
        /// </summary>
        public static SystemUser CurrentUser
        {
            get { return OrganizeHelper.GetLoginUser(); }
        }

        /// <summary>
        /// 当前登录用户部门主键
        /// </summary>
        public static int CurrentDepartmentId
        {
            get { return CurrentDepartment.Id; }
        }

        /// <summary>
        /// 当前登录用户部门名称
        /// </summary>
        public static string CurrentDepartmentName
        {
            get { return CurrentDepartment.Name; }
        }

        /// <summary>
        /// 当前登录用户部门对象
        /// </summary>
        public static SystemDepartment CurrentDepartment
        {
            get { return OrganizeHelper.GetLoginDepartment(); }
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="resourceName">资源名称</param>
        /// <param name="resourceId">资源主键</param>
        /// <returns>返回以上传并保存到数据库的文件列表</returns>
        protected List<SystemFile> UploadFileResource(string resourceName, string resourceId)
        {
            var service = new SystemFileService();
            var fileList = new List<SystemFile>();
            foreach (string key in Request.Files.Keys)
            {
                var file = Request.Files[key];
                if (file == null || file.ContentLength == 0) continue;
                var dir = Server.MapPath("~/UploadFiles/");
                string fileName = file.FileName.Replace("&", "");
                string name = Path.GetFileNameWithoutExtension(fileName).Replace("&", "");
                string ext = Path.GetExtension(fileName);
                if (string.IsNullOrEmpty(ext))
                {
                    ext = ".rar";
                    fileName += ext;
                }

                string filePath = Path.Combine(dir, fileName);
                if (System.IO.File.Exists(filePath))
                {
                    name = name + StringHelper.GetGuid();
                    filePath = Path.Combine(dir, name + ext);
                }
                var entity = new SystemFile();
                entity.Name = Path.GetFileName(fileName);
                entity.Url = "~/UploadFiles/" + name + ext;
                entity.Size = file.ContentLength;
                entity.CreateDateTime = DateTime.Now;
                entity.CreateUserId = CurrentUserId.ToString();
                entity.CreateUserName = CurrentUserName;
                entity.ResourceId = resourceId;
                entity.ResourceName = resourceName;
                fileList.Add(entity);
                FileHelper.CreateDirectoryByPath(filePath);
                file.SaveAs(filePath);
                service.Insert(entity);
            }

            return fileList;
        }

        /// <summary>
        /// 更新资源主键
        /// </summary>
        /// <param name="resourceName">资源名称</param>
        /// <param name="oldResourceId">旧资源主键</param>
        /// <param name="newResourceId">新资源主键</param>
        /// <returns>执行成功返回BoolMessage.True</returns>
        protected BoolMessage UpdateFileResourceId(string resourceName, string oldResourceId, string newResourceId)
        {
            var service = new SystemFileService();
            return service.UpdateResourceId(resourceName, oldResourceId, newResourceId);
        }

        /// <summary>
        /// 文件上传Action
        /// </summary>
        /// <param name="resName">资源名称</param>
        /// <param name="resId">资源主键,如果没有指定资源主键则自动生成,返回在结果中.资源主键字段名称是:resId</param>
        /// <returns>返回Json消息</returns>
        public ActionResult UploadFile(string resName, string resId)
        {
            if (string.IsNullOrEmpty(resName))
            {
                throw new ArgumentNullException(nameof(resName), "资源名称不能为空");
            }
            if (string.IsNullOrEmpty(resId))
            {
                resId = StringHelper.GetGuid();
            }
            UploadFileResource(resName, resId);
            return Json(new { success = true, message = "文件上传成功", resId = resId });
        }
    }
}