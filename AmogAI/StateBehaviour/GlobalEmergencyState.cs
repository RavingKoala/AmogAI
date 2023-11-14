namespace AmogAI.StateBehaviour;

using AmogAI.World;

public class GlobalEmergencyState : IState<World> {
    public void Enter(World s) {
        // move SurvivorStateMachine's statemachine to EmergencyStateMachine
    }

    public void Execute(World s) {
        // start timer for the emergency and how long before it fails
    }

    public void Exit(World s) {
        // if emergency is completed -> move to GlobalTaskState
        // if emergency is failed -> game is over
    }
}
