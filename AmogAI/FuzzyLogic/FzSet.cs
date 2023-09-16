namespace AAIProject.Source.Engine.AI.Fuzzy
{
    /// <summary>
    /// Proxy class for a fuzzy set.
    /// </summary>
    public class FzSet : FuzzyTerm
    {
        public FuzzySet Set;

        public FzSet(FuzzySet s) { Set = s; }

        public override FuzzyTerm Clone() { return new FzSet(Set); }
        public override void ClearDOM() { Set.ClearDOM(); }

        public override double GetDOM() { return Set.GetDOM(); }

        public override void ORwithDOM(double val) { Set.ORwithDOM(val); }
    }
}
