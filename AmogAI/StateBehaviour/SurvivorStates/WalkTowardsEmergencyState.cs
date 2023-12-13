namespace AmogAI.StateBehaviour.SurvivorStates;

using AmogAI.World.Entity;

public class WalkTowardsEmergencyState : IState<Survivor> {
    public void Enter(Survivor survivor) {
        // calculate shortest path to emergency
    }

    public void Execute(Survivor survivor, float timeDelta) {
        // walk towards emergency then turn down velocity/mass to 0

        // survivor.SurvivorStateMachine.StateMachine.ChangeState(new DoEmergencyState());
    }

    public void Exit(Survivor survivor) {
        Console.WriteLine("Gonna start working on the emergency!");
    }
}
