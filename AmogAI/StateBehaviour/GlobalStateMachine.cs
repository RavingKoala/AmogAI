namespace AmogAI.StateBehaviour;

using AmogAI.World;

// GlobalTaskState
// GlobalEmergencyState

public class GlobalStateMachine : StateMachine<World> {
    public GlobalStateMachine(World owner) : base(owner) {
    }

    public override void Update(float timeDelta) {
        if (CurrentState != null)
            CurrentState.Execute(Owner);
    }
}
