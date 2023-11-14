namespace AmogAI.StateBehaviour;

using AmogAI.World.Entity;

// SeekTaskState
// WalkTowardsTaskState
// DoTaskState

public class TaskStateMachine<Survivor>: StateMachine<Survivor> {

    public TaskStateMachine(Survivor owner) : base(owner) {
        CurrentState = new SeekTaskState<Survivor>();
        CurrentState.Enter(owner);
    }
}
