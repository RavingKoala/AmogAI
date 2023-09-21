namespace AmogAI.FuzzyLogic;
public abstract class FuzzySet {
	protected double DOM;
	protected double RepresentativeValue;

	public FuzzySet(double repVal) {
		DOM = 0.0;
		RepresentativeValue = repVal;
	}
	// Return the DOM in this set of the given value
	public abstract double CalculateDOM(double val);
	// If the fuzzy set is part of a consequent, and it is fired by a rule,
	// set the DOM to the maximum of the parameter value of the set's existing DOM value
	public void ORwithDOM(double val) { if (val > DOM) DOM = val; }

	// Accessor
	public double GetRepresentativeValue() { return RepresentativeValue; }
	// Reset the DOM
	public void ClearDOM() { DOM = 0.0; }
	// Acessor
	public double GetDOM() { return DOM; }
	public void SetDOM(double val) {
		if (!(val <= 1 && val >= 0)) return;
		DOM = val;
	}
}
