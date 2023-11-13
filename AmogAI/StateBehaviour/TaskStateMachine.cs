namespace AmogAI.StateBehaviour;

using AmogAI.World.Entity;

public class TaskStateMachine : StateMachine {

    public TaskStateMachine(Survivor owner) : base(owner) {
    }

    public override void Update(float timeDelta) {
        if (GlobalState != null)
            GlobalState.Execute(Owner);

        if (CurrentState != null)
            CurrentState.Execute(Owner);
    }
}
