
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Database
{

    public class Table
    {
        private static string baglantiStr;
        private static Table tableIntances = null;

        private Table(string baglanti)
        {
            baglantiStr = baglanti;
        }

        public static Table getTableInstances(string baglanti)
        {
            if (ReferenceEquals(tableIntances, null))
            {
                tableIntances = new Table(baglanti);
            }
            return tableIntances;
        }



        public bool SQLCalistir(SqlCommand command)
        {
            command.Connection = Utils.databaseBaglantisiGetir(baglantiStr);
            command.Connection.Open();
            command.ExecuteNonQuery();
            command.Connection.Close();
            command.Dispose();
            return true;
        }

        public bool SQLCalistir(string sql)
        {
            bool flag = false;
            sql = sqlInjectionTemizle(sql);
            SqlCommand command = new SqlCommand(sql, Utils.databaseBaglantisiGetir(baglantiStr));
            try
            {
                command.Connection.Open();
                command.ExecuteNonQuery();
                command.Connection.Close();
                command.Dispose();
                flag = true;
            }
            catch (Exception)
            {
                return flag;
            }
            return flag;
        }

        public DataTable veriTablosuGetir(string tabloAd)
        {
            DataTable table2;
            try
            {
                SqlDataAdapter adapter = DataAdapter.veriAdaptoruGetirTablo(tabloAd, baglantiStr);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                adapter.Dispose();
                table2 = dataTable;
            }
            catch (Exception exception1)
            {
                throw exception1;
            }
            return table2;
        }

        public DataTable veriTablosuGetir(string tabloAd, string[] colonlar)
        {
            DataTable table;
            try
            {
                tabloAd = tabloAd + " ORDER BY ";
                int index = 0;
                while (true)
                {
                    if (index >= colonlar.Length)
                    {
                        tabloAd = tabloAd.Substring(0, tabloAd.Length - 3);
                        table = this.veriTablosuGetir(tabloAd);
                        break;
                    }
                    tabloAd = tabloAd + colonlar[index] + ", ";
                    index++;
                }
            }
            catch (Exception exception1)
            {
                throw exception1;
            }
            return table;
        }

        public DataTable veriTablosuGetirSQL(string sql)
        {
            DataTable table2;
            try
            {
                SqlDataAdapter adapter = DataAdapter.veriAdaptoruGetir(sql, baglantiStr);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                adapter.Dispose();
                table2 = dataTable;
            }
            catch (Exception exception1)
            {
                throw exception1;
            }
            return table2;
        }

        public bool veriTablosuKaydet(DataTable dt, string tabloAd, string kullanici)
        {
            bool flag = false;
            try
            {
                SqlDataAdapter adapter = DataAdapter.veriAdaptoruGetirTablo(tabloAd, baglantiStr);
                adapter.Update(dt);
                adapter.Dispose();
                flag = true;
            }
            catch (Exception exception1)
            {
                throw exception1;
            }
            return flag;
        }
        public string sqlInjectionTemizle(string gelenveri)
        {

            if (gelenveri != null)
            {
                string gvCopy = gelenveri.ToLowerInvariant();
                string[,] arr = new string[,]
                {
                { "'", "´" },{ "%27", "´" }, { "union", "" }, { "select", "" }, { "update", "" }, { "insert", "" }, { "delete", "" }, { "drop", "" }, { "+", "" }, { "into", "" },{ "where", "" }, { "order by", "" }, { "chr", "" }, { "isnull", "" }, { "xtype", "" }, { "sysobject", "" }, { "syscolumns", "" },{ "convert", "" }, { "db_name", "" }, { "@@", "" }, { "@var", "" }, { "declare", "" }, { "execute", "" }, { "having", "" }, { "1=1", "" }, { "exec", "" }, { "cmdshell", "" }, { "master", "" }, { "cmd", "" }, { "--", "" }, { "xp_", "" }, { ";", "" },  { "#", "" }, { "%", "" }, { "(", "" }, { ")", "" }, { "/*", "" }, { "*/", "" }, { "<", "" }, { ">", "" }, { "[", "" }, { "]", "" }, { "\"", "" }, { "½", "" }, { "^", "" }, { "{", "" }, { "}", "" }, { "~", "" }, { "|", "" }, { "*", "" }
                };
                int abc = -1;
                for (int i = 0; i < arr.Length / 2; i++)
                {
                    abc = gvCopy.IndexOf(arr[i, 0]);
                    if (abc > -1)
                    {
                    bastan:
                        gelenveri = gelenveri.Substring(0, abc) + arr[i, 1] + gelenveri.Substring(abc + arr[i, 0].Length, gelenveri.Length - abc - arr[i, 0].Length);
                        gvCopy = gvCopy.Substring(0, abc) + arr[i, 1] + gvCopy.Substring(abc + arr[i, 0].Length, gvCopy.Length - abc - arr[i, 0].Length);
                        abc = gvCopy.IndexOf(arr[i, 0]);
                        if (abc > -1)
                        {
                            goto bastan;
                        }
                    }
                }
            }
            return gelenveri;
        }

    }
}
