namespace AmogAI.StateBehaviour.WordStates;

using AmogAI.World;

// GlobalTaskState
// GlobalEmergencyState

public class GlobalStateMachine : StateMachine<World> {
    public GlobalStateMachine(World world) : base(world) {
        CurrentState = new GlobalTaskState();
        CurrentState.Enter(world);
    }
}
