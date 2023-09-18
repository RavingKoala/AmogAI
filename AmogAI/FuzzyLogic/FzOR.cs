using System.Collections.Generic;

namespace AmogAI.FuzzyLogic {
	public class FzOR : FuzzyTerm {
		private readonly List<FuzzyTerm> _terms = new List<FuzzyTerm>();

		public FzOR(FzOR fa) {
			foreach (FuzzyTerm ft in fa._terms) {
				fa._terms.Add(ft.Clone());
			}
		}

		public FzOR(FuzzyTerm ft1, FuzzyTerm ft2) {
			_terms.Add(ft1.Clone());
			_terms.Add(ft2.Clone());
		}

		public FzOR(FuzzyTerm ft1, FuzzyTerm ft2, FuzzyTerm ft3) {
			_terms.Add(ft1.Clone());
			_terms.Add(ft2.Clone());
			_terms.Add(ft3.Clone());

		}

		public FzOR(FuzzyTerm ft1, FuzzyTerm ft2, FuzzyTerm ft3, FuzzyTerm ft4) {
			_terms.Add(ft1.Clone());
			_terms.Add(ft2.Clone());
			_terms.Add(ft3.Clone());
			_terms.Add(ft4.Clone());

		}

		/// <summary>
		/// Get the Maxiumum DOM of the sets
		/// </summary>
		/// <returns>Maxiumum DOM</returns>
		public override double GetDOM() {
			double largest = float.MinValue;

			foreach (FuzzyTerm ft in _terms) {
				if (ft.GetDOM() > largest) {
					largest = ft.GetDOM();
				}
			}

			return largest;
		}

		public override void ORwithDOM(double val) {
			foreach (FuzzyTerm ft in _terms) {
				ft.ORwithDOM(val);
			}
		}

		public override void ClearDOM() {
			foreach (FuzzyTerm ft in _terms) {
				ft.ClearDOM();
			}
		}

		public override FuzzyTerm Clone() {
			return new FzOR(this);
		}
	}
}
