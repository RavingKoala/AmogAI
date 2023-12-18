namespace AmogAI.StateBehaviour.KillerStates;

using AmogAI.SteeringBehaviour;
using AmogAI.World.Entity;

public class WanderState : IState<Killer> {
    public void Enter(Killer killer) {
        killer.SteeringBehaviour.TurnOn(BehaviourType.Wander);
        killer.SteeringBehaviour.TurnOn(BehaviourType.WallAvoidance);
    }

    public void Execute(Killer killer, float timeDelta) {
        foreach (MovingEntity entity in killer.World.Survivors) {
            if (entity.GetType() != typeof(Survivor))
                continue;
            Survivor survivor = (Survivor)entity;

            // Close proximity detection logic
            if (killer.Position.Distance(survivor.Position) < killer.DetectionRadius) {
                killer.Target = survivor;
                killer.StateMachine.ChangeState(new KillState());
            }

            // Cone detection logic
            (Vector point1, Vector point2) = killer.CalculateDetectionCone();
            // if any enemies in triangle (p1-p2-Position)
                // if != a wall inbetween
                    // set as target
        }
    }

    public void Exit(Killer killer) {
        killer.SteeringBehaviour.TurnOff(BehaviourType.Wander);
        killer.SteeringBehaviour.TurnOff(BehaviourType.WallAvoidance);
    }
}
