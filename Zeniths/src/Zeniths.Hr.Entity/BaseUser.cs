// ===============================================================================
//  Copyright (c) 2015 正得信集团股份有限公司
// ===============================================================================

using System;
using Zeniths.Entity;

namespace Zeniths.Hr.Entity
{
    /// <summary>
    /// 用户管理
    /// </summary>
    [Table("Base_User", Caption = "")]
    [PrimaryKey("Id", true)]
    public class BaseUser
    {
        /// <summary>
        /// 
        /// </summary>
        [Column(Caption = "")]
        public int Id { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        [Column(Caption = "")]
        public string UserName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(Caption = "")]
        public string RealName { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        [Column(Caption = "")]
        public int LogOnCount { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        [Column(Caption = "")]
        public int Enabled { get; set; }
         
        /// <summary>
        /// 
        /// </summary>
        [Column(Caption = "")]
        public int SortCode { get; set; }
    }
}
