namespace AmogAI.StateBehaviour.KillerStates;

using AmogAI.SteeringBehaviour;
using AmogAI.World.Entity;

public class WanderState : IState<Killer> {
    public void Enter(Killer killer) {
        killer.SteeringBehaviour.TurnOn(BehaviourType.Wander);
        killer.SteeringBehaviour.TurnOn(BehaviourType.WallAvoidance);
    }

    public void Execute(Killer killer, float timeDelta) {
        foreach (Survivor survivor in killer.World.Survivors) {

            // Close proximity detection logic
            if (killer.Position.Distance(survivor.Position) < killer.DetectionRadius) {
                killer.Target = survivor;
                killer.StateMachine.ChangeState(new KillState());
            }
        }
    }

    public void Exit(Killer killer) {
        killer.SteeringBehaviour.TurnOff(BehaviourType.Wander);
        killer.SteeringBehaviour.TurnOff(BehaviourType.WallAvoidance);
    }
}
