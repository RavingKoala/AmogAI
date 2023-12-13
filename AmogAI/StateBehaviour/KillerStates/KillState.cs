namespace AmogAI.StateBehaviour.KillerStates;

using AmogAI.World.Entity;

public class KillState : IState<Killer> {
    public void Enter(Killer killer) {
        if (killer.target == null)
            killer.stateMachine.ChangeState(new WanderState());

        killer.SteeringBehaviour.TurnOn(SteeringBehaviour.BehaviourType.Pursuit);
        killer.SteeringBehaviour.TurnOn(SteeringBehaviour.BehaviourType.WallAvoidance);

        killer.MaxSpeed += 0.04f;
    }

    public void Execute(Killer killer, float timeDelta) {
        if (killer.target == null) {
            killer.stateMachine.ChangeState(new WanderState());
            return;
        }

        // approach target
        // if near enough damage survivor

        if (killer.target.Health <= 0)
            killer.stateMachine.ChangeState(new WanderState());
    }

    public void Exit(Killer killer) {
        killer.target = null;
        killer.MaxSpeed -= 0.02f;

        killer.SteeringBehaviour.TurnOff(SteeringBehaviour.BehaviourType.Pursuit);
        killer.SteeringBehaviour.TurnOff(SteeringBehaviour.BehaviourType.WallAvoidance);
    }
}
