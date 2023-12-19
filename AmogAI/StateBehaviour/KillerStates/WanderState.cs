namespace AmogAI.StateBehaviour.KillerStates;

using AmogAI.World.Entity;

public class WanderState : IState<Killer> {
    public void Enter(Killer killer) {

        killer.SteeringBehaviour.TurnOn(SteeringBehaviour.BehaviourType.Wander);
        killer.SteeringBehaviour.TurnOn(SteeringBehaviour.BehaviourType.WallAvoidance);
    }

    public void Execute(Killer killer, float timeDelta) {
        foreach (MovingEntity entity in killer.World.Survivors) {
            if (entity.GetType() != typeof(Survivor))
                continue;
            Survivor survivor = (Survivor)entity;

            if (killer.Position.Distance(survivor.Position) < killer.DetectionRadius) {
                killer.Target = survivor;
                killer.StateMachine.ChangeState(new KillState());
            }
        }
    }

    public void Exit(Killer killer) {
        killer.SteeringBehaviour.TurnOff(SteeringBehaviour.BehaviourType.Wander);
        killer.SteeringBehaviour.TurnOff(SteeringBehaviour.BehaviourType.WallAvoidance);
    }
}
