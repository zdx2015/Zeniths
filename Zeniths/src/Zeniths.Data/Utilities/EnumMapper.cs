﻿// ===============================================================================
// Copyright (c) 2015 正得信集团股份有限公司
// ===============================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zeniths.Utility;

namespace Zeniths.Data.Utilities
{
	internal static class EnumMapper
	{
		public static object EnumFromString(Type enumType, string value)
		{
			Dictionary<string, object> map = _types.Get(enumType, () =>
			{
				var values = Enum.GetValues(enumType);

				var newmap = new Dictionary<string, object>(values.Length, StringComparer.InvariantCultureIgnoreCase);

				foreach (var v in values)
				{
					newmap.Add(v.ToString(), v);
				}

				return newmap;
			});


			return map[value];
		}

		static Cache<Type, Dictionary<string, object>> _types = new Cache<Type,Dictionary<string,object>>();
	}
}
