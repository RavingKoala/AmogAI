using AAIProject.Source.Engine.AI.Fuzzy;
using AAIProject.Source.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace AAIProject.Source.Engine.AI.GoalDrivenBehaviours.AtomicGoals
{
    public class CookFishGoal : Goal
    {
        private readonly List<Fish> _fishToCook;

        private FuzzyModule _fm;

        private FzSet _qualityPoor;
        private FzSet _qualityOk;
        private FzSet _qualityGood;

        private FzSet _sizeSmall;
        private FzSet _sizeMedium;
        private FzSet _sizeLarge;

        private FzSet _cookingTimeShort;
        private FzSet _cookingTimeMedium;
        private FzSet _cookingTimeLong;

        private static double _timeWaited;
        private double _timeToWait;

        public CookFishGoal(Princess agent, string name) : base(agent, name)
        {
            _fishToCook = new List<Fish>();
        }

        public override void Activate()
        {
            Status = (int)Util.Status.Active;

            // Add three fish to be cooked
            while (_fishToCook.Count < 3)
            {
                _fishToCook.Add(new Fish());
            }

            InitFuzzy();
        }

        public override int Process()
        {
            ActivateIfInactive();

            // Keep track fo time waited
            _timeWaited += Game1.GameTime.ElapsedGameTime.TotalSeconds;

            if (_fishToCook.Count > 0)
            {

                // If the agent has waited enough time move on to the next fish
                if (_timeWaited > _timeToWait)
                {
                    _timeWaited = 0.0;
                    _timeToWait = 0.0;

                    Fish currentFish = _fishToCook[0];

                    // Fuzzyify fishquality and fishsize with the current fish
                    _fm.Fuzzify("FishQuality", currentFish.Quality);
                    _fm.Fuzzify("FishSize", currentFish.Size);

                    // Defuzzify the current fish to get the cookingtime
                    _timeToWait = _fm.DeFuzzify("CookingTime", FuzzyModule.DefuzzifyMethod.max_av);

                    _fishToCook.RemoveAt(0);
                }

            }
            else if (_timeWaited > _timeToWait)
            {
                Status = (int)Util.Status.Completed;
            }

            Name = $"Cook fish - {Math.Round(_timeToWait - _timeWaited, 2)}s";

            return Status;
        }

        public override void Terminate()
        {
            Status = (int)Util.Status.Inactive;
        }

        /// <summary>
        /// Init fuzzy moduls, fuzzysets and fuzzyrules
        /// </summary>
        private void InitFuzzy()
        {
            _fm = new FuzzyModule();
            FuzzyVariable fishQuality = _fm.CreateFLV("FishQuality");

            _qualityPoor = fishQuality.AddLeftShoulderSet("Quality_Poor", 10, 40, 60);
            _qualityOk = fishQuality.AddTriangularSet("Quality_Ok", 40, 60, 80);
            _qualityGood = fishQuality.AddRightShoulderSet("Quality_Good", 60, 80, 100);

            FuzzyVariable fishSize = _fm.CreateFLV("FishSize");

            _sizeSmall = fishSize.AddTriangularSet("Size_Small", 5, 5, 30);
            _sizeMedium = fishSize.AddTriangularSet("Size_Medium", 5, 30, 40);
            _sizeLarge = fishSize.AddRightShoulderSet("Size_Large", 30, 40, 50);

            FuzzyVariable cookingTime = _fm.CreateFLV("CookingTime");

            _cookingTimeShort = cookingTime.AddTriangularSet("Cooking_Time_Short", 5, 5, 15);
            _cookingTimeMedium = cookingTime.AddTriangularSet("Cooking_Time_Medium", 5, 15, 40);
            _cookingTimeLong = cookingTime.AddRightShoulderSet("Cooking_Time_Long", 15, 40, 50);

            _fm.AddRule(new FzAND(_qualityPoor, _sizeSmall), _cookingTimeShort);
            _fm.AddRule(new FzAND(_qualityPoor, _sizeMedium), _cookingTimeShort);
            _fm.AddRule(new FzAND(_qualityPoor, _sizeLarge), _cookingTimeMedium);

            _fm.AddRule(new FzAND(_qualityOk, _sizeSmall), _cookingTimeMedium);
            _fm.AddRule(new FzAND(_qualityOk, _sizeMedium), _cookingTimeMedium);
            _fm.AddRule(new FzAND(_qualityOk, _sizeLarge), _cookingTimeLong);

            _fm.AddRule(new FzAND(_qualityGood, _sizeSmall), _cookingTimeMedium);
            _fm.AddRule(new FzAND(_qualityGood, _sizeMedium), _cookingTimeLong);
            _fm.AddRule(new FzAND(_qualityGood, _sizeLarge), _cookingTimeLong);
        }
    }
}
