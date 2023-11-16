namespace AmogAI.StateBehaviour;

using AmogAI.World.Entity;

// SeekTaskState
// WalkTowardsTaskState
// DoTaskState

public class TaskStateMachine: StateMachine<Survivor> {

    public TaskStateMachine(Survivor survivor) : base(survivor) {
        CurrentState = new SeekTaskState();
        CurrentState.Enter(survivor);
    }
}
