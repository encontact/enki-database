using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database {
	public abstract class Restriction {
		public Restriction LeftHandSide { get; set; }
		public Restriction RightHandSide { get; set; }
		public string Value { get; set; }

		public abstract string Write(Writer writer);
	}
}
