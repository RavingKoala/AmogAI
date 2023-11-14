namespace AmogAI.StateBehaviour;

using AmogAI.World.Entity;

public class DoTaskState : IState<Survivor> {
    public void Enter(Survivor s) {
        // stop moving and start task
    }

    public void Execute(Survivor s) {
        // wait at task location until task is done (its timer is up)
    }

    public void Exit(Survivor s) {
        // transition to SeekTaskState
    }
}
