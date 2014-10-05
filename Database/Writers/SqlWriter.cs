using Database.Restrictions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Writers {
	public class SqlWriter : Writer {
		/// <summary>
		/// Gera comando de select a ser aplicado.
		/// </summary>
		/// <param name="select"></param>
		/// <param name="from"></param>
		/// <param name="where"></param>
		/// <param name="groupBy"></param>
		/// <param name="orderBy"></param>
		/// <returns></returns>
		public override string Select(List<Column> select, List<Table> from, Restriction where, List<Column> groupBy, List<Tuple<Column, Order>> orderBy) {
			var s = "";
			var f = "";
			var g = "";
			var o = "";

			if (select == null || select.Count == 0) throw new Exception("Select columns are required.");
			if (from == null || from.Count == 0) throw new Exception("Select tables are required.");

			select.ForEach(col => {
				if (s != "") s += ",";
				if (!System.String.IsNullOrEmpty(col.Sql)) s += "(" + col.Sql + ")";
				else s += Field(col.Name);
				if (!System.String.IsNullOrEmpty(col.Alias)) s += " AS " + col.Alias;
			});

			from.ForEach(t => {
				if (f != "") f += ",";
				f += Field(t.Name);
				if (!System.String.IsNullOrEmpty(t.Alias)) f += " AS " + t.Alias;
			});

			if (groupBy != null) {
				groupBy.ForEach(c => {
					if (g != "") g += ",";
					g += Field(c.Name);
				});
			}

			if (orderBy != null) {
				orderBy.ForEach(t => {
					if (o != "") o += ",";
					o += Field(t.Item1.Name) + " " + t.Item2.ToString();
				});
			}

			if (g != "") g = " GROUP BY " + g;
			if (o != "") o = " ORDER BY " + o;
			return "SELECT " + s + " FROM " + f + " " + (where != null ? " WHERE " + where.Write(this) : "") + " " + g + " " + o;
		}

		public override string Delete(Table from, Restriction where) {
			return "DELETE FROM " + from.Name + " " + (where != null ? " WHERE " + where.Write(this) : "");
		}

		public override string Update(Table table, List<Equal> values, Restriction where) {
			var v = "";
			values.ForEach(col => {
				if (v != "") v += ",";
				v += col.Write(this);
			});
			return "UPDATE " + table.Name + " SET " + v + " " + (where != null ? " WHERE " + where.Write(this) : "");
		}

		public override string Insert(Table table, List<Tuple<Column, string>> values) {
			var c = "";
			var v = "";
			values.ForEach(t => {
				if (c != "") c += ",";
				if (v != "") v += ",";
				c += "[" + t.Item1.Name + "]";
				v += "'" + t.Item2.Replace("'", "''") + "'";
			});
			var insert = "INSERT INTO " + table.Name + " (" + c + ") VALUES (" + v + ")";
			switch(Database.DatabaseType){
				case DatabaseType.MsSql2008:
					insert += " SELECT SCOPE_IDENTITY(); ";
					break;
			}
			return insert;
		}

		public override string And(Restriction leftHandSide, Restriction rightHandSide) {
			return "(" + leftHandSide.Write(this) + " AND " + rightHandSide.Write(this) + ")";
		}

		public override string Or(Restriction leftHandSide, Restriction rightHandSide) {
			return "(" + leftHandSide.Write(this) + " OR " + rightHandSide.Write(this) + ")";
		}

		public override string Equal(Restriction leftHandSide, Restriction rightHandSide) {
			return leftHandSide.Write(this) + " = " + rightHandSide.Write(this);
		}

		public override string In(Restriction leftHandSide, Restriction rightHandSide) {
			return leftHandSide.Write(this) + " in (" + rightHandSide.Write(this) + ")";
		}

		public override string String(string value) {
			return "'" + value.Replace("'", "''") + "'";
		}

		public override string Field(string field) {
			return "[" + field + "]";
		}
	}
}
