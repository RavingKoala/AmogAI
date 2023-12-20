namespace AmogAI.StateBehaviour.SurvivorStates;

using AmogAI.FuzzyLogic;
using AmogAI.SteeringBehaviour;
using AmogAI.World.Entity;

public class SeekTaskState : IState<Survivor> {
    public void Enter(Survivor survivor) {
        Console.WriteLine("Entering SeekTaskState");
        survivor.SteeringBehaviour.TurnOn(BehaviourType.Wander);
        survivor.SteeringBehaviour.TurnOn(BehaviourType.WallAvoidance);
    }

    public void Execute(Survivor survivor, float timeDelta) {
        survivor.SeekingForObjectiveTime += timeDelta;
        survivor.decisionIntervalDelta += timeDelta;

        if (survivor.SeekingForObjectiveTime >= survivor.SeekTimer) {
            if (survivor.decisionIntervalDelta >= Survivor.decisionInterval) {
                survivor.decisionIntervalDelta = 0;

                if (survivor.World.Objectives.Count(o => !o.IsDone) > 0) {
                    Objective objective = survivor.CalculateNearestObjective();
                    //float distanceKillerAndObjective = survivor.CalculateDistanceBetweenNearestKillerAndPotentialObjective(objective);
                    float distanceKillerAndObjective = 10000f;

                    float result = survivor.SurvivorTaskGoal.Process(objective, distanceKillerAndObjective);
                    Console.WriteLine(result);

                    if (result >= 60) {
                        survivor.SetObjective(objective);
                        survivor.ResetSeekingForObjectiveTime();
                        survivor.SurvivorStateMachine.StateMachine.ChangeState(new WalkTowardsTaskState());
                    }
                }
            }
        }
    }

    public void Exit(Survivor survivor) {
        survivor.SteeringBehaviour.TurnOff(BehaviourType.Wander);
    }
}
