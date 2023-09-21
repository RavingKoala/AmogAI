using System;
using System.Collections.Generic;
using System.Text;

namespace AmogAI.FuzzyLogic; 
	public class FzVery : FuzzyTerm {
		private readonly FuzzySet _set;

		public FzVery(FzSet ft) { _set = ft.Set; }

		public override void ClearDOM() {
			_set.ClearDOM();
		}

		public override FuzzyTerm Clone() {
			return new FzVery(new FzSet(_set));
		}

		public override double GetDOM() {
			return _set.GetDOM() * _set.GetDOM();
		}

		public override void ORwithDOM(double val) {
			_set.ORwithDOM(val * val);
		}
	}
