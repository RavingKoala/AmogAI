using AmogAI.World;
using AmogAI.World.Entity;

namespace AmogAI.FuzzyLogic {
    public class SurvivorTaskGoal {
        private readonly List<Person> _survivors;
        private readonly List<Objective> _tasks;

        private FuzzyModule _fm;

        private FzSet _taskDistanceClose;
        private FzSet _taskDistanceMedium;
        private FzSet _taskDistanceFar;

        private FzSet _survivorHealthLow;
        private FzSet _survivorHealthmedium;
        private FzSet _survivorHealthHigh;

        private FzSet _undisirable;
        private FzSet _desirable;
        private FzSet _veryDesirably;

        private static double _timeWaited;
        private double _timeToWait;

        public SurvivorTaskGoal(List<Person> survivors) {
			_survivors = survivors;
         
            InitFuzzy();
		}

        public void Process(float timeDelta) {
            // Keep track fo time waited
            _timeWaited += timeDelta;

            if (_survivors.Count > 0) {

                // If the agent has waited enough time move on to the next fish
                if (_timeWaited > _timeToWait) {
                    _timeWaited = 0.0;
                    _timeToWait = 0.0;

                    Person currentSurvivor = _survivors[0];
                    Objective currentTask = _tasks[0];

					// Fuzzyify fishquality and fishsize with the current fish
					_fm.Fuzzify("TaskDistance", (currentSurvivor.Position - currentTask.Position).Length());
                    _fm.Fuzzify("SurvivorHealth", currentSurvivor.Health);

                    // Defuzzify the current fish to get the cookingtime
                    _timeToWait = _fm.DeFuzzify("CookingTime", FuzzyModule.DefuzzifyMethod.max_av);

					_survivors.RemoveAt(0);
                }
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

			_survivorHealthLow = survivorHealth.AddLeftShoulderSet("SurvivorHealth_Low", 0, 5, 30);
			_survivorHealthmedium = survivorHealth .AddTriangularSet("SurvivorHealth_Medium", 5, 30, 40);
			_survivorHealthHigh = survivorHealth .AddRightShoulderSet("SurvivorHealth_High", 30, 40, 50);

			FuzzyVariable desirability = _fm.CreateFLV("Desirability");

			_undisirable = desirability.AddLeftShoulderSet("Desirability_Low", 5, 5, 15);
            _desirable = desirability.AddTriangularSet("Desirability_Medium", 5, 15, 40);
            _veryDesirably = desirability.AddRightShoulderSet("Desirability_High", 15, 40, 50);

            _fm.AddRule(new FzAND(_taskDistanceClose, _survivorHealthLow), _undisirable);
            _fm.AddRule(new FzAND(_taskDistanceClose, _survivorHealthmedium), _undisirable);
            _fm.AddRule(new FzAND(_taskDistanceClose, _survivorHealthHigh), _desirable);

            _fm.AddRule(new FzAND(_taskDistanceMedium, _survivorHealthLow), _desirable);
            _fm.AddRule(new FzAND(_taskDistanceMedium, _survivorHealthmedium), _desirable);
            _fm.AddRule(new FzAND(_taskDistanceMedium, _survivorHealthHigh), _veryDesirably);

            _fm.AddRule(new FzAND(_taskDistanceFar, _survivorHealthLow), _desirable);
            _fm.AddRule(new FzAND(_taskDistanceFar, _survivorHealthmedium), _veryDesirably);
            _fm.AddRule(new FzAND(_taskDistanceFar, _survivorHealthHigh), _veryDesirably);
        }
    }
}
