using AmogAI.World.Entity;

namespace AmogAI.FuzzyLogic {
    public class SurvivorTaskGoal {
        private readonly Survivor _survivor;

        private FuzzyModule _fm;

        private FzSet _taskDistanceClose;
        private FzSet _taskDistanceMedium;
        private FzSet _taskDistanceFar;

        private FzSet _survivorHealthLow;
        private FzSet _survivorHealthmedium;
        private FzSet _survivorHealthHigh;

        private FzSet _undisirable;
        private FzSet _desirable;
        private FzSet _veryDesirable;

        public SurvivorTaskGoal(Survivor survivor) {
            _survivor = survivor;

            InitFuzzy();
        }

        public float Process(List<Objective> objectives) {
            Objective currentObjective = objectives[1];

            _fm.Fuzzify("TaskDistance", (_survivor.Position - currentObjective.Position).Length());
            _fm.Fuzzify("SurvivorHealth", _survivor.Health);

            return (float) _fm.DeFuzzify("Desirability", FuzzyModule.DefuzzifyMethod.max_av);
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
            _survivorHealthmedium = survivorHealth.AddTriangularSet("SurvivorHealth_Medium", 35, 50, 70);
            _survivorHealthHigh = survivorHealth.AddRightShoulderSet("SurvivorHealth_High", 50, 70, 100);

            FuzzyVariable desirability = _fm.CreateFLV("Desirability");

            _undisirable = desirability.AddLeftShoulderSet("Desirability_Low", 0, 20, 45);
            _desirable = desirability.AddTriangularSet("Desirability_Medium", 20, 45, 75);
            _veryDesirable = desirability.AddRightShoulderSet("Desirability_High", 45, 75, 100);

            _fm.AddRule(new FzAND(_taskDistanceFar, _survivorHealthLow), _undisirable);
            _fm.AddRule(new FzAND(_taskDistanceFar, _survivorHealthmedium), _undisirable);
            _fm.AddRule(new FzAND(_taskDistanceFar, _survivorHealthHigh), _desirable);

            _fm.AddRule(new FzAND(_taskDistanceMedium, _survivorHealthLow), _undisirable);
            _fm.AddRule(new FzAND(_taskDistanceMedium, _survivorHealthmedium), _desirable);
            _fm.AddRule(new FzAND(_taskDistanceMedium, _survivorHealthHigh), _veryDesirable);

            _fm.AddRule(new FzAND(_taskDistanceClose, _survivorHealthLow), _desirable);
            _fm.AddRule(new FzAND(_taskDistanceClose, _survivorHealthmedium), _veryDesirable);
            _fm.AddRule(new FzAND(_taskDistanceClose, _survivorHealthHigh), _veryDesirable);
        }
    }
}
