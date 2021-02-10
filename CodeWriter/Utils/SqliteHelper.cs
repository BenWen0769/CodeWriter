using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Data.SQLite;
using System.Data;

namespace CodeWriter.Utils
{
    public static class SqliteHelper
    {
        public static string connStr;

        public static void InitDb()
        {
            if (!File.Exists(connStr))
            {
                SQLiteConnection.CreateFile(connStr);
                //连接数据库
                using (SQLiteConnection conn = new SQLiteConnection())
                {
                    SQLiteConnectionStringBuilder connstr = new SQLiteConnectionStringBuilder();
                    connstr.DataSource = connStr;
                    conn.ConnectionString = connstr.ToString();
                    conn.Open();
                    //创建表
                    SQLiteCommand cmd = new SQLiteCommand();
                    string sql = "CREATE TABLE datasource(name nvarchar(20),conn nvarchar(100))";
                    cmd.CommandText = sql;
                    cmd.Connection = conn;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void ExcuteSql(string sql)
        {
            using (SQLiteConnection conn = new SQLiteConnection())
            {
                SQLiteConnectionStringBuilder connstr = new SQLiteConnectionStringBuilder();
                connstr.DataSource = connStr;
                conn.ConnectionString = connstr.ToString();
                conn.Open();
                //插入数据
                SQLiteCommand cmd = new SQLiteCommand();
                cmd.CommandText = sql;
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
            }
        }

        public static void Query(string sql)
        {
            using (SQLiteConnection conn = new SQLiteConnection())
            {
                SQLiteConnectionStringBuilder connstr = new SQLiteConnectionStringBuilder();
                connstr.DataSource = connStr;
                conn.ConnectionString = connstr.ToString();
                conn.Open();
                //取出数据
                SQLiteCommand cmd = new SQLiteCommand();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                System.Data.SQLite.SQLiteDataReader reader = cmd.ExecuteReader();
                StringBuilder sb = new StringBuilder();
                while (reader.Read())
                {
                    sb.Append("username:").Append(reader.GetString(0)).Append("\n")
                    .Append("password:").Append(reader.GetString(1));
                }
            }
        }

        public static DataTable QueryTable(string sql)
        {
            using (SQLiteConnection conn = new SQLiteConnection())
            {
                SQLiteConnectionStringBuilder connstr = new SQLiteConnectionStringBuilder();
                connstr.DataSource = connStr;
                conn.ConnectionString = connstr.ToString();
                conn.Open();
                //取出数据
                SQLiteCommand cmd = new SQLiteCommand();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                SQLiteDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                return dt;
            }
        }


        public static IList<T> Query<T>(string sql) where T : class , new()
        {
            using (SQLiteConnection conn = new SQLiteConnection())
            {
                SQLiteConnectionStringBuilder connstr = new SQLiteConnectionStringBuilder();
                connstr.DataSource = connStr;
                conn.ConnectionString = connstr.ToString();
                conn.Open();
                //取出数据
                SQLiteCommand cmd = new SQLiteCommand();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                SQLiteDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                IList<T> rtn = new List<T>();
                Type type = typeof(T);
                int count = dt.Columns.Count;
                foreach (DataRow dr in dt.Rows)
                {
                    T obj = new T();
                    foreach (DataColumn field in dt.Columns)
                    {
                        type.GetProperty(field.ColumnName).SetValue(obj, dr[field.ColumnName]);
                    }
                    rtn.Add(obj);
                }
                return rtn;
            }
        }

    }
}
