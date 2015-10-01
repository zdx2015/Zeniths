// ===============================================================================
// Copyright (c) 2015 正得信集团股份有限公司
// ===============================================================================
using System;
using System.Collections.Generic;
using System.Threading;

namespace Zeniths.Utility
{
    /// <summary>
    /// 静态缓存管理(读写锁)
    /// </summary>
    /// <typeparam name="TKey">键类型</typeparam>
    /// <typeparam name="TValue">值类型</typeparam>
    public class Cache<TKey, TValue>
    {
        private readonly Dictionary<TKey, TValue> _map = new Dictionary<TKey, TValue>();
        private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();

        /// <summary>
        /// 获取缓存数量
        /// </summary>
        public int Count
        {
            get { return _map.Count; }
        }

        /// <summary>
        /// 获取缓存对象使用指定的键
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <returns>返回缓存值</returns>
        public TValue Get(TKey key)
        {
            _lock.EnterReadLock();
            TValue val;
            try
            {
                if (_map.TryGetValue(key, out val))
                    return val;
            }
            finally
            {
                _lock.ExitReadLock();
            }
            return default(TValue);
        }

        /// <summary>
        /// 获取缓存对象使用指定的键
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="factory">找不到缓存时创建函数</param>
        /// <returns>返回缓存值</returns>
        public TValue Get(TKey key, Func<TValue> factory)
        {
            _lock.EnterReadLock();
            TValue val;
            try
            {
                if (_map.TryGetValue(key, out val))
                    return val;
            }
            finally
            {
                _lock.ExitReadLock();
            }

            _lock.EnterWriteLock();
            try
            {
                if (_map.TryGetValue(key, out val))
                    return val;

                val = factory();
                _map.Add(key, val);
                return val;
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        /// <summary>
        /// 确定缓存中是否包含指定的键值
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <returns>存在返回True</returns>
        public bool Contains(TKey key)
        {
            _lock.EnterReadLock();
            try
            {
                return _map.ContainsKey(key);
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        /// <summary>
        /// 获取缓存对象使用指定的键
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存值</param>
        /// <returns>返回缓存值</returns>
        public void Set(TKey key, TValue value)
        {
            _lock.EnterWriteLock();
            try
            {
                _map[key] = value;
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <param name="key">缓存键</param>
        public void Remove(TKey key)
        {
            _lock.EnterWriteLock();
            try
            {
                _map.Remove(key);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        /// <summary>
        /// 清空缓存
        /// </summary>
        public void Clear()
        {
            _lock.EnterWriteLock();
            try
            {
                _map.Clear();
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }
    }
}
