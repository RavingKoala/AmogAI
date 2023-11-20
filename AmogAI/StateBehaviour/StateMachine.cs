﻿namespace AmogAI.StateBehaviour;

public abstract class StateMachine<T> {
    public T Owner { get; set; }
    public IState<T> CurrentState { get; set; }

    public StateMachine(T owner) {
        CurrentState = null;
        Owner = owner;
    }

    public void Update(float timeDelta) {
        if (CurrentState != null)
            CurrentState.Execute(Owner);
    }

    public void ChangeState(IState<T> newState) {
        CurrentState.Exit(Owner);
        CurrentState = newState;
        CurrentState.Enter(Owner);
    }
}