namespace AmogAI.StateBehaviour.SurvivorStates;

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

        if (survivor.SeekingForObjectiveTime >= survivor.SeekTimer) {
            survivor.SetObjective(1);
            survivor.ResetSeekingForObjectiveTime();
            survivor.SurvivorStateMachine.StateMachine.ChangeState(new WalkTowardsTaskState());
        }
    }

    public void Exit(Survivor survivor) {
        survivor.SteeringBehaviour.TurnOff(BehaviourType.Wander);
    }
}
