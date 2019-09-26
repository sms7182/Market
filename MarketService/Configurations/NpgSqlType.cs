using NHibernate;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.SqlTypes;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace MarketService.Configurations
{


    public class PostgreSQL83Dialect : PostgreSQL82Dialect
    {
        public PostgreSQL83Dialect()
        {
            RegisterColumnType(System.Data.DbType.Guid, "uuid");
        }
    }
    public class NpgSqlType : SqlType
     {

         public NpgsqlDbType NpgDbType { get; }

         public NpgSqlType(DbType dbType, NpgsqlDbType npgDbType)
             : base(dbType)
         {
             NpgDbType = npgDbType;
         }

         public NpgSqlType(DbType dbType, NpgsqlDbType npgDbType, int length)
             : base(dbType, length)
         {
             NpgDbType = npgDbType;
         }

         public NpgSqlType(DbType dbType, NpgsqlDbType npgDbType, byte precision, byte scale)
             : base(dbType, precision, scale)
         {
             NpgDbType = npgDbType;
         }

     }
     public class NpgSqlDriver : NpgsqlDriver
     {

         protected override void InitializeParameter(
           DbParameter dbParam,
             string name,
             SqlType sqlType
         )
         {
             if (sqlType is NpgSqlType && dbParam is NpgsqlParameter)
             {
                 this.InitializeParameter(
                     dbParam as NpgsqlParameter,
                     name,
                     sqlType as NpgSqlType
                 );
             }
             else
             {
                 base.InitializeParameter(dbParam, name, sqlType);
             }
         }

         protected virtual void InitializeParameter(
             NpgsqlParameter dbParam,
             string name,
             NpgSqlType sqlType
         )
         {
             if (sqlType == null)
             {
                 throw new QueryException($"No type assigned to parameter '{name}'");
             }
             dbParam.ParameterName = FormatNameForParameter(name);
             dbParam.DbType = sqlType.DbType;
             dbParam.NpgsqlDbType = sqlType.NpgDbType;
         }
     }
}
