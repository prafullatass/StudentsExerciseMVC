using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace StudentExercisesMVC.Repositories
{
    public class Main
    {
        private static IConfiguration _config;

        public static void SetConfig(IConfiguration config)
        {
            _config = config;
        }

        public static SqlConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }

        public static DataSet ExecuteWithReader(string cmdText, SqlParameter[] sQLParameters)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = cmdText;
                    if(sQLParameters != null)
                        cmd.Parameters.AddRange(sQLParameters);

                    SqlDataAdapter reader = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    reader.Fill(ds);
                    return ds;
                }
            }
        }
    }
}
