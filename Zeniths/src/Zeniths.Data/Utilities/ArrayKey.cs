﻿// ===============================================================================
// Copyright (c) 2015 正得信集团股份有限公司
// ===============================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zeniths.Data.Utilities
{
	class ArrayKey<T>
	{
		public ArrayKey(T[] keys)
		{
			// Store the keys
			_keys = keys;

			// Calculate the hashcode
			_hashCode = 17;
			foreach (var k in keys)
			{
				_hashCode = _hashCode * 23 + (k==null ? 0 : k.GetHashCode());
			}
		}

		T[] _keys;
		int _hashCode;

		bool Equals(ArrayKey<T> other)
		{
			if (other == null)
				return false;

			if (other._hashCode != _hashCode)
				return false;

			if (other._keys.Length != _keys.Length)
				return false;

			for (int i = 0; i < _keys.Length; i++)
			{
				if (!object.Equals(_keys[i], other._keys[i]))
					return false;
			}

			return true;
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as ArrayKey<T>);
		}

		public override int GetHashCode()
		{
			return _hashCode;
		}

	}
}
