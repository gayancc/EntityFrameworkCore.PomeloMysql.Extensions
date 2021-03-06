﻿using System;
using System.Collections.Generic;
using System.Text;
using AltairCA.EntityFrameworkCore.PomeloMysql.Extensions.Attribute;
using AltairCA.EntityFrameworkCore.PomeloMysql.Extensions.Extensions;
using AltairCA.EntityFrameworkCore.PomeloMysql.Extensions.Functions;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace AltairCA.EntityFrameworkCore.PomeloMysql.Extensions.EfExtensions
{
    internal class AltairCADbContextOptionsExtension:IDbContextOptionsExtension
    {
        
        public AltairCADbContextOptionsExtension(string password)
        {
            AltairCaFunctionImplementation.Password = password.ToSha512();
        }
        private DbContextOptionsExtensionInfo _info;
        public void ApplyServices(IServiceCollection services)
        {
            services.AddSingleton<IMethodCallTranslatorProvider, AltairCaMySqlMethodCallTranslatorPlugin>();
        }

        public void Validate(IDbContextOptions options)
        {
            
        }

        public DbContextOptionsExtensionInfo Info
        {
            get
            {
                return this._info ?? (MyDbContextOptionsExtensionInfo)new MyDbContextOptionsExtensionInfo((IDbContextOptionsExtension)this);
            }
        }
        private sealed class MyDbContextOptionsExtensionInfo : DbContextOptionsExtensionInfo
        {
            public MyDbContextOptionsExtensionInfo(IDbContextOptionsExtension instance) : base(instance) { }

            public override bool IsDatabaseProvider => false;

            public override string LogFragment => "";

            public override void PopulateDebugInfo(IDictionary<string, string> debugInfo)
            {

            }

            public override long GetServiceProviderHashCode()
            {
                return 0;
            }
        }
    }
}
