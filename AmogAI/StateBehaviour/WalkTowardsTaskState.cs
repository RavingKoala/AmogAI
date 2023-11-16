namespace AmogAI.StateBehaviour;

using AmogAI.World.Entity;

public class WalkTowardsTaskState : IState<Survivor> {
    public void Enter(Survivor survivor) {
        survivor.SteeringBehaviour.WeightWallAvoidance = 0.01f; 

        // calculate shortest path to task and assign it to survivor
    }

    public void Execute(Survivor survivor) {
        // path towards task
        // if reached task -> transition to DoTaskState
    }

    public void Exit(Survivor survivor) {
        // ...
    }
}
