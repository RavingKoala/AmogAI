namespace AmogAI.FuzzyLogic;

public class FuzzyRule
{
    private FuzzyTerm _antecedent;

    private FuzzyTerm _consequence;

    public FuzzyRule(FuzzyTerm ant, FuzzyTerm con)
    {
        _antecedent = ant;
        _consequence = con;
    }

    public void SetConfidencesOfConsequentToZero() { _consequence.ClearDOM(); }

    public void Calculate()
    {
        _consequence.ORwithDOM(_antecedent.GetDOM());
    }
}
