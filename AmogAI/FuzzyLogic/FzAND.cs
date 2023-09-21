using System.Collections.Generic;

namespace AmogAI.FuzzyLogic; 
	public class FzAND : FuzzyTerm {
		private readonly List<FuzzyTerm> _terms = new List<FuzzyTerm>();

		public FzAND(FzAND fa) {
			foreach (FuzzyTerm ft in fa._terms) {
				fa._terms.Add(ft.Clone());
			}
		}

		public FzAND(FuzzyTerm ft1, FuzzyTerm ft2) {
			_terms.Add(ft1.Clone());
			_terms.Add(ft2.Clone());
		}

		public FzAND(FuzzyTerm ft1, FuzzyTerm ft2, FuzzyTerm ft3) {
			_terms.Add(ft1.Clone());
			_terms.Add(ft2.Clone());
			_terms.Add(ft3.Clone());

		}

		public FzAND(FuzzyTerm ft1, FuzzyTerm ft2, FuzzyTerm ft3, FuzzyTerm ft4) {
			_terms.Add(ft1.Clone());
			_terms.Add(ft2.Clone());
			_terms.Add(ft3.Clone());
			_terms.Add(ft4.Clone());

		}

		/// <summary>
		/// Return the Minimum DOM of two sets 
		/// </summary>
		/// <returns>Minium DOM</returns>
		public override double GetDOM() {
			double smallest = double.MaxValue;

			foreach (FuzzyTerm ft in _terms) {
				if (ft.GetDOM() < smallest) {
					smallest = ft.GetDOM();
				}
			}

			return smallest;
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
			return new FzAND(this);
		}
	}