namespace AmogAI.StateBehaviour;

using AmogAI.World.Entity;

public class WalkTowardsEmergencyState : IState<Survivor> {
    public void Enter(Survivor s) {
        // calculate shortest path to emergency
    }

    public void Execute(Survivor s) {
        // walk towards emergency
    }

    public void Exit(Survivor s) {
        // start doing emergency -> transition to DoEmergencyState
        // or if emergency is done by another survivor -> transition to SeekTaskState
    }
}
