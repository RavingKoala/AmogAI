namespace AmogAI.StateBehaviour.KillerStates;

using AmogAI.World.Entity;

public class WanderState : IState<Killer> {
    public void Enter(Killer killer) {

        killer.SteeringBehaviour.TurnOn(SteeringBehaviour.BehaviourType.Wander);
    }

    public void Execute(Killer killer, float timeDelta) {
        
    }

    public void Exit(Killer killer) {
        killer.SteeringBehaviour.TurnOff(SteeringBehaviour.BehaviourType.Wander);
    }
}
