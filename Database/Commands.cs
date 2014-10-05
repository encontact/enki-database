using Database.Restrictions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database {
	public enum Order {
		ASC,
		DESC
	}
	public static class Commands {
		public static DataSet Select(List<Column> select, List<Table> from, Restriction where, List<Column> groupBy, List<Tuple<Column, Order>> orderBy) {
			var writer = Database.GetWriter();
			return Database.ExecuteDataSet(writer.Select(select, from, where, groupBy, orderBy));
		}
		public static void Delete(Table from, Restriction where) {
			var writer = Database.GetWriter();
			Database.ExecuteDataSet(writer.Delete(from, where));
		}
		public static void Update(Table table, List<Equal> values, Restriction where) {
			var writer = Database.GetWriter();
			Database.ExecuteDataSet(writer.Update(table, values, where));
		}
		public static int Insert(Table table, List<Tuple<Column, string>> values) {
			var writer = Database.GetWriter();
			return Database.ExecuteScalar(writer.Insert(table, values));
		}
	}
}
