namespace AmogAI.StateBehaviour;

// SeekTaskState
// WalkTowardsTaskState
// DoTaskState

public class TaskStateMachine<Survivor> : StateMachine<Survivor> {

    public TaskStateMachine(Survivor owner) : base(owner) {
    }

    public override void Update(float timeDelta) {
        if (CurrentState != null)
            CurrentState.Execute(Owner);
    }
}
