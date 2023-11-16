namespace AmogAI.StateBehaviour;

using AmogAI.World.Entity;

public class DoTaskState : IState<Survivor> {
    public void Enter(Survivor survivor) {
        // stop moving and start task
    }

    public void Execute(Survivor survivor) {
        // wait at task location until task is done (its timer is up)

        // task is done -> transition to SeekTaskState
        // survivor.SurvivorStateMachine.StateMachine.ChangeState(new SeekTaskState());
    }

    public void Exit(Survivor survivor) {
        // ... 
    }
}
