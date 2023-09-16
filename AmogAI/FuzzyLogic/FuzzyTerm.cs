namespace AAIProject.Source.Engine.AI.Fuzzy
{
    public abstract class FuzzyTerm
    {
        // Clone the fuzzyTerm
        public abstract FuzzyTerm Clone();
        // Get the DOM of the fuzzyTerm
        public abstract double GetDOM();
        // Clear the DOM, set the DOM to 0.0
        public abstract void ClearDOM();
        // Update DOM of consequent when a rule fires
        public abstract void ORwithDOM(double val);
    }
}
