﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Helper;
using Core.Enum;

namespace Core.Application
{
    public class ApplicationBase : IApplication
    {
        /// <summary>
        /// Dapper帮助类
        /// </summary>
        /// <param name="dbConnEnum"></param>
        /// <returns></returns>
        public DapperHelper Dapper(DbConnEnum dbConnEnum)
        {
            return new DapperHelper(dbConnEnum);
        }
    }
}
