namespace AmogAI.FuzzyLogic;

using AmogAI.World.Entity;

public class SurvivorTaskGoal {
    private readonly Survivor _survivor;

    private FuzzyModule _fm;

    private FzSet _taskDistanceClose;
    private FzSet _taskDistanceMedium;
    private FzSet _taskDistanceFar;

    private FzSet _survivorHealthLow;
    private FzSet _survivorHealthMedium;
    private FzSet _survivorHealthHigh;

    private FzSet _killerProximityToTaskClose;
    private FzSet _killerProximityToTaskMedium;
    private FzSet _killerProximityToTaskFar;

    private FzSet _undesirable;
    private FzSet _desirable;
    private FzSet _veryDesirable;

    public SurvivorTaskGoal(Survivor survivor) {
        _survivor = survivor;

        InitFuzzy();
    }

    public float Process(Objective objective, float distance) {
        _fm.Fuzzify("TaskDistance", (_survivor.Position - objective.Position).Length());
        _fm.Fuzzify("SurvivorHealth", _survivor.Health);
        _fm.Fuzzify("KillerProximityToTask", distance);

        return (float)_fm.DeFuzzify("Desirability", FuzzyModule.DefuzzifyMethod.max_av);
    }

    public void Terminate() { }

    /// <summary>
    /// Init fuzzy moduls, fuzzysets and fuzzyrules
    /// </summary>
    private void InitFuzzy() {
        _fm = new FuzzyModule();
        FuzzyVariable taskDistance = _fm.CreateFLV("TaskDistance");

        _taskDistanceClose = taskDistance.AddLeftShoulderSet("TaskDistance_Close", 0, 100, 350);
        _taskDistanceMedium = taskDistance.AddTriangularSet("TaskDistance_Medium", 100, 350, 800);
        _taskDistanceFar = taskDistance.AddRightShoulderSet("TaskDistance_Far", 350, 800, 10000);

        FuzzyVariable survivorHealth = _fm.CreateFLV("SurvivorHealth");

        _survivorHealthLow = survivorHealth.AddLeftShoulderSet("SurvivorHealth_Low", 0, 35, 50);
        _survivorHealthMedium = survivorHealth.AddTriangularSet("SurvivorHealth_Medium", 35, 50, 70);
        _survivorHealthHigh = survivorHealth.AddRightShoulderSet("SurvivorHealth_High", 50, 70, 100);

        FuzzyVariable killerProximityToTask = _fm.CreateFLV("KillerProximityToTask");

        _killerProximityToTaskClose = killerProximityToTask.AddLeftShoulderSet("KillerProximityToTask_Close", 0, 100, 200);
        _killerProximityToTaskMedium = killerProximityToTask.AddTriangularSet("KillerProximityToTask_Medium", 100, 200, 300);
        _killerProximityToTaskFar = killerProximityToTask.AddRightShoulderSet("KillerProximityToTask_Far", 200, 300, 10000);

        FuzzyVariable desirability = _fm.CreateFLV("Desirability");

        _undesirable = desirability.AddLeftShoulderSet("Desirability_Low", 0, 20, 45);
        _desirable = desirability.AddTriangularSet("Desirability_Medium", 20, 45, 75);
        _veryDesirable = desirability.AddRightShoulderSet("Desirability_High", 45, 75, 100);

        _fm.AddRule(new FzAND(_taskDistanceFar, _survivorHealthLow, _killerProximityToTaskClose), _undesirable);
        _fm.AddRule(new FzAND(_taskDistanceFar, _survivorHealthLow, _killerProximityToTaskMedium), _undesirable);
        _fm.AddRule(new FzAND(_taskDistanceFar, _survivorHealthLow, _killerProximityToTaskFar), _undesirable);
        _fm.AddRule(new FzAND(_taskDistanceFar, _survivorHealthMedium, _killerProximityToTaskClose), _undesirable);
        _fm.AddRule(new FzAND(_taskDistanceFar, _survivorHealthMedium, _killerProximityToTaskMedium), _undesirable);
        _fm.AddRule(new FzAND(_taskDistanceFar, _survivorHealthMedium, _killerProximityToTaskFar), _undesirable);
        _fm.AddRule(new FzAND(_taskDistanceFar, _survivorHealthHigh, _killerProximityToTaskClose), _undesirable);
        _fm.AddRule(new FzAND(_taskDistanceFar, _survivorHealthHigh, _killerProximityToTaskMedium), _desirable);
        _fm.AddRule(new FzAND(_taskDistanceFar, _survivorHealthHigh, _killerProximityToTaskFar), _desirable);

        _fm.AddRule(new FzAND(_taskDistanceMedium, _survivorHealthLow, _killerProximityToTaskClose), _undesirable);
        _fm.AddRule(new FzAND(_taskDistanceMedium, _survivorHealthLow, _killerProximityToTaskMedium), _undesirable);
        _fm.AddRule(new FzAND(_taskDistanceMedium, _survivorHealthLow, _killerProximityToTaskFar), _undesirable);
        _fm.AddRule(new FzAND(_taskDistanceMedium, _survivorHealthMedium, _killerProximityToTaskClose), _undesirable);
        _fm.AddRule(new FzAND(_taskDistanceMedium, _survivorHealthMedium, _killerProximityToTaskMedium), _undesirable);
        _fm.AddRule(new FzAND(_taskDistanceMedium, _survivorHealthMedium, _killerProximityToTaskFar), _desirable);
        _fm.AddRule(new FzAND(_taskDistanceMedium, _survivorHealthHigh, _killerProximityToTaskClose), _undesirable);
        _fm.AddRule(new FzAND(_taskDistanceMedium, _survivorHealthHigh, _killerProximityToTaskMedium), _desirable);
        _fm.AddRule(new FzAND(_taskDistanceMedium, _survivorHealthHigh, _killerProximityToTaskFar), _veryDesirable);

        _fm.AddRule(new FzAND(_taskDistanceClose, _survivorHealthLow, _killerProximityToTaskClose), _desirable);
        _fm.AddRule(new FzAND(_taskDistanceClose, _survivorHealthLow, _killerProximityToTaskMedium), _desirable);
        _fm.AddRule(new FzAND(_taskDistanceClose, _survivorHealthLow, _killerProximityToTaskFar), _desirable);
        _fm.AddRule(new FzAND(_taskDistanceClose, _survivorHealthMedium, _killerProximityToTaskClose), _desirable);
        _fm.AddRule(new FzAND(_taskDistanceClose, _survivorHealthMedium, _killerProximityToTaskMedium), _veryDesirable);
        _fm.AddRule(new FzAND(_taskDistanceClose, _survivorHealthMedium, _killerProximityToTaskFar), _veryDesirable);
        _fm.AddRule(new FzAND(_taskDistanceClose, _survivorHealthHigh, _killerProximityToTaskClose), _veryDesirable);
        _fm.AddRule(new FzAND(_taskDistanceClose, _survivorHealthHigh, _killerProximityToTaskMedium), _veryDesirable);
        _fm.AddRule(new FzAND(_taskDistanceClose, _survivorHealthHigh, _killerProximityToTaskFar), _veryDesirable);
    }
}
