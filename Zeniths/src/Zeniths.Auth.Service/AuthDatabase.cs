﻿// ===============================================================================
// Copyright (c) 2015 正得信集团股份有限公司
// ===============================================================================

using Zeniths.Configuration;
using Zeniths.Data;

namespace Zeniths.Auth.Service
{
    public class AuthDatabase : Database
    {
        public AuthDatabase(): base("auth")
        {

        }
    }
}