using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountComparison
{
    public class AccountingMatching
    {
        /// <summary>
        /// 识别差异数据
        /// </summary>
        /// <param name="dtA">数据A</param>
        /// <param name="dtB">数据B</param>
        /// <param name="keyFilids">主键集</param>
        /// <param name="dicDifference">差异数据1漏数据(A没 B有) 2数据多余(A有 B没) 3同主键不同内容</param>
        public void Match(DataTable dtA, DataTable dtB, string[] keyFilids, ref Dictionary<int, DataTable> dicDifference)
        {
            DataTable dtBCopy = dtB.Copy();
            DataTable dtDifference1 = dtA.Clone();
            DataTable dtDifference2 = dtA.Clone();
            DataTable dtDifference3 = dtA.Clone();
            string strWhere = string.Empty;

            foreach (DataRow drA in dtA.Rows)
            {
                strWhere = keyFilids[0] + "='" + drA[keyFilids[0]] + "'";

                for (int i = 1; i < keyFilids.Length; i++)
                {
                    strWhere += " AND " + keyFilids[i] + "='" + drA[keyFilids[i]] + "'";
                }
                var drB = dtBCopy.Select(strWhere);
                if (drB == null || drB.Count() == 0)
                {
                    dtDifference2.ImportRow(drA);
                }
                else
                {
                    for (int c = keyFilids.Length; c < dtA.Columns.Count; c++)
                    {
                        if (!drA[c].Equals(drB[0][c]))
                        {
                            dtDifference3.ImportRow(drA);
                            dtDifference3.ImportRow(drB[0]);
                            break;
                        }
                    }
                    dtBCopy.Rows.Remove(drB[0]);
                }
            }
            foreach (DataRow drT in dtBCopy.Rows)
            {
                dtDifference1.ImportRow(drT);
            }
            if (dtDifference1.Rows.Count > 0) { dicDifference[1] = dtDifference1; }
            if (dtDifference2.Rows.Count > 0) { dicDifference[2] = dtDifference2; }
            if (dtDifference3.Rows.Count > 0) { dicDifference[3] = dtDifference3; }
        }
    }
}
