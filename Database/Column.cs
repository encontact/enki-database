using Database.Restrictions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database {
	public class Column {
		public string Name { get; set; }
		public string Alias { get; set; }
		public string Sql { get; set; }

		public Column() { }
		public Column(string name, string alias) {
			Name = name;
			Alias = alias ?? "";
		}
		public Column(string sqlCommand) {
			Sql = sqlCommand ?? "";
		}

		#region Conversão de Tipos
		public static implicit operator Field(Column c) {
			return c == null ? null : new Field {
				Value = c.Name
			};
		}
		#endregion
	}
}
