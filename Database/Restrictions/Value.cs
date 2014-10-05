using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Restrictions {
	public class Value : Restriction {
		public Value() { }
		public Value(string value) {
			Value = value;
		}

		public override string Write(Writer writer) {
			return writer.String(Value);
		}
	}
}
