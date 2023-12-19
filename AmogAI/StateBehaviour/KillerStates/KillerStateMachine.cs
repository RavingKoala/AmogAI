namespace AmogAI.StateBehaviour.KillerStates;

using AmogAI.StateBehaviour.SurvivorStates;
using AmogAI.World.Entity;

public class KillerStateMachine : StateMachine<Killer> {

    public KillerStateMachine(Killer killer) : base(killer) {
        CurrentState = new WanderState();
        CurrentState.Enter(killer);
    }
}
