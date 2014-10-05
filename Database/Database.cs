using Database.Writers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database {
	public enum DatabaseType {
		MsSql2008
	}
	public static class Database {
		public static string ConnectionString { get; set; }
		public static DatabaseType DatabaseType { get; set; }

		static Database() {
			ConnectionString = "";
			DatabaseType = DatabaseType.MsSql2008;
		}

		public static void ExecuteNonQuery(string query, string connectionString = null) {
			using (var conn = GetConnection(connectionString)) {
				conn.Open();
				var cmd = conn.CreateCommand();
				cmd.CommandText = query;
				cmd.ExecuteNonQuery();
			}
		}

		public static int ExecuteScalar(string query, string connectionString = null) {
			using (var conn = GetConnection(connectionString)) {
				conn.Open();
				var cmd = conn.CreateCommand();
				cmd.CommandText = query;
				var result = cmd.ExecuteScalar();
				return result != null && result != DBNull.Value ? Convert.ToInt32(result) : 0;
			}
		}

		public static DataSet ExecuteDataSet(string query, string connectionString = null) {
			var ds = new DataSet();
			using (var conn = GetConnection(connectionString)) {
				conn.Open();
				using (var da = GetDataAdapter(query, conn)) {
					da.Fill(ds);
				}
			}
			return ds;
		}

		public static void ExecuteNonQuery(DbCommand cmd, string connectionString = null) {
			using (var conn = GetConnection(connectionString)) {
				conn.Open();
				cmd.Connection = conn;
				cmd.ExecuteNonQuery();
			}
		}

		public static int ExecuteScalar(DbCommand cmd, string connectionString = null) {
			using (var conn = GetConnection(connectionString)) {
				conn.Open();
				cmd.Connection = conn;
				var result = cmd.ExecuteScalar();
				return result != null && result != DBNull.Value ? (int)result : 0;
			}
		}

		public static DataSet ExecuteDataSet(DbCommand cmd, string connectionString = null) {
			var ds = new DataSet();
			using (var conn = GetConnection(connectionString)) {
				conn.Open();
				using (var da = GetDataAdapter(cmd.CommandText, conn)) {
					da.Fill(ds);
				}
			}
			return ds;
		}

		public static DataAdapter GetDataAdapter(string query, DbConnection conn) {
			switch (DatabaseType) {
				case DatabaseType.MsSql2008:
					return new SqlDataAdapter(query, (SqlConnection)conn);
				default:
					return null;
			}
		}

		public static DbConnection GetConnection(string connectionString) {
			switch (DatabaseType) {
				case DatabaseType.MsSql2008:
					return new SqlConnection(connectionString ?? ConnectionString);
				default:
					return null;
			}
		}

		public static Writer GetWriter() {
			switch (DatabaseType) {
				case DatabaseType.MsSql2008:
					return new SqlWriter();
				default:
					return null;
			}
		}
	}
}
