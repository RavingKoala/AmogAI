namespace AmogAI.StateBehaviour;

using AmogAI.World.Entity;

public class SurvivorStateMachine  {
    public Survivor Owner { get; set; }
    public StateMachine StateMachine { get; set; }

    public SurvivorStateMachine(Survivor owner) {
        Owner = owner;
        StateMachine = new TaskStateMachine(owner);
    }

    public void ChangeStateMachine(StateMachine stateMachine) {
        StateMachine = stateMachine;
    }   

    public void Update(float timeDelta) {
        StateMachine.Update(timeDelta);
    }
}
