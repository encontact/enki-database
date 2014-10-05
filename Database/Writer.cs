using Database.Restrictions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database {
	/// <summary>
	/// Classe de escrita de códigos SQL para o comando solicitado.
	/// </summary>
	public abstract class Writer {
		public abstract string Select(List<Column> select, List<Table> from, Restriction where, List<Column> groupBy, List<Tuple<Column, Order>> orderBy);
		public abstract string Delete(Table from, Restriction where);
		public abstract string Update(Table table, List<Equal> values, Restriction where);
		public abstract string Insert(Table table, List<Tuple<Column, string>> values);
		public abstract string And(Restriction leftHandSide, Restriction rightHandSide);
		public abstract string Or(Restriction leftHandSide, Restriction rightHandSide);
		public abstract string Equal(Restriction leftHandSide, Restriction rightHandSide);
		public abstract string In(Restriction leftHandSide, Restriction rightHandSide);
		public abstract string String(string value);
		public abstract string Field(string value);
	}
}
