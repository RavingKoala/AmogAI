using AmogAI.World.Entity;

namespace AmogAI.FuzzyLogic {
    public class SurvivorTaskGoal {
        private readonly Person _survivor;

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

        private static double _timeWaited;
        private double _timeToWait;

        public SurvivorTaskGoal(Person survivor) {
			_survivor = survivor;
         
            InitFuzzy();
		}

        public void Process(float timeDelta, List<Objective> tasks) {
            // Keep track fo time waited
            _timeWaited += timeDelta;

            // If the agent has waited enough time move on to the next fish
            if (_timeWaited > _timeToWait) {
                _timeWaited = 0.0;
                _timeToWait = 0.0;

                Objective currentTask = tasks[0];

				// Fuzzyify fishquality and fishsize with the current fish
				_fm.Fuzzify("TaskDistance", (_survivor.Position - currentTask.Position).Length());
                _fm.Fuzzify("SurvivorHealth", _survivor.Health);

                // Defuzzify the current fish to get the cookingtime
                _timeToWait = _fm.DeFuzzify("TaskDistance", FuzzyModule.DefuzzifyMethod.max_av);
            }
        }

        public void Terminate() {}

        /// <summary>
        /// Init fuzzy moduls, fuzzysets and fuzzyrules
        /// </summary>
        private void InitFuzzy() {
            _fm = new FuzzyModule();
            FuzzyVariable TaskDistance = _fm.CreateFLV("TaskDistance");

			_taskDistanceClose = TaskDistance.AddLeftShoulderSet("TaskDistance_Close", 10, 40, 60);
			_taskDistanceMedium = TaskDistance.AddTriangularSet("TaskDistance_Medium", 40, 60, 80);
			_taskDistanceFar = TaskDistance.AddRightShoulderSet("TaskDistance_Far", 60, 80, 100);

            FuzzyVariable survivorHealth = _fm.CreateFLV("SurvivorHealth");

			_survivorHealthLow = survivorHealth.AddLeftShoulderSet("SurvivorHealth_Low", 0, 5, 40);
			_survivorHealthmedium = survivorHealth .AddTriangularSet("SurvivorHealth_Medium", 5, 40, 70);
			_survivorHealthHigh = survivorHealth .AddRightShoulderSet("SurvivorHealth_High", 40, 70, 100);

			FuzzyVariable desirability = _fm.CreateFLV("Desirability");

			_undisirable = desirability.AddLeftShoulderSet("Desirability_Low", 0, 25, 50);
            _desirable = desirability.AddTriangularSet("Desirability_Medium", 25, 50, 75);
            _veryDesirable = desirability.AddRightShoulderSet("Desirability_High", 50, 75, 100);

            _fm.AddRule(new FzAND(_taskDistanceClose, _survivorHealthLow), _undisirable);
            _fm.AddRule(new FzAND(_taskDistanceClose, _survivorHealthmedium), _undisirable);
            _fm.AddRule(new FzAND(_taskDistanceClose, _survivorHealthHigh), _desirable);

            _fm.AddRule(new FzAND(_taskDistanceMedium, _survivorHealthLow), _desirable);
            _fm.AddRule(new FzAND(_taskDistanceMedium, _survivorHealthmedium), _desirable);
            _fm.AddRule(new FzAND(_taskDistanceMedium, _survivorHealthHigh), _veryDesirable);

            _fm.AddRule(new FzAND(_taskDistanceFar, _survivorHealthLow), _desirable);
            _fm.AddRule(new FzAND(_taskDistanceFar, _survivorHealthmedium), _veryDesirable);
            _fm.AddRule(new FzAND(_taskDistanceFar, _survivorHealthHigh), _veryDesirable);
        }
    }
}
