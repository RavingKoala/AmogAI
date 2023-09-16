using System;
using System.Collections.Generic;
using System.Text;

namespace AAIProject.Source.Engine.AI.Fuzzy
{
    public class FzFairly : FuzzyTerm
    {
        private readonly FuzzySet _set;

        public FzFairly(FzSet ft) { _set = ft.Set; }

        public override void ClearDOM()
        {
            _set.ClearDOM();
        }

        public override FuzzyTerm Clone()
        {
            return new FzFairly(new FzSet(_set));
        }

        public override double GetDOM()
        {
            return Math.Sqrt(_set.GetDOM());
        }

        public override void ORwithDOM(double val)
        {
            _set.ORwithDOM(Math.Sqrt(val));
        }
    }
}
