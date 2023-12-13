namespace AmogAI.StateBehaviour.SurvivorStates;

using AmogAI.World.Entity;

// TaskStateMachine
// EmergencyStateMachine

public class SurvivorStateMachine {
    public Survivor Owner { get; set; }
    public StateMachine<Survivor> StateMachine { get; set; }

    public SurvivorStateMachine(Survivor survivor) {
        Owner = survivor;
        StateMachine = new TaskStateMachine(survivor);
    }

    public void ChangeStateMachine(StateMachine<Survivor> stateMachine) {
        StateMachine = stateMachine;
    }

    public void Update(float timeDelta) {
        StateMachine.Update(timeDelta);
    }
}
