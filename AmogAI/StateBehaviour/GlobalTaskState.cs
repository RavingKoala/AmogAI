namespace AmogAI.StateBehaviour;

using AmogAI.World;

public class GlobalTaskState : IState<World> {
    public void Enter(World s) {
        // move SurvivorStateMachine's statemachine to TaskStateMachine
    }

    public void Execute(World s) {
        // increment a timer for when an emergency should trigger
    }

    public void Exit(World s) {
        // move to GlobalEmergencyState
    }
}
