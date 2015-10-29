using System;
using Zeniths.Entity;

namespace Zeniths.Auth.Entity
{
    public class SystemDictionaryDetailExtend: SystemDictionaryDetails
    {
        /// <summary>
        /// 分类组
        /// </summary>
        [Column(Caption = "分类组")]
        public string Category { get; set; }
       
    }
}
