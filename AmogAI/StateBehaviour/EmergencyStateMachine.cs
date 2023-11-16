namespace AmogAI.StateBehaviour;

// WalkTowardsEmergencyState
// DoEmergencyState

using AmogAI.World.Entity;

public class EmergencyStateMachine : StateMachine<Survivor> {

    public EmergencyStateMachine(Survivor survivor) : base(survivor) {
        CurrentState = new WalkTowardsEmergencyState();
        CurrentState.Enter(survivor);
    } 
}
