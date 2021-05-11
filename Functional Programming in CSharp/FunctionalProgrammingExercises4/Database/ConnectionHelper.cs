using Microsoft.Data.SqlClient;
using System;
using System.Data;
using static LaYumba.Functional.F;

namespace Repository
{
    public static class ConnectionHelper
    {
        public static R Connect<R> (string connString, Func<SqlConnection, R> func)
        {
            using (var conn = new SqlConnection(connString))
            {
                conn.Open();
                return func(conn);
            }
        }

        public static R Transact<R> (SqlConnection conn, Func<SqlTransaction, R> f)
        {
            R r = default(R);
            using (var tran = conn.BeginTransaction())
            {
                r = f(tran);
                tran.Commit();
            }
            return r;
        }
    }

    public static class ConnectionHelper_V2
    {
        public static R Connect<R>(string connString, Func<IDbConnection, R> func)
           => Using(new SqlConnection(connString)
              , conn => { conn.Open(); return func(conn); });
    }
}
