namespace AmogAI.StateBehaviour;

using AmogAI.World.Entity;

public class WalkTowardsTaskState<Survivor> : IState<Survivor> {
    public void Enter(Survivor s) {
        // calculate shortest path to task
    }

    public void Execute(Survivor s) {
        // walk towards task
    }

    public void Exit(Survivor s) {
        // start doing task -> transition to DoTaskState
    }
}
