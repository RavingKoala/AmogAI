namespace AmogAI.StateBehaviour;

using AmogAI.World;

// GlobalTaskState
// GlobalEmergencyState

public class GlobalStateMachine : StateMachine<World> {
    public GlobalStateMachine(World owner) : base(owner) {
        CurrentState = new GlobalTaskState();
        CurrentState.Enter(owner);
    }
}
