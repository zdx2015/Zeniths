using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zeniths.Auth.Entity;
using Zeniths.Extensions;
using Zeniths.Helper;
using Zeniths.Utility;

namespace Zeniths.Auth.Service
{
    /// <summary>
    /// 上传文件服务
    /// </summary>
    public class SystemFileService
    {
        private readonly AuthRepository<SystemFile> repos = new AuthRepository<SystemFile>();

        /// <summary>
        /// 添加上传文件
        /// </summary>
        /// <param name="file">文件实体</param>
        public BoolMessage Insert(SystemFile file)
        {
            try
            {
                repos.Insert(file);
                return BoolMessage.True;
            }
            catch (Exception e)
            {
                return new BoolMessage(false, e.Message);
            }
        }

        /// <summary>
        /// 添加上传文件
        /// </summary>
        /// <param name="fileList">文件列表</param>
        public BoolMessage Insert(IEnumerable<SystemFile> fileList)
        {
            try
            {
                foreach (var item in fileList)
                {
                    repos.Insert(item);
                }
                return BoolMessage.True;
            }
            catch (Exception e)
            {
                return new BoolMessage(false, e.Message);
            }
        }

        /// <summary>
        /// 更新资源主键
        /// </summary>
        /// <param name="resourceName">资源名称</param>
        /// <param name="oldId">旧资源主键</param>
        /// <param name="newId">新资源主键</param>
        public BoolMessage UpdateResourceId(string resourceName,string oldId, string newId)
        {
            try
            {
                string sql = "UPDATE SystemFile SET ResourceId=@NewResourceId " +
                             "WHERE ResourceId=@OldResourceId AND ResourceName=@ResourceName";
                repos.Database.Execute(sql, new object[] { newId, oldId, resourceName });
                return BoolMessage.True;
            }
            catch (Exception e)
            {
                return new BoolMessage(false, e.Message);
            }
        }

        /// <summary>
        /// 删除上传文件
        /// </summary>
        /// <param name="resourceName">资源名称</param>
        /// <param name="resourceId">资源主键</param>
        public BoolMessage Delete(string resourceName, string resourceId)
        {
            try
            {
                repos.Delete(p => p.ResourceId == resourceId && p.ResourceName == resourceName);
                return BoolMessage.True;
            }
            catch (Exception e)
            {
                return new BoolMessage(false, e.Message);
            }
        }

        /// <summary>
        /// 删除上传文件
        /// </summary>
        /// <param name="fileIds">文件主键数组</param>
        public BoolMessage Delete(int[] fileIds)
        {
            try
            {
                foreach (var item in fileIds)
                {
                    var entity = repos.Get(item);
                    if (entity!=null && entity.Url.IsNotEmpty())
                    {
                        File.Delete(WebHelper.GetMapPath(entity.Url));
                    }
                    repos.Delete(item);
                }
                return BoolMessage.True;
            }
            catch (Exception e)
            {
                return new BoolMessage(false, e.Message);
            }
        }

        /// <summary>
        /// 获取上传文件对象
        /// </summary>
        /// <param name="fileId">上传文件主键</param>
        /// <returns>上传文件对象</returns>
        public SystemFile Get(int fileId)
        {
            return repos.Get(fileId);
        }

        /// <summary>
        /// 获取上传文件列表
        /// </summary>
        /// <param name="resourceName">资源名称</param>
        /// <param name="resourceId">资源主键</param>
        public List<SystemFile> GetList(string resourceName,string resourceId)
        {
            var query = repos.NewQuery.
                Where(p => p.ResourceId == resourceId && p.ResourceName == resourceName).
                OrderByDescending(p => p.CreateDateTime);
            return repos.Query(query).ToList();
        }
        
    }
}
