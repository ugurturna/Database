using System;
using System.Data.SqlClient;

namespace Database
{


    public class DataAdapter
    {
        private static SqlDataAdapter getDataAdapterWithSQL(string selectSQL, string deleteSQL, string updateSQL, string insertSQL, SqlConnection conn)
        {
            SqlDataAdapter adapter = new SqlDataAdapter
            {
                SelectCommand = new SqlCommand(selectSQL, conn),
                InsertCommand = new SqlCommand(insertSQL, conn),
                UpdateCommand = new SqlCommand(updateSQL, conn),
                DeleteCommand = new SqlCommand(deleteSQL, conn)
            };
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            return adapter;
        }

        public static SqlDataAdapter veriAdaptoruGetir(string sql, string baglanti)
        {
            SqlDataAdapter adapter = new SqlDataAdapter(sql, Utils.databaseBaglantisiGetir(baglanti));
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            return adapter;
        }

        public static SqlDataAdapter veriAdaptoruGetirTablo(string tabloAd, string baglanti) =>
            veriAdaptoruGetir("SELECT * FROM " + tabloAd, baglanti);
    }
}
