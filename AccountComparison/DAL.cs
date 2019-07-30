using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountComparison
{
    public class DAL
    {
        private string conn { get; set; }

        public DAL(string _conn)
        {
            conn = _conn;
        }

        public void InsertData2DB(KeyValuePair<int, DataTable> dic)
        {
            string tabName = "difference_" + dic.Key;
            doBulkCopy(dic.Value, tabName);
        }


        private void doBulkCopy(DataTable dt, string tabName)
        {
            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(conn, SqlBulkCopyOptions.Default))
            {
                bulkCopy.BatchSize = dt.Rows.Count;
                bulkCopy.DestinationTableName = tabName;
                bulkCopy.WriteToServer(dt);
            }
        }
    }
}
