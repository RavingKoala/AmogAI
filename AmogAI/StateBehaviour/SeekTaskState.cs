namespace AmogAI.StateBehaviour;

using AmogAI.SteeringBehaviour;
using AmogAI.World.Entity;

public class SeekTaskState : IState<Survivor> {
    public void Enter(Survivor survivor) {
        survivor.SteeringBehaviour.TurnOn(BehaviourType.Wander);
        survivor.SteeringBehaviour.TurnOn(BehaviourType.WallAvoidance);

        // clear any previous tasks (if any)
        // clear any previous paths (if any)
    }

    public void Execute(Survivor survivor) {
        // wander for a while until desirability of a task is high enough to assign

        // survivor.SurvivorStateMachine.StateMachine.ChangeState(new WalkTowardsTaskState());
    }

    public void Exit(Survivor survivor) {
        survivor.SteeringBehaviour.TurnOff(BehaviourType.Wander);
    }
}
