//// ===============================================================================
//// Copyright (c) 2015 正得信集团股份有限公司
//// ===============================================================================

//using System.Collections.Generic;
//using System.ComponentModel;

//namespace ZDX.Collections
//{
//    /// <summary>
//    /// 提供支持数据绑定的泛型集合
//    /// </summary>
//    /// <typeparam name="T">列表中元素的类型</typeparam>
//    public class BindingListEx<T> : BindingList<T> where T : class,new()
//    {
//        public BindingListEx()
//            : base()
//        {
//        }

//        public BindingListEx(IList<T> list)
//            : base(list)
//        {
//        }

//        public bool OnAddNewAsInsertFirst { get; set; }

//        protected override object AddNewCore()
//        {
//            if (OnAddNewAsInsertFirst)
//            {
//                AddingNewEventArgs e = new AddingNewEventArgs();
//                this.OnAddingNew(e);
//                object obj = e.NewObject ?? new T();
//                this.Insert(-1, obj as T);
//                return obj;
//            }
//            return base.AddNewCore();
//        }
//    }
//}