using System;
using System.Data.SqlClient;

namespace Database

{

    public class Utils
    {
        public static SqlConnection databaseBaglantisiGetir(string baglanti)
        {
            SqlConnection objA = new SqlConnection(baglanti);
            return (ReferenceEquals(objA, null) ? null : objA);
        }
    }
}


