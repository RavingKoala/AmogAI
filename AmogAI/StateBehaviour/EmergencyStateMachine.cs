namespace AmogAI.StateBehaviour;

// WalkTowardsEmergencyState
// DoEmergencyState

public class EmergencyStateMachine<Survivor> : StateMachine<Survivor> {

    public EmergencyStateMachine(Survivor owner) : base(owner) {
    }

    public override void Update(float timeDelta) {
        if (CurrentState != null)
            CurrentState.Execute(Owner);
    }   
}
