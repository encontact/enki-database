using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Restrictions {
	public class Equal : Restriction {

		public Equal() { }
		public Equal(Restriction left, Restriction right) {
			LeftHandSide = left;
			RightHandSide = right;
		}

		public override string Write(Writer writer) {
			return writer.Equal(LeftHandSide, RightHandSide);
		}
	}
}
