﻿using System;
using System.Collections.Generic;
using System.Data;
using AccountComparison;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        AccountingMatching accountingMatching = new AccountingMatching();

        [TestMethod]
        public void TestMethod123()
        {
            string[] keyFilids = new string[] { "mt","st"};
            DataTable dtA = new DataTable();
            dtA.Columns.Add("mt", typeof(int));
            dtA.Columns.Add("st", typeof(int));
            dtA.Columns.Add("col1", typeof(string));
            dtA.Columns.Add("col2", typeof(string));
            dtA.Columns.Add("col3", typeof(string));
            for (int m = 1; m <= 10; m++)
            {
                for (int s = 1; s <= 100; s++)
                {
                    DataRow dr = dtA.NewRow();
                    dr[0] = m;
                    dr[1] = s;
                    dr[2] = string.Format("col1-{0}-{1}", m, s);
                    dr[3] = string.Format("col2-{0}-{1}", m, s);
                    dr[4] = string.Format("col3-{0}-{1}", m, s);
                    dtA.Rows.Add(dr);
                }
            }
            var dtB = dtA.Copy();
            //1漏数据(A没 B有)
            DataRow drT = dtB.NewRow();
            drT[0] = 11;
            drT[1] = 1;
            drT[2] = string.Format("tttcol1-{0}-{1}", 11, 1);
            drT[3] = string.Format("tttcol2-{0}-{1}", 11, 1);
            drT[4] = string.Format("tttcol3-{0}-{1}", 11, 1);
            dtB.Rows.Add(drT);
            drT = dtB.NewRow();
            drT[0] = 11;
            drT[1] = 2;
            drT[2] = string.Format("tttcol1-{0}-{1}", 11, 2);
            drT[3] = string.Format("tttcol2-{0}-{1}", 11, 2);
            drT[4] = string.Format("tttcol3-{0}-{1}", 11, 2);
            dtB.Rows.Add(drT);

            //2数据多余(A有 B没)
            dtB.Rows.RemoveAt(0);
            dtB.Rows.RemoveAt(0);

            //3同主键不同内容
            dtB.Rows[12][2] = "aaaaaaaaaaaa";
            dtB.Rows[13][3] = "bbbbbbbbbbbb";
            dtB.Rows[14][4] = "cccccccccccc";

            var dicDifference = new Dictionary<int, DataTable>();
            accountingMatching.Match(dtA, dtB, keyFilids, ref dicDifference);
            //DataTable dtA, DataTable dtB, string[] keyFilids, ref Dictionary<int, DataTable> dicDifference
        }

        [TestMethod]
        public void TestMethod2()
        {
            string[] keyFilids = new string[] { "mt" };
            DataTable dtA = new DataTable();
            dtA.Columns.Add("mt", typeof(int));
            dtA.Columns.Add("col1", typeof(string));
            dtA.Columns.Add("col2", typeof(string));
            dtA.Columns.Add("col3", typeof(string));
            for (int m = 1; m <= 100; m++)
            {
                DataRow dr = dtA.NewRow();
                dr[0] = m;
                dr[1] = string.Format("col1-{0}", m);
                dr[2] = string.Format("col2-{0}", m);
                dr[3] = string.Format("col3-{0}", m);
                dtA.Rows.Add(dr);

            }
            var dtB = dtA.Copy();
            //1漏数据(A没 B有)
            DataRow drT = dtB.NewRow();
            drT[0] = 101;
            drT[1] = string.Format("tttcol1-{0}", 101);
            drT[2] = string.Format("tttcol2-{0}", 101);
            drT[3] = string.Format("tttcol3-{0}", 101);
            dtB.Rows.Add(drT);
            drT = dtB.NewRow();
            drT[0] = 102;
            drT[1] = string.Format("tttcol1-{0}", 102);
            drT[2] = string.Format("tttcol2-{0}", 102);
            drT[3] = string.Format("tttcol3-{0}", 102);
            dtB.Rows.Add(drT);

            //2数据多余(A有 B没)
            dtB.Rows.RemoveAt(0);
            dtB.Rows.RemoveAt(0);

            //3同主键不同内容
            dtB.Rows[11][1] = "aaaaaaaaaaaa";
            dtB.Rows[12][2] = "bbbbbbbbbbbb";
            dtB.Rows[13][3] = "cccccccccccc";

            var dicDifference = new Dictionary<int, DataTable>();
            accountingMatching.Match(dtA, dtB, keyFilids, ref dicDifference);
            //DataTable dtA, DataTable dtB, string[] keyFilids, ref Dictionary<int, DataTable> dicDifference
        }
    }
}

