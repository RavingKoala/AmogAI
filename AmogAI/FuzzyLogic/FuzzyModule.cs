namespace AmogAI.FuzzyLogic;

public class FuzzyModule {
	private readonly Dictionary<string, FuzzyVariable> _variables;
	private readonly List<FuzzyRule> _rules;

	public enum DefuzzifyMethod { max_av, centroid };

	public FuzzyModule() {
		_variables = new Dictionary<string, FuzzyVariable>();
		_rules = new List<FuzzyRule>();
	}
	/// <summary>
	/// Adds a rule to the module
	/// </summary>
	/// <param name="ant">Antecedent</param>
	/// <param name="con">Consequent</param>
	public void AddRule(FuzzyTerm ant, FuzzyTerm con) {
		_rules.Add(new FuzzyRule(ant, con));
	}

	/// <summary>
	/// Create a new empty fuzzy variable
	/// </summary>
	/// <param name="name">Name of FLV</param>
	/// <returns>New FLV</returns>
	public FuzzyVariable CreateFLV(string name) {
		_variables[name] = new FuzzyVariable();

		return _variables[name];
	}

	/// <summary>
	/// Call the fuzzify method of the given fuzzy variable
	/// </summary>
	/// <param name="name">Name of the FLV to fuzzify</param>
	/// <param name="val">Value to fuzzify with</param>
	public void Fuzzify(string name, double val) {
		if (!_variables.ContainsKey(name)) return;

		_variables[name].Fuzzify(val);
	}

	/// <summary>
	/// Defuzzify the given FLV
	/// </summary>
	/// <param name="name">Name of the FLV to defuzzify</param>
	/// <param name="method">Method to defuzzify with</param>
	/// <returns>Crisp value</returns>
	public double DeFuzzify(string name, DefuzzifyMethod method) {
		if (!_variables.ContainsKey(name)) return 0;

		SetConfidencesOfConsequentsToZero();

		foreach (FuzzyRule fuzzyRule in _rules) {
			fuzzyRule.Calculate();
		}

		return method switch {
			DefuzzifyMethod.max_av => _variables[name].DeFuzzifyMaxAv(),
			_ => 0,
		};
	}

	/// <summary>
	/// Set all the confidences to zero
	/// </summary>
	public void SetConfidencesOfConsequentsToZero() {
		foreach (FuzzyRule fuzzyRule in _rules) {
			fuzzyRule.SetConfidencesOfConsequentToZero();
		}
	}
}
