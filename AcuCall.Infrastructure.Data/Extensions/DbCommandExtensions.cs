using System;
using System.Data.Common;

namespace AcuCall.Infrastructure.Data.Extensions
{
    public static class DbCommandExtensions
    {
        public static DbCommand WithSqlParam(this DbCommand cmd, string paramName, object paramValue)
        {
            if (string.IsNullOrEmpty(cmd.CommandText))
                throw new InvalidOperationException("Call LoadStoredProc before using this method");

            var param = cmd.CreateParameter();
            param.ParameterName = paramName;
            param.Value = paramValue;
            cmd.Parameters.Add(param);
            return cmd;
        }
    }
}
