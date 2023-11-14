namespace AmogAI.StateBehaviour;

// WalkTowardsEmergencyState
// DoEmergencyState

public class EmergencyStateMachine<Survivor> : StateMachine<Survivor> {

    public EmergencyStateMachine(Survivor owner) : base(owner) {
        CurrentState = new WalkTowardsEmergencyState<Survivor>();
        CurrentState.Enter(owner);
    } 
}
