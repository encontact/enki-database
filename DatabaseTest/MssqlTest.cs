using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Database;
using System.Collections.Generic;
using Database.Restrictions;
using System.Data;

namespace DatabaseTest {
	[TestClass]
	public class MssqlTest {
		private string dbConnection = "server=127.0.0.1;user=sa;password=111lumma@;database=DBTESTE";

		public MssqlTest() {
			Database.Database.ConnectionString = dbConnection;
			Database.Database.DatabaseType = DatabaseType.MsSql2008;
		}

		[TestMethod]
		public void TestSimpleSelect() {
			////////////////////////////////////////////////////////////////////////////
			// Simple select one table with where
 			////////////////////////////////////////////////////////////////////////////
			// Columns
			var NameColumn = new Column() { Name = "name" };
			var SelectColumns = new List<Column>() { NameColumn };
			// Tables
			var Table = new Table() { Name = "SimpleTable" };
			var SelectTables = new List<Table>() { Table };
			// Where
			var where = new Equal() { LeftHandSide = new Field() { Value = "name" }, RightHandSide = new Value() { Value = "RegistroTeste1" } };
			var query = Database.Database.GetWriter().Select(SelectColumns, SelectTables, where, null, null);
			using (var ds = Database.Database.ExecuteDataSet(query, dbConnection)) {
				foreach (DataRow dr in ds.Tables[0].Rows) {
					Assert.AreEqual("RegistroTeste1", (string)dr["name"], "Erro ao recuperar select completo.");
				}
			}
		}

		[TestMethod]
		public void TestSimpleInsert() {
			//////////////////////////////////////////////////////////////
			// INSERT with Identity
			/////////////////////////////////////////////////////////////
			// Columns
			var NameColumn = new Column() { Name = "name" };
			var InsertColumns = new List<Tuple<Column, string>>() { new Tuple<Column, string>(NameColumn, "TesteInsert") };
			// Tables
			var Table = new Table() { Name = "SimpleTable" };
			var query = Database.Database.GetWriter().Insert(Table, InsertColumns);
			var lastId = Database.Database.ExecuteScalar(query);
			var realLastId = Database.Database.ExecuteScalar("Select max(id) from SimpleTable");
			Assert.AreEqual(realLastId, lastId, "Erro ao recuperar identity.");
			Database.Database.ExecuteNonQuery("delete from SimpleTable where name = 'TesteInsert'");
		}

		[TestMethod]
		public void TestSimpleUpdate() {
			//////////////////////////////////////////////////////////////
			// Update column with where
			/////////////////////////////////////////////////////////////
			// Columns
			var NameColumn = new Column() { Name = "name" };
			var CommentColumn = new Column() { Name = "comment" };
			var UpdateColumns = new List<Equal>() { new Equal(CommentColumn, new Value("")) };
			// Tables
			var Table = new Table() { Name = "SimpleTable" };
			var query = Database.Database.GetWriter().Update(Table, UpdateColumns, new Equal(NameColumn, new Value("RegistroTeste1")));
			Database.Database.ExecuteNonQuery(query);
			using (var ds = Database.Database.ExecuteDataSet("select Comment from SimpleTable where name = 'RegistroTeste1'", dbConnection)) {
				foreach (DataRow dr in ds.Tables[0].Rows) {
					Assert.AreEqual("", (string)dr["Comment"], "Erro ao recuperar select completo.");
				}
			}
			Database.Database.ExecuteNonQuery("Update SimpleTable set comment = 'Registro de teste simples versão 1.' where name = 'RegistroTeste1'");
		}
	}
}
