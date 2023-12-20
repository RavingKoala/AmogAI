namespace AmogAI.StateBehaviour.SurvivorStates;

using AmogAI.SteeringBehaviour;
using AmogAI.World.Entity;

public class SeekTaskState : IState<Survivor> {
    public void Enter(Survivor survivor) {
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
                    float distanceKillerAndObjective = survivor.CalculateDistanceBetweenNearestKillerAndPotentialObjective(objective);

                    survivor.Desirability = survivor.SurvivorTaskGoal.Process(objective, distanceKillerAndObjective);

                    if (survivor.Desirability >= Survivor.desirabilityThreshold && !objective.IsInProgress) {
                        survivor.SetObjective(objective);
                        survivor.ResetSeekingForObjectiveTime();
                        survivor.Desirability = 0;
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
