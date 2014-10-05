using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Restrictions {
	public class And : Restriction {
		public override string Write(Writer writer) {
			return writer.And(LeftHandSide, RightHandSide);
		}
	}
}
