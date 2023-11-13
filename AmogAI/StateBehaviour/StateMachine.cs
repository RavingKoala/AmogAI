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

    public abstract void Update(float timeDelta);

    public void ChangeState(IState<T> newState) {
        PreviousState = CurrentState;
        CurrentState.Exit(Owner);
        CurrentState = newState;
        CurrentState.Enter(Owner);
    }

    public void SetCurrentState(IState<T> state) {
        CurrentState = state;
    }

    public void SetPreviousState(IState<T> state) {
        PreviousState = state;
    }

    public void RevertToPreviousState() {
        ChangeState(PreviousState);
    }

    public bool IsInState(IState<T> state) {
        return CurrentState == state;
    }
}
