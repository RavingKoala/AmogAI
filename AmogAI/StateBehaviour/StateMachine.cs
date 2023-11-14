namespace AmogAI.StateBehaviour;

public abstract class StateMachine<T> {
    public T Owner { get; set; }
    public IState<T> CurrentState { get; set; }
    public IState<T> PreviousState { get; set; }

    public StateMachine(T owner) {
        CurrentState = null;
        PreviousState = null;
        Owner = owner;
    }

    public void Update(float timeDelta) {
        if (CurrentState != null)
            CurrentState.Execute(Owner);
    }

    public void ChangeState(IState<T> newState) {
        PreviousState = CurrentState;
        CurrentState.Exit(Owner);
        CurrentState = newState;
        CurrentState.Enter(Owner);
    }

    public void RevertToPreviousState() {
        ChangeState(PreviousState);
    }

    public bool IsInState(IState<T> state) {
        return CurrentState == state;
    }
}
