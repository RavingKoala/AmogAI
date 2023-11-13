namespace AmogAI.StateBehaviour;

using AmogAI.World.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public abstract class StateMachine {
    public Survivor Owner { get; set; }
    public IState CurrentState { get; set; }
    public IState PreviousState { get; set; }
    public IState GlobalState { get; set; }

    public StateMachine(Survivor owner) {
        CurrentState = null;
        PreviousState = null;
        GlobalState = null;
        Owner = owner;
    }

    public abstract void Update(float timeDelta);

    public void ChangeState(IState newState) {
        PreviousState = CurrentState;
        CurrentState.Exit(Owner);
        CurrentState = newState;
        CurrentState.Enter(Owner);
    }

    public void SetCurrentState(IState state) {
        CurrentState = state;
    }

    public void SetGlobalState(IState state) {
        GlobalState = state;
    }

    public void SetPreviousState(IState state) {
        PreviousState = state;
    }

    public void RevertToPreviousState() {
        ChangeState(PreviousState);
    }

    public bool IsInState(IState state) {
        return CurrentState == state;
    }
}
