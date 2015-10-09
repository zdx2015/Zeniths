
using Newtonsoft.Json;
using Zeniths.MvcUtility;

namespace Zeniths.MvcUtility
{
    /// <summary>
    /// Json实体消息
    /// </summary>
    public class EntityMessage : JsonMessage
    {
        /// <summary>
        /// 管理的数据
        /// </summary>
        [JsonProperty("data")]
        public object Data { get; set; }

        /// <summary>
        /// 构造Json实体消息
        /// </summary>
        public EntityMessage()
        {

        }

        /// <summary>
        /// 构造Json实体消息
        /// </summary>
        /// <param name="entity">指定的实体对象</param>
        public EntityMessage(object entity)
            : this(true, null, entity)
        {

        }

        /// <summary>
        /// 构造Json实体消息
        /// </summary>
        /// <param name="success">是否获取成功</param>
        /// <param name="message">获取消息</param>
        public EntityMessage(bool success, string message)
            : this(success, message, null)
        {

        }

        /// <summary>
        /// 构造Json实体消息
        /// </summary>
        /// <param name="success">是否获取成功</param>
        /// <param name="message">获取消息</param>
        /// <param name="data">指定的实体对象</param>
        public EntityMessage(bool success, string message, object data)
        {
            this.Success = success;
            this.Message = message;
            this.Data = data;
        }

    }
}