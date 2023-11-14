namespace AmogAI.StateBehaviour;

using AmogAI.World.Entity;

public class DoEmergencyState : IState<Survivor> {
    public void Enter(Survivor s) {
        // stop moving and start emergency
    }

    public void Execute(Survivor s) {
        // wait at task location until emergency is done (its timer is up)
    }

    public void Exit(Survivor s) {
        // transition to SeekTaskState
    }
}
