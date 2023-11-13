namespace AmogAI.StateBehaviour;

using AmogAI.World.Entity;

public class SeekTaskState : IState<Survivor> {
    public void Enter(Survivor s) {
        // choose a task to do depending on desirability
    }

    public void Execute(Survivor s) {
        // a* to task
    }

    public void Exit(Survivor s) {
        
    }
}
