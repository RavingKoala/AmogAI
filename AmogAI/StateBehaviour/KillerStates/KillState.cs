namespace AmogAI.StateBehaviour.KillerStates;

using AmogAI.World.Entity;

public class KillState : IState<Killer> {
    public void Enter(Killer killer) {
        if (killer.Target == null)
            killer.StateMachine.ChangeState(new WanderState());

        killer.SteeringBehaviour.TurnOn(SteeringBehaviour.BehaviourType.Pursuit);
        killer.SteeringBehaviour.TurnOn(SteeringBehaviour.BehaviourType.WallAvoidance);

        killer.MaxSpeed += 0.02f;
    }

    public void Execute(Killer killer, float timeDelta) {
        if (killer.Target == null) {
            killer.StateMachine.ChangeState(new WanderState());
            return;
        }
    }

    public void Exit(Killer killer) {
        killer.Target = null;
        killer.MaxSpeed -= 0.01f;

        killer.SteeringBehaviour.TurnOff(SteeringBehaviour.BehaviourType.Pursuit);
        killer.SteeringBehaviour.TurnOff(SteeringBehaviour.BehaviourType.WallAvoidance);
    }
}
