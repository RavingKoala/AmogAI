namespace AmogAI.StateBehaviour;

using AmogAI.World.Entity;

public class SeekTaskState : IState<Survivor> {
    public void Enter(Survivor s) {
        // calculate desirability of tasks
    }

    public void Execute(Survivor s) {
        // look for a task to do depending on desirability
    }

    public void Exit(Survivor s) {
        // assign task -> transition to WalkTowardsTaskState
    }
}
