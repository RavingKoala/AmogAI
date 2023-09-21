namespace AmogAI.FuzzyLogic;
public class FuzzyVariable {
	private readonly Dictionary<string, FuzzySet> _memberSets;
	private double _minRange = double.MaxValue;
	private double _maxRange = double.MinValue;

	public FuzzyVariable() { _memberSets = new Dictionary<string, FuzzySet>(); }

	/// <summary>
	/// Take the crisp value and calculate the DOM for each set in the variable
	/// </summary>
	/// <param name="val"></param>
	public void Fuzzify(double val) {
		// Make sure the value is within the bounds of this variable
		if (!(val >= _minRange && val <= _maxRange)) return;

		foreach (FuzzySet fuzzySet in _memberSets.Values) {
			fuzzySet.SetDOM(fuzzySet.CalculateDOM(val));
		}
	}

	/// <summary>
	/// Defuzzifies the value using MaxAv 
	/// 
	/// Output = sum (maxima * DOM) / sum (DOMs)
	/// </summary>
	/// <returns></returns>
	public double DeFuzzifyMaxAv() {
		double bottom = 0.0;
		double top = 0.0;

		foreach (FuzzySet fuzzySet in _memberSets.Values) {
			bottom += fuzzySet.GetDOM();

			top += fuzzySet.GetRepresentativeValue() * fuzzySet.GetDOM();
		}

		// Make sure bottom is not zero, to prevent a divide by zero error
		if (bottom == 0) return _minRange;

		return top / bottom;
	}

	/// <summary>
	/// Add triangular set to variable
	/// </summary>
	/// <param name="name">Name of variable</param>
	/// <param name="min">Minium value of set</param>
	/// <param name="peak">Peak of set</param>
	/// <param name="max">Maxmium value of set</param>
	/// <returns>Newly created set</returns>
	public FzSet AddTriangularSet(string name, double min, double peak, double max) {
		_memberSets.Add(name, new TriangleFuzzySet(peak, peak - min, max - peak));

		AdjustRangeToFit(min, max);

		return new FzSet(_memberSets[name]);

	}

	/// <summary>
	/// Add right shoulder set to variable
	/// </summary>
	/// <param name="name">Name of variable</param>
	/// <param name="min">Minium value of set</param>
	/// <param name="peak">Peak of set</param>
	/// <param name="max">Maxmium value of set</param>
	/// <returns>Newly created set</returns>
	public FzSet AddRightShoulderSet(string name, double min, double peak, double max) {
		_memberSets.Add(name, new RightShoulderFuzzySet(peak, peak - min, max - peak));

		AdjustRangeToFit(min, max);

		return new FzSet(_memberSets[name]);
	}

	/// <summary>
	/// Add left shoulder set to variable
	/// </summary>
	/// <param name="name">Name of variable</param>
	/// <param name="min">Minium value of set</param>
	/// <param name="peak">Peak of set</param>
	/// <param name="max">Maxmium value of set</param>
	/// <returns>Newly created set</returns>
	public FzSet AddLeftShoulderSet(string name, double min, double peak, double max) {
		_memberSets.Add(name, new LeftShoulderFuzzySet(peak, peak - min, max - peak));

		AdjustRangeToFit(min, max);

		return new FzSet(_memberSets[name]);
	}

	/// <summary>
	/// Adjust the upper and lower range of the variable each time a new set is added
	/// </summary>
	/// <param name="min">Min of new set</param>
	/// <param name="max">Max of new set</param>
	private void AdjustRangeToFit(double min, double max) {
		if (min < _minRange) _minRange = min;
		if (max > _maxRange) _maxRange = max;
	}
}
